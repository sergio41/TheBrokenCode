using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;
using static GameEnums;

namespace Assets.Scripts.Models
{
    [Serializable]
    public class Fixeria
    {
        public FixeriaJumpEnum jumpStatus { get; set; }
        public FixeriaSpellEnum castingStatus { get; set; }
        public int baseHealth { get; set; }
        public int currentHealth { get; set; }
        public List<string> visitedSections { get; set; }
        public SerializableDictionary<string, SerializableDictionary<string, bool>> eventsDone { get; set; }
        public SerializableDictionary<SpellEnum, int> learntSpells { get; set; }
        public SpellEnum activeSpellLeft { get; set; }
        public SpellEnum activeSpellRight { get; set; }
        public List<ItemEnum> inventory { get; set; }
        public int resourceNumber { get; set; }

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
            visitedSections = new List<string>();
            eventsDone = new SerializableDictionary<string, SerializableDictionary<string, bool>>();
            learntSpells = new SerializableDictionary<SpellEnum, int>() {
                { SpellEnum.PRINTIO, 1},
                { SpellEnum.INSTACER, 1}
            };
            inventory = new List<ItemEnum>() { ItemEnum.SPELLBOOK, ItemEnum.RESOURCES };
            activeSpellRight = SpellEnum.PRINTIO;
            activeSpellLeft = SpellEnum.INSTACER;
            resourceNumber = 0;
        }


        public Fixeria(SaveGame saveGame)
        {
            jumpStatus = FixeriaJumpEnum.Grounded;
            castingStatus = FixeriaSpellEnum.None;
            baseHealth = 5;
            currentHealth = baseHealth;
            visitedSections = saveGame.visitedSections;
            eventsDone = saveGame.eventsDone;
            learntSpells = saveGame.learntSpells;
            inventory = saveGame.inventory;
            activeSpellRight = saveGame.activeSpellRight;
            activeSpellLeft = saveGame.activeSpellLeft;
            resourceNumber = saveGame.resourceNumber;
        }

        public static void Reset()
        {
            instance = new Fixeria();
        }

        public static void LoadData(SaveGame saveGame)
        {
            instance = new Fixeria(saveGame);
        }

        public float HealthPercentage() 
        {
            return (float) currentHealth / (float) baseHealth;
        }
    }
}
