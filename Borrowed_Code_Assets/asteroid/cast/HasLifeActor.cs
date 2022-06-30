using genie.cast;

namespace asteroid.cast {
    class HasLifeActor : Actor {

        private int maxHP;
        private int currentHP;
        private int healthBarYOffset;
        private int healthBarHeight;
        private bool showTextHealth;

        public HasLifeActor(string path, int width, int height,
                        float x = 0, float y = 0,
                        float vx = 0, float vy = 0,
                        float rotation = 0, float rotationVel = 0,
                        
                        int healthBarYOffset = 0,
                        int healthBarHeight = 5,
                        int maxHP = 0,
                        bool showTextHealth = false) :
        base(path, width, height, x, y, vx, vy, rotation, rotationVel)
        {
            this.maxHP = maxHP;
            this.currentHP = maxHP;
            this.healthBarYOffset = healthBarYOffset;
            this.healthBarHeight = healthBarHeight;
            this.showTextHealth = showTextHealth;
        }

        public void SetHP(int HP) {
            this.currentHP = HP;
        }

        public int GetHP() {
            return this.currentHP;
        }

        public void SetMaxHP(int maxHP) {
            this.maxHP = maxHP;
        }

        public int GetMaxHP() {
            return this.maxHP;
        }

        public float GetHPPercent() {
            return ((float) this.currentHP) / ((float) this.maxHP);
        }

        public void SetHealthBarYOffset(int healthBarYOffset) {
            this.healthBarYOffset = healthBarYOffset;
        }

        public int GetHealthBarYOffset() {
            return this.healthBarYOffset;
        }

        public void SetHealthBarHeight(int healthBarHeight) {
            this.healthBarHeight = healthBarHeight;
        }

        public int GetHealthBarHeight() {
            return this.healthBarHeight;
        }

        public bool ShowTextHealth() {
            return this.showTextHealth;
        }

        public void SetShowTextHealth(bool showTextHealth) {
            this.showTextHealth = showTextHealth;
        }

        public void TakeDamage(int damage) {
            this.currentHP -= damage;
        }

        public void Heal(int heal) {
            this.currentHP = (heal > (this.maxHP - this.currentHP)) ? this.maxHP : (this.currentHP + heal);
        }
    }
}