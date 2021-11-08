using System;
using System.Collections.Generic;
using UnityEngine;

namespace Leaderboards
{
    public class LeaderboardSaveLoad
    {
        private string _key;
        private List<GameResult> _results;

        public LeaderboardSaveLoad (string key, List<GameResult> results)
        {
            _key = key;
            _results = results;
        }

        public void Save ()
        {
            SaveResults results = new SaveResults(_results.ToArray());
            string json = JsonUtility.ToJson(results);
            PlayerPrefs.SetString(_key, json);
            PlayerPrefs.Save();
        }

        public void Load ()
        {
            if (PlayerPrefs.HasKey(_key))
            {
                string json = PlayerPrefs.GetString(_key);
                SaveResults results = JsonUtility.FromJson<SaveResults>(json);
                _results.AddRange(results.Results);
            }
        }

        public void ClearDate ()
        {
            PlayerPrefs.DeleteKey(_key);
            _results.Clear();
        }

        [Serializable]
        private struct SaveResults
        {
            public GameResult[] Results;

            public SaveResults (GameResult[] results)
            {
                Results = results;
            }
        }
    }
}