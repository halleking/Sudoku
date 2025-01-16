using System;
using System.Collections.Generic;
using UnityEngine;

public class BoardController : MonoBehaviour
{
    [SerializeField] private RectTransform boardParentRt;
    private List<Cell> _cells;
    private int[,] _solution;
    private bool _isCompleted;
    public event Action OnSudokuCompleted;
    
    private void Awake()
    {
        _cells = new List<Cell>(boardParentRt.GetComponentsInChildren<Cell>());
    }

    public void InitializeBoard(int[,] board, int[,] solution)
    {
        _isCompleted = false;
        _solution = solution;
        // fill board with initial values
        for (int r = 0; r < board.GetLength(0); r++)
        {
            for (int c = 0; c < board.GetLength(1); c++)
            {
                var cell = _cells[(r * board.GetLength(0)) + c];
                cell.InitializeCell(r, c, board[r, c]);
                cell.OnCellChanged += CheckSolution;
            }
        }
    }

    public void ResetBoard()
    {
        foreach (Cell cell in _cells)
        {
            cell.OnCellChanged -= CheckSolution;
            cell.ResetCell();
        }
    }

    private void CheckSolution()
    {
        if (_isCompleted)
        {
            return;
        }
        
        foreach (Cell cell in _cells)
        {
            if (cell.Value != _solution[cell.Row, cell.Column])
            {
                return;
            }
        }
        
        _isCompleted = true;
        OnSudokuCompleted?.Invoke();
    }
}
