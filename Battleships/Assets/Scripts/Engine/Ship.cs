using UnityEngine;
using System;
using UnityEngine.EventSystems;
using UnityEngine.Events;

namespace Battleships.Engine
{
    public class Ship : MonoBehaviour, IPointerDownHandler
    {
        public enum Type { Carrier, Battleship, Cruiser, Submarine, Destroyer }

        [SerializeField]
        private Type _type;

        public Type ShipType { get { return _type; } }
        public UnityEvent<Type> ShipClicked { get; private set; }

        public void Init()
        {
            ShipClicked = new UnityEvent<Type>();
        }

        public int GetSize()
        {
            switch(_type)
            {
                case Type.Battleship:
                    return 4;
                case Type.Carrier:
                    return 5;
                case Type.Submarine:
                case Type.Cruiser:
                    return 3;
                case Type.Destroyer:
                    return 2;
                default:
                    throw new ArgumentException("Provided ship type does not have a size.");
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            ShipClicked.Invoke(_type);
        }
    }
}
