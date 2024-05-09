using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;
using static Unity.Collections.AllocatorManager;

public class iconScript : MonoBehaviour
{
    private Image image;
    private Sprite originSprite;
    [SerializeField] private Sprite swapToSprite;
    void Start()
    {
        image = GetComponent<Image>();
        originSprite = image.sprite;
    }

    public void swapSprites()
    {
        if(image.sprite == originSprite)
        {
            image.sprite = swapToSprite;
        }
        else
        {
            image.sprite = originSprite;
        }
    }
}