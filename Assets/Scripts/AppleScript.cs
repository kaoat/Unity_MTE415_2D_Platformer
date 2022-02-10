using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleScript : MonoBehaviour
{
    [SerializeField] private string PLAYER_TAG;
    [SerializeField] private LayerMask CHARACTER_LAYER;
    [SerializeField] private int addedHp;
    [SerializeField] private string PLAYER_OBJECT_NAME;

    private PlayerCharacter player;

    private CircleCollider2D circleCollider2d;
    private void Awake()
    {
        circleCollider2d = GetComponent<CircleCollider2D>();
        player = GameObject.Find(PLAYER_OBJECT_NAME).GetComponent<PlayerCharacter>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (circleCollider2d.IsTouchingLayers(CHARACTER_LAYER)&&collision.CompareTag(PLAYER_TAG))
        {
            AudioPlayer.instance.PlayGetAppleSound();
            player.AddHitPoint(Random.Range(1,addedHp));
            Destroy(gameObject);
        }
    }
}
