using System.ComponentModel.DataAnnotations.Schema;

public class GridModel
{
    public int Id { get; set; }

    [NotMapped]
    public int[,] Grid
    {
        get => ConvertFlatGridTo2D(FlatGrid);
        set => FlatGrid = Convert2DToFlat(value);
    }

    public int[] FlatGrid { get; set; }

    private int[,] ConvertFlatGridTo2D(int[] flatGrid)
    {
        // Assuming the original 2D array is of size 40x40
        int rows = 40;
        int columns = 40;
        int[,] grid = new int[rows, columns];

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                grid[i, j] = flatGrid[i * columns + j];
            }
        }

        return grid;
    }

    private int[] Convert2DToFlat(int[,] grid)
    {
        // Convert the 2D array to a flat array
        int rows = grid.GetLength(0);
        int columns = grid.GetLength(1);
        int[] flatGrid = new int[rows * columns];

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                flatGrid[i * columns + j] = grid[i, j];
            }
        }

        return flatGrid;
    }
}