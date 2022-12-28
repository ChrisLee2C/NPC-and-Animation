using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    private Animator animator;

    // Start is called before the first frame update
    void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        bool isWalking = Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0;
        animator.SetBool("IsWalking", isWalking);
        if (Input.GetKeyDown(KeyCode.C))
        {
            animator.SetTrigger("Capoeira");
        }
    }
}
