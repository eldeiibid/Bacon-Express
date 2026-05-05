using UnityEngine;
using UnityEngine.UI;

public class BreakAI : MonoBehaviour
{
    public enum EnemyState { Disabled, Idle, OnRails }

    [Header("AI Settings")]
    [Range(0, 10)]
    public int aiValue = 1;

    [Header("External References")]
    [SerializeField] private HealthSystem healthSystem;
    [SerializeField] private DistanceControl distanceControl;
    [SerializeField] private JumpscareController jumpscareController;
    [SerializeField] BreakBillboard breakBillboard;

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
        //si esta en los raíles o desactivado; dejar de actualizar el timer.
        if (currentState == EnemyState.Disabled || currentState == EnemyState.OnRails) return;

        CheckDistanceMilestone();
        timer -= Time.deltaTime;

        switch (currentState)
        {
            case EnemyState.Idle:
                UpdateIdle();
                break;

            case EnemyState.OnRails:
                UpdateOnRails();
                break;
        }
    }


    private void UpdateIdle()
    {
        if (timer <= 0f)
        {
            ChangeState(EnemyState.OnRails);
            breakBillboard.hasAttacked = false;
            Debug.Log($"[BreakAI] El enemigo se ha ido a las vías!!");
        }
    }


    private void UpdateOnRails()
    {
        //Debes hacer refrencia al billboard y esperar su llamada.
        return;
    }

    private void CheckDistanceMilestone()
    {
        int currentDistance = distanceControl.getDistanceDone();
        int currentThreshold = currentDistance / DISTANCE_REQUIRED_TO_INCREASE_AI;

        //Al llegar a cierta distancia, sube 1 el nivel de IA.
        if (currentThreshold > lastDistanceThreshold)
        {
            lastDistanceThreshold = currentThreshold;

            aiValue = Mathf.Min(aiValue + 1, 10);
            Debug.Log($"[Break] ¡Nuevo umbral de distancia! ({currentDistance}m) — aiValue sube a: {aiValue}");
            
        }
    }

  
    public void ChangeState(EnemyState newState)
    {
        currentState = newState;

        if (newState == EnemyState.OnRails)
        {
            breakBillboard.enabled = true;
        }
        else
        {
            breakBillboard.enabled = false;
        }

        ResetTimer(newState);
        Debug.Log($"[BreakAI] Nuevo estado: {newState}  |  Próximo timer: {timer:F2}s");
    }

    public void Jumpscare()
    {
        //restar vida al enemigo e iniciar jumpscare.
        healthSystem.decreaseHealth();
        Debug.Log("[Break] ¡El enemigo daña al jugador!");
        jumpscareController.TriggerJumpscare();
 
    }

    private void ResetTimer(EnemyState state)
    {
        if (state == EnemyState.Idle)
        {
            float random = Random.Range(40f, 120f); //Rango recomendado; entre 40 y 120s.
            timer = random - aiValue*3;
        }
        else
        {
            // Si esta Disabled o en los raíles, el timer no importa, se deja en 0
            timer = 0f;
        }
    }
}