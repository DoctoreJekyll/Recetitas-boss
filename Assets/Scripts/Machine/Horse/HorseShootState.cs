using System.Collections;
using UnityEngine;

namespace Machine.Horse
{
    public class HorseShootState : State
    {
        [Header("Shoot Settings")]
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private int numberOfBullets = 8;
        [SerializeField] private float bulletSpeed = 5f;
        [SerializeField] private float circleRadius = 2f;

        private StateMachine stateMachine;
        private Rigidbody2D rb2d;

        private void Awake()
        {
            stateMachine = GetComponent<StateMachine>();
            rb2d = GetComponent<Rigidbody2D>();
        }

        public override void Enter()
        {
            Debug.Log("Enter ShootState. Firing bullets.");
            ShootInCircle();
            
            StartCoroutine(WaitAndChargeAgain());
        }

        private void ShootInCircle()
        {
            float angleStep = 360f / numberOfBullets;

            for (int i = 0; i < numberOfBullets; i++)
            {
                float angle = i * angleStep;
                float angleRad = angle * Mathf.Deg2Rad;
                
                Vector3 shootDirection = new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad), 0);
                Vector3 spawnPosition = transform.position + shootDirection * circleRadius;

                GameObject bullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);
                Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();

                if (bulletRb != null)
                {
                    bulletRb.AddForce(shootDirection * bulletSpeed, ForceMode2D.Impulse);
                }
            }
        }
        
        private IEnumerator WaitAndChargeAgain()
        {
            Debug.Log("charge");
            yield return new WaitForSeconds(1.5f);
            stateMachine.ChangeState<BossChargeState>();
        }

        public override void Exit()
        {
            Debug.Log("Exit ShootState.");
        }
    }

}
