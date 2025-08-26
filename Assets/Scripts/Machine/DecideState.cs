using System;
using UnityEngine;

namespace Machine
{
    public class DecideState : State
    {
        
        protected StateMachine stateMachine;
        
        private void Awake()
        {
            stateMachine = GetComponent<StateMachine>();
        }

        public override void Enter()
        {
            Debug.Log("StateMachine enter");
        }

        public override void Exit()
        {
            Debug.Log("StateMachine exit");
        }

        public override void Tick()
        {
            Debug.Log("StateMachine tick");
        }
    }
}
