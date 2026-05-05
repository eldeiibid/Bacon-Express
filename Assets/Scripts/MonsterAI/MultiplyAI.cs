using UnityEngine;
using UnityEngine.UI;

public class MultiplyAI : MonoBehaviour
{
    public enum EnemyState { Disabled, Idle, OnDoor }

    [Header("AI Settings")]
    [Range(0, 10)]
    public int aiValue = 1;

    [Header("External References")]
    [SerializeField] private HealthSystem healthSystem;
    [SerializeField] private DoorController doorController;
    [SerializeField] private DistanceControl distanceControl;
    [SerializeField] private JumpscareController jumpscareController;
    [SerializeField] Image multiplyImage;

    private EnemyState currentState;
    [Header("Debug")]
    public float timer;
    private int lastDistanceThreshold = 0;
    private const int DISTANCE_REQUIRED_TO_INCREASE_AI = 100; //Valor recomendado: 100m
    

    private void Start()
    {
        if (aiValue == 0)
        {
            ChangeState(EnemyState.Disabled);
        }
        else
        {
            ChangeState(EnemyState.Idle);
        }
    }

    private void Update()
    {
        if (currentState == EnemyState.Disabled) return;

        CheckDistanceMilestone();
        timer -= Time.deltaTime;

        switch (currentState)
        {
            case EnemyState.Idle:
                UpdateIdle();
                break;

            case EnemyState.OnDoor:
                UpdateOnDoor();
                break;
        }
    }


    private void UpdateIdle()
    {
        if (timer <= 0f)
        {
            ChangeState(EnemyState.OnDoor);
        }
    }


    private void UpdateOnDoor()
    {
        if (timer <= 0f)
        {
            healthSystem.decreaseHealth();
            Debug.Log("[Multiply] ¡El enemigo daña al jugador!");
            jumpscareController.TriggerJumpscare();
            ChangeState(EnemyState.Idle);
        }

        if (doorController.isClosed)
        {
            Debug.Log("[Multiply] Puerta cerrada — el enemigo retrocede.");
            ChangeState(EnemyState.Idle);
            return;
        }
    }


    private void CheckDistanceMilestone()
    {
        int currentDistance = distanceControl.getDistanceDone();
        int currentThreshold = currentDistance / DISTANCE_REQUIRED_TO_INCREASE_AI;

        if (currentThreshold > lastDistanceThreshold)
        {
            lastDistanceThreshold = currentThreshold;

            aiValue = Mathf.Min(aiValue + 1, 10);
            Debug.Log($"[Multiply] ¡Nuevo umbral de distancia! ({currentDistance}m) — aiValue sube a: {aiValue}");
            
        }
    }

  
    private void ChangeState(EnemyState newState)
    {
        currentState = newState;

        if (newState == EnemyState.OnDoor)
        {
            multiplyImage.enabled = true;
        }
        else
        {
            multiplyImage.enabled = false;
        }

        ResetTimer(newState);
        Debug.Log($"[MultiplyAI] Nuevo estado: {newState}  |  Próximo timer: {timer:F2}s");
    }

    private void ResetTimer(EnemyState state)
    {
        if (state == EnemyState.OnDoor)
        {
            timer = 5f;
        }
        else if (state == EnemyState.Idle)
        {
            float random = Random.Range(40f, 90f); //Rango recomendado; entre 40s y 120s.
            timer = random - aiValue*3;
        }
        else
        {
            // Si esta Disabled, el timer no importa, se deja en 0
            timer = 0f;
        }
    }
}