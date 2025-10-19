using System.Collections.Generic;
using _Project.Scripts.Physics;
using _Project.Scripts.World;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.MovementFeature
{
    public class StrategyMoveAgent : ITickable
    {
        private readonly WorldBoundsService _worldBoundsService;
        private List<MoveSubject> _moveSubjects = new();

        public StrategyMoveAgent(WorldBoundsService worldBoundsService)
        {
            _worldBoundsService = worldBoundsService;
        }
        
        public void AddMoveSubject(PhysicsBody body, IMoveStrategy moveStrategy, Transform target = null)
        {
            _moveSubjects.Add(new MoveSubject(body, moveStrategy, target));
        }


        public void Tick()
        {
            foreach (var moveSubject in _moveSubjects)
            {
                if(moveSubject == null) continue;
                moveSubject.Strategy.Move(moveSubject.Body);
                moveSubject.Body.Position += moveSubject.Body.TotalVelocity * Time.deltaTime;
                moveSubject.Body.Position = _worldBoundsService.WrapPosition(moveSubject.Body.Position);
            }
        }

        public void RemoveMoveSubject(PhysicsBody body)
        {
            _moveSubjects.RemoveAll(x => x.Body == body);
        }
    }
}