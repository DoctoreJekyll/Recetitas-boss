using UnityEngine;

namespace Machine.Gunner
{
    public class GunnerStateMachine : StateMachine
    {
        public float lastShotTime;
        public bool isEnraged = false;

        private void Start()
        {
            lastShotTime = Time.time;
            ChangeState<BossIdleState>();
        }
    }
}
