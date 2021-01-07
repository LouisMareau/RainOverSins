/// <summary>
/// This class is used to store lists that are used as JSON objects.
/// DO NOT MODIFY, IN ANY CASE, THIS FILE WITHOUT KNWOING EXACTLY WHAT YOU ARE DOING AND THE STRUCTURE OF THE JSON OBJECTS.
/// </summary>

namespace RoS.Gameplay.Entities.Creatures
{
    using System.Collections.Generic;

    public class Creatures
    {
        public List<Creature> creatures;

        public Creatures() {
            this.creatures = new List<Creature>();
        }
    }
}