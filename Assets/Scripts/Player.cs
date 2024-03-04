using Mirror;
using UnityEngine;

public class Player : NetworkBehaviour
{
    [SerializeField] private int maxHealth = 100;

    [SyncVar] public int health;
    [SyncVar] private int score;

    private void Start()
    {
        health = maxHealth;
    }

    public float GetHealth()
    {
        return (float)health / (float)maxHealth;
    }

    public int GetScore()
    {
        return score;
    }

    public void IncreaseScore()
    {
        score++;
    }

    private void OnDisable()
    {
        RealmManager.Instance.SetHighScore(transform.name);
    }

    [ClientRpc]
    public void RpcTakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
            Respawn();
    }

    private void Respawn()
    {
        Transform spawnPoint = NetworkManager.singleton.GetStartPosition();
        transform.position = spawnPoint.position;
        transform.rotation = spawnPoint.rotation;
        health = maxHealth;
    }
}
