using UnityEngine;

public class Enemy : MonoBehaviour 
{
    [Header("Base Enemy")]
    [SerializeField] protected float speed = 2f; // base de speed
    [SerializeField] protected int contactDamage = 10; // dado no luffy
    [SerializeField] protected float damageCooldown = 1.0f; // um cooldown para cada hit

    protected Transform player; 
    protected float lastDamageTime = -999f; 

    public void SetTarget(Transform target) => player = target; // transformando o luffy em um target pro sanji ir atrás

    protected virtual void Awake()
    {
        var playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj) player = playerObj.transform;
    }

    protected virtual void Update() 
    {
        if (player == null) return;
        MoveTowardsPlayer();
    }

    protected virtual void MoveTowardsPlayer()
    {
        Vector3 dir = (player.position - transform.position);
        dir.y = 0f;
        if (dir.sqrMagnitude < 0.0001f) return;

        transform.rotation = Quaternion.LookRotation(dir.normalized, Vector3.up);
        transform.position += dir.normalized * speed * Time.deltaTime;
    }

    protected virtual void TryDealContactDamage(Collision collision) //colisão e dano
    {
        if (!collision.gameObject.CompareTag("Player")) return;
        if (Time.time - lastDamageTime < damageCooldown) return;

        PlayerHealth hp = collision.gameObject.GetComponent<PlayerHealth>();
        if (hp != null)
        {
            hp.TakeDamage(contactDamage);
            lastDamageTime = Time.time;
        }
    }

    protected virtual void OnCollisionEnter(Collision collision) => TryDealContactDamage(collision);
    protected virtual void OnCollisionStay(Collision collision) => TryDealContactDamage(collision);
}

