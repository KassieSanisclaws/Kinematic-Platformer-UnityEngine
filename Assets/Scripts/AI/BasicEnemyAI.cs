using UnityEngine;

public class BasicEnemyAI : MonoBehaviour
{
    // Declare public variables for enemy AI behavior
    public float detectionRadius = 10f;
    public float speed = 4f;
    public float attackCooldown = 2f;
    public Transform player;

    private AIState state = AIState.Idle;
    private float cooldownTimer = 0f;

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        switch (state)
        {
            case AIState.Idle:
                Wander();
                if (distance < detectionRadius) state = AIState.Attack;
                break;

            case AIState.Attack:
                SeekPlayer();
                if (distance > detectionRadius) state = AIState.Idle;
                break;

            case AIState.CoolDown:
                cooldownTimer += Time.deltaTime;
                if (cooldownTimer >= attackCooldown)
                {
                    cooldownTimer = 0f;
                    state = AIState.Idle;
                }
                break;
           }
        }

    // Wander around randomly when idle
    void Wander()
    {
        // Simple random walk on plane
        transform.Translate(Vector3.forward * Mathf.Sin(Time.time) * speed * Time.deltaTime);
    }

    void SeekPlayer()
    {
        Vector3 dir = player.position - transform.position;
        dir.y = 0f;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector3 normal = collision.contacts[0].normal;
            if (normal.y > 0.5f) // player on top
                Destroy(gameObject);
            else
            {
                // Damage player
                PlayerHealth health = collision.gameObject.GetComponent<PlayerHealth>();
                if (health != null)
                {
                    health.TakeDamage(1);
                    state = AIState.CoolDown;
                }
            }
        }
     }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
    {
        
    }
}
