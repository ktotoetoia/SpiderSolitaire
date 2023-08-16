using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

public class GameUI : MonoBehaviour
{
    private const string TimeLabel = "TimeLabel";
    private const string MovesLabel = "MovesLabel";
    private const string BestTimeLabel = "BestTimeLabel";
    private const string BestMovesLabel = "BestMovesLabel";
    private const string HintButton = "HintButton";

    [SerializeField] private GameOverChecker gameOverChecker;
    [SerializeField] private Hints hints;
    [Inject] private CardRowsContainer container;
    private Label timeLabel;
    private Label movesLabel;
    private Label bestTimeLabel;
    private Label bestMovesLabel;
    private float bestTime;
    private float bestMoves;
    private int movesCount;
    private bool updateTime = true;

    private void Start()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        timeLabel = root.Q<Label>(TimeLabel);
        movesLabel = root.Q<Label>(MovesLabel);
        bestTimeLabel = root.Q<Label>(BestTimeLabel);
        bestMovesLabel = root.Q<Label>(BestMovesLabel);
        root.Q<Button>(HintButton).clicked += () => hints.ShowHint();
        LoadMaxStats();
        
        gameOverChecker.OnGameOver += () =>
        {
            updateTime = false;
            SaveMaxStats();
        };

        container.OnMovePerformed += MovePerformed;

        movesLabel.text = "Moves " + movesCount;
        bestTimeLabel.text = "Best " + bestTime;
        bestMovesLabel.text = "Best " + bestMoves;
    }

    private void Update()
    {
        if (updateTime)
        {
            timeLabel.text = "Time " + (int)Time.timeSinceLevelLoad;
        }
    }

    private void MovePerformed()
    {
        movesLabel.text = "Moves " + ++movesCount;
    }

    private void SaveMaxStats()
    {
        GameSettings.Instance.SetBest((int)Time.timeSinceLevelLoad, movesCount);
    }

    private void LoadMaxStats()
    {
        bestTime = GameSettings.Instance.CurrentSuitSettings.BestTime;
        bestMoves = GameSettings.Instance.CurrentSuitSettings.BestMoves;
    }
}