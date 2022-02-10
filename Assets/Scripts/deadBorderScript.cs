using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class deadBorderScript : MonoBehaviour
{
    [SerializeField] private LayerMask CHARACTER_LAYER;
    [SerializeField] private string PLAYER_TAG;
    [SerializeField] private int SECOND_TO_LOAD_SCENE;

    private BoxCollider2D boxCollider2d;

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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsTouchingCharacterLayer &&collision.CompareTag(PLAYER_TAG))
        {
            AudioPlayer.instance.PlayDeadSound();
            StartCoroutine(_LoadScene());
        }
    }

    private IEnumerator _LoadScene()
    {
        yield return new WaitForSeconds(SECOND_TO_LOAD_SCENE);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
