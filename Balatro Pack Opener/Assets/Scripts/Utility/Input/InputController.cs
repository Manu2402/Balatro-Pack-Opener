using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Input
{
    // Controller that handles all inputs of the game with relative subjects.
    public class InputController : MonoBehaviour
    {
        private static InputController instance;

        private List<ITappable> tappables = new List<ITappable>();
        // Other input lists...

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
            InputManager.TouchOnScreen.performed += OnTouchOnScreenPerformed;
            // Other inputs...
        }

        private void OnDisable()
        {
            InputManager.TouchOnScreen.performed -= OnTouchOnScreenPerformed;
            // Other inputs...
        }
        #endregion

        #region Tap
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
            ITappable tappedObj = InputManager.TouchOnScreenExecute();
            if (!tappables.Contains(tappedObj)) return;
            tappedObj.ExecuteTap();
        }
        #endregion
    }
}
