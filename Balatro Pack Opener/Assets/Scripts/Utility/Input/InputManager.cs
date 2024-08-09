using UnityEngine;
using UnityEngine.InputSystem;

namespace NS_Input
{
    public static class InputManager
    {
        private const float touchRayLength = 15f;

        private static Inputs inputs;

        static InputManager()
        {
            inputs = new Inputs();
            inputs.Player.Enable();
        }

        public static InputAction TouchOnScreen
        {
            get { return inputs.Player.TouchOnScreen; }
        }

        public static ITappable TouchOnScreenExecute()
        {
            Vector2 touchPosition = TouchOnScreen.ReadValue<Vector2>();
            Ray ray = Camera.main.ScreenPointToRay(touchPosition); // Screen to Unity units
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction * touchRayLength);
            if (hit.collider == null) return null;
            if (!hit.collider.gameObject.TryGetComponent(out ITappable tappedObj)) return null;
            return tappedObj;
        }
    }
}