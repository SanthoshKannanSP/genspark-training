public class Program11
{
    public bool IsSudokuValid(int[,] grid)
    {
        for(int r=0;r<9;r++)
        {
            bool[] row = new bool[9];
            bool[] col = new bool[9];
            for(int c=0;c<9;c++)
            {
                int num = grid[r, c]-1;
                if (row[num])
                    return false;
                row[num] = true;
                if (col[num]) 
                    return false;
                col[num] = true;
            }
        }

        for(int r=0; r<9;r+=3)
        {
            for(int c=0; c<9;c+=3)
            {
                bool[] box = new bool[9];
                for(int i=0;i<3;i++)
                {
                    for(int j=0;j<3;j++)
                    {
                        int num = grid[r + i, c + j]-1;
                        if (box[num])
                            return false;
                        box[num] = true;
                    }
                }
            }
        }
        return true;
        
    }
    public void Run()
    {

        int[,] validSudokuBoard = new int[,]
        {
             {5, 3, 4, 6, 7, 8, 9, 1, 2},
             {6, 7, 2, 1, 9, 5, 3, 4, 8},
             {1, 9, 8, 3, 4, 2, 5, 6, 7},
             {8, 5, 9, 7, 6, 1, 4, 2, 3},
             {4, 2, 6, 8, 5, 3, 7, 9, 1},
             {7, 1, 3, 9, 2, 4, 8, 5, 6},
             {9, 6, 1, 5, 3, 7, 2, 8, 4},
             {2, 8, 7, 4, 1, 9, 6, 3, 5},
             {3, 4, 5, 2, 8, 6, 1, 7, 9}
        };

        int[,] invalidSudokuBoard = new int[,]
        {
             {5, 3, 4, 6, 7, 8, 9, 1, 2},
             {6, 7, 2, 1, 9, 5, 3, 4, 8},
             {1, 9, 8, 3, 4, 2, 5, 6, 7},
             {8, 5, 9, 7, 6, 1, 4, 2, 3},
             {4, 2, 6, 8, 5, 3, 7, 9, 1},
             {7, 1, 3, 9, 2, 4, 8, 5, 6},
             {9, 6, 1, 5, 3, 7, 2, 8, 4},
             {2, 8, 7, 4, 1, 9, 6, 3, 5},
             {3, 4, 5, 2, 8, 6, 1, 7, 5}
        };

        if (IsSudokuValid(validSudokuBoard))
            Console.WriteLine("The sudoku board is valid");
        else
            Console.WriteLine("The sudoku board is invalid");
    }
}