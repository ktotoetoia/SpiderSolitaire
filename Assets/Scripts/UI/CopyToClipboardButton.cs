using UnityEngine;
using UnityEngine.UIElements;

public class CopyToClipboardButton : MonoBehaviour
{
    [SerializeField] private string buttonName;
    [SerializeField] private string textToCopy;

    private void OnEnable()
    {
        Button button = GetComponent<UIDocument>().rootVisualElement.Q<Button>(buttonName);

        button.clicked += Copy;
    }

    private void Copy()
    {
        GUIUtility.systemCopyBuffer = textToCopy;
    }
}