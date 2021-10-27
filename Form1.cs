using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestApp
{
    public partial class Form1 : Form
    {
        private List<Arc> Arcs { get; set; }
        private List<Bus> Buses { get; set; }
        private readonly int finalTime = 1440;
        private int startStation = -1;
        private int endStation = -1;
        private int startTime = -1;
        private int stationsCount;

        public Form1()
        {
            InitializeComponent();
        }

        private void ChooseFile(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.RestoreDirectory = true;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                var fileName = dialog.FileName;
                using (var fileStream = new FileStream(fileName, FileMode.Open))
                {
                    ProcessFile(fileStream);
                    UpdateForm();
                }
            }
        }

        private void UpdateForm()
        {
            BusCountLabel.Text = "Количество автобусов: " + Buses.Count;
            StationsCountLabel.Text = "Количество остановок: " + stationsCount;
            ResultLabel.Text = "";
            for (int i = 0; i < Buses.Count; i++)
            {
                var bus = Buses[i];
                Panel.Controls.Add(new Label()
                {
                    Text = $"Автобус {i + 1}: стоимость проезда {bus.Fare} рублей, время отправления {TranslateTime(bus.StartTime)}, полное время маршрута {bus.TotalTime} минут",
                    Location = new System.Drawing.Point(10, i * 20 + 10),
                    AutoSize = true
                });
            }
        }

        private void ProcessFile(FileStream fileStream)
        {
            Buses = new List<Bus>();
            Arcs = new List<Arc>();
            using (var reader = new StreamReader(fileStream))
            {
                var busesCount = int.Parse(reader.ReadLine().Trim());
                stationsCount = int.Parse(reader.ReadLine().Trim());
                var times = reader.ReadLine().Trim().Split(' ');
                var fares = reader.ReadLine().Trim().Split(' ');
                Bus bus;
                for (int i = 0; i < times.Length; i++)
                {
                    bus = new Bus();
                    bus.Number = i + 1;
                    bus.StartTime = TranslateTime(times[i]);
                    bus.Fare = int.Parse(fares[i]);
                    var routeString = reader.ReadLine().Trim();
                    ExtractArcs(routeString, bus);
                    Buses.Add(bus);
                }
            }
        }

        private void ExtractArcs(string routeString, Bus bus)
        {
            int totalTime = 0;
            bus.Arcs = new List<Arc>();
            var parts = routeString.Split(' ');
            var length = (parts.Length - 1) / 2;
            if (length < 1)
            {
                bus.TotalTime = 0;
                return;
            }
            Arc arc;
            for (int i = 0; i < length - 1; i++)
            {
                arc = new Arc();
                arc.StartStation = int.Parse(parts[i + 1]) - 1;
                arc.EndStation = int.Parse(parts[i + 2]) - 1;
                arc.Time = int.Parse(parts[i + length + 1]);
                arc.Bus = bus;
                totalTime += arc.Time;
                Arcs.Add(arc);
                bus.Arcs.Add(arc);
            }
            arc = new Arc();
            arc.StartStation = int.Parse(parts[length]) - 1;
            arc.EndStation = int.Parse(parts[1]) - 1;
            arc.Time = int.Parse(parts[parts.Length - 1]);
            arc.Bus = bus;
            totalTime += arc.Time;
            Arcs.Add(arc);
            bus.Arcs.Add(arc);
            bus.TotalTime = totalTime;
        }

        private int TranslateTime(string time)
        {
            var parts = time.Split(':');
            return int.Parse(parts[0]) * 60 + int.Parse(parts[1]);
        }

        private string TranslateTime(int time)
        {
            return (time / 60).ToString() + ":" + (time % 60).ToString("00");
        }

        private void StationChanged(object sender, EventArgs e)
        {
            var textBox = (TextBox)sender;
            int value;
            if (!int.TryParse(textBox.Text, out value))
            {
                value = -1;
            }
            if (textBox == StartStationTextBox)
                startStation = value - 1;
            else
                endStation = value - 1;
        }

        private void StartTimeChanged(object sender, EventArgs e)
        {
            var textBox = (TextBox)sender;
            if (Regex.Match(textBox.Text, @"^(\d|\d\d):\d\d$").Success)
            {
                startTime = TranslateTime(textBox.Text);
            }
            else
            {
                startTime = -1;
            }
        }

        private void Calculate(object sender, EventArgs e)
        {
            string validateString = ValidateInputs();
            if (validateString == "")
            {
                var fastestWay = CalculateWay(WayType.Fast, (Arc nextArc, Bus lastBus, int value) =>
                {
                    return nextArc.Time + GetWaitTime(nextArc, value);
                });
                var cheapestWay = CalculateWay(WayType.Cheap, (Arc nextArc, Bus lastBus, int value) =>
                {
                    if (lastBus != nextArc.Bus)
                        return nextArc.Bus.Fare;
                    else
                        return 0;
                });
                ResultLabel.Text = GetWayDescription(fastestWay, WayType.Fast) + ";\n";
                ResultLabel.Text += GetWayDescription(cheapestWay, WayType.Cheap);
            }
            else
                ResultLabel.Text = validateString;
        }

        private string GetWayDescription(Way way, WayType type)
        {
            string stringType = type == WayType.Fast ? "быстрый" : "дешевый";
            if (startTime + way.Time > finalTime)
                return $"Нельзая построить {stringType} маршрут: слишком поздно";
            else
                return $"Самый {stringType} маршрут стоит {way.TotalFare} рублей и занимает {way.Time} минут. Описание: " +
                    string.Join(", затем ", way.Arcs.Select(x => { return $"на станции {x.StartStation + 1} сесть на {x.Bus.Number} автобус до станции {x.EndStation + 1}"; }));
        }

        private string ValidateInputs()
        {
            List<string> validateParts = new List<string>();
            if (startStation < 0 || startStation > stationsCount - 1)
                validateParts.Add("неправильно указана первая остановка");
            if (endStation < 0 || endStation > stationsCount - 1)
                validateParts.Add("неправильно указана конечная остановка");
            if (startTime < 0 || startTime > finalTime)
                validateParts.Add("неправильно указано время отправки");
            var validateString = string.Join("; ", validateParts);
            if (validateParts.Count > 0)
                return validateString[0].ToString().ToUpper() + validateString.Substring(1);
            else
                return "";
        }

        private Way CalculateWay(WayType type, Func<Arc, Bus, int, int> func)
        {
            int[] time = new int[stationsCount];
            int[] fare = new int[stationsCount];
            var ways = new List<Arc>[stationsCount];
            int length = time.Length;
            for (int i = 0; i < length; i++)
            {
                time[i] = int.MaxValue;
                fare[i] = int.MaxValue;
            }
            time[startStation] = 0;
            fare[startStation] = 0;
            ways[startStation] = new List<Arc>();
            int weight;
            Bus lastBus = null;
            if (type == WayType.Fast)
            {
                for (int i = 0; i < length - 1; i++)
                {
                    foreach (var arc in Arcs)
                    {
                        if (ways[arc.StartStation] != null && ways[arc.StartStation].Count > 0)
                            lastBus = ways[arc.StartStation].Last().Bus;
                        else
                            lastBus = null;
                        weight = arc.Time + GetWaitTime(arc, time[arc.StartStation]);
                        if (time[arc.StartStation] != int.MaxValue && time[arc.EndStation] > time[arc.StartStation] + weight)
                        {
                            time[arc.EndStation] = time[arc.StartStation] + weight;
                            ways[arc.EndStation] = GetWay(ways[arc.StartStation], arc);
                            if (lastBus != arc.Bus)
                                fare[arc.EndStation] = fare[arc.StartStation] + arc.Bus.Fare;
                            else
                                fare[arc.EndStation] = fare[arc.StartStation];
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < length - 1; i++)
                {
                    foreach (var arc in Arcs)
                    {
                        if (ways[arc.StartStation] != null && ways[arc.StartStation].Count > 0)
                            lastBus = ways[arc.StartStation].Last().Bus;
                        else
                            lastBus = null;
                        if (lastBus != arc.Bus)
                            weight = arc.Bus.Fare;
                        else
                            weight = 0;
                        if (fare[arc.StartStation] != int.MaxValue && fare[arc.EndStation] > fare[arc.StartStation] + weight)
                        {
                            fare[arc.EndStation] = fare[arc.StartStation] + weight;
                            ways[arc.EndStation] = GetWay(ways[arc.StartStation], arc);
                            time[arc.EndStation] = time[arc.StartStation] + arc.Time + GetWaitTime(arc, time[arc.StartStation]);
                        }
                    }
                }
            }
            return new Way
            {
                Arcs = GetFixedRoute(ways[endStation]),
                Time = time[endStation],
                TotalFare = fare[endStation]
            };
        }

        private List<Arc> GetFixedRoute(List<Arc> arcs)
        {
            var fixedRoute = new List<Arc>();
            var length = arcs.Count;
            Bus bus = null;
            Arc arc = null;
            for (int i = 0; i < length; i++)
            {
                if (arcs[i].Bus != bus)
                {
                    arc = new Arc()
                    {
                        StartStation = arcs[i].StartStation,
                        EndStation = arcs[i].EndStation,
                        Bus = arcs[i].Bus
                    };
                    fixedRoute.Add(arc);
                    bus = arcs[i].Bus;
                }
                else
                {
                    arc.EndStation = arcs[i].EndStation;
                }
            }
            return fixedRoute;
        }

        private int GetWaitTime(Arc arc, int passedTime)
        {
            int waitTime = arc.Bus.Arcs.TakeWhile(x => x.StartStation != arc.StartStation).Sum(x => x.Time);
            waitTime = arc.Bus.StartTime + waitTime - startTime - passedTime;
            if (waitTime < 0)
            {
                waitTime = waitTime % arc.Bus.TotalTime;
            }
            if (waitTime < 0)
            {
                waitTime += arc.Bus.TotalTime;
            }
            return waitTime;
        }

        private List<Arc> GetWay(List<Arc> sourceWay, Arc arc)
        {
            var way = new List<Arc>();
            foreach (var a in sourceWay)
                way.Add(a);
            way.Add(arc);
            return way;
        }
    }

    public class Way
    {
        public List<Arc> Arcs { get; set; }
        public int Time { get; set; }
        public int TotalFare { get; set;}
    }

    public class Bus
    {
        public int Number { get; set; }
        public int StartTime { get; set; }
        public int TotalTime { get; set; }
        public int Fare { get; set; }
        public List<Arc> Arcs { get; set; }
    }

    public class Arc
    {
        public int StartStation { get; set; }
        public int EndStation { get; set; }
        public int Time { get; set; }
        public Bus Bus { get; set; }
    }

    public enum WayType
    {
        Fast = 1,
        Cheap = 2
    }
}
