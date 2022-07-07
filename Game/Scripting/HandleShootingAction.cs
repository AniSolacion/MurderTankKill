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
        private genie.cast.Actor? tank;
        private DateTime lastBulletSpawn;
        private RaylibAudioService audioService;
        private float attackInterval;
        private (float vx, float vy) bulletVel;

        

        public HandleShootingAction(int priority, float attackInterval, (float, float) bulletVel,
                                    RaylibKeyboardService keyboardService,
                                    RaylibAudioService audioService) : base(priority)
        {
            // this.tank = null;
            this.lastBulletSpawn = DateTime.Now;
            this.attackInterval = attackInterval;
            this.bulletVel = bulletVel;
            this.keyboardService = keyboardService;
            this.audioService = audioService;
        }

        private void SpawnBullet(Clock clock, Cast cast) {

            Actor tank1 = cast.GetFirstActor("tank1");
            Actor tank2 = cast.GetFirstActor("tank2");

            TimeSpan timeSinceLastShot = DateTime.Now - this.lastBulletSpawn;
            if (tank1 != null && timeSinceLastShot.TotalSeconds >= this.attackInterval) {
                // Bullet's starting position should be the direction of the turret
                float bulletX = tank1.GetX();
                float bulletY = tank1.GetY() - (tank1.GetHeight()/2);

                // Create the bullet and put it in the cast
                Actor bullet = new Actor("./asteroid/assets/bullet.png", 20, 30, bulletX, bulletY, this.bulletVel.vx, this.bulletVel.vy);
                cast.AddActor("bullets", bullet);
                
                // Play the shooting sound :)
                this.audioService.PlaySound("asteroid/assets/sound/bullet_shot.wav", (float) 0.1);

                // Reset lastBulletSpawn to Now
                this.lastBulletSpawn = DateTime.Now;
            }

            if (tank2 != null && timeSinceLastShot.TotalSeconds >= this.attackInterval) {
                // Bullet's starting position should be the direction of the turret
                float bulletX = tank2.GetX();
                float bulletY = tank2.GetY() - (tank2.GetHeight()/2);

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
            // Grab the tank from the cast
            // this.tank = cast.GetFirstActor("tank");
            Actor tank1 = cast.GetFirstActor("tank1");
            Actor tank2 = cast.GetFirstActor("tank2");

            // If the space key is down, spawn a new bullet
            if (this.keyboardService.IsKeyDown(Keys.V)) {
                this.SpawnBullet(clock, cast);
            }
            if (this.keyboardService.IsKeyDown(Keys.B)) {
                this.SpawnBullet(clock, cast);
            }
        }
    }
}