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

    private GestureDetect gestureListener;
    private bool isPause = false;
    private void Awake()
    {
        trackCtrl = GameObject.Find("TrackController").GetComponent<TrackController>();

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
        Time.timeScale = 0;

        trackCtrl.Stop();

        restartMenu.SetActive(true);
    }

    //游戏暂停调研的函数
    public void Pause()
    {
        Time.timeScale = 0;

        trackCtrl.Stop();

        pausedMenu.SetActive(true);
    }

    //游戏继续调研的函数
    public void Continue()
    {
        Time.timeScale = 1;

        trackCtrl.Continue();

        pausedMenu.SetActive(false);
    }

    //游戏重新开始调用的游戏
    public void Restart() {
        

        SceneManager.LoadScene("main", LoadSceneMode.Single);

        Time.timeScale = 1;

        //trackCtrl.Restart();
    }
}
