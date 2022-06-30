using genie;
using genie.cast;
using genie.script;
using genie.services;
using genie.services.raylib;

namespace asteroid.script
{
    class HandleShootingAction : genie.script.Action
    {

        private RaylibKeyboardService keyboardService;
        private genie.cast.Actor? ship;
        private DateTime lastBulletSpawn;
        private RaylibAudioService audioService;
        private float attackInterval;
        private (float vx, float vy) bulletVel;

        public HandleShootingAction(int priority, float attackInterval, (float, float) bulletVel,
                                    RaylibKeyboardService keyboardService,
                                    RaylibAudioService audioService) : base(priority)
        {
            this.ship = null;
            this.lastBulletSpawn = DateTime.Now;
            this.attackInterval = attackInterval;
            this.bulletVel = bulletVel;
            this.keyboardService = keyboardService;
            this.audioService = audioService;
        }

        private void SpawnBullet(Clock clock, Cast cast) {
            TimeSpan timeSinceLastShot = DateTime.Now - this.lastBulletSpawn;
            if (this.ship != null && timeSinceLastShot.TotalSeconds >= this.attackInterval) {
                // Bullet's starting position should be right on top of the ship
                float bulletX = this.ship.GetX();
                float bulletY = this.ship.GetY() - (this.ship.GetHeight()/2);

                // Create the bullet and put it in the cast
                Actor bullet = new Actor("./asteroid/assets/bullet.png", 20, 30, bulletX, bulletY, this.bulletVel.vx, this.bulletVel.vy);
                cast.AddActor("bullets", bullet);
                
                // Play the shooting sound :)
                this.audioService.PlaySound("asteroid/assets/sound/bullet_shot.wav", (float) 0.1);

                // Reset lastBulletSpawn to Now
                this.lastBulletSpawn = DateTime.Now;

            }
        }

        public override void execute(Cast cast, Script script, Clock clock, Callback callback)
        {
            // Grab the ship from the cast
            this.ship = cast.GetFirstActor("ship");

            // If the space key is down, spawn a new bullet
            if (this.keyboardService.IsKeyDown(Keys.SPACE)) {
                this.SpawnBullet(clock, cast);
            }
        }
    }
}