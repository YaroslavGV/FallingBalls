using System;
using UnityEngine;

namespace Balls
{
    public class BallsSpawnTimer : MonoBehaviour, IBallSpawnTimer
    {
        public event Action TimeOut;

		[SerializeField] private float _timerTarget = 2;
		[Tooltip("Addition multiplyer per spawn")]
		[SerializeField] private float _timerSpeedAcceleration = 0.1f;
		private float _timer;
		private float _multiplayer;

        public override string ToString ()
        {
            string timerTarget = "Timer Target: "+_timerTarget;
            string speedAcceleration = "Timer Speed Acceleration: "+_timerSpeedAcceleration;
            string currentTimer = "Current Timer: "+_timer;
            string currentMultiplayer = "Current Multiplayer "+_multiplayer;
            return timerTarget+"\n"+speedAcceleration+"\n"+currentTimer+"\n"+currentMultiplayer;
        }

        public void Reset ()
        {
            _timer = _timerTarget;
            _multiplayer = 1;
        }

        public void UpdatePass (float delta)
        {
            _timer += delta*_multiplayer;
            while (_timer > _timerTarget)
            {
                _timer -= _timerTarget;
                _multiplayer += _timerSpeedAcceleration;
                TimeOut?.Invoke();
            }
        }

        public void OnCatch ()
        {

        }

        [ContextMenu("Log")]
        private void Log ()
        {
            Debug.Log(this.ToString());
        }
    }
}