using System;

namespace newconsole {

    public class Launcher {

        static CommandSettings commands;
        static Countries countries;

        static string input;
        static string prefix;

        public static void Main(string[] args) {

            commands = new CommandSettings();
            countries = new Countries();

            for (int i = 0; i < commands.commands.Length; i++)
            {
                DisplayInConsole(commands.commands[i]);
            }

            prefix = commands.prefix;
            getInput();
        }

        static void getInput()
        {
            input = Console.ReadLine();
            initCommand();
        }

        static void initCommand()
        {
            if (input.StartsWith(prefix + ""))
            {
                string command = input;
                if (command == prefix+"countries")
                {
                    for (int i = 0; i < countries.countries.Length; i++)
                    {
                        DisplayInConsole("[ID:" + (i+1) + "] " + countries.countries[i]);
                        System.Threading.Thread.Sleep(50);
                    }
                }
                ///////////////////////////////////////////////
                
                // FINDING SPECIFIC COUNTRY BY NAME OR ID

                ///////////////////////////////////////////////

                string[] words = command.Split();
                if (words[0].ToLower() == prefix + "get" && words.Length == 2)
                {
                    bool didFind = false;
                    for (int i = 0; i < countries.countries.Length; i++)
                    {
                        if (words[1] == (i + 1).ToString()) {
                            DisplayInConsole(countries.countries[i]);
                            DisplayInConsole(" ");
                            didFind = true;
                        }

                        else if (words[1].ToLower() == countries.countries[i].ToLower()) {
                            DisplayInConsole("COUNTRY ID: " + (i + 1).ToString());
                            DisplayInConsole(" ");
                            didFind = true;
                        }
                    }

                    if (!didFind)
                    {
                        if (words[1] != null)
                        {
                            DisplayInConsole("could not read the contents of '" + words[1] +  "'");
                        }
                    }
                }

                else if (words.Length < 2 && words[0] != null && words[0] == prefix+"getCountry") {
                    string error = "argument missing: " + prefix +"getCountry <ID> or " + prefix + "getCountry <COUNTRY NAME>";
                    DisplayInConsole(error);
                }

                else if (words.Length > 2) {
                    DisplayInConsole("too many arguments assigned");
                }

                ///////////////////////////////////////////////
                
                // GET COUNTRIES BY LETTER(S)

                ///////////////////////////////////////////////

                if (words[0].ToLower() == prefix+"listby")
                {
                    if (words[1] != null)
                    {
                        DisplayInConsole(" ");
                        for (int i = 0; i < countries.countries.Length; i++)
                        {
                            string countryFirstLetter = words[1].ToUpper();
                            string country = countries.countries[i].ToUpper();

                            if (country.StartsWith(countryFirstLetter))
                            {
                                DisplayInConsole("[ID:" + (i + 1) + "] " + countries.countries[i]);
                                System.Threading.Thread.Sleep(50);
                            }
                        }
                        DisplayInConsole(" ");
                    }
                }

                if (input.ToLower() == prefix+"cmds")
                {
                    for (int i = 0; i < commands.commands.Length; i++)
                    {
                        DisplayInConsole(commands.commands[i]);
                    }
                }

                getInput();
            }

            else
            {
                DisplayInConsole("invalid prefix");
                getInput();
            }
        }

        static void DisplayInConsole(string display)
        {
            Console.WriteLine(display);
        }
    } 

    class CommandSettings {
        public string prefix = "/";
        public string[] commands = {

            "/countries # lists all countries",
            "/get <ID> # lists country by its id in alphabetical order",
            "/get <COUNTRY_NAME> # lists countrys ID",
            "/listby <STARTING_LETTER> # displays countries that start with STARTING_LETTER",
            "/cmds # shows list of commands",

            " "};

    }
}
