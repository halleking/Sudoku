using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResultsController : MonoBehaviour
{
    
    [SerializeField] private TMP_Text resultsText;
    [SerializeField] private Button playAgainButton;
    
    public event Action OnPlayAgainClicked;
    
    private void Awake()
    {
        playAgainButton.onClick.AddListener(PlayAgain);
    }

    private void OnDestroy()
    {
        playAgainButton.onClick.RemoveListener(PlayAgain);
    }

    private void PlayAgain()
    {
        // play again button pressed
        OnPlayAgainClicked?.Invoke();
    }

    public void SetResults(float timer)
    {
        var minutes = Mathf.FloorToInt(timer / 60f);
        var seconds = Mathf.FloorToInt(timer - (minutes * 60));
        resultsText.text = $"Nice Job!\nYou completed the puzzle in {minutes:00}:{seconds:00}";
    }
}