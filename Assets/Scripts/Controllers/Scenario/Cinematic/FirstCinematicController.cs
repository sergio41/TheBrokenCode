using System.Collections;
using TMPro;
using UnityEngine;

public class FirstCinematicController : MonoBehaviour
{
    public TextMeshProUGUI m_HistoryText;
    public float m_TimeBetweenLetter;
    public float m_TimeBetweenPage;
    public float m_TimeToStartGame;
    public Animator m_Animator;

    void Start()
    {
        StartCoroutine(TypeText());
    }

    IEnumerator TypeText()
    {
        var textToType = GameConstants.FIRST_CINEMATIC.ToCharArray();
        for (int idx = 0; idx < textToType.Length; idx ++)
        {
            var letter = textToType[idx];
            var textToConcat = letter + "";
            if (GameConstants.symbolTranslation.ContainsKey(letter))
                textToConcat = GameConstants.symbolTranslation[letter];
            if (!textToConcat.Equals("|"))
            {
                m_HistoryText.text += textToConcat;
                yield return new WaitForSeconds(m_TimeBetweenLetter);
            }
            else
            {
                yield return new WaitForSeconds(m_TimeBetweenPage);
                m_HistoryText.text = "";
            }
            if (idx + 1 == textToType.Length) {
                yield return new WaitForSeconds(m_TimeToStartGame);
                PlayerPrefs.SetString(GameConstants.SCENE_TO_LOAD, GameConstants.FIRST_LEVEL_SCENE);
                m_Animator.SetTrigger(GameConstants.DO);
            }
        }
    }
}
