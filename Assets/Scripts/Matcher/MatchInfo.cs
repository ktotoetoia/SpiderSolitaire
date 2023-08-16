public class MatchInfo
{
    public delegate void MatchInfoDelegate();

    public event MatchInfoDelegate OnMatchFinished;

    public void Invoke()
    {
        OnMatchFinished?.Invoke();
    }
}