using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private Image _Image;
    [SerializeField] private RectTransform _Background;
    [SerializeField] private Gradient _Gradient;

    [SerializeField] private GameObject pnlScores;
    [SerializeField] private TextMeshProUGUI[] texts = new TextMeshProUGUI[5];

    public Player player;

    //public void SetScore(int ext_score)
    //{
    //    score.text = "Score: " + ext_score/* + "\n HighScore: " + ext_highscore*/;
    //}

    public void SetPlayer(Player _player)
    {
        player = _player;
    }

    private void Update()
    {
        UpdateLife(player.GetHealth());
        //SetScore(player.GetScore());
    }

    public void UpdateLife(float amount)
    {
        // If less than 0 = 0 / else if > maxlife = maxlife / else life - amount
        Vector3 CurrentScale = _Background.localScale;
        CurrentScale.x = amount;
        _Background.localScale = CurrentScale;

        _Image.color = _Gradient.Evaluate(CurrentScale.x);

        //if (life <= 0) GameManager.Instance.Death();
    }

    public void SetScores()
    {
        pnlScores.SetActive(!pnlScores.activeSelf);
        IQueryable<PlayerScore> players = RealmManager.Instance.SetScores();

        for (int i = 0; i < 5; i++)
        {
            if (players.ElementAt(i) != null)
            {
                texts[i].text = "Player : " + players.ElementAt(i).UserId + " a pour score " + players.ElementAt(i).Score + " et top score " + players.ElementAt(i).HighScore;
            }
        }
    }
}
