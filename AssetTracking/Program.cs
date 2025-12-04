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

List<AssetCategory> assetCategories = new(
    [
        new AssetCategory("Laptop Computers", []),
        new AssetCategory("Mobile Phones", []),
    ]
);

bool runProgram = true;
while (runProgram)
{
    UIManager.ShowMainMenu(menuOptions);
    int selection = UIManager.GetMainMenuSelection();

    switch (selection)
    {
        case 1:
            // Add New Asset
            Console.WriteLine("\nAdding a new asset...");
            break;
        case 2:
            // View All Assets
            Console.WriteLine("\nViewing all assets...");
            break;
        case 3:
            // Create Asset Category
            UIManager.ShowCategoriesMenu(assetCategories);
            bool addingCategories = true;
            while (addingCategories)
            {
                AssetCategory result = UIManager.GetCategoryInput();
                if (result == null)
                {
                    addingCategories = false;
                    continue;
                }
                assetCategories.Add(result);
                Console.WriteLine($"Category \"{result.Name}\" added successfully!");
                UIManager.ShowCategoriesMenu(assetCategories);
            }
            Console.Clear();
            break;
        case 4:
            Console.WriteLine("\nExiting the program. Have a nice day!");
            runProgram = false;
            break;
        default:
            Console.WriteLine("\nUnrecognized menu selection.");
            break;
    }
}