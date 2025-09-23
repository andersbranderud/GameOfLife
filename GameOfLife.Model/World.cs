using System;

namespace GameOfLife.Model
{
    public class World
    {
        public World()
        { }

        // Create world with width and height
        public World(int width, int height)
        {

        }

        // Create world with a fixed starting pattern
        public World(string pattern)
        {

        }

        // Runs every tick
        public void Render()
        {
            Console.Write("\rRender World!");
        }
    }
}
