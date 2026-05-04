using UnityEngine;

public class Rotate : MonoBehaviour
{
    public Vector3 rotateAngle = new Vector3(0, 90, 0);
    public float speed = 1;
    public GameObject target;
    // Update is called once per frame
    void Update()
    {
        target.transform.Rotate(rotateAngle * Time.deltaTime * speed);


    }
}
