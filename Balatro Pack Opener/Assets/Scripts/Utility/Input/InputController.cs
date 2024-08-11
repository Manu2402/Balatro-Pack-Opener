using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

namespace NS_Input
{
    // Controller that handles all inputs of the game with relative subjects.
    public class InputController : MonoBehaviour
    {
        private static InputController instance;

        private List<ITappable> tappables = new List<ITappable>();
        // Other input lists...

        private ISwipeable lastTappedObj;

        public static InputController Get()
        {
            if (instance != null) return instance;
            instance = GameObject.FindObjectOfType<InputController>();
            return instance;
        }

        #region Mono
        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
                return;
            }
            instance = this;
            DontDestroyOnLoad(this);
        }

        private void OnEnable()
        {
            InputManager.TouchScreen.performed += OnTouchOnScreenPerformed;
            InputManager.TouchScreen.canceled += OnTouchOnScreenCanceled;
            // Other inputs...
        }

        private void OnDisable()
        {
            InputManager.TouchScreen.performed -= OnTouchOnScreenPerformed;
            InputManager.TouchScreen.canceled -= OnTouchOnScreenCanceled;
            // Other inputs...
        }
        #endregion

        #region Tap/Untap
        public void RegisterAsTappable(ITappable obj)
        {
            if (tappables.Contains(obj)) return;
            tappables.Add(obj);
        }

        public void UnregisterAsTappable(ITappable obj)
        {
            if (!tappables.Contains(obj)) return;
            tappables.Remove(obj);
        }

        private void OnTouchOnScreenPerformed(InputAction.CallbackContext context)
        {
            GameObject tappedObjGO = InputManager.TouchOnScreenExecute();
            if (tappedObjGO == null) return;

            ITappable tappedObj = tappedObjGO.GetComponent<ITappable>();
            if (tappedObj == null) return;

            if (!tappables.Contains(tappedObj)) return;
            tappedObj.ExecuteTap();

            lastTappedObj = tappedObjGO.GetComponent<ISwipeable>();
        }

        private void OnTouchOnScreenCanceled(InputAction.CallbackContext context)
        {
            if (lastTappedObj == null) return;

            SwipeDirection swipeDirection = InputManager.SwipeOnScreenExecuted();
            if (swipeDirection == SwipeDirection.None) return;
            lastTappedObj.ExecuteTapRelease(swipeDirection);
            lastTappedObj = null;
        }
        #endregion
    }
}
