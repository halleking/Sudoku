# Sudoku
A simple sudoku puzzle game developed with Unity/C#. There are three levels of diffulty to choose from: Beginner, Intermediate, and Advanced. The level of difficulty corresponds to an increased number of cells to fill in the grid. My goal for this project was to create a playable game in just one day, so advanced features and polished UI are not present. 

## How to Play
The rules of sudoku are as follows:
- Each row should have numbers 1-9, no repeats.
- Each column should have numbers 1-9, no repeats.
- Each 3x3 sub-grid should have numbers 1-9, no repeats.

Selecting a cell highlights it and enables editing. Use your keyboard to enter a value 1-9 in the selected cell. Only the initially empty cells are editable. 

The game is playable [here](https://halleking.github.io/Sudoku/)! 

## Puzzle Generation
Start with an initially empty 9x9 grid represented as a 2D array. Starting in the top left corner, find the next empty cell. Randomly assign a value 1-9, verifying that this value follows the sudoku rules and does not already exist in the row, column, or sub-grid. If a valid value exists, continue the process on the next empty cell. If there is not a valid value for the cell, backtrack to the previous cell and change the value, then continue. Continue the process until the entire grid is filled.

The selected level of difficulty determines how many initial cells need to be hidden. Until this number is met, randomly select a cell in the the graph to hide, hide the value and check that there only exists one possible solution to the puzzle. If more than one solution exists by hiding the value, unhide the cell and continue with a different cell, thus ensuring that the generated puzzle only has one valid solution. 

See [SudokuGenerator.cs](https://github.com/halleking/Sudoku/blob/main/Sudoku/Assets/Scripts/SudokuGenerator.cs)

## Getting Started
### Prerequisites
- Unity 2022.3.26f1
- Visual Studio or other C# IDE 
