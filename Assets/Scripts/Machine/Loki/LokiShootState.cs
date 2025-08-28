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

            // Calcula la dirección en la que el jefe está mirando
            Vector2 shootDirection = (playerTarget.position - transform.position).normalized;
    
            // Define una distancia de desplazamiento
            float offsetDistance = 1.0f; 

            // Calcula la nueva posición de inicio para la bala
            Vector3 spawnPosition = transform.position + (Vector3)shootDirection * offsetDistance;

            // Instancia el proyectil en la nueva posición
            Instantiate(projectilePrefab, spawnPosition, Quaternion.identity);
        }
        
        public override void Exit()
        {
            
        }
    }
}
