using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Labb3Threads
{
    internal class Car
    {
        public string carName { get; set; }
        public int carSpeed { get; set; }
        public decimal distanceTraveled { get; set; }
        public List<string> AccidentsLog { get; set; }
        public int PenaltyTimer { get; set; }
        private System.Timers.Timer eventTimer;
        public Car(string _carName, int _carSpeed)
        {
            carName = _carName;
            carSpeed = _carSpeed;
            distanceTraveled = 0;
            AccidentsLog = new List<string>();
            PenaltyTimer = 0;

            eventTimer = new System.Timers.Timer(3000);
            // Set the event handler for the Elapsed event, which fires when the timer interval elapses
            eventTimer.Elapsed += OnTimedEvent;
            eventTimer.AutoReset = true;
            // Start the timer
            eventTimer.Start();
        }

        public void StartDriving()
        {
            while (distanceTraveled < 10000 && Program.raceOngoing)
            {
                // Move the car forward
                distanceTraveled += carSpeed / 10;

                Thread.Sleep(PenaltyTimer);
                PenaltyTimer = 0;

                Thread.Sleep(50);

            }
        }
        private void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            if (new Random().Next(50) < 1)
            {
                AccidentsLog.Add("Car stopped to refuel");
                PenaltyTimer = 3000; // 30 seconds to refuel
            }
            else if (new Random().Next(50) < 2)
            {
                AccidentsLog.Add("Car stopped to change a tire");

                PenaltyTimer = 2000; // 20 seconds to change a tire

            }
            else if (new Random().Next(50) < 5)
            {
                AccidentsLog.Add("Car stopped to clean the windshield");

                PenaltyTimer = 1000; // 10 seconds to clean the windshield

            }
            else if (new Random().Next(50) < 10)
            {
                AccidentsLog.Add("Car slowed down due to engine trouble");

                carSpeed--; // decrease speed by 1 km/h

            }
        }
    }
}
