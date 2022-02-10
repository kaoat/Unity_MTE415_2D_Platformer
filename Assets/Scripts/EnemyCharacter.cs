using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharacter : MonoBehaviour,Character
{
    [SerializeField] private LayerMask GROUND_LAYER;
    [SerializeField] private LayerMask CHARACTER_LAYER;
    [SerializeField] private string PLAYER_TAG;
    [SerializeField] private int ATTACK_POWER;
    [SerializeField] private float MOVE_SPEED;

    private BoxCollider2D groundChecker;
    private Vector3 newScale;
    private bool isFlipNeeded;
    private Rigidbody2D rigid2d;

    private Vector3 DEFAULT_SCALE;
    private Vector2 newVelocity;

    private bool IsLeftSide
    {
        get => transform.localScale.x < 0;
    }

    public void Flip()
    {
        if (isFlipNeeded)
        {
            newScale.x *= -1;
            transform.localScale = newScale;
        }
    }
    public void FlipTo(string side)
    {
        side = side.ToUpper();

        if (side.Equals("RIGHT"))
        {

        } else if (side.Equals("LEFT"))
        {
            newScale.x = DEFAULT_SCALE.x*-1;
        }
        transform.localScale = newScale;
    }

    public void AttackTo(Character enemy,int damage)
    {
        enemy.TakeDamage(damage);
    }

    public void TakeDamage(int damage)
    {
        AudioPlayer.instance.PlayGetDamageSound();
        Destroy(gameObject);
    }
    public void Run()
    {
        if (IsLeftSide) newVelocity.x = -1 * MOVE_SPEED;
        else newVelocity.x = MOVE_SPEED;
        rigid2d.velocity = newVelocity;
    }
    public void Jump()
    {

    }

    public int GetHitPoint()
    {
        return 0;
    }

    public void AddHitPoint(int hp)
    {

    }

    private void Awake()
    {
        groundChecker = GetComponent<BoxCollider2D>();
        newScale = transform.localScale;
        DEFAULT_SCALE = transform.localScale;
        rigid2d = GetComponent<Rigidbody2D>();
        newVelocity = new Vector2(0,0);
    }

    // Start is called before the first frame update
    void Start()
    {
        isFlipNeeded = false;
    }

    // Update is called once per frame
    void Update()
    {
        isFlipNeeded = !groundChecker.IsTouchingLayers(GROUND_LAYER);
        
        Flip();
        Run();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        bool isTouchingPlayerTag = col.tag.Equals(PLAYER_TAG);
        bool isTouchingCharacterLayer = col.IsTouchingLayers(CHARACTER_LAYER);
        if (isTouchingPlayerTag && isTouchingCharacterLayer)
        {
            PlayerCharacter player = col.gameObject.GetComponent<PlayerCharacter>();
            AttackTo(player, ATTACK_POWER);
        }
    }

    public void AddScore(int score)
    {
        throw new System.NotImplementedException();
    }

    public int GetScore()
    {
        throw new System.NotImplementedException();
    }
}
