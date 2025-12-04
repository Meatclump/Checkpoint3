

namespace AssetTracking
{
    internal class UIManager()
    {

        public static void ShowMainMenu(List<string> menuOptions)
        {
            Console.WriteLine("Welcome to the Asset Tracking System. Please choose from the available menu options:");
            for (int i = 0; i < menuOptions.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {menuOptions[i]}");
            }
            Console.Write("Select an option: ");
        }

        public static int GetMainMenuSelection()
        {
            bool validInput = false;
            int menuOption = 0;
            while (!validInput)
            {
                string selection = Console.ReadLine();
                try
                {
                    menuOption = int.Parse(selection);
                    validInput = true;
                }
                catch
                {
                    Console.Write("Invalid input. Please use a numeric value: ");
                }
            }
            return menuOption;
        }

        internal static void ShowCategoriesMenu(List<AssetCategory> assetCategories)
        {
            Console.Clear();
            Console.WriteLine("Current Asset Categories:");
            Console.WriteLine("--------------------");
            for (int i = 0; i < assetCategories.Count; i++)
            {
                Console.WriteLine($"{i + 1} - {assetCategories[i].Name}");
            }
            Console.WriteLine("--------------------\n");
        }

        internal static AssetCategory GetCategoryInput()
        {
            string name = "";

            bool quit = false;
            bool validName = false;
            Console.Write("Please enter a name for the new category or \"q\" to return to the main menu: ");

            while (!validName)
            {
                name = Console.ReadLine();
                if (name == null)
                {
                    Console.Write("Category name must not be null. Please try again: ");
                    continue;
                }
                if (name.Trim() == "")
                {
                    Console.Write("Category name must not be empty. Please try again: ");
                    continue;
                }
                if (string.Equals(name, "q"))
                {
                    quit = true;
                }
                validName = true;
            }

            if (quit)
            {

                return null;
            }

            return new AssetCategory(name, []);
        }
    }
}
