using UnityEngine;

namespace GameSession
{
    public abstract class SessionState : MonoBehaviour, IState
    {

        public virtual void Enter () { }

        public virtual void Exit () { }
    }
}