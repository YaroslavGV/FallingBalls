using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace Leaderboards
{
    [Serializable]
    [CreateAssetMenu(fileName = "Leaderboard", menuName = "Leaderboard")]
    public class Leaderboard : ScriptableObject
    {
        [SerializeField] private string _key = "BestScore";
        [SerializeField] private int _limit = 10;
        [NonSerialized] private List<GameResult> _results;
        [NonSerialized] private LeaderboardSaveLoad _saveLoad;
        
        public int ResultCount => _results.Count;
        public IEnumerable<GameResult> Results => _results;

        public GameResult LastResult { get; private set; }

        public override string ToString ()
        {
            var rows = _results.Select((r, index) => index+" "+r.ToString()).ToArray();
            return string.Join(Environment.NewLine, rows);
        }

        public void Initialize ()
        {
            _results = new List<GameResult>();
            _saveLoad = new LeaderboardSaveLoad(_key, _results);
            _saveLoad.Load();
        }

        public void AddResult (GameResult result)
        {
            // Insert 0 for current result upped to top of same score results
            _results.Insert(0, result);
            _results.Sort(ResultComparison);
            if (_results.Count > _limit)
                _results.RemoveRange(_limit, _results.Count-_limit);
            _saveLoad.Save();
            LastResult = result;
        }

        private int ResultComparison (GameResult resultA, GameResult resultB)
        {
            return resultB.Score-resultA.Score;
        }

        [ContextMenu("ClearDate (Runtime)")]
        private void ClearDate ()
        {
            _saveLoad.ClearDate();
        }

        [ContextMenu("Log (Runtime)")]
        private void Log ()
        {
            Debug.Log(this.ToString());
        }
    }
}
