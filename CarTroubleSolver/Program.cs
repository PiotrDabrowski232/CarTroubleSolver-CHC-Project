using CarTroubleSolver.Data.Configuration;
using CarTroubleSolver.Logic.Configuration;
using CarTroubleSolver.Logic.Services.Interfaces;
using CarTroubleSolver.Logic.Validation;
using Microsoft.Extensions.DependencyInjection;
using TheCarMarket.Data.Models;

//Services Configuration
var serviceProvider = new ServiceCollection()
            .AddRepositories()
            .AddServices()
            .BuildServiceProvider();

var userService = serviceProvider.GetRequiredService(typeof(IUserService)) as IUserService;


//Variables
//bool wantEnd = false;
int selectedOption = 0;
int selectedOptionTryAgainMenu = 0;
string[] startingMenuOptions = { "Log In", "Register", "EndSession" };
string[] tryAgainMenu = { "Try Again", "Quick" };
int centerX = Console.WindowWidth / 2;
int centerY = Console.WindowHeight / 2;



while (true)
{
    Console.Clear();
    Console.WriteLine("Wybierz opcję:");
    for (int i = 0; i < startingMenuOptions.Length; i++)
    {
        if (i == selectedOption)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Blue;
        }
        Console.WriteLine($"{i + 1}. {startingMenuOptions[i]}");
        Console.ResetColor();
    }

    ConsoleKeyInfo keyInfo = Console.ReadKey();

    if (keyInfo.Key == ConsoleKey.UpArrow)
    {
        selectedOption = (selectedOption - 1 + startingMenuOptions.Length) % startingMenuOptions.Length;
    }
    else if (keyInfo.Key == ConsoleKey.DownArrow)
    {
        selectedOption = (selectedOption + 1) % startingMenuOptions.Length;
    }
    else if (keyInfo.Key == ConsoleKey.Enter)
    {
        if (selectedOption == 0)
        {
            Console.Clear();
            Console.WriteLine("Wybrano logowanie. Wprowadź swoje dane logowania.");


            Console.ReadKey();
        }
        else if (selectedOption == 1)
        {
        StartOfRegister:
            #region Register

            Console.Clear();
            Console.WriteLine("Wybrano rejestrację. Wprowadź dane rejestracyjne.");
            try
            {
                Console.WriteLine("Name: ");
                var name = Console.ReadLine();

                Console.WriteLine("Surname: ");
                var surname = Console.ReadLine();

                Console.WriteLine("Email: ");
                var email = Console.ReadLine();

                Console.WriteLine("Password: ");
                var password = Console.ReadLine();

                Console.WriteLine("PhoneNumber: ");
                var phoneNumber = int.Parse(Console.ReadLine());

                Console.WriteLine("Date Of Birth{dd-mm-yyyy}: ");
                var DateOfBirth = DateTime.Parse(Console.ReadLine());

                var user = new User()
                {
                    Id = Guid.NewGuid(),
                    Name = name,
                    Surname = surname,
                    Email = email,
                    Password = password,
                    PhoneNumber = phoneNumber,
                    DateOfBirth = DateOfBirth
                };

                Console.Clear();

                var validator = new UserValidator();
                var validationResult = validator.Validate(user);

                if (validationResult.IsValid)
                {
                    Console.SetCursorPosition(centerX, centerY);
                    Console.WriteLine("User is valid.");
                    Console.WriteLine("Congratulation You Have Created Account");
                    userService.Add(user);
                    Console.SetCursorPosition(0, 0);
                }
                else
                {
                    Console.WriteLine("User is not valid.");
                    foreach (var error in validationResult.Errors)
                    {
                        Console.WriteLine($"Property: {error.PropertyName}, Error: {error.ErrorMessage}");
                    }
                    await Task.Delay(3500);


                    while (true)
                    {
                        Console.Clear();

                        Console.WriteLine("\n Do you want to try again create account?");
                        for (int i = 0; i < tryAgainMenu.Length; i++)
                        {
                            if (i == selectedOptionTryAgainMenu)
                            {
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.BackgroundColor = ConsoleColor.Blue;
                            }
                            Console.WriteLine($"{i + 1}. {tryAgainMenu[i]}");
                            Console.ResetColor();
                        }
                        ConsoleKeyInfo tryAgainKeyInfo = Console.ReadKey();

                        if (tryAgainKeyInfo.Key == ConsoleKey.UpArrow)
                        {
                            selectedOptionTryAgainMenu = (selectedOptionTryAgainMenu - 1 + tryAgainMenu.Length) % tryAgainMenu.Length;
                        }
                        else if (tryAgainKeyInfo.Key == ConsoleKey.DownArrow)
                        {
                            selectedOptionTryAgainMenu = (selectedOptionTryAgainMenu + 1) % tryAgainMenu.Length;
                        }
                        else if (tryAgainKeyInfo.Key == ConsoleKey.Enter)
                        {
                            if (tryAgainKeyInfo.Key == ConsoleKey.Enter)
                            {
                                if (selectedOptionTryAgainMenu == 0)
                                {
                                    goto StartOfRegister;
                                }
                                else if (selectedOptionTryAgainMenu == 1)
                                {
                                    break;
                                }
                            }
                        }

                    }

                }

            }
            catch (Exception ex)
            {

            }
            #endregion

        }
        else if (selectedOption == 2)
        {
            // Opcja wyjścia
            Console.WriteLine("Wyjście z programu.");
            break;
        }
    }
}
serviceProvider.Dispose();