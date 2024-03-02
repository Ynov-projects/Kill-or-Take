using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private const string playerIdPrefix = "Player";

    private static Dictionary<string, Player> players = new Dictionary<string, Player>();

    public static void RegisterPlayer(string netId, Player player)
    {
        string playerId = playerIdPrefix + netId;
        players.Add(playerId, player);
        player.transform.name = playerId;
    }

    public static void UnregisterPlayer(string netId)
    {
        players.Remove(netId);
    }

    public static Player GetPlayer(string playerId)
    {
        if (players.ContainsKey(playerId))
            return players[playerId];
        else
            return null;
    }
}
