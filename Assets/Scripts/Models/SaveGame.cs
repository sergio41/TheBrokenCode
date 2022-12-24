
using System;
using System.Collections.Generic;
using UnityEngine;
using static GameEnums;

namespace Assets.Scripts.Models
{
    [Serializable]
    public class SaveGame
    {
        public string scene;
        public int sectionSaved;
        public long lastSave;
        public float secondsPlayed;
        public Vector3 position;
        public int baseHealth;
        public int currentHealth;
        public List<string> visitedSections;
        public SerializableDictionary<string, SerializableDictionary<string, bool>> eventsDone;
        public SerializableDictionary<SpellEnum, int> learntSpells;
        public SpellEnum activeSpellLeft;
        public SpellEnum activeSpellRight;
        public List<ItemEnum> inventory;
        public int resourceNumber;

        public SaveGame(Fixeria fixeria) {
            baseHealth = fixeria.baseHealth;
            currentHealth = fixeria.currentHealth;
            visitedSections = fixeria.visitedSections;
            eventsDone = fixeria.eventsDone;
            learntSpells = fixeria.learntSpells;
            activeSpellLeft = fixeria.activeSpellLeft;
            activeSpellRight = fixeria.activeSpellRight;
            inventory = fixeria.inventory;
            resourceNumber = fixeria.resourceNumber;
        }
    }
}
