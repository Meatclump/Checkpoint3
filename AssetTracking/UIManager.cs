

namespace AssetTracking
{
    internal class UIManager()
    {
        /// <summary>
        /// Displays the main menu to the console using the provided list of menu options.
        /// </summary>
        /// <remarks>The method writes the menu options to the console.</remarks>
        /// <param name="menuOptions">A list of strings representing the menu options to display. Each option will be shown with a corresponding
        /// number for user selection. Cannot be null.</param>
        public static void ShowMainMenu(List<string> menuOptions)
        {
            Console.WriteLine("Welcome to the Asset Tracking System. Please choose from the available menu options:");
            for (int i = 0; i < menuOptions.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {menuOptions[i]}");
            }
            Console.Write("Select an option: ");
        }

        /// <summary>
        /// Reads user input from the console and returns the selected main menu option as an int.
        /// </summary>
        /// <remarks>The method prompts the user until a numeric input is entered. Non-numeric input
        /// will keep looping until a number is entered.</remarks>
        /// <returns>An integer representing the user's selected main menu option.</returns>
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

        /// <summary>
        /// Prints the asset categories menu
        /// </summary>
        /// <param name="assetCategories"></param>
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

        /// <summary>
        /// Prompts the user to enter a name for a new asset category and returns an instance of <see
        /// cref="AssetCategory"/> with the specified name.
        /// </summary>
        /// <remarks>The method receives user input via console. Entering "q" will cancel
        /// the operation and return to the main menu. The category name must not be null or empty.</remarks>
        /// <returns>An <see cref="AssetCategory"/> object initialized with the entered name, or <see langword="null"/> if the
        /// user chooses to quit.</returns>
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

        /// <summary>
        /// Displays the assets menu for the asset categories.
        /// </summary>
        /// <param name="assetCategories">A list of asset categories to be presented in the assets menu. Cannot be null.</param>
        internal static void ShowAssetsMenu(List<AssetCategory> assetCategories, List<Office> offices, List<Currency> currencies)
        {
            ShowAssetsMenuHeader();
            ShowAssetsMenuBody(assetCategories, offices, currencies);
            Console.WriteLine("\nPress \"Enter\" key to return to main menu.");
            Console.ReadLine();
            Console.Clear();
        }

        /// <summary>
        /// Prints the header section for the assets menu
        /// </summary>
        internal static void ShowAssetsMenuHeader()
        {
            // Define headings with fixed string lengths
            List<string> headings = [
                GetFixedLengthString("Type", 17, " "),
                GetFixedLengthString("Brand", 15, " "),
                GetFixedLengthString("Model", 15, " "),
                GetFixedLengthString("Office", 15, " "),
                GetFixedLengthString("Purchase Date", 14, " "),
                GetFixedLengthString("Price in USD", 13, " "),
                GetFixedLengthString("Currency", 9, " "),
                GetFixedLengthString("Local Price Today", 18, " "),
            ];

            // Print headings and underline them
            Console.Clear();
            foreach (var item in headings)
            {
                Console.Write(item);
            }
            Console.WriteLine();
            foreach (var item in headings)
            {
                Console.Write(GetStringUnderline(item.Length - 1, "-") + " ");
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Displays a formatted list of assets grouped by category to the console, highlighting assets based on their
        /// expiration status.
        /// </summary>
        /// <remarks>Assets are displayed in order by category name and, within each category, by purchase
        /// date. The console output uses color to indicate assets that are near expiration.</remarks>
        /// <param name="assetCategories">A list of asset categories, each containing a collection of assets to be displayed. Cannot be null.</param>
        internal static void ShowAssetsMenuBody(List<AssetCategory> assetCategories, List<Office> offices, List<Currency>currencies)
        {
            foreach (var category in assetCategories.OrderBy(x => x.Name)) // Iterate through categories sorted by name.
            {
                if (category.Assets.Count >= 1)
                {
                    List<Asset> sortedAssets = [.. category.Assets.OrderBy(x => x.PurchaseDate)]; // Iterate through assets within each category, sorted by purchase date.
                    foreach (var asset in category.Assets)
                    {
                        Office office = offices.Find(o => o.Id == asset.OfficeKey); // Find the office for the asset.
                        Currency currency = currencies.Find(c => c.Key == office.CurrencyKey); // Find the currency for the office location.
                        double convertedPrice = asset.Price / currency.ConversionRateToUSD; // Convert asset price to office location currency.
                        DateTime expirationDate = asset.PurchaseDate.AddYears(3); // Calculate the expiration date (3 years from purchase date).
                        SetConsoleColorByExpiration(expirationDate); // Set console color to red or yellow if close to expiration.
                        Console.Write($"{GetFixedLengthString(category.Name, 16, " ")} ");
                        Console.Write($"{GetFixedLengthString(asset.Brand, 14, " ")} ");
                        Console.Write($"{GetFixedLengthString(asset.Model, 14, " ")} ");
                        Console.Write($"{GetFixedLengthString(office.Name, 14, " ")} ");
                        Console.Write($"{GetFixedLengthString(asset.PurchaseDate.ToString("yyyy-MM-dd"), 13, " ")} ");
                        Console.Write($"{GetFixedLengthString(asset.Price.ToString(), 12, " ")} ");
                        Console.Write($"{GetFixedLengthString(currency.Key, 8, " ")} ");
                        Console.Write($"{GetFixedLengthString(convertedPrice.ToString("#.##"), 17, " ")} \n");
                        Console.ResetColor();
                    }
                }
            }
        }

        /// <summary>
        /// Sets the console foreground color based on the proximity of the specified expiration date to the current
        /// date.
        /// </summary>
        /// <remarks>If the expiration date is in the past or within three months, the console color is
        /// set to dark red. If the expiration date is within six months, the color is set to yellow.</remarks>
        /// <param name="expirationDate">The expiration date to evaluate.</param>
        internal static void SetConsoleColorByExpiration(DateTime expirationDate)
        {
            // If the expiration date is in the past, make text red.
            if (DateTime.Now.Year > expirationDate.Year)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
            }
            else if (DateTime.Now.Year == expirationDate.Year) // If the expiration date is this year, further check if we are near expiration.
            {
                int expiresInMonths = expirationDate.Month - DateTime.Now.Month;
                if (expiresInMonths <= 3) // If we are past or less than 3 months from expiration, make text red.
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                }
                else if (expiresInMonths <= 6) // Otherwise, if we are less than 6 months from expiration, make text yellow.
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                }
            }
            else if (expirationDate.Year - DateTime.Now.Year == 1) // If the expiration date is next year, check how far we are from expiration.
            {
                int expiresInMonths = 12 + (expirationDate.Month - DateTime.Now.Month);
                if (expiresInMonths <= 3) // If we are less than 3 months from expiration, make text red.
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                }
                else if (expiresInMonths <= 6) // Otherwise, if we are less than 6 months from expiration, make text yellow.
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                }
            }
        }

        /// <summary>
        /// Utility function for underlining strings
        /// </summary>
        /// <param name="length"></param>
        /// <param name="underlineCharacter"></param>
        /// <returns></returns>
        internal static string GetStringUnderline(int length, string underlineCharacter)
        {
            string result = "";
            for (int i = 0; i < length; i++)
            {
                result += underlineCharacter;
            }
            return result;
        }

        /// <summary>
        /// Utility function for fixing strings to a certain length
        /// </summary>
        /// <param name="input"></param>
        /// <param name="length"></param>
        /// <param name="padCharacter"></param>
        /// <returns></returns>
        internal static string GetFixedLengthString(string input, int length, string padCharacter)
        {
            string result = "";
            if (input.Length > length)
            {
                result = input.Substring(0, length - 3) + "...";
            }
            else
            {
                result = input;
                for (int i = input.Length; i < length; i++)
                {
                    result += padCharacter;
                }
            }
            return result;

        }
    }
}
