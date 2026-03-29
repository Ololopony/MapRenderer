using MapLayoutGenerator;

public class BaseCellType : CellType
{
    protected CellTypeConnectionRules _cellTypeConnectionRules;

    public override bool CellTypeIsCompatable(CellType otherCellType)
    {
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
