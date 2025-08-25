using System;
using General;
using UnityEngine;

namespace Player
{
    public class GunController : MonoBehaviour
    {
        [Header("Weapon Settings")]
        [SerializeField] private Transform gun;
        [SerializeField] private float gunDistance = 1f;
        [SerializeField] private float fireRate = 0.2f;
        [SerializeField] private float bulletSpeed = 10f;
        [SerializeField] private float knockBackForce = 5f;
        
        [Header("Double Shot")]
        [SerializeField] private bool doubleShotEnabled = false;
        [SerializeField] private float shotSpread = 0.5f;

        [Header("References")]
        [SerializeField] private GameObject bullet;

        private Rigidbody2D rb2d;
        private PlayerMovement playerMovement;
        private CharacterStats stats;

        private float nextFireTime;

        private void Awake()
        {
            rb2d = GetComponent<Rigidbody2D>();
            playerMovement = GetComponent<PlayerMovement>();
            stats = GetComponent<CharacterStats>();
        }

        private void Update()
        {
            GunLookAround();
            ShootInput();
        }

        private void GunLookAround()
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // Aseguramos que el eje Z sea 0 para un juego 2D
            mousePosition.z = 0;
            Vector3 direction = mousePosition - transform.position;
            
            // Calculamos el ángulo para el sprite del arma
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            gun.rotation = Quaternion.Euler(0f, 0f, angle);

            // Posicionamos el arma a una distancia fija del jugador
            Vector3 offset = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad), 0);
            gun.position = transform.position + offset * gunDistance;
        }

        private void ShootInput()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && Time.time >= nextFireTime)
            {
                nextFireTime = Time.time + fireRate;
                InstantiateBullets();
            }
        }

        private void InstantiateBullets()
        {
            // La dirección de disparo es la misma que la del arma
            Vector3 shootDirection = (gun.position - transform.position).normalized;

            if (doubleShotEnabled)
            {
                // Disparo 1: ligeramente a la izquierda
                Vector3 offsetDirection1 = Quaternion.Euler(0, 0, 90) * shootDirection;
                Vector3 shotPosition1 = gun.position + offsetDirection1 * shotSpread;
                ShootBullet(shotPosition1, shootDirection);

                // Disparo 2: ligeramente a la derecha
                Vector3 offsetDirection2 = Quaternion.Euler(0, 0, -90) * shootDirection;
                Vector3 shotPosition2 = gun.position + offsetDirection2 * shotSpread;
                ShootBullet(shotPosition2, shootDirection);
            }
            else
            {
                // Disparo normal
                ShootBullet(gun.position, shootDirection);
            }

            // Aplicar el knockback al jugador
            playerMovement.ApplyExternalForce(-shootDirection * knockBackForce);
        }
        
        private void ShootBullet(Vector3 startPosition, Vector3 direction)
        {
            // Instanciar la bala y obtener su Rigidbody2D
            GameObject newBullet = Instantiate(bullet, startPosition, gun.rotation);
            Bullet newBulletScript = newBullet.GetComponent<Bullet>();
            newBulletScript.Damage = stats.DamagePerBullet;
            Rigidbody2D bulletRb = newBullet.GetComponent<Rigidbody2D>();

            // Aplicar una fuerza instantánea a la bala
            bulletRb.AddForce(direction * bulletSpeed, ForceMode2D.Impulse);
        }
    }
}