using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tile : MonoBehaviour, IPointerClickHandler
{
    public delegate void OnPlayerTileClicked(Tile tileClicked);
    public static OnPlayerTileClicked onPlayerTileClicked;

    public void OnPointerClick(PointerEventData eventData)
    {
        // Use this to find a path for the player
        onPlayerTileClicked?.Invoke(this);
    }
}
