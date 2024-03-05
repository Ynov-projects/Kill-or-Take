using Mirror;
using UnityEngine;

public class Player : NetworkBehaviour
{
    [SerializeField] private int maxHealth = 100;

    [SyncVar] public int health;

    [SyncVar] public string realmUserId;

    private void Start()
    {
        health = maxHealth;
    }

    public float GetHealth()
    {
        return (float)health / (float)maxHealth;
    }

    private void OnDisable()
    {
        if(isLocalPlayer)
            RealmManager.Instance.SetHighScore(GameManager.GetLogged(transform.name));
        LogManager.Instance.alreadyChecked = false;
    }

    [ClientRpc]
    public void RpcTakeDamage(int amount, Transform _player)
    {
        health -= amount;
        if (health <= 0)
        {
            Respawn();
            string loggedId = GameManager.GetLogged(_player.name);
            RealmManager.Instance.IncreaseScore(loggedId);
        }
    }

    public void Respawn()
    {
        Transform spawnPoint = NetworkManager.singleton.GetStartPosition();
        transform.position = spawnPoint.position;
        transform.rotation = spawnPoint.rotation;
        health = maxHealth;
    }
}
