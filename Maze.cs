using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rogueLike
{
    internal class Maze
    {
        private String[,] Grid =
        {
            {""},
            {""}
        };
        private String ground = " ";
        private String wall = "█";
        private String room = ".";
        private int _numberOfRooms = 3; // количество комнат
        // Размеры комнаты(площади удаления)
        private int rheight = 7, 
                    rwidth = 5, 
                    roomsPercentage = 3; // коэффициент заполненности лабиринта комнатами
        private Random rnd = new Random();
        private int _sizeX = 20, _sizeY = 20;

        public Maze ()
        {
            SizeRandomizer();
            RoomsNumberSelection();
            Grid = new String[_sizeX, _sizeY];
            GenerateMaze(_sizeY, _sizeX, _numberOfRooms);
        }
        public Maze(int sizeX, int sizeY, int numberOfRooms)
        {
            _sizeX = ToOdd(sizeX);
            _sizeY = ToOdd(sizeY);
            Grid = new String[_sizeX, _sizeY];
            GenerateMaze(_sizeY, _sizeX, numberOfRooms);
        }
        public Maze(int sizeX, int sizeY)
        {
            _sizeX = ToOdd(sizeX);
            _sizeY = ToOdd(sizeY);
            RoomsNumberSelection();
            Grid = new String[_sizeX, _sizeY];
            GenerateMaze(_sizeY, _sizeX, _numberOfRooms);
        }


        public String[,] GetGrid()
        {
            return Grid;
        }

        private void SizeRandomizer()
        {
            this._sizeX = ToOdd(rnd.Next(30, 120));
            this._sizeY = ToOdd(rnd.Next(20, 29));
        }

        private void RoomsNumberSelection()
        {
            _numberOfRooms = ((_sizeX * _sizeY) / (rheight * rwidth)) / roomsPercentage;
        }

        private int ToOdd(int number)
        {
            return number % 2 == 0 ? number - 1 : number;
        }

        private bool DeadEnd(int x, int y, String[,] Grid, int height, int width)
        {
            int a = 0;
                if (x != 1 && x - 2 >= 0)
            {
                if (Grid[y,x - 2] == ground)
                    a += 1;
            }
            else a += 1;

            if (y != 1 && y - 2 >= 0)
            {
                if (Grid[y - 2,x] == ground)
                    a += 1;
            }
            else a += 1;

            if (x != width - 2 && x + 2 < width)
            {
                if (Grid[y,x + 2] == ground)
                    a += 1;
            }
            else a += 1;

            if (y != height - 2 && y + 2 < height)
            {
                if (Grid[y + 2,x] == ground)
                    a += 1;
            }
            else a += 1;

            if (a == 4)
                return true;
            else
                return false;
        }

        private void GenerateBase(int height, int width)
        {
            for (int i = 0; i < height; i++) // Массив заполняется стеной
                for (int j = 0; j < width; j++)
                    Grid[i, j] = wall;
        }

        private void DigTunnels(int height, int width)
        {
            int x = 3, y = 3, a = 0; // Точка приземления крота и счетчик
            while (a < 10000)
            { // Да, простите, костыль, иначе есть как, но лень
                Grid[y, x] = ground; a++;
                while (true)
                { // Бесконечный цикл, который прерывается только тупиком
                    int c = rnd.Next() % 4; // Напоминаю, что крот прорывает
                    switch (c)
                    {  // по две клетки в одном направлении за прыжок
                        case 0:
                            if (y != 1 && y - 2 >= 0)
                                if (Grid[y - 2, x] == wall)
                                { // Вверх
                                    Grid[y - 1, x] = ground;
                                    Grid[y - 2, x] = ground;
                                    y -= 2;
                                }
                            break;
                        case 1:
                            if (y != height - 2 && y + 2 < height)
                                if (Grid[y + 2, x] == wall)
                                { // Вниз
                                    Grid[y + 1, x] = ground;
                                    Grid[y + 2, x] = ground;
                                    y += 2;
                                }
                            break;
                        case 2:
                            if (x != 1 && x - 2 >= 0)
                                if (Grid[y, x - 2] == wall)
                                { // Налево
                                    Grid[y, x - 1] = ground;
                                    Grid[y, x - 2] = ground;
                                    x -= 2;
                                }
                            break;
                        case 3:
                            if (x != width - 2 && x + 2 < width)
                                if (Grid[y, x + 2] == wall)
                                { // Направо
                                    Grid[y, x + 1] = ground;
                                    Grid[y, x + 2] = ground;
                                    x += 2;
                                }
                            break;
                    }
                    if (DeadEnd(x, y, Grid, height, width))
                        break;
                }
                if (DeadEnd(x, y, Grid, height, width))
                { // Вытаскиваем крота из тупика
                    do
                    {
                        x = 2 * (rnd.Next() % ((width - 1) / 2)) + 1;
                        y = 2 * (rnd.Next() % ((height - 1) / 2)) + 1;
                    }
                    while (Grid[y, x] != ground);
                }
            }
        }

        private void GenerateRooms(int height, int width, int k)
        {
            int x = 3, y = 3;
            bool b = true, swap = true;
            for (int l = 0; l < k; l++)
            {  // Генерация комнат
                b = true;
                while (b)
                {
                    do
                    { // Точка-центр комнаты
                        if (rwidth % 4 == 0) // Комнаты, с разной делимостью на 4 ведут себя 
                            x = 2 * (rnd.Next() % (width / 2)) + 1; // по разному, унифицируем
                        else
                            x = 2 * (rnd.Next() % (width / 2)) + 2;
                        if (rheight % 4 == 0)
                            y = 2 * (rnd.Next() % (height / 2)) + 1;
                        else
                            y = 2 * (rnd.Next() % (height / 2)) + 2;
                    }
                    while (x < (rwidth + 2) || x > (width - rwidth - 2) ||
                                y < (rheight + 2) || y > (height - rheight - 2));
                    b = false; // Комнаты не должны прикасаться
                    for (int i = (y - rheight - 2); i < (y + rheight + 2); i++)
                        for (int j = (x - rwidth - 2); j < (x + rwidth + 2); j++)
                            if (Grid[i, j] == room)
                                b = false;

                    if (b)
                        continue;

                    DigRoom( y,  x, swap);
                    // swap отвечает за возможность поворачивать комнату на 90°
                }
            }
        }

        private void DigRoom(int y, int x, bool swap)
        {
            for (int i = (y - rheight / 2); i < (y + rheight / 2 + 1); i++) // Раскопки комнаты
                for (int j = (x - rwidth / 2); j < (x + rwidth / 2 + 1); j++)
                    Grid[i, j] = room;
            if (rnd.Next() % 4 == 0) Grid[y + rheight / 2 + 1, x - rwidth / 2 + 2 * (rnd.Next() % (rwidth / 2 + 1))] = room;
            if (rnd.Next() % 4 == 1) Grid[y - rheight / 2 - 1, x - rwidth / 2 + 2 * (rnd.Next() % (rwidth / 2 + 1))] = room;
            if (rnd.Next() % 4 == 2) Grid[y - rheight / 2 + 2 * (rnd.Next() % (rheight / 2 + 1)), x + rwidth / 2 + 1] = room;
            if (rnd.Next() % 4 == 3) Grid[y - rheight / 2 + 2 * (rnd.Next() % (rheight / 2 + 1)), x - rwidth / 2 - 1] = room;
            RotateRoom(swap);
        }

        private void RotateRoom(bool swap)
        {
            // swap отвечает за возможность поворачивать комнату на 90°
            if (swap)
            {
                rheight += rwidth;
                rwidth = rheight - rwidth;
                rheight -= rwidth;
            } // Вот так настоящие мужики меняют переменные значениями
        }

        private void CreateExit(int height, int width)
        {
            // Вставка символа выхода/цели
            // в случае если сверху выхода стена 
            var T = rnd.Next(1, width - 2);
            if (Grid[height - 2, T] != "█")
                Grid[height - 1, T] = "X";
            else
            if ((T - 1) != 0)
                Grid[height - 1, T - 1] = "X";
            else
            if ((T - 1) != width - 1)
                Grid[height - 1, T + 1] = "X";
        }

        public void GenerateMaze(int height, int width, int k)
        {
            GenerateBase(width, height);

            DigTunnels(width, height);

            GenerateRooms(width, height, k);

            CreateExit(width, height);
        }
        
    }
}
