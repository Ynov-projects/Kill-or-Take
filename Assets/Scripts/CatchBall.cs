using UnityEngine;

public class CatchBall : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "RemotePlayer")
        {
        }
        gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        transform.localPosition = Vector3.zero;
        gameObject.SetActive(false);
    }
}
