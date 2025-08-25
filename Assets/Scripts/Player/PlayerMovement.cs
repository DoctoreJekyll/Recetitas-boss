using System;
using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField]
        [Header("Player Movement")]
        private float speed;
        
        private Vector2 moveDirection;
        private Rigidbody2D rb2d;

        private void Awake()
        {
            rb2d = GetComponent<Rigidbody2D>();
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

        private void Move()
        {
            rb2d.linearVelocity = new Vector2(moveDirection.x * speed, moveDirection.y * speed);
        }

        public Vector2 GetDirection()
        {
            return  moveDirection;
        }
    }
}
