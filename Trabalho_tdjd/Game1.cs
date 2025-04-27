using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;

namespace Trabalho_tdjd
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private int nrLinhas = 0;
        private int nrColunas = 0;
        private SpriteFont font;

        private string[] levels = { "level1.txt", "level2.txt" };
        private int currentLevel = 0;
        public char[,] level;

        public Player player;
        public Enemie enemie;
        public Chest chest;
        public List<Potion> potions;
        private int win_cond = 0;

        private Texture2D  player_t, enemie_t, background, chest_t, floor, potion, wall, wall_bot, wall_vert;
        public int tileSize = 32;

        public bool gameOver = false;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            LoadLevel(levels[currentLevel]);
            win_cond = potions.Count;
            _graphics.PreferredBackBufferHeight = tileSize * level.GetLength(1);
            _graphics.PreferredBackBufferWidth = tileSize * level.GetLength(0);
            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            font = Content.Load<SpriteFont>("File");

            player_t = Content.Load<Texture2D>("player");
            enemie_t = Content.Load<Texture2D>("enemie");
            chest_t = Content.Load<Texture2D>("chest");
            floor = Content.Load<Texture2D>("floor");
            potion = Content.Load<Texture2D>("potion");
            wall = Content.Load<Texture2D>("wall");
            wall_bot = Content.Load<Texture2D>("wall_bot");
            wall_vert = Content.Load<Texture2D>("wall_vert");
            background = Content.Load<Texture2D>("background");

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            player.Update(gameTime);
            enemie.setDiff(player.inventory_p, chest.stored_p);

            if (Keyboard.GetState().IsKeyDown(Keys.R))
            {
                Initialize();
                gameOver = false;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Escape)) Exit();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            // TODO: Add your drawing code here

            _spriteBatch.Begin();

            
            Rectangle back_rect = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
            _spriteBatch.Draw(background, back_rect, Color.White);
            Rectangle position = new Rectangle(0, 0, tileSize, tileSize);
            for (int x = 0; x < level.GetLength(0); x++)
            {
                for (int y = 0; y < level.GetLength(1); y++)
                {
                    position.X = x * tileSize;
                    position.Y = y * tileSize;

                    switch (level[x, y])
                    {
                        case 'Y':
                            if (InView(x, y)) _spriteBatch.Draw(floor, position, Color.White);
                            break;
                        case 'E':
                            if (InView(x, y)) _spriteBatch.Draw(floor, position, Color.White);
                            break;
                        case 'C':
                            if (InView(x, y))
                            {
                                _spriteBatch.Draw(floor, position, Color.White);
                                _spriteBatch.Draw(chest_t, position, Color.White);
                            }
                            break;
                        case 'F':
                            if (InView(x, y)) _spriteBatch.Draw(floor, position, Color.White);
                            break;
                        case 'P':
                            if (InView(x, y)) _spriteBatch.Draw(floor, position, Color.White);
                            break;
                        case 'W':
                            if (InView(x, y)) _spriteBatch.Draw(wall, position, Color.White);
                            break;
                        case 'B':
                            if (InView(x, y)) _spriteBatch.Draw(wall_bot, position, Color.White);
                            break;
                        case 'V':
                            if(InView(x, y)) _spriteBatch.Draw(wall_vert, position, Color.White);
                            break;
                    }
                }
            }

            

            foreach(Potion p in potions)
            {
                position.X = p.Position.X * tileSize;
                position.Y = p.Position.Y * tileSize;
                if (InView(p.Position.X,p.Position.Y)) _spriteBatch.Draw(potion, position, Color.White);
            }
            

            position.X = enemie.Position.X * tileSize;
            position.Y = enemie.Position.Y * tileSize;
            if (InView(enemie.Position.X, enemie.Position.Y))  _spriteBatch.Draw(enemie_t, position, Color.White);

            position.X = player.Position.X * tileSize;
            position.Y = player.Position.Y * tileSize;
            _spriteBatch.Draw(player_t, position, Color.White);
            
            _spriteBatch.DrawString(font, $"Potions in inventory: {player.inventory_p}", new Vector2(0, 0) * tileSize,Color.Yellow,0f,Vector2.Zero,1.3f,SpriteEffects.None,0f);
            _spriteBatch.DrawString(font, $"Potions in the chest: {chest.stored_p}", new Vector2(0, 1) * tileSize,Color.Green,0f,Vector2.Zero,1.3f,SpriteEffects.None,0f);
            _spriteBatch.DrawString(font, $"Difficulty: {enemie.Diff}", new Vector2(level.GetLength(0) / 2, 0) * tileSize, Color.Yellow, 0f, Vector2.Zero, 1.3f, SpriteEffects.None, 0f);

            if (chest.stored_p == win_cond)
            {
                gameOver = true;
                _spriteBatch.DrawString(font, "YOU WIN", new Vector2(level.GetLength(0) / 2, level.GetLength(1) / 2) * tileSize, Color.DarkGreen, 0f, Vector2.Zero, 1.3f, SpriteEffects.None, 0f);
                if (Keyboard.GetState().IsKeyDown(Keys.Space) && currentLevel <= levels.Length)
                {
                    currentLevel++;
                    Initialize();
                }
            }

            if(player.Position == enemie.Position)
            {
                gameOver = true;
                _spriteBatch.DrawString(font, "YOU LOSE", new Vector2(level.GetLength(0) / 2, level.GetLength(1) / 2) * tileSize, Color.DarkRed, 0f, Vector2.Zero, 1.3f, SpriteEffects.None, 0f);
            }
            _spriteBatch.End();

            base.Draw(gameTime);
        }

        void LoadLevel(string levelFile)
        {
            potions = new List<Potion>();
            string[] linhas = File.ReadAllLines($"Content/{levelFile}");
            nrLinhas = linhas.Length;
            nrColunas = linhas[0].Length;

            level = new char[nrColunas, nrLinhas];

            for (int x = 0; x < nrColunas; x++)
            {
                for (int y = 0; y < nrLinhas; y++)
                {
                    if (linhas[y][x] == 'Y')
                    {
                        player = new Player(this, x, y);
                        level[x, y] = ' ';
                    }
                    if (linhas[y][x] == 'E')
                    {
                        enemie = new Enemie(this, x, y);
                        level[x, y] = ' ';
                    }
                    if (linhas[y][x] == 'C')
                    {
                        chest = new Chest(x, y);
                        level[x, y] = 'C';
                    }
                    if (linhas[y][x] == 'P')
                    {
                        potions.Add(new Potion(x, y));
                        level[x, y] = 'P';
                    }
                    else
                    {
                        level[x, y] = linhas[y][x];
                    }
                }
            }
        }
        public bool InView(int x, int y)
        {
            if (currentLevel == 0)
            {
                if ((Math.Abs(player.Position.X - x) < 2) && (Math.Abs(player.Position.Y - y) < 2))
                {
                    return true;
                }
                return false;
            }
            else if (currentLevel == 1)
            {
                if ((Math.Abs(player.Position.X - x) < 4) && (Math.Abs(player.Position.Y - y) < 4))
                {
                    return true;
                }
                return false;
            }
            return false;
        }
        public bool HasPotion(int x, int y)
        {
            foreach (Potion p in potions)
            {
                if (p.Position.X == x && p.Position.Y == y) return true;
            }
            return false;
        }
        
    }
}
