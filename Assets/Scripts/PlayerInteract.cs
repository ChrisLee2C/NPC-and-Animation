using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] GameObject equipPosition;
    PlayerController playerController;
    private bool isEquip = false;

    private void Awake() { playerController = GetComponent<PlayerController>(); }

    public Enemy GetEnemy()
    {
        Collider[] others = Physics.OverlapSphere(transform.position, 2);
        foreach (Collider item in others)
        {
            if (item.TryGetComponent(out Enemy enemy))
            {
                return enemy;
            }
        }
        return null;
    }

    void TalkWithNPC()
    {
        Collider[] others = Physics.OverlapSphere(transform.position, 2);
        foreach (Collider item in others)
        {
            if (item.TryGetComponent(out Enemy enemy))
            {
                if (enemy.isDead != true)
                {
                    enemy.StartConversation();
                    playerController.isTalking = true;
                }
            }
        }
    }

    void UseFlashLight()
    {
        Flashlight flashlight = gameObject.GetComponentInChildren<Flashlight>();
        if (flashlight != null) 
        { 
            flashlight.Blink(); 

        }
    }


    void EquipItem()
    {
        Collider[] others = Physics.OverlapSphere(transform.position, 2);
        foreach (Collider item in others)
        {
            if (item.TryGetComponent(out Flashlight flashlight)) 
            {
                if (isEquip == false)
                {
                    flashlight.transform.SetParent(equipPosition.transform);
                    flashlight.transform.localPosition = equipPosition.transform.localPosition;
                    flashlight.transform.localEulerAngles = new Vector3(0, 0, 90);
                    flashlight.transform.localScale = equipPosition.transform.localScale;
                    flashlight.Equip();
                    isEquip = true;
                }
            }
        }
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.E)) { EquipItem(); }

        if (Input.GetKeyDown(KeyCode.Space)) { TalkWithNPC(); }

        if (Input.GetMouseButtonDown(0)) { UseFlashLight(); }
    }
}
