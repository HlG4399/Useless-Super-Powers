using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Btn : MonoBehaviour
{ 
    public Sprite SourceImage, TransformImage;
    private Image BtnImage;

    void Start()
    {
        BtnImage = this.GetComponent<Image>();
    }

    void OnMouseEnter()
    {
        BtnImage.sprite = TransformImage;
    }

    void OnMouseExit()
    {
        BtnImage.sprite = SourceImage;
    }
}
