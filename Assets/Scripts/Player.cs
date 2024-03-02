using Mirror;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Player : NetworkBehaviour
{
    [SerializeField] private int maxHealth = 100;

    [SyncVar] private int health;
    [SyncVar] private int score;

    private void Awake()
    {
        health = maxHealth;
    }

    public float GetHealth()
    {
        return (float) health / (float) maxHealth;
    }

    public int GetScore()
    {
        return score;
    }

    public void IncreaseScore()
    {
        score++;
    }

    public bool TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
            StartCoroutine(Respawn());
        return (health <= 0);
    }

    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(MatchSettings.secondsForSpawn);
        Transform spawnPoint = NetworkManager.singleton.GetStartPosition();
        transform.position = spawnPoint.position;
        transform.rotation = spawnPoint.rotation;
        health = maxHealth;
    }
}
