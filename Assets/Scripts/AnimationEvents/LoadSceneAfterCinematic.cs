using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneAfterCinematic : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        SceneManager.LoadScene(PlayerPrefs.GetString("SceneToLoad"));
    }
}
