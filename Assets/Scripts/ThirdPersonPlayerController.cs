using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonPlayerController : MonoBehaviour
{
    [HideInInspector] public bool isTalking = false;
    [SerializeField] private float movingSpeed;
    [SerializeField] private float turningSpeed = 1;
    private int speedModifier = 2;
    private Animator animator;
    private Vector3 movement;
    private Quaternion rotation;
    private Rigidbody rigidbody;

    // Start is called before the first frame update
    void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
        Cursor.lockState = CursorLockMode.Locked;
        rotation = Quaternion.identity;
        rigidbody = GetComponent<Rigidbody>();
    }

    void PlayerRotate()
    {
        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, movement, turningSpeed * Time.deltaTime, 0f); 
        rotation = Quaternion.LookRotation(desiredForward);
        rigidbody.MoveRotation(rotation);
    }

    void Movement()
    {
        movement = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        movement = movement.normalized;
        bool isWalking = movement.x != 0 || movement.z != 0;
        if (isWalking)
        {
            transform.Translate(movement * movingSpeed * speedModifier * Time.deltaTime);
            animator.SetBool("IsWalking", isWalking);
        }
        else
        {
            animator.SetBool("IsWalking", isWalking);
        }
    }

    void SpeedUp()
    {
        if (Input.GetKey(KeyCode.LeftShift)) { speedModifier = 2; }
    }

    void SlowDown()
    {
        if (Input.GetKeyUp(KeyCode.LeftShift)) { speedModifier = 1; }
    }

    void Dance() { animator.SetTrigger("Capoeira"); }

    // Update is called once per frame
    void Update()
    {
        if(isTalking != true)
        {
            Movement();

            PlayerRotate();

            SpeedUp();

            SlowDown();

            if (Input.GetKeyDown(KeyCode.C)) { Dance(); }
        }
    }
}
