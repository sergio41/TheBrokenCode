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
    public enum EnemyMovementEnum
    {
        Waiting = 0,
        Wandering = 1
    }
    public enum SpellEnum
    {
        PRINTIO,
        AIFELSEN,
        LOOPFOR,
        INSTACER
    }
    public enum ItemEnum
    {
        SPELLBOOK,
        BRIDGE_SCHEME,
        RESOURCES
    }
}
