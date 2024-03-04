using UnityEngine;
using Mirror;

public class PlayerShoot : NetworkBehaviour
{
    [SerializeField]
    private LayerMask mask;

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    [Client]
    private void Shoot()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, 100f, mask))
        {
            Debug.DrawRay(transform.position, transform.forward, Color.green);
            if (hit.collider.tag == "Player")
            {
                CmdPlayerShot(hit.collider.name, 10);
            }
        }
    }

    [Command]
    private void CmdPlayerShot(string playerId, int damage)
    {
        Player enemy = GameManager.GetPlayer(playerId);
        enemy.RpcTakeDamage(damage);
        if (enemy.GetHealth() <= 0)
        {
            Player _player = transform.parent.parent.parent.GetComponent<Player>();
            RealmManager.Instance.IncreaseScore(transform.name);
            _player.IncreaseScore();
        }
    }
}
