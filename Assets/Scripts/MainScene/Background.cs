using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour {

    private BackgroundController backgroundCtrl;

    private void Awake() {
        backgroundCtrl = GameObject.Find("BackgroundController").GetComponent<BackgroundController>();
    }


    private void Update() {
        float posX = transform.position.x + backgroundCtrl.speed;

        posX = posX > 200 ? -220 : posX;

        transform.position = new Vector3(posX, transform.position.y, transform.position.z);
    }
}
