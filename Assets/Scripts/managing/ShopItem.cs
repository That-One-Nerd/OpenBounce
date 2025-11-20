using UnityEngine;
using UnityEngine.EventSystems;

public class ShopItem : MonoBehaviour, IPointerClickHandler
{
    public GameObject placeablePrefab; // Prefab to place on board

    public void OnPointerClick(PointerEventData eventData)
    {
        PlacementManager.Instance.BeginPlacement(placeablePrefab);
        GameStateManager.Instance.ReturnToBoardFromShop();

    }
}
