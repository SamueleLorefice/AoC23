using System.Text.RegularExpressions;

string[] input = File.ReadAllLines("input.txt");
Regex cardParse = new Regex(@"(?:Card) +(?<cardN>[0-9]+): *(?:(?<winNum>[0-9]+) +)+\| *(?:(?<playedNum>[0-9]+) *)+",
    RegexOptions.Compiled | RegexOptions.IgnoreCase);

List<Card> cards = new List<Card>();

foreach (var line in input) {
    var matches = cardParse.Match(line);
    cards.Add(new Card(
        int.Parse(matches.Groups["cardN"].Captures[0].Value),
        matches.Groups["winNum"].Captures.Select(c => int.Parse(c.Value)).ToArray(),
        matches.Groups["playedNum"].Captures.Select(c => int.Parse(c.Value)).ToArray()
    ));
}

Parallel.ForEach(cards, card => { card.score = (int)Math.Pow(2,card.winNums.Intersect(card.playedNums).Count() -1); });

return cards.Where(c => c.score > 0).Sum(c => c.score);

record Card(int cardN, int[] winNums, int[] playedNums, int score=-1) {
    public int score { get; set; } = score;
}