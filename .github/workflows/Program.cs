using System;
using System.IO;
using System.Globalization;

namespace SimilarWebAssighnment
{
    class Program
    {
        static void Main(string[] args)
        {
            string userInput;
            string userChoice;
            bool toContinue = true;
            Sessionizing sessionizing = new Sessionizing();

            Console.WriteLine("Hi, you should provide an input for the Sessionizing operation.");

            while (toContinue)
            {
                try
                {
                    Console.WriteLine("Do you prefer to enter a directory which all the files in it will be taken from, or do you prefer to enter specific path?");
                    Console.WriteLine("(Enter '1' for directory or enter '2' for specific file path): ");
                    userChoice = Console.ReadLine();

                    switch (userChoice)
                    {
                        case "1":
                            Console.WriteLine("Please enter a valid directory which contains your .csv files");
                            userInput = Console.ReadLine();
                            sessionizing.LoadInputFilesByDirectory(userInput);
                            toContinue = false;
                            break;
                        case "2":
                            while (toContinue)
                            {
                                Console.WriteLine("Please enter a valid path of your .csv files:");
                                userInput = Console.ReadLine();
                                sessionizing.LoadInputFileByFilePath(userInput);
                                Console.WriteLine("Do you want to input another file ('no' - to stop, otherwise - to continue): ");
                                userInput = Console.ReadLine();

                                if (userInput == "no")
                                    toContinue = false;
                            }
                            break;
                        default:
                            Console.WriteLine("Wrong input...please try again.\n");
                            break;
                    }
                }
                catch (DirectoryNotFoundException)
                {
                    Console.WriteLine("Entered Directory is not valid or not exists!\n");
                    continue;
                }
                catch (FileNotFoundException)
                {
                    Console.WriteLine("Wanted files havn't been found!\n");
                    continue;
                }
                catch (FormatException e)
                {
                    Console.WriteLine("There is a format problem: " + e.Message + "\n");
                    continue;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception have beed occurred: message = " + e.Message + "\n");
                    continue;
                }
            }

            sessionizing.ManageSitesAfterInputReading();
            toContinue = true;

            while (toContinue)
            {
                Console.WriteLine("Which information do you want on your input files? Please enter:");
                Console.WriteLine("'1' - for number of Sessions + median of Sessions length (in seconds) for the given site");
                Console.WriteLine("'2' - for number of unique visited sites by the given visitor ");
                Console.WriteLine("'q' - to quit");
                userChoice = Console.ReadLine();

                switch (userChoice)
                {
                    case "1":
                        Console.WriteLine("Please enter the site url:");
                        userInput = Console.ReadLine();
                        int numSession = sessionizing.NumSession(userInput);

                        if (numSession != -1)
                        {
                            double medianSessionLength = sessionizing.MedianSessionLength(userInput);
                            Console.WriteLine(string.Format("Num sessions for site {0} = {1}, median session length = {2}\n",
                                                            userInput, numSession.ToString(), medianSessionLength.ToString("F1", CultureInfo.InvariantCulture)));
                        }
                        else
                        {
                            Console.WriteLine("Input URL isn't exist...please try again.\n");
                        }
                        break;
                    case "2":
                        Console.WriteLine("Please enter visitor id: ");
                        userInput = Console.ReadLine();
                        int numUniqueVisitedSites = sessionizing.NumUniqueVisitedSites(userInput);

                        if (numUniqueVisitedSites != -1)
                        {
                            Console.WriteLine(string.Format("Num of unique sites for {0} = {1}\n",
                                                            userInput, numUniqueVisitedSites.ToString()));
                        }
                        else
                        {
                            Console.WriteLine("Input visitor id isn't exist...please try again.\n");
                        }
                        break;
                    case "q":
                        toContinue = false;
                        break;
                    default:
                        Console.WriteLine("Wrong input...please try again.\n");
                        break;
                }
            }
        }
    }
}
