using System.Collections;
using UnityEngine;

namespace Machine.Horse
{
    public class InstantiateEnemies : MonoBehaviour
    {

        
        [SerializeField] private GameObject objectToInstantiate;
        [SerializeField] private int numberOfEnemies;
        [SerializeField] private float timeBetweenSpawns;

        public void InstantiateEnemiesFrom(Vector3 position, Quaternion rotation)
        {
            StartCoroutine(InstantiateEnemiesCoroutine(position, rotation));
        }

        IEnumerator InstantiateEnemiesCoroutine(Vector3 position, Quaternion rotation)
        {
            for (int i = 0; i < numberOfEnemies; i++)
            {
                Instantiate(objectToInstantiate, position, rotation);
                yield return new WaitForSeconds(timeBetweenSpawns);
            }
        }
    
    
    
    
    }
}
