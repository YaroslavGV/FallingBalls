using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Balls
{
    public interface IBallSpawnTimer
    {
        event Action TimeOut;
        void Reset ();
        void UpdatePass (float delta);
        void OnCatch ();
    }

    public interface IBallSpawner
    {
        void Initialize (Func<Ball> getNewBall);
        void Reset ();
        void SpawnBall ();
    }

    public class FallingBalls : MonoBehaviour
    {
        public event Action<Ball> Falled;
        public event Action<Ball> Catched;

        [SerializeField] private BallsPool _pool;
        [RequireInterface(typeof(IBallSpawnTimer))]
        [SerializeField] private GameObject _timerContainer;
        [RequireInterface(typeof(IBallSpawner))]
        [SerializeField] private GameObject _spawnerContainer;
        [SerializeField] private FallOffscreenDetecter _offscreen;
        [SerializeField] private BallsCatcher _catcher;
        private IBallSpawnTimer _timer;
        private IBallSpawner _spawner;
        private bool _isInit;
        private List<Ball> _balls;
        private IEnumerator _fallProcess;

        public bool InProcess => _fallProcess != null;

        [ContextMenu("Initialize (Runtime)")]
        public void Initialize ()
        {
            if (_isInit)
                return;
            _pool.Initialize();
            _balls = new List<Ball>();

            _spawner = _spawnerContainer.GetComponent<IBallSpawner>();
            _spawner.Initialize(SpawnBall);

            _timer = _timerContainer.GetComponent<IBallSpawnTimer>();
            _timer.Reset();
            _timer.TimeOut += _spawner.SpawnBall;

            _offscreen.Initialize(_balls);
            _offscreen.Detected += OnFallOffscreen;

            _catcher.Initialize(_balls);
            _catcher.Catched += OnCatch;

            _isInit = true;
        }

        [ContextMenu("Reset (Runtime)")]
        public void Reset ()
        {
            _timer.Reset();
            _spawner.Reset();
            for (int i = _balls.Count-1; i > -1; i--)
                Remove(_balls[i]);
            TryStopFall();
        }

        [ContextMenu("Start Fall (Runtime)")]
        public bool TryStartFall ()
        {
            if (InProcess)
                return false;
            _fallProcess = FallProcess();
            StartCoroutine(_fallProcess);
            return true;
        }

        [ContextMenu("Stop Fall (Runtime)")]
        public bool TryStopFall ()
        {
            if (InProcess == false)
                return false;
            StopCoroutine(_fallProcess);
            _fallProcess = null;
            return true;
        }

        private IEnumerator FallProcess ()
        {
            while (true)
            {
                UpdatePass();
                yield return null;
            }
        }

        private void UpdatePass ()
        {
            float delta = Time.deltaTime;
            foreach (var ball in _balls)
                ball.UpdateFall(delta);
            _timer.UpdatePass(delta);
            _offscreen.UpdatePass(delta);
            _catcher.UpdatePass(delta);
        }

        private Ball SpawnBall ()
        {
            Ball ball = _pool.GetElement();
            _balls.Add(ball);
            return ball;
        }

        private void OnFallOffscreen (Ball ball)
        {
            Falled?.Invoke(ball);
            Remove(ball);
        }

        private void OnCatch (Ball ball)
        {
            _timer.OnCatch();
            Catched?.Invoke(ball);
            Remove(ball);
        }

        private void Remove (Ball ball)
        {
            _balls.Remove(ball);
            _pool.ReturnToPull(ball);
        }
    }
}