using UnityEngine;

namespace _Project.Scripts.MovementFeature
{
    public class MoveSubject
    {
        public GameObject GameObject;
        public IMoveStrategy Strategy;
        public Transform Target;
            
        public MoveSubject(GameObject gameObject, IMoveStrategy strategy, Transform target = null)
        {
            GameObject = gameObject;
            Strategy = strategy;
            Target = target;
        }
    }
}