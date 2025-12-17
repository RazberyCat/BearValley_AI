using System;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    private State _currentState;

    private Dictionary<Type, State> _states = new();

    public Character Actor { get; set; } // FSM이 제어할 대상 (캐릭터)

    public void AddState(State state)    // 상태 추가
    {
        var type = state.GetType();
        state.SetMachine(this);
        _states[type] = state;
    }


    public void ChangeState<T>() where T : State // 상태 변경
    {
        var type = typeof(T);

        if (!_states.TryGetValue(type, out var next))
        {
            Debug.LogWarning($"State {type.Name} not found!");
            return;
        }

        if (_currentState == next) return;

        _currentState?.Exit();
        _currentState = next;
        _currentState.Enter();
    }

    public void Update() // 업데이트 처리
    {
        _currentState?.Update();
        //Debug.Log($"FSM State = {_currentState?.GetType().Name}");
    }
    public Type CurrentStateType => _currentState?.GetType();
}
