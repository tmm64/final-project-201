using UnityEngine;

public class WanderSpawner : MonoBehaviour
{
    public GameObject prefab;
    public float spawnDist = 5f;
 
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            Vector3 spawnPos = transform.position + transform.forward * spawnDist;
            GameObject spawned = Instantiate(prefab, spawnPos, Quaternion.identity);
            Destroy(spawned, 5);
        }
    }
}
