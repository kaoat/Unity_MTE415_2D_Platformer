using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCharacter : MonoBehaviour,Character
{
    [SerializeField] private float JUMP_MULTIPLIER;
    [SerializeField] private float RUN_SLOW_MULTIPLIER;
    [SerializeField] private float RUN_FAST_MULTIPLIER;
    [SerializeField] private LayerMask GROUND_LAYER;
    [SerializeField] private float RAYCAST_DISTANCE;
    [SerializeField] private int JUMP_LIMIT;
    [SerializeField] private LayerMask CHARACTER_LAYER;
    [SerializeField] private string ENEMY_TAG;
    [SerializeField] private GameObject FEET;
    [SerializeField] private float IMMORTAL_TIME;
    [SerializeField] private int MAX_HIT_POINT;
    [SerializeField] private int MIN_HIT_POINT;
    [SerializeField] private Bullet[] BULLETS;
    [SerializeField] private GameObject FIRE_POINT;
    [SerializeField] private int ammo;
    [SerializeField] private int SECOND_TO_LOAD_SCENE;
    [SerializeField] private GameObject EYE;

    private CapsuleCollider2D capsuleCollider2d;
    private int hitPoint;
    private int score;
    public static PlayerCharacter instance;
    private Vector3 newScale;
    private Rigidbody2D rigidbody2d;
    private Vector2 newVelocity;
    private int jumpCounter;
    private float horizontalValue;
    private bool isJumpPressed;
    private Animator animator;
    private float tempImmortalTime;
    private SpriteRenderer renderer2d;
    private bool isShoot;
    private Vector3 DEFAULT_SCALE;
    private Vector2 size;

    private bool IsGroundNext
    {
        get => Physics2D.CapsuleCast(EYE.transform.position, size, CapsuleDirection2D.Vertical, 0f,
            EYE.transform.position.x >
            transform.position.x ? Vector2.right : Vector2.left,RAYCAST_DISTANCE,GROUND_LAYER);
    }

    public int GetHitPoint()
    {
        return hitPoint;
    }

    public void AddHitPoint(int hp)
    {
        if (hitPoint + hp > MAX_HIT_POINT)
        {
            hitPoint = MAX_HIT_POINT;
        }
        else if (hitPoint + hp < MIN_HIT_POINT)
        {
            hitPoint = MIN_HIT_POINT;
        }
        else
        {
            hitPoint += hp;
        }
    }

    private bool CanTakeDamage
    {
        get => tempImmortalTime == 0;
    }

    private bool IsFlipNeeded
    {
        get => Mathf.Abs(horizontalValue) > Mathf.Epsilon;
    }

    private float RunMultiplier
    {
        get => Input.GetKey(KeyCode.LeftShift) ? RUN_FAST_MULTIPLIER : RUN_SLOW_MULTIPLIER;
    }

    private bool IsOnGround
    {
        get => Physics2D.Raycast(FEET.transform.position, Vector2.down, RAYCAST_DISTANCE, GROUND_LAYER);
    }
    
    private bool IsRun
    {
        get => Mathf.Abs(horizontalValue) > Mathf.Epsilon;
    }

    private bool IsImmortal
    {
        get => rigidbody2d.bodyType == RigidbodyType2D.Static;
    }

    private bool IsJumpLimit
    {
        get => jumpCounter >= JUMP_LIMIT;
    }

    public void Flip()
    {
        if (IsFlipNeeded)
        {
            newScale.x = Mathf.Sign(horizontalValue)* DEFAULT_SCALE.x;
            transform.localScale = newScale;
        }
        
    }

    public void FlipTo(string side)
    {
        if (IsFlipNeeded)
        {
            side = side.ToUpper();

            if (side.Equals("LEFT"))
            {
                newScale.x = -1*DEFAULT_SCALE.x;
            }
            else if (side.Equals("RIGHT"))
            {
                newScale.x = 1*DEFAULT_SCALE.x;
            }
            transform.localScale = newScale;
        }
    }
    public void AttackTo(Character enemy,int damage)
    {
        enemy.TakeDamage(damage);
        newVelocity.y = JUMP_MULTIPLIER;
        rigidbody2d.velocity = newVelocity;
    }
    public void TakeDamage(int damage)
    {
        if (CanTakeDamage&& rigidbody2d.bodyType != RigidbodyType2D.Static)
        {
            AudioPlayer.instance.PlayGetDamageSound();
            _ChangeToImmortal();
            if (hitPoint - damage < 0)
            {
                hitPoint = 0;
            }
            else
            {
                hitPoint -= damage;
            }
            if (hitPoint <= 0)
            {
                _Died();
            }
        }
    }

    private IEnumerator _LoadScene()
    {
        yield return new WaitForSeconds(SECOND_TO_LOAD_SCENE);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void _Died()
    {
        AudioPlayer.instance.PlayDeadSound();
        StartCoroutine(_LoadScene());
    }

    public void AddScore(int addScore)
    {
        score += addScore;
    }
    public int GetScore()
    {
        return score;
    }

    private void _ChangeToImmortal()
    {
        rigidbody2d.bodyType = RigidbodyType2D.Static;
        tempImmortalTime += Time.deltaTime;
        capsuleCollider2d.isTrigger = true;

    }

    private void _Flash()
    {
        if (!CanTakeDamage)
        {
            renderer2d.enabled = !renderer2d.enabled;
        }
        else if (CanTakeDamage && !renderer2d.enabled)
        {
            renderer2d.enabled = true;
        }
    }

    private void _ChangeToMortal()
    {
        rigidbody2d.bodyType = RigidbodyType2D.Dynamic;
        tempImmortalTime = 0;
        capsuleCollider2d.isTrigger = false;
    }

    public void Run()
    {
        if (IsRun&& CanTakeDamage&& !IsGroundNext)
        {
            newVelocity.x = horizontalValue * RunMultiplier;
            newVelocity.y = rigidbody2d.velocity.y;
            rigidbody2d.velocity = newVelocity;
        }
        
    }

    public void Jump()
    {
        if (isJumpPressed && !IsJumpLimit&&CanTakeDamage)
        {
            AudioPlayer.instance.PlayJumpSound();
            newVelocity.y = JUMP_MULTIPLIER;
            newVelocity.x = rigidbody2d.velocity.x;
            jumpCounter++;
            rigidbody2d.velocity = newVelocity;
        }
        
    }

    private void Awake()
    {
        instance = this;
        newScale = transform.localScale;
        newVelocity = new Vector2(0, 0);
        rigidbody2d = GetComponent<Rigidbody2D>();
        DEFAULT_SCALE = transform.localScale;
        animator = GetComponent<Animator>();
        capsuleCollider2d = GetComponent<CapsuleCollider2D>();
        renderer2d = GetComponent<SpriteRenderer>();
        size = new Vector2(1, 1);
    }

    private void _ResetJumpCount()
    {
        if (IsOnGround && jumpCounter > 0)
        {
            jumpCounter = 0;
        }
    }

    private void _CountImmortalTime()
    {
        if (!CanTakeDamage) tempImmortalTime += Time.deltaTime;
    }

    private void _ResetImmortalTime()
    {
        if (tempImmortalTime >= IMMORTAL_TIME) tempImmortalTime = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        jumpCounter = 0;
        horizontalValue = 0;
        tempImmortalTime = 0;
        hitPoint = MAX_HIT_POINT;
    }

    private void _Shoot()
    {
        if (ammo>0)
        {
            if(!cheatMenu.instance.infiniteAmmoToggle.isOn)ammo--;
            AudioPlayer.instance.PlayAttackSound();
            Instantiate(BULLETS[0], FIRE_POINT.transform.position, Quaternion.identity);
            animator.SetBool("IsAttack", false);
        }
    }
    public int GetAmmo()
    {
        return ammo;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalValue= Input.GetAxis("Horizontal");
        isJumpPressed = Input.GetButtonDown("Jump");
        isShoot = Input.GetKeyDown(KeyCode.LeftControl);
        if(isShoot) animator.SetBool("IsAttack", true);
        _ResetJumpCount();
        _CountImmortalTime();
        _ResetImmortalTime();
        if (CanTakeDamage && IsImmortal) _ChangeToMortal();

        animator.SetBool("IsGround", IsOnGround);
        animator.SetBool("IsIdle", !IsRun);

        Flip();
        Run();
        Jump();
        _Flash();
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        bool isTouchingEnemyTag = col.tag.Equals(ENEMY_TAG);
        bool isTouchingCharacterLayer = col.IsTouchingLayers(CHARACTER_LAYER);
        if ( isTouchingEnemyTag && isTouchingCharacterLayer&&CanTakeDamage)
        {
            EnemyCharacter enemy = col.gameObject.GetComponent<EnemyCharacter>();
            AttackTo(enemy,50);
        }
    }
}
