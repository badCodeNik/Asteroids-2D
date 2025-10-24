using UnityEngine;

namespace _Project.Scripts.Physics
{
    public abstract class PhysicsBody : MonoBehaviour
    {
        [SerializeField] private float _collisionRadius;
        [SerializeField] private float _externalVelocityDrag = 0.98f;


        public Vector2 Position
        {
            get => transform.position;
            set => transform.position = value;
        }

        public float Rotation
        {
            get => transform.eulerAngles.z;
            set => transform.rotation = Quaternion.Euler(0f, 0f, value);
        }

        public Vector2 Velocity { get; set; }
        public Vector2 ExternalVelocity { get; set; }

        public float Radius => _collisionRadius;
        public Vector2 TotalVelocity => Velocity + ExternalVelocity;
        public virtual PhysicsBodyType BodyType { get; set; }
        public bool IsActive { get; } = true;
        public bool CanCollide => _canCollide && !_isInvulnerable;
        private bool _canCollide = true;
        private bool _isInvulnerable;

        public virtual void ApplyForce(Vector2 force)
        {
            ExternalVelocity += force;
        }
        
        public void SetCanCollide(bool canCollide) => _canCollide = canCollide;

        private void Update()
        {
            if(ExternalVelocity.magnitude > 0.01f)
            {
                ExternalVelocity *= _externalVelocityDrag;

                if (ExternalVelocity.magnitude < 0.01f)
                {
                    ExternalVelocity = Vector2.zero;
                }
            }
        }


        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, _collisionRadius);

            if (Application.isPlaying && Velocity.magnitude > 0.01f)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawRay(transform.position, Velocity.normalized);
            }
        }
    }
}