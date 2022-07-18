using genie;
using genie.cast;
using genie.script;
using genie.services;
using genie.services.raylib;
using asteroid.cast;

namespace asteroid.script
{
    class HandleShootingAction : genie.script.Action
    {

        private RaylibKeyboardService keyboardService;
        private genie.cast.Actor? turret;
        private genie.cast.Actor? turret2;
        private DateTime p1lastBulletSpawn;
        private DateTime p2lastBulletSpawn;
        private RaylibAudioService audioService;
        private float attackInterval;
        private float ShootingDirection;
        private (float vx, float vy) bulletVel;

        private int turretSelector = 0;

        

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

        private void SpawnBullet(Clock clock, Cast cast, int turretSelector) {
            if (turretSelector == 1) {
                turret = cast.GetFirstActor("turret1");
            }
            if (turretSelector == 2) {
                turret2 = cast.GetFirstActor("turret2");
            }

            TimeSpan p1timeSinceLastShot = DateTime.Now - this.p1lastBulletSpawn;
            if (turret != null && p1timeSinceLastShot.TotalSeconds >= this.attackInterval && turretSelector == 1) {
                // Bullet's starting position should be the tip of the turret and direction
                //Calculate turret tip == turret location + direction of 1/2 height

                double angle = 0;
                double pi = Math.PI;
                angle = (double)turret.GetRotation();
                double convert = pi/180*angle;
                this.bulletVel.vx = (float)(Math.Cos(convert));
                this.bulletVel.vy = (float)(Math.Sin(convert));

                float bulletX = turret.GetX() + (float)(Math.Cos(convert)) * turret.GetWidth();
                float bulletY = turret.GetY() + (float)(Math.Sin(convert)) * turret.GetWidth();
            
                // Create the bullet and put it in the cast
                Bullet bullet = new Bullet("./Game/Asset/Bullet.png", 10, 10, bulletX, bulletY, this.bulletVel.vx, this.bulletVel.vy, 3);
                cast.AddActor("bullets", bullet);
                
                // Play the shooting sound :)
                this.audioService.PlaySound("asteroid/assets/sound/bullet_shot.wav", (float) 0.1);

                // Reset lastBulletSpawn to Now
                this.p1lastBulletSpawn = DateTime.Now;
                turretSelector = 1;
            }

            TimeSpan p2timeSinceLastShot = DateTime.Now - this.p2lastBulletSpawn;
            if (turret2 != null && p2timeSinceLastShot.TotalSeconds >= this.attackInterval && turretSelector == 2) {
                // Bullet's starting position should be the direction of the turret
                float bulletX = turret2.GetX();
                float bulletY = turret2.GetY() - turret2.GetWidth();

                double angle = 0;
                double pi = Math.PI;
                angle = (double)turret2.GetRotation();
                double convert = pi/180*angle;
                this.bulletVel.vx = (float)(Math.Cos(convert));
                this.bulletVel.vy = (float)(Math.Sin(convert));

                float bulletX2 = turret2.GetX() + (float)(Math.Cos(convert)) * turret2.GetWidth();
                float bulletY2 = turret2.GetY() + (float)(Math.Sin(convert)) * turret2.GetWidth();

                // Create the bullet and put it in the cast
                Actor bullet = new Actor("./Game/Asset/Bullet.png", 10, 10, bulletX2, bulletY2, this.bulletVel.vx, this.bulletVel.vy);
                cast.AddActor("bullets", bullet);
                
                // Play the shooting sound :)
                this.audioService.PlaySound("asteroid/assets/sound/bullet_shot.wav", (float) 0.1);

                // Reset lastBulletSpawn to Now
                this.p2lastBulletSpawn = DateTime.Now;
                turretSelector = 2;
            }
        }

        public override void execute(Cast cast, Script script, Clock clock, Callback callback)
        {
            // Grab the tank from the cast
            // this.tank = cast.GetFirstActor("tank");
            Actor? turret1 = cast.GetFirstActor("turret1");
            Actor? turret2 = cast.GetFirstActor("turret2");

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