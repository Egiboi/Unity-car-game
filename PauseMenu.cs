using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    public static bool GamePause=false;
    public GameObject pauseMenuUI;
    GameObject SceneFollower;
    GameObject Player;

    private void Start()
    {
        SceneFollower = GameObject.Find("Scene follower");
        Player = GameObject.Find("Player");
    }
    // Update is called once per frame
    void Update()
    {
        //Checks for a escape key press.
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // If game is paused already the game will resume on escape key
            if (GamePause == true)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
    //Resume, sets pause menu canvas to hidden. Resumes time flow and changes boolean for game paused state.
    public void Resume ()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GamePause = false;

    }
    // Pause, sets pause menu canvas to active. Pauses time flow and changes boolean for game paused state.
    void Pause ()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GamePause = true;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneFollower.GetComponent<SceneFollower>().SetBool(false);
        SceneManager.LoadScene("Main menu");
    }
    // method to quit the application
    public void QuitGame()
    {

        // Running in a build
        Application.Quit();
    }
    public void Retry()
    {
        Time.timeScale = 1f;
        SceneFollower.GetComponent<SceneFollower>().SetScene(Player.GetComponent<PlayerController>().SceneNumber());
        SceneManager.LoadScene(SceneFollower.GetComponent<SceneFollower>().GetScene());
    }
}
