using UnityEngine;

public class Fly : MonoBehaviour
{
    [SerializeField] private float speed;       
    [SerializeField] private float tapForce;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float maxRotation;
    [SerializeField] private float minRotation;

    private Rigidbody2D rb;
    private Quaternion tiltUpRotation;
    private Quaternion tiltDownRotation;

    private Vector2 startPosition;
    private bool isAlive = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        tiltUpRotation = Quaternion.Euler(0f, 0f, maxRotation);
        tiltDownRotation = Quaternion.Euler(0f, 0f, minRotation);
    }

    private void Start()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        if (!isAlive) return;

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            Flap();
        }

        transform.rotation = Quaternion.Lerp(transform.rotation, tiltDownRotation, rotationSpeed * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        if (!isAlive) return;

        rb.linearVelocity = new Vector2(speed, rb.linearVelocity.y);
    }

    private void Flap()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, tapForce);
        transform.rotation = tiltUpRotation;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isAlive) return;

        Die();
        if (GameManager.Instance != null)
        {
            GameManager.Instance.GameOver();
        }
    }

    public void Die()
    {
        isAlive = false;
        rb.linearVelocity = Vector2.zero;
    }

    public void ResetBird()
    {
        transform.position = startPosition;
        transform.rotation = Quaternion.identity;
        rb.linearVelocity = Vector2.zero;
        isAlive = true;
    }
}