using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu_SceneManager : MonoBehaviour
{
    private void Start()
    {
        

    }
    public void GameStart()
    {
        SceneManager.LoadScene("Stage_1");        
    }
    public void Quit()
    {
        Application.Quit();
    }
}
