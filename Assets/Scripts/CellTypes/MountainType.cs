using System.Collections.Generic;
using MapLayoutGenerator;
using UnityEngine;

public class MountainType : CellType
{
    private CellTypeConnectionRules _cellTypeConnectionRules;

    public MountainType()
    {
        EnumCellType = CellTypes.Forest;
    }

    public override bool CellTypeIsCompatable(CellType otherCellType)
    {
        // if (otherCellType.EnumCellType.Equals(CellTypes.Lake))
        // {
        //     return false;
        // }
        // else
        // {
        //     return true;
        // }
        if (_cellTypeConnectionRules.GetListOfCopatableCellTypes().Contains(otherCellType))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
