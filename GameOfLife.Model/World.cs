namespace GameOfLife.Model
{
    public class World
    {
        private int[,] currentState = null;
        private bool _replaceZerosWithBlanks = false;

        public World()
        { 
            currentState = MapUtility.GenerateWorld(10, 10);
        }

        public void InitWorld(int width, int length, string gridArray = "", bool replaceZerosWithBlanks = false)
        {
            _replaceZerosWithBlanks = replaceZerosWithBlanks;

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
            MapUtility.PrintOutCurrentState(currentState, _replaceZerosWithBlanks);
            currentState = StateHelper.GenerateNewGameOfLifeState(currentState);
        }
    }
}
