using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class pauseController : MonoBehaviour
{
    public GameObject unpause;
    public GameObject pause;
    public GameObject main;
    public GameObject optionsToggle;
    public GameObject cameraToggle;
    public AudioSource monsterSounds;
    public Slider mouseSensitive;
    public AudioMixer sound;
    public GameObject load;
    // Start is called before the first frame update
    void Start()
    {
        unpause.SetActive(true);
        pause.SetActive(false);
        load.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Pause"))
        {
            Time.timeScale = 0;
            pause.SetActive(true);
            unpause.SetActive(false);
            cameraToggle.GetComponent<FirstPersonView>().enabled = false;
            Cursor.lockState = CursorLockMode.None;
            monsterSounds.enabled = false;
        }
    }
    public void Resume()
    {
        Time.timeScale = 1;
        pause.SetActive(false);
        unpause.SetActive(true);
        cameraToggle.GetComponent<FirstPersonView>().enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        monsterSounds.enabled = true;


    }
    public void Options()
    {
        optionsToggle.SetActive(true);
        main.SetActive(false);
    }
    public void Leave()
    {
        Time.timeScale = 1;
        cameraToggle.GetComponent<FirstPersonView>().enabled = true;
        unpause.SetActive(true);
        // SceneManager.UnloadSceneAsync("Mansion");
        // SceneManager.LoadScene("Loading");
        main.SetActive(false);
        load.SetActive(true);

        StartCoroutine(LoadYourAsyncScene());

        LoadYourAsyncScene();
        
        SceneManager.LoadScene("Main Menu");
    }
    public void mouseSensitivity()
    {
        cameraToggle.GetComponent<FirstPersonView>().mouseSens = mouseSensitive.value;
    }
    public void Back()
    {
        main.SetActive(true);
        optionsToggle.SetActive(false);
    }
    public void SetVolume(float volume)
    {
        sound.SetFloat("Volume", Mathf.Log10(volume) * 20);
    }
    IEnumerator LoadYourAsyncScene()
    {
        AsyncOperation load = SceneManager.LoadSceneAsync("Main Menu");

        while (!load.isDone)
        {
            yield return null;
        }
    }
}
