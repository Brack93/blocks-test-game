using UnityEngine;

public class UIScenesNavigation : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.Instance.LoadScene(Scenes.GameplayScene);
    }

    public void GoHome()
    {
        SceneManager.Instance.LoadScene(Scenes.HomeScene);
    }

}
