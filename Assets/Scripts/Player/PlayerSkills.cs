using System;
using UnityEngine;

namespace Player
{
    public class PlayerSkills : MonoBehaviour
    {
        public bool moreLife = false;
        public bool moreDamage = false;
        public bool invulnerableWhenDash = false;
        public bool doubleShoot;
        public bool holyMantle;


        private Life playerLife;
        private GunController gunController;
        private CharacterStats stats;
        private Dash dash;

        private void Awake()
        {
            playerLife = GetComponent<Life>();
            gunController = GetComponent<GunController>();
            stats = GetComponent<CharacterStats>();
            dash = GetComponent<Dash>();
        }

        public void DisableMoreLife()
        {
            moreLife = false;
            playerLife.LifeActual -= 3;
            playerLife.MaxLife -= 3;
        }

        public void DisableMoreDamage()
        {
            moreDamage = false;
            stats.DamagePerBullet -= 5;
        }

        public void DisableInvulnerableWhenDash()
        {
            invulnerableWhenDash = false;
            dash.MakeInvulnerableDashDisable();
            
        }

        public void DisableDoubleShoot()
        {
            doubleShoot = false;
            gunController.MakeDoubleShootDisable();
        }

        public void DisableHolyMantle()
        {
            holyMantle = false;
            //Later
        }
        
        
    }
}
