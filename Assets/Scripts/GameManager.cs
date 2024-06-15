using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public enum GameState
{
    PregameMenu,
    PlayingRegularPong,
    PONGTransition,
    FightingPONG,
    BeatPONG,
    Dead
}

public class GameManager : MonoBehaviour
{
    public Vector2Int Score;
    public GameState State;
    public int PlayerHits = 0;
    
    public delegate void ScoredEventHandler(bool isPlayerScore);
    public event ScoredEventHandler Scored;

    public delegate void StateChangedHandler(GameState newState);
    public event StateChangedHandler StateChanged;

    public delegate void PlayerHitHandler(int totalHits);
    public event PlayerHitHandler PlayerHit;

    public GameObject BallPrefab;
    
    private GameObject _rightWall;
    private GameObject _ball;

    private InputAction _clickAction;

    public void Start()
    { 
        Score = new Vector2Int(0, 0);
        State = GameState.PregameMenu;
        _rightWall = GameObject.Find("Right Wall");
        Scored += SpawnBall;
        _clickAction = InputSystem.actions.FindAction("Click");
    }

    public void Update()
    {
        if (State == GameState.PregameMenu || State == GameState.Dead)
        {
            if (_clickAction.triggered)
            {
                StartGame();
            }
        }
    }

    public void ResetGame()
    {
        Scored = null;
        StateChanged = null;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void PlayerTakeDamage(GameObject ball)
    {
        PlayerHits++;
        PlayerHit?.Invoke(PlayerHits);
        Destroy(ball);
    }

    public void StartGame()
    {
        State = GameState.PlayingRegularPong;
        TriggerStateChanged();
        SpawnBall();
    }
    

    public void TransitionToPONGFight()
    {
        State = GameState.PONGTransition;
        Scored -= SpawnBall;
        if (_ball != null) Destroy(_ball);
        TriggerStateChanged();
    }

    public void StartPongFight()
    {
        State = GameState.FightingPONG;
        TriggerStateChanged();
    }

    public void PONGKilled()
    {
        State = GameState.BeatPONG;
        TriggerStateChanged();
    }
    
    public void TriggerScored(bool isPlayerScore)
    {
        Debug.Log((isPlayerScore ? "Player" : "CPU") + " scored");
        Score += isPlayerScore ? new Vector2Int(1, 0) : new Vector2Int(0, 1);
        
        if (Score.x == 3)
        {
            TransitionToPONGFight();
        }
        
        Scored?.Invoke(isPlayerScore);
    }

    public void TriggerStateChanged()
    {
        Debug.Log("Game state is now: " + State);
        StateChanged?.Invoke(State);
    }

    public void SpawnBall(bool _ = false)
    {
        if (_ball != null)
        {
            Destroy(_ball);
        }

        _ball = Instantiate(BallPrefab);
    }
    
    #region Singleton
    public static GameManager i;

    void Awake()
    {
        if (i == null)
        {
            i = this;
        } else if (i != this)
        {
            Destroy(this);
        }
        
        DontDestroyOnLoad(this);
    }
    #endregion
}
