
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement; 
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    [Header("Vida")]
    public int maxHealth = 100;
    public int currentHealth;

    [Header("Eventos")]
    public UnityEvent<int, int> onHealthChanged; // vidaAtual e vidaMax
    public UnityEvent onDied;

    [Header("Morte / Rein�cio")]
    public float restartDelay = 2f;       // tempo em segundos antes de reiniciar
    public bool disableMovementOnDeath = true;

    void Start()
    {
        currentHealth = maxHealth;
        onHealthChanged?.Invoke(currentHealth, maxHealth);
    }

    public void TakeDamage(int damage)
    {
        if (currentHealth <= 0) return;

        currentHealth = Mathf.Max(currentHealth - damage, 0);
        onHealthChanged?.Invoke(currentHealth, maxHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        if (currentHealth <= 0) return; // pra n curar quando morrer
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        onHealthChanged?.Invoke(currentHealth, maxHealth);
    }

    void Die()
    {
        Debug.Log("Player morreu!");

      
        onDied?.Invoke();

        //  desabilita movimento ao morrer
        if (disableMovementOnDeath)
        {
            var move = GetComponent<PlayerMovement>();
            if (move != null) move.enabled = false;

            var rb = GetComponent<Rigidbody>();
            if (rb != null) rb.linearVelocity = Vector3.zero;
        }

        // Reinicia a cena com delay
        StartCoroutine(RestartSceneAfterDelay());
    }

    private IEnumerator RestartSceneAfterDelay()
    {
        yield return new WaitForSeconds(restartDelay);

        // Carrega novamente a cena atual
        var active = SceneManager.GetActiveScene();
        SceneManager.LoadScene(active.name);
    }
}



