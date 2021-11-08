using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Balls
{
    public class FallOffscreenDetecter : MonoBehaviour
    {
        public Action<Ball> Detected;

        private bool _isInit;
        private List<Ball> _balls;
        private float _botY;
        private IEnumerator _detect;

        public void Initialize (List<Ball> balls)
        {
            if (_isInit)
                return;
            _balls = balls;
            Camera camera = Camera.main;
            _botY = -camera.orthographicSize;
            _isInit = true;
        }

        public void UpdatePass (float delta)
        {
            for (int i = _balls.Count-1; i > -1; i--)
            {
                Ball ball = _balls[i];
                if (ball.transform.localPosition.y < _botY-ball.Radius)
                    Detect(ball);
            }
        }

        private void Detect (Ball ball)
        {
            Detected?.Invoke(ball);
        }
    }
}