namespace Machine.Horse
{
    public class HorseStateMachine : StateMachine
    {
        private void Start()
        {
            ChangeState<BossChargeState>();
        }
    }
}
