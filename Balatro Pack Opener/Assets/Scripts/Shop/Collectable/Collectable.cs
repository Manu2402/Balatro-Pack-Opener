using UnityEngine;
using NS_Input;

namespace NS_Shop
{
    public class Collectable : MonoBehaviour, ISwipeable
    {
        private const float speed = 50;

        [SerializeField]
        private DB_Collectable collectableDatas;
        [SerializeField]
        private AnimationCurve animationCurve;
        [SerializeField]
        private PoolData collectablePoolDatas;

        private Rigidbody2D rb;
        private bool canMove = false;
        
        private Vector2 defaultSpeed = Vector2.up;
        private float timer;

        public uint Amount { get { return collectableDatas.Amount; } }
        public Rarity Rarity { get { return collectableDatas.Rarity; } }
        public PoolData CollectablePoolDatas { get { return collectablePoolDatas; } }

        #region Mono
        private void Awake()
        {
            timer = 0f;
            rb = GetComponent<Rigidbody2D>();
        }

        private void OnEnable()
        {
            RegisterTap();
        }

        private void OnDisable()
        {
            UnregisterTap();
        }

        private void FixedUpdate()
        {
            if (!canMove) return;
            // Swipe animation.
            rb.velocity = animationCurve.Evaluate(timer) * speed * defaultSpeed;
            timer += Time.fixedDeltaTime;
        }

        private void OnBecameInvisible()
        {
            gameObject.SetActive(false);
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
                    SwipeUpPerformed();
                    break;
                default: return;
            }
        }
        #endregion

        private bool InputControllerIsValid()
        {
            return InputController.Get() != null;
        }

        private void SwipeUpPerformed()
        {
            Debug.Log("Swipe up!");
            canMove = true;
        }

    }
}