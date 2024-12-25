using System.Diagnostics;

Run(@"input.txt");

void Run(string inputfile)
{
    var S = File.ReadAllLines(inputfile).ToList();
    long answer1 = 0;
    int i = 0;
    var locks = new List<int[]>();
    var keys = new List<int[]>();
    while (i < S.Count)
    {
        var thing = new int[5];
        var s = S[i];
        bool isKey = (s == ".....");
        while (i < S.Count && S[i] != "")
        {
            for (int j = 0; j < S[i].Length; j++)
            {
                if (S[i][j] == '#') thing[j]++;
            }
            i++;
        }
        if (isKey) { keys.Add(thing); }
        else { locks.Add(thing); }
        i++;
    }

    foreach (var key in keys)
    {
        foreach (var lck in locks)
        {
            bool fit = true;
            for (int j = 0; j < lck.Length; j++)
            {
                if (lck[j] + key[j] > 7) fit = false;
            }
            if (fit) answer1++;
        }
    }
}