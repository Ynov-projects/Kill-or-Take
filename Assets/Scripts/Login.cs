using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour
{
    [SerializeField] private TMP_InputField login;
    [SerializeField] private TMP_InputField password;

    public async void TryLogin()
    {
        if (await RealmManager.Instance.Login(login.text, password.text) != "")
        {
            SceneManager.LoadScene("Demo");
        }
    }
}
