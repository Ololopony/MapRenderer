using System.Collections.Generic;
using UnityEngine;

public class RealCell : ScriptableObject
{
    [SerializeField]
    private List<Edge> _edges = new List<Edge>();
    [SerializeField]
    private GameObject _cellPrefab;

    public GameObject GetCellGameObject()
    {
        return _cellPrefab;
    }

    public List<Edge> GetEdges()
    {
        return _edges;
    }
}
