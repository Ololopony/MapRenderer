using UnityEngine;
using UnityEngine.UI;

public class InputFiledHandler : MonoBehaviour
{
    [SerializeField]
    private SpawnButton _spawnButton;

    void Awake()
    {
        _spawnButton.SetInputField(gameObject);
    }

    public void SetTextOnEndEdit(string text)
    {
        _spawnButton.SetInputFieldText(text);
    }
}
