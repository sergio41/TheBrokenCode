using System;
using UnityEngine;
using static GameEnums;

namespace Assets.Scripts.Models
{
    [Serializable]
    public class Spell
    {
        public SpellEnum spell;
        public GameObject spellObject;
    }
}
