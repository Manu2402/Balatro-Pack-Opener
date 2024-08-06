using UnityEngine;
using Input;

namespace Pack
{
    public class Pack : MonoBehaviour, ITappable
    {
        [SerializeField]
        private DB_Pack packDatas;

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
            Debug.Log("You tapped on a pack");
        }
        #endregion

        private bool InputControllerIsValid()
        {
            return InputController.Get() != null;
        }

    }
}
