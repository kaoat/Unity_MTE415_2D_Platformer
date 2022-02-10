using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trap : MonoBehaviour
{
    [SerializeField] private LayerMask GROUND_LAYER;
    [SerializeField] private LayerMask CHARACTER_LAYER;
    [SerializeField] private string PLAYER_TAG;
    [SerializeField] private float SPEED;
    [SerializeField] private string PLAYER_OBJECT_NAME;
    [SerializeField] private int DAMAGE;

    private CircleCollider2D circleCollider2d;
    private Rigidbody2D rigidbody2d;
    private Vector2 newVelocity;
    private PlayerCharacter player;

    private bool IsTouchingCharacterLayer
    {
        get => circleCollider2d.IsTouchingLayers(CHARACTER_LAYER);
    }

    private bool IsTouchingGroundLayer
    {
        get => circleCollider2d.IsTouchingLayers(GROUND_LAYER);
    }

    private void Awake()
    {
        circleCollider2d = GetComponent<CircleCollider2D>();
        player = GameObject.Find(PLAYER_OBJECT_NAME).GetComponent<PlayerCharacter>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        newVelocity = Vector2.down;
    }

    private void _Fall()
    {
        rigidbody2d.velocity = newVelocity * SPEED;
    }
    private void Update()
    {
        _Fall();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsTouchingGroundLayer) Destroy(gameObject);
        if (IsTouchingCharacterLayer && collision.CompareTag(PLAYER_TAG))
        {
            player.TakeDamage(DAMAGE);
            Destroy(gameObject);
        }
    }
}
