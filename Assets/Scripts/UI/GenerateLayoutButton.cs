using System.Threading.Tasks;
using UnityEngine;

public class GenerateLayoutButton : MonoBehaviour
{
    [SerializeField]
    private GridFiller _gridFiller;
    [SerializeField]
    private GameObject _spawnButton;

    private GameObject _inputField;
    private string _inputFieldText = string.Empty;

    public void GenerationClick()
    {
        Generate();
    }

    private async Task Generate()
    {
        await _gridFiller.StartLayoutGenerator(_inputFieldText);
        gameObject.SetActive(false);
        _inputField.SetActive(false);
        _spawnButton.SetActive(true);
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
