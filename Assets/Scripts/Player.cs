using Mirror;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Player : NetworkBehaviour
{
    [SerializeField] private int maxHealth = 100;

    [SyncVar] public int health;

    [SerializeField] private Image _Image;
    [SerializeField] private RectTransform _Background;
    [SerializeField] private Gradient _Gradient;


    private void Awake()
    {
        SetDefault();
        UpdateLife();
    }

    private void UpdateLife()
    {
        // If less than 0 = 0 / else if > maxlife = maxlife / else life - amount
        Vector3 CurrentScale = _Background.localScale;
        CurrentScale.x = (float)health / (float)maxHealth;
        _Background.localScale = CurrentScale;

        _Image.color = _Gradient.Evaluate(CurrentScale.x);

        //if (life <= 0) GameManager.Instance.Death();
    }


    public void SetDefault()
    {
        health = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        UpdateLife();
    }
}
