using System.Runtime.CompilerServices;

string[] input = File.ReadAllLines("input.txt");
char[,] schematicMap = new char[input.Length, input[0].Length];
var lenght = input[0].Length;
List<Number> numbers = new List<Number>();
List<Star> stars = new List<Star>();

Parallel.For(0, input.Length * lenght, i => {
    schematicMap[i / lenght, i % lenght] = input[i / lenght][i % lenght];
});

[MethodImpl(MethodImplOptions.AggressiveInlining)]
bool OutOfBounds(int x, int y) => x < 0 || y < 0 || x >= schematicMap.GetLength(0) || y >= schematicMap.GetLength(1);

bool FindStarAround(int x, int y, out int sx, out int sy) {
    bool result = false;
    for (int i = x - 1; i <= x + 1; i++) {
        for (int j = y - 1; j <= y + 1; j++) {
            if (OutOfBounds(i, j)) continue;
            char c = schematicMap[i, j];
            if (c == '*') {
                result = true;
                sx = i;
                sy = j;
                return true;
            }
        }
        if (result) break;
    }
    sx = -1;
    sy = -1;
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
        if (schematicMap[x, y] == '*') {
            stars.Add(new Star{x= x, y = y});
        }
        if (!char.IsDigit(schematicMap[x, y])) {
            inNumber = false;
            continue;
        }
        if (char.IsDigit(schematicMap[x, y])) {
            if(inNumber) continue;
            inNumber = true;
            numbers.Add(new Number{ value = ScanNumber(x, y), x = x, y = y} );
        }
    }
}

int ratio = 0;
foreach (var star in stars) {
    var nums = new List<Number>();
    foreach (var num in numbers) {
        for (int i = num.y; i < num.y + num.value.ToString().Length; i++) {
            if (FindStarAround(num.x, i, out int sx, out int sy))
                if (sx == star.x && sy == star.y) {
                    nums.Add(new Number { x = sx, y = sy, value = num.value, valid = true });
                    break;
                }
        }
    }
    if(nums.Count < 2 || nums.Count > 2) continue;
    ratio += nums[0].value * nums[1].value;
}

return ratio;

class Number {
    public int x;
    public int y;
    public int value;
    public bool valid;
}

class Star {
    public int x;
    public int y;
}