using UnityEngine;

namespace Player
{
    public class CharacterStats : MonoBehaviour
    {
        
        [SerializeField] private float playerSpeed;
        [SerializeField] private bool canMove;
    
        [SerializeField] private int playerLife;
        [SerializeField] private int playerMaxLife;
    
        [SerializeField] private int damagePerBullet;
        [SerializeField] private float gunShootRate;
    
        [SerializeField] private int lifeStealth;

        public float PlayerSpeed
        {
            get => playerSpeed;
            set => playerSpeed = value;
        }

        public bool CanMove
        {
            get => canMove;
            set => canMove = value;
        }

        public int PlayerLife
        {
            get => playerLife;
            set => playerLife = value;
        }

        public int PlayerMaxLife
        {
            get => playerMaxLife;
            set => playerMaxLife = value;
        }

        public int DamagePerBullet
        {
            get => damagePerBullet;
            set => damagePerBullet = value;
        }

        public float GunShootRate
        {
            get => gunShootRate;
            set => gunShootRate = value;
        }

        public int LifeStealth
        {
            get => lifeStealth;
            set => lifeStealth = value;
        }
    }
}
