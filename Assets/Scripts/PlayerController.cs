using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    //主角跳跃的速度
    public float jumpForce;
    //主角的重力
    public float gravity;
    //跑道的宽度
    public float trackWidth;
    //变换跑道的速度
    public float changeTrackSpeed;

    private bool jump;
    private bool hasChangedPosition;
    private float target;

    private Transform foot;
    private Rigidbody body;
    private TrackController trackCtrl;

    private void Awake() {
        body = GetComponent<Rigidbody>();
        foot = transform.Find("Foot");
        trackCtrl = GameObject.Find("TrackController").GetComponent<TrackController>();
    }

    // Use this for initialization
    void Start () {
        jump = false;

        hasChangedPosition = false;

        target = 0;
    }
	
	void Update () {
        float input = Input.GetAxis("Horizontal");

        if (input!= 0 && !hasChangedPosition) {
            target = target + Mathf.Sign(input) * trackWidth;
            target = Mathf.Clamp(target, -trackWidth, trackWidth);
            

            hasChangedPosition = true;
        }

        if(input == 0 && hasChangedPosition) {
            hasChangedPosition = false;
        }

        jump = Physics.Linecast(transform.position, foot.position, 
            1 << LayerMask.NameToLayer("Ground"));
	}

    private void FixedUpdate() {
        body.AddForce(Vector3.down * gravity);

        float zPos = Mathf.MoveTowards(transform.position.z, target, changeTrackSpeed * Time.deltaTime);

        transform.position = new Vector3(
                transform.position.x,
                transform.position.y,
                zPos
            );

        if (jump) {
            if (Input.GetButtonDown("Jump")) {
                body.velocity = new Vector3(0, jumpForce, 0);
            }
        }
    }


    private void Death() {
        Time.timeScale = 0;

        trackCtrl.Stop();
    }


    private void OnTriggerEnter(Collider other) {

        if (other.CompareTag("Obstacle")) {
            Death();
        }
    }
}
