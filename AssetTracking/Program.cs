using AssetTracking;

// Define available main menu options
List<string> menuOptions = [
        "Add New Asset",
        "View All Assets",
        "Create Asset Categories",
        "Exit Program"
    ];

// Define available offices and their currency key
List<Office> offices = new([
        new Office("001", "New York", "USD"),
        new Office("002", "London", "GBP"),
        new Office("003", "Sweden", "SEK"),
        new Office("004", "Germany", "EUR"),
    ]);

// Define available currencies and conversion rates
List<Currency> currencies = new([
        new Currency("USD", "US Dollar", 1.0),
        new Currency("GBP", "British Pound", 1.25),
        new Currency("SEK", "Swedish Krona", 0.11),
        new Currency("EUR", "Euro", 1.10),
    ]);

// Define some initial asset categories and assets
List<AssetCategory> assetCategories = new(
    [
        new AssetCategory("Mobile Phones", [
            new Asset("Apple", "iPhone 13", 799.99, new DateTime(2021, 9, 24), "001"),
            new Asset("Samsung", "Galaxy S21", 699.99, new DateTime(2025, 1, 29), "003")
        ]),
        new AssetCategory("Laptop Computers", [
            new Asset("Dell", "XPS 13", 999.99, new DateTime(2020, 11, 15), "002"),
            new Asset("Apple", "MacBook Pro", 1299.99, new DateTime(2026, 5, 21), "001"),
            new Asset("Lenovo", "ThinkPad X1 Carbon", 1199.99, new DateTime(2022, 3, 10), "004"),
            new Asset("HP", "Spectre x360", 1099.99, new DateTime(2023, 01, 5), "003"),
            new Asset("Asus", "ZenBook 14", 899.99, new DateTime(2023, 4, 18), "002"),
        ]),
    ]
);

bool runProgram = true;
while (runProgram)
{
    Controller.ShowMainMenu(menuOptions); // Main menu printout
    int selection = Controller.GetMainMenuSelection(); // Main menu selection

    switch (selection)
    {
        case 1:
            // Add New Asset
            Controller.ShowCreateAssetMenu(); // Create asset menu printout
            bool addingAssets = true;
            while (addingAssets) // Loop to add multiple assets
            {
                Asset result = Controller.GetAssetInput(offices);
                if (result == null) // Exit asset creation on null return
                {
                    addingAssets = false;
                    continue;
                }
                Controller.AddAssetToCategory(result, assetCategories); // Add asset to chosen category
            }
            Console.Clear();
            break;
        case 2:
            // View All Assets
            Controller.ShowAssetsMenu(assetCategories, offices, currencies); // View assets menu printout
            break;
        case 3:
            // Create Asset Category
            Controller.ShowCategoriesMenu(assetCategories); // View current categories printout
            bool addingCategories = true;
            while (addingCategories) // Loop to add multiple categories
            {
                AssetCategory result = Controller.GetCategoryInput();
                if (result == null) // Exit category creation on null return
                {
                    addingCategories = false;
                    continue;
                }
                assetCategories.Add(result);
                Console.WriteLine($"Category \"{result.Name}\" added successfully!");
                Controller.ShowCategoriesMenu(assetCategories); // Show updated categories
            }
            Console.Clear();
            break;
        case 4:
            // Exit Program
            Controller.ShowExitMessage();
            runProgram = false;
            break;
        default:
            // A menu option out of range was chosen
            Console.WriteLine("\nUnrecognized menu selection.");
            break;
    }
}