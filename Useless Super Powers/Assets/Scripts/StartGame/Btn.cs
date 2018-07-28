using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Btn : MonoBehaviour
{
    public Image BtnImage;
    public Sprite SourceImage, TransformImage;
    public Text volumeText;
    public PlayMusic Sound;
    public Scrollbar scrollbar;
    public Text creditsText;

    private static bool isSet=true;
    private static bool isShow = true;

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
    
    public void Options()
    {
        if (isSet)
        {
            scrollbar.gameObject.SetActive(true);
            scrollbar.value = Sound.sound.volume;
            volumeText.text= "当前音量:" + scrollbar.value.ToString("F2") + "/1.0";
        }
        else
            scrollbar.gameObject.SetActive(false);
        isSet = !isSet;
    }

    public void setVolume(float volume)
    {
        Sound.sound.volume = scrollbar.value;
        volumeText.text = "当前音量:" + scrollbar.value.ToString("F2") + "/1.0";
    }

    public void Credits()
    {
        if (isShow)
            creditsText.gameObject.SetActive(true);
        else
            creditsText.gameObject.SetActive(false);
        isShow = !isShow;
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
