using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private GameOverChecker gameOverChecker;
    [SerializeField] private float fadeInLengthInSec;
    private VisualElement root;
    private UIDocument document;

    void Start()
    {
        document = GetComponent<UIDocument>();

        root = document.rootVisualElement;
        root.style.display = DisplayStyle.None;
        root.style.opacity = 0;
        gameOverChecker.OnGameOver += Activate;
    }

    public void Activate()
    {
        root.style.display = DisplayStyle.Flex;
        StartCoroutine(ChangeRootOpacity());
    }

    private IEnumerator ChangeRootOpacity()
    {
        float currentTime = 0;
        float normalizedTime = 0;

        while (normalizedTime < 1)
        {
            currentTime += Time.deltaTime;
            normalizedTime = currentTime / fadeInLengthInSec;
            root.style.opacity = normalizedTime;
            yield return null;
        }
    }
}