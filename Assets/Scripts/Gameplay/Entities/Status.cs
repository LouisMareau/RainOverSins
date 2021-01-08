namespace RoS.Gameplay
{    
    using UnityEngine;

    [System.Serializable]
    public class Status
    {
        public enum Type {
            REGENERATION
        }

        [Header("CORE")]
        public string name;
        public string description;
        public Sprite thumbnail;
        public Type type;

        [Header("EFFECT")]
        public float effectValue;
        public int duration;

        public Status(Type type, float effectValue, int duration) {
            Init();
            this.type = type;
            this.effectValue = effectValue;
            this.duration = duration;
        }

        public void Init() {
            switch (type) {
                case Type.REGENERATION:
                    this.name = "Regeneration";
                    this.description = "Regenerates its wounds at a faster pace by collecting life force in and out itself.";
                    // ** this.thumbnail = [...] ** //
                break;  
            }
        }
    }
}