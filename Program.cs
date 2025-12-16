namespace project2;

class Program
{
    const string S = "| |"; // space
    const string P = "|O|"; // player
    const string E = "|V|"; // enemy
    const string F = "|";

    static List<string> ures_sor = new List<string> {F,S,S,S,S,S,S,S,S,S,F};
    
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

    static void AddEnemy()
    {
        HashSet<int> hol = new HashSet<int>();
        
        for (int i = 0; i < 6; i++)
        {
            
            hol.Add(r.Next(1, 10));
        }

        foreach (var elem in hol)
        {
            map[0, elem] = E;
        }
    }

    static void Kiiras()
    {
        Console.Clear();
        for (int i = 0; i < map.GetLength(0); i++)
        {
            for (int j = 0; j < map.GetLength(1); j++)
            {
                Console.Write($"{map[i, j]}");
            }

            Console.WriteLine();
        }
    }



    static void Mozgas()
    {
        Console.WriteLine("Üdvözöllek a játékban! Nyomj egy gombot a folytatáshoz!");
        
        ConsoleKeyInfo pressed_key = Console.ReadKey(true);
        
        
        bool kilepes = false;
        
        do
        {
            Kiiras();
            
        int player_x = 0, player_y = 0;
        
        pressed_key = Console.ReadKey(true);

        Console.Clear();
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

            if (pressed_key.Key == ConsoleKey.RightArrow)
            {
                if (map[player_y, player_x + 1] != F)
                {
                    map[player_y, player_x] = S;
                    map[player_y, player_x++] = P;
                }
                else
                {
                    map[player_y, player_x] = S;
                    player_x = 0;
                    map[player_y, player_x] = P;
                }
            }
            
            if (pressed_key.Key == ConsoleKey.LeftArrow)
            {
                if (map[player_y, player_x - 1] != F)
                {
                    map[player_y, player_x] = S;
                    map[player_y, player_x--] = P;
                }
                else
                {
                    map[player_y, player_x] = S;
                    player_x = 9;
                    map[player_y, player_x] = P;
                }
            }
            
            for (int i = map.GetLength(0) - 1; i != 0; i--)
            {
                for (int j = map.GetLength(1) - 1; j >= 0; j--)
                {
                    map[i, j] = map[i - 1, j];
                }
            }
            
            for (int k = map.GetLength(1) - 1; k >= 0; k--)
            {
                map[0, k] = ures_sor[k];

                if (map[8, k] == E)
                {
                    kilepes = true;
                }
            }
            
            map[player_y, player_x] = P;


            if (pressed_key.Key == ConsoleKey.Escape) kilepes = true;

        } while (!kilepes);

        Kiiras();
        Console.WriteLine();
        Console.WriteLine("Vesztettél!");
        Console.WriteLine();
        Console.WriteLine("Nyomjon egy gombot a kilépéshez!");
        Console.ReadKey(true);
    }
    
    
    
    
    static void Main(string[] args)
    {
        AddEnemy();
        Mozgas();
    }
}