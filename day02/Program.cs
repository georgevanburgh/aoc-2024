using MoreLinq;

var lines = File.ReadAllLines("day02/input.txt").Select(line => line.Split().Select(int.Parse).ToArray()).ToArray();

var result1 = lines.Count(IsSafe);
Console.WriteLine($"Part 1: {result1}");

var result2 = lines.Count(IsSafeWithDamper);
Console.WriteLine($"Part 2: {result2}");

static bool IsSafe(int[] input)
{
    var deltas = input.Pairwise((a, b) => b - a).ToArray();

    return deltas.All(delta => delta is > 0 and < 4) || deltas.All(delta => delta is < 0 and > -4);
}

static bool IsSafeWithDamper(int[] input)
{
    var skipPermutations = Enumerable.Range(0, input.Length).Select(i => input.Take(i).Concat(input.Skip(i + 1)).ToArray()).ToArray();
    return IsSafe(input) || skipPermutations.Any(IsSafe);
}