namespace GameOfLife.Model
{
    public class World
    {
        int[,] currentState = null;
        public World()
        { 
            currentState = MapUtility.GenerateWorld(10, 10);
        }

        public void InitWorld(int width, int length, string gridArray = "")
        {
            if (gridArray == MapUtility.GliderPattern)
            {
                currentState = MapUtility.GenerateWorldBasedOnPattern(gridArray);
            }
            else if (!string.IsNullOrEmpty(gridArray))
            {
                currentState = MapUtility.GenerateWorld(gridArray, width, length);
            }            
            else
            {
                currentState = MapUtility.GenerateWorld(width, length);
            }
        }

        // Runs every tick
        public void Render()
        {
            MapUtility.PrintOutCurrentState(currentState);
            currentState = StateHelper.GenerateNewGameOfLifeState(currentState);
        }
    }
}
