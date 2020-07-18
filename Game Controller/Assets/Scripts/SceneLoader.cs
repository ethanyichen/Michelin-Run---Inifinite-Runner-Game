using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {

	public void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    public void LoadStartScene()
    {
        SceneManager.LoadScene(0);
    }
    public void LoadPlayScene()
    {
        SceneManager.LoadScene(2);
    }
    public void LoadRecipeScene()
    {
        SceneManager.LoadScene(3);
    }
    public void LoadCreditsScene()
    {
        SceneManager.LoadScene(4);
    }
    public void LoadHighScoreScene()
    {
        SceneManager.LoadScene(5);
    }
    public void Mute()
    {
        AudioListener.pause = !AudioListener.pause;
    }


    public void QuitGame()
    {
        Application.Quit();
    }
}
