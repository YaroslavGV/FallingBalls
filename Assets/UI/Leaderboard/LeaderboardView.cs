using UnityEngine;

namespace Leaderboards
{
    public class LeaderboardView : MonoBehaviour
    {
        [SerializeField] private Color _hightlightCurrentResult = Color.cyan;
        [SerializeField] private Leaderboard _leaderboard;
        [SerializeField] private GameResultView[] _gameResults;

        private void OnEnable ()
        {
            UpdateList();
        }

        public void UpdateList ()
        {
            foreach (var elements in _gameResults)
            {
                elements.SetMissData();
                elements.SetColor(Color.white);
            }
            int index = 0;
            foreach (var result in _leaderboard.Results)
            {
                if (index >= _gameResults.Length)
                    return;
                _gameResults[index].SetData(result, index+1);
                if (result.Date == _leaderboard.LastResult.Date)
                    _gameResults[index].SetColor(_hightlightCurrentResult);
                index++;
            }
        }
    }
}