using UnityEngine;

public class FlyingEnemy : Enemy // Enemy
{
    [Header("Flying")]
    [SerializeField] private float keepHeight = 5f;  // speed do sanji

    protected override void MoveTowardsPlayer()  // seguir o luffy
    {
        if (player == null) return;

        Vector3 targetPos = new Vector3(player.position.x, keepHeight, player.position.z);
        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

        Vector3 lookPos = new Vector3(player.position.x, transform.position.y, player.position.z);
        transform.LookAt(lookPos);
    }
}




