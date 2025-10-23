using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Robot2
{
    internal class Mobilrobot
    {
        static int max_X;
        static int max_Y;

        string id, model, mode;
        int x, y, charge;
        double speed;

        public string Id { get { return id; } }
        public string Model { get { return model; } }
        public string Mode
        {
            get { return mode; }
            set { 
                if(value == "auto" || value == "manual" || value == "dock")
                {
                    mode = value;
                }
                
               }
        }
        public int X { get { return x; } }
        public int Y { get { return x; } }

        public double Speed
        {
            get { return speed; }
            set
            {
                if (value >= 0)

                   speed = value;
            }
        }
        public int Charge
        {
            get { {  return charge; } }
            set
            {
                if(value >= 0 && value <= 100)
                {
                   charge = value;
                }
            }
        }








        static Mobilrobot()
        {
            max_X = 10;
            max_Y = 10;
        }
        public Mobilrobot(string line)
        {
            string[] data = line.Split('#');
            id = data[0];
            model = data[1];
            speed = double.Parse(data[2]);
            mode = data[3];
            this.x = 0;
            this.y = 0;
            this.charge = 100;
        }
        public string Display()
        {
            return $"ID: - {id}\n" +
                $"Model - {model} \n" +
                $"Speed - {speed}[m/s]\n" +
                $"Mode - {mode}\n" +
                $"Charge - {charge}%\n" +
                $"X - {x} ||Y - {y}\n";

            
        }
        public void changePos(int dx, int dy)
        {
            int uj_X = x + dx;
            int uj_Y = y + dy;
            if(uj_X >= 0 && uj_X <= max_X && uj_Y >= 0 && uj_Y <= max_Y)
            {
                x = uj_X;
                y = uj_Y;
            }
        }

       

             


    }
}
