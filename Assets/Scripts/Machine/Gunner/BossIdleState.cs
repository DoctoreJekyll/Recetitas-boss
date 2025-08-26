using General;
using Player;
using UnityEngine;

namespace Machine.Gunner
{
    public class BossIdleState : State
    {
        [SerializeField] private float rotationSpeed = 5f;
        [SerializeField] private float healthThreshold = 50f;
        [SerializeField] private float timeBetweenShots = 2f;

        private GunnerStateMachine bossStateMachine;
        private Transform target;
        private EnemiesLife life;

        private void Awake()
        {
            bossStateMachine = GetComponent<GunnerStateMachine>();
            target = GameObject.FindWithTag("Player").transform;
            life = GetComponent<EnemiesLife>();
        }

        public override void Tick()
        {
            // Comprueba la vida del jefe para activar el modo "enfurecido"
            if (!bossStateMachine.isEnraged && life != null && life.LifeActual <= life.MaxLife * (healthThreshold / 100))
            {
                bossStateMachine.isEnraged = true;
            }
            
            if (target != null)
            {
                // Apuntar al jugador
                Vector3 direction = target.position - transform.position;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

                if (CheckTimeToShoot())
                {
                    bossStateMachine.ChangeState<BossShootState>();
                }
            }
        }

        private bool CheckTimeToShoot()
        {
            return Time.time > bossStateMachine.lastShotTime + timeBetweenShots;
        }
    }
}