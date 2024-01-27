using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetect : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    private PlayerMovement playerMovement;

    private BoxCollider2D boxCollider;
    // Start is called before the first frame update
    void Start()
    {
        playerMovement = player.GetComponent<PlayerMovement>();
        boxCollider = player.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ground")
        {
            playerMovement.airCount = 0;
            playerMovement.isGrounded = true;
            Debug.Log("isGrounded");
            // boxCollider.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Ground")
        {
            playerMovement.isGrounded = false;
            Debug.Log("isNotGrounded");
            //boxCollider.enabled = false;
        }
    }

}
