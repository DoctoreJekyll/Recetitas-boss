using System;
using System.Collections;
using Player;
using UnityEngine;
using UnityEngine.Events;

namespace General
{
    public class EnemiesLife : MonoBehaviour
    {

        [SerializeField] private string tagToLoseLife;
        
        [SerializeField] private int life;

        public bool specialCondition;
        public bool canGetDamage;

        public UnityEvent eventLauncher;

        public int LifeActual
        {
            get => life;
            set => life = value;
        }

        public int MaxLife
        {
            get => maxLife;
            set => maxLife = value;
        }

        private int maxLife;
        
        private bool canTakeDamage;
        private bool armor;

        
        private CharacterStats characterStats;

        [SerializeField] private bool isAPlayer;
        
        private void Awake()
        {
            characterStats = GetComponent<CharacterStats>();
        }

        private void Start()
        {
            life = characterStats.PlayerLife;
            maxLife = characterStats.PlayerMaxLife;
        }

        private void Update()
        {
            Dead();
        }

        private void LoseLife(int amount)
        {
            if (canGetDamage)
            {
                life -= amount;
            }
        }

        private void LetDamageOn()
        {
            canGetDamage = true;
        }

        public void SpecialConditionOff()
        {
            Debug.Log("Calling PillarDestroyEvent");
            specialCondition = false;
            LetDamageOn();
        }

        public void MakeInvulnerable()
        {
            canTakeDamage = false;
        }

        public void MakeVulnerable()
        {
            canTakeDamage = true;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(tagToLoseLife))
            {
                Bullet bullet = other.GetComponent<Bullet>();
                LoseLife(bullet.Damage);
                Destroy(other.gameObject);
            }
        }

        //TODO make invulnerable when hit

        private void LoseMaxLife()
        {
            maxLife--;
        }

        private void Health()
        {
            life++;

            if (life > maxLife)
            {
                life = maxLife;
            }
        }

        private void StealthLife()
        {
            life += characterStats.PlayerLife;

            if (life > maxLife)
            {
                life = maxLife;
            }
        }

        private void Dead()
        {
            if (life <= 0)
            {
                life = 0;
                Destroy(this.gameObject);
            }
        }

        private void OnDestroy()
        {
            eventLauncher?.Invoke();
        }
    }
}
