using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace lab2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            bool continueProgram = true; // флаг, пока true — программа работает

            while (continueProgram)
            {
                Console.Clear();
                CenterText("РОБОТА ЗІ СПИСКАМИ ЧИСЕЛ", 1);
                TypeText("\n1. Знайти індекс першого від'ємного числа (double)");
                TypeText("\n2. Розділити список цілих чисел на додатні та від'ємні");
                TypeText("\n0. Вихід");
                TypeText("\nВаш вибір: ");

                string choice = Console.ReadLine()?.Trim(); // прочитываем, что написал пользователь,
                                                        //убраем лишние пробелы, и если вдруг null
                                                       //(пользователь ничего не ввел) — не даем программе упасть

                switch (choice)
                {
                    case "1":
                        Task1();
                        break;

                    case "2":
                        Task2();
                        break;

                    case "0":
                        continueProgram = false;
                        TypeText("\nДо побачення!");
                        break;

                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nНевірний вибір.\n");
                        Console.ResetColor();
                        Console.ReadLine();
                        break;
                }

                if (continueProgram)
                {
                    TypeText("\nНатисніть Enter, щоб повернутися до меню.");
                    Console.ReadLine();
                }
            }
        }

        static void Task1()
        {
            TypeText("\nЗавдання 1: Індекс першого від'ємного числа\n");

            // Список вещественных чисел 
            List<double> numbers = ListOfDoubles();

            // Проверка если пользователь нычего не ввел
            if (numbers.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nСписок порожній.\n");
                Console.ResetColor();
                return;
            }

            // Ищем индекс первого отрицательного элемента
            int index = -1; // Индекс -1 означает, что не нашли 
            
            for (int i = 0; i < numbers.Count; i++)  // .Count своцство которое есть у любого объекта списка
            {
                if (numbers[i] < 0)  // Если число отрицательное
                {
                    index = i;     // запоминаем его позицию
                    break;
                }
            }

            // Выводим результат взависимости от того, нашли отрицательное или нет
            
            if (index != -1)
            {
                TypeText($"\nПерше від'ємне число знайдено на індексі: {index} (значення: {numbers[index]})");
            }

            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nВід'ємних чисел у списку немає.\n");
                Console.ResetColor();
            }
        }

        static void Task2()
        {
            TypeText("\nЗавдання 2: Розділення списку на додатні та від'ємні числа\n");

            // Список целых чисел
            List<int> numbers = ListOfIntegers();

            // Проверка на пустой список
            if (numbers.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nСписок порожній.\n");
                Console.ResetColor();
                return;
            }

            // Создаем два пустых списка для результатов
            List<int> positive = new List<int>();
            List<int> negative = new List<int>();

            // Проходим по всем числам спискам
            foreach (int num in numbers)
            {
                if (num > 0)
                    positive.Add(num);  // положительные пойдут в спысок позитивных
                else if (num < 0)
                    negative.Add(num); // негативные пойдут в список негативных
            }

            // Вывод результатов
            TypeText("\nДодатні числа:");
            PrintList(positive, "додатних");

            TypeText("\nВід'ємні числа:");
            PrintList(negative, "від'ємних");
        }

        ///////////////////////////////////////////////////////////////////// Функції для введення списків


        static List<double> ListOfDoubles()
        {
            // Создаем пустой список
            List<double> list = new List<double>();

            // Просим ввод пользователя
            
            TypeText("\nЯкщо бажаєте ввести числа вручну введіть (в).\nЯкщо хочете згенерувати випадкові числа введіть (г). \nВаш вибір: ");
            string mode = Console.ReadLine()?.Trim().ToLower();

            if (mode == "в" || mode == "вручну")
            {
                TypeText("\nВводьте числа по одному за раз. Для завершення введіть 'готово' або натисніть на пробіл.\n");

                while (true)
                {
                    Console.Write("Число: ");
                    string input = Console.ReadLine()?.Trim();

                    if (string.IsNullOrEmpty(input) || input.ToLower() == "готово")
                        break;

                    try
                    {
                        double number = double.Parse(input); // пытаемся преобразовать строку в double
                                                             // если не вышло , отлавливаем все исключения
                        list.Add(number);
                    }
                    catch
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nПомилка! Введіть дійсне число.\n");
                        Console.ResetColor();
                    }
                }
            }
            else   // любой другой ввод будем считать генерацией
            {
                TypeText("\nСкільки чисел бажаєте згенерувати? ");
                if (int.TryParse(Console.ReadLine(), out int count) && count > 0)
                {
                    Random rand = new Random();
                    for (int i = 0; i < count; i++)
                    {
                        // задаем диапазон
                        // rand.NextDouble() число от 0.0 до 1.0
                        // * 40 - 20 диапозон от -20.0 до + 20.0
                        // Math.Round(... , 2) округляем до двух знаков после запятой
                        
                        double value = Math.Round(rand.NextDouble() * 40 - 20, 2);
                        list.Add(value);
                    }
                    TypeText($"Згенеровано {count} чисел.");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nНекоректна кількість, ваш список буде порожнім.\n");
                    Console.ResetColor();
                }
            }
            //Вывод результата
            TypeText("\nСписок: " + string.Join("  ", list));
            return list;
        }

        static List<int> ListOfIntegers()
        {
            List<int> list = new List<int>();

            TypeText("\nЯкщо бажаєте ввести числа вручну введіть (в).\nЯкщо хочете згенерувати випадкові числа введіть (г). \nВаш вибір: ");
            string mode = Console.ReadLine()?.Trim().ToLower();

            if (mode == "в" || mode == "вручну")
            {
                TypeText("\nВводьте числа по одному за раз. Для завершення введіть 'готово' або натисніть на пробіл.\n");

                while (true)
                {
                    Console.Write("Число: ");
                    string input = Console.ReadLine()?.Trim();

                    if (string.IsNullOrEmpty(input) || input.ToLower() == "готово")
                        break;

                    if (int.TryParse(input, out int number))
                    {
                        list.Add(number);
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nПомилка! Введіть ціле число.\n");
                        Console.ResetColor();
                    }
                }
            }
            else
            {
                TypeText("\nСкільки чисел бажаєте згенерувати? ");
                if (int.TryParse(Console.ReadLine(), out int count) && count > 0)
                {
                    Random rand = new Random();
                    for (int i = 0; i < count; i++)
                    {
                        int value = rand.Next(-30, 31);
                        list.Add(value);
                    }
                    TypeText($"Згенеровано {count} чисел.");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nНекоректна кількість, ваш список буде порожнім.\n");
                    Console.ResetColor();

                }
            }

            TypeText("\nСписок: " + string.Join("  ", list));
            return list;
        }

        static void PrintList(List<int> list, string typeName)
        {
            if (list.Count == 0)
            {
                TypeText($"   (немає {typeName} чисел)");
                return;
            }

            TypeText("   " + string.Join(", ", list));
            TypeText($"   Кількість: {list.Count}");
        }

        ///////////////////////////////////////////////// Style Functions


        public static void CenterText(string text, int line)
        {
            Console.SetCursorPosition((Console.WindowWidth - text.Length) / 2, line);
            Console.WriteLine(text);
        }

        public static void TypeText(string text, int delay = 25)
        {
            foreach (char c in text)
            {
                Console.Write(c);
                Thread.Sleep(delay);
            }
        }
    }
}
