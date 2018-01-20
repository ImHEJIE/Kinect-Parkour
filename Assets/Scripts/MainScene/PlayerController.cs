using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
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
    private GameController gameCtrl;

    private AvatarController avatarCtrl;

    private GestureDetect gestureListener;
    public Animator animator;

    private void Awake()
    {
        body = GetComponent<Rigidbody>();
        foot = transform.Find("Foot");

        gameCtrl = GameObject.Find("GameController").GetComponent<GameController>();
        avatarCtrl = GameObject.Find("AvatarController").GetComponent<AvatarController>();
    }

    // Use this for initialization
    void Start()
    {
        jump = false;

        hasChangedPosition = false;

        target = 0;
        // get the gestures listener
        gestureListener = GestureDetect.Instance;
        animator = GameObject.Find("Basic_BanditPrefab").GetComponent<Animator>();
        Sprint();
    }

    void Update()
    {
        // float input = Input.GetAxis("Horizontal");

        // if (input != 0 && !hasChangedPosition)
        // {
        //     target = target + Mathf.Sign(input) * trackWidth;
        //     target = Mathf.Clamp(target, -trackWidth, trackWidth);


        //     hasChangedPosition = true;
        // }

        // if (input == 0 && hasChangedPosition)
        // {
        //     hasChangedPosition = false;
        // }

        float input = avatarCtrl.deltaPosition();
        if (input != 0 && !hasChangedPosition)
        {
            target = target + Mathf.Sign(input) * trackWidth;
            target = Mathf.Clamp(target, -trackWidth, trackWidth);
            hasChangedPosition = true;
        }
        if (input == 0 && hasChangedPosition)
        {
            hasChangedPosition = false;
        }

        //Debug.Log(input.ToString());
        jump = Physics.Linecast(transform.position, foot.position,
            1 << LayerMask.NameToLayer("Ground"));
    }

    private void FixedUpdate()
    {
        body.AddForce(Vector3.down * gravity);

        float zPos = Mathf.MoveTowards(transform.position.z, target, changeTrackSpeed * Time.deltaTime);

        transform.position = new Vector3(
                transform.position.x,
                transform.position.y,
                zPos
            );
        if (jump)//跳跃和下蹲都只有在人物脚在平面上的时候才能做
        {
            if (gestureListener.IsJump())
            {
                Jump();
                body.velocity = new Vector3(0, jumpForce, 0);
            }
            if (gestureListener.IsSquat()) 
            {
                Squat();
            }
        }
    }

    public void Death() {
        Time.timeScale = 0;
    }

    public void Restart() {
        Time.timeScale = 1;
    }

    public void Pause() {
        Time.timeScale = 0;
    }

    public void Idle()
    {
        //animator = GetComponent<Animator>();
        animator.SetBool("Walk", false);
        animator.SetBool("SprintJump", false);
        animator.SetBool("SprintSlide", false);
    }

    public void Sprint()
    {
        //animator = GetComponent<Animator>();
        animator.SetBool("Walk", false);
        animator.SetBool("SprintJump", true);
        animator.SetBool("SprintSlide", false);
    }

    public void Jump()
    {
        //animator = GetComponent<Animator>();
        // animator.SetBool("Walk", false);
        // animator.SetBool("SprintJump", false);
        // animator.SetBool("SprintSlide", true);
        animator.Play("JUMP00");
    }

    public void Squat()
    {
        animator.Play("SLIDE00");
    }


    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Obstacle"))
        {
            gameCtrl.Gameover();
        }
    }
}
