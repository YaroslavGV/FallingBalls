using System;
using UnityEngine;

namespace GameSession
{
    public class SessionStages : MonoBehaviour
    {
        private StateMashine<SessionState> _stateMashine;
        [SerializeField] private SessionState[] _states;

        public void Initialize ()
        {
            _stateMashine = new StateMashine<SessionState>();
            foreach (var state in _states)
                _stateMashine.AddState(state);
        }

        public void SetState (Type stateType)
        {
            _stateMashine.SetState(stateType);
        }

        [Serializable]
        private struct SessionStateTransition
        {
            public SessionState From;
            public SessionState To;
        }
    }
}
