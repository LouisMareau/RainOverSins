namespace RoS.Gameplay.BattleSystem
{
    using System.Collections.Generic;
    using UnityEngine;

    [System.Serializable]
    public class Battle : MonoBehaviour
    {
        public BattleSystem battleSystem;

        /// <summary>
        /// A turn can increment when all the entities in the battle have passed there turn
        /// </summary>
        public int turn { get; private set; }

        /// <summary>
        /// The time passed since the beginning of the current entity's turn, in seconds
        /// </summary>
        public float elapsedTime { get; private set; }
        public string elapsedTimeStr { get; private set; }

        private float timerSinceBattleStart;
        public float GetTimeSinceBattleStart() { return timerSinceBattleStart; }

        public List<Entity> allies;
        public List<Entity> opponents;

        public List<Entity> queue;

        /// <summary>
        /// Inits all the core members of the battle
        /// </summary>
        /// <param name="battleSystem">A reference to the battle system</param>
        public void InitBattle(BattleSystem battleSystem) {
            this.battleSystem = battleSystem;
            turn = 0;
            elapsedTime = 0;
            timerSinceBattleStart = 0;
        }

        /// <summary>
        /// Sets all the actors that are going to participate in the battle
        /// </summary>
        /// <param name="allies">All the allies that are going to participate in the battle</param>
        /// <param name="opponents">All the opponents that are going to participate in the battle</param>
        public void SetActors(List<Entity> allies, List<Entity> opponents) {
            this.allies = allies;
            this.opponents = opponents;
            InitQueue();
        }

        #region UNITY (METHODS)
        private void FixedUpdate() {
            // We actualize the time
            elapsedTime += Time.fixedDeltaTime;
            elapsedTimeStr = elapsedTime.ToString("F0");
        }
        #endregion

        /// <summary>
        /// Returns the time left before the end of the current entity's turn is forced
        /// </summary>
        public float GetTimeLeft() { return battleSystem.maxTimePerTurn - elapsedTime; }

        #region QUEUE (INIT and SORTING)
        private void InitQueue() {
            if (allies != null && opponents != null) {
                // We populate a tempQueue with all the available entities (allies + opponents)
                List<Entity> tempQueue = new List<Entity>();
                foreach (Entity ally in allies) { AddToQueue(ally, tempQueue); }
                foreach (Entity opponent in opponents) { AddToQueue(opponent, tempQueue); }

                // We initialize the queue list to mirror the temp queue (ie. same capacity)
                this.queue = new List<Entity>(tempQueue.Count);

                // We sort the entities by the haste stat and add them to the queue, in order
                Entity next = tempQueue[0];
                for (int i = 0; i < this.queue.Count; i++) {
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
        public void AddToQueue(Entity entity, List<Entity> tempQueue) { tempQueue.Add(entity); }
        public void ResetQueue() {

        }
        #endregion
    }
}