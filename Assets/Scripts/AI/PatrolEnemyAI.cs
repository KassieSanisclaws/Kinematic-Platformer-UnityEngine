using UnityEngine;

public class PatrolEnemyAI : MonoBehaviour
{
    // This script is attached to a patrol enemy AI in a game. It handles the enemy's patrol behavior.
    public Transform[] waypoints;
    public float speed = 6f;
    public float arriveThreshold = 0.5f;

    private int currentIndex = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 target = waypoints[currentIndex].position;
        Vector3 dir = target - transform.position;
        dir.y = 0;

        transform.position += dir.normalized * speed * Time.deltaTime;

        if (dir.magnitude < arriveThreshold)
            currentIndex = (currentIndex + 1) % waypoints.Length;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth health = collision.gameObject.GetComponent<PlayerHealth>();
            if (health != null)
            {
                health.TakeDamage(1);
                Vector3 pushDir = (collision.transform.position - transform.position).normalized;
                collision.gameObject.GetComponent<CharacterController>().Move(pushDir * 2f);
            }
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            // Reverse waypoints on enemy collision
            System.Array.Reverse(waypoints);
        }
      }
    }
