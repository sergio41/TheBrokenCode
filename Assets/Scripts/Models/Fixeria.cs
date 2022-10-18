using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GameEnums;

namespace Assets.Scripts.Models
{
    public class Fixeria
    {
        public FixeriaJumpEnum jumpStatus { get; set; }
        public FixeriaSpellEnum castingStatus { get; set; }

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
        }
    }
}
