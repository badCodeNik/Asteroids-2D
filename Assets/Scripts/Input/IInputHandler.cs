using UnityEngine;

namespace _Project.Scripts.Input
{
    public interface IInputHandler
    {
        Vector2 GetMovement();
        bool IsShooting();
        bool IsShootingLaser();
        
    }
}
