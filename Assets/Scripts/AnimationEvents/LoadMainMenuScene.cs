using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMainMenuScene : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        SceneManager.LoadScene(GameConstants.MAIN_MENU_SCENE);
    }
}
