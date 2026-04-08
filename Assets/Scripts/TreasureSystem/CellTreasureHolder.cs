using UnityEngine;

public class CellTreasureHolder : MonoBehaviour
{
    [SerializeField]
    private GameObject _treasureSign;

    private bool _hasTreasure;

    public void SetHasTreasure(bool hasTreasure)
    {
        _hasTreasure = hasTreasure;

        _treasureSign.SetActive(hasTreasure);
    }

    public bool GetHasTreasure()
    {
        return _hasTreasure;
    }
}
