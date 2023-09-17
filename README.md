# 📟 ConsoleTools 📟

**ConsoleTools** - это библиотека, разработанная для управления приложениями в операциионых системах,
где отсутствует или заблокирован графический интерфейс.

## Использование ⚡
### **[Меню](ConsoleTools/Menu/Menu.cs)**
```csharp
// Создание опций (кнопок) меню
List<Option> options = new List<Option>()
{
  // description является необязательным параметром.
  new Option(name: "Начать игру", description: "Этот пункт меню запустит игру."), 
  new Option("Настройки"),
  new Option("Выход"),
};

// Создание самого меню
Menu mainMenu = new Menu(
  title: "Главное меню",
  options: options,
  position: MenuPosition.Center, // это необязательный параметр 
  );

// Отображение и выбор кнопки
// Листать меню можно с помощью стрелочек на клавиатуре: ⬆️ и ⬇️
// Выбор кнопки по нажатию Enter
// Метод возвращает название кнопки в string
string selectedButton = mainMenu.Show();

if (selectedButton == "Начать игру")
{
  menu.Close(); // Закрыть меню, если это нужно.

  // Здесь ваша обработка нажатой кнопки
}
```
___
### **[Индикатор загрузки](ConsoleTools/ProgressBar/ProgressBar.cs)**
```csharp
const int totalProgress = 10;
ProgressBar progressBar = new ProgressBar(
  total: totalProgress, // Количество шагов. Необязательный параметр, по умолчанию: 100
  barLength: 30, // Длина индикатора. Необязательный параметр, по умолчанию: 40
  barColor: ConsoleColor.Cyan, // Цвет загрузки. Необязательный параметр, по умолчанию: ConsoleColor.Green
  borderColor: ConsoleColor.DarkCyan // Цвет границ. Необязательный параметр, по умолчанию: ConsoleColor.Gray
);

for (int i = 0; i <= totalProgress; i++)
{
  progressBar.Next(i); // Прибавление прогресса.
  progressBar.Draw(); // Отображение индикатора.

  // Отображение выполняемой информации. Необязательный параметр.
  progressBar.SetInformation($"Выполнение задачи №{new Random().Next(1, 100)} .");
  Thread.Sleep(50); // Имитация выполнения задачи.
}
```
