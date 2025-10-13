using _Project.Scripts.Configs;
using UnityEngine;

namespace _Project.Scripts.Shooting
{
    public class LaserView : MonoBehaviour
    {
        [SerializeField] private LineRenderer _lineRenderer;
        private float _maxDistance;

        private void Awake()
        {
            if (_lineRenderer == null)
                _lineRenderer = GetComponent<LineRenderer>();

            _lineRenderer.enabled = false;
        }

        public void Initialize(LaserConfig config)
        {
            _maxDistance = config.MaxDistance;
            _lineRenderer.startColor = config.LaserColor;
            _lineRenderer.endColor = config.LaserColor;
            _lineRenderer.startWidth = config.LaserWidth;
        }

        public void Show(Vector2 startPos, Vector2 direction)
        {
            _lineRenderer.enabled = true;

            Vector2 endPos = startPos + direction.normalized * _maxDistance;

            _lineRenderer.SetPosition(0, startPos);
            _lineRenderer.SetPosition(1, endPos);
        }

        public void Hide()
        {
            _lineRenderer.enabled = false;
        }
    }
}