using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

// TODO cauch find a way to template this
public enum PooledEnum
{
    FLOATING_TEXT,
    THROWABLE_WEIGHT
}

public class ObjectPooler : MonoBehaviour {

    [System.Serializable]
    public struct Pool
    {
        public GameObject Prefab;
        public PooledEnum Type;
        public int Size;
    }

    public List<Pool> Pools;
    private Dictionary<PooledEnum, Queue<GameObject>> _poolsDictionary;

    // TODO cauch transform to singleton?
    public static ObjectPooler Instance;

    private void Awake()
    {
        Instance = this;
    }

    // Use this for initialization
    void Start ()
    {
        _poolsDictionary = new Dictionary<PooledEnum, Queue<GameObject>>();
        foreach(Pool pool in Pools)
        {
            if(_poolsDictionary.ContainsKey(pool.Type))
            {
                throw new System.Exception("Pool already exists.");
            }

            Queue<GameObject> goPool = new Queue<GameObject>();

            for (int i = 0; i < pool.Size; i++)
            {
                GameObject go = GameObject.Instantiate(pool.Prefab);
                go.SetActive(false);
                goPool.Enqueue(go);
            }

            _poolsDictionary.Add(pool.Type, goPool);
        }

	}

    public GameObject SpawnFromPool(PooledEnum type, Vector3 position, Quaternion rotation, object[] objects = null)
    {
        if (_poolsDictionary[type].Any() == false)
        {
            Debug.Log("Pool is empty");
            return null;
        }

        GameObject spawn = _poolsDictionary[type].Dequeue();

        IPooledObject p = spawn.GetComponent<IPooledObject>();

        // Should trigger error if not a IPooledObject, implement it god dammit
        p.OnSpawn(objects);

        spawn.transform.position = position;
        spawn.transform.rotation = rotation;

        spawn.SetActive(true);

        _poolsDictionary[type].Enqueue(spawn);

        return spawn;
    }
}
