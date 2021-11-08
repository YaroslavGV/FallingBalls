using System;
using UnityEngine;

namespace Balls
{

    public interface IBallDataGenerator
    {
        BallData GetData ();
    }

    public class BallsSpawner : MonoBehaviour, IBallSpawner
    {
        [SerializeField] private float _additionSpeedPerSpawn = 0.1f;
        [RequireInterface(typeof(IBallDataGenerator))]
        [SerializeField] private GameObject _ballDataGeneratorConteiner;
        private bool _isInit;
        private Func<Ball> _getNewBall;
        private float _topY;
        private float _halfWidth;
        private IBallDataGenerator _ballDataGenerator;
        private float _additionSpeed;

        public void Initialize (Func<Ball> getNewBall)
        {
            if (_isInit)
                return;
            _getNewBall = getNewBall;
            _ballDataGenerator = _ballDataGeneratorConteiner.GetComponent<IBallDataGenerator>();
            Camera camera = Camera.main;
            _topY = camera.orthographicSize;
            _halfWidth = (camera.orthographicSize/(float)Screen.height)*(float)Screen.width;
            _isInit = true;
        }

        public void Reset ()
        {
            _additionSpeed = 0;
        }

        [ContextMenu("Spawn Ball")]
        public void SpawnBall ()
        {
            BallData data = _ballDataGenerator.GetData();
            Ball ball = _getNewBall();
            float radius = ball.Radius;
            float offset = _halfWidth-radius;
            float x = UnityEngine.Random.Range(-offset, offset);
            ball.transform.position = new Vector3(x, _topY+radius, 0);
            ball.SetData(data);
            ball.SetAdditionSpeed(_additionSpeed);
            _additionSpeed += _additionSpeedPerSpawn;
        }
    }
}