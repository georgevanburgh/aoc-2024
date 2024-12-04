var input = File.ReadLines("day04/input.txt");

var parsed = input.Select(x => x.ToCharArray()).ToArray();

List<Direction> directions1 = [Direction.Up, Direction.Down, Direction.Left, Direction.Right, Direction.DiagonalUpLeft, Direction.DiagonalUpRight, Direction.DiagonalDownLeft, Direction.DiagonalDownRight];
var matches1 = GetMatches(directions1, "XMAS");
Console.WriteLine($"Part 1: {matches1.Length}");

List<Direction> directions2 = [Direction.DiagonalUpLeft, Direction.DiagonalUpRight, Direction.DiagonalDownLeft, Direction.DiagonalDownRight];
var matches2 = GetMatches(directions2, "MAS");
var count = matches2.GroupBy(x => x).Count(x => x.Count() > 1);
Console.WriteLine($"Part 2: {count}");

(int, int)[] GetMatches(IEnumerable<Direction> directions, string word)
{
    var matches = new List<(int, int)>();
    var wordChars = word.ToCharArray();

    foreach (Direction direction in directions)
    {
        var offset = GetOffsetForDirection(direction);

        for (int y = 0; y < parsed.Length; y++)
        {
            for (int x = 0; x < parsed[y].Length; x++)
            {
                for (int i = 0; i < wordChars.Length; i++)
                {
                    if (parsed.GetValueOrDefault(x + offset.x * i, y + offset.y * i) != wordChars[i])
                    {
                        break;
                    }

                    if (i == wordChars.Length - 1)
                    {
                        // Hack: return the position of the second character for part 2
                        matches.Add((x + offset.x, y + offset.y));
                    }
                };
            }
        }
    }

    return matches.ToArray();
}

static (int x, int y) GetOffsetForDirection(Direction direction) => direction switch
{
    Direction.Up => (0, -1),
    Direction.Down => (0, 1),
    Direction.Left => (-1, 0),
    Direction.Right => (1, 0),
    Direction.DiagonalUpLeft => (-1, -1),
    Direction.DiagonalUpRight => (1, -1),
    Direction.DiagonalDownLeft => (-1, 1),
    Direction.DiagonalDownRight => (1, 1),
    _ => (0, 0)
};

enum Direction
{
    Up,
    Down,
    Left,
    Right,
    DiagonalUpLeft,
    DiagonalUpRight,
    DiagonalDownLeft,
    DiagonalDownRight
}

static class ArrayExtensions
{
    public static char GetValueOrDefault(this char[][] array, int x, int y) =>
        y >= 0 && y < array.Length && x >= 0 && x < array[0].Length ? array[y][x] : '\0';
}
