using UnityEngine;
using Mirror;

public class PlayerShoot : NetworkBehaviour
{
    [SerializeField]
    private LayerMask mask;

    private string error = "";

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
        Debug.Log(playerId + " a été touché.");

        try
        {
            Player player = GameManager.GetPlayer(playerId);
            player.TakeDamage(damage);
        }
        catch(System.Exception e)
        {
            error = e.Message;
        }
    }

    [System.Obsolete]
    private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(400, 400, 200, 500));
        GUILayout.BeginHorizontal();

        foreach (AudioListener audio in GameObject.FindObjectsOfType<AudioListener>())
        {
            GUILayout.Label(audio.name);
        }

        GUILayout.EndHorizontal();
        GUILayout.EndArea();
    }
}
