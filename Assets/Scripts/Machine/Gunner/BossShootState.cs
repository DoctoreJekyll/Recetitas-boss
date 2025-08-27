using UnityEngine;
using Machine.Gunner;
using System.Collections;
using System.Collections.Generic;
using General;
using Player; // Importante: añade esta línea

namespace Machine
{
    public class BossShootState : State
    {
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private float bulletSpeed = 10f;
        [SerializeField] private List<GameObject> positions;

        private CharacterStats stats;
        private GunnerStateMachine bossStateMachine;
        private Transform target;

        private void Awake()
        {
            stats = GetComponent<CharacterStats>();
            bossStateMachine = GetComponent<GunnerStateMachine>();
            target = GameObject.FindWithTag("Player").transform;
        }

        public override void Enter()
        {
            // Iniciar la corrutina al entrar en el estado
            StartCoroutine(ShootAndTransition());
        }

        private IEnumerator ShootAndTransition()
        {
            Shoot(); // Dispara la bala

            if (bossStateMachine.isEnraged)
            {
                Debug.Log("Enrage!!");
                SummonBullets();
            }
            
            yield return null; 

            // Después de la pausa, vuelve al estado de espera
            bossStateMachine.ChangeState<BossIdleState>();
        }

        private void SummonBullets()
        {
            int randomValue = Random.Range(0, positions.Count);
            
            Vector3 shootDirection = (target.position - positions[randomValue].transform.position).normalized;
            GameObject newBullet = Instantiate(bulletPrefab, positions[randomValue].transform.position, Quaternion.identity);
            BulletStat(newBullet).Damage = stats.DamagePerBullet;
            Rigidbody2D bulletRb = newBullet.GetComponent<Rigidbody2D>();
            bulletRb.AddForce(shootDirection * bulletSpeed, ForceMode2D.Impulse);
        }

        private void Shoot()
        {
            if (target != null)
            {
                // Disparar en la dirección del jugador
                Vector3 shootDirection = (target.position - transform.position).normalized;
                GameObject newBullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                BulletStat(newBullet).Damage = stats.DamagePerBullet;
                Rigidbody2D bulletRb = newBullet.GetComponent<Rigidbody2D>();
                bulletRb.AddForce(shootDirection * bulletSpeed, ForceMode2D.Impulse);
                
                // Actualizar el tiempo del último disparo en el BossStateMachine
                bossStateMachine.lastShotTime = Time.time;
            }
        }

        private Bullet BulletStat(GameObject reference)
        {
            Bullet bullet = reference.GetComponent<Bullet>();
            return bullet;
        }
    }
}