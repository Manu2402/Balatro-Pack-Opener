using UnityEngine;
using System.Collections.Generic;
using NS_Shop;
using NS_ObjectPooler;

namespace NS_Album
{
    public class Album : MonoBehaviour
    {
        private Album instance;

        // Csollectable | hasFound
        private Dictionary<Collectable, bool> collection = new Dictionary<Collectable, bool>();

        public Album Instance
        {
            get
            {
                if (instance != null) return instance;
                instance = GameObject.FindObjectOfType<Album>();
                return instance;
            }
        }

        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
                return;
            }

            instance = this;
            DontDestroyOnLoad(gameObject);

            InitCollection();
        }

        private void OnEnable()
        {
            GlobalEventManager.AddListener(GlobalEventIndex.PackOpened, OnPackOpened);
        }

        private void OnDisable()
        {
            GlobalEventManager.RemoveListener(GlobalEventIndex.PackOpened, OnPackOpened);
        }

        private void InitCollection()
        {
            PoolerListRequest[] poolerListRequests = GameObject.FindObjectsOfType<PoolerListRequest>();
            if (poolerListRequests == null) return;

            PoolerListRequest listRequestCollectables = GetPoolerCollectablesRequest(poolerListRequests);
            if (listRequestCollectables == null) return;

            Collectable[] allCollectables = CastObjectsToCollectables(listRequestCollectables.GetPoolPrefabs());

            // Edit when will implemented save system.
            foreach(Collectable collectable in allCollectables)
            {
                collection.Add(collectable, false);
            }
        }

        private PoolerListRequest GetPoolerCollectablesRequest(PoolerListRequest[] poolerListRequests)
        {
            foreach(PoolerListRequest poolerListRequest in poolerListRequests)
            {
                if(poolerListRequest.PoolerListType == PoolerListType.Collectable)
                {
                    return poolerListRequest;
                }
            }

            return null;
        }

        // Convert into static method if will be useful again.
        private Collectable[] CastObjectsToCollectables(GameObject[] prefabs)
        {
            List<Collectable> allCollectables = new List<Collectable>(prefabs.Length);

            foreach(GameObject prefab in prefabs)
            {
                Collectable currentCollectable = prefab.GetComponent<Collectable>();
                if (currentCollectable == null) continue;
                allCollectables.Add(currentCollectable);
            }

            return allCollectables.ToArray();
        }

        private void OnPackOpened(GlobalEventArgs message)
        {
            // Implements method.
        }
    }
}