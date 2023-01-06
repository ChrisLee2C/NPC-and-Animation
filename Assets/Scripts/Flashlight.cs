using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    public bool isEquip = false;
    [SerializeField] GameObject uiCanvas;
    private Animator animator;
    private bool isPlayer = false;

    //Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
        if (gameObject.transform.parent) { isEquip = true; }
    }

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

    public void Blink()
    {
        if (isEquip == true) { animator.SetTrigger("Blink"); }
    }

    public void Equip(Transform equipPosition)
    {
        gameObject.transform.SetParent(equipPosition);
        gameObject.transform.localPosition = equipPosition.localPosition;
        gameObject.transform.localEulerAngles = new Vector3(0, 0, 90);
        gameObject.transform.localScale = equipPosition.localScale;
        isEquip = true;
    }

    private void Update()
    {
        CheckPlayer();

        if (isEquip != true && isPlayer == true) { ShowUI(); } else { HideUI(); }
    }
}
