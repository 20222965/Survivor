using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public enum GameState { Lobby, Playing, End }

    public static GameManager Instance { get; private set; }
    public GameState State { get; set; }

    [Header("Time Control")]
    [field: SerializeField] public bool IsPause { get; set; } = true;
    [field: SerializeField] public float GameTime { get; set; }
    [field: SerializeField] public float EndTime { get; set; } = 180f;


    [field:Header("Game Object")]
    [field: SerializeField] public Transform UIJoy { get; set; }
    [field: SerializeField] public Player Player { get; set; }
    [field:SerializeField] public LevelUp UILevelUp { get; set; }
    [field: SerializeField] public Result UIResult { get; set; }

    void Awake()
    {
        Instance = this;
        Application.targetFrameRate = 60;
        if (Player == null) Player = FindAnyObjectByType<Player>(FindObjectsInactive.Include);
        if (UILevelUp == null) UILevelUp = FindAnyObjectByType<LevelUp>();
        if (UIResult == null) UIResult = FindAnyObjectByType<Result>(FindObjectsInactive.Include);

    }

    private void OnEnable()
    {
        State = GameState.Lobby;
        // 첫 화면 bgm 재생
        StartCoroutine(LobbyBGM());
    }

    IEnumerator LobbyBGM()
    {
        // 게임 시작 후 0.5초 뒤 로비 화면일 경우 bgm 재생
        yield return new WaitForSecondsRealtime(0.5f);
        if(State == GameState.Lobby)
            AudioManager.Instance.PlayBGM(AudioManager.BGM.Lobby);
    }

    void Update()
    {
        if (IsPause) return;

        if (State == GameState.Playing)
        {
            GameTime += Time.deltaTime;
            if (GameTime > EndTime)
            {
                GameTime = EndTime;
                GameVictory();
            }
        }
    }

    public void GameStart(int id)
    {
        State = GameState.Playing;

        Player.PlayerID = id;
        this.Player.gameObject.SetActive(true);

        UILevelUp.Select(id % (int)WeaponData.WeaponItem.Count);
        Resume();

        AudioManager.Instance.PlayBGM(AudioManager.BGM.Game, true);
        AudioManager.Instance.PlaySfx(AudioManager.Sfx.Select);
    }

    public void GameOver()
    {
        StartCoroutine(GameOverRoutine());
    }

    IEnumerator GameOverRoutine()
    {
        State = GameState.End;
        IsPause = true;
        yield return new WaitForSeconds(0.5f);
        UIResult.gameObject.SetActive(true);
        UIResult.Lose();
        Stop();

        AudioManager.Instance.PlayBGM(AudioManager.BGM.Game, false);
        AudioManager.Instance.PlaySfx(AudioManager.Sfx.Lose);
    }

    public void GameVictory()
    {
        StartCoroutine(GameVictoryRoutine());
    }

    IEnumerator GameVictoryRoutine()
    {
        State = GameState.End;
        IsPause = true;
        yield return new WaitForSeconds(0.5f);
        UIResult.gameObject.SetActive(true);
        UIResult.Win();
        Stop();

        AudioManager.Instance.PlayBGM(AudioManager.BGM.Game, false);
        AudioManager.Instance.PlaySfx(AudioManager.Sfx.Win);
    }


    public void GameRetry()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GameQuit()
    {
        State = GameState.End;
        IsPause = true;
        Application.Quit();
    }

    public void LevelUp()
    {
        UILevelUp.Show();
    }

    public void Stop()
    {
        IsPause = true;
        Time.timeScale = 0;
        UIJoy.localScale = Vector3.zero;
    }
    public void Resume()
    {
        if (State == GameState.Playing)
        {
            IsPause = false;
            UIJoy.localScale = Vector3.one;
        }
        Time.timeScale = 1;
    }
}
