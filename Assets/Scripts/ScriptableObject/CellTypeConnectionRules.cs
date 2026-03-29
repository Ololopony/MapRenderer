using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "CellTypeConnectionRules", menuName = "CellType Connection Rules")]
public class CellTypeConnectionRules : ScriptableObject
{
    [SerializeField]
    private List<CellTypes> _compatableCellTypes;

    public List<CellTypes> GetListOfCompatableCellTypes()
    {
        return _compatableCellTypes;
    }
}
