using System;
using UnityEngine;
using static GameEnums;

namespace Assets.Scripts.Models
{
    [Serializable]
    public class SpellParameters
    {
        public int damage;
        public float specialParameter;
        public int cost;

        public SpellParameters(int damage, float specialParameter, int cost)
        {
            this.damage = damage;
            this.specialParameter = specialParameter;
            this.cost = cost;
        }
    }
}
