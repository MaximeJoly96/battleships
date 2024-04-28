using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace Battleships.Engine
{
    public class ShipSelector : MonoBehaviour
    {
        [SerializeField]
        private Transform _shipContainer;
        [SerializeField]
        private Canvas _baseCanvas;
        [SerializeField]
        private GridCreator _gridCreator;

        private List<Ship> _ships;
        private ShipBlueprint _currentBlueprint;

        private void Awake()
        {
            GetAllShips();
        }

        private void GetAllShips()
        {
            _ships = new List<Ship>();

            if(_shipContainer)
            {
                foreach(Transform child in _shipContainer)
                {
                    Ship ship = child.GetComponent<Ship>();
                    ship.Init();
                    ship.ShipClicked.AddListener(SelectShip);
                    _ships.Add(ship);
                }
            }
        }

        private void SelectShip(Ship.Type type)
        {
            Ship blueprint = Instantiate(_ships.FirstOrDefault(s => s.ShipType == type), _baseCanvas.transform);
            _currentBlueprint = blueprint.gameObject.AddComponent<ShipBlueprint>();
            _currentBlueprint.AttemptToPlaceBlueprint.AddListener(TryToPlaceBlueprint);
        }

        private void TryToPlaceBlueprint(Vector2 position)
        {
            if(_gridCreator)
            {
                _gridCreator.GetCellFromScreenPosition(position);
            }
        }
    }
}
