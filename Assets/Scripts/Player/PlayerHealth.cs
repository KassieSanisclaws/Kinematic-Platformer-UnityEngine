using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    // Declare public variables for player health
    public int maxHealth = 3;
    private int currentHealth;

    void Start() => currentHealth = maxHealth;

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        Debug.Log("Player took damage. Remaining HP: " + currentHealth);
        if (currentHealth <= 0) Debug.Log("Player Died!");
    }
}
