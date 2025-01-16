using UnityEngine;
using UnityEngine.UI;
using System;

public enum Difficulty
{
    Beginner, 
    Intermediate, 
    Advanced,
}

public class DifficultyPanelController : MonoBehaviour
{
    [SerializeField] private Button easyButton;
    [SerializeField] private Button mediumButton;
    [SerializeField] private Button hardButton;

    public event Action<Difficulty> OnDifficultySelected;

    private void Awake()
    {
        easyButton.onClick.AddListener(() => HandleSelectDifficulty(Difficulty.Beginner));
        mediumButton.onClick.AddListener(() => HandleSelectDifficulty(Difficulty.Intermediate));
        hardButton.onClick.AddListener(() => HandleSelectDifficulty(Difficulty.Advanced));
    }

    private void OnDestroy()
    {
        easyButton.onClick.RemoveListener(() => HandleSelectDifficulty(Difficulty.Beginner));
        mediumButton.onClick.RemoveListener(() => HandleSelectDifficulty(Difficulty.Intermediate));
        hardButton.onClick.RemoveListener(() => HandleSelectDifficulty(Difficulty.Advanced));
    }

    private void HandleSelectDifficulty(Difficulty difficulty)
    {
        Debug.Log($"Difficulty selected: {difficulty}");
        OnDifficultySelected?.Invoke(difficulty);
    }
}
