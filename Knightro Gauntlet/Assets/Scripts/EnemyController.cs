using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int maxHealth = 2;
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("AttackArea"))
        {
            TakeDamage(1);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
