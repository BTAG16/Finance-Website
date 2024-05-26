# Finance-Website

## Overview

The Finance Website is a web application designed to help you manage your finances by tracking your income, expenses, and investment portfolio. It is built using WebSharper, a web development framework for F#.

## Live Demo

- You can preview the Website [here](https://finances.azurewebsites.net/)

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
- ![Screenshot 2024-05-26 110655](https://github.com/BTAG16/Finance-Website/assets/128963075/a7f4920d-546e-4628-8b23-e6189a7f8802)
- ![Screenshot 2024-05-26 110717](https://github.com/BTAG16/Finance-Website/assets/128963075/fe91f343-9bc3-417a-a8f3-124aab779957)
- ![Screenshot 2024-05-26 110728](https://github.com/BTAG16/Finance-Website/assets/128963075/fbbf94e7-8735-4746-ab74-22d83e706882)
- ![Screenshot 2024-05-26 110751](https://github.com/BTAG16/Finance-Website/assets/128963075/9a5907bb-5796-41a1-8ef0-dfab06be1a14)
- ![Screenshot 2024-05-26 110744](https://github.com/BTAG16/Finance-Website/assets/128963075/f1d86bb2-3e87-4ad3-b97f-9e0530149e21)

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
- **Client.fs**: The main F# file that defines the application endpoints and logic.
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

## Contact

For more information, visit the documentation or contact the project maintainers at [WebSharper](https://websharper.com/)

## Author

[Cosmos Junior Ighoraye](https://github.com/BTAG16)
