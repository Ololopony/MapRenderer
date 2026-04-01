public class MountainType : BaseCellType
{
    public MountainType(CellTypeConnectionRules cellTypeConnectionRules)
    {
        _cellTypeConnectionRules = cellTypeConnectionRules;
        EnumCellType = CellTypes.Mountain;
    }
}
