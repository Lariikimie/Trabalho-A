using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int healAmount = 20; // quando de vida ele da
    public bool destroyOnPickup = true;
    public AudioClip pickupSfx; // colocar ddps

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        var hp = other.GetComponent<PlayerHealth>();
        if (hp != null)
        {
            hp.Heal(healAmount);
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


