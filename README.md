# 🍪 Cookie Cookbook - C# Console Application

Welcome to the **Cookie Cookbook**, a console application built with C#. This project allows users to create, store, and manage cookie recipes by selecting from a list of ingredients. The recipes can be saved in either `.txt` or `.json` formats, making it easy to load and view them later.

## Features

- **Ingredient Selection**: Choose from a variety of ingredients such as Wheat Flour, Butter, and Sugar.
- **Recipe Creation**: Combine ingredients to create your custom cookie recipes.
- **Recipe Storage**: Save recipes in `.txt` or `.json` format. The format can be switched by adjusting a constant in the code.
- **Recipe Management**: View all existing recipes upon startup and add new ones to your cookbook.

## Ingredients

Each ingredient is identified by a unique ID, name, and preparation instruction. For example:

| ID  | Name            | Preparation Instruction                             |
| --- | --------------- | --------------------------------------------------- |
| 1   | Wheat flour      | Sieve. Add to other ingredients.                    |
| 2   | Butter           | Melt on low heat. Add to other ingredients.         |
| 3   | Chocolate        | Melt in a water bath. Add to other ingredients.     |
| 4   | Sugar            | Add to other ingredients.                          |

## How it Works

### Main Workflow

1. **Viewing Existing Recipes**: If recipes are already saved, they will be loaded from the file and displayed.
2. **Creating a New Recipe**:
   - The app shows a list of available ingredients.
   - You can select ingredients by entering their ID.
   - Once done, the recipe will be saved in the specified format (`.txt` or `.json`).
3. **Storing Recipes**: Recipes are stored as a collection of ingredient IDs, either in a text or JSON file, making it easy to retrieve them later.

### Example

**Recipe**:
- Ingredients: Wheat flour, Butter, Sugar

**Saved in `recipes.txt`**:
```
1,2,3
```

**Saved in `recipes.json`**:
```json
["1,2,3"]
```

## Getting Started

### Prerequisites

- .NET SDK
- A code editor (e.g., Visual Studio, Visual Studio Code)

### Running the Application

1. Clone the repository:
   ```bash
   git clone https://github.com/your-username/cookie-cookbook.git
   cd cookie-cookbook
   ```
2. Build and run the application:
   ```bash
   dotnet run
   ```

3. Follow the on-screen prompts to create a new recipe or view existing ones.

### Changing the File Format (Not yet implimented)

To switch between saving recipes in `.txt` or `.json`, modify the `FileFormat` constant in the program code:
```csharp
const FileFormat = FileFormat.JSON;  // Change to .TXT if desired
```

## Contributing

Feel free to fork the repository and submit pull requests if you'd like to improve or add features to the Cookie Cookbook. Contributions are welcome!

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.

## Acknowledgments

This project was created as part of the **Ultimate C# Masterclass Assignment**.
