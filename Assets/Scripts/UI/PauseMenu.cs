using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    // Start is called before the first frame update
    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale= 0;
    }
    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale= 1;
    }
     public void  Home()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
