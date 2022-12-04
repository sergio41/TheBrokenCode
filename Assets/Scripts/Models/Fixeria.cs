using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;
using static GameEnums;

namespace Assets.Scripts.Models
{
    public class Fixeria
    {
        public FixeriaJumpEnum jumpStatus { get; set; }
        public FixeriaSpellEnum castingStatus { get; set; }
        public int baseHealth { get; set; }
        public int currentHealth { get; set; }
        public Dictionary<string, List<int>> visitedSections { get; set; }
        public Dictionary<string, Dictionary<string, bool>> eventsDone { get; set; }
        public Dictionary<SpellEnum, int> learntSpells { get; set; }
        public SpellEnum activeSpellLeft { get; set; }
        public SpellEnum activeSpellRight { get; set; }
        public List<ItemEnum> inventory { get; set; }

        private static Fixeria instance = new Fixeria();

        public static Fixeria Instance
        {
            get
            {
                if (instance == null)
                    instance = new Fixeria();
                return instance;
            }
        }

        public Fixeria()
        {
            jumpStatus = FixeriaJumpEnum.Grounded;
            castingStatus = FixeriaSpellEnum.None;
            baseHealth = 5;
            currentHealth = baseHealth;
            visitedSections = new Dictionary<string, List<int>>();
            eventsDone = new Dictionary<string, Dictionary<string, bool>>();
            learntSpells = new Dictionary<SpellEnum, int>() {
                { SpellEnum.PRINTIO, 1},
                { SpellEnum.INSTACER, 1}/*,
                { SpellEnum.LOOPFOR, 3},
                { SpellEnum.AIFELSEN, 1}*/
            };
            inventory = new List<ItemEnum>() { ItemEnum.SPELLBOOK, ItemEnum.RESOURCES };
            activeSpellRight = SpellEnum.PRINTIO;
            activeSpellLeft = SpellEnum.INSTACER;
        }
        public static void Reset()
        {
            instance = new Fixeria();
        }

        public float HealthPercentage() 
        {
            return (float) currentHealth / (float) baseHealth;
        }
    }
}
