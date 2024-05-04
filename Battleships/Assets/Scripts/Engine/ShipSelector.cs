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

        private GameController _gameController;

        public void Init(GameController gc)
        {
            _gameController = gc;

            if (_shipContainer)
            {
                foreach (Transform child in _shipContainer)
                {
                    Ship ship = child.GetComponent<Ship>();
                    ship.Init();
                    ship.ShipClicked.AddListener(SelectShip);
                }
            }
        }

        private void SelectShip(Ship ship)
        {
            ShipBlueprint blueprint = ship.gameObject.AddComponent<ShipBlueprint>();
            blueprint.AttemptToPlaceBlueprint.AddListener(TryToPlaceBlueprint);

            ship.Placing = true;
            blueprint.ShipType = ship.ShipType;
            _gameController.UpdateState(GameController.GameState.PlacingShip);
        }

        private void TryToPlaceBlueprint(ShipBlueprint blueprint)
        {
            if(_gridCreator)
            {
                Ship ship = blueprint.GetComponent<Ship>();
                ship.Placing = false;
                _gridCreator.PlaceShip(ship);
                blueprint.FinishPlacement();
                Destroy(blueprint);
            }
        }
    }
}
