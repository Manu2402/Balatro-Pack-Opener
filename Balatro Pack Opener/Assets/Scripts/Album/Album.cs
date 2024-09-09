using UnityEngine;
using System.Collections.Generic;
using NS_Shop;
using NS_ObjectPooler;

namespace NS_Album
{
    public class Album : MonoBehaviour
    {
        private Album instance;

        // albumIndexOfCollectable | hasFound
        private readonly Dictionary<uint, bool> collection = new Dictionary<uint, bool>();

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
                collection.Add(collectable.AlbumIndex, false);
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

        // Convert into static method if it will be useful again.
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
            for (int i = 1; i < message.Args.Length; i++)
            {
                GameObject currentCollectableGO = (GameObject)message.Args[i].GetValue();
                Collectable currentCollectable = currentCollectableGO.GetComponent<Collectable>(); // We are sure about this.
                if (!collection.ContainsKey(currentCollectable.AlbumIndex))
                {
                    Debug.LogError("This collectable isn't in the collection!");
                    return;
                }

                collection[currentCollectable.AlbumIndex] = true;
            }
        }
    }
}