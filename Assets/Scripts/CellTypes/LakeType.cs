using MapLayoutGenerator;
using UnityEngine;

public class LakeType : CellType
{
    public LakeType()
    {
        EnumCellType = CellTypes.Forest;
    }

    public override bool CellTypeIsCompatable(CellType otherCellType)
    {
        if (otherCellType.EnumCellType.Equals(CellTypes.Mountain))
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
