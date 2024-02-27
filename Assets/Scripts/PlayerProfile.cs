using Realms;
using Realms.Sync;

public class PlayerScore : RealmObject
{

    [PrimaryKey]
    [MapTo("_id")]
    public string UserId { get; set; }

    [MapTo("high_score")]
    public int HighScore { get; set; }

    [MapTo("score")]
    public int Score { get; set; }
    public PlayerScore() { }

    public PlayerScore(string userId)
    {
        this.UserId = userId;
        this.HighScore = 0;
        this.Score = 0;
    }

}