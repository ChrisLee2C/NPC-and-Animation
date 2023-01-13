using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    [SerializeField] GameObject uiCanvas;
    private Animator animator;
    private bool isPlayer = false;

    void Awake() => animator = GetComponent<Animator>();

    void CheckPlayer()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.up), out hit, 1))
        {
            if (hit.transform.GetComponent<PlayerController>() != null) { isPlayer = true; }
        }
        else
        {
            isPlayer = false;
        }
    }

    void ShowUI() { uiCanvas.SetActive(true); }

    void HideUI() { uiCanvas.SetActive(false); }

    public void Blink() => animator.SetTrigger("Blink"); 

    private void Update()
    {
        CheckPlayer();

        if (isPlayer == true) { ShowUI(); } else { HideUI(); }
    }
}
