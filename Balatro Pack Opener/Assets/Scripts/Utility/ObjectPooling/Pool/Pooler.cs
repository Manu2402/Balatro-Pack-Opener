using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace NS_ObjectPooler
{
    public class Pooler : MonoBehaviour
    {
        private static Pooler instance;

        public static Pooler Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<Pooler>();
                }

                return instance;
            }
        }

        private Dictionary<string, GameObject[]> pool = new Dictionary<string, GameObject[]>();

        private List<string> poolToDestroy = new List<string>();

        public void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
                return;
            }
            DontDestroyOnLoad(gameObject);
            instance = this;

            SceneManager.sceneUnloaded += OnSceneUnloaded;
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode loadMode)
        {
            foreach (string key in poolToDestroy)
            {
                if (!pool.ContainsKey(key)) continue;
                GameObject[] toDelete = pool[key];

                foreach (GameObject obj in toDelete)
                {
                    Destroy(obj);
                }

                pool.Remove(key);
            }
        }

        private void OnSceneUnloaded(Scene scene)
        {
            poolToDestroy.Clear();
            foreach (KeyValuePair<string, GameObject[]> item in pool)
            {
                poolToDestroy.Add(item.Key);
            }
        }

        public void AddToPool(PoolData data)
        {
            if (poolToDestroy.Contains(data.PoolKey))
            {
                poolToDestroy.Remove(data.PoolKey);
            }

            if (!pool.ContainsKey(data.PoolKey))
            {
                InternalAddToPool(data);
                return;
            }

            if (pool[data.PoolKey].Length < data.PoolNumber)
            {
                ExtendExistingPool(data);
            }
        }

        public GameObject GetPooledObject(PoolData data)
        {
            if (!pool.ContainsKey(data.PoolKey)) return null;

            foreach (GameObject obj in pool[data.PoolKey])
            {
                if (!obj.activeSelf) return obj;
            }

            if (!data.ResizablePool)
            {
                return null;
            }

            ExtendExistingPool(data.PoolKey, data.Obj, (int)(pool[data.PoolKey].Length * 1.33f));
            return GetPooledObject(data);
        }

        private void InternalAddToPool(PoolData data)
        {
            GameObject[] pooledObject = new GameObject[data.PoolNumber];

            for (int i = 0; i < pooledObject.Length; i++)
            {
                pooledObject[i] = InternalInstantiate(data.Obj, data.ActiveAtStart);
            }

            pool.Add(data.PoolKey, pooledObject);
        }

        private void ExtendExistingPool(PoolData data)
        {
            ExtendExistingPool(data.PoolKey, data.Obj, data.PoolNumber);
        }

        private void ExtendExistingPool(string key, Object obj, int newPoolNumber)
        {
            GameObject[] pooledObject = new GameObject[newPoolNumber];
            GameObject[] existingPool = pool[key];
            int i = 0;

            for (; i < existingPool.Length; i++)
            {
                pooledObject[i] = existingPool[i];
            }

            for (; i < pooledObject.Length; i++)
            {
                pooledObject[i] = InternalInstantiate(obj);
            }

            pool[key] = pooledObject;
        }

        private GameObject InternalInstantiate(Object obj, bool activeAtStart = false)
        {
            GameObject temp = null;

            switch (obj)
            {
                // Indirect cast to a new type.
                case IPooler prefabInterface: // Every script that implements "IPooler".
                    temp = Instantiate(prefabInterface.GetGameObject());
                    break;
                case GameObject prefabGameObject: // Every single GameObject (prefab).
                    temp = Instantiate(prefabGameObject);
                    break;
                default:
                    Debug.LogError("Invalidate type to be pooled!");
                    break;
            }

            DontDestroyOnLoad(temp);
            temp.SetActive(activeAtStart);
            return temp;
        }
    }

}