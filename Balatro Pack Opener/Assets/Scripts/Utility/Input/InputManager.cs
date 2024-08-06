using UnityEngine;
using UnityEngine.InputSystem;

public static class InputManager
{
    private const float touchRayLength = 15;

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

    public static Inputs.PlayerActions TouchOnScreenTest
    {
        get { return inputs.Player; }
    }

    public static GameObject GenericTouchOnScreen()
    {
        Vector2 touchPosition = TouchOnScreen.ReadValue<Vector2>();
        Ray ray = Camera.main.ScreenPointToRay(touchPosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction * touchRayLength);
        if (hit.collider == null) return null;
        return hit.collider.gameObject;
    }
}
