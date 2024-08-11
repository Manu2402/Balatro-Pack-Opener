using UnityEngine;
using NS_Input;
using System.Collections.Generic;
using System.Linq;
using System;

namespace NS_Shop
{
    public class Pack : MonoBehaviour, ITappable
    {
        private const int collectablesAmount = 5;

        [SerializeField]
        private DB_Pack packDatas;

        private ShopHandler shopHandler;

        private Collectable[] collectablesGenerated = new Collectable[collectablesAmount];

        #region Mono
        private void Awake()
        {
            shopHandler = GameObject.FindObjectOfType<ShopHandler>();
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
            Dictionary<Collectable, bool> allCollection = shopHandler.OnGetCollectionData?.Invoke();
            List<Collectable> allCollectionCollectables = allCollection.Keys.ToList();
            Collectable[] generatedCollectables = new Collectable[collectablesAmount];
            int[] generatedIndexes = new int[collectablesAmount];

            InitIndexes(generatedIndexes);

            for (int i = 0; i < collectablesAmount; i++)
            {
                int index;
                do
                {
                    index = UnityEngine.Random.Range(0, allCollectionCollectables.Count);
                }
                while (generatedIndexes.Contains(index));

                Collectable generatedCollectable = allCollectionCollectables[index];

                // Last collectable must be at least rare, instead all rest must be common.
                if (i >= collectablesAmount - 1)
                {
                    if (generatedCollectable.Rarity == Rarity.Common)
                    {
                        i--;
                        continue;
                    }
                }
                else
                {
                    if(generatedCollectable.Rarity != Rarity.Common)
                    {
                        i--;
                        continue;
                    }
                }

                generatedIndexes[i] = index;

                generatedCollectable.gameObject.SetActive(true);
                generatedCollectables[i] = generatedCollectable;
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