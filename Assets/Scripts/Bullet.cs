using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private LayerMask CHARACTER_LAYER;
    [SerializeField] private string ENEMY_TAG;
    [SerializeField] private string PLAYER_TAG;
    [SerializeField] private float TIME_TO_LIVE;
    [SerializeField] private int damage;
    [SerializeField] private float speed;
    [SerializeField] private LayerMask GROUND_LAYER;
    [SerializeField] private LayerMask WALL_LAYER;

    private GameObject player;
    private Rigidbody2D rigidbody2d;
    private float tempTimeToLive;
    private CircleCollider2D circleCollider2d;
    private Quaternion newQuaternion;
    private bool isOnLeftSide;
    private Vector3 newVelocity;

    private bool IsTouchingCharacterLayer
    {
        get => circleCollider2d.IsTouchingLayers(CHARACTER_LAYER);
    }

    private bool IsTouchingGroundLayer
    {
        get => circleCollider2d.IsTouchingLayers(GROUND_LAYER);
    }

    private bool IsTouchingWallLayer
    {
        get => circleCollider2d.IsTouchingLayers(WALL_LAYER);
    }

    private void Awake()
    {
        tempTimeToLive = 0;
        rigidbody2d = GetComponent<Rigidbody2D>();
        circleCollider2d = GetComponent<CircleCollider2D>();
        newQuaternion = transform.rotation;
        newVelocity = rigidbody2d.velocity;
        player = GameObject.FindGameObjectWithTag(PLAYER_TAG);

    }
    // Start is called before the first frame update
    void Start()
    {
        isOnLeftSide = player.transform.position.x > transform.position.x;
        if (isOnLeftSide) speed *= -1;
    }

    void _GoStraight()
    {
        newVelocity.x += speed;
        rigidbody2d.velocity = newVelocity;
    }

    // Update is called once per frame
    void Update()
    {
        _GoStraight();
        _AlwaysSpin();
        _DestroySelf();
    }

    private void _DestroySelf()
    {
        tempTimeToLive += Time.deltaTime;
        if (tempTimeToLive > TIME_TO_LIVE)
        {
            Destroy(gameObject);
        }
    }

    private void _AlwaysSpin()
    {
        newQuaternion.z = Random.Range(Vector2.negativeInfinity.x,Vector2.positiveInfinity.x);
        transform.rotation = newQuaternion;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsTouchingGroundLayer || IsTouchingWallLayer)
        {
            Destroy(gameObject);
        }
        if (IsTouchingCharacterLayer && collision.CompareTag(ENEMY_TAG))
        {
            EnemyCharacter enemy = collision.gameObject.GetComponent<EnemyCharacter>();
            enemy.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
