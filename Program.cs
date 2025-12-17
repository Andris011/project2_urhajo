namespace project2;

class Program
{
    const string S = "| |"; // space
    const string P = "|O|"; // player
    const string E = "|V|"; // enemy
    const string F = "|";
    const string B = "|^|";

    static List<string> ures_sor = new List<string> { F, S, S, S, S, S, S, S, S, S, F };

    static string[,] map =
    {
        {F,S,S,S,S,S,S,S,S,S,F},
        {F,S,S,S,S,S,S,S,S,S,F},
        {F,S,S,S,S,S,S,S,S,S,F},
        {F,S,S,S,S,S,S,S,S,S,F},
        {F,S,S,S,S,S,S,S,S,S,F},
        {F,S,S,S,S,S,S,S,S,S,F},
        {F,S,S,S,S,S,S,S,S,S,F},
        {F,S,S,S,S,S,S,S,S,S,F},
        {F,S,S,S,S,P,S,S,S,S,F}
    };

    static Random r = new Random();

    static int enemySzam = 0;

    static void AddEnemy()
    {

        enemySzam = 6;

        List<int> hol = new List<int>();

        while (hol.Count < enemySzam)
        {
            int pos = (r.Next(1, 10));

            if (!hol.Contains(pos))
            {
                hol.Add(pos);
            }

        }

        foreach (var elem in hol)
        {
            map[0, elem] = E;
        }
    }

    static void Kiiras()
    {

        string ki = "\x1B[H \n";

        for (int i = 0; i < map.GetLength(0); i++)
        {
            for (int j = 0; j < map.GetLength(1); j++)
            {
                ki += ($"{map[i, j]}");
            }

            ki += Environment.NewLine;
        }

        Console.WriteLine(ki);
    }


    static void Mozgas()
    {
        Console.WriteLine("Üdvözöllek a játékban! Nyomj egy gombot a folytatáshoz!");

        ConsoleKeyInfo pressed_key = Console.ReadKey(true);

        Console.Clear();

        AddEnemy();

        bool kilepes = false;

        int mozgasCount = 0;
        int lovesCount = 0;


        int player_x = 0, player_y = 0;

        for (int i = 0; i < map.GetLength(0); i++)
        {
            for (int j = 0; j < map.GetLength(1); j++)
            {

                if (map[i, j] == P)
                {
                    player_y = i;
                    player_x = j;
                }
                // Console.Write($"{map[i, j]}");
            }
            // Console.WriteLine();
        }

        do
        {

            if (Console.KeyAvailable)
            {

                pressed_key = Console.ReadKey(true);


                if (pressed_key.Key == ConsoleKey.Escape) kilepes = true;

                if (pressed_key.Key == ConsoleKey.RightArrow)
                {
                    map[player_y, player_x] = S;

                    if (map[player_y, player_x + 1] != F)
                    {
                        player_x++;
                        map[player_y, player_x] = P;
                    }
                    else
                    {
                        player_x = 1;
                        map[player_y, player_x] = P;
                    }
                }

                if (pressed_key.Key == ConsoleKey.LeftArrow)
                {
                    map[player_y, player_x] = S;

                    if (map[player_y, player_x - 1] != F)
                    {
                        player_x--;
                        map[player_y, player_x] = P;
                    }
                    else
                    {
                        player_x = 9;
                        map[player_y, player_x] = P;
                    }
                }

                if (pressed_key.Key == ConsoleKey.Spacebar)
                {
                    if (map[player_y - 1, player_x] == E)
                    {
                        enemySzam--;
                        map[player_y - 1, player_x] = S;
                    }
                    else
                    {
                        map[player_y - 1, player_x] = B;
                    }


                    

                }
            }

            lovesCount++;

            if (lovesCount >= 12)
            {
                for (int i = 0; i < map.GetLength(0); i++)
                {
                    for (int j = 0; j < map.GetLength(1); j++)
                    {
                        if (map[i, j] == B)
                        {
                            if (i == 0)
                            {
                                map[i, j] = S;
                            }
                            else if (map[i - 1, j] != E)
                            {
                                map[i, j] = S;
                                map[i - 1, j] = B;
                            }
                            else if (map[i - 1, j] == E)
                            {
                                map[i, j] = S;
                                map[i - 1, j] = S;
                                enemySzam--;
                            }
                        }
                    }
                }

                lovesCount = 0;
            }

            mozgasCount++;

            if (mozgasCount >= 24)
            {
                for (int i = map.GetLength(0) - 1; i != 0; i--)
                {
                    for (int j = map.GetLength(1) - 1; j >= 0; j--)
                    {

                        if (map[i - 1, j] == E)
                        {
                            if (map[i, j] != B)
                            {
                                map[i, j] = map[i - 1, j];
                                map[i - 1, j] = S;
                            }
                            else
                            {
                                map[i, j] = S;
                                map[i - 1, j] = S;

                                enemySzam--;
                            }
                        }
                    }
                }

                mozgasCount = 0;

                int talal = 0;

                for (int k = map.GetLength(1) - 1; k >= 0; k--)
                {

                    map[0, k] = ures_sor[k];

                    if (map[8, k] == E && talal == 0)
                    {
                        Console.WriteLine("Vesztettél, a Föld elesett!");
                        kilepes = true;
                        talal++;
                    }
                }
            }

            if (enemySzam <= 0)
            {
                Kiiras();

                Console.WriteLine("Nyertél!");

                Console.WriteLine("Szeretnél megint játszani? Nyomj ENTERT ha igen");

                pressed_key = Console.ReadKey(true);

                if (pressed_key.Key == ConsoleKey.Enter)
                {
                    AddEnemy();
                    Console.Clear();
                }
                else
                {
                    Console.Clear();
                    kilepes = true;
                }
            }


            Kiiras();

            Thread.Sleep(1000 / 24);

        } while (!kilepes);
        
        Kiiras();
        Console.WriteLine();
        Console.WriteLine("Viszlát!");
        Console.WriteLine();
        Console.WriteLine("Nyomjon egy gombot a kilépéshez!");
        Console.ReadKey(true);
    }
    
    static void Main(string[] args)
    {
        Mozgas();
    }
}