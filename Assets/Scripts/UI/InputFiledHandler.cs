using UnityEngine;
using UnityEngine.UI;

public class InputFiledHandler : MonoBehaviour
{
    [SerializeField]
    private GenerateLayoutButton _generateLayoutButton;

    void Awake()
    {
        _generateLayoutButton.SetInputField(gameObject);
    }

    public void SetTextOnEndEdit(string text)
    {
        _generateLayoutButton.SetInputFieldText(text);
    }
}
