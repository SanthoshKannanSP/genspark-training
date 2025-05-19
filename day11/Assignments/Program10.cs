public class Program10
{
    public void Run()
    {
        var sudokuRow = new int[] { 1, 2, 3, 4, 5, 9, 7, 8, 9 };
        if (IsSudokuRowValid(sudokuRow))
            Console.WriteLine("The sudoku row is valid");
        else
            Console.WriteLine("The sudoku row is invalid");
    }

    private bool IsSudokuRowValid(int[] sudokuRow)
    {
        if (sudokuRow.Distinct().Count() == 9 && sudokuRow.Max() == 9 && sudokuRow.Min() == 1)
            return true;
        return false;
    }
}