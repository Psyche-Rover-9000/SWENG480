using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject popUp;
    public static bool isPaused;

    //creates an instance of the pause menu
    private static PauseMenu pauseInstance = null;
    private void Awake()
    {
        if (pauseInstance == null)
        {
            pauseInstance = this; //set instance
            DontDestroyOnLoad(this.gameObject); //used to keep the same object in all scenes
            return;
        }
        Destroy(this.gameObject); //destroy duplicate pause menu objects when returning to MainWorld

    }

    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
        isPaused = true;
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        isPaused = false;
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("PsycheRover9000");
        isPaused = false;
    }

    public void IronInfo()
    {
        popUp.gameObject.SetActive(true); //pop up appears
        popUp.transform.Find("NewElementText").gameObject.SetActive(false); //hide "new element found" text
        popUp.transform.Find("IronInfo").gameObject.SetActive(true); //iron info on pop up appears 
    }
}