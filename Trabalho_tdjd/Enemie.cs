using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trabalho_tdjd
{
    public class Enemie
    {
        private Point position;
        private int diff;
        public int Diff => diff;
        private Game1 game;
        public Point Position => position;
        public Enemie(Game1 game1, int x, int y) 
        {
            position = new Point(x, y);
            game = game1;
        }

        public void setDiff(int p_potions, int c_potions)
        {
            int total_p = p_potions + c_potions;
            if (total_p < 2) diff = 1;
            else if (total_p < 4) diff = 2;
            else if (total_p > 4) diff = 3;
        }
        public void move()
        {
            Random rx = new Random();
            Random ry = new Random();
            int nx, ny;
            int possible_x = position.X, possible_y = position.Y;
            
            switch (diff)
            {
                case 1:
                    nx = rx.Next(-1, 1);
                    ny = ry.Next(-1, 1);
                    if(nx == -1)
                    {
                        if(FreeTile_enemie(position.X - 1, position.Y)) position.X -=  1;
                    }
                    else if (nx == 1)
                    {
                        if (FreeTile_enemie(position.X + 1, position.Y)) position.X += 1;
                    }

                    if (ny == -1)
                    {
                        if (FreeTile_enemie(position.X, position.Y - 1)) position.Y -= 1;
                    }
                    else if (ny == 1)
                    {
                        if (FreeTile_enemie(position.X, position.Y + 1)) position.Y += 1;
                    }
                    break;
                case 2:
                    nx = rx.Next(-2, 2);
                    ny = ry.Next(-2, 2);
                    if(nx < 0)
                    {
                        for(int i = -1; i >= nx; i--)
                        {
                            if (FreeTile_enemie(position.X + i, position.Y)) possible_x = position.X + i;
                            else if (!FreeTile_enemie(position.X + i, position.Y)) break;
                        }
                    }
                    else if (nx > 0)
                    {
                        for (int i = 1; i <= nx; i++)
                        {
                            if (FreeTile_enemie(position.X + i, position.Y)) possible_x = position.X + i;
                            else if (!FreeTile_enemie(position.X + i, position.Y)) break;
                        }
                    }
                    position.X = possible_x;

                    if (ny < 0)
                    {
                        for (int i = -1; i >= ny; i--)
                        {
                            if (FreeTile_enemie(position.X, position.Y + i)) possible_y = position.Y + i;
                            else if (!FreeTile_enemie(position.X, position.Y + i)) break;
                        }
                    }
                    else if (ny > 0)
                    {
                        for (int i = 1; i <= ny; i++)
                        {
                            if (FreeTile_enemie(position.X, position.Y + i)) possible_y = position.Y + i;
                            else if (!FreeTile_enemie(position.X, position.Y + i)) break;
                        }
                    }
                    position.Y = possible_y;
                    break;
                case 3:
                    nx = rx.Next(-3, 3);
                    ny = ry.Next(-3, 3);
                    if (nx < 0)
                    {
                        for (int i = -1; i >= nx; i--)
                        {
                            if (FreeTile_enemie(position.X + i, position.Y)) possible_x = position.X + i;
                            else if (!FreeTile_enemie(position.X + i, position.Y)) break;
                        }
                    }
                    else if (nx > 0)
                    {
                        for (int i = 1; i <= nx; i++)
                        {
                            if (FreeTile_enemie(position.X + i, position.Y)) possible_x = position.X + i;
                            else if (!FreeTile_enemie(position.X + i, position.Y)) break;
                        }
                    }
                    position.X = possible_x;

                    if (ny < 0)
                    {
                        for (int i = -1; i >= ny; i--)
                        {
                            if (FreeTile_enemie(position.X, position.Y + i)) possible_y = position.Y + i;
                            else if (!FreeTile_enemie(position.X, position.Y + i)) break;
                        }
                    }
                    else if (ny > 0)
                    {
                        for (int i = 1; i <= ny; i++)
                        {
                            if (FreeTile_enemie(position.X, position.Y + i)) possible_y = position.Y + i;
                            else if (!FreeTile_enemie(position.X, position.Y + i)) break;
                        }
                    }
                    position.Y = possible_y;
                    break;
            }
        }


        public bool FreeTile_enemie(int x, int y)
        {
            if ((game.level[x, y] == 'F') || (game.level[x, y] == 'P') || (game.level[x, y] == 'E')) return true;
            return false;
        }

        public bool SearchPlayer(Point target)
        {
            switch (diff)
            {
                case 1:
                    for (int x = -1; x <= 1; x++)
                    {
                        for(int y = -1; y <= 1; y++)
                        {
                            if ((target.X == position.X + x) && (target.Y == position.Y + y))
                            {
                                position = target;
                                return true;
                            }
                        }
                    }
                    break;
                case 2:
                    for (int x = -2; x <= 2; x++)
                    {
                        for (int y = -2; y <= 2; y++)
                        {
                            if ((target.X == position.X + x) && (target.Y == position.Y + y))
                            {
                                position = target;
                                return true;
                            }
                                
                        }
                    }
                    break;
                case 3:
                    for (int x = -2; x <= 2; x++)
                    {
                        for (int y = -2; y <= 2; y++)
                        {
                            if ((target.X == position.X + x) && (target.Y == position.Y + y))
                            {
                                position = target;
                                return true;
                            }
                        }
                    }
                    break;
            }
            return false; 
        }
    }
}
