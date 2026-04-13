using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


//Sistema de la puerta, solo se cierra mientras se mantenga clickado el boton
public class DoorController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Image doorImage;
    [SerializeField] private Button doorButton;

    public bool isClosed = false;

    private void Start()
    {
        SetDoorState(false);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        SetDoorState(true);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        SetDoorState(false);
    }

    private void SetDoorState(bool state)
    {
        isClosed = state;
        doorImage.enabled = state;
    }
}