using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Enemy Health settings")]
    [SerializeField] int maxHealth = 100;
    [SerializeField] int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void EnemyTakeDamage(int damage)
    {
        currentHealth -= damage;

        if(currentHealth <= 0)
        {
            ScoreManager.instance.AddScore(20);
            Destroy(gameObject);
        }
    }
}