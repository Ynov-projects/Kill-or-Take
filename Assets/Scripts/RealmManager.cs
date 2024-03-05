using Realms.Sync.Exceptions;
using Realms.Sync;
using Realms;
using System.Threading.Tasks;
using UnityEngine;
using System.Linq;
using UnityEngine.SocialPlatforms.Impl;
using System.Collections.Generic;

public class RealmManager : MonoBehaviour
{
    public string RealmAppId = "kill-or-take-jgksm";
    public static RealmManager Instance;

    public Realm _realm;
    private App _realmApp;

    void OnDisable()
    {
        if (_realm != null)
        {
            _realm.Dispose();
        }
    }

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
        Spawn();
    }

    public IQueryable<PlayerScore> SetScores()
    {
        string[] players = new string[100];
        IQueryable<PlayerScore> _playerProfiles = _realm.All<PlayerScore>().OrderByDescending(p => p.HighScore).OrderByDescending(p => p.Score);
        foreach (PlayerScore playerProfile in _playerProfiles)
        {
            players.Append(playerProfile.UserId);
        }
        return _playerProfiles;
    }

    private async void Spawn()
    {
        _realmApp = App.Create(new AppConfiguration(RealmAppId)
        {
            MetadataPersistenceMode = MetadataPersistenceMode.NotEncrypted
        });
        try
        {
            _realm = await Realm.GetInstanceAsync();
        }
        catch (ClientResetException clientResetEx)
        {
            if (_realm != null)
            {
                _realm.Dispose();
            }
            clientResetEx.InitiateClientReset();
        }
    }

    public async Task<string> Login(string email, string password)
    {
        if (email != "" && password != "")
        { 
            User _realmUser = null;
            _realmUser = await _realmApp.LogInAsync(Credentials.EmailPassword(email, password));
            LogManager.Instance.realmUserId = _realmUser.Id;
            return _realmUser.Id;
        }
        return "";
    }

    public PlayerScore GetPlayerProfile(string playerId)
    {
        PlayerScore _playerProfile = _realm.Find<PlayerScore>(playerId);
        if (_playerProfile == null && playerId.Trim() != "")
        {
            _realm.Write(() =>
            {
                _playerProfile = _realm.Add(new PlayerScore(playerId));
            });
        }
        return _playerProfile;
    }

    public void IncreaseScore(string playerId)
    {
        PlayerScore _playerProfile = GetPlayerProfile(playerId);
        if (_playerProfile != null)
        {
            int score = _playerProfile.Score;
            score++;
            _realm.Write(() =>
            {
                _playerProfile.Score = score;
            });
        }
    }

    public void SetHighScore(string playerId)
    {
        PlayerScore _playerProfile = GetPlayerProfile(playerId);
        if (_playerProfile != null)
        {
            _realm.Write(() =>
            {
                if (_playerProfile.Score > _playerProfile.HighScore)
                {
                    _playerProfile.HighScore = _playerProfile.Score;
                }
                _playerProfile.Score = 0;
            });
        }
    }
}
