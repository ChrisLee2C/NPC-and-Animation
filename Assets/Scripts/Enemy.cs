using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [HideInInspector] public bool isDead = false;
    [SerializeField] private PlayerInteract playerInteract;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private EnemyUI enemyUI;
    private Animator animator;
    private bool isAttack = false;

    // Start is called before the first frame update
    void Awake() { animator = gameObject.GetComponent<Animator>(); }

    public void StartConversation()
    {
        if(isDead != true)
        {
            enemyUI.ShowUI();
            enemyUI.AttachCamera();
        }
    }

    public void ContinueConversation() { enemyUI.ShowDialogue(); }

    public IEnumerator EndConversation() 
    { 
        playerController.isTalking = false;
        isDead = true;
        Die();
        yield return new WaitForSeconds(4);
        animator.SetTrigger("Respawn");
        yield return new WaitForSeconds(3);
        isDead = false;
    }

    public IEnumerator Attack() 
    { 
        animator.SetTrigger("Attack");
        isAttack = true;
        yield return new WaitForSeconds(3);
        isAttack = false;
    }
    public void Die() { animator.SetTrigger("Die"); }

    private void CheckPlayerNearby()
    {
        if (playerInteract.GetEnemy() != null && isDead != true)
        {
            enemyUI.ShowPrompt();
        }
        else
        {
            enemyUI.HidePrompt();
        }
    }

    private void Update()
    {
        CheckPlayerNearby();

        if(playerController.isTalking == true && isAttack != true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ContinueConversation();
            }
        }
    }
}
