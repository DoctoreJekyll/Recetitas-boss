using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Machine.Loki
{
    public class LokiTeleportState : State
    {
        [Header("Configure")]
        [SerializeField] private Transform[] teleportsTargets;
        [SerializeField] private float timeBetweenTeleports = 0.5f;
        [SerializeField] private int numberOfTeleports = 3;

        private StateMachine stateMachine;

        private void Awake()
        {
            stateMachine = GetComponent<StateMachine>();
            
            GameObject[] teleportGameObjects = GameObject.FindGameObjectsWithTag("Teleport");
            teleportsTargets = new Transform[teleportGameObjects.Length];
        
            for (int i = 0; i < teleportGameObjects.Length; i++)
            {
                teleportsTargets[i] = teleportGameObjects[i].transform;
            }
        }

        public override void Enter()
        {
            StartCoroutine(PerformTeleport());
        }

        private IEnumerator PerformTeleport()
        {
            for (int i = 0; i < numberOfTeleports; i++)
            {
                if (teleportsTargets.Length == 0)
                {
                    Debug.LogWarning("No teleport targets found.");
                    break;
                }

                int randomPosition = Random.Range(0, teleportsTargets.Length);
                this.transform.position = teleportsTargets[randomPosition].position;
                yield return new WaitForSeconds(timeBetweenTeleports);
            }

            // Una vez que todos los tps se han completado, transicionar al estado de disparo.
            stateMachine.ChangeState<LokiShootState>();
        }
        
        public override void Exit()
        {
            Debug.Log("Exit LokiTeleportState.");
            StopAllCoroutines();
        }
    }
}