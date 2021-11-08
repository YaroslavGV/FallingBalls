using UnityEngine;

namespace GameSession
{
    public class MainMenuState : SessionState
    {
        [SerializeField] private GameObject _ui;

        public override void Enter ()
        {
            _ui.SetActive(true);
        }

        public override void Exit ()
        {
            _ui.SetActive(false);
        }
    }
}