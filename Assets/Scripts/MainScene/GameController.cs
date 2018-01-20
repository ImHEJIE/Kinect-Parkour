using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
    private GameObject pausedMenu;

    private TrackController trackCtrl;
    private PlayerController playerCtrl;

    private void Awake() {
        trackCtrl = GameObject.Find("TrackController").GetComponent<TrackController>();

        playerCtrl = GameObject.Find("Player").GetComponent<PlayerController>();

        pausedMenu = GameObject.Find("PausedMenu");
        pausedMenu.SetActive(false);
    }

    public void Gameover() {
        playerCtrl.Death();

        trackCtrl.Stop();
    }

    public void Pause() {
        playerCtrl.Pause();

        trackCtrl.Stop();

        pausedMenu.SetActive(true);
    }

    public void Restart() {
        playerCtrl.Restart();

        trackCtrl.Restart();

        pausedMenu.SetActive(false);
    }
}
