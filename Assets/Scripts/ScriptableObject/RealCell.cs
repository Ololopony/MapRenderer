using System.Collections.Generic;
using UnityEngine;
using MapLayoutGenerator;

[CreateAssetMenu (fileName = "RealCell", menuName = "Real Cell")]
public class RealCell : ScriptableObject
{
    [SerializeField]
    private List<Edge> _edges = new List<Edge>();
    [SerializeField]
    private GameObject _cellPrefab;
    [SerializeField]
    private CellTypes _enumCellType;

    public GameObject GetCellPrefab()
    {
        return _cellPrefab;
    }

    public List<Edge> GetEdges()
    {
        return _edges;
    }

    public CellTypes GetEnumCellType()
    {
        return _enumCellType;
    }
    
    public Edge GetOppositeEdge(Edge otherEdge)
    {
        switch (otherEdge.GetDirection())
        {
            case RelativeDirection.Up:
                return GetEdgeByRelativeDirection(RelativeDirection.Down);
            case RelativeDirection.Right:
                return GetEdgeByRelativeDirection(RelativeDirection.Left);
            case RelativeDirection.Down:
                return GetEdgeByRelativeDirection(RelativeDirection.Up);
            case RelativeDirection.Left:
                return GetEdgeByRelativeDirection(RelativeDirection.Right);
        }
        return GetEdgeByRelativeDirection(RelativeDirection.Up);
    }

    public Edge GetEdgeByRelativeDirection(RelativeDirection relativeDirection)
    {
        foreach (Edge edge in _edges)
        {
            if (edge.GetDirection() == relativeDirection)
            {
                return edge;
            }
        }
        return _edges[0];
    }
}
