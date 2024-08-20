using UnityEngine;
using NS_Input;
using System.Linq;
using System;
using NS_ObjectPooler;

namespace NS_Shop
{
    public class Pack : MonoBehaviour, ITappable
    {
        private const float zOffset = 0.01f;

        [SerializeField]
        private DB_Pack packDatas;
        [SerializeField]
        private ShopHandler shopHandler;

        private PoolerListRequest poolerListRequest;
        private Collectable[] collectablesGenerated;
        private uint collectablesAmount;

        private SpriteRenderer spriteRenderer;
        private BoxCollider2D boxCollider;

        #region Mono
        private void Awake()
        {
            poolerListRequest = GameObject.FindObjectOfType<PoolerListRequest>();
            collectablesAmount = packDatas.CollectablesAmount;
            collectablesGenerated = new Collectable[collectablesAmount];

            spriteRenderer = GetComponent<SpriteRenderer>();
            boxCollider = GetComponent<BoxCollider2D>();
        }

        private void OnEnable()
        {
            shopHandler.OnEndedPackOpening += OnEndedPackOpening;
            RegisterTap();
        }

        private void OnDisable()
        {
            shopHandler.OnEndedPackOpening -= OnEndedPackOpening;
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
            PackManager.HidePacks();

            // Aggiungere i collezionabili all'album.
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

                pickedCollectable.transform.position = zIndexDepth;
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

        private void OnEndedPackOpening()
        {
            PackManager.ShowPacks();
        }

        public void ShowPack()
        {
            spriteRenderer.enabled = true;
            boxCollider.enabled = true;
        }

        public void HidePack()
        {
            spriteRenderer.enabled = false;
            boxCollider.enabled = false;
        }

    }
}