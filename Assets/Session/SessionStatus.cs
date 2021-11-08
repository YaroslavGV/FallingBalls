using System;
using UnityEngine;
using Leaderboards;

namespace GameSession
{
    public class SessionStatus : MonoBehaviour
    {
        public event Action Reseted;
        public event Action<int> HealthChanged;
        public event Action<int> ScoreChanged;

        [SerializeField] private int _baseHealh = 10;
        [SerializeField] private Leaderboard _leaderboards;
        private Action _onLose;

        public int Health { get; private set; }
        public int Score { get; private set; }

        public void Initialize (Action onLose)
        {
            _leaderboards.Initialize();
            Reset();
            _onLose = onLose;
        }

        public void Reset ()
        {
            Health = _baseHealh;
            Score = 0;

            Reseted?.Invoke();
        }

        public void EarnScore (int score)
        {
            Score += score;
            ScoreChanged?.Invoke(Score);
        }

        public void ApplyDamage (int damage)
        {
            Health += -damage;
            if (Health < 0)
                Health = 0;
            if (Health == 0)
                Lose();
            HealthChanged?.Invoke(Score);
        }

        private void Lose ()
        {
            GameResult result = new GameResult(Score, DateTime.Now);
            _leaderboards.AddResult(result);
            _onLose?.Invoke();
        }
    }
}