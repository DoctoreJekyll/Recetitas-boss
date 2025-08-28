using System.Collections;
using UnityEngine;

namespace Machine.Horse
{
    public class BossChargeState : State
    {
        [Header("Charge Settings")]
        [SerializeField] private float chargeImpulseForce = 15f;
        [SerializeField] private float chargeDuration = 1f;
        [SerializeField] private float windUpTime = 0.5f;
        [SerializeField] private float postBounceDelay = 0.2f;
        [SerializeField] private float gradualStopDuration = 0.5f; // Nuevo: tiempo para frenar

        private StateMachine stateMachine;
        private Rigidbody2D rb2d;
        private Transform target;

        private bool hasBounced = false;

        private void Awake()
        {
            stateMachine = GetComponent<StateMachine>();
            rb2d = GetComponent<Rigidbody2D>();
            target = GameObject.FindWithTag("Player").transform;
        }

        public override void Enter()
        {
            Debug.Log("Enter ChargeState. Starting wind-up.");
            hasBounced = false;
            StartCoroutine(PerformCharge());
        }

        private IEnumerator PerformCharge()
        {
            Vector2 chargeDirection = (target.position - transform.position).normalized;
            
            yield return new WaitForSeconds(windUpTime);
            
            rb2d.AddForce(chargeDirection * chargeImpulseForce, ForceMode2D.Impulse);
            
            yield return new WaitForSeconds(chargeDuration);

            if (!hasBounced)
            {
                // Iniciar el frenado gradual antes de la transición
                StartCoroutine(GradualStopAndTransition());
            }
        }
        
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (!hasBounced && collision.gameObject.CompareTag("Wall"))
            {
                StopCoroutine("PerformCharge");
                
                hasBounced = true;
                Debug.Log("¡Rebote detectado por material!");
                
                StartCoroutine(TransitionAfterBounce());
            }
        }
        
        private IEnumerator TransitionAfterBounce()
        {
            yield return new WaitForSeconds(postBounceDelay);
            StartCoroutine(GradualStopAndTransition());
        }

        private IEnumerator GradualStopAndTransition()
        {
            float startTime = Time.time;
            Vector2 initialVelocity = rb2d.velocity;

            // Reduce la velocidad gradualmente a lo largo de 'gradualStopDuration'
            while (Time.time < startTime + gradualStopDuration)
            {
                float t = (Time.time - startTime) / gradualStopDuration;
                rb2d.velocity = Vector2.Lerp(initialVelocity, Vector2.zero, t);
                yield return null;
            }

            // Asegurar que la velocidad sea exactamente cero al final
            rb2d.velocity = Vector2.zero;

            // Transicionar al siguiente estado
            stateMachine.ChangeState<HorseShootState>();
        }

        public override void Exit()
        {
            Debug.Log("Exit ChargeState. Stopping movement.");
            // Detener cualquier corrutina de frenado al salir
            StopAllCoroutines();
            rb2d.velocity = Vector2.zero;
        }
    }
}