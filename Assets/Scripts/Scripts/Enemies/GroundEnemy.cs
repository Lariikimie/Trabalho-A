using UnityEngine;

public class GroundEnemy : Enemy // Enemy
{
    [Header("Ground")]
    [SerializeField] private float stopDistance = 0.8f; // speed do sanji

    protected override void MoveTowardsPlayer() // seguir o luffy
    {
        if (player == null) return;

        Vector3 toPlayer = player.position - transform.position;
        toPlayer.y = 0f;
        float distance = toPlayer.magnitude;

        if (toPlayer.sqrMagnitude > 0.001f)
            transform.rotation = Quaternion.LookRotation(toPlayer.normalized, Vector3.up);

        if (distance > stopDistance)
            transform.position += toPlayer.normalized * speed * Time.deltaTime;
    }
}
