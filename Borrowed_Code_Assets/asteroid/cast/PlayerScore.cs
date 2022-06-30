using genie.cast;

namespace asteroid.cast {
    class PlayerScore : Actor {

        private int score;

        public PlayerScore(string path, int width = 0, int height = 0,
                        float x = 0, float y = 0,
                        float vx = 0, float vy = 0,
                        float rotation = 0, float rotationVel = 0,
                        
                        int score = 0) :
        base(path, width, height, x, y, vx, vy, rotation, rotationVel) {
            
            this.score = score;
        }

        public void SetScore(int score) {
            this.score = score;
        }

        public int GetScore() {
            return this.score;
        }

        public void AddScore(int points) {
            this.score += points;
        }

        public void Penalize(int points) {
            this.score -= points;
        }
    }
}