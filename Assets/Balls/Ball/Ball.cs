using System;
using UnityEngine;

namespace Balls
{
	public class Ball : MonoBehaviour
	{
		public event Action<BallData> DataSetted;

		[SerializeField] private float _radius = 0.5f;
		[Tooltip("Percent of start speed")]
		[SerializeField] private float _acceleration = 0.01f;
		private Transform _transform;
		private BallData _data;
		private float _additionSpeed;
		private float _startSpeed;

		public float Radius => _radius;
		public BallData Data => _data;

		private void Awake ()
		{
			_transform = transform;
			SetRadius(_radius);
		}

		public void UpdateFall (float delta)
		{
			_additionSpeed += _startSpeed*_acceleration*delta;
			float moveDelta = (_startSpeed+_additionSpeed)*delta;
			Vector3 position = _transform.localPosition;
			position.y -= moveDelta;
			_transform.localPosition = position;
		}

		public void SetData (BallData data)
		{
			_data = data;
			_startSpeed = _data.Speed;
			_additionSpeed = 0;
			DataSetted?.Invoke(_data);
		}

		public void SetAdditionSpeed (float speed)
        {
			_startSpeed = _data.Speed+speed;
			_additionSpeed = 0;
		}

		public void SetRadius (float radius)
		{
			_radius = radius;
			float scale = _radius*2f;
			_transform.localScale = new Vector3(scale, scale, scale);
		}
	}
}
