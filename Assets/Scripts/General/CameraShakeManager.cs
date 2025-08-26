using System;
using Cinemachine;
using UnityEngine;

namespace General
{
    public class CameraShakeManager : MonoBehaviour
    {

        public static CameraShakeManager instance;

        [SerializeField] private float globalShakeForce = 1f;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
        }

        public void CameraShake(CinemachineImpulseSource impulseSource)
        {
            impulseSource.GenerateImpulseWithForce(globalShakeForce);
        }
    }
}
