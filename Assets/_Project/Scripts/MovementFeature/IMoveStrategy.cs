using UnityEngine;

namespace _Project.Scripts.MovementFeature
{
    public interface IMoveStrategy
    {
        void Move(GameObject gameObject);
    }
}