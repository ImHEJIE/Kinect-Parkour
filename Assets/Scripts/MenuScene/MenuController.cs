using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

    private GameObject instruction;
    private GameObject menu;

    private void Awake() {
        instruction = GameObject.Find("Instruction");
        menu = GameObject.Find("Menu");
    }

    void Start() {
        instruction.SetActive(false);
    }

    public void StartBtn() {
        SceneManager.LoadScene("main", LoadSceneMode.Single);
    }

    public void InstructionBtn() {
        menu.SetActive(false);

        instruction.SetActive(true);
    }

    public void ExitBtn() {
        Debug.Log("Exit");
        Application.Quit();
    }

    public void CloseBtn() {
        menu.SetActive(true);

        instruction.SetActive(false);
    }
}