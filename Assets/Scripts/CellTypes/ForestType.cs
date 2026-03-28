using MapLayoutGenerator;
using UnityEngine;

public class ForestType : CellType
{
    public ForestType()
    {
        EnumCellType = CellTypes.Forest;
    }

    public override bool CellTypeIsCompatable(CellType otherCellType)
    {
        return true;
    }
}
