using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour
{
    public int maxHealth = 2;
    private int currentHealth;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("AttackArea"))
        {
            TakeDamage(1);
            StartCoroutine(BlinkSprite(Color.red));
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

    public void UpdateMaxHealth(int newMaxHealth)
    {
        maxHealth = newMaxHealth;
        currentHealth = newMaxHealth;
    }

    IEnumerator BlinkSprite(Color color)
    {
        float duration = 0.2f;
        float blinkInterval = 0.1f;

        for (float timer = 0; timer < duration; timer += blinkInterval)
        {
            spriteRenderer.color = color;
            yield return new WaitForSeconds(blinkInterval);
            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(blinkInterval);
        }

        spriteRenderer.color = Color.white;
    }
}
