using System;
using General;
using UnityEngine;

namespace Player
{
    public class GunController : MonoBehaviour
    {

        [SerializeField] private Transform gun;
        [SerializeField] private float gunDistance;
        
        [SerializeField] private float fireRate = 0.2f;
        [SerializeField] private float bulletSpeed = 10f;
        [SerializeField] private float knockBackForce = 5f;
        
        [SerializeField] private GameObject bullet;

        private Rigidbody2D rb2d;
        private PlayerMovement playerMovement;
        private CharacterStats stats;

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
            Vector3 direction = mousePosition - transform.position;
            
            gun.rotation = Quaternion.LookRotation(direction);
            
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            gun.position = transform.position + Quaternion.Euler(0,0,angle) * new Vector3(gunDistance, 0, 0);
        }

        private void ShootInput()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                InstantiateBullets();
            }
        }

        private void InstantiateBullets()
        {
            // La dirección de disparo es la misma que la del arma
            Vector3 shootDirection = (gun.position - transform.position).normalized;

            // Instanciar la bala y obtener su Rigidbody2D
            GameObject newBullet = Instantiate(bullet, gun.position, gun.rotation);
            Bullet newBulletScript = newBullet.GetComponent<Bullet>();
            newBulletScript.Damage = stats.DamagePerBullet;
            Rigidbody2D bulletRb = newBullet.GetComponent<Rigidbody2D>();

            // Aplicar una fuerza instantánea a la bala
            bulletRb.AddForce(shootDirection * bulletSpeed, ForceMode2D.Impulse);

            // Aplicar el knockback al jugador (sin cambios)
            playerMovement.ApplyExternalForce(-shootDirection * knockBackForce);
        }
    }
}
