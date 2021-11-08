using System;
using System.Collections.Generic;
using UnityEngine;

namespace Balls
{
    public class BallsCatcher : MonoBehaviour
    {
        public event Action<Ball> Catched;
        private bool _isInit;
        private List<Ball> _balls;
        private Camera _camera;
        private Plane _plane;
        
        [ContextMenu("Initialize")]
        public void Initialize (List<Ball> balls)
        {
            if (_isInit)
                return;
            _balls = balls;
            _camera = Camera.main;
            _plane = new Plane(Vector3.back, Mathf.Abs(_camera.transform.position.z*2f));
            _isInit = true;
        }

        public void UpdatePass (float delta)
        {
            if (Input.GetMouseButtonDown(0))
                OnClick(Input.mousePosition);
        }

        private void OnClick (Vector3 position)
        {
            Ray ray = _camera.ScreenPointToRay(position);
            float enter = 0.0f;
            if (_plane.Raycast(ray, out enter))
            {
                Vector3 hitPoint = ray.GetPoint(enter);
                ChackHitBalls(hitPoint);
            }
        }

        private void ChackHitBalls (Vector3 position)
        {
            for (int i = _balls.Count-1; i > -1; i--)
            {
                Ball ball = _balls[i];
                float distance = Vector2.Distance(position, ball.transform.position);
                if (distance < ball.Radius)
                    Catch(ball);
            }
        }

        private void Catch (Ball ball)
        {
            Catched?.Invoke(ball);
        }
    }
}