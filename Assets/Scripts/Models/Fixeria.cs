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


        public Fixeria()
        {
            jumpStatus = FixeriaJumpEnum.Grounded;
        }
    }
}
