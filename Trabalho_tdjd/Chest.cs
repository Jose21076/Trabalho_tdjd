using Microsoft.Xna.Framework;

namespace Trabalho_tdjd
{
    public class Chest
    {
        private Point position;
        public Point Position => position;

        public int stored_p = 0;
        public Chest(int x, int y)
        {
            position = new Point(x, y);
        }

        public void add_potions(int potions)
        {
            stored_p += potions;
        }
    }
}
