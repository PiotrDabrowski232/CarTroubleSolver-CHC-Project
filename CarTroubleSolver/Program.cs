using CarTroubleSolver.Logic.Configuration;
using CarTroubleSolver.Logic.Functions.UserFunctions.Queries;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using TheCarMarket.Data.Models;

//Services Configuration
var serviceProvider = new ServiceCollection()
            .AddRepositories()
            .BuildServiceProvider();

var mediator = serviceProvider.GetRequiredService<IMediator>();

//Variables
bool wantEnd = false;
int selectedOption = 0;
string[] menuOptions = { "Logowanie", "Rejestracja", "Wyjście" };



while (true)
{
    Console.Clear();
    Console.WriteLine("Wybierz opcję:");
    for (int i = 0; i < menuOptions.Length; i++)
    {
        if (i == selectedOption)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Blue;
        }
        Console.WriteLine($"{i + 1}. {menuOptions[i]}");
        Console.ResetColor();
    }

    ConsoleKeyInfo keyInfo = Console.ReadKey();

    if (keyInfo.Key == ConsoleKey.UpArrow)
    {
        selectedOption = (selectedOption - 1 + menuOptions.Length) % menuOptions.Length;
    }
    else if (keyInfo.Key == ConsoleKey.DownArrow)
    {
        selectedOption = (selectedOption + 1) % menuOptions.Length;
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
            Console.Clear();
            Console.WriteLine("Wybrano rejestrację. Wprowadź dane rejestracyjne.");

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

            Console.WriteLine("Email: ");
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

            var something = await mediator.Send(new CreateUserQuery { User = user });

            Console.ReadKey();
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