using UnityEngine;
using UnityEngine.UIElements;

public class OpenUrlButton : MonoBehaviour
{
    [SerializeField] private string buttonName;
    [SerializeField] private string url;

    private void OnEnable()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        Button button = root.Q<Button>(buttonName);

        button.clicked += () => Application.OpenURL(url);
    }
}