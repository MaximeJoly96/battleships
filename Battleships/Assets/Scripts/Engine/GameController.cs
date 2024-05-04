using UnityEngine;

namespace Battleships.Engine
{
    public class GameController : MonoBehaviour
    {
        public enum GameState { Generating, WaitingForPlacement, PlacingShip }
        public GameState CurrentState { get; private set; }

        [SerializeField]
        private GridCreator _gridCreator;
        [SerializeField]
        private ShipSelector _shipSelector;

        private void Awake()
        {
            UpdateState(GameState.Generating);

            _gridCreator.Init(this);
            _shipSelector.Init(this);

            UpdateState(GameState.WaitingForPlacement);
        }

        public void UpdateState(GameState state)
        {
            CurrentState = state;
        }
    }
}