public class LakeType : BaseCellType
{
    public LakeType(CellTypeConnectionRules cellTypeConnectionRules)
    {
        _cellTypeConnectionRules = cellTypeConnectionRules;
        EnumCellType = CellTypes.Forest;
    }
}
