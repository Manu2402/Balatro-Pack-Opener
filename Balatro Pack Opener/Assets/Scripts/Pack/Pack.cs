using UnityEngine;
using NS_Input;
using NS_Collectable;

namespace NS_Pack
{
    public class Pack : MonoBehaviour, ITappable
    {
        private const int collectablesAmount = 5;

        [SerializeField]
        private DB_Pack packDatas;

        private Collectable[] collectables = new Collectable[collectablesAmount];

        #region Mono
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
            // Edit

            // Metodo che genera i collezionabili.

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
            // Fixare con Object Pooling.
            for (int i = 0; i < collectables.Length; i++)
            {
                
            }

            return null;
        }

    }
}
