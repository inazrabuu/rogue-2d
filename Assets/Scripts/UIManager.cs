using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject[] hearts;
    public TextMeshProUGUI coinText;
    public GameObject keyIcon;
    public TextMeshProUGUI levelText;
    public RawImage map;

    public static UIManager Instance;
    private void Awake()
    {
        Instance = this;
    }

    public void UpdateHealth(int health)
    {
        for (int x = 0; x < hearts.Length; x++)
        {
            hearts[x].SetActive(x < health);
        }
    }

    public void UpdateCoinText(int coins)
    {
        coinText.text = coins.ToString();
    }

    public void ToggleKeyIcon(bool state)
    {
        keyIcon.SetActive(state);
    }

    public void UpdateLevelText(int level)
    {
        levelText.text = string.Format("Level {0}", level);
    }
}
