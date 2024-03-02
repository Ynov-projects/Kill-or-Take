using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI score;

    [SerializeField] private Image _Image;
    [SerializeField] private RectTransform _Background;
    [SerializeField] private Gradient _Gradient;

    private Player player;

    public void SetScore(int ext_score, int ext_highscore)
    {
        score.text = "Score: " + ext_score + "\n HighScore: " + ext_highscore;
    }

    public void SetPlayer(Player _player)
    {
        player = _player;
    }

    private void Update()
    {
        UpdateLife(player.GetHealth());
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
}
