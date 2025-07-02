using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth instance;


    public int maxHealth;
    public int currentHealth;


    public UnityEvent<float, float> onHealthChanged;

    public AudioClip sfClip;
    public Transform playerPs;
    private void Awake()
    {

        if (instance == null)
            instance = this;

        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int dmg)
    {

        currentHealth = Mathf.Max(0, currentHealth - dmg);
        onHealthChanged.Invoke(currentHealth, maxHealth);
        if (currentHealth == 0) Die();
    }

    void Die()
    {
        SoundFManager.instance.PlaySoundSfxClip(sfClip, playerPs, .2f);
        Player.instance.PlayerDeathAnimation();
        GameManager.Instance.ShowGameOver();
    }

    
}
