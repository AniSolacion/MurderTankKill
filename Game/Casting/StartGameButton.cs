using genie.cast;

namespace asteroid.cast {
    class StartGameButton : Actor
    {
        //This class creates a button and it's list of attributes to use in actor.

        public StartGameButton(string path, int width, int height,
                        float x = 0, float y = 0,
                        float vx = 0, float vy = 0,
                        float rotation = 0, float rotationVel = 0) :
        base(path, width, height, x, y, vx, vy, rotation, rotationVel)
        {

        }
    }
}