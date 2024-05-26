# Finance-Website

## Overview

The Account Manager is a web application designed to help you manage your finances by tracking your income, expenses, and investment portfolio. It is built using WebSharper, a web development framework for F#.

## Features

- **Cash Flow Management**: Track various sources of income and categories of expenses. The application provides a summary of total income, total expenses, and the resulting balance.
- **Investment Portfolio Management**: Add, view, and manage your investment assets. The application calculates the total portfolio value and unrealized profit/loss, and updates the asset prices dynamically.

## Usage

### Navigation

- **Cash Flow**: This section allows you to add and view your income and expenses.
- **Equity Portfolio**: This section allows you to manage your investment portfolio.

### Adding Income

1. **Select Category**: Choose the category of income from the dropdown menu.
2. **Enter Amount**: Input the amount of income.
3. **Add Income**: Click the "Add Income" button to add the income to the list.

### Adding Expenses

1. **Select Category**: Choose the category of expenses from the dropdown menu.
2. **Enter Amount**: Input the amount of expenses.
3. **Add Expenses**: Click the "Add Expenses" button to add the expense to the list.

### Managing Portfolio

1. **Add Asset**: Input the asset name, quantity, and purchase price, then click "Add Asset" to add it to your portfolio.
2. **View Portfolio**: See the total value, unrealized profit/loss, and details of each asset.
3. **Remove Asset**: Click the "Remove" button to delete an asset from your portfolio.

## Screenshots

- <iframe width="560" height="315" src="https://www.youtube.com/embed/HVwBWkvhvCc" frameborder="0" allowfullscreen></iframe>


_Screenshot images will be added here._

## Live Demo

- You can preview the Website [here](https://finances.azurewebsites.net/)

## License

This project is licensed under the MIT License.

## How to Run the Project

1. **Clone the Repository**:

   ```sh
   git clone https://github.com/yourusername/AccountManager.git
   cd AccountManager
   ```

2. **Install Dependencies**:

   ```sh
   # Assuming you have .NET and WebSharper installed
   dotnet restore
   ```

3. **Build the Project**:

   ```sh
   dotnet build
   ```

4. **Run the Project**:

   ```sh
   dotnet run
   ```

5. **Access the Application**:
   Open your web browser and go to http://localhost:5000 (or the port specified in your configuration).

## Project Structure

- **Content/**: Contains CSS and JavaScript files.
- **wwwroot/**: Contains static assets and the HTML templates.
- **Program.fs**: The main F# file that defines the application endpoints and logic.
- **\*.fsproj**: The project file defining dependencies and build configurations.

## Development

To contribute to this project, please fork the repository and create a pull request. For major changes, please open an issue first to discuss what you would like to change.

### Contributing Guidelines

1. **Fork the Repository**
2. **Create a New Branch**:

   ```sh
   git checkout -b feature/your-feature-name
   ```

3. **Commit Your Changes**:

   ```sh
   git commit -m "Add your message here"
   ```

4. **Push to the Branch**:

   ```sh
   git push origin feature/your-feature-name
   ```

5. **Create a Pull Request**

### Contributors

Thank you to all the contributors who have helped in the development of this project.

## Contact

For more information, visit the documentation or contact the project maintainers at email@example.com.

## Author

Cosmos Junior Ighoraye
