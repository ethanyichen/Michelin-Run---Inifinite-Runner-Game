using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;
    public GameObject recipeUI;

    public GameObject pauseButton;
    public string menuScene = "MenuScene";
	
    // Update is called once per frame
	void Update () {



        if (GameIsPaused)
        {
            pauseButton.GetComponent<Button>().onClick.AddListener(Resume);
        }
        else
        {
            pauseButton.GetComponent<Button>().onClick.AddListener(Pause);
        }
    }


    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;

        recipeUI.SetActive(false);
    }

    public void ViewRecipes()
    {
        Debug.Log("open recipes");
        pauseMenuUI.SetActive(false);
        recipeUI.SetActive(true);
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(menuScene);
    }

    // intended to quit app??? never used
    public void QuitGame()
    {
        Debug.Log("quitting game");
        Application.Quit();
    }
}


