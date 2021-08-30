using UnityEngine;

public class NumberBallonScript : MonoBehaviour
{
    public int value;
    Rigidbody rb;
    public float timeToNextDirection;
    public float height;
    public Vector3 moveVelocity;
    public Transform ballonHolder;

    void Start()
    {
        height = transform.position.y;
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        Vector3 vector3 = transform.position;
        vector3.y = height;
        transform.position = vector3;
        if (Time.time > timeToNextDirection)
        {
            timeToNextDirection += UnityEngine.Random.Range(5f, 15f);
            moveVelocity = GetRandomDirection() * 5f;
        }
    }
    Vector3 GetRandomDirection()
    {
        return new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f));
    }
    private void FixedUpdate()
    {
        rb.velocity = moveVelocity;
    }
}