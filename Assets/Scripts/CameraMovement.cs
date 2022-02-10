using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float xLeftBorder;
    [SerializeField] private float xRightBorder;
    [SerializeField] private float yTopBorder;
    [SerializeField] private float yBottomBorder;

    private Transform playerTransform;
    private Vector3 playerPosition;
    private Vector3 defaultCameraPosition;
    private Vector3 defaultPlayerPosition;
    private Vector3 positionDiff;

    private bool IsXBorder
    {
        get => defaultCameraPosition.x + positionDiff.x < xLeftBorder ||
            defaultCameraPosition.x + positionDiff.x > xRightBorder;
    }

    private bool IsYBorder
    {
        get => defaultCameraPosition.y + positionDiff.y < yBottomBorder ||
            defaultCameraPosition.y + positionDiff.y > yTopBorder;
    }

    private void Awake()
    {
        playerTransform = player.GetComponent<Transform>();
        defaultCameraPosition = gameObject.transform.position;
        defaultPlayerPosition = playerTransform.position;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void _MoveCamera()
    {
        positionDiff = playerTransform.position - defaultPlayerPosition;
        if (IsXBorder)
        {
            positionDiff.x= gameObject.transform.position.x-defaultCameraPosition.x;
        }
        if (IsYBorder)
        {
            positionDiff.y = gameObject.transform.position.y - defaultCameraPosition.y;
        }
        gameObject.transform.position = defaultCameraPosition + positionDiff;
    }

    // Update is called once per frame
    void Update()
    {
        _MoveCamera();
    }


}
