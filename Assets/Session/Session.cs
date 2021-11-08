using System;
using UnityEngine;
using Balls;

namespace GameSession
{
    public class Session : MonoBehaviour
    {
        public Action Reseted;

        [SerializeField] private SessionStatus _status;
        [SerializeField] private FallingBalls _balls;
        [Space]
        [SerializeField] private SessionStages _stages;

        private void Start ()
        {
            _stages.Initialize();
            _status.Initialize(LiderboardMenu);
            _balls.Initialize();
            _balls.Catched += OnCatch;
            _balls.Falled += OnFall;
            
            MainMenu();
        }

        [ContextMenu("MainMenu (Runtime)")]
        public void MainMenu ()
        {
            _stages.SetState(typeof(MainMenuState));
        }

        [ContextMenu("Liderboard (Runtime)")]
        public void LiderboardMenu ()
        {
            _stages.SetState(typeof(LiderboardState));
        }

        [ContextMenu("StartSession (Runtime)")]
        public void StartSession ()
        {
            Reset();
            _stages.SetState(typeof(GameplayState));
        }

        [ContextMenu("Pause (Runtime)")]
        public void Pause ()
        {
            _stages.SetState(typeof(PauseState));
        }

        [ContextMenu("Unpause (Runtime)")]
        public void Unpause ()
        {
            _stages.SetState(typeof(GameplayState));
        }

        public void Reset ()
        {
            _status.Reset();
            _balls.Reset();
            Reseted?.Invoke();
        }

        private void OnCatch (Ball ball)
        {
            _status.EarnScore(ball.Data.Score);
        }

        private void OnFall (Ball ball)
        {
            _status.ApplyDamage(ball.Data.Damage);
        }
    }
}
