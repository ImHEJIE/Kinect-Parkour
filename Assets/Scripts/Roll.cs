using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roll : MonoBehaviour {
    private float length;

    private GameObject instance;

    private TrackController trackCtrl;

    private bool hasCreated;
    private GameObject[] tracks;

    private void Awake() {
        trackCtrl = GameObject.Find("TrackController").GetComponent<TrackController>();
    }
    private void Start() {
        tracks = trackCtrl.tracks;
        length = trackCtrl.length;

        hasCreated = false;
    }

    private void Update() {
        transform.position = new Vector3(
            transform.position.x + trackCtrl.currentSpeed,
            transform.position.y,
            transform.position.z
        );

        if(transform.position.x > length) {
            Destroy(this.gameObject);

            trackCtrl.RunOver();
        }

        if(!hasCreated && transform.position.x > 0) {
            hasCreated = true;

            CreateTrack();
        }
    }

    private void CreateTrack() {
        int index = Random.Range(0, tracks.Length);
        float xPos = transform.position.x - length;

        Vector3 pos = new Vector3(xPos, transform.position.y, transform.position.z);

        Instantiate(tracks[index], pos, Quaternion.identity);
    }
}
