using System;

namespace GameOfLife.Model
{
    public class World
    {
        int[,] currentState = null;
        public World()
        { 
            currentState = MapGenerator.GenerateWorld(10, 10);
        }

        // Runs every tick
        public void Render()
        {
            MapGenerator.PrintOutCurrentState(currentState);
            currentState = StateHelper.GenerateNewGameOfLifeState(currentState);
            Console.Write("\rRender World!");
        }
    }
}
