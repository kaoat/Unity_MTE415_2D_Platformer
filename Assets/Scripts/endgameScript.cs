using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class endgameScript : MonoBehaviour
{
    [SerializeField] private string PLAYER_TAG;
    [SerializeField] private LayerMask CHARACTER_LAYER;
    [SerializeField] private GameObject flag;
    [SerializeField] private float Y_LAST_POSITION;
    [SerializeField] private float SECOND_FOR_FLAG_UP;

    private BoxCollider2D boxCollider2d;
    private Vector3 newLocalFlagPosition;
    private bool isGameEnd;
    private float yPerSecond;
    private float yFirstPosition;

    private bool IsTouchingCharacterLayer
    {
        get => boxCollider2d.IsTouchingLayers(CHARACTER_LAYER);
    }
    private void Awake()
    {
        boxCollider2d = GetComponent<BoxCollider2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        newLocalFlagPosition = flag.transform.localPosition;
        isGameEnd = false;
        yFirstPosition = newLocalFlagPosition.y;
        yPerSecond = (Y_LAST_POSITION- yFirstPosition) /SECOND_FOR_FLAG_UP;
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameEnd)
        {
            _FlagUp();
        }
    }

    private void _FlagUp()
    {
        newLocalFlagPosition.y += yPerSecond * Time.deltaTime;
        flag.transform.localPosition = newLocalFlagPosition;

        if (flag.transform.localPosition.y >= Y_LAST_POSITION)
        {
            isGameEnd = false;
        }
    }

    private void _EndGame()
    {
        AudioPlayer.instance.PlayEndGameSound();
        isGameEnd = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsTouchingCharacterLayer && collision.CompareTag(PLAYER_TAG))
        {
            _EndGame();
        }
    }
}
