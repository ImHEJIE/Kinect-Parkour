using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private GameObject pausedMenu;
    private GameObject restartMenu;

    private TrackController trackCtrl;
    private PlayerController playerCtrl;
    private GestureDetect gestureListener;
    private bool isPause = false;
    private void Awake()
    {
        trackCtrl = GameObject.Find("TrackController").GetComponent<TrackController>();

        playerCtrl = GameObject.Find("Player").GetComponent<PlayerController>();

        pausedMenu = GameObject.Find("PausedMenu");
        pausedMenu.SetActive(false);

        restartMenu = GameObject.Find("RestartMenu");
        restartMenu.SetActive(false);
    }

    void Start()
    {
        // get the gestures listener
        gestureListener = GestureDetect.Instance;
    }

    void Update()
    {
        if (!gestureListener)
            return;

        if (gestureListener.IsWave())
        {
            if (!isPause)
            {
                Pause();
                isPause = true;
            }
            else
            {
                Restart();
                isPause = false;
            }

        }
    }

    //游戏结束调用的函数
    public void Gameover()
    {
        playerCtrl.Death();

        trackCtrl.Stop();

        restartMenu.SetActive(true);
    }

    //游戏暂停调研的函数
    public void Pause()
    {
        playerCtrl.Pause();

        trackCtrl.Stop();

        pausedMenu.SetActive(true);
    }

    //游戏继续调研的函数
    public void Continue()
    {
        playerCtrl.Restart();

        trackCtrl.Restart();

        pausedMenu.SetActive(false);
    }

    //游戏重新开始调用的游戏
    public void Restart() {
        

        SceneManager.LoadScene("main", LoadSceneMode.Single);

        playerCtrl.Restart();

        trackCtrl.Restart();
    }
}
