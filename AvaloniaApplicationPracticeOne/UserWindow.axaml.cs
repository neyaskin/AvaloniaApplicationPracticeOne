using System.Linq;
using System.Net.Mime;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Microsoft.EntityFrameworkCore;

namespace AvaloniaApplicationPracticeOne;

public partial class UserWindow : Window
{
    private DataGrid usersDGrid;
    private TextBox searchTBox;
    public UserWindow()
    {
        InitializeComponent();

        usersDGrid = this.FindControl<DataGrid>("UsersDGrid");
        searchTBox = this.FindControl<TextBox>("SearchTBox");
        
        // Передаем данные в DataGrid
        usersDGrid.Items = Service.GetDbContext().Users.Include(q=>q.IdRoleNavigation).ToList();
        
#if DEBUG
        this.AttachDevTools();
#endif
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    // Данный метод срабатывает, когда пользователь нажмет на кнопку Выйти 
    private void LogOutBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        new MainWindow().Show();
        Close();
    }

    // Данный метод срабатывает, когда пользователь нажимает на кнопку Найти
    private void SearchBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        // Проверяем, есть ли данные в поле для ввода
        // Если поле пустое, выводим список всех пользователей
        if (string.IsNullOrWhiteSpace(searchTBox.Text))
        {
            usersDGrid.Items = Service.GetDbContext().Users.Include(q=>q.IdRoleNavigation).ToList();
        }
        // Если в поле есть данные, фильтруем массив данных, ищем совпадения
        else
        {
            usersDGrid.Items = Service.GetDbContext().Users
                .Where(q => q.Login.ToLower().Contains(searchTBox.Text.ToLower()) 
                            || q.Name.ToLower().Contains(searchTBox.Text.ToLower()) 
                            || q.Surname.ToLower().Contains(searchTBox.Text.ToLower())).ToList();   
        }
    }
}