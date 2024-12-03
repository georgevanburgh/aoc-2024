using System.Text.RegularExpressions;

var input = File.ReadAllText("day03/input.txt");
var regex = new Regex(@"
    (?<disable>don't\(\)) |
    (?<enable>do\(\)) |
    (?<mul>mul\(
        (?<first>\d+),
        (?<second>\d+)
    \))
", RegexOptions.IgnorePatternWhitespace | RegexOptions.Multiline);

var matches = regex.Matches(input);

// Skip enabled and disabled matches for part 1
var operands = matches.Where(match => match.Groups["mul"].Success).Select(ParseMul);
var result1 = SumOperands(operands);
Console.WriteLine($"Part 1: {result1}");

var enabled = true;
var enabledOperands = new List<(int first, int second)>();
foreach (Match match in matches)
{
    if (match.Groups["disable"].Success)
    {
        enabled = false;
    }
    else if (match.Groups["enable"].Success)
    {
        enabled = true;
    }
    else if (match.Groups["mul"].Success && enabled)
    {
        enabledOperands.Add(ParseMul(match));
    }
}

var result2 = SumOperands(enabledOperands);
Console.WriteLine($"Part 2: {result2}");

static (int first, int second) ParseMul(Match match) => (
        int.Parse(match.Groups["first"].Value),
        int.Parse(match.Groups["second"].Value)
    );

static int SumOperands(IEnumerable<(int first, int second)> operands) =>
    operands.Sum(o => o.first * o.second);