using System;
using UnityEngine;

namespace Machine
{
    public class StateMachine : MonoBehaviour
    {
        State currentState;
        public State CurrentState => currentState;

        protected bool InTransition;

        private void Start()
        {
            //ChangeState<DecideState>();
        }

        public void ChangeState<T>() where T : State
        {
            throw new NotImplementedException();
        }

        private void InitiateNewState(State targetState)
        {
            if (currentState != targetState && !InTransition)
            {
                CallNewState(targetState);
            }
        }

        private void CallNewState(State newState)
        {
            InTransition = true;
            
            currentState?.Exit();
            currentState = newState;
            currentState?.Enter();
            
            InTransition = false;
        }

        private void Update()
        {
            if (CurrentState != null && !InTransition)
            {
                CurrentState.Tick();
            }
        }
    }
}
