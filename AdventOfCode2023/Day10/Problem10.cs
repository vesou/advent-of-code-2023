namespace AdventOfCode2023.Day10;

public class Problem10
{
    #region Task 2

    public static int Solve2()
    {
        var inputLines = File.ReadAllLines("Day10/input1.txt").ToList();
        var maze = TranslateInput(inputLines);

        return CountTilesEnclosedByMainLoop(maze);
    }

    public static int CountTilesEnclosedByMainLoop(Maze maze)
    {
        return maze.Tiles.SelectMany(tiles => tiles).Count(tile => tile.IsInsideMainLoop);
    }

    #endregion

    #region Task 1

    public static int Solve1()
    {
        var inputLines = File.ReadAllLines("Day10/input1.txt").ToList();

        var maze = TranslateInput(inputLines);

        return FindFurthestTile(maze);
    }

    public static int FindFurthestTile(Maze maze)
    {
        return maze.Tiles.SelectMany(tiles => tiles).Where(tile => tile.TypeOfTile.IsPipe).Max(tile => tile.DistanceFromStart);
    }

    #endregion

    public static Maze TranslateInput(List<string> inputLines)
    {
        return new Maze(inputLines);
    }
}

public class Maze
{
    public Maze(List<string> inputLines)
    {
        Tiles = new Tile[inputLines.Count][];
        for (int y = 0; y < inputLines.Count; y++)
        {
            Tiles[y] = new Tile[inputLines[y].Length];
            for (int x = 0; x < inputLines[y].Length; x++)
            {
                Tiles[y][x] = new Tile(inputLines[y][x], x, y);
            }
        }

        for (int y = 0; y < inputLines.Count; y++)
        {
            for (int x = 0; x < inputLines[y].Length; x++)
            {
                Tile currentTile = Tiles[y][x];
                if (Tiles[y][x].TypeOfTile.IsPipe)
                {
                    List<Tile?> neighbours =
                    [
                        GetNeighbour(Tiles, y + currentTile.TypeOfTile.Neighbour1Transition.y,
                            x + currentTile.TypeOfTile.Neighbour1Transition.x),
                        GetNeighbour(Tiles, y + currentTile.TypeOfTile.Neighbour2Transition.y,
                            x + currentTile.TypeOfTile.Neighbour2Transition.x)
                    ];

                    currentTile.Neighbours = neighbours.Where(tile => tile != null).Select(tile => tile!).ToArray();
                }
            }
        }

        // calculate distance from start
        Tile startTile = Tiles.SelectMany(tiles => tiles).First(tile => tile.TypeOfTile.Type == 'S');
        startTile.DistanceFromStart = 0;
        startTile.Neighbours = FindStartNeighbours(startTile, Tiles);
        Queue<Tile> tilesToProcess = new();
        tilesToProcess.Enqueue(startTile);
        while (tilesToProcess.Any())
        {
            Tile currentTile = tilesToProcess.Dequeue();
            Tiles[currentTile.Y][currentTile.X].Processed = true;
            Tiles[currentTile.Y][currentTile.X].PartOfMainLoop = true;
            foreach (Tile neighbour in currentTile.Neighbours.Where(tile => !tile.Processed))
            {

                neighbour.DistanceFromStart = currentTile.DistanceFromStart + 1;
                tilesToProcess.Enqueue(neighbour);
            }
        }

        // find out which direction is inside by counting number of turns
        Tile nextNeighbour = startTile.Neighbours[0];
        bool isAntiClockwise = NumberOfLeftTurnsIsMoreThanRightTurns(startTile, nextNeighbour);

        Queue<(Tile previousTile, Tile currentTile)> tilesToProcess2 = new();
        tilesToProcess2.Enqueue((startTile, nextNeighbour));
        while (tilesToProcess2.Any())
        {
            (Tile previousTile, Tile currentTile) = tilesToProcess2.Dequeue();
            MarkAllInsideTiles(Tiles, currentTile, previousTile, isAntiClockwise);
            Tile neighbour = currentTile.Neighbours.First(tile => tile != previousTile);
            if(neighbour == startTile)
                break;
            tilesToProcess2.Enqueue((currentTile, neighbour));
        }

    }

    private void MarkAllInsideTiles(Tile[][] tiles, Tile currentTile, Tile previousTile, bool isAntiClockwiseDirection)
    {
        List<char> notAllowedTiles = new List<char>();
        int xMovement = previousTile.X - currentTile.X;
        int yMovement = previousTile.Y - currentTile.Y;
        int insideDirectionY = isAntiClockwiseDirection ? 1 : -1;

        if (xMovement != 0)
        {
            insideDirectionY *= xMovement;
        }
        else if (yMovement != 0)
        {
            insideDirectionY += yMovement;
        }

        if(yMovement < 0) // moving down on board
        {
            notAllowedTiles.Add('|');
            if(isAntiClockwiseDirection)
                notAllowedTiles.Add('L');
            else
                notAllowedTiles.Add('J');

            insideDirectionY = 1;
        }

        if(yMovement > 0) // moving up the board
        {
            notAllowedTiles.Add('|');
            if(isAntiClockwiseDirection)
                notAllowedTiles.Add('7');
            else
                notAllowedTiles.Add('F');

            insideDirectionY = -1;
        }

        if (notAllowedTiles.Contains(currentTile.TileChar)) return;

        int currY = currentTile.Y + insideDirectionY;
        int currX = currentTile.X;
        while (currY >= 0 && currY < tiles.Length && !tiles[currY][currX].PartOfMainLoop)
        {
            tiles[currY][currX].IsInsideMainLoop = true;
            currY += insideDirectionY;
        }
    }

    private bool NumberOfLeftTurnsIsMoreThanRightTurns(Tile startTile, Tile nextNeighbour)
    {
        int numberOfLeftTurns = 0;
        int numberOfRightTurns = 0;
        Tile previousTile = startTile;
        Tile currentTile = nextNeighbour;

        while (true)
        {
            Tile nextTile = GetNextTile(currentTile, previousTile);

            if(nextTile.TileChar == TileType.Start.Type)
                break;
            if (IsLeftTurn(currentTile, nextTile))
            {
                numberOfLeftTurns++;
            }
            else if (nextTile.TileChar == TileType.HorizontalPipe.Type || nextTile.TileChar == TileType.VerticalPipe.Type)
            {
                // do nothing
            }
            else
            {
                numberOfRightTurns++;
            }

            previousTile = currentTile;
            currentTile = nextTile;
        }

        return numberOfLeftTurns > numberOfRightTurns;
    }

    private bool IsLeftTurn(Tile currentTile, Tile nextTile)
    {
        if (IsTop(nextTile.Y, currentTile.Y))
        {
            return nextTile.TileChar == TileType.SouthWestBend.Type;
        }
        else if (IsBottom(nextTile.Y, currentTile.Y))
        {
            return nextTile.TileChar == TileType.NorthEastBend.Type;
        }
        else if (IsLeft(nextTile.X, currentTile.X))
        {
            return nextTile.TileChar == TileType.SouthEastBend.Type;
        }
        else if (IsRight(nextTile.X, currentTile.X))
        {
            return nextTile.TileChar == TileType.NorthWestBend.Type;
        }

        return false;
    }

    private bool IsRight(int nextTileX, int currentTileX)
    {
        return nextTileX > currentTileX;
    }

    private bool IsLeft(int nextTileX, int currentTileX)
    {
        return nextTileX < currentTileX;
    }

    private bool IsBottom(int nextTileY, int currentTileY)
    {
        return nextTileY > currentTileY;
    }

    private bool IsTop(int nextTileY, int currentTileY)
    {
        return nextTileY < currentTileY;
    }

    private Tile GetNextTile(Tile currentTile, Tile previousTile)
    {
        return currentTile.Neighbours.First(tile => tile != previousTile);
    }

    private Tile? GetNeighbour(Tile[][] tiles, int neighbourY, int neighbourX)
    {
        if (neighbourY < 0 || neighbourY >= tiles.Length)
        {
            return null;
        }

        if (neighbourX < 0 || neighbourX >= tiles[neighbourY].Length)
        {
            return null;
        }

        return tiles[neighbourY][neighbourX];
    }

    private Tile[] FindStartNeighbours(Tile startTile, Tile[][] tiles)
    {
        List<Tile> neighbours = new();
        if (IsNeighbourConnectedToStartTile(tiles, startTile, startTile.Y, startTile.X-1))
        {
            neighbours.Add(tiles[startTile.Y][startTile.X - 1]);
        }

        if (IsNeighbourConnectedToStartTile(tiles, startTile, startTile.Y, startTile.X+1))
        {
            neighbours.Add(tiles[startTile.Y][startTile.X + 1]);
        }

        if (IsNeighbourConnectedToStartTile(tiles, startTile, startTile.Y-1, startTile.X))
        {
            neighbours.Add(tiles[startTile.Y-1][startTile.X]);
        }

        if (IsNeighbourConnectedToStartTile(tiles, startTile, startTile.Y+1, startTile.X))
        {
            neighbours.Add(tiles[startTile.Y+1][startTile.X]);
        }

        return neighbours.ToArray();
    }

    private bool IsNeighbourConnectedToStartTile(Tile[][] tiles, Tile startTile, int neighbourY, int neighbourX)
    {
        if (neighbourY < 0 || neighbourY >= tiles.Length)
        {
            return false;
        }

        if (neighbourX < 0 || neighbourX >= tiles[neighbourY].Length)
        {
            return false;
        }

        Tile neighbour = tiles[neighbourY][neighbourX];
        if (neighbour.TypeOfTile.IsPipe && neighbour.Neighbours.Any(x => x == startTile))
        {
            return true;
        }

        return false;
    }

    public Tile[][] Tiles { get; set; }
}

public class Tile
{
    public Tile(char typeOfTile, int x, int y)
    {
        TypeOfTile = TileType.GetTileType(typeOfTile);
        X = x;
        Y = y;
        TileChar = typeOfTile;
    }

    public char TileChar { get; set; }

    public int X { get; set; }
    public int Y { get; set; }
    public TileType TypeOfTile { get; set; }
    public Tile[] Neighbours { get; set; }
    public int DistanceFromStart { get; set; } = -1;
    public bool Processed { get; set; } = false;
    public bool IsInsideMainLoop { get; set; } = false;
    public bool PartOfMainLoop { get; set; } = false;
}

/*
 * | is a vertical pipe connecting north and south.
   - is a horizontal pipe connecting east and west.
   L is a 90-degree bend connecting north and east.
   J is a 90-degree bend connecting north and west.
   7 is a 90-degree bend connecting south and west.
   F is a 90-degree bend connecting south and east.
   . is ground; there is no pipe in this tile.
   S is the starting position of the animal; there is a pipe on this tile, but your sketch doesn't show what shape the pipe has.
 */
public class TileType
{
    private TileType(char type, (int x, int y) neighbour1Transition, (int x, int y) neighbour2Transition)
    {
        Type = type;
        IsPipe = type != '.' && type != 'S'; // Ground and Start are not counted as pipes as S needs a special treatment
        Neighbour1Transition = neighbour1Transition;
        Neighbour2Transition = neighbour2Transition;
    }

    public char Type { get; set; }
    public bool IsPipe { get; set; }
    public (int x, int y) Neighbour1Transition { get; set; }
    public (int x, int y) Neighbour2Transition { get; set; }

    public static TileType VerticalPipe => new('|', (0, -1), (0, 1));
    public static TileType HorizontalPipe => new('-', (-1, 0), (1, 0));
    public static TileType NorthEastBend => new('L', (1, 0), (0, -1));
    public static TileType NorthWestBend => new('J', (-1, 0), (0, -1));
    public static TileType SouthWestBend => new('7', (-1, 0), (0, 1));
    public static TileType SouthEastBend => new('F', (1, 0), (0, 1));
    public static TileType Ground => new('.', (0, 0), (0, 0));
    public static TileType Start => new('S', (0, 0), (0, 0));

    public static TileType GetTileType(char type)
    {
        return type switch
        {
            '|' => VerticalPipe,
            '-' => HorizontalPipe,
            'L' => NorthEastBend,
            'J' => NorthWestBend,
            '7' => SouthWestBend,
            'F' => SouthEastBend,
            '.' => Ground,
            'S' => Start,
            _ => Ground
        };
    }
}
