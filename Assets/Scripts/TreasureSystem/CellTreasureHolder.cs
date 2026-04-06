using UnityEngine;

public class CellTreasureHolder : MonoBehaviour
{
    private bool _hasTreasure;

    public void SetHasTreasure(bool hasTreasure)
    {
        _hasTreasure = hasTreasure;
    }

    public bool GetHasTreasure()
    {
        return _hasTreasure;
    }
}
