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
            if(movement.x > 0)
            {
                transform.Translate(Vector3.right * movingSpeed * Time.deltaTime);
            }
            else if(movement.x < 0)
            {
                transform.Translate(Vector3.left * movingSpeed * Time.deltaTime);
            }
            if(movement.z > 0)
            {
                transform.Translate(Vector3.forward * movingSpeed * Time.deltaTime);
            }
            else if(movement.z < 0)
            {
                transform.Translate(Vector3.back * movingSpeed * Time.deltaTime);
            }
            animator.SetBool("IsWalking", isWalking);
        }
    }

    private void Conversation()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Collider[] others = Physics.OverlapSphere(transform.position, 2);
            foreach (Collider enemy in others)
            {
                if (enemy.TryGetComponent(out Enemy enemyDialogue))
                {
                    print("Enemy");
                    enemyDialogue.StartConversation();
                    //enemyDialogue.StartConversation();
                    //enemyDialogue.ContinueConversation();
                    //enemyDialogue.EndConversation();
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        PlayerRotate();

        Movement();

        Conversation();
        
        if (Input.GetKeyDown(KeyCode.C))
        {
            animator.SetTrigger("Capoeira");
        }
    }
}
