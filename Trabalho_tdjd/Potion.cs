using Microsoft.Xna.Framework;

namespace Trabalho_tdjd
{
    public class Potion
    {
        private Point position;
        public Point Position => position;
        public Potion(int x, int y)
        {
            position = new Point(x, y);
        }
    }
}
