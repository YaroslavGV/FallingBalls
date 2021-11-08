using System;
using UnityEngine;
using UnityEngine.UI;

namespace Leaderboards
{
	public class GameResultView : MonoBehaviour
	{
		[SerializeField] private string _scoreFormat = "N0";
		[SerializeField] private string _dateFormat = "HH:mm dd:MM:y";
		[Space]
		[SerializeField] private string _missIndex = "-";
		[SerializeField] private string _missScore = "--- --- ---";
		[SerializeField] private string _missDate = "--:-- --:--:--";
		[Space]
		[SerializeField] private Text _index;
		[SerializeField] private Text _score;
		[SerializeField] private Text _date;

		public void SetMissData ()
		{
			_index.text = _missIndex;
			_score.text = _missScore;
			_date.text = _missDate;
		}

		public void SetData (GameResult date, int index)
		{
			_index.text = index.ToString();
			_score.text = date.Score.ToString(_scoreFormat);
			DateTime dateTime = date.GetDateTime();
			_date.text = dateTime.ToString(_dateFormat);
		}

		public void SetColor (Color color)
		{
			_index.color = color;
			_score.color = color;
			_date.color = color;
		}
	}
}
