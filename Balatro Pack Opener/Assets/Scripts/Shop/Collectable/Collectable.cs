using UnityEngine;
using NS_Input;

namespace NS_Shop
{
    public class Collectable : MonoBehaviour, ITappable
    {
        [SerializeField]
        private DB_Collectable collectableDatas;

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
            Debug.Log("You tapped on a collectable");
        }
        #endregion

        private bool InputControllerIsValid()
        {
            return InputController.Get() != null;
        }

    }
}