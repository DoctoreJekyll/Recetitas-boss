using System;
using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        private CharacterStats characterStats;
        
        private float speed;
        
        private Vector2 moveDirection;
        private Rigidbody2D rb2d;

        private void Awake()
        {
            rb2d = GetComponent<Rigidbody2D>();
            characterStats = GetComponent<CharacterStats>();
        }

        private void Start()
        {
            speed = characterStats.PlayerSpeed;
        }

        private void Update()
        {
            InputsValues();
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void InputsValues()
        {
            float x = Input.GetAxisRaw("Horizontal");
            float y = Input.GetAxisRaw("Vertical");
            
            moveDirection = new Vector2(x, y).normalized;
        }
        
        private Vector2 externalForce; // Variable para almacenar la fuerza externa (knockback)

        private void Move()
        {
            // Combinar la velocidad del jugador y la fuerza externa
            rb2d.velocity = (moveDirection * speed) + externalForce;

            // Reducir la fuerza externa gradualmente
            externalForce = Vector2.Lerp(externalForce, Vector2.zero, 5f * Time.fixedDeltaTime);
        }
        
        public void ApplyExternalForce(Vector2 force)
        {
            externalForce = force;
        }

        public Vector2 GetDirection()
        {
            return  moveDirection;
        }
    }
}
