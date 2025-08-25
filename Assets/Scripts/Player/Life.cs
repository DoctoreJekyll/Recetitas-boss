using System;
using General;
using UnityEngine;

namespace Player
{
    public class Life : MonoBehaviour
    {

        [SerializeField] private string tagToLoseLife;
        
        [SerializeField] private int life;
        private int maxLife;
        
        private bool canTakeDamage;

        
        private CharacterStats characterStats;
        
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
                life -= amount;
            }
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
