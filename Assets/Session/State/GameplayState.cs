using UnityEngine;
using Balls;

namespace GameSession
{
    public class GameplayState : SessionState
    {
        [SerializeField] private GameObject _ui;
        [Space]
        [SerializeField] private FallingBalls _balls;
        [SerializeField] private ParticleEffects _catchEffects;
        
        private void Start ()
        {
            _catchEffects.Initialize();
            _balls.Catched += OnCatch;
        }

        public override void Enter ()
        {
            _balls.TryStartFall();
            _ui.SetActive(true);
        }

        public override void Exit ()
        {
            _balls.TryStopFall();
            _ui.SetActive(false);
        }

        private void OnCatch (Ball ball)
        {
            _catchEffects.Play(ball.transform.position, ball.Data.Color);
        }
    }
}