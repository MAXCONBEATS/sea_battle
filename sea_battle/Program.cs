
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Linq;
using System.IO;
class GameField
{
    string[,] realarr = new string[5, 5];
    string[,] visiblearr = new string[5, 5];
    int shotCounter = 0;

    public string RandomChar()
    {
        string[] chars = [" ", "$"];
        Random rnd = new();
        return chars[rnd.Next(chars.Length)];
    }
    public void FillGameField()
    {
        int shipCounter = 0;
        if (realarr.Cast<string>().Any(x => x == null) == true)
        {
            for (int i = 0; i < realarr.GetLength(0); i++)
            {
                for (int j = 0; j < realarr.GetLength(1); j++)
                {
                    string v = RandomChar();
                    if (v == "$")
                    {
                        shipCounter++;
                    }
                    if (shipCounter > 3)
                    {
                        realarr[i, j] = " ";
                    }
                    else { realarr[i, j] = v; }
                    
                }
            }

            for (int i = 0; i < visiblearr.GetLength(0); i++)
            {
                for (int j = 0; j < visiblearr.GetLength(1); j++)
                {
                    visiblearr[i, j] = "[ _ ]";
                    Console.Write($"{visiblearr[i, j]} ");
                }
                Console.WriteLine();
            }
        }
        else
        {
            for (int i = 0; i < visiblearr.GetLength(0); i++)
            {
                for (int j = 0; j < visiblearr.GetLength(1); j++)
                {
                    Console.Write($"{visiblearr[i, j]} ");
                }
                Console.WriteLine();
            }
        }
        

    }
    public void Shot(int i, int j)
    {
        //if (i+1 > realarr.GetLength(0) | j+1 > realarr.GetLength(1))
        //{
        //    Console.WriteLine("Выстрел вне поля!");
        //    FillGameField();
        //}
        i -= 1; j -= 1;
        if (realarr[i, j] == "$")
        {
            if (visiblearr[i, j] == "[ X ]")
            {
                visiblearr[i, j] = "[ X ]";
                FillGameField();
            }
            else
            {
                visiblearr[i, j] = "[ X ]";
                shotCounter++;
                FillGameField();
            }
            
        }
        else
        {
            visiblearr[i, j] = "[ * ]";
            FillGameField();
        }
    }
    public int ReturnResult()
    {
        return shotCounter;
    }
    public void SaveResult() { File.AppendAllText("C:\\Users\\maxco\\source\\repos\\sea_battle\\sea_battle\\score.txt", $"\n {DateTime.Now} - {shotCounter}");  }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("Морской бой");
        GameField gameField = new GameField();
        gameField.FillGameField();
        int c = 0;
        while (true)
        {
            Console.Write("Пишите координаты выстрела: ");
            int[] numbers = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
            int i = numbers[0]; int j = numbers[1];

            gameField.Shot(i, j);
            c ++;

            if (c > 10) { 
                Console.WriteLine($"Вы проиграли! Число попаданий: {gameField.ReturnResult()}") ;
                gameField.SaveResult();
                break; 
                
            }
            else if (gameField.ReturnResult() == 3)
            {
                Console.WriteLine("Победа!");
                gameField.SaveResult();
                break;

            }

        }

    }
}
