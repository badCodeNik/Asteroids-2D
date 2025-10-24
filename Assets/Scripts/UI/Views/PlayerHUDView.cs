using _Project.Scripts.UI.ViewModels;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Project.Scripts.UI.Views
{
    public class PlayerHUDView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _coordinatesText;
        [SerializeField] private TMP_Text _angleText;
        [SerializeField] private TMP_Text _immediateSpeedText;
        [SerializeField] private TMP_Text _laserAmmoText;
        [SerializeField] private TMP_Text _laserCooldownText;
        
        [Header("Health")]
        [SerializeField] private Image[] _hearts;
        [SerializeField] private GameObject _heartPrefab;
        [SerializeField] private Transform _healthContainer;
        private PlayerHUDViewModel _viewModel;


        [Inject]
        public void Init(PlayerHUDViewModel viewModel)
        {
            _viewModel = viewModel;
            _viewModel.Coordinates.Subscribe(SetCoordinates);
            _viewModel.ShipAngle.Subscribe(SetShipAngle);
            _viewModel.ImmediateSpeed.Subscribe(SetImmediateSpeed);
            _viewModel.LaserCooldown.Subscribe(SetLaserCooldown);
            _viewModel.LaserAmmo.Subscribe(SetAmmo);
            _viewModel.Health.Subscribe(SetHealthView);
            InitHealth(viewModel);
        }

        private void InitHealth(PlayerHUDViewModel viewModel)
        {
            var hearts = new Image[viewModel.Health.Value];
            for (var i = 0; i < viewModel.Health.Value; i++)
            {
                var heart = Instantiate(_heartPrefab, _healthContainer);
                hearts[i] = heart.GetComponent<Image>();;
            }
            _hearts = hearts;
        }


        private void SetHealthView(int health)
        {
            for (var i = 0; i < _hearts.Length; i++)
            {
                _hearts[i].gameObject.SetActive(i < health);
            }
        }
        private void SetLaserCooldown(float cooldown)
        {
            _laserCooldownText.text = $"Время восстановления заряда лазера : {cooldown} ";
        }
        private void SetImmediateSpeed(int speed)
        {
            _immediateSpeedText.text = $"Мнгновенная скорость : {speed}";
        }
        private void SetAmmo(int ammo)
        {
            _laserAmmoText.text = $"Осталось {ammo} зарядов лазера";
        }
        private void SetShipAngle(float angle)
        {
            _angleText.text = $"Поворот корябля : {angle:F1} ";
        }
        private void SetCoordinates(Vector2 pos)
        {
            _coordinatesText.text = $"Координаты: X: {pos.x:F1}, Y: {pos.y:F1}";
        }

    }
}