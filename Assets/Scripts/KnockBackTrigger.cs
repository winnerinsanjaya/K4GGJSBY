using UnityEngine;

public class KnockbackTrigger : MonoBehaviour
{
    [SerializeField]
    private float knockbackValue;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {

            PlayerMovement playerMovement = collision.GetComponent<PlayerMovement>();
            if (playerMovement != null)
            {
                playerMovement.Knockback(transform, knockbackValue);
            }
        }
    }
}