using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public static UIController Instance;

    [SerializeField] private TextMeshProUGUI score;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void SetScores(int ext_score)
    {
        score.text = ext_score.ToString();
    }
}
