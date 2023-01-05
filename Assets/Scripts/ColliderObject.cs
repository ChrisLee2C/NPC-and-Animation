using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderObject : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private GameObject uiCanvas;
    [SerializeField] private List<Transform> spawnPoints;
    [SerializeField] private float force;
    private bool isGrounded = true;
    private bool isCollide = false;
    private float gravity = 10;
    private int index = 1;

    private void OnTriggerStay(Collider other)
    {
        if(other.GetComponent<PlayerController>() != null) { isCollide = true; }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerController>() != null) { isCollide = false; }
    }

    void ShowUI() { uiCanvas.SetActive(true); }

    void HideUI() { uiCanvas.SetActive(false); }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "Floor")
        {
            isGrounded = true;
        }
        else if(collision.gameObject.name == "Player") 
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name == "Floor")
        {
            isGrounded = false;
        }
    }

    void Thrust()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            rb.AddForce(Vector3.up * force, ForceMode.Impulse);
            isCollide = false;
        }
    }

    void Teleport()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            if(index == spawnPoints.Capacity) { index = 0; }
            gameObject.transform.position = spawnPoints[index].position;
            index++;
        }
    }

    void Gravity() { rb.AddForce(Vector3.down * gravity, ForceMode.Acceleration); }

    // Update is called once per frame
    void Update()
    {
        Gravity();

        if (isCollide && isGrounded)
        {
            Thrust();
            Teleport();
            ShowUI();
        }
        else
        {
            HideUI();
        }
    }
}
