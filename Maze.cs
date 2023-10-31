using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rogueLike
{
    internal class Maze
    {
        public static String[,] Grid =
        {
            {""},
            {""}
        };
        private String ground = constants.ground;
        private String wall = constants.wall;
        private String room = constants.room;
        private int _numberOfRooms; // количество комнат
        // Размеры комнаты(площади удаления)
        private int rheight = 7, rwidth = 5, 
            roomsPercentage = 3; // коэффициент заполненности лабиринта комнатами
        private Random rnd = new Random();
        private int _sizeX, _sizeY;

        public Maze ()
        {
            sizeRandomizer();
            roomsNumberSelection();
            Grid = new String[_sizeX, _sizeY];
            Grid = Generate(_sizeY, _sizeX, _numberOfRooms);
        }
        public Maze(int sizeX, int sizeY, int numberOfRooms)
        {
            _sizeX = toOdd(sizeX);
            _sizeY = toOdd(sizeY);
            Grid = new String[_sizeX, _sizeY];
            Grid = Generate(_sizeY, _sizeX, numberOfRooms);
        }
        public Maze(int sizeX, int sizeY)
        {
            _sizeX = toOdd(sizeX);
            _sizeY = toOdd(sizeY);
            roomsNumberSelection();
            Grid = new String[_sizeX, _sizeY];
            Grid = Generate(_sizeY, _sizeX, _numberOfRooms);
        }

        private void sizeRandomizer()
        {
            this._sizeX = toOdd(rnd.Next(30, 120));
            this._sizeY = toOdd(rnd.Next(20, 29));
        }

        private void roomsNumberSelection()
        {
            _numberOfRooms = ((_sizeX * _sizeY) / (rheight * rwidth)) / roomsPercentage;
        }

        private int toOdd(int number)
        {
            return number % 2 == 0 ? number - 1 : number;
        }
        
        // Отсюда не мой код. Попытался адаптировать под c#, изначально был на с++

        private bool deadEnd(int x, int y, String[,] maze, int height, int width)
        {
            int a = 0;
                if (x != 1 && x - 2 >= 0)
            {
                if (maze[y,x - 2] == ground)
                    a += 1;
            }
            else a += 1;

            if (y != 1 && y - 2 >= 0)
            {
                if (maze[y - 2,x] == ground)
                    a += 1;
            }
            else a += 1;

            if (x != width - 2 && x + 2 < width)
            {
                if (maze[y,x + 2] == ground)
                    a += 1;
            }
            else a += 1;

            if (y != height - 2 && y + 2 < height)
            {
                if (maze[y + 2,x] == ground)
                    a += 1;
            }
            else a += 1;

            if (a == 4)
                return true;
            else
                return false;
        }


        public String[,] Generate(int height, int width, int k)
        {
            int x, y, c, a;
            bool b, swap = true;
            var rnd = new Random();
            String[,] maze = new String[height,width]; 
            for (int i = 0; i < height; i++) // Массив заполняется землей-ноликами
                for (int j = 0; j < width; j++)
                    maze[i,j] = wall;
            rheight--; rwidth--;
            x = 3; y = 3; a = 0; // Точка приземления крота и счетчик

            while (a < 10000)
            { // Да, простите, костыль, иначе есть как, но лень
                maze[y,x] = ground; a++;
                while (true)
                { // Бесконечный цикл, который прерывается только тупиком
                    c = rnd.Next() % 4; // Напоминаю, что крот прорывает
                    switch (c)
                    {  // по две клетки в одном направлении за прыжок
                        case 0:
                            if (y != 1 && y - 2 >= 0)
                                if (maze[y - 2, x] == wall)
                                { // Вверх
                                    maze[y - 1, x] = ground;
                                    maze[y - 2, x] = ground;
                                    y -= 2;
                                }
                            break;
                        case 1:
                            if (y != height - 2 && y + 2 < height)
                                if (maze[y + 2, x] == wall)
                                { // Вниз
                                    maze[y + 1, x] = ground;
                                    maze[y + 2, x] = ground;
                                    y += 2;
                                }
                            break;
                        case 2:
                            if (x != 1 && x - 2 >= 0)
                                if (maze[y, x - 2] == wall)
                                { // Налево
                                    maze[y, x - 1] = ground;
                                    maze[y, x - 2] = ground;
                                    x -= 2;
                                }
                            break;
                        case 3:
                            if (x != width - 2 && x + 2 < width)
                                if (maze[y, x + 2] == wall)
                                { // Направо
                                    maze[y, x + 1] = ground;
                                    maze[y, x + 2] = ground;
                                    x += 2;
                                }
                            break;
                    }
                    if (deadEnd(x, y, maze, height, width))
                    break;

                    
                }

                
                if (deadEnd(x, y, maze, height, width))
                { // Вытаскиваем крота из тупика
                    do
                    {
                        x = 2 * (rnd.Next() % ((width - 1) / 2)) + 1;
                        y = 2 * (rnd.Next() % ((height - 1) / 2)) + 1;
                    }
                    while (maze[y, x] != ground);
                    
                }
            } // На этом и все.

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
                            if (maze[i,j] == room)
                                b = false;

                    if (b)
                        continue;

                    for (int i = (y - rheight / 2); i < (y + rheight / 2 + 1); i++) // Раскопки комнаты
                        for (int j = (x - rwidth / 2); j < (x + rwidth / 2 + 1); j++)
                            maze[i,j] = room;

                    c = rnd.Next() % 4; // Дверь в комнату, определяем в какую стену
                                    // Нижняя, верхняя, правая и левая соответственно
                                    // Нагромождение в виде rand()... нужно для того, чтобы дверь стояла в разных
                                    // местах стены
                    if (c == 0) maze[y + rheight / 2 + 1, x - rwidth / 2 + 2 * (rnd.Next() % (rwidth / 2 + 1))] = room;
                    if (c == 1) maze[y - rheight / 2 - 1, x - rwidth / 2 + 2 * (rnd.Next() % (rwidth / 2 + 1))] = room;
                    if (c == 2) maze[y - rheight / 2 + 2 * (rnd.Next() % (rheight / 2 + 1)),x + rwidth / 2 + 1] = room;
                    if (c == 3) maze[y - rheight / 2 + 2 * (rnd.Next() % (rheight / 2 + 1)),x - rwidth / 2 - 1] = room;
                    // swap отвечает за возможность поворачивать комнату на 90°
                    if (swap)
                    {
                        rheight += rwidth;
                        rwidth = rheight - rwidth;
                        rheight -= rwidth;
                    } // Вот так настоящие мужики меняют переменные значениями
                }
            }

            // Вставка символа выхода/цели
            // в случае если сверху выхода стена
            var T = rnd.Next(1, width - 2);
            if (maze[height - 2, T] != "█")
                maze[height - 1, T] = "X";
            else
            if ((T - 1) != 0)
                maze[height - 1, T - 1] = "X";
            else
            if ((T - 1) != width - 1)
                maze[height - 1, T + 1] = "X";
            return maze;
        }
        
    }
}
