using UnityEngine;

namespace _Project.Scripts.Configs
{
    [CreateAssetMenu(fileName = "WorldConfig", menuName = "Configs/WorldConfig")]
    public class WorldConfig : ScriptableObject
    {
        public float WorldWidth = 1000f;
        public float WorldHeight = 1000f;
    }
}