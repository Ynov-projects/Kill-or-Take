using Mirror;
using UnityEngine;

public class Player : NetworkBehaviour
{
    [SerializeField] private int maxHealth = 100;

    [SyncVar] public int health;

    [SyncVar] public string realmUserId;

    private NetworkManager networkManager;

    private void Start()
    {
        health = maxHealth;
        networkManager = NetworkManager.singleton;
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

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "CatchBall" && gameObject.layer == LayerMask.NameToLayer("LocalPlayer"))
        {
            try
            {
                Disconnect();
            }
            catch (System.Exception e)
            { }
        }
    }

    public void Disconnect()
    {
        if (isClientOnly)
        {
            networkManager.StopClient();
        }
        else
        {
            networkManager.StopHost();
        }
    }
}
