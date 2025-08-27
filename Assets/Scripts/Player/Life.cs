using System;
using System.Collections;
using General;
using UnityEngine;

namespace Player
{
    public class Life : MonoBehaviour
    {

        [SerializeField] private string tagToLoseLife;
        
        [SerializeField] private int life;

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
            
            canTakeDamage = true;
        }

        private void Update()
        {
            Dead();
        }

        private void LoseLife(int amount)
        {
            if (canTakeDamage)
            {
                if (isAPlayer)
                {   
                    StartCoroutine(TakeDamage(amount));
                }
                else
                {
                    life -= amount;
                }
            }

        }

        IEnumerator TakeDamage(int amount)
        {
            life -= amount;
            Debug.Log(life);
            canTakeDamage = false;
            //Visual effect
            yield return new WaitForSeconds(0.5f);
            canTakeDamage = true;
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
    }
}
