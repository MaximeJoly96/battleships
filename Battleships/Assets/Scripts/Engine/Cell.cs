using UnityEngine;
using UnityEngine.EventSystems;

namespace Battleships.Engine
{
    public class Cell : MonoBehaviour, IPointerDownHandler
    {
        public int Row { get; set; }
        public int Column { get; set; }

        public void OnPointerDown(PointerEventData eventData)
        {
            Debug.Log("Row " + Row + " Col " + Column);
        }
    }
}

