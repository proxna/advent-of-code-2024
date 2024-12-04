// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

string fullFile = await File.ReadAllTextAsync("index.txt");
string[] lines = fullFile.Split("\r\n");
int charactersInLine = lines[0].Length;

int[,] xmasMatrix = new int[lines.Length, charactersInLine];

for (int i = 0; i < lines.Length; i++)
{
    for (int j = 0; j < charactersInLine; j++)
    {
        xmasMatrix[i, j] = lines[i][j] switch
        {
            'X' => 1,
            'M' => 2,
            'A' => 3,
            'S' => 4,
            _ => 0
        };
    }
}

List<(int x, int y)> firstLetterLocations = FindAllValues(xmasMatrix, 1);

var filteredLocations = firstLetterLocations.Where(tp => HasANeighbor(xmasMatrix, tp.x, tp.y, 2));

int[,] matrixWithBorder = new int[xmasMatrix.GetLength(0) + 6, xmasMatrix.GetLength(1) + 6];
Array.Clear(matrixWithBorder, 0, matrixWithBorder.Length);

for (int i = 0; i < lines.Length; i++)
{
    for (int j = 0; j < charactersInLine; j++)
    {
        matrixWithBorder[i + 3, j + 3] = xmasMatrix[i, j];
    }
}

int xmasAmount = 0;

foreach ((int x, int y) in filteredLocations)
{
    int[,] fragmentGrid = ExtractSubgrid(xmasMatrix, x, y, 7);

    xmasAmount += ContainsSequence(fragmentGrid, [1, 2, 3, 4]);
}

Console.WriteLine(xmasAmount);

List<(int x, int y)> ALocations = FindAllValues(xmasMatrix, 3);

List<int[,]> xmasSamples = new List<int[,]>()
{
    new int[3,3]{
        { 2, 0, 2 },
        { 0, 3, 0 },
        { 4, 0, 4 }
    },
    new int[3,3]{
        { 2, 0, 4 },
        { 0, 3, 0 },
        { 2, 0, 4 }
    },
    new int[3,3]{
        { 4, 0, 4 },
        { 0, 3, 0 },
        { 2, 0, 2 }
    },
    new int[3,3]{
        { 4, 0, 2 },
        { 0, 3, 0 },
        { 4, 0, 2 }
    },
};

int x_masAmount = 0;

foreach ((int x, int y) in ALocations)
{
    int[,] xmasFragment = ExtractSubgrid(matrixWithBorder, x + 3, y + 3, 3);
    xmasFragment[0, 1] = 0;
    xmasFragment[1, 0] = 0;
    xmasFragment[2, 1] = 0;
    xmasFragment[1, 2] = 0;

    foreach (int[,] sample in xmasSamples)
    {
        if (xmasFragment.Cast<int>().SequenceEqual(sample.Cast<int>()))
        {
            x_masAmount++;
            break;
        }
    }
}

Console.WriteLine(x_masAmount);

// filtering the localization where to lookup to matches
List<(int x, int y)> FindAllValues(int[,] xmasMatrix, int value)
{
    List<(int x, int y)> result = new();
    for (int i = 0; i < xmasMatrix.GetLength(0); i++)
    {
        for (int j = 0; j < xmasMatrix.GetLength(1); j++)
        {
            if (xmasMatrix[i, j] == value)
                result.Add((i, j));
        }
    }

    return result;
}

//filtering first letters without any sequence
bool HasANeighbor(int[,] xmasMatrix, int x, int y, int value)
{
    for (int i = x - 1; i <= x + 1; i++)
    {
        if (i < 0 || i >= xmasMatrix.GetLength(0))
            continue;

        for (int j = y - 1; j <= y + 1; j++)
        {
            if (j < 0 || j >= xmasMatrix.GetLength(1))
                continue;

            if (xmasMatrix[i, j] == value)
                return true;
        }
    }

    return false;
}

static int ContainsSequence(int[,] grid, int[] sequence)
{
    int rows = grid.GetLength(0);
    int cols = grid.GetLength(1);
    int seqLength = sequence.Length;
    int result = 0;
    // Define direction vectors for 8 possible directions
    int[,] directions = new int[,]
    {
            { 0, 1 }, // Right
            { 1, 0 }, // Down
            { 1, 1 }, // Diagonal down-right
            { -1, 1 }, // Diagonal up-right
            { 0, -1 }, // Left
            { -1, 0 }, // Up
            { 1, -1 }, // Diagonal down-left
            { -1, -1 } // Diagonal up-left
    };

    // Check each direction
    for (int d = 0; d < directions.GetLength(0); d++)
    {
        int dr = directions[d, 0];
        int dc = directions[d, 1];

        // Check if the sequence matches in this direction
        if (MatchesSequence(grid, sequence, 3, 3, dr, dc))
        {
            result++;
        }
    }

    return result;
}

static bool MatchesSequence(int[,] grid, int[] sequence, int startRow, int startCol, int dr, int dc)
{
    int rows = grid.GetLength(0);
    int cols = grid.GetLength(1);

    for (int i = 0; i < sequence.Length; i++)
    {
        int r = startRow + i * dr;
        int c = startCol + i * dc;

        // Check bounds
        if (r < 0 || r >= rows || c < 0 || c >= cols)
        {
            return false;
        }

        // Check value
        if (grid[r, c] != sequence[i])
        {
            return false;
        }
    }

    return true;
}

int[,] ExtractSubgrid(int[,] largeGrid, int centerRow, int centerCol, int length)
{
    int rows = largeGrid.GetLength(0);
    int cols = largeGrid.GetLength(1);

    int[,] subgrid = new int[length, length];

    // Offset to define bounds of 7x7 grid
    int startRow = centerRow - ((length - 1)/2);
    int startCol = centerCol - ((length - 1)/2);

    for (int i = 0; i < length; i++)
    {
        for (int j = 0; j < length; j++)
        {
            int sourceRow = startRow + i;
            int sourceCol = startCol + j;

            // Fill subgrid with values from largeGrid or 0 if out of bounds
            if (sourceRow >= 0 && sourceRow < rows && sourceCol >= 0 && sourceCol < cols)
            {
                subgrid[i, j] = largeGrid[sourceRow, sourceCol];
            }
            else
            {
                subgrid[i, j] = 0; // Default value for out-of-bounds elements
            }
        }
    }

    return subgrid;
}