using UnityEngine;
using Leaderboards;

namespace GameSession
{
    public class LiderboardState : SessionState
    {
        [SerializeField] private GameObject _ui;

        public override void Enter ()
        {
            _ui.gameObject.SetActive(true);
        }

        public override void Exit ()
        {
            _ui.gameObject.SetActive(false);
        }
    }
}