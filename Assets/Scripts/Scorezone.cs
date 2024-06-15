using UnityEngine;
using UnityEngine.Serialization;

public class Scorezone : MonoBehaviour
{
    [FormerlySerializedAs("isPlayerZone")] public bool isPlayerScorezone;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (GameManager.i.State == GameState.PlayingRegularPong && otherCollider.CompareTag("Scoreable"))
        {
            GameManager.i.TriggerScored(isPlayerScorezone);
        } else if (GameManager.i.State == GameState.FightingPONG)
        {
            GameManager.i.PlayerTakeDamage(otherCollider.gameObject);
        }
    }
}
