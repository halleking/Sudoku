using System.Collections.Generic;
using System.Linq;
using Random = System.Random;

public static class SudokuGenerator
{
    private const int Rows = 9;
    private const int Columns = 9;
    private const int BeginnerHidden = 5;
    private const int IntermediateHidden = 45;
    private const int AdvancedHidden = 60;
    private static readonly int[] Values = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
    
    public static (int[,], int[,]) Generate(Difficulty difficulty)
    {
        // rows must have unique values 1-9
        // cols must have unique values 1-9
        // 3x3 square subgrid must have unique values 1-9
        
        // start with empty grid
        int[,] empty = new int[Rows, Columns];

        // use solver to fill empty cells
        (bool generated, int[,] solution) = Solve(empty);

        if (generated)
        {
            // the sudoku board exists
            var result = (int[,])solution.Clone();
            var board = GeneratePlayableBoard(result, difficulty);
            return (board, solution);
        }
        
        return (empty, empty);
    }
    
    private static int[,] GeneratePlayableBoard(int[,] grid, Difficulty difficulty)
    {
        // difficulty determines the amount of cells to hide
        int amountToHide = 0;
        switch (difficulty)
        {
            case Difficulty.Beginner:
                amountToHide = BeginnerHidden;
                break;
            case Difficulty.Intermediate:
                amountToHide = IntermediateHidden;
                break;
            case Difficulty.Advanced:
                amountToHide = AdvancedHidden;
                break;
        }
        
        // randomly remove values and check if the board is still solvable with only 1 solution
        var removed = new List<int>();
        int hidden = 0;
        while (hidden < amountToHide)
        {
            Random rnd = new Random();
            int row = rnd.Next(Rows);
            int column = rnd.Next(Columns);

            if (grid[row, column] != 0)
            {
                removed.Add( grid[row, column]);
                hidden++;
                grid[row, column] = 0;
            }
            
            // attempt to solve the board after hiding value
            if (MoreThanOneSolution(grid))
            {
                // if there's more than one solution after hiding value, restore value
                grid[row, column] = removed[^1];
            }
        }
        
        return grid;
    }


    private static bool MoreThanOneSolution(int[,] grid)
    {
        int solutionCount = 0;

        void SolveWithCount(int[,] grid)
        {
            for (int i = 0; i < Rows * Columns; i++)
            {
                var row = i / Rows;
                var col = i % Columns;

                if (grid[row, col] == 0) // empty cell
                {
                    foreach (var number in Values)
                    {
                        if (ValidInRow(grid, row, number) &&
                            ValidInCol(grid, col, number) &&
                            ValidInSquare(grid, row, col, number))
                        {
                            grid[row, col] = number;

                            // recurse to next empty cell
                            SolveWithCount(grid); 
                        }
                    }

                    grid[row, col] = 0; // backtrack
                    return;
                }
            }

            // no empty cell found, this is a valid solution
            solutionCount++;
            if (solutionCount > 1)
            {
                return;
            }
        }

        SolveWithCount(grid);
        return solutionCount > 1;
    }
    
    private static (bool, int[,]) Solve(int[,] grid)
    {
        for (int i = 0; i < (Rows * Columns); i++)
        {
            var row = i / Rows;
            var col = i % Columns;
        
            if (grid[row, col] == 0) // empty cell
            {
                // shuffle values for more randomized solutions
                var rng = new Random();
                var shuffledValues = Values.OrderBy(x => rng.Next()).ToArray();

                foreach (var number in shuffledValues)
                {
                    // check all rules before assigning value
                    if (ValidInRow(grid, row, number) &&
                        ValidInCol(grid, col, number) &&
                        ValidInSquare(grid, row, col, number))
                    {
                        grid[row, col] = number;

                        if (IsGridFilled(grid))
                        {
                            // grid is solved, return result
                            return (true, grid);
                        }

                        // there are more cells to solve
                        // recursively continue solving
                        if (Solve(grid).Item1)
                        {
                            return (true, grid);
                        }
                    }
                }

                // backtrack
                grid[row, col] = 0; 
                return (false, grid);
            }
        }

        return (false, grid);
    }
    
    private static bool IsGridFilled(int[,] grid)
    {
        // check that the grid has values in every cell
        for (int r = 0; r < Rows; r++)
        {
            for (int c = 0; c < Columns; c++)
            {
                if (grid[r,c] == 0)
                {
                    return false;
                }
            }
        }
        return true;
    }

    private static bool ValidInRow(int[,] grid, int row, int number)
    {
        for (int col = 0; col < Columns; col++)
        {
            if (grid[row, col] == number)
            {
                return false;
            }
        }
        return true;
    }
    
    private static bool ValidInCol(int[,] grid, int col, int number)
    {
        for (int row = 0; row < Rows; row++)
        {
            if (grid[row, col] == number)
            {
                return false;
            }
        }
        return true;
    }

    private static bool ValidInSquare(int[,] grid, int row, int col, int number)
    {
        (int[] rowIndexes, int[] colIndexes) = GetSquareIndexes(row, col);

        foreach (var rowIndex in rowIndexes)
        {
            foreach (var colIndex in colIndexes)
            {
                if (grid[rowIndex, colIndex] == number)
                {
                    return false;
                }
            }
        }
        return true;
    }

    private static (int[], int[]) GetSquareIndexes(int row, int col)
    {
        var rIndexes = new int[3];
        var cIndexes = new int[3];
        
        if (row < 3)
        {
            rIndexes = new int[] {0, 1, 2};
        }
        else if (row < 6)
        {
            rIndexes = new int[] {3, 4, 5};
        }
        else if (row < 9)
        {
            rIndexes = new int[] {6, 7, 8};
        }
        
        if (col < 3)
        {
            cIndexes = new int[] {0, 1, 2};
        }
        else if (col < 6)
        {
            cIndexes = new int[] {3, 4, 5};
        }
        else if (col < 9)
        {
            cIndexes = new int[] {6, 7, 8};
        }
        
        return (rIndexes, cIndexes);
    }
}
