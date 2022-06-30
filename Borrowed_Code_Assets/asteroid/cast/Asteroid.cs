
namespace asteroid.cast {
    class Asteroid : HasLifeActor {

        private int points;

        public Asteroid(string path, int width, int height,
                        float x = 0, float y = 0,
                        float vx = 0, float vy = 0,
                        float rotation = 0, float rotationVel = 0,
                        
                        int healthBarYOffset = 0,
                        int healthBarHeight = 5,
                        int maxHP = 0,
                        bool showTextHealth = false,
                        
                        int points = 0) :
        base(path, width, height, x, y, vx, vy, rotation, rotationVel,
                    healthBarYOffset, healthBarHeight, maxHP, showTextHealth) {
            
            this.points = points;
        }

        public void SetPoints(int points) {
            this.points = points;
        }

        public int GetPoints() {
            return this.points;
        }
    }
}