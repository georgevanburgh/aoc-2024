// Parsing
var input = File.ReadLines("day01/input.txt");

var parsed = input.Select(x => x.Split(' ').Select(int.Parse));

var left = parsed.Select(x => x.First()).Order().ToArray();
var right = parsed.Select(x => x.Last()).Order().ToArray();

// Part 1
var result1 = (int) left.Zip(right).Sum(line => Math.Abs(line.First - line.Second));
Console.WriteLine(result1);

// Part 2
var counts = right.GroupBy(x => x).ToDictionary(x => x.Key, x => x.Count());

var result2 = left.Sum(x => x * counts.GetValueOrDefault(x, 0));
Console.WriteLine(result2);
