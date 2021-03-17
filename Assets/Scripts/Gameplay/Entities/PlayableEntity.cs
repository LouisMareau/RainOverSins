namespace RoS.Gameplay.Entities
{
    using UnityEngine;
    using UnityEngine.AI;
    
    public class PlayableEntity : Entity
    {
        [Header("NAVMESH")]
        public NavMeshAgent navMeshAgent;

        [Header("PHYSICS")]
        private new Collider collider;

        [Header("BATTLE")]
        public State state;

        /// <summary>
        /// Amount of turns (of the ongoing battle) the entity has fainted.
        /// </summary>
        public int turnsSinceFainted;
        /// <summary>
        /// Amount of turns (of the ongoing battle) the entity has been dead.
        /// </summary>
        public int turnsSinceDeath;

        protected override void OnValidate() {
            base.OnValidate();

            stats.SetStats();
        }

        protected virtual void Awake() {
            collider = transform.Find("Mesh").GetComponent<Collider>();
            navMeshAgent = GetComponent<NavMeshAgent>();

            // ** This needs to be set from last save when first starting the game and saved before exiting the game
            stats.health = stats.maxHealth;
            stats.mana = stats.maxMana;
        }

        public void CheckState() {
            if (stats.health <= 0) {
                if (turnsSinceFainted <= GameManager.maxTurnsBeforeDeath) {
                    state = State.FAINTED;
                    turnsSinceFainted++;
                } 
                else {
                    state = State.DEAD;
                    if (turnsSinceFainted > 0) { turnsSinceFainted = 0; }
                    turnsSinceDeath++;
                }
            }
            else {
                state = State.ALIVE;
            }
        }

        
        public enum State {
            ALIVE,
            FAINTED,
            DEAD
        }
    }
}