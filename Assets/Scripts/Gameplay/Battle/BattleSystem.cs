namespace RoS.Gameplay.BattleSystem 
{
    using System.Collections.Generic;
    using UnityEngine;

    public class BattleSystem : MonoBehaviour
    {

        [Header("CORE")]
        public int maxTurns;
        public float maxTimePerTurn;

        public Battle ongoingBattle { get; private set;}

        [Header("REFS")]
        public GameObject battleUI;

        public void StartBattle(List<Entity> allies, List<Entity> opponents) {
            Battle battle = new GameObject("Battle").AddComponent<Battle>();
            ongoingBattle = Instantiate<Battle>(battle, Vector3.zero, Quaternion.identity, this.transform);

            // We setup the battle members
            ongoingBattle.InitBattle(this);
            ongoingBattle.SetActors(allies, opponents);
        }
    }
}