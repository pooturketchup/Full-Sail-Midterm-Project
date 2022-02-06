using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadGame : MonoBehaviour
{
    public GameObject credits;
    public GameObject menu;
    public GameObject Load;
    public GameObject help;
    private bool loading;
    private bool showMenu;
    private bool showOptions;
    // Start is called before the first frame 
    // Update is called once per frame
    private void Start()
    {
      Load.SetActive(false);
    }
    public void Game()
    {
        // SceneManager.UnloadSceneAsync("Main Menu");
        // SceneManager.LoadScene("Loading");

        //turn off the menu text and show that the game is loading
        menu.SetActive(false);
        credits.SetActive(false);
        Load.SetActive(true);

        StartCoroutine(LoadYourAsyncScene());
        LoadYourAsyncScene();
        
        
        //SceneManager.LoadScene("Mansion");
    }
    public void CloseGame()
    {
        Application.Quit();
    }
    public void LoadOptions()
    {
        credits.SetActive(true);
        menu.SetActive(false);
    }
    public void BackButton()
    {
        credits.SetActive(false);
        menu.SetActive(true);
    }
    public void HelpButton()
    {
        help.SetActive(true);
        menu.SetActive(false);
    }
    public void BackHelp()
    {
        help.SetActive(false);
        menu.SetActive(true);
    }
    IEnumerator LoadYourAsyncScene()
    {
        AsyncOperation load = SceneManager.LoadSceneAsync("Mansion");

        while (!load.isDone)
        {
            yield return null;
        }
    }
}
