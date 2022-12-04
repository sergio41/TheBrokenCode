using Assets.Scripts.Models;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using static GameEnums;

public static class GameConstants
{
    #region PlayerPrefs
    public const string SCENE_TO_LOAD = "SceneToLoad";
    #endregion PlayerPrefs

    #region Tags
    public const string PLAYER = "Player";
    public const string SPELL = "Spell";
    public const string SPELL_IGNORE = "SpellIgnore";
    public const string ENEMY = "Enemy";
    #endregion Tags

    #region Scenes
    public const string MAIN_MENU_SCENE = "MainMenuScene";
    public const string CINEMATIC_SCENE = "CinematicScene";
    public const string CREDITS_SCENE = "CreditsScene";
    public const string FIRST_LEVEL_SCENE = "FirstLevelScene";
    #endregion Scenes

    #region Animation variables
    public const string IS_RUN = "isRun";
    public const string IS_JUMP = "isJump";
    public const string ATTACK = "attack";
    public const string DIE = "die";
    public const string HURT = "hurt";
    public const string IS_FOLLOW = "isFollow";
    public const string DO = "Do";
    public const string DIRECTION = "direction";
    public const string OPEN = "open";
    #endregion Triggers

    #region Other constants
    public const string MUSIC_VOLUME = "MusicVolume";
    #endregion Other constants

    #region Game descriptions
    public static Dictionary<ItemEnum, string> descriptions = new Dictionary<ItemEnum, string>(){
            { ItemEnum.SPELLBOOK, "Libro de hechizos de Fixeria. Contiene todas las instrucciones para invocar los conjuros que ha ido aprendiendo a lo largo de sus aventuras." },
            { ItemEnum.BRIDGE_SCHEME, "Esquema de un puente. Usando este plano junto con el hechizo Instacer se pueden crear nuevos puentes donde haya alguna l�mpara de conjuros apagada." },
            { ItemEnum.RESOURCES, "Los recursos son materiales valiosos para Fixeria que le permiten aumentar su capacidad y realizar conjuros a�n m�s poderosos." }
        };
    public static Dictionary<SpellEnum, string> spellDescriptions = new Dictionary<SpellEnum, string>(){
            { SpellEnum.PRINTIO, "Hechizo b�sico de Fixeria. Algunos hechizeros lo utilizan para obtener datos del funcionamiento de artilugios o procesos de manera visual. Fixeria lo utiliza para da�ar a sus enemigos y monitorizar cuanta vida les resta para ser vencidos.\n\nDa�o {0}\nMuestra la vida durante {1} segundos" },
            { SpellEnum.AIFELSEN, "Conjuro bifurcador o condicional. Al contacto con los enemigos libera dos hechizos nuevos que actuan por su cuenta. Otros magos los utilizan para, en su d�a a d�a, cubrir diferentes escenarios asegurando que uno de los hechizos generados cumpla la funci�n deseada.\n\nDa�o {0}\nSe divide {1} veces" },
            { SpellEnum.LOOPFOR, "Hechizo iterante. Cuando impacta con alg�n obst�culo o enemigo permanece en �l repitiendo un bucle alrededor del punto de impacto. Se suele utilizar para realizar tareas repetitivas y evitar tener que realizar varios hechizos consecutivos.\n\nDa�o {0}\nItera durante {1} segundos" },
            { SpellEnum.INSTACER, "Instrucci�n creadora. Con los esquemas adecuados permite a Fixeria crear objetos basados en ellos. Gracias al modelado de la realidad mediante los esquemas se permite crear replicas virtuales de los objetos las veces que sean necesarias.\n\nNo produce da�o." }
    };

    public static Dictionary<int, SpellParameters> printioLevels = new Dictionary<int, SpellParameters> { 
        { 1, new SpellParameters(20, 3f, 0) },
        { 2, new SpellParameters(30, 6f, 300) },
        { 3, new SpellParameters(60, 12f, 1500) }
    };
    public static Dictionary<int, SpellParameters> aifelsenLevels = new Dictionary<int, SpellParameters> {
        { 1, new SpellParameters(20, 2f, 0) },
        { 2, new SpellParameters(30, 3f, 500) },
        { 3, new SpellParameters(40, 5f, 2000) }
    };
    public static Dictionary<int, SpellParameters> loopforLevels = new Dictionary<int, SpellParameters> {
        { 1, new SpellParameters(10, 3f, 0) },
        { 2, new SpellParameters(15, 5f, 600) },
        { 3, new SpellParameters(20, 10f, 2500) }
    };
    public static Dictionary<SpellEnum, Dictionary<int, SpellParameters>> spellLevels = new Dictionary<SpellEnum, Dictionary<int, SpellParameters>>(){
        { SpellEnum.PRINTIO, printioLevels },
        { SpellEnum.AIFELSEN, aifelsenLevels },
        { SpellEnum.LOOPFOR, loopforLevels }
    };
    #endregion Game descriptions

    #region Utils
    public static Regex upperCaseInside = new Regex("(?<=[A-Z])[A-Z]+?((?=[A-Z][a-z])|(?=[0-9]))");
    #endregion Utils

    #region History
    public const string FIRST_CINEMATIC = "En el lejano mundo de Sistemia, todo esta en armon�a y calma. Cada ser tiene un prop�sito y convive pac�ficamente con los dem�s. Es un reino rebosante de felicidad. O al menos lo era...|" +
        "Hace no mucho, las fuerzas del mal, formadas por monstruos llamados bugs, irrumpieron y alteraron el orden establecido causando que la armon�a y felicidad que reinaban comenzaran a colapsar. El cerebro detr�s de esta invasi�n a�n se desconoce, pero si se sabe que la vanguardia del enemigo es comandada por Nullo, uno de sus generales.|" +
        "Desesperado, el gobierno de Sistemia no tiene m�s remedio que recurrir a Fixeria. Una joven guerrera y hechizera que deber� buscar y acabar con los generales, mientras investiga quien es el culpable de esta invasi�n.&Sin embargo, a pesar de ser ya muy poderosa, Fixeria deber� desarrollar su poder a la vez que intenta cumplir con sus objetivos, de otro modo es posible que fracase.|" +
        "El primer objetivo ser� la caverna neblinosa, donde se rumorea que se esconde Nullo, el primer gran rival al que deber� hacer frente nuestra joven hero�na y que servir� de punto de inicio a su gran aventura por salvar el reino...";
    #endregion History

    #region Symbol Translation
    public static Dictionary<char, string> symbolTranslation = new Dictionary<char, string> {
        { '&', "\n" }
    };
    #endregion

}
