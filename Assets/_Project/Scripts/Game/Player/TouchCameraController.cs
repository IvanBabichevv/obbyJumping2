using UnityEngine;
using UnityEngine.EventSystems;
using YG;

namespace Player
{
    public class TouchCameraController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler, IPointerClickHandler
    {
        public static CameraMovement CameraController;

        [SerializeField] float touchSensitivity = 0.2f;
        //[SerializeField] float zoomSensitivity = 2.0f;

        bool isPinching = false;

        float temp;

        /*void Update()
        {
            if (isPinching)
            {
                //if (CursorManager.attentionFocused) return;
                float distance = Vector3.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position);

                if (temp > distance)
                    CameraController.DecreaseDistance();
                else if (temp < distance)
                    CameraController.IncreaseDistance();

                temp = distance;
            }
        }*/

        public void OnPointerDown(PointerEventData eventData)
        {
            if (Input.touchCount == 2)
            {
                isPinching = true;

                if (TouchMovementController.Instance.GetJoystick().isUse)
                {
                    isPinching = false;
                }
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            isPinching = false;
            temp = 0;
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (isPinching)
                return;

            //touchSensitivity = YG2.saves.mouseSensitivity / 25;

            if (eventData.delta.magnitude > 0.5f)
            {
                Vector2 pos = eventData.delta;

                CameraController.SetMouseSensitivity(touchSensitivity);

                CameraController.SetAxis(pos.x, pos.y);
            }
        }
        
        public void OnPointerClick(PointerEventData eventData)
        {
            PlayerMovement.Instance.IncreaseJumpPower();
            SoundManager.instance.PlayButtonClick();
        }
    }
}