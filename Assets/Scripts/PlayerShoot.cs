using UnityEngine;
using Mirror;

public class PlayerShoot : NetworkBehaviour
{
    [SerializeField] private LayerMask mask;

    [SerializeField] private Camera cam;

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            GetComponent<PlayerSetup>().DisplayScorePanel();
        }
    }

    [Client]
    private void Shoot()
    {
        RaycastHit hit;

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 100f, mask))
        {
            Debug.Log(hit.collider.transform.name);
            if (hit.collider.tag == "Player")
                CmdPlayerShot(hit.collider.name, 10);
        }
    }

    [Command]
    private void CmdPlayerShot(string playerId, int damage)
    {
        Player enemy = GameManager.GetPlayer(playerId);
        enemy.RpcTakeDamage(damage, transform);
    }
}
