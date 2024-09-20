using System;
using System.Text.Json;
using Cookies_Cookbook.Helpers;

namespace CookieCookbook
{
    public class Program
    {
        private const string RecipesFilePath = "/Users/kevin.godinho/RiderProjects/CookiesCookbook/CookiesCookbook/recipes.json";
        private const string IngredientsFilePath = "/Users/kevin.godinho/RiderProjects/CookiesCookbook/CookiesCookbook/ingredients.json";
        
        public class Ingredient
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Instructions { get; set; }
        }
        
        public class IngredientList
        {
            public List<Ingredient> Ingredients { get; set; }
        }

        static void Main()
        {
            PrintRecipes(IngredientsFilePath, RecipesFilePath);
            PrintIngredients(IngredientsFilePath);
            AddRecipe(SelectIngredients(IngredientsFilePath), RecipesFilePath);
            PrintLastAddedRecipe(IngredientsFilePath, RecipesFilePath);
            ExitProgram();
        }

        private static void PrintRecipes(string ingredientsFilePath, string recipesFilePath)
        {
            if (!Helpers.FileExists(recipesFilePath))
            {
                Helpers.PrintNoRecipeMessage();
                return;
            }
            
            string fileContent = File.ReadAllText(recipesFilePath);
            
            if (Helpers.IsFileContentEmpty(fileContent))
            {
                Helpers.PrintNoRecipeMessage();
            }
            else
            {
                Helpers.PrintExistingRecipeMessage(ingredientsFilePath, recipesFilePath);
            }
        }

        static void PrintIngredients(string filePath)
        {
            string jsonString = Helpers.ReadFileContent(filePath);
            IngredientList ingredientList = Helpers.DeserializeIngredientList(jsonString);
            
            Console.WriteLine("Create a new cookie recipe! Available ingredients are:");
            Helpers.DisplayIngredients(ingredientList);
        }

        private static string SelectIngredients(string filePath)
        {
            List<int> ingredientIds = Helpers.GetListOfIngredientIds(filePath);
            List<int> selectedIngredients = Helpers.GetValidIngredientSelections(ingredientIds);
            
            if (selectedIngredients.Count == 0)
            {
                Console.WriteLine("No ingredients have been selected. Recipe will not be saved.");
            }

            return Helpers.FormatSelectedIngredients(selectedIngredients);
        }

        static void AddRecipe(string recipe, string filePath)
        {
            List<string> recipeList;

            if (!File.Exists(filePath))
            {
                Console.WriteLine("No recipes found. Creating a new recipe list.");
                recipeList = new List<string>();
            }
            else
            {
                string jsonString = Helpers.ReadFileContent(filePath);
                recipeList = Helpers.DeserializeRecipeList(jsonString);
            }

            recipeList.Add(recipe);

            string updatedJsonString = Helpers.SerializeRecipe(recipeList);

            Helpers.SaveRecipe(filePath, updatedJsonString);
        }

        static void PrintLastAddedRecipe(string ingredientsFilePath, string recipesFilePath)
        {
            Console.WriteLine("Recipe added:");
            
            List<string> recipeList = Helpers.DeserializeRecipeList(Helpers.ReadFileContent(recipesFilePath));
            IngredientList ingredientList = Helpers.DeserializeIngredientList(Helpers.ReadFileContent(ingredientsFilePath));
        
            Dictionary<int, Program.Ingredient> ingredientDict = ingredientList.Ingredients.ToDictionary(ing => ing.Id);

            int lastElementInList = recipeList.Count - 1; 
            
            List<int> ingredientIds = recipeList[lastElementInList].Split(',').Select(int.Parse).ToList();

            foreach (int id in ingredientIds)
            {
                if (ingredientDict.TryGetValue(id, out Program.Ingredient ingredient))
                {
                    Console.WriteLine($"{ingredient.Name}. {ingredient.Instructions}");
                }
            }
            Console.WriteLine();
        }

        static void ExitProgram()
        {
            Console.WriteLine("Press any key to exit.");
        }
    }
}