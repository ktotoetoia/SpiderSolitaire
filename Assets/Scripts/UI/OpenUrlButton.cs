using UnityEngine;
using UnityEngine.UIElements;

public class OpenUrlButton : MonoBehaviour
{
    [SerializeField] private string buttonName;
    [SerializeField] private string url;

    private void OnEnable()
    {
        Button button = GetComponent<UIDocument>().rootVisualElement.Q<Button>(buttonName);

        button.clicked += () => Application.OpenURL(url);
    }
}