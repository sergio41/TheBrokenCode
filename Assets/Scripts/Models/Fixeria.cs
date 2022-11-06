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
        public int m_BaseHealth { get; set; }
        public int m_CurrentHealth { get; set; }
        public Dictionary<string, List<int>> m_VisitedSections { get; set; }

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
            m_BaseHealth = 3;
            m_CurrentHealth = m_BaseHealth - 1;
            m_VisitedSections = new Dictionary<string, List<int>>();
        }
        public static void Reset()
        {
            instance = new Fixeria();
        }

        public float HealthPercentage() 
        {
            return (float) m_CurrentHealth / (float) m_BaseHealth;
        }
    }
}
