using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    // Declare public variables for player health
    public int maxHealth = 3;
    private int currentHealth;
    private Vector3 spawnPosition;
    private CharacterController characterController;
    private PlayerController playerController;
    public float respawnDelay = 2f;

    void Start()
    {
        currentHealth = maxHealth;
        spawnPosition = transform.position; // Save initial spawn point
        characterController = GetComponent<CharacterController>();
        playerController = GetComponent<PlayerController>();
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        Debug.Log("Player took damage. Remaining HP: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Player died!");
        playerController.enabled = false;
        characterController.enabled = false;
        // Hide Player
        //GetComponent<Renderer>().enabled = false;

        Invoke(nameof(Respawn), respawnDelay);
    }

    private void Respawn()
    {
        transform.position = spawnPosition;
        currentHealth = maxHealth;

        // Re-enable movement and visuals
        characterController.enabled = true;
        playerController.enabled = true;
        GetComponent<Renderer>().enabled = true;

        Debug.Log("Player respawned with " + maxHealth + " health.");
    }
  }
