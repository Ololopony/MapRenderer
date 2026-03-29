public class ForestType : BaseCellType
{
    public ForestType(CellTypeConnectionRules cellTypeConnectionRules)
    {
        _cellTypeConnectionRules = cellTypeConnectionRules;
        EnumCellType = CellTypes.Forest;
    }
}
