using UnityEngine;
using UnityEngine.UI;

public class MultiplyAI : MonoBehaviour
{
    public enum EnemyState { Idle, OnDoor }

    [Header("AI Settings")]
    [Range(1, 10)]
    public int aiValue = 1;

    [Header("External References")]
    [SerializeField] private HealthSystem healthSystem;
    [SerializeField] private DoorController doorController;
    [SerializeField] private DistanceControl distanceControl;
    [SerializeField] private JumpscareController jumpscareController;
    [SerializeField] Image multiplyImage;

    private EnemyState currentState;
    private float timer;
    private int lastDistanceThreshold = 0;

    private void Start()
    {
        ChangeState(EnemyState.Idle);
    }

    private void Update()
    {
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
        if (doorController.isClosed)
        {
            Debug.Log("[Multiply] Puerta cerrada — el enemigo retrocede.");
            ChangeState(EnemyState.Idle);
            return;
        }

        if (timer <= 0f)
        {
            healthSystem.decreaseHealth();
            Debug.Log("[Multiply] ¡El enemigo daña al jugador!");
            //Llamar a la funcion de jumpsacare.
            jumpscareController.TriggerJumpscare(); 
            ChangeState(EnemyState.Idle);
        }
    }


    private void CheckDistanceMilestone()
    {
        int currentDistance = distanceControl.getDistanceDone();
        int currentThreshold = currentDistance / 50; //Valor recomendado: 500m

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
            //Muestra al bicho por pantalla.
            multiplyImage.enabled = true;
        }
        else if (newState == EnemyState.Idle)
        {
            //Ocultar al bicho.
            multiplyImage.enabled = false;   
        }

        ResetTimer(newState);
        Debug.Log($"[MultiplyAI] Nuevo estado: {newState}  |  Próximo timer: {timer:F2}s");
    }

    private void ResetTimer(EnemyState state)
    {
        if (state == EnemyState.OnDoor)
        {
            timer = 3f; //Valor recomendado, 5 segundos. 
        }
        else
        {
            //Valores recomendados para partida normal, entre 30 y 60 segundos.
            float random = Random.Range(20f, 40f);
            timer = random - aiValue;
        }
    }
}