using System;
using System.Threading;

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

            // Swap buffer arrays so we can read values from our Double Buffer
            gameWorld.Swap();
        }

        public void Gameloop(int msFramesPerSecond)
        {
            renderer.RenderGameWorld(gameWorld);

            // Initialize game loop
            while (true)
            {
                // Obtain actual time in ticks
                long start = DateTime.Now.Ticks;

                // Update world
                Update();

                // Swap buffer
                gameWorld.Swap();
                // Send it to renderer
                renderer.RenderGameWorld(gameWorld);

                // Wait until it is time for the next iteration
                Thread.Sleep((int)
                    (start / 10000 + msFramesPerSecond 
                    - DateTime.Now.Ticks / 10000));
            }
        }

        public void Update()
        {

        }
    }
}
