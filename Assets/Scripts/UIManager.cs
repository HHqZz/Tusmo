using System;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("UI Elements")]
    public GameObject scoreObject;
    public GameObject modeObject;
    public GameObject gameOverPanel;

    private int currentScore = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        UpdateScore(0);
        UpdateMode("Starting...");
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
    }

    public void UpdateScore(int score)
    {
        currentScore = score;
        SetText(scoreObject, "Score: " + score.ToString());
    }

    public void UpdateMode(string modeName)
    {
        SetText(modeObject, "Mode: " + modeName);
    }

    public void ShowGameOver()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }
    }

    public void HideGameOver()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
    }

    public void AddScore(int points)
    {
        UpdateScore(currentScore + points);
    }

    private void SetText(GameObject target, string value)
    {
        if (target == null)
            return;

        var component = GetTextComponent(target);
        if (component == null)
            return;

        var textProperty = component.GetType().GetProperty("text");
        if (textProperty != null && textProperty.CanWrite)
        {
            textProperty.SetValue(component, value);
        }
    }

    private Component GetTextComponent(GameObject target)
    {
        var typeNames = new[]
        {
            "TMPro.TextMeshProUGUI, Unity.TextMeshPro",
            "UnityEngine.UI.Text, UnityEngine.UI",
            "UnityEngine.UIElements.Label, UnityEngine.UIElements"
        };

        foreach (var typeName in typeNames)
        {
            var type = Type.GetType(typeName);
            if (type == null)
                continue;

            var component = target.GetComponent(type);
            if (component != null)
                return component;
        }

        return null;
    }
}