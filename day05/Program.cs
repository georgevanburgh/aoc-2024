using MoreLinq;

var lines = File.ReadAllLines("day05/input.txt");

var orderingRules = lines
    .TakeWhile(l => !string.IsNullOrWhiteSpace(l))
    .Select(l => l.Split('|', StringSplitOptions.TrimEntries))
    .Select(parts => (int.Parse(parts[0]), int.Parse(parts[1])))
    .ToHashSet();

var updates = lines
    .SkipWhile(l => !string.IsNullOrWhiteSpace(l))
    .Skip(1)
    .Select(l => l
        .Split(',', StringSplitOptions.TrimEntries)
        .Select(int.Parse)
        .ToArray())
    .ToArray();

var validUpdates = updates.Where(IsValidSequence).ToArray();
var invalidUpdates = updates.Where(u => !IsValidSequence(u)).ToArray();

var result1 = validUpdates.Select(GetMiddleElementInArray).Sum();
Console.WriteLine($"Part 1: {result1}");

var fixedUpdates = invalidUpdates.Select(FixOrdering);
Console.WriteLine($"Part 2: {fixedUpdates.Select(GetMiddleElementInArray).Sum()}");

int[] FixOrdering(int[] sequence)
{
    while (!IsValidSequence(sequence))
    {
        var invalidPairs = sequence
            .Pairwise((a, b) => (a, b))
            .Where(pair => !IsValidPair(pair))
            .ToArray();

        foreach (var (a, b) in invalidPairs)
        {
            // Swap (a, b) if (b, a) is a valid pair
            if (IsValidPair((b, a)))
            {
                var aIndex = Array.IndexOf(sequence, a);
                var bIndex = Array.IndexOf(sequence, b);
                sequence[aIndex] = b;
                sequence[bIndex] = a;
            }
        }
    }

    return sequence;
}

bool IsValidSequence(int[] update)
{
    return update
        .Pairwise((a, b) => (a, b))
        .All(IsValidPair);
}

bool IsValidPair((int a, int b) pair) =>
    orderingRules.TryGetValue((pair.a, pair.b), out _);

// We know that arrays are always odd length
int GetMiddleElementInArray(int[] arr) =>
    arr[arr.Length / 2];

