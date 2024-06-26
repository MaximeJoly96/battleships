using UnityEngine;
using UnityEngine.UI;

namespace Battleships.Engine
{
    public class GridCreator : MonoBehaviour
    {
        private const int COLUMNS = 15;
        private const int ROWS = 10;
        private const float LABEL_SHIFT = 60.0f; // pixels

        [SerializeField]
        private GameObject _baseTile;
        [SerializeField]
        private GameObject _laneTag;

        [SerializeField]
        private Transform _numbersLane;
        [SerializeField]
        private Transform _lettersLane;

        private GameController _gameController;

        public void Init(GameController gc)
        {
            _gameController = gc;

            CreateGrid();
            CreateNumbers();
            CreateLetters();
        }

        private void CreateGrid()
        {
            if(_baseTile)
            {
                for(int i = 0; i < ROWS; i++)
                {
                    for(int j = 0; j < COLUMNS; j++)
                    {
                        GameObject go = Instantiate(_baseTile, transform);
                        Cell cell = go.GetComponent<Cell>();

                        cell.Row = i;
                        cell.Column = j;
                    }
                }
            }
        }

        private void CreateNumbers()
        {
            if(_laneTag && _numbersLane)
            {
                for (int i = 0; i < COLUMNS; i++)
                {
                    GameObject go = Instantiate(_laneTag, _numbersLane);
                    Text label = go.GetComponent<Text>();
                    label.text = (i + 1).ToString();
                }

                float cellHeight = _numbersLane.GetComponent<GridLayoutGroup>().cellSize.y;
                _numbersLane.transform.Translate(new Vector2(0.0f, cellHeight * ROWS / 2.0f + LABEL_SHIFT));
            }
        }

        private void CreateLetters()
        {
            if(_laneTag && _lettersLane)
            {
                for (int i = 0; i < ROWS; i++)
                {
                    GameObject go = Instantiate(_laneTag, _lettersLane);
                    Text label = go.GetComponent<Text>();
                    label.text = ((char) (65 + i)).ToString(); // A is 65 in ASCII
                }

                float cellWidth = _lettersLane.GetComponent<GridLayoutGroup>().cellSize.x;
                _lettersLane.transform.Translate(new Vector2(-cellWidth * COLUMNS / 2.0f - LABEL_SHIFT, 0.0f));
            }
        }

        public Cell GetCellFromScreenPosition(Vector2 position)
        {
            Cell[] cells = FindObjectsOfType<Cell>();
            Cell closestCell = cells[0];

            float minDistance = float.MaxValue;

            for(int i = 0; i < cells.Length; i++)
            {
                float distance = Vector2.Distance(position, cells[i].transform.position);
                if(distance < minDistance)
                {
                    minDistance = distance;
                    closestCell = cells[i];
                }
            }

            return closestCell;
        }

        public void PlaceShip(Ship ship)
        {
            Cell cell = GetCellFromScreenPosition(ship.transform.position);
            Vector2 cellPosition = cell.transform.position;

            if(blueprint.ShipType == Ship.Type.Battleship || blueprint.ShipType == Ship.Type.Destroyer)
            {
                float cellHeight = _lettersLane.GetComponent<GridLayoutGroup>().cellSize.y;
                cellPosition.y += cellHeight / 2.0f;
            }

            blueprint.transform.position = cellPosition;

            switch (blueprint.ShipType)
            {
                case Ship.Type.Destroyer:
                    break;
                case Ship.Type.Battleship:
                    break;
                case Ship.Type.Carrier:
                    break;
                case Ship.Type.Cruiser:
                    break;
                case Ship.Type.Submarine:
                    break;
            }

            _gameController.UpdateState(GameController.GameState.WaitingForPlacement);
        }
    }
}
