using System;
using System.Collections.Generic;
public class TreasureInstantiator
{
    private int _maxAmountOfTreasureCells = 0;
    private int _currentAmountOfTreasureCells = 0;
    private Random random = new Random();

    public TreasureInstantiator(int amountOfCells)
    {
        _maxAmountOfTreasureCells = amountOfCells / 10;
    }

    public void SetTreasureForCells(List<CellTreasureHolder> cellTreasureHolders)
    {
        foreach (CellTreasureHolder cellTreasureHolder in cellTreasureHolders)
        {
            if (_currentAmountOfTreasureCells == _maxAmountOfTreasureCells)
            {
                break;
            }
            if (random.Next(0, 2) == 0)
            {
                cellTreasureHolder.SetHasTreasure(true);
                _currentAmountOfTreasureCells++;
            }
            else
            {
                cellTreasureHolder.SetHasTreasure(false);
            }
        }
    }
}
