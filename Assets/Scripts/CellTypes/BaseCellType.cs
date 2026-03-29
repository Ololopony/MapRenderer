using MapLayoutGenerator;

public class BaseCellType : CellType
{
    protected CellTypeConnectionRules _cellTypeConnectionRules;

    public override bool CellTypeIsCompatable(CellType otherCellType)
    {
        foreach (CellTypes cellTypes in _cellTypeConnectionRules.GetListOfCompatableCellTypes())
        {
            if (cellTypes.Equals(otherCellType.EnumCellType))
            {
                return true;
            }
        }
        return false;
    }
}
