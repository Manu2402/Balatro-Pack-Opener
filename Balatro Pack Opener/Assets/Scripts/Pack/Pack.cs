using UnityEngine;
using UnityEngine.InputSystem;

namespace Pack
{
    public class Pack : MonoBehaviour
    {
        [SerializeField]
        private DB_Pack packDatas;

        private Collider2D packCollider;

        private void Awake()
        {
            packCollider = GetComponent<Collider2D>();
        }

        private void OnEnable()
        {
            InputManager.TouchOnScreen.performed += OnOpenPackPerformed;
        }

        private void OnDisable()
        {
            InputManager.TouchOnScreen.performed -= OnOpenPackPerformed;
        }

        private void OnOpenPackPerformed(InputAction.CallbackContext context)
        {
            InputManager.GenericTouchOnScreen();
        }
    }
}
