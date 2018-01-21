using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private GameObject pausedMenu;

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

    public void Gameover()
    {
        playerCtrl.Death();

        trackCtrl.Stop();
    }

    public void Pause()
    {
        playerCtrl.Pause();

        trackCtrl.Stop();

        pausedMenu.SetActive(true);
    }

    public void Restart()
    {
        playerCtrl.Restart();

        trackCtrl.Restart();

        pausedMenu.SetActive(false);
    }
}
