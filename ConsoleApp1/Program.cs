using System;
using System.IO;
namespace ConsoleApp1
{
    //
    class MainClass
    {

        class Tr
        {
            public struct x_y
            {
                public double x, y; // можно использовать класс Point
            }

            public double degrees, speed, angle;

            public void angle_set(double degrees)
            {
                angle = (Math.PI * degrees / 180);
            }

            public x_y get_Tr(double time, double x_speed, double y_speed, x_y def_coor)
            {
                def_coor.x = def_coor.x + x_speed * time;
                def_coor.y = def_coor.y + y_speed * time;
                if (def_coor.y <= 0)
                    def_coor.y = 0;

                return (def_coor);
            }

            public void find_dot(double time, double x_speed, double y_speed, x_y def_coor)
            {
                /*x_y coor; 
                coor.x = speed * time * Math.Cos(angle); ;
                coor.y = speed * time * Math.Sin(angle) - 4.9 * time * time;
                if (coor.y <= 0)
                    coor.y = 0;
                */
                
                Console.WriteLine("{0,6:F2} {1,6:F2}" ,get_Tr(time, x_speed, y_speed, def_coor).x ,get_Tr(time, x_speed, y_speed, def_coor).y);
                
               
                return;
            }
            public double K_tx(double time)
            {
                return 4*Math.Cos(time);
            }
            public double K_ty(double time)
            {
                return 4*Math.Sin(time);
            }

            public void Tr_paint(double time, double steps)
            {
                x_y def_coor;
                def_coor.x = 0;
                def_coor.y = 0;
                double mass = 1;

                double x_speed = speed * Math.Cos(angle);
                double y_speed = speed * Math.Sin(angle);
                string writePath = @"../../../Txt.txt";
                find_dot(0, x_speed, y_speed, def_coor);
                for (double i = 0; i < time; i += steps)
                {
                                     
                    find_dot(steps, x_speed, y_speed, def_coor);
                    def_coor = get_Tr(steps, x_speed, y_speed, def_coor);
                    x_y point = get_Tr(i, x_speed, y_speed, def_coor);
                    try
                    {
                        using (StreamWriter sw = new StreamWriter(writePath, true, System.Text.Encoding.Default))
                        {
                            sw.WriteLine(point.x);
                            sw.WriteLine(point.y);
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    x_speed = x_speed - steps * K_tx(i)*x_speed/mass;
                    y_speed = y_speed - steps * (K_ty(i)* y_speed/mass+9.8);

                }
                
            }

            public Tr(double degrees, double speed)
            {
                this.degrees = degrees;
                this.speed = speed;
                angle_set(degrees);
            }
        }
           


        public static void Main(string[] args)
        {
            // Хорошо бы было еще предусмотреть чтение из файла начальных данных
            Tr Gr = new Tr(60, 30);
            Gr.Tr_paint(10, 0.1);
        }
    }
}
