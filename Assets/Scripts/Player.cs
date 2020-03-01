using UnityEngine;
using UnityEngine.EventSystems;

namespace PingPong
{
    public sealed class Player : MonoBehaviour, IDragHandler, IPointerExitHandler, IPointerEnterHandler
    {
        private bool _isDrag;
        private Vector3 _scanPosition;
        private Camera _cameraMain;

        private void Awake()
        {
            _cameraMain = Camera.main;
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (!_isDrag)
            {
                return;
            }

            var currentScreenPoint = _cameraMain.ScreenToWorldPoint(eventData.pointerCurrentRaycast.screenPosition);
            var currentPosition = new Vector3(currentScreenPoint.x, transform.position.y, 0.0f);

            transform.position = currentPosition;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _isDrag = false;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _isDrag = true;
        }
    }
}
