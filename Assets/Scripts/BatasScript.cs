using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatasScript : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Player")
        {
            PlayerHealth playerHealth = collision.transform.GetComponent<PlayerHealth>();
            playerHealth.GetDamage(10);
        }
    }
}
