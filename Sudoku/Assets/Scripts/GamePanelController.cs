using System;
using UnityEngine;

public class GamePanelController : MonoBehaviour
{
    private static readonly int GoToDifficultyPanel = Animator.StringToHash("GoToDifficultyPanel");
    private static readonly int GoToGamePanel = Animator.StringToHash("GoToGamePanel");
    private static readonly int GoToResultsPanel = Animator.StringToHash("GoToResultsPanel");

    [SerializeField] private Animator panelAnimator;
    [SerializeField] private TitlePanelController titlePanel;
    [SerializeField] private DifficultyPanelController difficultyPanel;
    [SerializeField] private GameController gameController;
    [SerializeField] private ResultsController resultPanel;

    private void Awake()
    {
        titlePanel.OnPlayClicked += HandleOnPlayClicked;
        difficultyPanel.OnDifficultySelected += HandleSelectDifficulty;
        gameController.OnGameOver += HandleGameOver;
        resultPanel.OnPlayAgainClicked += HandleOnPlayClicked;
    }

    private void OnDestroy()
    {
        titlePanel.OnPlayClicked -= HandleOnPlayClicked;
        difficultyPanel.OnDifficultySelected -= HandleSelectDifficulty;
        gameController.OnGameOver -= HandleGameOver;
        resultPanel.OnPlayAgainClicked -= HandleOnPlayClicked;
    }

    private void HandleOnPlayClicked()
    {
        // transition to difficulty selection panel
        panelAnimator.SetTrigger(GoToDifficultyPanel);
    }

    private void HandleSelectDifficulty(Difficulty difficulty)
    {
        // generate sudoku board of selected difficulty
        gameController.GenerateBoard(difficulty);

        // transition to game panel
        panelAnimator.SetTrigger(GoToGamePanel);
    }
    
    private void HandleGameOver(float timer)
    {
        resultPanel.SetResults(timer);
        panelAnimator.SetTrigger(GoToResultsPanel);
    }
}