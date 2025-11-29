using UnityEngine;

public class SpeedPickup : MonoBehaviour
{
    public float speedMultiplier = 1.5f; // o quanto aumentar ao pegar o speed 
    public float duration = 5f;
    public bool destroyOnPickup = true;
    public AudioClip pickupSfx; // colocar depois

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        var mv = other.GetComponent<PlayerMovement>();
        if (mv != null)
        {
            mv.ApplySpeedBoost(speedMultiplier, duration);
            if (pickupSfx) AudioSource.PlayClipAtPoint(pickupSfx, transform.position);
            if (destroyOnPickup) Destroy(gameObject); else DisablePickup();
        }
    }

    private void DisablePickup()
    {
        var col = GetComponent<Collider>(); if (col) col.enabled = false;
        var rend = GetComponent<Renderer>(); if (rend) rend.enabled = false;
    }
}

