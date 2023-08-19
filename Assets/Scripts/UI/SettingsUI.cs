using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SettingsUI : MonoBehaviour
{
    private const string SaveKey = "SuitsCount";

    private UIDocument document;
    private VisualElement root;
    private List<RadioButton> radioButtons;

    private RadioButton CurrentButton
    {
        get
        {
            return radioButtons[PlayerPrefs.GetInt(SaveKey)];
        }
    }

    private void Start()
    {
        document = GetComponent<UIDocument>();
        root = document.rootVisualElement;
        radioButtons = root.Query<RadioButton>().ToList();
        CurrentButton.value = true;

        RegisterCallbacks();
    }

    private void RegisterCallbacks()
    {
        radioButtons[0].RegisterCallback<ChangeEvent<bool>>((evt) => ChangeSettings(evt, 1));
        radioButtons[1].RegisterCallback<ChangeEvent<bool>>((evt) => ChangeSettings(evt, 2));
        radioButtons[2].RegisterCallback<ChangeEvent<bool>>((evt) => ChangeSettings(evt, 4));
    }

    private void SetCurrentButton(int value)
    {
        PlayerPrefs.SetInt(SaveKey, value);
    }

    private void ChangeSettings(ChangeEvent<bool> evt, int count)
    {
        if (evt.newValue)
        {
            GameSettings.Instance.SetSuitCount(count);
            SetCurrentButton(Mathf.Clamp(count - 1, 0, 2));
        }
    }
}