using UnityEngine;

public class CatchBall : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "RemotePlayer")
        {
            Debug.Log("coucou");
        }
        gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        transform.localPosition = Vector3.zero;
        gameObject.SetActive(false);
    }
}
