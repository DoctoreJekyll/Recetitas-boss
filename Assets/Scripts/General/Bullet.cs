using System;
using UnityEngine;

namespace General
{
    public class Bullet : MonoBehaviour
    {
        public int Damage { get; set; }


        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Wall"))
            {
                Destroy(this.gameObject);
            }
        }
    }
}
