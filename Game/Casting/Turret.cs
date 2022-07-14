using genie.cast;

namespace asteroid.cast {
    class Turret : Actor {

        public Turret(string path, int width, int height,
                        float x = 0, float y = 0,
                        float vx = 0, float vy = 0,
                        float rotation = 0, float rotationVel = 0) : 
        base(path, width, height, x, y, vx, vy, rotation, rotationVel)
        {

        }
    }
    // public void SetRotation(float rotation) {
    //         if (this.rotation >= 360 || this.rotation <= -360) {
    //            rotation == 0; 
    //         }
    //     }
}