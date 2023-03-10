using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [HideInInspector] public bool isTalking = false;
    [SerializeField] private float movingSpeed;
    [SerializeField] private float turningSpeed;
    private int speedModifier = 2;
    private float cameraRotateX;
    private float cameraRotateY;
    private Animator animator;
    private Camera mainCamera;

    // Start is called before the first frame update
    void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
        mainCamera = Camera.main;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void PlayerRotate()
    {
        cameraRotateX += Input.GetAxis("Mouse X");
        cameraRotateY -= Input.GetAxis("Mouse Y");
        cameraRotateY = Mathf.Clamp(cameraRotateY, -45, 45);
        mainCamera.transform.eulerAngles = new Vector3(cameraRotateY, cameraRotateX, 0);
        gameObject.transform.eulerAngles = new Vector3(0, cameraRotateX, 0);
    }

    void Movement()
    {
        Vector3 movement = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
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
