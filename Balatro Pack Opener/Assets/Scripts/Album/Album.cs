using UnityEngine;
using System.Collections.Generic;
using NS_Shop;

namespace NS_Album
{
    public class Album : MonoBehaviour
    {
        private Album instance;

        [SerializeField]
        private ShopHandler shopHandler;

        // Collectable | hasFound
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
        }

        private void OnEnable()
        {
            shopHandler.OnGetCollectionData += OnGetCollectionData;
        }

        private void OnDisable()
        {
            shopHandler.OnGetCollectionData -= OnGetCollectionData;
        }

        private void Start()
        {
            // Getting all collectables.
            Collectable[] collectables = FindObjectsByType<Collectable>(FindObjectsInactive.Include, FindObjectsSortMode.None);
            foreach (Collectable collectable in collectables)
            {
                collection.Add(collectable, collectable.gameObject.activeSelf);
            }
        }

        private Dictionary<Collectable, bool> OnGetCollectionData()
        {
            return collection;
        }
    }
}
