using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class JumpscareController : MonoBehaviour
{
    [SerializeField] private Image jumpscareImage;

    [Header("Jumpscare Settings")]
    [SerializeField] private float displayDuration = 0.5f;
    [SerializeField] private float flashSpeed = 0.05f;
    [SerializeField] private int flashCount = 3;

    private void Start()
    {
        jumpscareImage.enabled = false;
    }

    public void TriggerJumpscare()
    {
        StartCoroutine(JumpscareRoutine());
    }

    private IEnumerator JumpscareRoutine()
    {
        jumpscareImage.enabled = true;

        // Flashes rápidos al aparecer
        for (int i = 0; i < flashCount; i++)
        {
            jumpscareImage.enabled = false;
            yield return new WaitForSeconds(flashSpeed);
            jumpscareImage.enabled = true;
            yield return new WaitForSeconds(flashSpeed);
        }

        // Espera el tiempo de display y se oculta
        yield return new WaitForSeconds(displayDuration);
        jumpscareImage.enabled = false;
    }
}