using System;
using System.Linq;
namespace secretChat
{
    class Program
    {
        static void Main(string[] args)
        {
            string message = Console.ReadLine();
            string input = string.Empty;
            while((input = Console.ReadLine()) != "Reveal")
            {
                string[] cmdArgs = input.Split(":|:");
                string command = cmdArgs[0];
                if(command == "InsertSpace")
                {
                    int index = int.Parse(cmdArgs[1]);
                    message = message.Insert(index, " ");
                    Console.WriteLine(message);
                }
                else if (command == "Reverse")
                {
                    string substringCheck = cmdArgs[1];
                  
                    if (message.Contains(substringCheck))
                    {
                        int indexForCut = message.IndexOf(substringCheck);
                        int lengthForCut = substringCheck.Length;
                        message = message.Remove(indexForCut, lengthForCut);
                        string tailForAdd = string.Empty;
                        for (int i = lengthForCut-1; i >= 0 ; i--)
                        {
                            tailForAdd += substringCheck[i];
                        }
                        message = message + tailForAdd;
                        Console.WriteLine(message);
                    }
                    else
                    {
                        Console.WriteLine("error");
                    }
                }
                else if(command == "ChangeAll")
                {
                    string searchedSubstring = cmdArgs[1];
                    string newSubstring = cmdArgs[2];
                    message = message.Replace(searchedSubstring, newSubstring);
                    Console.WriteLine(message);
                }
            }
            Console.WriteLine($"You have a new text message: {message}");
        }
    }
}
