using genie.cast;

namespace asteroid.cast {
    class Bullet : Actor {

        public Bullet(string path, int width, int height,
                        float x = 0, float y = 0,
                        float vx = 0, float vy = 0,
                        float rotation = 0, float rotationVel = 0,
                        int counter = 0) : 
        base(path, width, height, x, y, vx, vy, rotation, rotationVel, counter)
        {
            
        }
    }
}