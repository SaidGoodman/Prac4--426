using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AudioVideoEquipment;

namespace AudioVideoEquipmentTests
{
    // Создание виртуальной консоли для анализа вывода
    internal static class TestHelpers
    {
        public static string CaptureConsoleOut(Action action)
        {
            var sw = new StringWriter();
            var old = Console.Out;
            Console.SetOut(sw);
            try { action(); }
            finally { Console.SetOut(old); }
            return sw.ToString();
        }
    }

    
    [TestClass]
    [DoNotParallelize]
    public class AudioVideoEquipment_Base_Tests
    {
        // Дефолтный конструктор
        [TestMethod]
        public void Ctor_DefaultValues_AreApplied()
        {
            var d = new AudioVideoEquipment.AudioVideoEquipment();
            Assert.AreEqual("Unknown", d.Brand);
            Assert.AreEqual("Unknown", d.Model);
            Assert.AreEqual(0, d.Price);
            Assert.AreEqual(0, d.PowerConsumption);
        }

        // Конструктор с параментрами
        [TestMethod]
        public void Ctor_WithParams_Assigns_Validated()
        {
            var d = new AudioVideoEquipment.AudioVideoEquipment("Sony", "XZ", 19999, 150);
            Assert.AreEqual("Sony", d.Brand);
            Assert.AreEqual("XZ", d.Model);
            Assert.AreEqual(19999, d.Price);
            Assert.AreEqual(150, d.PowerConsumption);
        }

        // Значение цены вышло за допустимые границы
        [TestMethod]
        public void Price_OutOfRange_BecomesZero()
        {
            var d = new AudioVideoEquipment.AudioVideoEquipment();
            d.Price = -1;
            Assert.AreEqual(0, d.Price);

            d.Price = AudioVideoEquipment.AudioVideoEquipment.MAX_PRICE + 1;
            Assert.AreEqual(0, d.Price);
        }

        // Значение потребляемой энергии вышло за допустимые границы
        [TestMethod]
        public void Power_OutOfRange_BecomesZero()
        {
            var d = new AudioVideoEquipment.AudioVideoEquipment();
            d.PowerConsumption = -10;
            Assert.AreEqual(0, d.PowerConsumption);

            d.PowerConsumption = AudioVideoEquipment.AudioVideoEquipment.MAX_POWER + 1;
            Assert.AreEqual(0, d.PowerConsumption);
        }

        // Проверка геттеров
        [TestMethod]
        public void Getters_Return_CurrentValues()
        {
            var d = new AudioVideoEquipment.AudioVideoEquipment("LG", "AB", 123, 50);
            Assert.AreEqual("LG", d.GetBrand());
            Assert.AreEqual("AB", d.GetModel());
            Assert.AreEqual(123, d.GetPrice());
            Assert.AreEqual(50, d.GetPowerConsumption());
        }

        // Проверка сеттеров
        [TestMethod]
        public void Setters_Update_AndValidate()
        {
            var d = new AudioVideoEquipment.AudioVideoEquipment("LG", "AB", 100, 100);
            d.SetPrice(200);
            d.SetPowerConsumption(150);
            Assert.AreEqual(200, d.Price);
            Assert.AreEqual(150, d.PowerConsumption);

            d.SetPrice(-5);
            d.SetPowerConsumption(-1);
            Assert.AreEqual(0, d.Price);
            Assert.AreEqual(0, d.PowerConsumption);
        }

        // Печать объекта базового класса 
        [TestMethod]
        public void PrintInfo_Base_ContainsExpectedFields()
        {
            var d = new AudioVideoEquipment.AudioVideoEquipment("LG", "Q1", 999, 77);
            var output = TestHelpers.CaptureConsoleOut(() => d.PrintInfo());

            StringAssert.Contains(output, "Тип: Базовое аудио-видео оборудование");
            StringAssert.Contains(output, "Бренд: LG");
            StringAssert.Contains(output, "Модель: Q1");
            StringAssert.Contains(output, "Цена: 999");
            StringAssert.Contains(output, "Потребляемая мощность: 77");
        }
    }

    [TestClass]
    [DoNotParallelize]
    public class Television_Tests
    {
        // Дефолтный конструктор
        [TestMethod]
        public void Ctor_Defaults_AreApplied()
        {
            var tv = new Television();
            Assert.AreEqual("Unknown", tv.Brand);
            Assert.AreEqual("Unknown", tv.Model);
            Assert.AreEqual(32, tv.ScreenSize);
            Assert.AreEqual("1920x1080", tv.Resolution);
            Assert.IsFalse(tv.HasSmartTV);
        }

        // Конструктор с параметрами
        [TestMethod]
        public void Ctor_Params_Assign_All()
        {
            var tv = new Television("Samsung", "Q90", 50000, 200, 55, "3840x2160", true);
            Assert.AreEqual("Samsung", tv.Brand);
            Assert.AreEqual("Q90", tv.Model);
            Assert.AreEqual(50000, tv.Price);
            Assert.AreEqual(200, tv.PowerConsumption);
            Assert.AreEqual(55, tv.ScreenSize);
            Assert.AreEqual("3840x2160", tv.Resolution);
            Assert.IsTrue(tv.HasSmartTV);
        }

        // Значение размера экрана вышло за допустимые границы
        [TestMethod]
        public void ScreenSize_OutOfRange_DefaultsTo32()
        {
            var tv = new Television();
            tv.ScreenSize = 5;
            Assert.AreEqual(32, tv.ScreenSize);

            tv.ScreenSize = 1000;
            Assert.AreEqual(32, tv.ScreenSize);
        }

        // Проверка передачи пустого поля в разрешение экрана
        [TestMethod]
        public void Resolution_Empty_Defaults()
        {
            var tv = new Television();
            tv.Resolution = null;
            Assert.AreEqual("1920x1080", tv.Resolution);
            tv.Resolution = "";
            Assert.AreEqual("1920x1080", tv.Resolution);
        }

        // Проверка геттеров и сеттеров
        [TestMethod]
        public void Getters_AndSetters_Work()
        {
            var tv = new Television();
            tv.SetScreenSize(49);
            tv.SetSmartTV(true);

            Assert.AreEqual(49, tv.GetScreenSize());
            Assert.IsTrue(tv.GetSmartTVStatus());

            tv.Resolution = "2560x1440";
            Assert.AreEqual("2560x1440", tv.GetResolution());
        }

        // Вывод объекта производного класса Television
        [TestMethod]
        public void PrintInfo_Television_ContainsSpecificFields()
        {
            var tv = new Television("Sony", "X90", 30000, 120, 50, "3840x2160", true);
            var output = TestHelpers.CaptureConsoleOut(() => tv.PrintInfo());

            StringAssert.Contains(output, "Тип: Телевизор");
            StringAssert.Contains(output, "Бренд: Sony");
            StringAssert.Contains(output, "Модель: X90");
            StringAssert.Contains(output, "Диагональ экрана: 50");
            StringAssert.Contains(output, "Разрешение: 3840x2160");
            StringAssert.Contains(output, "Smart TV: Да");
        }
    }

    [TestClass]
    [DoNotParallelize]
    public class RadioReceiver_Tests
    {
        // Дефолтный конструктор
        [TestMethod]
        public void Ctor_Defaults_AreApplied()
        {
            var r = new RadioReceiver();
            Assert.AreEqual("Unknown", r.Brand);
            Assert.AreEqual("Unknown", r.Model);
            Assert.AreEqual("FM", r.FrequencyRange);
            Assert.IsFalse(r.HasBluetooth);
            Assert.AreEqual(10, r.NumberOfPresets);
        }

        // Конструктор с параметрами
        [TestMethod]
        public void Ctor_Params_Assign_All()
        {
            var r = new RadioReceiver("Panasonic", "R1", 1500, 15, "AM/FM", true, 20);
            Assert.AreEqual("Panasonic", r.Brand);
            Assert.AreEqual("R1", r.Model);
            Assert.AreEqual(1500, r.Price);
            Assert.AreEqual(15, r.PowerConsumption);
            Assert.AreEqual("AM/FM", r.FrequencyRange);
            Assert.IsTrue(r.HasBluetooth);
            Assert.AreEqual(20, r.NumberOfPresets);
        }

        // Проверка передачи пустого поля в воспринимаемые частоты
        [TestMethod]
        public void Frequency_Empty_DefaultsToFM()
        {
            var r = new RadioReceiver();
            r.FrequencyRange = null;
            Assert.AreEqual("FM", r.FrequencyRange);
            r.FrequencyRange = "";
            Assert.AreEqual("FM", r.FrequencyRange);
        }

        // Значение количества пресетов вышло за допустимые границы
        [TestMethod]
        public void Presets_OutOfRange_DefaultsTo10()
        {
            var r = new RadioReceiver();
            r.NumberOfPresets = -1;
            Assert.AreEqual(10, r.NumberOfPresets);

            r.NumberOfPresets = 101;
            Assert.AreEqual(10, r.NumberOfPresets);
        }

        // Проверка геттеров и сеттеров
        [TestMethod]
        public void Getters_AndSetters_Work()
        {
            var r = new RadioReceiver();
            r.SetFrequencyRange("DAB");
            r.SetBluetooth(true);

            Assert.AreEqual("DAB", r.GetFrequencyRange());
            Assert.IsTrue(r.GetBluetoothStatus());

            r.NumberOfPresets = 42;
            Assert.AreEqual(42, r.GetNumberOfPresets());
        }

        // Вывод объекта производого класса Radio
        [TestMethod]
        public void PrintInfo_Radio_ContainsSpecificFields()
        {
            var r = new RadioReceiver("Sony", "RX", 999, 7, "AM/FM", true, 30);
            var output = TestHelpers.CaptureConsoleOut(() => r.PrintInfo());

            StringAssert.Contains(output, "Тип: Радиоприемник");
            StringAssert.Contains(output, "Бренд: Sony");
            StringAssert.Contains(output, "Модель: RX");
            StringAssert.Contains(output, "Диапазон частот: AM/FM");
            StringAssert.Contains(output, "Bluetooth: Да");
            StringAssert.Contains(output, "Количество пресетов: 30");
        }
    }

    [TestClass]
    public class Polymorphism_Tests
    {
        // Проверка полиморфизма функции вывода объекта
        [TestMethod]
        public void PrintInfo_Virtual_Dispatch_Works_Through_BaseRef()
        {
            var items = new List<AudioVideoEquipment.AudioVideoEquipment>
            {
                new AudioVideoEquipment.AudioVideoEquipment("B", "Base", 1, 1),
                new Television("TBrand", "TModel", 2, 2, 32, "1920x1080", false),
                new RadioReceiver("RBrand", "RModel", 3, 3, "FM", true, 5)
            };

            var output = TestHelpers.CaptureConsoleOut(() =>
            {
                foreach (var x in items)
                    x.PrintInfo();
            });

            StringAssert.Contains(output, "Тип: Базовое аудио-видео оборудование");
            StringAssert.Contains(output, "Тип: Телевизор");
            StringAssert.Contains(output, "Тип: Радиоприемник");
        }
    }
}
