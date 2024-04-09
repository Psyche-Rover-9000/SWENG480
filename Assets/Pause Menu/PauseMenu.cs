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
        SceneManager.LoadScene("PsycheRover9000");
        ResumeGame();
    }

    public void ElementInfo()
    {
        PopUp.popUpElementActive = true; //pauses controls
        popUp.gameObject.SetActive(true); //pop up appears
        popUp.transform.Find("NewElementText").gameObject.SetActive(false); //hide "new element found" text
        return;
    }

    public void IronInfo()
    {
        ElementInfo();
        popUp.transform.Find("IronInfo").gameObject.SetActive(true); //relevent info on pop up appears 
    }

    public void RockInfo()
    {
        ElementInfo();
        popUp.transform.Find("RockInfo").gameObject.SetActive(true); //relevent info on pop up appears 
    }

    public void MicaInfo()
    {
        ElementInfo();
        popUp.transform.Find("MicaInfo").gameObject.SetActive(true); //relevent info on pop up appears
    }

    public void NickelInfo()
    {
        ElementInfo();
        popUp.transform.Find("NickelInfo").gameObject.SetActive(true); //relevent info on pop up appears

    }

   public void OlivineInfo()
   { 
        ElementInfo();
        popUp.transform.Find("OlivineInfo").gameObject.SetActive(true); //relevent info on pop up appears 
   }

    public void FeldsparInfo()
    {
        ElementInfo();
        popUp.transform.Find("FeldsparInfo").gameObject.SetActive(true); //relevent info on pop up appears 
    }

    public void PyroxeneInfo()
    {
        ElementInfo();
        popUp.transform.Find("PyroxeneInfo").gameObject.SetActive(true); //relevent info on pop up appears 
    }

    public void QuartzInfo()
    {
        ElementInfo();
        popUp.transform.Find("QuartzInfo").gameObject.SetActive(true); //relevent info on pop up appears 
    }
}