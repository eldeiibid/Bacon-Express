using UnityEngine;
using UnityEngine.UI;

public class LongstepperAI : MonoBehaviour
{
    public enum EnemyState { Disabled, Idle, OnWindow }

    [Header("AI Settings")]
    [Range(0, 10)]
    public int aiValue = 1;

    [Header("External References")]
    [SerializeField] private HealthSystem healthSystem;
    [SerializeField] private TrackAnim trackAnim;
    [SerializeField] private DistanceControl distanceControl;
    [SerializeField] private JumpscareController jumpscareController;
    [SerializeField] Image longstepperImage;

    private EnemyState currentState;
    [Header("Debug")]
    public float timer;
    private int lastDistanceThreshold = 0;
    private const int DISTANCE_REQUIRED_TO_INCREASE_AI = 200; //Valor recomendado: 100m
    

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

            case EnemyState.OnWindow:
                UpdateOnWindow();
                break;
        }
    }


    private void UpdateIdle()
    {
        if (timer <= 0f)
        {
            ChangeState(EnemyState.OnWindow);
        }
    }


    private void UpdateOnWindow()
    {
        if (timer <= 0f)
        {
            healthSystem.decreaseHealth();
            Debug.Log("[Longstepper] ¡El enemigo daña al jugador!");
            jumpscareController.TriggerJumpscare();
            ChangeState(EnemyState.Idle);
        }

        if (trackAnim.GetSpeed() == 0)
        {
            Debug.Log("[Longstepper] El tren se ha parado, el enemigo se va.");
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
            Debug.Log($"[Longstepper] ¡Nuevo umbral de distancia! ({currentDistance}m) — aiValue sube a: {aiValue}");
            
        }
    }

  
    private void ChangeState(EnemyState newState)
    {
        currentState = newState;

        if (newState == EnemyState.OnWindow)
        {
            longstepperImage.enabled = true;
        }
        else
        {
            longstepperImage.enabled = false;
        }

        ResetTimer(newState);
        Debug.Log($"[LongstepperAI] Nuevo estado: {newState}  |  Próximo timer: {timer:F2}s");
    }

    private void ResetTimer(EnemyState state)
    {
        if (state == EnemyState.OnWindow)
        {
            timer = 6f;
        }
        else if (state == EnemyState.Idle)
        {
            float random = Random.Range(40f, 90f); //Rango recomendado; entre 40s y 90s.
            timer = random - aiValue*2;
        }
        else
        {
            // Si esta Disabled, el timer no importa, se deja en 0
            timer = 0f;
        }
    }
}
