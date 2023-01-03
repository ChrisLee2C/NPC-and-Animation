using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    PlayerController playerController;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }

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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Collider[] others = Physics.OverlapSphere(transform.position, 2);
            foreach (Collider item in others)
            {
                if (item.TryGetComponent(out Enemy enemy))
                {
                    if(enemy.isDead != true)
                    {
                        playerController.isTalking = true;
                        enemy.StartConversation();
                    }
                }
            }
        }
    }
}
