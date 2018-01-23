using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour {
    //用来保存跑道的初始速度
    public float speed;

    private float preSpeed;

    private void Start() {

        speed = speed * Time.deltaTime;
        preSpeed = speed;
    }

    public void Stop() {
        speed = 0;
    }

    public void Restart() {
        speed = preSpeed;
    }
}
