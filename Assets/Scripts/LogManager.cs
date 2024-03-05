using UnityEngine;

public class LogManager : MonoBehaviour
{
    // Pour conserver le user Id au log
    public string realmUserId;

    public bool alreadyChecked = false;

    public static LogManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
    }
}
