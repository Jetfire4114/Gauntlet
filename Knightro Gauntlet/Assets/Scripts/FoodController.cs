using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController playerController = other.GetComponent<PlayerController>();

            if (playerController != null)
            {
                Destroy(gameObject);

                if (gameObject.CompareTag("Apple"))
                {
                    playerController.AddTime(3f);
                }
                else if (gameObject.CompareTag("Meat"))
                {
                    playerController.AddTime(5f);
                }
            }
        }
    }
}
