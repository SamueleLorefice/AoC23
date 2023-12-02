using System.Text.RegularExpressions;
string[] input = File.ReadAllLines("input.txt");
bool[] possible = new bool[input.Length+1];
possible[0] = false;
var maxRed = 12;
var maxGreen = 13;
var maxBlue = 14;
Regex gameParse = new Regex("Game (?<id>[0-9]+): (?:(?:(?<count>[0-9]+) (?<color>red|green|blue))(?:(?<pass>,)|(?<end>;))* *)+", RegexOptions.Compiled|RegexOptions.IgnoreCase);
Parallel.ForEach(input, (line, _, index) => {
    var match = gameParse.Match(line);
    var id = int.Parse(match.Groups["id"].Value);
    var counts = match.Groups["count"].Captures;
    var colors = match.Groups["color"].Captures;
    var ends = match.Groups["end"].Captures;
    var red = 0;
    var green = 0;
    var blue = 0;
    Action reset = () => {
        red = 0;
        green = 0;
        blue = 0;
    };
    for (int i = 0; i < counts.Count; i++) {
        if (ends.Any(capture => capture.Index == counts[i].Index - 2)) {
            possible[id] = red <= maxRed && green <= maxGreen && blue <= maxBlue;
            if (!possible[id]) break;
            reset();
        }
        switch (colors[i].Value) {
            case "red":
                red += int.Parse(counts[i].Value);
                break;
            case "green":
                green += int.Parse(counts[i].Value);
                break;
            case "blue":
                blue += int.Parse(counts[i].Value);
                break;
        }
    }
    if (possible[id]) {
        possible[id] = red <= maxRed && green <= maxGreen && blue <= maxBlue;
    }
});
return possible.Select((value, i) => new { value, index = i }).Where(x => x.value).Sum(x => x.index);