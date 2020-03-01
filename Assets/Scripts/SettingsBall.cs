using UnityEngine;

namespace PingPong
{
    [CreateAssetMenu(fileName = "SettingsBall", menuName = "CreateScriptableObject/SettingsBall", order = 0)]
    public sealed class SettingsBall : ScriptableObject
    {
        [Range(0.0f, 10.0f)] public float TimeWaitForStart = 3.0f;
        [SerializeField] private Vector2Int _minAndMaxVelocityForX;
        [SerializeField] private Vector2Int _minAndMaxVelocityForY;
        [SerializeField] private Vector2 _minAndMaxScale;
        [SerializeField] private Color[] _colors;

        public Color[] Colors => _colors;

        public float GetScaleRandom()
        {
            return Random.Range(_minAndMaxScale.x, _minAndMaxScale.y);
        }
        
        public Color GetColorRandom()
        {
            var colorRandom = Random.Range(0, Colors.Length - 1);
            return Colors[colorRandom];
        }

        public int GetVelocityXRandom()
        {
            return Random.Range(_minAndMaxVelocityForX.x, _minAndMaxVelocityForX.y);
        }

        public int GetVelocityYRandom()
        {
            return Random.Range(_minAndMaxVelocityForY.x, _minAndMaxVelocityForY.y);
        }
    }
}
