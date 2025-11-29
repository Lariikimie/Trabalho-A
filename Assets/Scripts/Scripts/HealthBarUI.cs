
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public Slider slider;

    void Start()
    {
        // Encontra referências
        if (playerHealth == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null) playerHealth = playerObj.GetComponent<PlayerHealth>();
        }

        if (slider == null)
        {
            slider = GetComponentInChildren<Slider>();
        }

        if (playerHealth == null || slider == null)
        {
            Debug.LogError("[HealthBarUI] Referências faltando (playerHealth ou slider).");
            return;
        }

        slider.minValue = 0;
        slider.maxValue = playerHealth.maxHealth;
        slider.value = playerHealth.currentHealth;

        playerHealth.onHealthChanged.AddListener(UpdateBar);
    }

    void UpdateBar(int current, int max) // update da vida
    {
        slider.maxValue = max;
        slider.value = current;
    }
}


