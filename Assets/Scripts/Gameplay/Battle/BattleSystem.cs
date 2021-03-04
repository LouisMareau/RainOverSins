namespace RoS.Gameplay.BattleSystem 
{
    using System.Collections.Generic;
    using UnityEngine;

    public class BattleSystem : MonoBehaviour
    {
        private int turn;
        public int maxTurns;

        private float timer; 

        public Battle ongoingBattle;

        public void StartBattle(List<Entity> allies, List<Entity> opponents) {
            ongoingBattle = new Battle(allies, opponents);
        }

        public int GetCurrentTurn() { return turn; }
        public float GetTimer() { return timer; }
    }
}