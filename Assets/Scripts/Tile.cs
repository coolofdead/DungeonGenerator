using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tile : MonoBehaviour, IPointerClickHandler, IWalkable
{
    public delegate void OnPlayerTileClicked(Tile tileClicked);
    public static OnPlayerTileClicked onPlayerTileClicked;

    public IMovableAlongPath MovableOnTile { get; private set; }
    private IEnumerable<Tile> neighbours;

    public virtual void OnMovableArrive(IMovableAlongPath movable)
    {
    }

    public virtual void OnMovableStopOnIt(IMovableAlongPath movable)
    {
        (movable as MonoBehaviour).transform.SetParent(transform);
        MovableOnTile = movable;
    }

    public virtual void OnMovableLeaveTile(IMovableAlongPath movable)
    {
        (movable as MonoBehaviour).transform.SetParent(null);
        MovableOnTile = null;
    }

    public void SetNeighbours(IEnumerable<Tile> neighbours)
    {
        this.neighbours = neighbours;
    }

    public IEnumerable<IWalkable> GetNeighbours()
    {
        return neighbours;
    }

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
