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
    public bool useKinectInput;//是否使用Kinect输入

    private bool grouded;
    private bool hasChangedPosition;
    private float target;

    private Transform foot;
    private Rigidbody body;
    private GameController gameCtrl;

    private AvatarController avatarCtrl;

    private GestureDetect gestureListener;
    public Animator animator;

    private bool arrived;

    private void Awake()
    {
        body = GetComponent<Rigidbody>();
        foot = transform.Find("Foot");

        gameCtrl = GameObject.Find("GameController").GetComponent<GameController>();
        avatarCtrl = GameObject.Find("AvatarController").GetComponent<AvatarController>();
    }


    void Start()
    {
        grouded = false;

        hasChangedPosition = false;

        target = 0;
        // get the gestures listener
        gestureListener = GestureDetect.Instance;

        animator.SetFloat("MoveSpeed", 1.5f);
    }

    void Update()
    {
        //是否着地来决定角色的动画状态机
        animator.SetBool("Grounded", grouded);

        arrived = Mathf.Abs(transform.position.z - target) < 0.1;

        float input;
        if (!useKinectInput)
        {
            //使用键盘当作输入
            input = Input.GetAxis("Horizontal");
        }
        else
        {
            //使用kinect作为输入
            input = avatarCtrl.deltaPosition();
        }

        if (input != 0 && !hasChangedPosition && arrived)
        {
            target = target + Mathf.Sign(input) * trackWidth;
            target = Mathf.Clamp(target, -trackWidth, trackWidth);
            hasChangedPosition = true;
        }

        if (input == 0 && hasChangedPosition)
        {

            hasChangedPosition = false;
        }

        grouded = Physics.Linecast(transform.position, foot.position,
            1 << LayerMask.NameToLayer("Ground"));
    }

    private void FixedUpdate()
    {
        //给角色添加重力
        body.AddForce(Vector3.down * gravity);

        float zPos = Mathf.MoveTowards(transform.position.z, target, changeTrackSpeed * Time.deltaTime);
        transform.position = new Vector3(
                transform.position.x,
                transform.position.y,
                zPos
            );

        //跳跃和下蹲都只有在人物脚在平面上的时候才能做
        //变道过程中不能跳跃或者蹲下
        if (grouded && arrived)
        {
            if (!useKinectInput)
            {
                //使用键盘当作输入
                if (Input.GetButton("Jump"))
                {
                    body.velocity = new Vector3(0, jumpForce, 0);
                    GetComponent<AudioSource>().Play();
                }

                if (Input.GetAxis("Vertical") < 0)
                {
                    Squat();
                }
            }
            else
            {
                //使用Kinect作为输入
                if (avatarCtrl.isJumping())
                {
                    body.velocity = new Vector3(0, jumpForce, 0);
                    GetComponent<AudioSource>().Play();
                }
                if (avatarCtrl.isCrouch())
                {
                    Squat();
                }
            }
        }
    }


    public void Death()
    {
        //Time.timeScale = 0;
    }
    public void Squat()
    {
        animator.Play("SLIDE00");
    }


    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Obstacle"))
        {
            Death();
            gameCtrl.Gameover();
        }
    }
}
