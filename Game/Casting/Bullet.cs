using genie.cast;

namespace asteroid.cast {
    class Bullet : Actor {
        
        private int bounceCounter;

        public Bullet(string path, int width, int height,
                        float x = 0, float y = 0,
                        float vx = 0, float vy = 0,
                        float rotation = 0, float rotationVel = 0,
                        int bounceCounter = 0) : 
        base(path, width, height, x, y, vx, vy, rotation, rotationVel)
        {
            this.bounceCounter = 0;
        }

        public int getBulletCounter() {
            return bounceCounter;
        }

        public void setBulletCounter (int change) {
            this.bounceCounter = change;
        }
    }
}