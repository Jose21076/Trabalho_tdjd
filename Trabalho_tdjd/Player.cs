using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Trabalho_tdjd
{
    public class Player
    {
        private Point position;
        private Game1 game;
        private bool keysReleased = true;
        public int inventory_p = 0;
        public Point Position => position;
        private int speed = 2;
        private int delta = 0;

        public Player(Game1 game1, int x, int y)
        {
            position = new Point(x, y);
            game = game1;
        }

        public void Update(GameTime gameTime)
        {
            if (delta > 0)
            {
                delta = (delta + speed) % game.tileSize;
            }
            else
            {
                KeyboardState kState = Keyboard.GetState();
                Point lastposition = position;
                if (!game.gameOver)
                {
                    if (kState.IsKeyDown(Keys.A) || (kState.IsKeyDown(Keys.Left)))
                    {
                        position.X--;
                        delta = speed;
                        //if (!game.enemie.SearchPlayer(position)) game.enemie.move();
                    }
                    else if (kState.IsKeyDown(Keys.W) || (kState.IsKeyDown(Keys.Up)))
                    {
                        position.Y--;
                        delta = speed;
                        //if (!game.enemie.SearchPlayer(position)) game.enemie.move();
                    }
                    else if (kState.IsKeyDown(Keys.S) || (kState.IsKeyDown(Keys.Down)))
                    {
                        position.Y++;
                        delta = speed;
                        //if (!game.enemie.SearchPlayer(position)) game.enemie.move();
                    }
                    else if (kState.IsKeyDown(Keys.D) || (kState.IsKeyDown(Keys.Right)))
                    {
                        position.X++;
                        delta = speed;
                        //if (!game.enemie.SearchPlayer(position)) game.enemie.move();
                    }
                }

                if (game.HasPotion(position.X, position.Y))
                {
                    for (int i = 0; i < game.potions.Count; i++)
                    {
                        if (game.potions[i].Position.X == position.X && game.potions[i].Position.Y == position.Y)
                        {
                            game.potions.Remove(game.potions[i]);
                        }
                    }
                    inventory_p++;
                }

                if (game.chest.Position == position)
                {
                    position = lastposition;
                    game.chest.add_potions(inventory_p);
                    inventory_p = 0;
                }

                if (!FreeTile(position.X, position.Y))
                {
                    position = lastposition;
                }
            }
        }


        public bool FreeTile(int x, int y)
        {
            if ((game.level[x, y] == 'W') || (game.level[x, y] == 'V') || (game.level[x, y] == 'B')) return false;
            return true;
        }

    }
}
