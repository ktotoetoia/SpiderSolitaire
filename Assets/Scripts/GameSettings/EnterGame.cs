using UnityEngine;

public class EnterGame : MonoBehaviour
{
    private void OnEnable()
    {
        Application.targetFrameRate = 60;
    }
}