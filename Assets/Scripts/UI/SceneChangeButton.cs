using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class SceneChangeButton : MonoBehaviour
{
    [SerializeField] private string buttonName;
    [SerializeField] private string sceneName;
    [SerializeField] private FadeTransition transition;
    private Button button;

    private void OnEnable()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        button = root.Q<Button>(buttonName);
        button.clicked += ChangeScene;
    }

    private void ChangeScene()
    {
        button.SetEnabled(false);

        if (transition != null)
        {
            SceneTransitionManager.LoadSceneAsync(sceneName, transition);

            return;
        }

        SceneManager.LoadSceneAsync(sceneName);
    }
}