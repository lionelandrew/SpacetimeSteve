using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class GameManager : MonoBehaviour {

    public enum GameState
    {
        started,
        running,
        finished
    }

    public static ItemContainer equipmentContainer;
    public static GameObject player;

    public ShooterGameCamera gameCamera;
    public GameObject playerPrefab;
    public ItemContainer gameEquipmentContainer;
    public WaveManager waveManager;
    public GameObject playerStartWeapon;

    public Transform aimTarget;

    public GameObject gameOverPanel;
    public GameObject restartGameButton;
    public GameObject exitGameButton;

    public static GameState gameState = GameState.started;

	void Start () {
        equipmentContainer = gameEquipmentContainer;

        UIEventListener.Get(restartGameButton).onClick += RestartGame;
        UIEventListener.Get(exitGameButton).onClick += ExitGame;

        SocialPlayUserLogin.OnUserAuthEvent += OnUserLogin;
	}

    void OnUserLogin(string userGuid)
    {
        StartNewGame();
    }

    public void StartNewGame()
    {
        gameOverPanel.transform.localPosition = new Vector3(1000, 0, 0);

        PlayerScore.ResetScore();
        gameState = GameState.started;
        SpawnAndInitializePlayer();
        GameObject.Instantiate(playerStartWeapon, new Vector3(0, 0, 2), transform.rotation);
        gameCamera.Initialize(player.transform);
        WaveManager.waveNumber = 0;
        waveManager.SpawnWave();

        gameState = GameState.running;
    }

    void SpawnAndInitializePlayer()
    {
        player = (GameObject)GameObject.Instantiate(playerPrefab, Vector3.zero, transform.rotation);
        player.name = "Player";

        //CharacterWeaponManager weaponManager = player.GetComponent<CharacterWeaponManager>();
        //weaponManager.ClearWeapons();
        equipmentContainer.Clear();

        PlayerController playerController = player.GetComponent<PlayerController>();
        playerController.aimTarget = aimTarget;

        gameCamera.player = player.transform;
    }

    public void EndGame()
    {

        gameState = GameState.finished;
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }

        string[] newParams = new string[2];
        newParams[0] = "Score";
        newParams[1] = PlayerScore.GetPlayerScore().ToString();

        CallBackBrowserHook.CreateExternalCall(OnPostScore, "LinkCallback", "PlatformStatsHandler", newParams);
        gameOverPanel.transform.localPosition = new Vector3(0, 0, 0);

    }

    void OnPostScore(string callbackMsg)
    {
        Debug.Log("Score Posted!");
    }

    void RestartGame(GameObject restartButton)
    {
        StartNewGame();
    }

    void ExitGame(GameObject exitButton)
    {
        Application.LoadLevel("MainMenu");
    }

    void Update()
    {
        if (gameState == GameState.finished)
            Screen.lockCursor = false;
        else
            Screen.lockCursor = true;
       
    }
}
