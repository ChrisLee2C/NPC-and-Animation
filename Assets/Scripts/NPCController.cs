using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Animations.Rigging;

public class NPCController : MonoBehaviour
{
    [SerializeField] private GameObject textBox;
    [SerializeField] private Text dialogueText;
    [SerializeField] private Text nameText;
    [SerializeField] private Text overHeadNameText;
    [SerializeField] private List<Transform> wayPoints;
    [SerializeField] private Transform target;
    [SerializeField] NPCDialogueScriptableObject npcDialogue;
    private NavMeshAgent navMeshAgent;
    private Animator animator;
    private Transform playerPosition;
    private Vector3 defaultTransform;
    private int currentText = 0;
    private int currentDestination = 0;
    private bool isTalk = false;

    void Awake() 
    { 
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        currentDestination = Random.Range(0, wayPoints.Capacity);
        navMeshAgent.destination = new Vector3(wayPoints[currentDestination].position.x, wayPoints[currentDestination].position.y, wayPoints[currentDestination].position.z);
        animator.SetBool("IsWalking", true);
        overHeadNameText.text = npcDialogue.npcName;
        defaultTransform = target.localPosition;
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
            playerPosition = other.transform;
        }
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

    private void Patrol(Transform destination, bool isWalk)
    {
        navMeshAgent.SetDestination(new Vector3(destination.position.x, destination.position.y, destination.position.z));
        animator.SetBool("IsWalking", isWalk);
        if (navMeshAgent.remainingDistance < 0.2f)
        {
            int newDestination = Random.Range(0, wayPoints.Capacity);
            while (newDestination == currentDestination)
            {
                currentDestination = Random.Range(0, wayPoints.Capacity);
            }
        }
    }

    public void ShowUI() { textBox.gameObject.SetActive(true); }

    public void HideUI() { textBox.gameObject.SetActive(false); }

    public void Dance() { animator.SetTrigger("Capoeira"); }

    // Update is called once per frame
    void Update()
    {
        if(isTalk != true)
        {
            Patrol(wayPoints[currentDestination], !isTalk);
            target.localPosition = new Vector3(Mathf.Lerp(target.localPosition.x, defaultTransform.x, Time.deltaTime), Mathf.Lerp(target.localPosition.y, defaultTransform.y, Time.deltaTime), Mathf.Lerp(target.localPosition.z, defaultTransform.z, Time.deltaTime));
        }
        else
        {
            ShowDialogue();
            Patrol(transform, !isTalk);
            target.position = new Vector3(Mathf.Lerp(target.position.x, playerPosition.position.x, Time.deltaTime), Mathf.Lerp(target.position.y, playerPosition.position.y+(float)1.7, Time.deltaTime), Mathf.Lerp(target.position.z, playerPosition.position.z, Time.deltaTime));
        }
    }
}
