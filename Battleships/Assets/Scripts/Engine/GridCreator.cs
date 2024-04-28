using UnityEngine;
using UnityEngine.UI;

namespace Battleships.Engine
{
    public class GridCreator : MonoBehaviour
    {
        private const int COLUMNS = 15;
        private const int ROWS = 10;

        [SerializeField]
        private GameObject _baseTile;
        [SerializeField]
        private GameObject _laneTag;

        [SerializeField]
        private Transform _numbersLane;
        [SerializeField]
        private Transform _lettersLane;

        private void Awake()
        {
            CreateGrid();
            CreateNumbers();
            CreateLetters();
        }

        private void CreateGrid()
        {
            if(_baseTile)
            {
                for(int i = 0; i < COLUMNS; i++)
                {
                    for(int j = 0; j < ROWS; j++)
                    {
                        Instantiate(_baseTile, transform);
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
                _numbersLane.transform.Translate(new Vector2(0.0f, cellHeight * ROWS / 2.0f + 60.0f));
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
                    label.text = ((char) (65 + i)).ToString();
                }

                float cellWidth = _lettersLane.GetComponent<GridLayoutGroup>().cellSize.x;
                _lettersLane.transform.Translate(new Vector2(-cellWidth * COLUMNS / 2.0f -60.0f, 0.0f));
            }
        }
    }
}