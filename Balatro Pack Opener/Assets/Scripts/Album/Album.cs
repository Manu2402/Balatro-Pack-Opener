using UnityEngine;
using System.Collections.Generic;
using NS_Shop;

namespace NS_Album
{
    public class Album : MonoBehaviour
    {
        private Album instance;

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
    }
}