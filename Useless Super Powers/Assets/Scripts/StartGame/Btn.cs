using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Btn : MonoBehaviour
{ 
    public Sprite SourceImage, TransformImage;
    private Image BtnImage;
    PlayMusic Sound;

    void Start()
    {
        BtnImage = this.GetComponent<Image>();
        Sound = GameObject.Find("Sound").GetComponent<PlayMusic>();
        DontDestroyOnLoad(Sound);
    }

    public void OnMouseEnter()
    {
        BtnImage.sprite = TransformImage;
    }

    public void OnMouseExit()
    {
        BtnImage.sprite = SourceImage;
    }

    public void StartGame()
    {
        Sound.playMusic();
        SceneManager.LoadScene(1);
    }

    public void setVolume(float volume)
    {
        Sound.sound.volume = volume;
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
