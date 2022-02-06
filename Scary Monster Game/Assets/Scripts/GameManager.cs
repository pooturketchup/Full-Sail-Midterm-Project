using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // the game manager script reference to be used by other scripts
    public static GameManager instance;

    // player script reference
    public PlayerBehavior player;
    public GameObject playerSoundDrop;
    // monster script reference
    public MonsterBehavior monster;
    // UI canvas reference
    public GameObject UI;



    private void Awake()
    {
        LoadManager();
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerBehavior>();
        playerSoundDrop = GameObject.Find("SoundDrop");
        monster = GameObject.Find("Monster").GetComponent<MonsterBehavior>();
        UI = GameObject.Find("UI_Canvas");

    }

    void LoadManager()
    {
        if (instance != null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }


}
