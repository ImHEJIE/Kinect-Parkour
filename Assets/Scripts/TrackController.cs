using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackController : MonoBehaviour {
    //用于保存跑道prefabs
    public GameObject[] tracks;
    //用来保存跑道的初始速度
    public float initialSpeed;
    //用来保存跑道每次加速率
    public float speedRate;
    //用来保存跑过多少个跑道之后加速
    public int acceleratedNumber;
    //用来保存跑道的长度
    public float length;


    //用来保存跑过的跑道数
    [HideInInspector]
    public int count;
    //用来保存跑道当前的速度
    [HideInInspector]
    public float currentSpeed;
    


    //用来保存跑道是否已经在当前轮加速过
    private bool hasAccelerated;

    private void Awake() {
        count = 0;

        currentSpeed = initialSpeed * Time.deltaTime;

        hasAccelerated = false;
    }

    private void Update() {
        if(count % acceleratedNumber == 0) {
            if(!hasAccelerated && count > 0) {

                currentSpeed = currentSpeed * speedRate;

                hasAccelerated = true;

                Debug.Log("Change");
            }
        } else {
            hasAccelerated = false;
        }
    }

    public void RunOver() {
        count++;
    }

    public void Stop() {
        currentSpeed = 0;
    }
}
