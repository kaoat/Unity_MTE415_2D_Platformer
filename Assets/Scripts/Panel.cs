using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Panel : MonoBehaviour
{
    [SerializeField] private Button restartButton;
    [SerializeField] private Slider soundSlider;
    // Start is called before the first frame update
    void Start()
    {
        soundSlider.value = AudioPlayer.instance.GetVolume();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ChangeSoundVolume()
    {
        AudioPlayer.instance.SetVolume(soundSlider.value);
    }
    
}
