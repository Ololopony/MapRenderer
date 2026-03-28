using System.Collections.Generic;
using MapLayoutGenerator;
using UnityEngine;

public class CellTypeConnectionRules : ScriptableObject
{
    [SerializeField]
    private List<CellType> _compatableCellTypes;

    public List<CellType> GetListOfCopatableCellTypes()
    {
        return _compatableCellTypes;
    }
}
