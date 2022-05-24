using System.Diagnostics;

class Program
{

    static void Main(string[] args)
    {
        int[] data = { 0, 0 }; // index 0 is current cpu load, 1 is the last cpu load.
        int lastPosY = 0;
        int rowIndex, columIndex = 5;

        drawAxies();

        while (true)
        {
            data[0] = getCpuLoadPercentage();

            // Flip the load percentage and then sets the cursor position to it.
            // EX. 7 => 3
            rowIndex = (((Math.Abs(data[0] / 10)) - 10) * -1) - 1;
            Console.SetCursorPosition(columIndex, rowIndex);
            Console.WriteLine(charReturn(rowIndex, lastPosY));

            lastPosY = rowIndex;

            Console.SetCursorPosition(73, 10);
            Console.WriteLine(data[0] + "% load ");
            Console.SetCursorPosition(rowIndex, lastPosY);

            if (columIndex == 65) // Loops the progress
            {
                Console.Clear();
                drawAxies();
                columIndex = 5;
            }


            columIndex++;
        }

        Console.ReadLine();
    }

    // writes the axies.
    static void drawAxies()
    {
        // 100 |
        //     |
        // 80  |
        //     |
        // 60  |
        //     |
        // 40  |
        //     |
        // 20  |
        //     |
        // 0   |------------------------------------------------------------> Time

        int procentIndex = 100;
        for (int i = 0; i <= 10; i++)
        {
            if (i % 2 == 0)
            {
                Console.Write(procentIndex);

                if (procentIndex == 100)
                {
                    Console.Write(" |");
                }
                else if (procentIndex < 100 && procentIndex > 10)
                {
                    Console.Write("  |");
                }
                else
                {
                    Console.Write("   |");
                }

                procentIndex -= 20;
                Console.WriteLine();

            }
            else
            {
                Console.WriteLine("    |");
            }
            if (i == 10)
            {
                Console.SetCursorPosition(5, 10);
                Console.Write("------------------------------------------------------------> Time");
            }
        }
    }


    //Determin which char to use
    static char charReturn(int pos1, int pos2)
    {
        char[] chars = { '/', '_', '\\', '|' };

        if (pos1 < pos2)
        {
            return chars[0];
        }
        else if (pos1 == pos2)
        {
            return chars[1];
        }
        else if (pos1 > pos2)
        {
            return chars[2];
        }
        return 'x';
    }
    static int getCpuLoadPercentage()
    {
        // Gets the cpu load and percentage and execute the command in the cmd
        string command = "wmic cpu get LoadPercentage /value";

        Process cmdProcess = new Process();
        cmdProcess.StartInfo.FileName = "cmd.exe";
        cmdProcess.StartInfo.Arguments = "/C " + command;
        cmdProcess.StartInfo.RedirectStandardOutput = true;
        cmdProcess.Start();

        string output = cmdProcess.StandardOutput.ReadToEnd();
        cmdProcess.WaitForExit();

        // Creats a string and fill it with the numbers from the output string
        string outputstring = "";
        foreach (var number in output)
        {
            switch (number)
            {
                case '0':
                    outputstring += "0";
                    break;
                case '1':
                    outputstring += "1";
                    break;
                case '2':
                    outputstring += "2";
                    break;
                case '3':
                    outputstring += "3";
                    break;
                case '4':
                    outputstring += "4";
                    break;
                case '5':
                    outputstring += "5";
                    break;
                case '6':
                    outputstring += "6";
                    break;
                case '7':
                    outputstring += "7";
                    break;
                case '8':
                    outputstring += "8";
                    break;
                case '9':
                    outputstring += "9";
                    break;
                default:
                    break;
            }
        }

        return Convert.ToInt32(outputstring);
    }
}