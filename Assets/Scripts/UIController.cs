using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    private GameObject _scoreCanvas;
    private GameObject _heartCanvas;
    private GameObject _menuCanvas;
    private GameObject _hitsCanvas;
    
    private TextMeshProUGUI _playerScore;
    private TextMeshProUGUI _cpuScore;

    private TextMeshProUGUI _playerHits;

    private float _timeToShow = 0.0f;
    private float _timeTillScoreChange = -1.0f;

    private bool _scoreChanged = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameManager.i.Scored += StartScoreSequence;
        
        _scoreCanvas = transform.GetChild(0).gameObject;
        _scoreCanvas.SetActive(false);
        _heartCanvas = transform.GetChild(1).gameObject;
        _heartCanvas.SetActive(false);
        _menuCanvas = transform.GetChild(2).gameObject;
        _hitsCanvas = transform.GetChild(3).gameObject;
        _hitsCanvas.SetActive(false);
        
        _playerScore = _scoreCanvas.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        _cpuScore = _scoreCanvas.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        _timeToShow = 3;

        _playerHits = _hitsCanvas.transform.GetChild(0).GetComponent<TextMeshProUGUI>();

        GameManager.i.StateChanged += HandleStateChanged;
        GameManager.i.PlayerHit += PlayerHitHandler;
    }

    // Update is called once per frame
    void Update()
    {
        if (_playerScore.gameObject.activeSelf)
        {
            if (!_scoreChanged)
            {
                _timeTillScoreChange -= Time.deltaTime;
                if (_timeTillScoreChange < 0)
                {
                    _playerScore.text = GameManager.i.Score.x.ToString();
                    _cpuScore.text = GameManager.i.Score.y.ToString();
                    _scoreChanged = true;
                }
            }
            else
            {
                _timeToShow -= Time.deltaTime;
                if (_timeToShow < 0)
                {
                    _scoreCanvas.SetActive(false);
                }
            }
        }
    }

    void PlayerHitHandler(int totalHits)
    {
        _playerHits.text = "Hits taken: " + totalHits;
    }

    void StartScoreSequence(bool _)
    {
        _timeTillScoreChange = 1;
        _timeToShow = 3;
        _scoreCanvas.SetActive(true);
        _scoreChanged = false;
    }

    void ShowPlayerHealth()
    {
        Debug.Log("Now showing player health");
    }

    void HandleStateChanged(GameState state)
    {
        if (state == GameState.PlayingRegularPong)
        {
            _menuCanvas.SetActive(false);
            StartScoreSequence(false);
        } else if (state == GameState.FightingPONG)
        {
            _hitsCanvas.SetActive(true);
        }else if (state == GameState.BeatPONG)
        {
            // _hitsCanvas.SetActive(false);
            _heartCanvas.SetActive(true);
        }
    }
}
