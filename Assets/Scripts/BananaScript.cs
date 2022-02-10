using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BananaScript : MonoBehaviour
{
    [SerializeField] private int SCORE;
    [SerializeField] private LayerMask CHARACTER_LAYER;
    [SerializeField] private string PLAYER_TAG;
    [SerializeField] private string PLAYER_OBJECT_NAME;

    private CircleCollider2D circleCollider2d;
    private PlayerCharacter player;
    private void Awake()
    {
        circleCollider2d = GetComponent<CircleCollider2D>();
        player = GameObject.Find(PLAYER_OBJECT_NAME).GetComponent<PlayerCharacter>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (circleCollider2d.IsTouchingLayers(CHARACTER_LAYER) && collision.CompareTag(PLAYER_TAG))
        {
            AudioPlayer.instance.PlayGetBananaSound();
            player.AddScore(SCORE);
            Destroy(gameObject);
        }
    }
}
