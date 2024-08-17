using UnityEngine;
using UnityEngine.EventSystems;

public class BackgroundClick : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        GameManager.Instance._gameUi.SpendEnergy(null);
    }
}
