using UnityEngine;
using Realms;
using Realms.Sync;
using Realms.Sync.Exceptions;
using System.Threading.Tasks;
using TMPro;

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

    void OnDisable()
    {
        if (_realm != null)
        {
            _realm.Dispose();
        }
    }

    public async Task<string> Login(string email, string password)
    {
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

    public void IncreaseScore(string name)
    {
        PlayerScore _playerProfile = GetPlayerProfile();
        if (_playerProfile != null)
        {
            int score = _playerProfile.Score;
            switch (name)
            {
                case "RedSquare":
                    score--;
                    break;
                case "GreenSquare":
                    score++;
                    break;
                case "WhiteSquare":
                    score=0;
                    break;
                default:
                    break;
            }
            _realm.Write(() => {
                _playerProfile.Score = score;
            });
            UIController.Instance.SetScores(_playerProfile.Score);
        }
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