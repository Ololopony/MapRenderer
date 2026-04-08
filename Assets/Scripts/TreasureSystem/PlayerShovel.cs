using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShovel : MonoBehaviour
{
    [SerializeField]
    private int _amountOfTreasureToDig;
    [SerializeField]
    private GameObject _foundTreasureUI;
    [SerializeField]
    private GameObject _foundAllTreasureUI;
    [SerializeField]
    private UIShowCorutine _uiShowCorutine;
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
            StartCoroutine(_uiShowCorutine.ShowIU(_foundTreasureUI, 10));
            _amountOfTreasureToDig--;
            if (_amountOfTreasureToDig == 0)
            {
                _foundAllTreasureUI.SetActive(true);
            }
            _cellTreasureHolder.SetHasTreasure(false);
        }
        else
        {
            Debug.Log("Nothing here");
        }
    }
}
