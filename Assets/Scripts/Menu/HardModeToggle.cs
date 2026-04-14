using UnityEngine;
using UnityEngine.UI;

public class HardModeToggle : MonoBehaviour
{
    [SerializeField] private Toggle hardModeToggle;
    [SerializeField] private MultiplyAI multiplyAI;

    private const int HardModeBonus = 2;

    private void Start()
    {
        hardModeToggle.onValueChanged.AddListener(OnToggleChanged);
    }

    private void OnDestroy()
    {
        hardModeToggle.onValueChanged.RemoveListener(OnToggleChanged);
    }

    private void OnToggleChanged(bool isOn)
    {
        if (isOn)
        {
            multiplyAI.aiValue = Mathf.Min(multiplyAI.aiValue + HardModeBonus, 10);
            Debug.Log($"[HardMode] Modo difícil activado — aiValue: {multiplyAI.aiValue}");
        }
        else
        {
            multiplyAI.aiValue = Mathf.Max(multiplyAI.aiValue - HardModeBonus, 1);
            Debug.Log($"[HardMode] Modo difícil desactivado — aiValue: {multiplyAI.aiValue}");
        }
    }
}