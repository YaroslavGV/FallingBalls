using System;
using System.Collections.Generic;

public interface IState
{
    void Enter ();
    void Exit ();
}

public class StateMashine<T> where T : IState
{
    private readonly bool _transitionContract;
    private Dictionary<Type, T> _stages;
    private List<StateTransition> _transitions;
    private T _current;

    public StateMashine (bool transitionContract = false)
    {
        _transitionContract = transitionContract;
        _stages = new Dictionary<Type, T>();
        _transitions = new List<StateTransition>();
    }

    public void AddState (T state)
    {
        _stages.Add(state.GetType(), state);
    }

    public void AddTransition (Type from, Type to)
    {
        _transitions.Add(new StateTransition(from, to));
    }

    public void SetState (Type stateType)
    {
        if (_current == null || _transitionContract == false)
            SetState(_stages[stateType]);
        else
            TransitionToState(stateType);
    }

    private void SetState (T state)
    {
        if (_current != null)
            _current.Exit();
        _current = state;
        _current.Enter();
    }

    private void TransitionToState (Type stateType)
    {
        if (TransitionIsValid(stateType))
            SetState(_stages[stateType]);
        else
            InvalidTransition(stateType);
    }

    private bool TransitionIsValid (Type stateType)
    {
        StateTransition transition = new StateTransition(_current.GetType(), stateType);
        return _transitions.Contains(transition);
    }

    private void InvalidTransition (Type stateType)
    {
        throw new ArgumentException("Impermissible transition ("+_current.GetType()+" > "+stateType+")");
    }

    private struct StateTransition 
    {
        public Type FromType;
        public Type ToType;

        public StateTransition (Type fromType, Type toType)
        {
            FromType = fromType;
            ToType = toType;
        }
    }
}