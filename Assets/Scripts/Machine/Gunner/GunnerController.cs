using System;
using System.Collections.Generic;
using General;
using UnityEngine;

namespace Machine.Gunner
{
    public class GunnerController : MonoBehaviour
    {

        private int pillars;
        [SerializeField] private EnemiesLife bossLife;

        private bool startDamage = true;

        public void PillarDestroyEvent()
        {
            pillars++;
        }

        private void Update()
        {
            if (startDamage)
            {
                CheckPillars();
            }
        }

        private void CheckPillars()
        {
            if (pillars >= 4)
            {
                Debug.Log("Calling PillarDestroyEvent");
                bossLife.SpecialConditionOff();
                startDamage = false;
            }
        }
        

    }
}
