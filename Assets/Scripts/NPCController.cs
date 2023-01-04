using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCController : MonoBehaviour
{
    [SerializeField] private GameObject textBox;
    [SerializeField] private Text dialogueText;
    [SerializeField] private Text nameText;
    [SerializeField] NPCDialogueScriptableObject npcDialogue;
    private Animator animator;
    private int currentText = 0;
    private bool isTalk = false;

    void Awake() { animator = gameObject.GetComponent<Animator>(); }

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
        if (other.GetComponent<PlayerController>() != null) { isTalk = true; }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerController>() != null)
        {
            HideUI();
            currentText = 0;
            nameText.text = "";
            isTalk = false;
        }
    }

    public void ShowDialogue()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (currentText < npcDialogue.dialogue.Length)
            {
                nameText.text = npcDialogue.npcName + ":";
                dialogueText.text = npcDialogue.dialogue[currentText];
                currentText++;
            }
            else
            {
                HideUI();
                nameText.text = "";
            }
        }
    }

    public void ShowUI() { textBox.gameObject.SetActive(true); }

    public void HideUI() { textBox.gameObject.SetActive(false); }

    public void Dance() { animator.SetTrigger("Capoeira"); }

    // Update is called once per frame
    void Update()
    {
        if (isTalk) { ShowDialogue(); }
    }
}
