using TMPro;
using UnityEngine;

namespace _Project.Scripts.UI.Views
{
    public class PlayerHUDView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _coordinatesText;
        [SerializeField] private TMP_Text _angleText;
        [SerializeField] private TMP_Text _immediateSpeedText;
        [SerializeField] private TMP_Text _laserAmmoText;
        [SerializeField] private TMP_Text _laserCooldownText;

        public TMP_Text CoordinatesText => _coordinatesText;

        public TMP_Text AngleText => _angleText;

        public TMP_Text ImmediateSpeedText => _immediateSpeedText;

        public TMP_Text LaserAmmoText => _laserAmmoText;

        public TMP_Text LaserCooldownText => _laserCooldownText;
    }
}