using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Camera UICamera;
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

    public void StartConversation()
    {
        dialogueText.text = "Press Spacebar to Start Conversation";
        ShowUI();
        AttachCamera();
    }

    public void ContinueConversation()
    {
        isTalk = true;
    }

    public void EndConversation()
    {
        HideUI();
        currentText = 0;
        isTalk = false;
    }

    private void AttachCamera()
    {
        if (gameObject.GetComponentInChildren<Camera>() == null)
        {
            Instantiate(UICamera, new Vector3(0, 0, -2), Quaternion.identity, gameObject.transform);
        }
    }

    private void DetachCamera()
    {
        Destroy(GetComponentInChildren<Camera>().gameObject);
    }

    public void ShowDialogue()
    {
        if (currentText < npcDialogue.dialogue.Length)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (npcDialogue.dialogue[currentText].Contains("Kill"))
                {
                    Attack();
                }
                dialogueText.text = npcDialogue.dialogue[currentText];
                currentText++;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                HideUI();
                Die();
                DetachCamera();
            }
        }
    }

    private void Die()
    {
        animator.SetTrigger("Die");
    }

    private void Attack()
    {
        animator.SetTrigger("Attack");
    }

    public void ShowUI()
    {
        textBox.gameObject.SetActive(true);
    }

    public void HideUI()
    {
        textBox.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isTalk)
        {
            ShowDialogue();
        }
    }
}
