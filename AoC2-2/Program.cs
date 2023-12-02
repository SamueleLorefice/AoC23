using System.Text.RegularExpressions;
string[] input = File.ReadAllLines("input.txt");
int[] powers = new int[input.Length+1];

Regex gameParse = new Regex("Game (?<id>[0-9]+): (?:(?:(?<count>[0-9]+) (?<color>red|green|blue))(?:(?<pass>,)|(?<end>;))* *)+", RegexOptions.Compiled|RegexOptions.IgnoreCase);
Parallel.ForEach(input, (line, _, index) => {
    var match = gameParse.Match(line);
    var id = int.Parse(match.Groups["id"].Value);
    var counts = match.Groups["count"].Captures;
    var colors = match.Groups["color"].Captures;
    var red = 0;
    var green = 0;
    var blue = 0;
    for (int i = 0; i < counts.Count; i++) {
        var newNum = int.Parse(counts[i].Value);
        switch (colors[i].Value) {
            case "red":
                red = newNum > red ? newNum : red;  
                break;
            case "green":
                green = newNum > green ? newNum : green;
                break;
            case "blue":
                blue = newNum > blue ? newNum : blue;
                break;
        }
    }
    powers[id] = red * green * blue;
});
return powers.Sum();