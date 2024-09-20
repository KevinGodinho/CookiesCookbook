using System.Text.Json;
using CookieCookbook;

namespace Cookies_Cookbook.Helpers;

public class Helpers
{
    public static void DisplayIngredients(Program.IngredientList ingredientList)
    {
        foreach (var ingredient in ingredientList.Ingredients)
        {
            Console.WriteLine($"{ingredient.Id}. {ingredient.Name}");
        }
    }

    public static List<int> GetListOfIngredientIds(string filePath)
    {
        var ingredientIds = new List<int>();

        string jsonString = File.ReadAllText(filePath);
        Program.IngredientList ingredientList = JsonSerializer.Deserialize<Program.IngredientList>(jsonString);

        foreach (var ingredient in ingredientList.Ingredients)
        {
            ingredientIds.Add(ingredient.Id);
        }

        return ingredientIds;
    }

    public static List<int> GetValidIngredientSelections(List<int> ingredientIds)
    {
        List<int> selectedIngredients = new List<int>();

        while (true)
        {
            Console.WriteLine("Add an ingredient by its ID or type anything else if finished.");
            string userIngredientSelection = Console.ReadLine();

            if (!int.TryParse(userIngredientSelection, out int id))
            {
                break;
            }

            if (!ingredientIds.Contains(id))
            {
                Console.WriteLine("Invalid ingredient ID. Try again.");
                continue;
            }

            selectedIngredients.Add(id);
        }

        return selectedIngredients;
    }

    public static string FormatSelectedIngredients(List<int> selectedIngredients)
    {
        return string.Join(", ", selectedIngredients);
    }

    public static void PrintNoRecipeMessage()
    {
        Console.WriteLine("No recipe is saved.");
    }

    public static void PrintExistingRecipeMessage(string ingredientsFilePath, string recipesFilePath)
    {
        List<string> recipeList = DeserializeRecipeList(ReadFileContent(recipesFilePath));
        Program.IngredientList ingredientList = DeserializeIngredientList(ReadFileContent(ingredientsFilePath));
        
        Dictionary<int, Program.Ingredient> ingredientDict = ingredientList.Ingredients.ToDictionary(ing => ing.Id);
        
        Console.WriteLine("Existing recipes are:");

        for (int i = 0; i < recipeList.Count; i++)
        {
            Console.WriteLine($"***** {i + 1} *****");
            
            List<int> ingredientIds = recipeList[i].Split(',').Select(int.Parse).ToList();

            foreach (int id in ingredientIds)
            {
                if (ingredientDict.TryGetValue(id, out Program.Ingredient ingredient))
                {
                    Console.WriteLine($"{ingredient.Name}. {ingredient.Instructions}");
                }
            }
            Console.WriteLine();
        }
        
    }

    public static bool FileExists(string filePath)
    {
        return File.Exists(filePath);
    }

    public static bool IsFileContentEmpty(string fileContent)
    {
        return string.IsNullOrWhiteSpace(fileContent);
    }

    public static string ReadFileContent(string filePath)
    {
        if (!FileExists(filePath))
        {
            Console.WriteLine("File not found.");
            return string.Empty;
        }

        return File.ReadAllText(filePath);
    }

    public static Program.IngredientList DeserializeIngredientList(string jsonString)
    {
        try
        {
            return JsonSerializer.Deserialize<Program.IngredientList>(jsonString);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error deserializing JSON: {ex.Message}");
            return null;
        }
    }
    
    public static List<string> DeserializeRecipeList(string jsonString)
    {
        try
        {
            return JsonSerializer.Deserialize<List<string>>(jsonString);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error deserializing JSON: {ex.Message}");
            return null;
        }
    }

    public static string SerializeRecipe(List<string> recipeList)
    {
        return JsonSerializer.Serialize(recipeList, new JsonSerializerOptions { WriteIndented = true });
    }
    
    public static void SaveRecipe(string filePath, string updatedJsonString)
    {
        File.WriteAllText(filePath, updatedJsonString);
    }
}