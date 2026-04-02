using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpawnButton : MonoBehaviour
{
    [SerializeField]
    private GridFiller _gridFiller;

    private GameObject _inputField;
    private string _inputFieldText = string.Empty;

    public void Spawn()
    {
        if (!_inputFieldText.Equals(string.Empty))
        {
            _gridFiller.StartFiller();
            gameObject.SetActive(false);
            _inputField.SetActive(false);
        }
    }

    public void SetInputFieldText(string inputFieldtext)
    {
        _inputFieldText = inputFieldtext;
    }

    public void SetInputField(GameObject inputField)
    {
        _inputField = inputField;
    }
}
