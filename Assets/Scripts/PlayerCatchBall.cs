using Mirror;
using UnityEngine;

public class PlayerCatchBall : NetworkBehaviour
{
    [SerializeField] private GameObject ball;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            SendBall();
        }
    }

    [Command]
    private void SendBall()
    {
        RpcBall();
    }

    [ClientRpc]
    private void RpcBall()
    {
        ball.SetActive(true);
        ball.GetComponent<Rigidbody>().AddForce(transform.forward * 1000);
    }
}
