using UnityEngine;
using NS_Input;

namespace NS_Shop
{
    public class Collectable : MonoBehaviour, ISwipeable
    {
        [SerializeField]
        private DB_Collectable collectableDatas;

        public uint Amount { get { return collectableDatas.Amount; } }
        public Rarity Rarity { get { return collectableDatas.Rarity; } }

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

        public void ExecuteTapRelease(SwipeDirection swipeDirection)
        {
            // Edit
            switch (swipeDirection)
            {
                case SwipeDirection.Up:
                    Debug.Log("Swipe up!");
                    break;
                default: return;
                
            }
        }
        #endregion

        private bool InputControllerIsValid()
        {
            return InputController.Get() != null;
        }

    }
}