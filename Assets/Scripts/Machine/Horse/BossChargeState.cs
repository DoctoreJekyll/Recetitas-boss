using System.Collections;
using UnityEngine;

namespace Machine.Horse
{
    public class BossChargeState : State
    {

        [Header("Charge Settings")]
        [SerializeField] private float chargeSpeed = 15f;
        [SerializeField] private float chargeDuration = 1f;
        [SerializeField] private float windUpTime = 0.5f;

        private StateMachine stateMachine;
        private Rigidbody2D rb2d;
        private Transform target;

        private Vector2 chargeDirection;

        private void Awake()
        {
            stateMachine = GetComponent<StateMachine>();
            rb2d = GetComponent<Rigidbody2D>();
            target = GameObject.FindWithTag("Player").transform;
        }

        public override void Enter()
        {
            Debug.Log("Enter ChargeState. Starting wind-up.");
            StartCoroutine(PerformCharge());
        }

        private IEnumerator PerformCharge()
        {
            // Apuntar al jugador antes de la carga
            chargeDirection = (target.position - transform.position).normalized;
            
            // Tiempo de preparación (wind-up)
            yield return new WaitForSeconds(windUpTime);
            
            // Iniciar la carga
            float chargeEndTime = Time.time + chargeDuration;
            while (Time.time < chargeEndTime)
            {
                rb2d.velocity = chargeDirection * chargeSpeed;
                yield return null; // Espera un frame
            }

            // Detener la carga y pasar al siguiente estado
            rb2d.velocity = Vector2.zero;
            
            stateMachine.ChangeState<HorseShootState>();
        }

        public override void Exit()
        {
            Debug.Log("Exit ChargeState. Stopping movement.");
        }
    }
    
}
