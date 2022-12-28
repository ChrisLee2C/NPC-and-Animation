using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float movingSpeed;
    [SerializeField] private float turningSpeed;
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

    void RotateCamera()
    {
        cameraRotateX += Input.GetAxis("Mouse X");
        cameraRotateX = Mathf.Clamp(cameraRotateX, -45, 45);
        cameraRotateY -= Input.GetAxis("Mouse Y");
        cameraRotateY = Mathf.Clamp(cameraRotateY, -45, 45);
        mainCamera.transform.eulerAngles = new Vector3(cameraRotateY, cameraRotateX, 0);
    }

    void Movement()
    {
        Vector3 movement = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        movement = movement.normalized;
        bool isWalking = movement.x != 0 || movement.z != 0;
        if (isWalking)
        {
            transform.position += movement * Time.deltaTime * movingSpeed;
            animator.SetBool("IsWalking", isWalking);
        }
    }

    // Update is called once per frame
    void Update()
    {
        RotateCamera();

        Movement();
        
        if (Input.GetKeyDown(KeyCode.C))
        {
            animator.SetTrigger("Capoeira");
        }
    }
}
