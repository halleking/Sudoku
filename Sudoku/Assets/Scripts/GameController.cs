using System;
using System.Collections;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private BoardController boardController;

    private int _timerMinutes;
    private int _timerSeconds; 
    private float _timer;

    private bool _isGameOver;
    
    public event Action<float> OnGameOver;

    private void Awake()
    {
        boardController.OnSudokuCompleted += HandleGameOver;
    }

    private void OnDestroy()
    {
        boardController.OnSudokuCompleted -= HandleGameOver;
    }

    private void OnEnable()
    {
        StopAllCoroutines();
        StartTimer();
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    public void GenerateBoard(Difficulty difficulty)
    {
        _isGameOver = false;
        boardController.ResetBoard();
        
        (int[,] board, int[,] solution) = SudokuGenerator.Generate(difficulty);
        boardController.InitializeBoard(board, solution);
    }
    
    private void StartTimer()
    {
        _timer = 0f;
        StartCoroutine(Tick());
    }

    private IEnumerator Tick()
    {
        while (!_isGameOver)
        {
            _timer += Time.deltaTime;
            _timerMinutes = Mathf.FloorToInt(_timer / 60f);
            _timerSeconds = Mathf.FloorToInt(_timer - (_timerMinutes * 60));
            timerText.text = $"{_timerMinutes:00}:{_timerSeconds:00}";
            yield return null; // wait until the next frame
        }
    }

    private void HandleGameOver()
    {
        _isGameOver = true;
        OnGameOver?.Invoke(_timer);
    }
}