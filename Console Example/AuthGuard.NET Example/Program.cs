using System;
using System.Windows;

namespace AuthGuard
{
    class Program
    {
        static void Main(string[] args)
        {
            //Detect if your application is running in a Virual Machine / Sandboxie...
            //Anti_Analysis.Init();
            //This connects your file to the AuthGuard.net API, and sends back your application settings and such
            Guard.Initialize("PROGRAMSECRET", "VERSION", "VARIABLESECRET");
            if (GuardSettings.Freemode)
            {
                //Usually when your application doesn't need a login and has freemode enabled you put the code here you want to do
                MessageBox.Show("Freemode is active, bypassing login!", GuardSettings.ProgramName, MessageBoxButton.OK, MessageBoxImage.Information);
            }
        home:
            PrintLogo();
            Console.WriteLine("[1] Register");
            Console.WriteLine("[2] Login");
            Console.WriteLine("[3] Extend Subscription");
            string option = Console.ReadLine();
            if (option == "1")
            {
            re:
                Console.Clear();
                PrintLogo();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("***************************************************");
                Console.WriteLine("Register:");
                Console.WriteLine("***************************************************");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine();
                Console.WriteLine("Username:");
                string username = Console.ReadLine();
                Console.WriteLine("Password:");
                string password = Console.ReadLine();
                Console.WriteLine("Email:");
                string email = Console.ReadLine();
                Console.WriteLine("License:");
                string license = Console.ReadLine();
                if (Guard.Register(username, password, email, license))
                {
                    MessageBox.Show("You have successfully registered!", GuardSettings.ProgramName, MessageBoxButton.OK, MessageBoxImage.Information);
                    // Do code of what you want after successful register here!
                    Console.Clear();
                    goto home;
                }
                else goto re; //Retry
            }
            else if (option == "2")
            {
            re:
                Console.Clear();
                PrintLogo();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("***************************************************");
                Console.WriteLine("Login:");
                Console.WriteLine("***************************************************");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Username:");
                string username = Console.ReadLine();
                Console.WriteLine("Password:");
                string password = Console.ReadLine();
                if (Guard.Login(username, password))
                {
                    MessageBox.Show("You have successfully logged in!", UserInfo.Username, MessageBoxButton.OK, MessageBoxImage.Information);
                    Console.Clear();
                    PrintLogo();
                    // Success login stuff goes here
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("***************************************************");
                    Console.WriteLine("All user information:");
                    Console.WriteLine("***************************************************");
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"Username -> {UserInfo.Username}");
                    Console.WriteLine($"Email -> {UserInfo.Email}");
                    Console.WriteLine($"HWID -> {UserInfo.HWID}");
                    Console.WriteLine($"User Level -> {UserInfo.Level}");
                    Console.WriteLine($"User IP -> {UserInfo.IP}");
                    Console.WriteLine($"Expiry -> {UserInfo.Expires}");
                    //Put variable name here with the name of the variable in your panel - https://i.imgur.com/W7yl3MH.png
                    Console.WriteLine($"Variable -> {Guard.Var("VARIABLENAME")}");
                }
                else goto re; //Retry
            }
            else if (option == "3")
            {
            re:
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("***************************************************");
                Console.WriteLine("Extend Subscription:");
                Console.WriteLine("***************************************************");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine();
                Console.WriteLine("Username:");
                string username = Console.ReadLine();
                Console.WriteLine("Password:");
                string password = Console.ReadLine();
                Console.WriteLine("License:");
                string token = Console.ReadLine();
                if (Guard.RedeemToken(username, password, token))
                {
                    MessageBox.Show("You have successfully extended your subscription!", GuardSettings.ProgramName, MessageBoxButton.OK, MessageBoxImage.Information);
                    //Do code of what you want after successful extend here!
                    Console.Clear();
                    goto home;
                }
                else goto re; //Retry
            }
            Console.Read();
        }
        public static void PrintLogo()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(@"_______        _________        _______         _______ _______ ______  ");
            Console.WriteLine(@"(  ___  |\     /\__   __|\     /(  ____ |\     /(  ___  (  ____ (  __  \ ");
            Console.WriteLine(@"| (   ) | )   ( |  ) (  | )   ( | (    \| )   ( | (   ) | (    )| (  \  )");
            Console.WriteLine(@"| (___) | |   | |  | |  | (___) | |     | |   | | (___) | (____)| |   ) |");
            Console.WriteLine(@"|  ___  | |   | |  | |  |  ___  | | ____| |   | |  ___  |     __| |   | |");
            Console.WriteLine(@"| (   ) | |   | |  | |  | (   ) | | \_  | |   | | (   ) | (\ (  | |   ) |");
            Console.WriteLine(@"| )   ( | (___) |  | |  | )   ( | (___) | (___) | )   ( | ) \ \_| (__/  )");
            Console.WriteLine(@"|/     \(_______)  )_(  |/     \(_______(_______|/     \|/   \__(______/ ");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
