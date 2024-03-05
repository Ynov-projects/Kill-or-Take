using Mirror;
using UnityEngine;

public class CatchBall : MonoBehaviour
{
    private Vector3 initial;

    private void Awake()
    {
        initial = transform.localPosition;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.layer != LayerMask.NameToLayer("LocalPlayer"))
        {
            gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            transform.localPosition = initial;
            gameObject.SetActive(false);
        }
    }
}
