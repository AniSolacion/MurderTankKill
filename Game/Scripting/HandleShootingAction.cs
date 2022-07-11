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
        private genie.cast.Actor? tank2;
        private DateTime p1lastBulletSpawn;
        private DateTime p2lastBulletSpawn;
        private RaylibAudioService audioService;
        private float attackInterval;
        private (float vx, float vy) bulletVel;

        private int tankSelector = 0;

        

        public HandleShootingAction(int priority, float attackInterval, (float, float) bulletVel,
                                    RaylibKeyboardService keyboardService,
                                    RaylibAudioService audioService) : base(priority)
        {
            // this.tank = null;
            this.p1lastBulletSpawn = DateTime.Now;
            this.p2lastBulletSpawn = DateTime.Now;
            this.attackInterval = attackInterval;
            this.bulletVel = bulletVel;
            this.keyboardService = keyboardService;
            this.audioService = audioService;
        }

        private void SpawnBullet(Clock clock, Cast cast, int tankSelector) {
            if (tankSelector == 1) {
                tank = cast.GetFirstActor("tank1");
            }
            if (tankSelector == 2) {
                tank2 = cast.GetFirstActor("tank2");
            }

            TimeSpan p1timeSinceLastShot = DateTime.Now - this.p1lastBulletSpawn;
            if (tank != null && p1timeSinceLastShot.TotalSeconds >= this.attackInterval && tankSelector == 1) {
                // Bullet's starting position should be the direction of the turret
                float bulletX = tank.GetX();
                float bulletY = tank.GetY() - (tank.GetHeight()/2);

                // Create the bullet and put it in the cast
                Actor bullet = new Actor("./asteroid/assets/bullet.png", 20, 30, bulletX, bulletY, this.bulletVel.vx, this.bulletVel.vy);
                cast.AddActor("bullets", bullet);
                
                // Play the shooting sound :)
                this.audioService.PlaySound("asteroid/assets/sound/bullet_shot.wav", (float) 0.1);

                // Reset lastBulletSpawn to Now
                this.p1lastBulletSpawn = DateTime.Now;
                tankSelector = 1;
            }
            TimeSpan p2timeSinceLastShot = DateTime.Now - this.p2lastBulletSpawn;
            if (tank2 != null && p2timeSinceLastShot.TotalSeconds >= this.attackInterval && tankSelector == 2) {
                // Bullet's starting position should be the direction of the turret
                float bulletX = tank2.GetX();
                float bulletY = tank2.GetY() - (tank2.GetHeight()/2);

                // Create the bullet and put it in the cast
                Actor bullet = new Actor("./asteroid/assets/bullet.png", 20, 30, bulletX, bulletY, this.bulletVel.vx, this.bulletVel.vy);
                cast.AddActor("bullets", bullet);
                
                // Play the shooting sound :)
                this.audioService.PlaySound("asteroid/assets/sound/bullet_shot.wav", (float) 0.1);

                // Reset lastBulletSpawn to Now
                this.p2lastBulletSpawn = DateTime.Now;
                tankSelector = 2;
            }
        }

        public override void execute(Cast cast, Script script, Clock clock, Callback callback)
        {
            // Grab the tank from the cast
            // this.tank = cast.GetFirstActor("tank");
            Actor? tank1 = cast.GetFirstActor("tank1");
            Actor? tank2 = cast.GetFirstActor("tank2");

            // If the space key is down, spawn a new bullet
            if (this.keyboardService.IsKeyDown(Keys.V)) {
                this.SpawnBullet(clock, cast, 1);
            }
            if (this.keyboardService.IsKeyDown(Keys.B)) {
                this.SpawnBullet(clock, cast, 2);
            }
        }
    }
}