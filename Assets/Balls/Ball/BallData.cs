using UnityEngine;

namespace Balls
{
    public struct BallData
    {
        public readonly float Speed;
        public readonly int Score;
        public readonly int Damage;
        public readonly Color Color;

        public BallData (float speed, int score, int damage, Color color)
        {
            Speed = speed;
            Score = score;
            Damage = damage;
            Color = color;
        }
    }
}