using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShovel : MonoBehaviour
{
    private CellTreasureHolder _cellTreasureHolder;
    private Mouse _mouse = Mouse.current;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out CellTreasureHolder cellTreasureHolder))
        {
            _cellTreasureHolder = cellTreasureHolder;
        }
    }

    private void Update()
    {
        if (_mouse.leftButton.wasPressedThisFrame)
        {
            Shovel();
        }
    }

    private void Shovel()
    {
        if (_cellTreasureHolder.GetHasTreasure())
        {
            Debug.Log("Treasure found");
            _cellTreasureHolder.SetHasTreasure(false);
        }
        else
        {
            Debug.Log("Nothing here");
        }
    }
}
