using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tile : MonoBehaviour, IPointerClickHandler
{
    public delegate void OnPlayerTileClicked(Tile tileClicked);
    public static OnPlayerTileClicked onPlayerTileClicked;

    public IMovableAlongPath MovableOnTile { get; private set; }

    public Vector3 GetPosition()
    {
        return transform.position + Vector3.up * 1.5f;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // Use this to find a path for the player
        onPlayerTileClicked?.Invoke(this);
    }
}
