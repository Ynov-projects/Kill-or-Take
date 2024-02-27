using UnityEngine;

public class Button : MonoBehaviour
{
    private void OnMouseDown()
    {
        RealmController.Instance.IncreaseScore(name);
    }
}
