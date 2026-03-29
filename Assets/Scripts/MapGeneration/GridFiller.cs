using System.Collections.Generic;
using MapLayoutGenerator;
using UnityEngine;
using UnityEngine.AI;

public class GridFiller : MonoBehaviour
{
    private LayoutGenerator _layoutGenerator = new LayoutGenerator();
    private Layout _layout;
    private RealCell[,] _realGrid;
    private List<RealCell> _allPossibleCells = new List<RealCell>();
    private List<Vector2Int> _toFill = new List<Vector2Int>();
    private Vector2Int[] _offsets = new Vector2Int[]
    {
        new Vector2Int(0, 1),
        new Vector2Int(0, -1),
        new Vector2Int(1, 0),
        new Vector2Int(-1, 0)
    };

    private void Awake()
    {
        _layoutGenerator.InitiateGenerator();
        _layoutGenerator.GenerateLayout();
        _layout = _layoutGenerator.GetLayout();
    }

    private void Start()
    {
        _realGrid = new RealCell[_layout.GetMapWidth(), _layout.GetMapHight()];

        FillMap();
    }

    private void FillMap()
    {
        _toFill.Clear();
        _toFill.Add(new Vector2Int(_layout.GetMapWidth() / 2, _layout.GetMapHight() / 2));

        while (_toFill.Count > 0)
        {
            int x = _toFill[0].x;
            int y = _toFill[0].y;

            List<RealCell> potentialCells = new List<RealCell>(_allPossibleCells);

            for (int i = 0; i < _offsets.Length; i++)
            {
                Vector2Int neighbour = new Vector2Int(x + _offsets[i].x, y + _offsets[i].y);

                if (IsInsideGrid(neighbour))
                {
                    RealCell neighbourCell = _realGrid[neighbour.x, neighbour.y];

                    if (neighbour != null)
                    {
                        RemoveCellsByType(potentialCells, _layout.GetCellByIndex(x, y).GetCellType());
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

            GameObject newCell = Instantiate(_realGrid[x, y].GetCellPrefab(), new Vector3(x, y, 0f), Quaternion.identity);
            _toFill.RemoveAt(0);
        }
    }

    private void RemoveCellsByEdges(List<RealCell> potentialCells, Edge edge)
    {
        foreach (RealCell potentialCell in potentialCells)
        {
            if (!potentialCell.GetOppositeEdge(edge).GetEnumEdgeType().Equals(edge.GetEnumEdgeType()))
            {
                potentialCells.Remove(potentialCell);
            }
        }
    }

    private void RemoveCellsByType(List<RealCell> potentialCells, CellType cellType)
    {
        foreach (RealCell potentialCell in potentialCells)
        {
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
