using UI.MobileJoystick;
using UnityEngine;
using UnityEngine.Events;

namespace Player
{
    public class TouchMovementController : MonoBehaviour
    {
        public static TouchMovementController Instance { get; private set; }

        public static PlayerMovement movementController;

        public static UnityEvent OnJumpButtonDown = new();

        [SerializeField] private Joystick joystick;

        private void Awake()
        {
            Instance = this;
        }

        void FixedUpdate()
        {
            Vector3 direction = new Vector3(joystick.Horizontal, 0f, joystick.Vertical);
            if (movementController)
                movementController.SetAxis(direction);
        }

        public Joystick GetJoystick()
        {
            return joystick;
        }

        public void OnJumpButton()
        {
            OnJumpButtonDown.Invoke();
        }
    }
}