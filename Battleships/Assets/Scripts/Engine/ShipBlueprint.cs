using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

namespace Battleships.Engine
{
    public class ShipBlueprint : MonoBehaviour, IPointerDownHandler
    {
        public UnityEvent<Vector2> AttemptToPlaceBlueprint { get; set; }
        public bool Placing { get; set; }

        private void Awake()
        {
            SetTransparent();
            RemoveShipComponent();
            Placing = true;

            AttemptToPlaceBlueprint = new UnityEvent<Vector2>();
        }

        private void Update()
        {
            if(Placing)
                transform.position = Input.mousePosition;
        }

        private void SetTransparent()
        {
            Image img = GetComponent<Image>();

            if (img)
                img.color = new Color(img.color.r, img.color.g, img.color.b, 0.5f);
        }

        private void RemoveShipComponent()
        {
            Ship ship = GetComponent<Ship>();

            if (ship)
                Destroy(ship);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            AttemptToPlaceBlueprint.Invoke(transform.position);
        }
    }
}
