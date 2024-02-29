using UnityEngine;
using Mirror;

public class PlayerSetup : NetworkBehaviour
{
    [SerializeField]
    private Behaviour[] componentsToDisable;

    [SerializeField] private string remoteLayerName = "RemotePlayer";

    Camera sceneCamera;
    [SerializeField] GameObject life;

    private void Start()
    {
        if (!isLocalPlayer)
        {
            DisableComponents();
            assignRemotePlayer();
        }
        else
        {
            sceneCamera = Camera.main;
            if (sceneCamera != null)
            {
                sceneCamera.gameObject.SetActive(false);
                life.SetActive(true);
                //sceneCamera.gameObject.GetComponent<AudioListener>().enabled = false;
            }
        }
    }

    public override void OnStartClient()
    {
        base.OnStartClient();

        GameManager.RegisterPlayer(GetComponent<NetworkIdentity>().netId.ToString(), GetComponent<Player>());
    }

    private void DisableComponents()
    {
        for (int i = 0; i < componentsToDisable.Length; i++)
        {
            componentsToDisable[i].enabled = false;
        }
    }

    private void assignRemotePlayer()
    {
        gameObject.layer = LayerMask.NameToLayer(remoteLayerName);
    }

    private void OnDisable()
    {
        if (sceneCamera != null)
            sceneCamera.gameObject.SetActive(true);
        life.SetActive(false);

        GameManager.UnregisterPlayer(transform.name);
    }
}