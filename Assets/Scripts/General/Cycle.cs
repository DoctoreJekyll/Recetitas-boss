using UnityEngine;

namespace General
{
    public class Cycle : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            Destroy(this.gameObject, 2f);
        }
    }
}
