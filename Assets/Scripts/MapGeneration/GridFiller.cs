using System.Collections;
using System.Collections.Generic;
using MapLayoutGenerator;
using UnityEngine;

public class GridFiller : MonoBehaviour
{
    [SerializeField]
    private List<RealCell> _allPossibleCells = new List<RealCell>();

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

    private void Awake()
    {
        _layoutGenerator.InitiateGenerator();
        _layoutGenerator.GenerateLayout();
        _layout = _layoutGenerator.GetLayout();
    }

    private void Start()
    {
        _realGrid = new RealCell[_layout.GetMapWidth(), _layout.GetMapHight()];

        StartCoroutine(FillMap());
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

            for (int i = 0; i < _offsets.Length; i++)
            {
                Vector2Int neighbour = new Vector2Int(x + _offsets[i].x, y + _offsets[i].y);

                if (IsInsideGrid(neighbour))
                {
                    RealCell neighbourCell = _realGrid[neighbour.x, neighbour.y];

                    if (neighbourCell != null)
                    {
                        //RemoveCellsByType(potentialCells, _layout.GetCellByIndex(x, y).GetCellType());
                        switch (i)
                        {
                            case 0:
                                RemoveCellsByEdges(potentialCells, neighbourCell.GetEdgeByRelativeDirection(RelativeDirection.Down));
                                Debug.Log("Сосед сверху");
                                Debug.Log(neighbourCell.GetEdgeByRelativeDirection(RelativeDirection.Down).GetDirection());
                                Debug.Log(neighbourCell.GetEdgeByRelativeDirection(RelativeDirection.Down).GetEnumEdgeType());
                                Debug.Log("____________________________________");
                                break;
                            case 1:
                                RemoveCellsByEdges(potentialCells, neighbourCell.GetEdgeByRelativeDirection(RelativeDirection.Up));
                                Debug.Log("Сосед снизу");
                                Debug.Log(neighbourCell.GetEdgeByRelativeDirection(RelativeDirection.Up).GetDirection());
                                Debug.Log(neighbourCell.GetEdgeByRelativeDirection(RelativeDirection.Up).GetEnumEdgeType());
                                Debug.Log("____________________________________");
                                break;
                            case 2:
                                RemoveCellsByEdges(potentialCells, neighbourCell.GetEdgeByRelativeDirection(RelativeDirection.Left));
                                Debug.Log("Сосед справа");
                                Debug.Log(neighbourCell.GetEdgeByRelativeDirection(RelativeDirection.Left).GetDirection());
                                Debug.Log(neighbourCell.GetEdgeByRelativeDirection(RelativeDirection.Left).GetEnumEdgeType());
                                Debug.Log("____________________________________");
                                break;
                            case 3:
                                RemoveCellsByEdges(potentialCells, neighbourCell.GetEdgeByRelativeDirection(RelativeDirection.Right));
                                Debug.Log("Сосед слева");
                                Debug.Log(neighbourCell.GetEdgeByRelativeDirection(RelativeDirection.Right).GetDirection());
                                Debug.Log(neighbourCell.GetEdgeByRelativeDirection(RelativeDirection.Right).GetEnumEdgeType());
                                Debug.Log("____________________________________");
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

            Instantiate(_realGrid[x, y].GetCellPrefab(), new Vector3(y * 3, 0f, x * 3), Quaternion.identity);
            foreach (Edge edge in _realGrid[x, y].GetEdges())
            {
                Debug.Log("Края клетки:");
                Debug.Log(edge.GetDirection());
                Debug.Log(edge.GetEnumEdgeType());
            }
            Debug.Log("_________________________");
            yield return new WaitForSecondsRealtime(2);
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
                // Debug.Log("--------------------------------------------------------------");
                // Debug.Log(potentialCell.GetOppositeEdge(edge).GetEnumEdgeType().ToString());
                // Debug.Log(edge.GetEnumEdgeType().ToString());
                // Debug.Log("--------------------------------------------------------------");
                potentialCells.Remove(potentialCell);
            }
            else
            {
                // Debug.Log("--------------------------------------------------------------");
                // Debug.Log(potentialCell.GetOppositeEdge(edge).GetEnumEdgeType().ToString());
                // Debug.Log(edge.GetEnumEdgeType().ToString());
                // Debug.Log("--------------------------------------------------------------");
            }
        }
    }

    private void RemoveCellsByType(List<RealCell> potentialCells, CellType cellType)
    {
        for (int i = 0; i < potentialCells.Count; i++)
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
