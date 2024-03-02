using UnityEngine;
using Realms;
using Realms.Sync;
using Realms.Sync.Exceptions;
using System.Threading.Tasks;
using TMPro;
using Unity.VisualScripting;

public class RealmController : MonoBehaviour
{

    public static RealmController Instance;

    public TextMeshProUGUI Score;

    public string RealmAppId = "kill-or-take-jgksm";

    private Realm _realm;
    private App _realmApp;
    private User _realmUser;

    void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
    }

    private void Start()
    {
        Login("", "");
    }

    void OnDisable()
    {
        if (_realm != null)
        {
            ResetScore();
            _realm.Dispose();
        }
    }

    public async Task<string> Login(string email, string password)
    {
        email = "romainfatali30@gmail.com";
        password = "Romain";
        if (email != "" && password != "")
        {
            _realmApp = App.Create(new AppConfiguration(RealmAppId)
            {
                MetadataPersistenceMode = MetadataPersistenceMode.NotEncrypted
            });
            try
            {
                if (_realmUser == null)
                {
                    _realmUser = await _realmApp.LogInAsync(Credentials.EmailPassword(email, password));
                    _realm = await Realm.GetInstanceAsync();
                }
                else
                {
                    _realm = Realm.GetInstance();
                }
            }
            catch (ClientResetException clientResetEx)
            {
                if (_realm != null)
                {
                    _realm.Dispose();
                }
                clientResetEx.InitiateClientReset();
            }
            return _realmUser.Id;
        }
        return "";
    }

    public PlayerScore GetPlayerProfile()
    {
        PlayerScore _playerProfile = _realm.Find<PlayerScore>(_realmUser.Id);
        if (_playerProfile == null)
        {
            _realm.Write(() => {
                _playerProfile = _realm.Add(new PlayerScore(_realmUser.Id));
            });
        }
        return _playerProfile;
    }

    public void IncreaseScore()
    {
        PlayerScore _playerProfile = GetPlayerProfile();
        if (_playerProfile != null)
        {
            int score = _playerProfile.Score;
            score++;
            _realm.Write(() => {
                _playerProfile.Score = score;
            });
        }
        GetComponent<PlayerSetup>().GetUIController().SetScore(_playerProfile.Score, _playerProfile.HighScore);
    }

    public void ResetScore()
    {
        PlayerScore _playerProfile = GetPlayerProfile();
        if (_playerProfile != null)
        {
            _realm.Write(() => {
                if (_playerProfile.Score > _playerProfile.HighScore)
                {
                    _playerProfile.HighScore = _playerProfile.Score;
                }
                _playerProfile.Score = 0;
            });
        }
    }
}