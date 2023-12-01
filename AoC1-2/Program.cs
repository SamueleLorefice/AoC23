using AoC1_2;
using System.Text.RegularExpressions;

string[] input = File.ReadAllLines("input1.txt");
int[] numbers = new int[input.Length];
Func<string, int?> FindNumber = s => {
    var pattern = String.Join("|", Enum.GetNames(typeof(DigitWord)));
    var match = Regex.Match(s, @"^(" + pattern + @")", RegexOptions.IgnoreCase);
    return match.Success ? (int)Enum.Parse<DigitWord>(match.Value, true) : null;
};
Func<string, List<int>> NumberifyString= s => {
    var numbers = new List<int>();
    for (int i = 0; i < s.Length; i++) {
        if(Char.IsDigit(s[i])){
            numbers.Add(Int32.Parse(s[i].ToString()));
            continue;
        }
        if (FindNumber(s.Substring(i)) != null) {
            numbers.Add((int)FindNumber(s.Substring(i)));
        }
    }
    return numbers;
};

for (int i = 0; i < input.Length; i++) {
    var numbersInString = NumberifyString(input[i]);
    numbers[i] = numbersInString.First()*10+ numbersInString.Last();
}
return numbers.Sum();