using UnityEngine;

public interface IMovableAlongPath
{
    void Move(Vector3 movePos);
    bool HasReachPos();
}
