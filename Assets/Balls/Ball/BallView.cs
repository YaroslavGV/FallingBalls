using System;
using UnityEngine;

namespace Balls
{
	[RequireComponent(typeof(Ball))]
	public class BallView : MonoBehaviour
	{
		[RequireInterface(typeof(BallData))]
		[SerializeField] private GameObject _skinTemplate;
		private Ball _ball;
		private IBallSkin _skin;

		private void Awake ()
		{
			_ball = GetComponent<Ball>();
			_ball.DataSetted += OnSetData;
			IBallSkin template = _skinTemplate.GetComponent<IBallSkin>();
			SetSkin(template);
		}

		public void SetSkin (IBallSkin template)
		{
			if (template == null)
				throw new ArgumentException("skin template is null");
			if (_skin != null)
				_skin.Destroy();
			_skin = template.Create(transform);
		}

		public void OnSetData (BallData data)
		{
			_skin.SetColor(data.Color);
		}
	}
}