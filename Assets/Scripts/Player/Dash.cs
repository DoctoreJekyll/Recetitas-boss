using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    public class Dash : MonoBehaviour
    {

        [Header("Dash Settings")]
        [SerializeField] private float dashSpeed = 15f;
        [FormerlySerializedAs("dashTime")] [SerializeField] private float timePlayerIsDashing = 0.2f;
        [SerializeField] private float dashCooldown = 1f;
        [SerializeField] private KeyCode keyCode = KeyCode.Space;
        
        private PlayerMovement playerMovement;
        private Life life;
        private Rigidbody2D rb;
        private bool canDash;

        private bool invulnerableSkill;

        private void Awake()
        {
            playerMovement = GetComponent<PlayerMovement>();
            rb = GetComponent<Rigidbody2D>();
            life = GetComponent<Life>();
        }

        private void Start()
        {
            invulnerableSkill = true;
            canDash = true;
        }

        private void Update()
        {
            if (Input.GetKeyDown(keyCode) && canDash)
            {
                StartCoroutine(DashAction());
            }
        }

        public void MakeInvulnerableDashDisable()
        {
            invulnerableSkill = false;
        }

        private IEnumerator DashAction()
        {
            canDash = false;
            playerMovement.enabled = false;

            DashMove();

            yield return new WaitForSeconds(timePlayerIsDashing);
            
            if (invulnerableSkill)
            {
                life.MakeInvulnerable();
            }
            
            playerMovement.enabled = true;
            
            yield return new WaitForSeconds(dashCooldown);
            canDash = true;
        }

        private void DashMove()
        {
            if (invulnerableSkill)
            {
                life.MakeInvulnerable();
            }
            
            Vector2 dashDirection = playerMovement.GetDirection();
            if (dashDirection == Vector2.zero)
            {
                dashDirection = Vector2.up; 
            }
            
            rb.AddForce(dashDirection * dashSpeed, ForceMode2D.Impulse);
        }
    }
}
