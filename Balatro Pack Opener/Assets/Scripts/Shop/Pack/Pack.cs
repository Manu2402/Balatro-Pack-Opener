using UnityEngine;
using NS_Input;
using System.Linq;
using System;
using NS_ObjectPooler;

namespace NS_Shop
{
    public class Pack : MonoBehaviour, ITappable
    {
        private const int collectablesAmount = 5;
        private const float zOffset = 0.01f;

        [SerializeField]
        private DB_Pack packDatas;

        private PoolerListRequest poolerListRequest;

        private Collectable[] collectablesGenerated = new Collectable[collectablesAmount];

        #region Mono
        private void Awake()
        {
            poolerListRequest = GameObject.FindObjectOfType<PoolerListRequest>();
        }

        private void OnEnable()
        {
            RegisterTap();
        }

        private void OnDisable()
        {
            UnregisterTap();
        }
        #endregion

        #region TapInterface
        public void RegisterTap()
        {
            if (!InputControllerIsValid()) return;
            InputController.Get().RegisterAsTappable(this);
        }

        public void UnregisterTap()
        {
            if (!InputControllerIsValid()) return;
            InputController.Get().UnregisterAsTappable(this);
        }

        public void ExecuteTap()
        {
            collectablesGenerated = GenerateCollectables();

            // Dopo lo scorrimento di tutti i collezionabili far tornare il pacchetto.

            // Aggiungere i collezionabili all'album.

            gameObject.SetActive(false);
        }
        #endregion

        private bool InputControllerIsValid()
        {
            return InputController.Get() != null;
        }

        private Collectable[] GenerateCollectables()
        {
            Collectable[] generatedCollectables = new Collectable[collectablesAmount];
            int[] generatedIndexes = new int[collectablesAmount];

            InitIndexes(generatedIndexes);

            Vector3 zIndexDepth = Vector3.zero;

            for (int i = 0; i < collectablesAmount; i++)
            {
                int index;
                do
                {
                    index = UnityEngine.Random.Range(0, poolerListRequest.GetDataLength());
                }
                while (generatedIndexes.Contains(index));

                PoolData generatedCollectable = poolerListRequest.GetRandomData(index);
                if (generatedCollectable.GetType() != typeof(CollectablePoolData)) return null;

                Collectable pickedCollectable = Pooler.Instance.GetPooledObject(generatedCollectable).GetComponent<Collectable>();
                if (pickedCollectable == null) return null;

                // Last collectable must be at least rare, instead all rest must be common.
                if (i >= collectablesAmount - 1)
                {
                    if (pickedCollectable.Rarity == Rarity.Common)
                    {
                        i--;
                        continue;
                    }
                }
                else
                {
                    if (pickedCollectable.Rarity != Rarity.Common)
                    {
                        i--;
                        continue;
                    }
                }

                generatedIndexes[i] = index;

                pickedCollectable.transform.position = transform.position + zIndexDepth;
                zIndexDepth.z += zOffset;

                pickedCollectable.gameObject.SetActive(true);
                generatedCollectables[i] = pickedCollectable;
            }

            return generatedCollectables;
        }

        private void InitIndexes(int[] generatedIndexes)
        {
            for (int i = 0; i < generatedIndexes.Length; i++)
            {
                generatedIndexes[i] = -1; // -1 isn't in the collection.
            }
        }

    }
}