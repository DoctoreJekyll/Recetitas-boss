using System;
using UnityEngine;

namespace General
{
    public class CircleShooting : MonoBehaviour
    {

        [Header("Bullet Settings")]
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private int numberOfBullets = 5;
        [SerializeField] private float bulletSpeed = 5f;
        [SerializeField] private float fireRate = 2f;
        [SerializeField] private float healthThreshold = 50f;
        
        [Header("Circle Settings")]
        [SerializeField] private float circleRadius = 2f;

        private float nextFireTime;
        private EnemiesLife life;

        private void Start()
        {
            life = GetComponent<EnemiesLife>();
        }

        private void Update()
        {
            if (life.LifeActual <= life.MaxLife * (healthThreshold / 100) )
            {
                if (Time.time > nextFireTime)
                {
                    ShootingCircle();
                    nextFireTime = Time.time + fireRate;
                }
            }
        }

        private void ShootingCircle()
        {
            float angleStep = 360f / numberOfBullets;

            for (int i = 0; i < numberOfBullets; i++)
            {
                float angle = angleStep * i;
                float angleRad = angle * Mathf.Deg2Rad;
                
                // Calculamos la posición de la bala
                Vector3 spawnPosition = transform.position + new Vector3(Mathf.Cos(angleRad) * circleRadius, Mathf.Sin(angleRad) * circleRadius, 0);

                // Instanciamos la bala
                GameObject bullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);
                Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();

                if (bulletRb != null)
                {
                    // Calculamos la dirección y aplicamos la fuerza
                    Vector3 shootDirection = (spawnPosition - transform.position).normalized;
                    bulletRb.AddForce(shootDirection * bulletSpeed, ForceMode2D.Impulse);
                }
            }
        }
    }
}
