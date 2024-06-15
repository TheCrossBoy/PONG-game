using UnityEngine;

public class WallExplosionController : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;

    public Sprite NoCracksSprite;
    public Sprite LightCracksSprite;
    public Sprite HeavyCracksSprite;

    public GameObject BrokenWallSprite;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        GameManager.i.Scored += HandlePlayerScored;
    }

    // Update is called once per frame
    void Update()
    {
        
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

    void ChangeToNoCracksSprite()
    {
        _spriteRenderer.sprite = NoCracksSprite;
    }

    void ChangeToLightlyCrackedWall()
    {
        Debug.Log("Changing to lightly cracked sprite");
        _spriteRenderer.sprite = LightCracksSprite;
    }

    void ChangeToHeavilyCrackedSprite()
    {
        Debug.Log("Changing to heavily cracked sprite");
        _spriteRenderer.sprite = HeavyCracksSprite;
    }

    void DoExplosion()
    {
        Debug.Log("Doing explosion effect");
        Destroy(transform.GetChild(0).gameObject);
        Instantiate(BrokenWallSprite, transform);
        GameManager.i.Scored -= HandlePlayerScored;
    }
}
