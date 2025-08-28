using System;
using System.Collections;
using UnityEngine;

namespace Machine.Loki
{
    public class LokiShootState : State
    {
        [Header("Shoot Settings")]
        [SerializeField] private GameObject projectilePrefab;
        [SerializeField] private int numberOfShots = 3;
        [SerializeField] private float timeBetweenShots = 0.5f;
        [SerializeField] private float returnToTeleportDelay = 1.5f;
        [SerializeField] private float bulletForce;

        private StateMachine stateMachine;
        private Transform playerTarget;

        private void Awake()
        {
            stateMachine = GetComponent<StateMachine>();
            playerTarget = GameObject.FindWithTag("Player").transform;
        }

        public override void Enter()
        {
            Debug.Log("Enter LokiShootState. Firing projectiles.");
            StartCoroutine(PerformShoot());
        }

        private IEnumerator PerformShoot()
        {
            for (int i = 0; i < numberOfShots; i++)
            {
                ShootAtPlayer();
                yield return new WaitForSeconds(timeBetweenShots);
            }

            // Después de disparar, espera y vuelve al estado de teletransporte.
            yield return new WaitForSeconds(returnToTeleportDelay);
            stateMachine.ChangeState<LokiTeleportState>();
        }

        private void ShootAtPlayer()
        {
            if (projectilePrefab == null || playerTarget == null) return;

            // Instancia el proyectil
            GameObject newProjectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            
            // Dirige el proyectil hacia el jugador (si tiene un Rigidbody2D)
            Rigidbody2D rb = newProjectile.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 direction = (playerTarget.position - transform.position).normalized;
                rb.AddForce(direction * bulletForce, ForceMode2D.Impulse);
            }
        }
        
        public override void Exit()
        {
            Debug.Log("Exit LokiShootState.");
        }
    }
}
