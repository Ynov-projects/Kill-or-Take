using UnityEngine;
using Mirror;
using Mirror.Examples.Basic;

public class PlayerSetup : NetworkBehaviour
{
    [SerializeField]
    private Behaviour[] componentsToDisable;

    [SerializeField] private string remoteLayerName = "RemotePlayer";

    Camera sceneCamera;

    [SerializeField]
    private GameObject UIPrefab;

    private GameObject UIPrefabInstance;

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
            }
            UIPrefabInstance = Instantiate(UIPrefab);
            UIController ui = UIPrefabInstance.GetComponent<UIController>();
            if (ui != null)
                ui.SetPlayer(GetComponent<Player>());
        }
    }

    public UIController GetUIController()
    {
        return UIPrefabInstance.GetComponent<UIController>();
    }

    public override void OnStartClient()
    {
        base.OnStartClient();
        GameManager.RegisterPlayer(GetComponent<NetworkIdentity>().netId.ToString(), GetComponent<Player>());
    }

    public void DisplayScorePanel()
    {
        UIPrefabInstance.GetComponent<UIController>().SetScores();
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
        Destroy(UIPrefabInstance);
        if (sceneCamera != null)
            sceneCamera.gameObject.SetActive(true);

        GameManager.UnregisterPlayer(transform.name);
        GameManager.UnregisterLogged(transform.name);
    }
}