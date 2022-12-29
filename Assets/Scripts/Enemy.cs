using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject textBox;
    [SerializeField] private Text dialogueText;
    [SerializeField] NPCDialogueScriptableObject npcDialogue;
    private int currentText = 0;
    private Animator animator;
    private bool isTalk = false;

    // Start is called before the first frame update
    void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>() != null)
        {
            dialogueText.text = "Press Spacebar to Start Conversation";
            ShowUI();
            Dance();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<PlayerController>() != null)
        {
            isTalk = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerController>() != null)
        {
            HideUI();
            currentText = 0;
            isTalk = false;
        }
    }

    public void ShowDialogue()
    {
        if (currentText < npcDialogue.dialogue.Length)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                dialogueText.text = npcDialogue.dialogue[currentText];
                currentText++;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                HideUI();
            }
        }
    }

    public void ShowUI()
    {
        textBox.gameObject.SetActive(true);
    }

    public void HideUI()
    {
        textBox.gameObject.SetActive(false);
    }

    public void Dance()
    {
        animator.SetTrigger("Capoeira");
    }

    // Update is called once per frame
    void Update()
    {
        //    bool isWalking = Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0;
        //    animator.SetBool("IsWalking", isWalking);
        if (isTalk)
        {
            ShowDialogue();
        }
    }
}
