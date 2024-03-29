using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private const string playerIdPrefix = "Player";

    private static Dictionary<string, Player> players = new Dictionary<string, Player>();
    public static Dictionary<string, string> logged = new Dictionary<string, string>();

    public static GameManager Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
    }

    public static void RegisterPlayer(string netId, Player player)
    {
        string playerId = playerIdPrefix + netId;
        if(!players.ContainsKey(playerId))
            players.Add(playerId, player);
    
        player.transform.name = playerId;
        
        if (player.realmUserId == "" && !LogManager.Instance.alreadyChecked)
        {
            player.realmUserId = LogManager.Instance.realmUserId;
            LogManager.Instance.alreadyChecked = true;
        }
        if(!logged.ContainsKey(playerId))
            logged.Add(playerId, player.realmUserId);
    }

    public static void UnregisterPlayer(string netId)
    {
        players.Remove(netId);
    }

    public static void UnregisterLogged(string netId)
    {
        logged.Remove(netId);
    }

    public static Player GetPlayer(string playerId)
    {
        if (players.ContainsKey(playerId))
            return players[playerId];
        else
            return null;
    }

    public static string GetLogged(string playerId)
    {
        if (logged.ContainsKey(playerId))
            return logged[playerId];
        else
            return null;
    }
}
