using UnityEngine;

public interface IMovableAlongPath
{
    bool CanWalkOn(IWalkable walkable);
    void Move(Vector3 movePos);
    bool HasReachPos();
}
