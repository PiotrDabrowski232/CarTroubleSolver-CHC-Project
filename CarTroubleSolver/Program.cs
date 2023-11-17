using CarTroubleSolver.Data.Configuration;
using CarTroubleSolver.Data.Models.Enums;
using CarTroubleSolver.Logic.Configuration;
using CarTroubleSolver.Logic.Dto.Accident;
using CarTroubleSolver.Logic.Dto.Cars;
using CarTroubleSolver.Logic.Dto.User;
using CarTroubleSolver.Logic.Services.Interfaces;
using CarTroubleSolver.Logic.Validation;
using ConsoleTables;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

#region ServicesConfiguration
//Services Configuration
var serviceProvider = new ServiceCollection()
            .AddRepositories()
            .AddServices()
            .BuildServiceProvider();

var userService = serviceProvider.GetRequiredService(typeof(IUserService)) as IUserService;
var carService = serviceProvider.GetRequiredService(typeof(ICarService)) as ICarService;
var accidentService = serviceProvider.GetRequiredService(typeof(IAccidentService)) as IAccidentService;
#endregion



#region Variables
string validationErrors = string.Empty;
int selectedOption = 0;
int selectedOptionTryAgainMenu = 0;
bool userIsLogged = false;
LogedInUserDto user = null;
CarDto carHolder = null;

string[] fields = { "Name", "Surname", "Email", "Password", "Confirm Password", "Phone Number", "Date Of Birth", "Submit", "Quit" };
string[] startingMenuOptions = { "Log In", "Register", "EndSession" };
string[] tryAgainMenu = { "Try Again", "Quick" };
string[] logedUserMenu = { "User Profile", "Find Help", "Log Out" };
string[] userCarCRUD = { "Add Car", "Delete Cars", "Accident History", "Quit" };

int centerX = Console.WindowWidth / 2;


Console.OutputEncoding = System.Text.Encoding.UTF8;

// ElementsPosition
const int LOGIN_MESSAGE_TOP = 5;
const int MENU_TOP = 3;
const int INPUT_PROMPT_TOP = 7;
#endregion




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
                Console.SetCursorPosition(15, LOGIN_MESSAGE_TOP);
                Console.WriteLine("Login Panel:");

                Console.SetCursorPosition(15, INPUT_PROMPT_TOP);
                Console.Write("Email: ");
                var email = Console.ReadLine();

                Console.SetCursorPosition(15, INPUT_PROMPT_TOP + 2);
                Console.Write("Hasło: ");
                var password = GetPasswordInput();

                if (userService.VerifyUserInputs(email, password))
                {
                    userIsLogged = true;
                    user = userService.GetLoggedInUser(email);
                    validationErrors = string.Empty;
                }
                else
                {
                    Console.SetCursorPosition(0, INPUT_PROMPT_TOP + 10);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Nieprawidłowy email lub hasło");
                    await Task.Delay(2500);
                    Console.ResetColor();
                }
                #endregion
            }
            else if (selectedOption == 1)
            {
                #region Register
                var userFromInputs = GetUserData();

                if (userFromInputs != null)
                {
                    userService.Add(userFromInputs);
                    Console.Clear();
                    Console.SetCursorPosition(centerX - 10, MENU_TOP + 5);
                    Console.WriteLine("Congratulations!!! User Created");
                    await Task.Delay(3500);
                    validationErrors = string.Empty;
                }
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
        bool tablclicked = false;
        int selectedAdvertisement = int.MaxValue;
        var accidents = accidentService.GetAllFreeAccidents(user.Email).ToList();

        while (true)
        {
            Console.SetCursorPosition(centerX + 35, MENU_TOP - 3);
            Console.WriteLine($"Welcome {user.Name} {user.Surname}");

            Console.SetCursorPosition(centerX - 10, MENU_TOP - 3);
            Console.WriteLine("Click Tab To Change Menu");
            for (int i = 0; i < logedUserMenu.Length; i++)
            {
                if (i == selectedOption)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Blue;
                }


                Console.SetCursorPosition((Console.WindowWidth / logedUserMenu.Length) * i + 10, MENU_TOP);
                Console.WriteLine($"{i + 1}. {logedUserMenu[i]}");
                Console.ResetColor();
            }

            Console.SetCursorPosition(0, MENU_TOP + 5);

            ConsoleKey key;

            Console.SetCursorPosition(12, 5);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Immediate assistance needed");

            Console.SetCursorPosition(12, 7);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Moderate Severity");

            Console.SetCursorPosition(12, 9);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("Minor Severity");

            Console.ResetColor();

            for (int i = 0; i < accidents.Count(); i++)
            {
                if (i == selectedAdvertisement)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                else if (accidents[i].CollisionSeverity == CollisionSeverity.Severe)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                else if (accidents[i].CollisionSeverity == CollisionSeverity.Moderate)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                }
                else if (accidents[i].CollisionSeverity == CollisionSeverity.Minor)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                }

                Console.SetCursorPosition(centerX - 35, MENU_TOP + i + 12);
                Console.WriteLine(accidents[i].ToString());
                Console.WriteLine();

                Console.ResetColor();
            }

            ConsoleKeyInfo keyInfo = Console.ReadKey();

            if (keyInfo.Key == ConsoleKey.LeftArrow || keyInfo.Key == ConsoleKey.DownArrow)
            {
                if (tablclicked)
                {
                    if (selectedAdvertisement < accidents.Count() - 1)
                        selectedAdvertisement++;
                }
                else
                    selectedOption = (selectedOption - 1 + logedUserMenu.Length) % logedUserMenu.Length;
            }
            else if (keyInfo.Key == ConsoleKey.RightArrow || keyInfo.Key == ConsoleKey.UpArrow)
            {
                if (tablclicked)
                {
                    if (selectedAdvertisement > 0)
                        selectedAdvertisement--;
                }
                else
                    selectedOption = (selectedOption + 1) % logedUserMenu.Length;
            }
            else if (keyInfo.Key == ConsoleKey.Tab)
            {
                tablclicked = !tablclicked;

                if (tablclicked)
                {
                    selectedAdvertisement = 0;
                    selectedOption = int.MaxValue;
                }
                else
                {
                    selectedAdvertisement = int.MaxValue;
                    selectedOption = 0;
                }

            }
            else if (keyInfo.Key == ConsoleKey.Enter)
            {
                if (tablclicked && accidents.Count() > 0)
                {
                    await DisplayAccidentDetails(accidents[selectedAdvertisement]);
                    break;
                }
                else
                {
                    #region UserView
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

                        var userCars = carService.GetUserCars<CarDto>(user.Email);

                        var userCarsTable = new ConsoleTable("Brand", "Model", "Engine Type", "Fuel", "Mileage", "Doors", "Color");

                        foreach (var car in userCars)
                        {
                            userCarsTable.AddRow(car.Brand, car.CarModels, car.EngineType, car.FuelType, car.Mileage, car.DoorCount, car.Color);
                        }

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
                                selectedOption = (selectedOption - 1 + userCarCRUD.Length) % userCarCRUD.Length;
                            }
                            else if (keyInfo.Key == ConsoleKey.DownArrow)
                            {
                                selectedOption = (selectedOption + 1) % userCarCRUD.Length;
                            }
                            else if (keyInfo.Key == ConsoleKey.Enter)
                            {
                                if (selectedOption == 0)
                                {
                                    comaBackToCarCreator:
                                    var car = AddCarProfile();

                                    if(!validationErrors.IsNullOrEmpty())
                                    {
                                        Console.Clear();
                                        Console.SetCursorPosition(centerX, 0);
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine("Invalid Model.\n\n");

                                        Console.SetCursorPosition(centerX, 0);
                                        Console.WriteLine(validationErrors);
                                        Console.ResetColor();
                                        await Task.Delay(3500);
                                        selectedOption = 0;
                                        Console.Clear();
                                        validationErrors = "";
                                        goto comaBackToCarCreator;
                                    }
                                    else
                                    {
                                        carService.Add(car, user.Email);
                                        break;
                                    }
                                }
                                else if (selectedOption == 1)
                                {
                                    if (userCars.Count() > 0)
                                        SelectCarFromTable(userCars.ToList());
                                    else
                                    {
                                        Console.Clear();
                                        Console.SetCursorPosition(centerX - 14, MENU_TOP + 10);
                                        Console.WriteLine("You have no cars to remove ");
                                        await Task.Delay(3000);
                                        Console.Clear();
                                        break;
                                    }
                                    break;
                                }
                                else if (selectedOption == 2)
                                {
                                    ShowHistory();
                                    break;
                                }
                                else if (selectedOption == 3)
                                {
                                    break;
                                }
                            }

                        }
                        Console.Clear();

                    }
                    #endregion
                    #region AddHelpRequest
                    else if (selectedOption == 1)
                    {
                        Console.Clear();


                        var cars = carService.GetUserCars<CarDto>(user.Email).ToList();

                        if (cars.Count() > 0)
                        {
                            Console.SetCursorPosition(centerX - 14, MENU_TOP - 3);
                            Console.WriteLine("Add a request for assistance");

                            var accidentHappened = SendAccidentRequest(cars);
                            accidentService.AddAccident(accidentHappened, user.Email);

                            Console.Clear();
                        }
                        else
                        {
                            Console.SetCursorPosition(centerX - 14, MENU_TOP + 10);
                            Console.WriteLine("Firstly Add Cars In User Profile");
                            await Task.Delay(3000);
                            Console.Clear();
                            break;
                        }
                    }
                    #endregion
                    #region Logout
                    else if (selectedOption == 2)
                    {
                        userIsLogged = false;
                        selectedOption = 0;
                        break;
                    }
                    #endregion
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
CarDto AddCarProfile()
{
    Console.Clear();

    CarDto car = new CarDto();

    int selectedBrandIndex = 0;

    ConsoleKey key;
    #region Brand
    do
    {
        Console.Clear();
        Console.WriteLine("Select Car Brand:");

        int columnsPerRow = 5;

        for (int i = 0; i < Enum.GetNames(typeof(CarBrand)).Length; i++)
        {
            if (i == selectedBrandIndex)
            {
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Black;
            }

            Console.Write($"{i + 1}. {((CarBrand)i),-15}\t");

            if (i == selectedBrandIndex)
            {
                Console.ResetColor();
            }

            if ((i + 1) % columnsPerRow == 0)
            {
                Console.WriteLine();
            }
        }

        key = Console.ReadKey(true).Key;

        if (key == ConsoleKey.LeftArrow && selectedBrandIndex > 0)
        {
            selectedBrandIndex--;
        }
        else if (key == ConsoleKey.RightArrow && selectedBrandIndex < Enum.GetNames(typeof(CarBrand)).Length - 1)
        {
            selectedBrandIndex++;
        }
        else if (key == ConsoleKey.UpArrow && (selectedBrandIndex - 5) >= 0)
        {
            selectedBrandIndex -= 5;
        }
        else if (key == ConsoleKey.DownArrow && (selectedBrandIndex + 5) <= Enum.GetNames(typeof(CarBrand)).Length - 1)
        {
            selectedBrandIndex += 5;
        }
    } while (key != ConsoleKey.Enter);
    #endregion

    car.Brand = (CarBrand)selectedBrandIndex;
    Console.Clear();

    Console.WriteLine($"Selected Brand: {car.Brand}");

    Console.Write("Car Model: ");
    car.CarModels = Console.ReadLine();

    Console.Write("Type Car Engine Type (For Example V12): ");
    car.EngineType = Console.ReadLine();

    #region FuelType
    int selectedFuelIndex = 0;
    do
    {
        Console.Clear();
        Console.WriteLine("Select Fuel Type:");
        for (int i = 0; i < Enum.GetNames(typeof(FuelType)).Length; i++)
        {
            if (i == selectedFuelIndex)
            {
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Black;
            }

            Console.WriteLine($"{i + 1}. {((FuelType)i)}");

            if (i == selectedFuelIndex)
            {
                Console.ResetColor();
            }
        }

        key = Console.ReadKey(true).Key;

        if (key == ConsoleKey.UpArrow && selectedFuelIndex > 0)
        {
            selectedFuelIndex--;
        }
        else if (key == ConsoleKey.DownArrow && selectedFuelIndex < Enum.GetNames(typeof(FuelType)).Length - 1)
        {
            selectedFuelIndex++;
        }

    } while (key != ConsoleKey.Enter);

    #endregion


    car.FuelType = (FuelType)selectedFuelIndex;
    Console.Clear();

    Console.WriteLine($"Selected Brand: {car.Brand}");
    Console.WriteLine($"\nCar Model: {car.CarModels}");
    Console.WriteLine($"\nType Car Engine Type (For Example V12): {car.EngineType}");

    Console.WriteLine($"\nSelected Fuel: {car.FuelType}");
    try
    {
        Console.Write("\nHow many doors your car has: ");
        car.DoorCount = int.Parse(Console.ReadLine());
    }
    catch(Exception e)
    {
        validationErrors += "\nDoorCount Should Have only numers";
    }

    try
    {
        Console.Write("\nType Mileage: ");
        car.Mileage = int.Parse(Console.ReadLine());
    }
    catch (Exception e)
    {
        validationErrors += "\nMileage Should Have only numers";
    }

    Console.Write("\nCar Color: ");
    car.Color = Console.ReadLine();

    Console.Clear();

    var validator = new CarDtoValidator();
    var validationResult = validator.Validate(car);

    if (validationResult.IsValid)
    {
        validationErrors = string.Empty;
    }
    else
    {
        foreach (var error in validationResult.Errors)
        {
            validationErrors += $"\n Error: {error.ErrorMessage}";
        }
    }
    return car;
}
void SelectCarFromTable(IList<CarDto> cars)
{

    Console.Clear();
    selectedOption = 0;
    while (true)
    {
        Console.SetCursorPosition(centerX - 10, MENU_TOP - 3);
        Console.WriteLine($"Select Car To delete");

        for (int i = 0; i < cars.Count(); i++)
        {
            if (i == selectedOption)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Blue;
            }


            Console.SetCursorPosition(centerX - 10, MENU_TOP + i);
            Console.WriteLine($"{i + 1}. {cars[i].Brand} {cars[i].CarModels}");
            Console.ResetColor();
        }

        ConsoleKeyInfo keyInfo = Console.ReadKey();

        if (keyInfo.Key == ConsoleKey.UpArrow)
        {
            selectedOption = (selectedOption - 1 + cars.Count()) % cars.Count();
        }
        else if (keyInfo.Key == ConsoleKey.DownArrow)
        {
            selectedOption = (selectedOption + 1) % cars.Count();
        }
        else if (keyInfo.Key == ConsoleKey.Enter)
        {
            var carToDelete = cars[selectedOption];
            string[] yesNoAnswer = new string[] { "Yes", "No" };
            selectedOption = 0;
            while (true)
            {
                Console.Clear();
                Console.SetCursorPosition(0, MENU_TOP - 3);
                Console.WriteLine("Are you sure that wyou want Delete car " +
                    $"({carToDelete.Brand} {carToDelete.CarModels} {carToDelete.FuelType} {carToDelete.EngineType}) ?");

                for (int i = 0; i < yesNoAnswer.Length; i++)
                {
                    if (i == selectedOption)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.BackgroundColor = ConsoleColor.Blue;
                    }


                    Console.SetCursorPosition(centerX - 10, MENU_TOP + i);
                    Console.WriteLine($"{i + 1}. {yesNoAnswer[i]}");
                    Console.ResetColor();
                }

                keyInfo = Console.ReadKey();

                if (keyInfo.Key == ConsoleKey.UpArrow)
                {
                    selectedOption = 0;
                }
                else if (keyInfo.Key == ConsoleKey.DownArrow)
                {
                    selectedOption = 1;
                }
                else if (keyInfo.Key == ConsoleKey.Enter)
                {
                    if (selectedOption == 1)
                    {
                        break;
                    }
                    else
                    {
                        carService.DeleteCarFromUserCollection(carToDelete, user.Email);
                        break;
                    }
                }
            }
            break;
        }
    }
}
AccidentDto SendAccidentRequest(IList<CarDto> cars)
{
    AccidentDto accident = new AccidentDto();

    selectedOption = 0;

    while (true)
    {
        Console.SetCursorPosition(centerX - 10, MENU_TOP - 1);
        Console.WriteLine($"Select Car:");

        for (int i = 0; i < cars.Count(); i++)
        {
            if (i == selectedOption)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Blue;
            }


            Console.SetCursorPosition(centerX - 10, MENU_TOP + i);
            Console.WriteLine($"{i + 1}. {cars[i].Brand} {cars[i].CarModels}");
            Console.ResetColor();
        }

        ConsoleKeyInfo keyInfo = Console.ReadKey();

        if (keyInfo.Key == ConsoleKey.UpArrow)
        {
            selectedOption = (selectedOption - 1 + cars.Count()) % cars.Count();
        }
        else if (keyInfo.Key == ConsoleKey.DownArrow)
        {
            selectedOption = (selectedOption + 1) % cars.Count();
        }
        else if (keyInfo.Key == ConsoleKey.Enter)
        {
            var carFromAccident = cars[selectedOption];

            accident.CarId = carService.GetCarId(carFromAccident, user.Email);

            int selectedSeverityIndex = 0;

            ConsoleKey key;
            do
            {
                Console.Clear();
                Console.WriteLine("Select Severity:");
                for (int i = 0; i < Enum.GetNames(typeof(CollisionSeverity)).Length; i++)
                {
                    if (i == selectedSeverityIndex)
                    {
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.ForegroundColor = ConsoleColor.Black;
                    }

                    Console.WriteLine($"{i + 1}. {((CollisionSeverity)i)}");

                    if (i == selectedSeverityIndex)
                    {
                        Console.ResetColor();
                    }
                }

                key = Console.ReadKey(true).Key;

                if (key == ConsoleKey.UpArrow && selectedSeverityIndex > 0)
                {
                    selectedSeverityIndex--;
                }
                else if (key == ConsoleKey.DownArrow && selectedSeverityIndex < Enum.GetNames(typeof(CollisionSeverity)).Length - 1)
                {
                    selectedSeverityIndex++;
                }

            } while (key != ConsoleKey.Enter);

            accident.CollisionSeverity = (CollisionSeverity)selectedSeverityIndex;
            Console.Clear();

            Console.WriteLine($"Vehicle involved in the accident: " +
                $"\nBrand: {carFromAccident.Brand}\nModel: {carFromAccident.CarModels}\n" +
                $"Engine Type: {carFromAccident.EngineType}\nMileage: {carFromAccident.Mileage}");

            Console.WriteLine($"Collision Severity: {accident.CollisionSeverity}");

            Console.WriteLine("\nWrite here Description of Accident: ");
            accident.AccidentDescription = Console.ReadLine();

            return accident;
        }
    }

}
async Task DisplayAccidentDetails(AccidentAdvertisementDto accident)
{
    string[] accidentMenu = { "Commitment of aid", "Quit" };

    int selectedOption = 0;

    while (true)
    {
        ConsoleKey key;
        do
        {
            Console.Clear();

            Console.Write($"\n\n\t\t\t\tUser: {accident.ApplicantUserInfo.Name} {accident.ApplicantUserInfo.Surname}\n" +
                    $"\t\t\t\tTelephone Number: {accident.ApplicantUserInfo.PhoneNumber}\n" +
                    $"\n\t\t\t\tCar:\n" +
                    $"\t\t\t\tBrand: {accident.CarInfo.Brand}\n" +
                    $"\t\t\t\tModel: {accident.CarInfo.CarModels}\n" +
                    $"\t\t\t\tEngine: {accident.CarInfo.EngineType}\n" +
                    $"\t\t\t\tMileage {accident.CarInfo.Mileage}\n" +
                    $"\t\t\t\tFuel Type: {accident.CarInfo.FuelType}\n" +
                    $"\t\t\t\tDoor Count: {accident.CarInfo.DoorCount}\n" +
                    $"\n\t\t\t\tAccident Description:\n" +
                    $"\t\t\t\tColison Severity: {accident.CollisionSeverity}\n" +
                    $"\t\t\t\tColision Description: {accident.AccidentDescription}");

            for (int i = 0; i < accidentMenu.Length; i++)
            {
                if (i == selectedOption)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                }


                if (i > 0)
                    Console.SetCursorPosition(centerX + accidentMenu[i - 1].Length + 3 - 20, MENU_TOP + 20);
                else
                    Console.SetCursorPosition(centerX - 20, MENU_TOP + 20);

                Console.WriteLine(accidentMenu[i]);
                Console.ResetColor();
            }
            key = Console.ReadKey(true).Key;

            if (key == ConsoleKey.LeftArrow && selectedOption > 0)
            {
                selectedOption--;
            }
            else if (key == ConsoleKey.RightArrow && selectedOption < accidentMenu.Length - 1)
            {
                selectedOption++;
            }
        } while (key != ConsoleKey.Enter);

        if (selectedOption == 0)
        {
            accidentService.HelpInAccident(user.Email, accident.Id);
            Console.Clear();
            Console.SetCursorPosition(centerX - 15, MENU_TOP);
            Console.WriteLine($"congratulations!!!");
            Console.SetCursorPosition(centerX - 25, MENU_TOP + 1);
            Console.WriteLine($"You have made a commitment to help for {accident.ApplicantUserInfo.Name}");
            Console.SetCursorPosition(centerX - 32, MENU_TOP + 2);
            Console.WriteLine($" You can see all your contact information to {accident.ApplicantUserInfo.Name} in your user panel");

            await Task.Delay(4000);

            break;
        }
        else
        {
            break;
        }

    }
}
void ShowHistory()
{
    Console.Clear();

    var historyOfAsignee = accidentService.ShowHistoryOfAccidentsAsignee(user.Email);
    var historyOfApplicant = accidentService.ShowHistoryOfAccidentsApplicant(user.Email);

    var assigneeHistory = new ConsoleTable("Applicant Name", "Applicant Surname", "Applicant Telephone", "Car Brand", "Car Model", "Severity");

    foreach (var accident in historyOfAsignee)
    {
        assigneeHistory.AddRow(accident.ApplicantUserInfo.Name, accident.ApplicantUserInfo.Surname, accident.ApplicantUserInfo.PhoneNumber, accident.CarInfo.Brand, accident.CarInfo.CarModels, accident.CollisionSeverity);
    }

    Console.WriteLine(assigneeHistory);


    var applicantHistory = new ConsoleTable("Car Brand", "Car Model", "Severity");

    foreach (var accident in historyOfApplicant)
    {
        applicantHistory.AddRow(accident.CarInfo.Brand, accident.CarInfo.CarModels, accident.CollisionSeverity);
    }

    Console.SetCursorPosition(0, MENU_TOP + assigneeHistory.Rows.Count() + 12);

    Console.WriteLine(applicantHistory);


    Console.SetCursorPosition(centerX + 38, MENU_TOP + 5);

    Console.ForegroundColor = ConsoleColor.White;
    Console.BackgroundColor = ConsoleColor.DarkCyan;

    Console.WriteLine("Press anything to Quit");
    Console.ResetColor();

    Console.ReadKey();

    Console.Clear();

}
RegisterUserDto? GetUserData()
{
    RegisterUserDto registerUser;

    int currentIndex = 0;

    string name = "";
    string surname = "";
    string email = "";
    string password = "";
    string confirmPassword = "";
    int phoneNumber = 0;
    DateTime dateOfBirth = DateTime.Now;

    while (true)
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.White;
        Console.BackgroundColor = ConsoleColor.DarkBlue;

        Console.SetCursorPosition(centerX - 9, MENU_TOP + 10);
        Console.WriteLine("You can change the value of a field by re-entering the field value");
        Console.SetCursorPosition(centerX + 15, MENU_TOP + 12);
        Console.WriteLine("To Submit field press Enter");
        Console.SetCursorPosition(centerX + 15, MENU_TOP + 14);
        Console.WriteLine("To start writing press Enter");
        Console.SetCursorPosition(centerX + 15, MENU_TOP + 16);
        Console.WriteLine("Move By arrows");

        Console.ResetColor();


        for (int i = 0; i < fields.Length; i++)
        {
            if (currentIndex == i)
            {
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Black;
            }
            else
            {
                Console.ResetColor();
            }


            if (i == 7)
            {
                Console.SetCursorPosition(centerX, 12 + i * 2);
                Console.Write(fields[i]);
            }
            else
            {
                if (i == 8)
                {
                    Console.SetCursorPosition(centerX - 8, 12 + i * 2);
                    Console.Write(fields[i]);

                }
                else
                {
                    Console.SetCursorPosition(10, 12 + i * 2);
                    Console.Write(fields[i] + ": ");
                    Console.Write(GetFieldValue(i, name, surname, email, password, confirmPassword, phoneNumber, dateOfBirth));
                }
            }
        }

        Console.ResetColor();

        if (!validationErrors.IsNullOrEmpty())
        {
            Console.SetCursorPosition(centerX, 0);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Invalid Model.\n\n");

            Console.SetCursorPosition(centerX, 0);
            Console.WriteLine(validationErrors);
            Console.ResetColor();
        }

        Console.ResetColor();

        ConsoleKeyInfo keyInfo = Console.ReadKey();

        if (keyInfo.Key == ConsoleKey.DownArrow)
        {
            currentIndex = (currentIndex + 1) % fields.Length;
        }
        else if (keyInfo.Key == ConsoleKey.UpArrow)
        {
            currentIndex = (currentIndex - 1 + fields.Length) % fields.Length;
        }
        else if (keyInfo.Key == ConsoleKey.Enter)
        {
            if (currentIndex == 7)
            {
                registerUser = new RegisterUserDto()
                {
                    Name = name,
                    Surname = surname,
                    Email = email,
                    ConfirmPassword = confirmPassword,
                    Password = password,
                    PhoneNumber = phoneNumber,
                    DateOfBirth = dateOfBirth
                };

                validationErrors = DisplayValidationErrors(registerUser);
                if (validationErrors.IsNullOrEmpty())
                {
                    return registerUser;
                }
                else
                {
                    Console.SetCursorPosition(centerX, 0);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid Model.\n\n");

                    Console.SetCursorPosition(centerX, 2);
                    Console.WriteLine(validationErrors);
                    Console.ResetColor();
                }
            }
            else if (currentIndex == 8)
            {
                return null;
            }
            else
            {
                EditFieldValue(currentIndex, ref name, ref surname, ref email, ref password, ref confirmPassword, ref phoneNumber, ref dateOfBirth);

            }
        }
    }


}
void EditFieldValue(int index, ref string name, ref string surname, ref string email, ref string password, ref string confirmPassword, ref int phoneNumber, ref DateTime dateOfBirth)
{
    Console.SetCursorPosition(10 + fields[index].Length + 2 + GetFieldValue(index, name, surname, email, password, confirmPassword, phoneNumber, dateOfBirth).Length + 2, 12 + index * 2);
    
    string input;
    
    if (index == 3 || index == 4)
    {
        input = GetPasswordInput();

    }
    else
    {
        input = Console.ReadLine();

    }
    if (index == 0)
    {
        name = input;
    }
    else if (index == 1)
    {
        surname = input;
    }
    else if (index == 2)
    {
        email = input;
    }
    else if (index == 3)
    {
        password = input;
    }
    else if (index == 4)
    {
        confirmPassword = input;
    }
    else if (index == 5)
    {
        if (input.Length != 9)
            validationErrors += "\nPhone Number Should Have 9 figuer no less and no longer";
        else
            int.TryParse(input, out phoneNumber);
    }
    else if (index == 6)
    {
        DateTime.TryParse(input, out dateOfBirth);
    }

}
string GetFieldValue(int index, string name, string surname, string email, string password, string confirmPassword, int phoneNumber, DateTime dateOfBirth)
{
    switch (index)
    {
        case 0:
            return name;
        case 1:
            return surname;
        case 2:
            return email;
        case 3:
            return showAsStars(password);
        case 4:
            return showAsStars(confirmPassword);
        case 5:
            return phoneNumber.ToString();
        case 6:
            return dateOfBirth.ToString("dd-MM-yyyy");
        default:
            return "";
    }
}
string showAsStars(string password)
{
    string stars = "";
    for(int i = 0; i<password.Length; i++)
    {
        stars += "*";
    }
    return stars;
}
