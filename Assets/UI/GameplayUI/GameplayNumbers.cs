using UnityEngine;
using GameSession;
using Balls;
using System;

public class GameplayNumbers : MonoBehaviour 
{
	[SerializeField] private FlashNumbers _scoreNumbers;
	[SerializeField] private FlashNumbers _damageNumbers;
	[SerializeField] private RectTransform _damagePoint;
	[Space]
	[SerializeField] private FallingBalls _balls;
	[SerializeField] private Session _session;

	private void Awake ()
    {
		_scoreNumbers.Initialize();
		_damageNumbers.Initialize();
		_balls.Catched += OnCatch;
		_balls.Falled += OnFall;
		_session.Reseted += OnReset;
	}

    private void OnReset ()
    {
		_scoreNumbers.RemoveAll();
		_damageNumbers.RemoveAll();
	}

    private void OnCatch (Ball ball)
    {
		_scoreNumbers.PlayWorld(ball.Data.Score, ball.transform.position);

	}

	private void OnFall (Ball ball)
	{
		Vector2 position = ball.transform.position;
		Vector2 localPosition = _damageNumbers.WorldToLocal(position);
		localPosition.y = _damagePoint.anchoredPosition.y;
		_damageNumbers.PlayLocal(ball.Data.Damage, localPosition);
	}
}
