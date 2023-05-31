using AvaloniaApplicationPracticeOne.Models;

namespace AvaloniaApplicationPracticeOne;

public class Service
{
    // Переменная хранит экземпляр контекста
    private static SaneyaskinContext? _db;

    // Метод, если экземпляр еще не создан, создает и возвращает его
    // Если экземпляр создан, возвращает его
    public static SaneyaskinContext  GetDbContext()
    {
        if (_db == null)
        {
            _db = new SaneyaskinContext();
        }
        return _db;
    }
}