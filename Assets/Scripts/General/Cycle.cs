using UnityEngine;

namespace General
{
    public class Cycle : MonoBehaviour
    {
        // Start is called before the first frame update

        [SerializeField] private float timeToDestroyBullet = 3f;
        
        void Start()
        {
            Destroy(this.gameObject, timeToDestroyBullet);
        }
    }
}
