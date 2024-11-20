using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void LoadSceneByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    
    public void LoadGameScene()
    {
        SceneManager.LoadScene("GAME_SCENE");
    }
}

