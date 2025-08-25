using System;
using UnityEngine;

namespace Player
{
    public class GunController : MonoBehaviour
    {

        [SerializeField] private Transform gun;
        [SerializeField] private float gunDistance;

        private void Update()
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 direction = mousePosition - transform.position;
            
            gun.rotation = Quaternion.LookRotation(direction);
            
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            gun.position = transform.position + Quaternion.Euler(0,0,angle) * new Vector3(gunDistance, 0, 0);
        }
    }
}
