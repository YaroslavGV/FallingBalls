using UnityEngine;
using GameSession;

public class GameplayStatus : MonoBehaviour 
{
	[SerializeField] private TextIntTransition _healthText;
	[SerializeField] private TextIntTransition _scoreText;
	[Space]
	[SerializeField] private SessionStatus _status;

	private void OnEnable ()
    {
		SetCurrentNumbers();
	}

	private void Awake ()
	{
		_status.Reseted += OnReset;
		_status.HealthChanged += OnHealthChange;
		_status.ScoreChanged += OnScoreChange;
	}

	private void OnReset ()
	{
		SetCurrentNumbers();
	}

	private void OnHealthChange (int number)
	{
		_healthText.TransitionToNumber(_status.Score);
	}

	private void OnScoreChange (int number)
	{
		_scoreText.TransitionToNumber(_status.Score);
	}

	private void SetCurrentNumbers ()
    {
		_healthText.SetNumber(_status.Health);
		_scoreText.SetNumber(_status.Score);
	}
}
