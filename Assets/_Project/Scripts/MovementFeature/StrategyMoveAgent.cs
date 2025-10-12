using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.MovementFeature
{
    public class StrategyMoveAgent : ITickable
    {
        private List<MoveSubject> _moveSubjects = new();
        
        public void AddMoveSubject(GameObject moveSubject, IMoveStrategy moveStrategy, Transform target = null)
        {
            _moveSubjects.Add(new MoveSubject(moveSubject, moveStrategy, target));
        }


        public void Tick()
        {
            foreach (var moveSubject in _moveSubjects)
            {
                if(moveSubject == null) continue;
                moveSubject.Strategy.Move(moveSubject.GameObject);
            }
        }

        public void RemoveMoveSubject(GameObject moveSubject)
        {
            _moveSubjects.RemoveAll(x => x.GameObject == moveSubject);
        }
    }
}