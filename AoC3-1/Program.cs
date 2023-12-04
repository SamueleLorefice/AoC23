using System.Runtime.CompilerServices;

string[] input = File.ReadAllLines("input.txt");
char[,] schematicMap = new char[input.Length, input[0].Length];
var lenght = input[0].Length;
List<Number> numbers = new List<Number>();

Parallel.For(0, input.Length * lenght, i => {
    schematicMap[i / lenght, i % lenght] = input[i / lenght][i % lenght];
});

[MethodImpl(MethodImplOptions.AggressiveInlining)]
bool OutOfBounds(int x, int y) => x < 0 || y < 0 || x >= schematicMap.GetLength(0) || y >= schematicMap.GetLength(1);

bool CheckSurroundingPlaces(int x, int y) {
    bool result = false;
    for (int i = x - 1; i <= x + 1; i++) {
        for (int j = y - 1; j <= y + 1; j++) {
            if (OutOfBounds(i, j)) continue;
            char c = schematicMap[i, j];
            if (c != '.' && !char.IsDigit(c)) {
                result = true;
                break;
            }
        }
        if (result) break;
    }
    return result;
}

int ScanNumber(int x, int y) {
    int result = 0;
    for (int i = y; i < schematicMap.GetLength(1); i++) {
        if (char.IsDigit(schematicMap[x, i])) {
            result *= 10;
            result += int.Parse(schematicMap[x, i].ToString());
        } else {
            break;            
        }
    }
    return result;
}

for (int x = 0; x < schematicMap.GetLength(0); x++) {
    bool inNumber = false;
    for (int y = 0; y < schematicMap.GetLength(1); y++) {
        if (!char.IsDigit(schematicMap[x, y])) {
            inNumber = false;
            continue;
        }
        if (char.IsDigit(schematicMap[x, y])) {
            if(inNumber) continue;
            inNumber = true;
            numbers.Add(new Number(){ value = ScanNumber(x, y), x = x, y = y} );
        }
    }
}

foreach (var number in numbers) {
    bool isValid = false;

    for (int y = number.y; y <= number.y + number.value.ToString().Length -1; y++) {
        if (isValid) break;
        isValid = CheckSurroundingPlaces(number.x, y);
    }
    number.valid = isValid;
}

return numbers.Where(n => n.valid).Sum(n => n.value);

class Number {
    public int x;
    public int y;
    public int value;
    public bool valid;
}