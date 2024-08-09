using UnityEngine;
using NS_Input;
using System.Collections.Generic;

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
            // Metodo che genera i collezionabili. Finire.
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

            Collectable[] generatedCollectables = new Collectable[collectablesAmount];
            
            // Generazione dei collezionabili, generati randomicamente dall'intera collezione "allCollection".

            return generatedCollectables;
        }

    }
}
