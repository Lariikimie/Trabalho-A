using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;     // configurar a speed no inspector
    public float Speed
    {
        get => _speed;
        set => _speed = Mathf.Max(0f, value);       
    }

    public float jumpForce = 5f;
    public Rigidbody rb;
    private bool isGrounded = true;

    void Start() // movimento wsad
    {
        if (rb == null) rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 dir = new Vector3(h, 0f, v);
        transform.Translate(dir * _speed * Time.deltaTime, Space.World);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    void OnCollisionEnter(Collision collision) // colisões 
    {
        if (collision.gameObject.CompareTag("Ground"))
            isGrounded = true;
    }
    public void ApplySpeedBoost(float multiplier, float duration)
    {
        StopAllCoroutines();
        StartCoroutine(SpeedBoostRoutine(multiplier, duration));
    }

    private System.Collections.IEnumerator SpeedBoostRoutine(float multiplier, float duration)
    {
        float original = _speed;
        _speed = original * multiplier;
        yield return new WaitForSeconds(duration);
        _speed = original;
    }
}



