using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnums : MonoBehaviour
{
    public enum FixeriaJumpEnum
    {
        Grounded = 0,
        Jumping = 1,
        Falling = 2
    }
    public enum FixeriaSpellEnum
    {
        Casting = 0,
        Charging = 1,
        Released = 2,
        None = 2
    }
}
