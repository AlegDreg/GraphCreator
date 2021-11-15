using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs
{
    public class Graph : Actions
    {
        private string path;
        private string res_path
        {
            get
            {
                return path;
            }
            set
            {
                if (value != null)
                    path = value;
            }
        }

        private int[] nums { get; set; }

        /// <summary>
        /// Цвет линий графика
        /// </summary>
        private Color PenColor { get; set; }

        /// <summary>
        /// Цвет пунктирных линий
        /// </summary>
        private Color DashColor { get; set; }

        /// <summary>
        /// Цвет подписей пунктирных линий
        /// </summary>
        private Color TextColor { get; set; }
        public Exception Excep { get; private set; }

        /// <summary>
        /// Инициализирует класс
        /// </summary>
        /// <param name="num">Массив точек</param>
        /// <param name="path">Путь сохранения графика</param>
        /// <param name="color">Цвет линии графика</param>
        /// <param name="dash">Цвет пунктирных линий</param>
        /// <param name="text">Цвет текста</param>
        public Graph(int[] num, string path, Color color, Color dash, Color text)
        {
            nums = num;
            res_path = path;
            PenColor = color;
            DashColor = dash;
            TextColor = text;
        }

        /// <summary>
        /// Инициализирует класс
        /// </summary>
        /// <param name="num">Массив точек</param>
        /// <param name="color">Цвет линии графика</param>
        /// <param name="dash">Цвет пунктирных линий</param>
        /// <param name="text">Цвет текста</param>
        public Graph(int[] num, Color color, Color dash, Color text)
        {
            nums = num;
            res_path = Directory.GetCurrentDirectory() + @"\chart.jpg";
            PenColor = color;
            DashColor = dash;
            TextColor = text;
        }

        /// <summary>
        /// Возвращает путь к созданному графику
        /// </summary>
        /// <returns></returns>
        public string GetGraphPath()
        {
            return res_path;
        }

        /// <summary>
        /// Создание графика
        /// </summary>
        /// <returns>true - если график создан, false - ошибка создания графика</returns>
        public bool Create()
        { 
            int h = GetLenght(nums) * 2;

            int w = h;

            int dif = w / nums.Length;
            int min = h + GetMin(nums);
            int lenghtText = w / 20;
            int thick = w / 300;
            if (dif == 0)
                dif = 1;

            Bitmap myBitmap;

            try
            {
                myBitmap = new Bitmap(w + lenghtText, h);
            }
            catch(Exception ex)
            {
                Excep = ex;
                return false;
            }

            using (Graphics g = Graphics.FromImage(myBitmap))
            {
                Pen p = new Pen(PenColor, thick);

                Point p1;
                Point p2;

                for (int i = 1; i < nums.Length; i++)
                {
                    p1 = new Point((i - 1) * dif + lenghtText, min - nums[i - 1]);

                    p2 = new Point(i * dif + lenghtText, min - nums[i]);

                    g.DrawLine(p, p1, p2);
                }

                DrawVerticalLine(g, thick, lenghtText, h);

                DrawDashes(g, thick, min, w - lenghtText,
                    new int[] {
                        nums[nums.Length - 1],
                        nums[nums.Length / 2],
                        GetMax(nums) });

                DrawCaptions(g, min, lenghtText, new int[] {
                        nums[nums.Length - 1],
                        nums[nums.Length / 2],
                        GetMax(nums) });

                myBitmap.Save(res_path);
                myBitmap.Dispose();
            }

            return true;
        }

        /// <summary>
        /// Создание подписей линий
        /// </summary>
        /// <param name="graphics">Экземпляр класса Graphics</param>
        /// <param name="min">Начало отсчёта</param>
        /// <param name="lenghtText">Толщина линий</param>
        /// <param name="vs">Массив точек y</param>
        private void DrawCaptions(Graphics graphics, int min, int lenghtText, int[] vs)
        {
            Font font = new Font("Arial", lenghtText / 5.5f);
            SolidBrush drawBrush = new SolidBrush(TextColor);

            for (int i = 0; i < vs.Length; i++)
            {
                graphics.DrawString(vs[i].ToString(), font, drawBrush, 0, (int)(min - vs[i]));
            }
        }

        /// <summary>
        /// Создание вертикальной линии
        /// </summary>
        /// <param name="graphics">Экземпляр класса Graphics/param>
        /// <param name="thick">Толщина линий</param>
        /// <param name="len">Смещение графика для создания вертикальной линии</param>
        /// <param name="h">Высота графика</param>
        private void DrawVerticalLine(Graphics graphics, int thick, int len, int h)
        {
            graphics.DrawLine(new Pen(DashColor, thick / 2), new Point(len, 0), new Point(len, h));
        }

        /// <summary>
        /// Создание пунктирных линий
        /// </summary>
        /// <param name="graphics">Экземпляр класса Graphics</param>
        /// <param name="thick">Толщина линий</param>
        /// <param name="min">Начало отсчёта</param>
        /// <param name="width">Ширина графика</param>
        /// <param name="verticalPoints">Массив точек y</param>
        private void DrawDashes(Graphics graphics, int thick, int min, int width, int[] verticalPoints)
        {
            Pen pen = new Pen(DashColor, thick / 2);
            Point p1;
            Point p2;

            int l = 15;
            double lenSegment = width / l;

            for (int i = 0; i < verticalPoints.Length; i++)
            {
                for (int j = 1; j < l * 2; j++)
                {
                    if (j % 2 == 0)
                    {
                        p1 = new Point((int)((j - 1) * lenSegment), min - verticalPoints[i]);
                        p2 = new Point((int)(j * lenSegment), min - verticalPoints[i]);

                        graphics.DrawLine(pen, p1, p2);
                    }
                }
            }
        }
    }
}
