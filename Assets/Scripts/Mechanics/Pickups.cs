using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickups : MonoBehaviour
{
    public enum PickupType
    {
        Powerup,
        Life,
        Score,
    }

    public PickupType currentPickup;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            switch (currentPickup)
            {
                case PickupType.Powerup:
                    collision.GetComponent<PlayerController>().StartJumpForceChange();
                    //do powerup functionality
                    break;
                case PickupType.Life:
                    GameManager.Instance.lives++;
                    //do life functionality
                    break;
                case PickupType.Score:
                    GameManager.Instance.score++;
                    //do score functionality
                    break;
            }

            Destroy(gameObject);

            
        }
    }
}
