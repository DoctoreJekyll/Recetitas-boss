namespace Machine.Loki
{
    public class LokiStateMachine : StateMachine
    {
        private void Start()
        {
            ChangeState<LokiTeleportState>();
        }
    }
}
