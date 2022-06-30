using genie;
using genie.cast;
using genie.script;
using genie.services;
using genie.services.raylib;

using asteroid.cast;

namespace asteroid.script {
    class DrawHealthBarAction : genie.script.Action {
        
        private RaylibScreenService screenService;

        public DrawHealthBarAction(int priority, RaylibScreenService screenService) : base(priority) {
            this.screenService = screenService;
        }

        private void DrawHealthBar(HasLifeActor? actor) {

            if (actor != null) {
                // Figure out the health portion of the healthbar
                (float x, float y) bottomLeft = actor.GetBottomLeft();
                float healthWidth = actor.GetHPPercent() *actor.GetWidth();
                float healthHeight = actor.GetHealthBarHeight();

                // Position of the health (red) portion of the hp bar
                float health_center_x = bottomLeft.Item1 + healthWidth/2;
                float health_center_y = actor.GetY() + actor.GetHealthBarYOffset();

                // Position of the empty (grey) portion of the hp bar
                float emptyBarWidth = actor.GetWidth() - healthWidth;
                float emptyBar_x = health_center_x + healthWidth/2 + emptyBarWidth/2;
                float emptyBar_y = health_center_y;

                // Draw the RED portion
                this.screenService.DrawRectangle((health_center_x, health_center_y), healthWidth, healthHeight, Color.RED);

                // Draw the GREY portion
                this.screenService.DrawRectangle((emptyBar_x, emptyBar_y), emptyBarWidth, healthHeight, Color.GRAY);

                // If the actor is supposed to show the HP text, draw it:
                if (actor.ShowTextHealth()) {
                    this.screenService.DrawText($"{actor.GetHP()}/{actor.GetMaxHP()}",
                                                (actor.GetX(), actor.GetY() + actor.GetHealthBarYOffset()),
                                                "", actor.GetHealthBarHeight(), Color.WHITE,
                                                true, true);
                }
            }
        }

        public override void execute(Cast cast, Script script, Clock clock, Callback callback) {
            
            // Draw Healthbars for all asteroids
            foreach (HasLifeActor actor in cast.GetActors("asteroids")) {                
                this.DrawHealthBar(actor);
            }

            // Draw Healthbar for the mothership
            this.DrawHealthBar((HasLifeActor?) cast.GetFirstActor("mothership"));
        }
    }
}