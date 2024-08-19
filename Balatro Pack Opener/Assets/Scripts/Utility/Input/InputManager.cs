using UnityEngine;
using UnityEngine.InputSystem;

namespace NS_Input
{
    public enum SwipeDirection
    {
        Up,
        Down,
        Left,
        Right,
        None
    }

    public static class InputManager
    {
        private struct SwipeControlPosition
        {
            private Vector2 touchPressed;
            private Vector2 touchReleased;

            public Vector2 TouchPressed { readonly get { return touchPressed; } set { touchPressed = value; } }
            public Vector2 TouchReleased { readonly get { return touchReleased; } set { touchReleased = value; } }

            public SwipeControlPosition(Vector2 touchPressed, Vector2 touchReleased)
            {
                this.touchPressed = touchPressed;
                this.touchReleased = touchReleased;
            }

            public void ClearPositions()
            {
                touchPressed = Vector2.zero;
                touchReleased = Vector2.zero;
            }
        }

        private const float touchRayLength = 15f;
        private const float swipeValidateMagnitude = 1f;

        private static Inputs inputs;
        private static SwipeControlPosition swipeControlPosition;

        static InputManager()
        {
            inputs = new Inputs();
            inputs.Player.Enable();

            swipeControlPosition = new SwipeControlPosition(Vector2.zero, Vector2.zero);
        }

        // Touch action.
        public static InputAction TouchScreen
        {
            get { return inputs.Player.TouchScreen; }
        }

        // Touch action position.
        public static InputAction TouchPosition
        {
            get { return inputs.Player.TouchPosition; }
        }

        public static GameObject TouchOnScreenExecute()
        {
            Vector2 touchPressedPosition = TouchPosition.ReadValue<Vector2>();
            Ray ray = Camera.main.ScreenPointToRay(touchPressedPosition); // Screen to Unity units
            swipeControlPosition.TouchPressed = ray.origin;

            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction * touchRayLength);
            if (hit.collider == null) return null;
            if (hit.collider.gameObject.GetComponent<ITappable>() == null) return null;
            return hit.collider.gameObject;
        }

        #region Swipe
        public static SwipeDirection SwipeOnScreenExecuted()
        {
            Vector2 touchReleasedPosition = TouchPosition.ReadValue<Vector2>();
            Ray ray = Camera.main.ScreenPointToRay(touchReleasedPosition);
            swipeControlPosition.TouchReleased = ray.origin;

            SwipeDirection swipeDirection =  GetSwipeDirection();
            swipeControlPosition.ClearPositions();

            return swipeDirection;
        }

        private static SwipeDirection GetSwipeDirection()
        {
            Vector2 swipeVector = swipeControlPosition.TouchReleased - swipeControlPosition.TouchPressed;
            if (swipeVector.sqrMagnitude <= swipeValidateMagnitude * swipeValidateMagnitude) return SwipeDirection.None;

            if (Mathf.Abs(swipeVector.x) > Mathf.Abs(swipeVector.y)) return InternalGetSwipeDirection(true, swipeControlPosition.TouchPressed.x < swipeControlPosition.TouchReleased.x);
            return InternalGetSwipeDirection(false, swipeControlPosition.TouchPressed.y < swipeControlPosition.TouchReleased.y);
        }

        private static SwipeDirection InternalGetSwipeDirection(bool isConditionOnXAxis, bool condition)
        {
            if (isConditionOnXAxis)
            {
                if (condition) return SwipeDirection.Right;
                return SwipeDirection.Left;
            }

            if (condition) return SwipeDirection.Up;
            return SwipeDirection.Down;
        }
        #endregion
    }
}