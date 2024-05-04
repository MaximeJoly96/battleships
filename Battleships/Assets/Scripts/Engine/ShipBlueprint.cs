using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

namespace Battleships.Engine
{
    public class ShipBlueprint : MonoBehaviour, IPointerDownHandler
    {
        public UnityEvent<ShipBlueprint> AttemptToPlaceBlueprint { get; set; }
        public Ship.Type ShipType { get; set; }

        private void Awake()
        {
            SetTransparent();

            AttemptToPlaceBlueprint = new UnityEvent<ShipBlueprint>();
        }

        private void SetTransparent()
        {
            Image img = GetComponent<Image>();

            if (img)
                img.color = new Color(img.color.r, img.color.g, img.color.b, 0.5f);
        }

        public void FinishPlacement()
        {
            Image img = GetComponent<Image>();

            if (img)
                img.color = new Color(img.color.r, img.color.g, img.color.b, 1.0f);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            AttemptToPlaceBlueprint.Invoke(this);
        }
    }
}
