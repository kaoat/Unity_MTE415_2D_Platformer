using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapScript : MonoBehaviour
{
    [SerializeField] private Camera MAIN_CAMERA;
    [SerializeField] private Camera MINIMAP_CAMERA;

    private int mainDisplay;
    private int altDisplay;
    private bool isTabPressed;
    private void Awake()
    {
        mainDisplay = MAIN_CAMERA.targetDisplay;
        altDisplay = mainDisplay + 1;
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        isTabPressed = Input.GetKey(KeyCode.Tab);

        if (isTabPressed) _ShowMinimap();
        else _HideMinimap();
    }

    private void _ShowMinimap()
    {
        if (MINIMAP_CAMERA.targetDisplay != mainDisplay)
        {
            MINIMAP_CAMERA.targetDisplay = mainDisplay;
            MAIN_CAMERA.targetDisplay = altDisplay;
        }
    }
    private void _HideMinimap()
    {
        if (MAIN_CAMERA.targetDisplay != mainDisplay)
        {
            MAIN_CAMERA.targetDisplay = mainDisplay;
            MINIMAP_CAMERA.targetDisplay = altDisplay;
        }
    }
}
