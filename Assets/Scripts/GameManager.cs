using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int level;
    public int baseSeed;

    private int _prevPlayerHealth;
    private int _prevPlayerCoins;

    private Player _player;

    public static GameManager Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        level = 1;
        baseSeed = PlayerPrefs.GetInt("Seed");
        Random.InitState(baseSeed);
        Generation.Instance.Generate();

        UIManager.Instance.UpdateLevelText(level);

        _player = FindObjectOfType<Player>();

        SceneManager.sceneLoaded += OnSceneLoad;
    }

    public void GoToNextLevel()
    {
        _prevPlayerCoins = _player.coins;
        _prevPlayerHealth = _player.curHp;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != "Game")
        {
            Destroy(gameObject);
            return;
        }

        _player = FindObjectOfType<Player>();
        level++;
        baseSeed++;

        Generation.Instance.Generate();

        _player.curHp = _prevPlayerHealth;
        _player.coins = _prevPlayerCoins;

        UIManager.Instance.UpdateCoinText(_prevPlayerCoins);
        UIManager.Instance.UpdateHealth(_prevPlayerHealth);
        UIManager.Instance.UpdateLevelText(level);
    }
}
