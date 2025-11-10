using System;
using System.Collections.Generic;

namespace AudioVideoEquipment
{
    // Базовый класс
    public class AudioVideoEquipment
    {
        private string brand;
        private string model;
        private int price;
        private int powerConsumption;

        // Области допустимых значений
        public const int MIN_PRICE = 0;
        public const int MAX_PRICE = 100000;
        public const int MIN_POWER = 0;
        public const int MAX_POWER = 2000;

        // Конструктор
        public AudioVideoEquipment(string brand = "Unknown", string model = "Unknown",
                                  int price = 0, int powerConsumption = 0)
        {
            Brand = brand;
            Model = model;
            Price = price;
            PowerConsumption = powerConsumption;
        }

        // Свойства с валидацией
        public string Brand
        {
            get => brand;
            set => brand = string.IsNullOrEmpty(value) ? "Unknown" : value;
        }

        public string Model
        {
            get => model;
            set => model = string.IsNullOrEmpty(value) ? "Unknown" : value;
        }

        public int Price
        {
            get => price;
            set => price = (value >= MIN_PRICE && value <= MAX_PRICE) ? value : 0;
        }

        public int PowerConsumption
        {
            get => powerConsumption;
            set => powerConsumption = (value >= MIN_POWER && value <= MAX_POWER) ? value : 0;
        }

        // Виртуальный метод для вывода сведений
        public virtual void PrintInfo()
        {
            Console.WriteLine($"Тип: Базовое аудио-видео оборудование");
            Console.WriteLine($"Бренд: {Brand}");
            Console.WriteLine($"Модель: {Model}");
            Console.WriteLine($"Цена: {Price} руб.");
            Console.WriteLine($"Потребляемая мощность: {PowerConsumption} Вт");
            Console.WriteLine("------------------------");
        }

        // Методы для получения характеристик
        public string GetBrand() => Brand;
        public string GetModel() => Model;
        public int GetPrice() => Price;
        public int GetPowerConsumption() => PowerConsumption;

        // Методы для изменения характеристик
        public void SetPrice(int newPrice) => Price = newPrice;
        public void SetPowerConsumption(int newPower) => PowerConsumption = newPower;
    }

    // Производный класс - Телевизор
    public class Television : AudioVideoEquipment
    {
        private double screenSize;
        private string resolution;
        private bool hasSmartTV;

        // Области допустимых значений
        public const double MIN_SCREEN_SIZE = 10;
        public const double MAX_SCREEN_SIZE = 100;

        // Конструктор
        public Television(string brand = "Unknown", string model = "Unknown", int price = 0, int powerConsumption = 0,
                          double screenSize = 32, string resolution = "1920x1080", bool hasSmartTV = false)
            : base(brand, model, price, powerConsumption)
        {
            ScreenSize = screenSize;
            Resolution = resolution;
            this.hasSmartTV = hasSmartTV;
        }

        // Свойства
        public double ScreenSize
        {
            get => screenSize;
            set => screenSize = (value >= MIN_SCREEN_SIZE && value <= MAX_SCREEN_SIZE) ? value : 32;
        }

        public string Resolution
        {
            get => resolution;
            set => resolution = string.IsNullOrEmpty(value) ? "1920x1080" : value;
        }

        public bool HasSmartTV
        {
            get => hasSmartTV;
            set => hasSmartTV = value;
        }

        // Переопределенный метод вывода сведений
        public override void PrintInfo()
        {
            Console.WriteLine($"Тип: Телевизор");
            Console.WriteLine($"Бренд: {Brand}");
            Console.WriteLine($"Модель: {Model}");
            Console.WriteLine($"Цена: {Price} руб.");
            Console.WriteLine($"Потребляемая мощность: {PowerConsumption} Вт");
            Console.WriteLine($"Диагональ экрана: {ScreenSize}\"");
            Console.WriteLine($"Разрешение: {Resolution}");
            Console.WriteLine($"Smart TV: {(HasSmartTV ? "Да" : "Нет")}");
            Console.WriteLine("------------------------");
        }

        // Дополнительные методы
        public double GetScreenSize() => ScreenSize;
        public string GetResolution() => Resolution;
        public bool GetSmartTVStatus() => HasSmartTV;

        public void SetScreenSize(double newSize) => ScreenSize = newSize;
        public void SetSmartTV(bool hasSmart) => HasSmartTV = hasSmart;
    }

    // Производный класс - Радиоприемник
    public class RadioReceiver : AudioVideoEquipment
    {
        private string frequencyRange;
        private bool hasBluetooth;
        private int numberOfPresets;

        // Конструктор
        public RadioReceiver(string brand = "Unknown", string model = "Unknown",
                           int price = 0, int powerConsumption = 0,
                           string frequencyRange = "FM", bool hasBluetooth = false,
                           int numberOfPresets = 10)
            : base(brand, model, price, powerConsumption)
        {
            FrequencyRange = frequencyRange;
            this.hasBluetooth = hasBluetooth;
            NumberOfPresets = numberOfPresets;
        }

        // Свойства
        public string FrequencyRange
        {
            get => frequencyRange;
            set => frequencyRange = string.IsNullOrEmpty(value) ? "FM" : value;
        }

        public bool HasBluetooth
        {
            get => hasBluetooth;
            set => hasBluetooth = value;
        }

        public int NumberOfPresets
        {
            get => numberOfPresets;
            set => numberOfPresets = (value >= 0 && value <= 100) ? value : 10;
        }

        // Переопределенный метод вывода сведений
        public override void PrintInfo()
        {
            Console.WriteLine($"Тип: Радиоприемник");
            Console.WriteLine($"Бренд: {Brand}");
            Console.WriteLine($"Модель: {Model}");
            Console.WriteLine($"Цена: {Price} руб.");
            Console.WriteLine($"Потребляемая мощность: {PowerConsumption} Вт");
            Console.WriteLine($"Диапазон частот: {FrequencyRange}");
            Console.WriteLine($"Bluetooth: {(HasBluetooth ? "Да" : "Нет")}");
            Console.WriteLine($"Количество пресетов: {NumberOfPresets}");
            Console.WriteLine("------------------------");
        }

        // Дополнительные методы
        public string GetFrequencyRange() => FrequencyRange;
        public bool GetBluetoothStatus() => HasBluetooth;
        public int GetNumberOfPresets() => NumberOfPresets;

        public void SetFrequencyRange(string newRange) => FrequencyRange = newRange;
        public void SetBluetooth(bool hasBT) => HasBluetooth = hasBT;
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            List<AudioVideoEquipment> equipmentList = new List<AudioVideoEquipment>();
            bool running = true;

            while (running)
            {
                Console.WriteLine("\nМеню:");
                Console.WriteLine("1 - Добавить устройство");
                Console.WriteLine("2 - Показать список");
                Console.WriteLine("3 - Удалить устройство");
                Console.WriteLine("4 - Выход");

                int choice = ReadInt("Выбор: ", 1, 4);

                switch (choice)
                {
                    case 1:
                        AddEquipment(equipmentList);
                        break;
                    case 2:
                        PrintList(equipmentList);
                        break;
                    case 3:
                        RemoveEquipment(equipmentList);
                        break;
                    case 4:
                        running = false;
                        break;
                }
            }

            ClearList(equipmentList);
            Console.WriteLine("Работа завершена.");
        }

        // Безопасный ввод
        private static string ReadString(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string s = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(s)) return s.Trim();
                Console.WriteLine("Введите значение.");
            }
        }

        private static int ReadInt(string prompt, int min, int max)
        {
            while (true)
            {
                Console.Write(prompt);
                if (int.TryParse(Console.ReadLine(), out int val) && val >= min && val <= max)
                    return val;
                Console.WriteLine($"Введите число от {min} до {max}.");
            }
        }

        private static double ReadDouble(string prompt, double min, double max)
        {
            while (true)
            {
                Console.Write(prompt);
                if (double.TryParse(Console.ReadLine(), out double val) && val >= min && val <= max)
                    return val;
                Console.WriteLine($"Введите число от {min} до {max}.");
            }
        }

        private static bool ReadBool(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string s = (Console.ReadLine() ?? "").Trim().ToLower();
                if (s == "1" || s == "true" || s == "y" || s == "да" || s == "д") return true;
                if (s == "0" || s == "false" || s == "n" || s == "нет" || s == "н") return false;
                Console.WriteLine("Введите 1/0 или Да/Нет.");
            }
        }

        // Добавление объекта в список
        private static void AddEquipment(List<AudioVideoEquipment> list)
        {
            Console.WriteLine("\n1 - Базовое устройство");
            Console.WriteLine("2 - Телевизор");
            Console.WriteLine("3 - Радиоприемник");
            int type = ReadInt("Тип: ", 1, 3);

            string brand = ReadString("Бренд: ");
            string model = ReadString("Модель: ");
            int price = ReadInt($"Цена ({AudioVideoEquipment.MIN_PRICE}-{AudioVideoEquipment.MAX_PRICE}): ", AudioVideoEquipment.MIN_PRICE, AudioVideoEquipment.MAX_PRICE);
            int power = ReadInt($"Мощность ({AudioVideoEquipment.MIN_POWER}-{AudioVideoEquipment.MAX_POWER}): ", AudioVideoEquipment.MIN_POWER, AudioVideoEquipment.MAX_POWER);

            AudioVideoEquipment device = null;

            if (type == 1)
            {
                device = new AudioVideoEquipment(brand, model, price, power);
            }
            else if (type == 2)
            {
                double size = ReadDouble($"Диагональ ({Television.MIN_SCREEN_SIZE}-{Television.MAX_SCREEN_SIZE}): ", Television.MIN_SCREEN_SIZE, Television.MAX_SCREEN_SIZE);
                string res = ReadString("Разрешение: ");
                bool smart = ReadBool("Smart TV (Да/Нет): ");
                device = new Television(brand, model, price, power, size, res, smart);
            }
            else if (type == 3)
            {
                string freq = ReadString("Диапазон (FM/AM): ");
                bool bt = ReadBool("Bluetooth (Да/Нет): ");
                int presets = ReadInt("Пресеты (0-100): ", 0, 100);
                device = new RadioReceiver(brand, model, price, power, freq, bt, presets);
            }

            list.Add(device);
            Console.WriteLine("Добавлено.");
        }

        // Вывод списка объектов
        private static void PrintList(List<AudioVideoEquipment> list)
        {
            if (list.Count == 0)
            {
                Console.WriteLine("Список пуст.");
                return;
            }

            for (int i = 0; i < list.Count; i++)
            {
                Console.WriteLine($"\n#{i + 1}");
                list[i].PrintInfo();
            }
        }

        // Удаление объекта из списка
        private static void RemoveEquipment(List<AudioVideoEquipment> list)
        {
            if (list.Count == 0)
            {
                Console.WriteLine("Список пуст.");
                return;
            }

            for (int i = 0; i < list.Count; i++)
                Console.WriteLine($"#{i + 1}: {list[i].GetBrand()} {list[i].GetModel()}");

            int num = ReadInt("Номер для удаления: ", 1, list.Count) - 1;
            list.RemoveAt(num);
            Console.WriteLine("Удалено.");
        }
        
        // Освобождение динамически выделенной памяти
        private static void ClearList(List<AudioVideoEquipment> list)
        {
            int count = list.Count;
            list.Clear();
            GC.Collect();
            GC.WaitForPendingFinalizers();
            Console.WriteLine($"Освобождено {count} объект(ов).");
        }
    }
}
