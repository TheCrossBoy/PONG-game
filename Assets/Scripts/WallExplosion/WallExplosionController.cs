using UnityEngine;

public class WallExplosionController : MonoBehaviour
{
    // private SpriteRenderer _spriteRenderer;

    // public Sprite NoCracksSprite;
    // public Sprite LightCracksSprite;
    // public Sprite HeavyCracksSprite;

    public GameObject BrokenWallSprite;

    private GameObject _regularWall;
    private GameObject _lightlyCrackedWall;
    private GameObject _heavilyCrackedWall;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // _spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        _regularWall = transform.GetChild(0).gameObject;
        _lightlyCrackedWall = transform.GetChild(1).gameObject;
        _lightlyCrackedWall.SetActive(false);
        _heavilyCrackedWall = transform.GetChild(2).gameObject;
        _heavilyCrackedWall.SetActive(false);
        GameManager.i.Scored += HandlePlayerScored;
    }

    void HandlePlayerScored(bool isPlayerScore)
    {
        if (!isPlayerScore) return;
        switch (GameManager.i.Score.x)
        {
            case 1:
                ChangeToLightlyCrackedWall();
                break;
            case 2:
                ChangeToHeavilyCrackedSprite();
                break;
            case 3:
                DoExplosion();
                break;
            default:
                Debug.LogError("Nonsensical player score: " + GameManager.i.Score.x);
                break;
        }
    }

    void ChangeToLightlyCrackedWall()
    {
        Debug.Log("Changing to lightly cracked sprite");
        Destroy(_regularWall);
        _lightlyCrackedWall.SetActive(true);
    }

    void ChangeToHeavilyCrackedSprite()
    {
        Debug.Log("Changing to heavily cracked sprite");
        Destroy(_lightlyCrackedWall);
        _heavilyCrackedWall.SetActive(true);
    }

    void DoExplosion()
    {
        Debug.Log("Doing explosion effect");
        Destroy(_heavilyCrackedWall);
        Instantiate(BrokenWallSprite, transform);
        GameManager.i.Scored -= HandlePlayerScored;
    }
}
