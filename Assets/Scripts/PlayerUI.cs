using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private PlayerCharacter player;
    [SerializeField] private Text hitPointTextBox;
    [SerializeField] private Text scoreTextBox;
    [SerializeField] private Text ammoTextBox;
    [SerializeField] private GameObject panel;

    private bool isPanelNeededActive;

    private string HitpointText
    {
        get=> $"Hit Point: {player.GetHitPoint()}";
    }

    private string AmmoText
    {
        get => $"Orange left: {player.GetAmmo()}";
    }

    private string ScoreText
    {
        get => $"Score: {player.GetScore()}";
    }

    private void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        hitPointTextBox.text = HitpointText;
        scoreTextBox.text = ScoreText;
        ammoTextBox.text = AmmoText;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _TogglePanel();
        }
        hitPointTextBox.text = HitpointText;
        scoreTextBox.text = ScoreText;
        ammoTextBox.text = AmmoText;

    }

    private void _TogglePanel()
    {
        panel.SetActive(!panel.activeSelf);
        Time.timeScale = panel.activeSelf ? 0 : 1;
    }
}
