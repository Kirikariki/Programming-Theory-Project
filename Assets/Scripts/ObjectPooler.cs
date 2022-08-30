using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles activation/instantiation and deactivation of various shootable objects
/// </summary>
public class ObjectPooler : MonoBehaviour
{
    [SerializeField] private GameObject[] shootablePrefabs;     // shootable prefabs to be used in game
    private ObjectPool[] objectPools;                           // object pools for shootable objects

    // Start is called before the first frame update
    void Start()
    {
        // initialize object pools
        int length = shootablePrefabs.Length;
        objectPools = new ObjectPool[length];
        for (int i = 0; i < length; i++)
        {
            objectPools[i] = new(shootablePrefabs[i], this.gameObject);
        }
    }

    // ABSTRACTION
    // SpawnShootable method can be called with one line of code wherever necessary. The code in the method does not need to be rewritten/copied elsewhere.
    /// <summary>
    /// Spawns a random shootable object in a random position in the game
    /// </summary>
    public void SpawnShootable()
    {
        int rand = Random.Range(0, objectPools.Length);
        objectPools[rand].SpawnObject();
    }

    /// <summary>
    /// Handles activation/instantiation and deactivation of one type of shootable objects
    /// </summary>
    private class ObjectPool
    {
        private readonly List<ShootableBase> shootables;    // list with all instantiated shootables in this object pool
        private readonly GameObject prefab, parent;         // shootable prefab to instantiate, parent to add instantiated objects to

        /// <summary>
        /// Creates an object pool for the given prefab. Instantiated prefabs will be children of the given parent.
        /// </summary>
        /// <param name="prefab"></param>
        /// <param name="parent"></param>
        public ObjectPool(GameObject prefab, GameObject parent)
        {
            shootables = new();
            this.prefab = prefab;
            this.parent = parent;
        }

        // ABSTRACTION
        // SpawnObject method can be called with one line of code wherever necessary. The code in the method does not need to be rewritten/copied elsewhere.
        /// <summary>
        /// Activates and initializes the first inactive object in the object pool or instantiates a new one if no inactive object is found.
        /// </summary>
        public void SpawnObject()
        {
            // find first inactive object in list and activate/initialize it
            foreach(ShootableBase shootable in shootables)
            {
                if (!shootable.gameObject.activeInHierarchy)
                {
                    shootable.Initialize();
                    return;
                }
            }

            // instantiate new object if no inactive object was found
            ShootableBase newShootable = Instantiate(prefab, parent.transform).GetComponent<ShootableBase>();
            shootables.Add(newShootable);
        }
    }
}
