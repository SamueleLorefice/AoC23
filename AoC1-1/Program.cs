string[] input = File.ReadAllLines("input1.txt");
int[] numbers = new int[input.Length];
Func<string, char?> FindFirstNumeral = s => s.FirstOrDefault(c => char.IsDigit(c));
for (int i = 0; i < input.Length; i++) {
    var digits = new char[2];
    digits[0] = FindFirstNumeral(input[i]).Value;
    digits[1] = FindFirstNumeral(new String(input[i].Reverse().ToArray())).Value;
    numbers[i] = Int32.Parse(digits);
}
Console.WriteLine(numbers.Sum());
return numbers.Sum();