using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
namespace Testovoe
{
    class Program
    {                                //    Задача 1 Прочитать с консоли количество этажей, подъездов и номер квартиры. По введенному
                                     //    номеру квартиры выдать номер подъезда и этажа, где находится эта квартира, а также
                                     //    положение квартиры на лестничной площадке. 
                                     //  [2   3]
                                     //  [1   4]
                                     // находистя сумма квартир, путем умножения кол-ва этаже на 4 и на кол-во подъездов.
                                     // то,сколько раз номер квартиры делится нацело на 4 = (номер площадки -1). Кол-во номеров площадки = сумма квартир/4.
                                     // то, какой остается остаток при делении номера квартиры на 4 = номер позиции на лестничной площадке(если 0, то позиция = 4) .
                                     // площадок столько в подъезде, сколько в нем этажей


        static int sentPackets = 0;      // Счетчик отправленных пакетов
        static int receivedPackets = 0;  // Счетчик принятых пакетов
        static void Main(string[] args)
        {
            Console.WriteLine("Выберите решение (1 или 2 или 3, 1 = без if, 2 = с if, 3 = генератор UDP):"); // выбор польльзователем решения
            int choice = int.Parse(Console.ReadLine());
            if (choice == 1)
            {
                Console.WriteLine("Введите количество этажей");
                int FloorCount = Int32.Parse(Console.ReadLine());
                Console.WriteLine("Введите количество подъездов");
                int EntranceCount = Int32.Parse(Console.ReadLine());
                Console.WriteLine("Введите номер квартиры");
                int ApartmentNumber = Int32.Parse(Console.ReadLine());
                int SumApartment = FloorCount * EntranceCount * 4; // Всего квартир
                if (ApartmentNumber < 1 || ApartmentNumber > SumApartment)
                {
                    Console.WriteLine("Такой квартиры нет");
                }
                else
                {
                    int ApartmentPosition = ApartmentNumber % 4;
                    int EntranceNumber = (ApartmentNumber - 1) / (FloorCount * 4) + 1; // Нахождение номера подъезда, путем деления нацело номера квартиры на произведение кол-ва этажей и кол-ва квартир на этаже,
                                                                                       // то есть, если взять ситуацию где 1 этаЖ, 2 подъезда и на каждом этаже 2 квартиры, будет легче всего понять, а +1, для округления,
                                                                                       // ведь делим мы целые числа, и -1 в номере квартиры ставится для последнего номера, ведь без него выдаст несуществующий эатж дома

                    int FloorNumber = (ApartmentNumber - 1) / (EntranceCount * 4) % FloorCount + 1; // Нахождение номере этажа, мы делим номер квартиры на количество квартир в одном подъезде(произведение этажей и 4)
                                                                                                    // так мы находим часть, занимающую квартирой внутри дома и затем находим остаток от деления на кол-во этажей,
                                                                                                    // опять же, легче проиллюстрировать это на маленьком доме с малым кол-вом всех переменных.

                    string a = ""; // вспомогательная переменная, меняющая номер позиции квартиры на лестничной площадке, на название этой позиции
                    switch (ApartmentPosition)
                    {
                        case 1:
                            a = "Ближняя слева";
                            break;
                        case 2:
                            a = "Дальняя слева";
                            break;
                        case 3:
                            a = "Дальняя справа";
                            break;
                        case 0:
                            a = "Ближняя справа";
                            break;
                    }
                    int remainder = EntranceCount % 10;// создание конструкции для определения правильного окончания слова 'подъезд'
                    int secondToLastDigit = (EntranceCount / 10) % 10;

                    string result = "подъездов";

                    if (secondToLastDigit != 1)
                    {
                        switch (remainder)
                        {
                            case 1:
                                result = "подъезд";
                                break;
                            case 2:
                            case 3:
                            case 4:
                                result = "подъезда";
                                break;
                        }
                    }
                    Console.WriteLine($"В доме, имеющем {FloorCount} этажей, {EntranceCount}  {result}, квартира номер {ApartmentNumber}, расположена в {EntranceNumber} подъезде и на {FloorNumber} этаже и имеет положение - {a}");
                }
            }
            else if (choice == 2)
            {
                Console.WriteLine("Введите количество этажей");
                int FloorCount = Int32.Parse(Console.ReadLine());
                Console.WriteLine("Введите количество подъездов");
                int EntranceCount = Int32.Parse(Console.ReadLine());
                Console.WriteLine("Введите номер квартиры");
                int ApartmentNumber = Int32.Parse(Console.ReadLine());
                int SumApartment = FloorCount * EntranceCount * 4; // Всего квартир
                int apartmentsPerEntrance = FloorCount * 4; // Нахождение квартир в подъезде
                if (ApartmentNumber < 1 || ApartmentNumber > SumApartment)
                {
                    Console.WriteLine("Такой квартиры нет");
                }
                else
                {
                    int entranceNumber, floorNumber;
                    int ApartmentPosition = ApartmentNumber % 4;
                    if (ApartmentNumber <= apartmentsPerEntrance)// Определения, находится ли квартира в первом подъезде, или в другом, и затем определяются номер подъезда и этажа.
                    {
                        entranceNumber = 1;
                        floorNumber = (ApartmentNumber - 1) / 4 + 1;
                    }
                    else
                    {
                        entranceNumber = (ApartmentNumber - 1) / apartmentsPerEntrance + 1;
                        floorNumber = ((ApartmentNumber - 1) % apartmentsPerEntrance) / 4 + 1;
                    }
                    string a = ""; // Вспомогательная переменная, меняющая номер позиции квартиры на лестничной площадке, на название этой позиции
                    switch (ApartmentPosition)
                    {
                        case 1:
                            a = "Ближняя слева";
                            break;
                        case 2:
                            a = "Дальняя слева";
                            break;
                        case 3:
                            a = "Дальняя справа";
                            break;
                        case 0:
                            a = "Ближняя справа";
                            break;
                    }
                    int remainder = EntranceCount % 10;// создание конструкции для определения правильного окончания слова 'подъезд'
                    int secondToLastDigit = (EntranceCount / 10) % 10;

                    string result = "подъездов";

                    if (secondToLastDigit != 1)
                    {
                        switch (remainder)
                        {
                            case 1:
                                result = "подъезд";
                                break;
                            case 2:
                            case 3:
                            case 4:
                                result = "подъезда";
                                break;
                        }
                    }
                    Console.WriteLine($"В доме, имеющем {FloorCount} этажей, {EntranceCount}  {result}, квартира номер {ApartmentNumber}, расположена в {entranceNumber} подъезде и на {floorNumber} этаже и имеет положение - {a}");
                }
                
            }
            else if (choice == 3)
            {
                    Console.WriteLine("Программа генератора и приема UDP трафика");
                    Console.WriteLine();

                    // Запрашиваем у пользователя информацию для настройки генерации и приема трафика.
                    Console.Write("Введите IP-адрес назначения: ");
                    string ipAddress = Console.ReadLine();

                    Console.Write("Введите порт назначения: ");
                    int port = int.Parse(Console.ReadLine());

                    Console.Write("Введите количество пакетов для отправки: ");
                    int packetCount = int.Parse(Console.ReadLine());

                    Console.Write("Введите размер пакета (в байтах): ");
                    int packetSize = int.Parse(Console.ReadLine());

                    // Создаем и запускаем поток для отправки пакетов.
                    Thread senderThread = new Thread(() =>
                    {
                        sentPackets = SendUdpTraffic(ipAddress, port, packetCount, packetSize);
                    });
                    senderThread.Start();

                    // Создаем и запускаем поток для приема пакетов.
                    Thread receiverThread = new Thread(() =>
                    {
                        receivedPackets = ReceiveUdpTraffic(ipAddress, port, packetCount);
                    });
                    receiverThread.Start();

                    // Ожидаем завершения работы обоих потоков.
                    senderThread.Join();
                    receiverThread.Join();

                    // Выводим информацию о количестве отправленных, полученных и потерянных пакетах.
                    Console.WriteLine($"Отправлено: {sentPackets} пакетов");
                    Console.WriteLine($"Принято: {receivedPackets} пакетов");
                    Console.WriteLine($"Потеряно: {sentPackets - receivedPackets} пакетов");
                

                // Метод для отправки UDP-трафика
                static int SendUdpTraffic(string ipAddress, int port, int packetCount, int packetSize)
                {
                    UdpClient udpClient = new UdpClient();
                    IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(ipAddress), port);

                    int sentPackets = 0;

                    try
                    {
                        for (int i = 0; i < packetCount; i++)
                        {
                            byte[] data = new byte[packetSize];
                            udpClient.Send(data, data.Length, endPoint);
                            sentPackets++;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Ошибка отправки: {ex.Message}");
                    }
                    finally
                    {
                        udpClient.Close();
                    }

                    return sentPackets;
                }

                // Метод для приема UDP-трафика
                static int ReceiveUdpTraffic(string ipAddress, int port, int packetCount)
                {
                    UdpClient udpClient = new UdpClient(port);
                    IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(ipAddress), port);

                    int receivedPackets = 0;

                    try
                    {
                        for (int i = 0; i < packetCount; i++)
                        {
                            udpClient.Receive(ref endPoint);
                            receivedPackets++;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Ошибка приема: {ex.Message}");
                    }
                    finally
                    {
                        udpClient.Close();
                    }

                    return receivedPackets;
                }
            }
            else
            {
                Console.WriteLine("Неверный выбор.");
            }
            
        

        }
    }
}

