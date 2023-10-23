using CarTroubleSolver.Data.Configuration;
using CarTroubleSolver.Logic.Configuration;
using CarTroubleSolver.Logic.Dto.Cars;
using CarTroubleSolver.Logic.Dto.User;
using CarTroubleSolver.Logic.Services.Interfaces;
using CarTroubleSolver.Logic.Validation;
using ConsoleTables;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

//Services Configuration
var serviceProvider = new ServiceCollection()
            .AddRepositories()
            .AddServices()
            .BuildServiceProvider();

var userService = serviceProvider.GetRequiredService(typeof(IUserService)) as IUserService;
var carService = serviceProvider.GetRequiredService(typeof(ICarService)) as ICarService;




//Variables
int selectedOption = 0;
int selectedOptionTryAgainMenu = 0;
bool userIsLogged = false;
LogedInUserDto user = null;
CarDto carHolder = null;


string[] startingMenuOptions = { "Log In", "Register", "EndSession" };
string[] tryAgainMenu = { "Try Again", "Quick" };
string[] logedUserMenu = { "User Information", "Find Help", "Try Help Somebody", "Log Out" };
string[] userCarCRUD = { "Add Car", "Update Car Info.", "Delete Cars", "Quit" };

int centerX = Console.WindowWidth / 2;


Console.OutputEncoding = System.Text.Encoding.UTF8;

// ElementsPosition
const int LOGIN_MESSAGE_TOP = 5;
const int MENU_TOP = 3;
const int INPUT_PROMPT_TOP = 7;

while (true)
{
    if (!userIsLogged)
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
            Console.SetCursorPosition(centerX - 40, MENU_TOP + i);
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
                #region Login
                Console.Clear();
                Console.SetCursorPosition(0, LOGIN_MESSAGE_TOP);
                Console.WriteLine("Panel logowania:");

                Console.SetCursorPosition(0, INPUT_PROMPT_TOP);
                Console.WriteLine("Email: ");
                var email = Console.ReadLine();

                Console.SetCursorPosition(0, INPUT_PROMPT_TOP + 2);
                Console.WriteLine("Hasło: ");
                var password = GetPasswordInput();

                if (userService.VerifyUserInputs(email, password))
                {
                    userIsLogged = true;
                    user = userService.GetLoggedInUser(email);
                }
                else
                {
                    Console.SetCursorPosition(0, INPUT_PROMPT_TOP + 10);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Nieprawidłowy email lub hasło");
                    await Task.Delay(3500);
                    Console.ResetColor();
                }
                #endregion
            }
            else if (selectedOption == 1)
            {
            Register:
                #region
                RegisterUserDto newUser = GetUserData();

                do
                {
                    Console.Clear();
                    string validationMessages = DisplayValidationErrors(newUser);

                    if (validationMessages.IsNullOrEmpty())
                    {
                        Console.SetCursorPosition(centerX, 0);
                        Console.WriteLine("User is valid.");
                        Console.SetCursorPosition(centerX - 5, 1);
                        Console.WriteLine("Congratulations, you have created an account.");
                        await Task.Delay(3000);
                        userService.Add(newUser);
                        Console.SetCursorPosition(0, 0);
                        break;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid Model.\n\n");

                        Console.WriteLine(validationMessages);
                        Console.ResetColor();
                        await Task.Delay(3500);
                    }

                    if (ShowTryAgainMenu() == 0)
                    {
                        newUser = GetUserData();
                    }
                    else
                    {
                        break;
                    }

                } while (true);
                #endregion
            }
            else if (selectedOption == 2)
            {
                Console.WriteLine("Quick from service.");
                break;
            }
        }
    }
    else
    {
        Console.Clear();

        while (true)
        {
            for (int i = 0; i < logedUserMenu.Length; i++)
            {
                if (i == selectedOption)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Blue;
                }


                Console.SetCursorPosition(centerX - 40, MENU_TOP + i);
                Console.WriteLine($"{i + 1}. {logedUserMenu[i]}");
                Console.ResetColor();
            }

            ConsoleKeyInfo keyInfo = Console.ReadKey();

            if (keyInfo.Key == ConsoleKey.UpArrow)
            {
                selectedOption = (selectedOption - 1 + logedUserMenu.Length) % logedUserMenu.Length;
            }
            else if (keyInfo.Key == ConsoleKey.DownArrow)
            {
                selectedOption = (selectedOption + 1) % logedUserMenu.Length;
            }
            else if (keyInfo.Key == ConsoleKey.Enter)
            {
                if (selectedOption == 0)
                {
                    Console.Clear();

                    var userTable = new ConsoleTable("Property Name", "Value")
                        .AddRow("Name:", user.Name)
                        .AddRow("Surname:", user.Surname)
                        .AddRow("Email:", user.Email)
                        .AddRow("Phone Nummber:", user.PhoneNumber)
                        .AddRow("Date Of Birth:", user.DateOfBirth.ToShortDateString());

                    Console.WriteLine(userTable);

                    var userCarsTable = new ConsoleTable("Brand", "Model", "Fuel", "Engine Type", "Mileage", "Doors");// skonczyc tu

                    Console.WriteLine(userCarsTable);

                    while (true)
                    {

                        for (int i = 0; i < userCarCRUD.Length; i++)
                        {
                            if (i == selectedOption)
                            {
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.BackgroundColor = ConsoleColor.Blue;
                            }


                            Console.SetCursorPosition(centerX + 12, MENU_TOP + i);
                            Console.WriteLine($"{i + 1}. {userCarCRUD[i]}");
                            Console.ResetColor();
                        }

                        keyInfo = Console.ReadKey();

                        if (keyInfo.Key == ConsoleKey.UpArrow)
                        {
                            selectedOption = (selectedOption - 1 + logedUserMenu.Length) % logedUserMenu.Length;
                        }
                        else if (keyInfo.Key == ConsoleKey.DownArrow)
                        {
                            selectedOption = (selectedOption + 1) % logedUserMenu.Length;
                        }
                        else if (keyInfo.Key == ConsoleKey.Enter)
                        {
                            if (selectedOption == 0)
                            {
                                //Add Car
                            }
                            else if (selectedOption == 1)
                            {
                                //UpdateCar
                            }
                            else if (selectedOption == 2)
                            {
                                //DeleteCar
                            }
                            else if (selectedOption == 3)
                            {
                                break;
                            }
                        }

                    }
                    Console.Clear();

                }
                else if (selectedOption == 1)
                {

                }
                else if (selectedOption == 2)
                {
                    userIsLogged = false;
                    break;
                }
            }


        }
    }
}
serviceProvider.Dispose();



string GetPasswordInput()
{
    string password = "";
    ConsoleKeyInfo keyInfo;

    do
    {
        keyInfo = Console.ReadKey(intercept: true); // Pojedyńcze znaki są zczytywane ale nie są wyświetlane

        if (keyInfo.Key != ConsoleKey.Backspace && keyInfo.Key != ConsoleKey.Enter)
        {
            password += keyInfo.KeyChar;
            Console.Write("*");
        }
        else if (keyInfo.Key == ConsoleKey.Backspace && password.Length > 0)
        {
            password = password.Remove(password.Length - 1);
            Console.Write("\b \b");
        }
    } while (keyInfo.Key != ConsoleKey.Enter);

    Console.WriteLine();

    return password;
}
RegisterUserDto GetUserData()
{
    Console.Clear();
    Console.WriteLine("Welcome in register panel enter your data: ");

    Console.WriteLine("Name: ");
    var name = Console.ReadLine();

    Console.WriteLine("Surname: ");
    var surname = Console.ReadLine();

    Console.WriteLine("Email: ");
    var email = Console.ReadLine();

    Console.WriteLine("Password: ");
    var password = GetPasswordInput();

    Console.WriteLine("Confirm Password: ");
    var confirmPassword = GetPasswordInput();

    Console.WriteLine("PhoneNumber: ");
    var phoneNumber = int.Parse(Console.ReadLine());

    Console.WriteLine("Date Of Birth (dd-mm-yyyy): ");
    var DateOfBirth = DateTime.Parse(Console.ReadLine());

    return new RegisterUserDto()
    {
        Name = name,
        Surname = surname,
        Email = email,
        ConfirmPassword = confirmPassword,
        Password = password,
        PhoneNumber = phoneNumber,
        DateOfBirth = DateOfBirth
    };

}//Dodać try catch w podawaniu inputach
int ShowTryAgainMenu()
{
    int selectedOptionTryAgainMenu = 0;

    while (true)
    {
        Console.Clear();

        Console.WriteLine("\nDo you want to try again to create an account?");
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
            return selectedOptionTryAgainMenu;
        }
    }
}
string DisplayValidationErrors(RegisterUserDto user)
{
    var validator = new RegisterUserDtoValidator();
    var validationResult = validator.Validate(user);
    if (validationResult.IsValid)
    {
        return string.Empty;
    }
    else
    {
        var output = "";
        foreach (var error in validationResult.Errors)
        {
            output += $"\nProperty: {error.PropertyName}, Error: {error.ErrorMessage}";
        }
        return output;
    }


}