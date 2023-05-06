using System.Threading;

namespace Labb3Threads
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            RaceStart();

        }

        public static bool raceOngoing = true;
        
        public static void RaceStart()
        {

            List<Car> cars = new List<Car>();
            List<Thread> threads = new List<Thread>();
            int speed = 120;

            Car Car1 = new Car("Volvo", speed);
            Car Car2 = new Car("Ford", speed);

            cars.Add(Car1);
            cars.Add(Car2);

            Thread thread1 = new Thread(() => Car1.StartDriving());
            Thread thread2 = new Thread(() => Car2.StartDriving());

            threads.Add(thread1);
            threads.Add(thread2);

            thread1.Start();
            thread2.Start();

            // Get the initial cursor position
            int initialTop = Console.CursorTop;

            while (raceOngoing)
            {
                // Update the relevant lines of the console
                Console.SetCursorPosition(0, initialTop);
                DisplayDistances(cars);

                Thread.Sleep(50);

                // Check if any car has reached the finish line
                if (cars.Any(c => c.distanceTraveled >= 10000))
                {
                    raceOngoing = false;
                    Console.SetCursorPosition(0, initialTop);
                    DisplayDistances(cars);
                    break;
                }

            }

            // Join all threads to ensure they have terminated
            foreach (Thread thread in threads)
            {
                thread.Join();
            }

            List<Car> sortedCarsByPlacing =  cars.OrderByDescending(c => c.distanceTraveled).ToList();

            Console.WriteLine($"{sortedCarsByPlacing.First().carName} - Won the Race ^^ {sortedCarsByPlacing.First().distanceTraveled}");

            Console.WriteLine("Race finished!");

            foreach (Car car in cars)
            {
                Console.WriteLine($"Accidents Log for {car.carName}");
                foreach (string log in car.AccidentsLog)
                {
                    Console.WriteLine(log);
                } 
            }

            Console.ReadKey();

        }
        public static void DisplayDistances(List<Car> cars)
        {
            // Save the current cursor position
            int left = Console.CursorLeft;
            int top = Console.CursorTop;

            // Sort the cars by distance in descending order
            List<Car> sortedCars = cars.OrderByDescending(c => c.distanceTraveled).ToList();

            // Display the distance of each car in order
            for (int i = 0; i < sortedCars.Count; i++)
            {
                Console.SetCursorPosition(left, top + i);
                Console.WriteLine($"[{i + 1}] - {sortedCars[i].carName} - Distans Traveled: {sortedCars[i].distanceTraveled}km Current Speed: {sortedCars[i].carSpeed}km/h                  ");
            }
        }

    }
}