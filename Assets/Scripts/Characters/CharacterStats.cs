using System;
using UnityEngine;

namespace TPS.Characters
{
    [Serializable]
    public class CharacterStats
    {
        [Tooltip("The maximum health of the character")]
        public int MaxHealth = 100;

        [Tooltip("The movement speed of the character in units per second")]
        public int MovementSpeed = 5;
    }
}
