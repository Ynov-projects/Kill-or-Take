using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour
{
    //[SerializeField] private TextMeshProUGUI login;
    [SerializeField] private TMP_InputField login;
    [SerializeField] private TMP_InputField password;
    //[SerializeField] private TextMeshProUGUI password;

    public async void TryLogin()
    {
        if (await RealmController.Instance.Login(login.text, password.text) != "")
        {
            SceneManager.LoadScene("SampleScene");
        }
    }
}
