using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float shootInterval = 2f;
    public float detectionRange = 20f;
    public float projectileSpeed = 5f;

    private Transform player;
    private float timer;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        timer = shootInterval;
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f && CanSeePlayer())
        {
            Shoot();
            timer = shootInterval;
        }
    }

    bool CanSeePlayer()
    {
        Vector3 dirToPlayer = player.position - transform.position;

        if (dirToPlayer.magnitude > detectionRange) return false;

        if (Physics.Raycast(transform.position, dirToPlayer.normalized,
            out RaycastHit hit, detectionRange))
        {
            return hit.collider.CompareTag("Player");
        }

        return false;
    }

    void Shoot()
    {
        Vector3 dirToPlayer = (player.position - transform.position).normalized;

        Vector3 spawnPos = transform.position + transform.forward * 1.5f;
        GameObject proj = Instantiate(projectilePrefab, spawnPos, Quaternion.identity);

        Rigidbody rb = proj.GetComponent<Rigidbody>();
        if (rb != null)
            rb.linearVelocity = dirToPlayer * projectileSpeed;

        Destroy(proj, 5f);
    }
}
