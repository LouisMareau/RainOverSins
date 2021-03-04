namespace RoS.Gameplay.BattleSystem
{
    using System.Collections.Generic;

    [System.Serializable]
    public class Battle
    {
        public List<Entity> allies;
        public List<Entity> opponents;

        public List<Entity> queue;

        #region CONSTRUCTORS
        /// <summary>
        /// Empty constructor
        /// </summary>
        public Battle() {
            this.allies = new List<Entity>();
            this.opponents = new List<Entity>();

            this.queue = new List<Entity>();
        }

        public Battle(List<Entity> allies, List<Entity> opponents) {
            this.allies = allies;
            this.opponents = opponents; 

            InitQueue();
        }
        #endregion

        private void InitQueue() {
            if (allies != null && opponents != null) {
                this.queue = new List<Entity>();
                List<Entity> tempQueue = new List<Entity>();
        
                // We populate the tempQueue with all the available entities (allies + opponents)
                foreach (Entity ally in allies) { AddToQueue(ally, tempQueue); }
                foreach (Entity opponent in opponents) { AddToQueue(opponent, tempQueue); }

                // We sort the entities by the haste stat and add them to the queue, in order
                Entity next = tempQueue[0];
                for (int i = 0; i < tempQueue.Count; i++) {
                    foreach (Entity entity in tempQueue) {
                        // Sort by Haste (stat)
                        if (entity.stats.haste > next.stats.haste) { next = entity; }
                    }
                    AddToQueue(next);
                    tempQueue.Remove(next);
                }
            }
        }

        public void AddToQueue(Entity entity) { this.queue.Add(entity); }
        public void AddToQueue(Entity entity, List<Entity> queue) { queue.Add(entity); }
    }
}