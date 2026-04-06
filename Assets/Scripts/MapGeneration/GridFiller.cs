using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using MapLayoutGenerator;
using UnityEngine;

public class GridFiller : MonoBehaviour
{
    [SerializeField]
    private List<RealCell> _allPossibleCells = new List<RealCell>();

    private List<CellTreasureHolder> _cellTreasureHolders = new List<CellTreasureHolder>();
    private TreasureInstantiator _treasureInstantiator = new TreasureInstantiator(100);
    private LayoutGenerator _layoutGenerator = new LayoutGenerator();
    private Layout _layout;
    private RealCell[,] _realGrid;
    private List<Vector2Int> _toFill = new List<Vector2Int>();
    private Vector2Int[] _offsets = new Vector2Int[]
    {
        new Vector2Int(0, 1),   //Up
        new Vector2Int(0, -1),  //Down
        new Vector2Int(-1, 0),   //Right
        new Vector2Int(1, 0)   //Left
    };

    public async Task StartLayoutGenerator(string prompt)
    {
        await _layoutGenerator.InitiateGenerator(prompt);
        _layoutGenerator.GenerateLayout();
        _layout = _layoutGenerator.GetLayout();
    }

    public void StartFiller()
    {
        _realGrid = new RealCell[_layout.GetMapWidth(), _layout.GetMapHight()];

        StartCoroutine(FillMap());
        _treasureInstantiator.SetTreasureForCells(_cellTreasureHolders);
    }

    private IEnumerator FillMap()
    {
        _toFill.Clear();
        _toFill.Add(new Vector2Int(_layout.GetMapWidth() / 2, _layout.GetMapHight() / 2));

        while (_toFill.Count > 0)
        {
            int x = _toFill[0].x;
            int y = _toFill[0].y;

            List<RealCell> potentialCells = new List<RealCell>(_allPossibleCells);

            RemoveCellsByType(potentialCells, _layout.GetCellByIndex(x, y).GetCellType());

            for (int i = 0; i < _offsets.Length; i++)
            {
                Vector2Int neighbour = new Vector2Int(x + _offsets[i].x, y + _offsets[i].y);

                if (IsInsideGrid(neighbour))
                {
                    RealCell neighbourCell = _realGrid[neighbour.x, neighbour.y];

                    if (neighbourCell != null)
                    {
                        switch (i)
                        {
                            case 0:
                                RemoveCellsByEdges(potentialCells, neighbourCell.GetEdgeByRelativeDirection(RelativeDirection.Down));
                                break;
                            case 1:
                                RemoveCellsByEdges(potentialCells, neighbourCell.GetEdgeByRelativeDirection(RelativeDirection.Up));
                                break;
                            case 2:
                                RemoveCellsByEdges(potentialCells, neighbourCell.GetEdgeByRelativeDirection(RelativeDirection.Left));
                                break;
                            case 3:
                                RemoveCellsByEdges(potentialCells, neighbourCell.GetEdgeByRelativeDirection(RelativeDirection.Right));
                                break;
                        }
                    }
                    else
                    {
                        if (!_toFill.Contains(neighbour))
                        {
                            _toFill.Add(neighbour);
                        }
                    }
                }
            }

            if (potentialCells.Count < 1)
            {
                _realGrid[x, y] = _allPossibleCells[0];
                Debug.LogWarning("No compatable cells at point " + x + ", " + y);
            }
            else
            {
                _realGrid[x, y] = potentialCells[Random.Range(0, potentialCells.Count)];
            }

            GameObject cell = Instantiate(_realGrid[x, y].GetCellPrefab(), new Vector3(y * 3, 0f, x * 3), Quaternion.identity);
            _cellTreasureHolders.Add(cell.GetComponent<CellTreasureHolder>());
            yield return new WaitForEndOfFrame();
            _toFill.RemoveAt(0);
        }
    }

    private void RemoveCellsByEdges(List<RealCell> potentialCells, Edge edge)
    {
        for (int i = potentialCells.Count - 1; i >= 0; i--)
        {
            RealCell potentialCell = potentialCells[i];
            if (!potentialCell.GetOppositeEdge(edge).GetEnumEdgeType().Equals(edge.GetEnumEdgeType()))
            {
                potentialCells.Remove(potentialCell);
            }
        }
    }

    private void RemoveCellsByType(List<RealCell> potentialCells, CellType cellType)
    {
        Debug.Log(cellType.ToString());
        for (int i = potentialCells.Count - 1; i >= 0; i--)
        {
            RealCell potentialCell = potentialCells[i];
            if (!potentialCell.GetEnumCellType().Equals(cellType.EnumCellType))
            {
                potentialCells.Remove(potentialCell);
            }
        }
    }

    private bool IsInsideGrid(Vector2Int cellVector)
    {
        return cellVector.x > -1 && cellVector.x < _layout.GetMapWidth() && cellVector.y > -1 && cellVector.y < _layout.GetMapHight();
    }
}
