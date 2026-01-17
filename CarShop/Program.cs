using System.Text.Json;
using System.Text.Json.Serialization;
using CarShop;

Console.Clear();

var dataFilePath = Path.Combine(Directory.GetCurrentDirectory(), "data.json");
var error = false;
Data data = new Data();
if (Path.Exists(dataFilePath))
{
    try
    {
        var dataFileContent = File.ReadAllText(dataFilePath);
        data = JsonSerializer.Deserialize<Data>(dataFileContent) ?? data;
    }
    catch (Exception ex)
    {
        Console.Error.WriteLine($"Error while reading file {dataFilePath}: {ex.ToString()}");
        error = true;
    }
}

var shopOpen = true;
var firstRun = true;
while (shopOpen)
{
    PrintShopMenu(firstRun && error);

    var key = Console.ReadKey(true);
    switch (key.KeyChar)
    {
        case '0':
            shopOpen = false;
            break;
        case '1':
            AllVehiclesMenu();
            break;
        case '2':
            SearchByYearMenu();
            break;
        case '3':
            SearchByModelMenu();
            break;
        case '4':
            SearchByEngineMenu();
            break;
        case '5':
            AddVehicleMenu();
            break;
        case '6':
            EditVehicleMenu();
            break;
        case '7':
            DeleteVehicleMenu();
            break;
        default:
            break;
    }

    firstRun = false;
}

void PrintShopMenu(bool skipConsoleClear)
{
    if (!skipConsoleClear)
    {
        Console.Clear();
    }
    Console.WriteLine("╒══════════════════════════════╕");
    Console.WriteLine("│           CAR SHOP           │");
    Console.WriteLine("├──────────────────────────────┤");
    Console.WriteLine("│ 1. List all vehicles         │");
    Console.WriteLine("│ 2. Search vehicles by year   │");
    Console.WriteLine("│ 3. Search vehicles by model  │");
    Console.WriteLine("│ 4. Search vehicles by engine │");
    Console.WriteLine("│ 5. Add new vehicle           │");
    Console.WriteLine("│ 6. Edit vehicle by ID        │");
    Console.WriteLine("│ 7. Remove vehicle by ID      │");
    Console.WriteLine("│ 0. Close shop                │");
    Console.WriteLine("╘══════════════════════════════╛");
}

void AllVehiclesMenu(string? header = null, List<Car>? cars = null, List<Bike>? bikes = null)
{
    Console.Clear();
    Console.WriteLine("╒══════════════════════════════╕");
    Console.WriteLine("│           CAR SHOP           │");
    Console.WriteLine("├──────────────────────────────┤");
    header ??= "          Vehicles          ";
    Console.WriteLine($"│ {header} │");
    Console.WriteLine("├──────────────────────────────┤");

    cars ??= data.Cars;
    bikes ??= data.Bikes;

    if (cars.Count > 0 || bikes.Count > 0)
    {
        var count = 1;

        if (cars.Count > 0)
        {
            Console.WriteLine("│             Cars             │");
            Console.WriteLine("├┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┘");
            foreach (var car in cars)
            {
                Console.Write($"│ {count++}. ");
                car.PrintMe();
                Console.WriteLine();
            }
            Console.WriteLine("├──────────────────────────────┐");

        }

        if (bikes.Count > 0)
        {
            Console.WriteLine("│             Bikes            │");
            Console.WriteLine("├┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┘");
            foreach (var bike in bikes)
            {
                Console.Write($"│ {count++}. ");
                bike.PrintMe();
                Console.WriteLine();
            }
            Console.WriteLine("├──────────────────────────────┐");
        }
    }
    else
    {
        Console.WriteLine("│         No vehicles          │");
        Console.WriteLine("├──────────────────────────────┤");
    }

    Console.WriteLine("│ 0. Go back                   │");
    Console.WriteLine("╘══════════════════════════════╛");
    char key;
    do
    {
        key = Console.ReadKey(true).KeyChar;
    } while (key != '0');
}

void AddVehicleMenu()
{
    char typeKey;
    do
    {
        Console.Clear();
        Console.WriteLine("╒══════════════════════════════╕");
        Console.WriteLine("│           CAR SHOP           │");
        Console.WriteLine("├──────────────────────────────┤");
        Console.WriteLine("│          Add vehicle         │");
        Console.WriteLine("├──────────────────────────────┤");
        Console.WriteLine("│ 1. Car                       │");
        Console.WriteLine("│ 2. Bike                      │");
        Console.WriteLine("│ 0. Cancel                    │");
        Console.WriteLine("╘══════════════════════════════╛");

        typeKey = Console.ReadKey(true).KeyChar;
        if (typeKey is '1' or '2' or '0')
        {
            break;
        }
    } while (true);
    if (typeKey == '0')
    {
        return;
    }

    var typePrint = typeKey == '1' ? "Car                   " : "Bike                  ";
    string? model = null;
    do
    {
        Console.Clear();
        Console.WriteLine("╒══════════════════════════════╕");
        Console.WriteLine("│           CAR SHOP           │");
        Console.WriteLine("├──────────────────────────────┤");
        Console.WriteLine("│          Add vehicle         │");
        Console.WriteLine("├──────────────────────────────┤");
        Console.WriteLine($"│ Type: {typePrint} │");
        Console.WriteLine("├──────────────────────────────┤");
        Console.WriteLine("│          Enter model         │");
        Console.WriteLine("╘══════════════════════════════╛");

        model = Console.ReadLine();

    } while (string.IsNullOrWhiteSpace(model));

    int year = 0;
    var modelPrint = model.Length > 21 ? $"{model[..20]}…" : model + new String(' ', 21 - model.Length);
    do
    {
        Console.Clear();
        Console.WriteLine("╒══════════════════════════════╕");
        Console.WriteLine("│           CAR SHOP           │");
        Console.WriteLine("├──────────────────────────────┤");
        Console.WriteLine("│          Add vehicle         │");
        Console.WriteLine("├──────────────────────────────┤");
        Console.WriteLine($"│ Type: {typePrint} │");
        Console.WriteLine($"│ Model: {modelPrint} │");
        Console.WriteLine("├──────────────────────────────┤");
        Console.WriteLine("│          Enter year          │");
        Console.WriteLine("╘══════════════════════════════╛");

        var yearInput = Console.ReadLine() ?? string.Empty;
        int.TryParse(yearInput, out year);

    } while (year <= 0);

    double engine = -1;
    var yearString = year.ToString();
    var yearPrint = yearString.Length > 22 ? $"{yearString[..21]}…" : yearString + new String(' ', 22 - yearString.Length);
    do
    {
        Console.Clear();
        Console.WriteLine("╒══════════════════════════════╕");
        Console.WriteLine("│           CAR SHOP           │");
        Console.WriteLine("├──────────────────────────────┤");
        Console.WriteLine("│          Add vehicle         │");
        Console.WriteLine("├──────────────────────────────┤");
        Console.WriteLine($"│ Type: {typePrint} │");
        Console.WriteLine($"│ Model: {modelPrint} │");
        Console.WriteLine($"│ Year: {yearPrint} │");
        Console.WriteLine("├──────────────────────────────┤");
        Console.WriteLine("│     Enter engine capacity    │");
        Console.WriteLine("╘══════════════════════════════╛");

        var engineInput = Console.ReadLine() ?? string.Empty;
        if (!double.TryParse(engineInput, out engine))
        {
            engine = -1;
        }

    } while (engine < 0);

    if (typeKey == '1')
    {
        var car = new Car(engine, model, year);
        data.Cars.Add(car);
    }
    else
    {
        var bike = new Bike(engine, model, year);
        data.Bikes.Add(bike);
    }
    Console.Clear();
    SaveData();

    var engineString = engine.ToString();
    var enginePrint = engineString.Length > 20 ? $"{engineString[..19]}…" : engineString + new String(' ', 20 - engineString.Length);
    var id = typeKey == '1' ? data.Cars.Count : data.Cars.Count + data.Bikes.Count;
    var idString = id.ToString();
    var idPrint = idString.Length > 24 ? $"{idString[..23]}…" : idString + new String(' ', 24 - idString.Length);

    Console.WriteLine("╒══════════════════════════════╕");
    Console.WriteLine("│           CAR SHOP           │");
    Console.WriteLine("├──────────────────────────────┤");
    Console.WriteLine("│         Vehicle added        │");
    Console.WriteLine("├──────────────────────────────┤");
    Console.WriteLine($"│ ID: {idPrint} │");
    Console.WriteLine($"│ Type: {typePrint} │");
    Console.WriteLine($"│ Model: {modelPrint} │");
    Console.WriteLine($"│ Year: {yearPrint} │");
    Console.WriteLine($"│ Engine: {enginePrint} │");
    Console.WriteLine("├──────────────────────────────┤");
    Console.WriteLine("│ 0. Go back                   │");
    Console.WriteLine("╘══════════════════════════════╛");

    char key;
    do
    {
        key = Console.ReadKey(true).KeyChar;
    } while (key != '0');
}

void DeleteVehicleMenu()
{
    var finish = false;

    do
    {
        int id = -1;
        Console.Clear();
        Console.WriteLine("╒══════════════════════════════╕");
        Console.WriteLine("│           CAR SHOP           │");
        Console.WriteLine("├──────────────────────────────┤");
        Console.WriteLine("│         Delete vehicle       │");
        Console.WriteLine("├──────────────────────────────┤");
        Console.WriteLine("│           Enter ID           │");
        Console.WriteLine("│        or 0 to go back       │");
        Console.WriteLine("╘══════════════════════════════╛");

        var idInput = Console.ReadLine() ?? string.Empty;
        if (!int.TryParse(idInput, out id))
        {
            id = -1;
        }

        if (id == 0)
        {
            return;
        }

        Vehicle? vehicle = null;
        Console.Clear();
        Console.WriteLine("╒══════════════════════════════╕");
        Console.WriteLine("│           CAR SHOP           │");
        Console.WriteLine("├──────────────────────────────┤");
        if (id < 0 || id > data.Cars.Count + data.Bikes.Count)
        {
            Console.WriteLine("│           Invalid ID         │");
            Console.WriteLine("├──────────────────────────────┤");
            Console.WriteLine("│ 0. Retry                     │");
        }
        else
        {
            vehicle = id <= data.Cars.Count ? data.Cars[id - 1] : data.Bikes[id - data.Cars.Count - 1];
            Console.WriteLine("│         Confirm delete       │");
            Console.WriteLine("├┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┘");
            Console.Write("│ ");
            vehicle.PrintMe();
            Console.WriteLine();
            Console.WriteLine("├──────────────────────────────┐");
            Console.WriteLine("│ 1. Yes                       │");
            Console.WriteLine("│ 2. No                        │");
        }

        Console.WriteLine("╘══════════════════════════════╛");

        char confirmKey;
        do
        {
            confirmKey = Console.ReadKey(true).KeyChar;
            if (vehicle != null)
            {
                if (confirmKey != '1')
                {
                    break;
                }
                if (vehicle is Car car)
                {
                    data.Cars.Remove(car);
                }
                else if (vehicle is Bike bike)
                {
                    data.Bikes.Remove(bike);
                }
                vehicle = null;
                SaveData();
                do
                {
                    Console.WriteLine("╒══════════════════════════════╕");
                    Console.WriteLine("│           CAR SHOP           │");
                    Console.WriteLine("├──────────────────────────────┤");
                    Console.WriteLine("│        Vehicle removed       │");
                    Console.WriteLine("├──────────────────────────────┤");
                    Console.WriteLine("│ 0. Go back                   │");
                    Console.WriteLine("╘══════════════════════════════╛");

                    confirmKey = Console.ReadKey(true).KeyChar;
                    Console.Clear();
                    finish = true;
                } while (confirmKey != '0');
            }
        } while (confirmKey != '0');
    } while (!finish);
}

void SearchByYearMenu()
{
    int year = -1;

    do
    {
        Console.Clear();
        Console.WriteLine("╒══════════════════════════════╕");
        Console.WriteLine("│           CAR SHOP           │");
        Console.WriteLine("├──────────────────────────────┤");
        Console.WriteLine("│        Search vehicles       │");
        Console.WriteLine("├──────────────────────────────┤");
        Console.WriteLine("│          Enter Year          │");
        Console.WriteLine("│        or 0 to go back       │");
        Console.WriteLine("╘══════════════════════════════╛");

        var yearInput = Console.ReadLine() ?? string.Empty;
        if (!int.TryParse(yearInput, out year))
        {
            year = -1;
        }
    } while (year < 0);

    if (year == 0)
    {
        return;
    }

    var cars = data.Cars.Where(c => c.Year == year).ToList();
    var bikes = data.Bikes.Where(b => b.Year == year).ToList();

    AllVehiclesMenu("    Search results (year)   ", cars, bikes);
}

void SearchByModelMenu()
{
    string? model = null;

    do
    {
        Console.Clear();
        Console.WriteLine("╒══════════════════════════════╕");
        Console.WriteLine("│           CAR SHOP           │");
        Console.WriteLine("├──────────────────────────────┤");
        Console.WriteLine("│        Search vehicles       │");
        Console.WriteLine("├──────────────────────────────┤");
        Console.WriteLine("│          Enter Model         │");
        Console.WriteLine("│        or 0 to go back       │");
        Console.WriteLine("╘══════════════════════════════╛");

        model = Console.ReadLine() ?? string.Empty;
    } while (string.IsNullOrWhiteSpace(model));

    if (model.Trim() == "0")
    {
        return;
    }

    var cars = data.Cars.Where(c => c.Model.Contains(model, StringComparison.OrdinalIgnoreCase)).ToList();
    var bikes = data.Bikes.Where(b => b.Model.Contains(model, StringComparison.OrdinalIgnoreCase)).ToList();

    AllVehiclesMenu("    Search results (model)  ", cars, bikes);
}

void SearchByEngineMenu()
{
    double engine = -1;
    var finish = false;

    do
    {
        Console.Clear();
        Console.WriteLine("╒══════════════════════════════╕");
        Console.WriteLine("│           CAR SHOP           │");
        Console.WriteLine("├──────────────────────────────┤");
        Console.WriteLine("│        Search vehicles       │");
        Console.WriteLine("├──────────────────────────────┤");
        Console.WriteLine("│     Enter engine capacity    │");
        Console.WriteLine("│        or q to go back       │");
        Console.WriteLine("╘══════════════════════════════╛");

        var engineInput = Console.ReadLine() ?? string.Empty;
        if (engineInput.Trim() == "q")
        {
            finish = true;
            break;
        }
        if (!double.TryParse(engineInput, out engine))
        {
            engine = -1;
        }
    } while (engine < 0);

    if (finish)
    {
        return;
    }

    var cars = data.Cars.Where(c => c.EngineCapacity == engine).ToList();
    var bikes = data.Bikes.Where(b => b.EngineCapacity == engine).ToList();

    AllVehiclesMenu("   Search results (engine)  ", cars, bikes);
}

void EditVehicleMenu()
{
    var finish = false;

    do
    {
        int id = -1;
        Console.Clear();
        Console.WriteLine("╒══════════════════════════════╕");
        Console.WriteLine("│           CAR SHOP           │");
        Console.WriteLine("├──────────────────────────────┤");
        Console.WriteLine("│         Edit vehicle         │");
        Console.WriteLine("├──────────────────────────────┤");
        Console.WriteLine("│           Enter ID           │");
        Console.WriteLine("│        or 0 to go back       │");
        Console.WriteLine("╘══════════════════════════════╛");

        var idInput = Console.ReadLine() ?? string.Empty;
        if (!int.TryParse(idInput, out id))
        {
            id = -1;
        }

        if (id == 0)
        {
            return;
        }

        Vehicle? vehicle = null;
        Console.Clear();
        Console.WriteLine("╒══════════════════════════════╕");
        Console.WriteLine("│           CAR SHOP           │");
        Console.WriteLine("├──────────────────────────────┤");

        if (id < 0 || id > data.Cars.Count + data.Bikes.Count)
        {
            Console.WriteLine("│           Invalid ID         │");
            Console.WriteLine("├──────────────────────────────┤");
            Console.WriteLine("│ 0. Retry                     │");
            Console.WriteLine("╘══════════════════════════════╛");

            char retryKey;
            do
            {
                retryKey = Console.ReadKey(true).KeyChar;
            } while (retryKey != '0');
            continue;
        }

        vehicle = id <= data.Cars.Count ? data.Cars[id - 1] : data.Bikes[id - data.Cars.Count - 1];

        Console.WriteLine("│       Select field to edit   │");
        Console.WriteLine("├┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┘");
        Console.Write("│ ");
        vehicle.PrintMe();
        Console.WriteLine();
        Console.WriteLine("├──────────────────────────────┐");
        Console.WriteLine("│ 1. Model                     │");
        Console.WriteLine("│ 2. Year                      │");
        Console.WriteLine("│ 3. Engine capacity           │");
        Console.WriteLine("│ 0. Cancel                    │");
        Console.WriteLine("╘══════════════════════════════╛");

        char fieldKey;
        do
        {
            fieldKey = Console.ReadKey(true).KeyChar;
        } while (fieldKey is not ('0' or '1' or '2' or '3'));

        if (fieldKey == '0')
        {
            return;
        }

        switch (fieldKey)
        {
            case '1':
                EditModel(vehicle);
                break;
            case '2':
                EditYear(vehicle);
                break;
            case '3':
                EditEngine(vehicle);
                break;
        }

        SaveData();

        Console.Clear();
        Console.WriteLine("╒══════════════════════════════╕");
        Console.WriteLine("│           CAR SHOP           │");
        Console.WriteLine("├──────────────────────────────┤");
        Console.WriteLine("│        Vehicle updated       │");
        Console.WriteLine("├┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┘");
        Console.Write("│ ");
        vehicle.PrintMe();
        Console.WriteLine();
        Console.WriteLine("├──────────────────────────────┐");
        Console.WriteLine("│ 0. Go back                   │");
        Console.WriteLine("╘══════════════════════════════╛");

        char confirmKey;
        do
        {
            confirmKey = Console.ReadKey(true).KeyChar;
        } while (confirmKey != '0');

        finish = true;

    } while (!finish);
}

void EditModel(Vehicle vehicle)
{
    string? newModel = null;

    do
    {
        Console.Clear();
        Console.WriteLine("╒══════════════════════════════╕");
        Console.WriteLine("│           CAR SHOP           │");
        Console.WriteLine("├──────────────────────────────┤");
        Console.WriteLine("│         Edit vehicle         │");
        Console.WriteLine("├──────────────────────────────┘");
        Console.WriteLine($"│ Current model: {vehicle.Model}");
        Console.WriteLine("├──────────────────────────────┐");
        Console.WriteLine("│       Enter new model        │");
        Console.WriteLine("╘══════════════════════════════╛");

        newModel = Console.ReadLine();

    } while (string.IsNullOrWhiteSpace(newModel));

    vehicle.Model = newModel;
}

void EditYear(Vehicle vehicle)
{
    int newYear = 0;

    do
    {
        Console.Clear();
        Console.WriteLine("╒══════════════════════════════╕");
        Console.WriteLine("│           CAR SHOP           │");
        Console.WriteLine("├──────────────────────────────┤");
        Console.WriteLine("│         Edit vehicle         │");
        Console.WriteLine("├──────────────────────────────┘");
        Console.WriteLine($"│ Current year: {vehicle.Year}");
        Console.WriteLine("├──────────────────────────────┐");
        Console.WriteLine("│        Enter new year        │");
        Console.WriteLine("╘══════════════════════════════╛");

        var yearInput = Console.ReadLine() ?? string.Empty;
        int.TryParse(yearInput, out newYear);

    } while (newYear <= 0);

    vehicle.Year = newYear;
}

void EditEngine(Vehicle vehicle)
{
    double newEngine = -1;

    do
    {
        Console.Clear();
        Console.WriteLine("╒══════════════════════════════╕");
        Console.WriteLine("│           CAR SHOP           │");
        Console.WriteLine("├──────────────────────────────┤");
        Console.WriteLine("│         Edit vehicle         │");
        Console.WriteLine("├──────────────────────────────┘");
        Console.WriteLine($"│ Current engine: {vehicle.EngineCapacity}");
        Console.WriteLine("├──────────────────────────────┐");
        Console.WriteLine("│  Enter new engine capacity   │");
        Console.WriteLine("╘══════════════════════════════╛");

        var engineInput = Console.ReadLine() ?? string.Empty;
        if (!double.TryParse(engineInput, out newEngine))
        {
            newEngine = -1;
        }

    } while (newEngine < 0);

    vehicle.EngineCapacity = newEngine;
}

void SaveData()
{
    try
    {
        var dataToWrite = JsonSerializer.Serialize(data);
        File.WriteAllText(dataFilePath, dataToWrite);
    }
    catch (Exception ex)
    {
        Console.Error.WriteLine($"Error while saving file {dataFilePath}: {ex.ToString()}");
    }
}

class Data
{
    [JsonInclude]
    public List<Car> Cars { get; private set; } = new();

    [JsonInclude]
    public List<Bike> Bikes { get; private set; } = new();
}
