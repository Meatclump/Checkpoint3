/*
Todo:
Functionality for creating Assets
Functionality for viewing Assets

Maybe:
Functionality for viewing and setting currency conversion rates
Functionality for viewing and creating offices
*/

using AssetTracking;

List<string> menuOptions = [
        "Add New Asset",
        "View All Assets",
        "Create Asset Categories",
        "Exit Program"
    ];

List<Office> offices = new([
        new Office("001", "New York", "USD"),
        new Office("002", "London", "GBP"),
        new Office("003", "Sweden", "SEK"),
        new Office("004", "Germany", "EUR"),
    ]);

List <Currency> currencies = new([
        new Currency("USD", "US Dollar", 1.0),
        new Currency("GBP", "British Pound", 1.25),
        new Currency("SEK", "Swedish Krona", 0.11),
        new Currency("EUR", "Euro", 1.10),
    ]);

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
    Controller.ShowMainMenu(menuOptions);
    int selection = Controller.GetMainMenuSelection();

    switch (selection)
    {
        case 1:
            // Add New Asset
            Controller.ShowCreateAssetMenu();
            bool addingAssets = true;
            while (addingAssets)
            {
                Asset result = Controller.GetAssetInput(offices);
                if (result == null)
                {
                    addingAssets = false;
                    continue;
                }
                Controller.AddAssetToCategory(result, assetCategories);
            }
            break;
        case 2:
            // View All Assets
            Controller.ShowAssetsMenu(assetCategories, offices, currencies);
            break;
        case 3:
            // Create Asset Category
            Controller.ShowCategoriesMenu(assetCategories);
            bool addingCategories = true;
            while (addingCategories)
            {
                AssetCategory result = Controller.GetCategoryInput();
                if (result == null)
                {
                    addingCategories = false;
                    continue;
                }
                assetCategories.Add(result);
                Console.WriteLine($"Category \"{result.Name}\" added successfully!");
                Controller.ShowCategoriesMenu(assetCategories);
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