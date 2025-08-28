using UnityEngine;

namespace Machine.Loki
{
    public class HomingBullets : MonoBehaviour
    {
        [Header("Homing Settings")]
        [SerializeField] private float initSpeed = 5f;
        [SerializeField] private float homingStrength = 15f;
        [SerializeField] private float homingDuration = 1f;

        [SerializeField] private float initialHomingImpulse = 10f;

        private Rigidbody2D rb2d;
        private Transform target;
        private float homingEnd;
        private bool homingActive = true;

        private void Awake()
        {
            rb2d = GetComponent<Rigidbody2D>();
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }

        private void Start()
        {
            homingEnd = Time.time + homingDuration;

            if (target != null)
            {
                Vector2 direction = (target.position - transform.position).normalized;

                // Velocidad inicial
                rb2d.velocity = direction * initSpeed;

                // Impulso inicial de seguimiento
                rb2d.AddForce(direction * initialHomingImpulse, ForceMode2D.Impulse);
            }
        }

        private void FixedUpdate()
        {
            if (homingActive && target != null)
            {
                if (Time.time < homingEnd)
                {
                    // Mientras dure el homing → aplicar fuerza hacia el jugador
                    Vector2 direction = (target.position - transform.position).normalized;
                    rb2d.AddForce(direction * homingStrength, ForceMode2D.Force);
                }
                else
                {
                    // Homing finalizado → fijar velocidad actual y dejarla libre
                    rb2d.velocity = rb2d.velocity.normalized * initSpeed;
                    homingActive = false;
                }
            }
        }
    }
}