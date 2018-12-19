using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tron
{
    public class Tron
    {
        private Renderer renderer;
        private DoubleBuffer2D<bool> gameWorld;

        public Tron(int xdim, int ydim, Renderer renderer)
        {
            // Initialize double buffer where we store the game world
            gameWorld = new DoubleBuffer2D<bool>(xdim, ydim);

            // Save renderer into variable
            this.renderer = renderer;

            // Initialize world
            for (int i = 0; i < xdim; i++)
            {
                for (int j = 0; j < ydim; j++)
                {
                    // All tiles are set to false until players step on them
                    gameWorld[i, j] = false; 
                }
            }

            // Swap buffer arrays so we can read values from it
            gameWorld.Swap();
        }

        public void Gameloop()
        {
            renderer.RenderGameWorld(gameWorld);
        }

        public void Update()
        {

        }
    }
}
