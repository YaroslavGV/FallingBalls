using UnityEngine;

namespace Balls
{
    public class BallDataGenerator : MonoBehaviour, IBallDataGenerator
    {
        [SerializeField] private float _minSpeed = 5;
        [SerializeField] private float _maxSpeed = 10;
        [Space]
        [SerializeField] private int _minScore = 1;
        [SerializeField] private int _maxScore = 5;
        [SerializeField] private int _scoreMultiplyer = 10;
        [Space]
        [SerializeField] private int _minDamage = 1;
        [SerializeField] private int _maxDamage = 3;
        [SerializeField] private int _damageMultiplyer = 1;
        [Space]
        [Range(0, 1)]
        [SerializeField] private float _colorValue = 1;
        [Range(0, 1)]
        [SerializeField] private float _colorSaturation = 1;

        public BallData GetData ()
        {
            float speed = UnityEngine.Random.Range(_minSpeed, _maxSpeed);
            int score = UnityEngine.Random.Range(_minScore, _maxScore)*_scoreMultiplyer;
            int damage = UnityEngine.Random.Range(_minDamage, _maxDamage)*_damageMultiplyer;
            float hue = UnityEngine.Random.Range(0f, 1f);
            Color color = Color.HSVToRGB(hue, _colorSaturation, _colorValue);
            return new BallData(speed, score, damage, color);
        }
    }
}