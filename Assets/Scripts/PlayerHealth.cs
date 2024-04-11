using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth = 20f;
    [SerializeField] private GameObject bloodDropsFX;
    [SerializeField] private float dmgImmunityRate = .5f;
    [SerializeField] private Slider healthBar;
    [SerializeField] private Image bloodScreen;
    [SerializeField] private float smoothColor = 2f;
    [SerializeField] private AudioClip playerGrunt;

    private float currentHealth;
    private float nextDamage;
    private bool isDamaged;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        currentHealth = maxHealth;
        healthBar.maxValue = maxHealth;
        healthBar.value = maxHealth;
        nextDamage = 0f;
    }

    private void Update()
    {
        if (isDamaged)
        {
            bloodScreen.color = new(255, 255, 255, .8f);
        }
        else
        {
            bloodScreen.color = Color.Lerp(
                bloodScreen.color,
                new(255, 255, 255, 0f),
                Time.deltaTime * smoothColor);
        }
        isDamaged = false;
    }

    public void GetHealth(float healAmmount)
    {
        currentHealth += healAmmount;
        if (currentHealth > maxHealth) currentHealth = maxHealth;
        healthBar.value = currentHealth;
    }

    public void TakeDamage(float damage)
    {
        if (nextDamage < Time.time)
        {
            nextDamage = Time.time + dmgImmunityRate;
            Instantiate(bloodDropsFX, transform.position, bloodDropsFX.transform.localRotation);
            currentHealth -= damage;
            audioSource.PlayOneShot(playerGrunt, 0.6f);
            isDamaged = true;
            healthBar.value = currentHealth;

            if (currentHealth <= 0) MakeDeath();
        }
    }

    private void MakeDeath()
    {
        Destroy(gameObject);
        bloodScreen.color = new(255, 255, 255, 1f);
    }
}
