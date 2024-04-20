using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
public class Scroll : PlayerItem
{
    [SerializeField]
    GameObject scrollPrefab;

    [SerializeField]
    Sprite[] scrollSprites;

    public Scroll(Vector2 pos, int type)
    {
        GameObject scrollObject = Instantiate(scrollPrefab, pos, Quaternion.Euler(0, 0, 0));
        switch (type)
        {
            case 1:
                renderer.sprite = scrollSprites[0];
                break;
            case 2:
                renderer.sprite = scrollSprites[1];
                break;
            case 3:
                renderer.sprite = scrollSprites[2];
                break;
            case 4:
                renderer.sprite = scrollSprites[3];
                break;
            case 5:
                renderer.sprite = scrollSprites[4];
                break;
            case 6:
                renderer.sprite = scrollSprites[5];
                break;
            case 7:
                renderer.sprite = scrollSprites[6];
                break;
            default:
                Debug.Log("Invalid scroll type");
                break;
        }
    }

    private void Start()
    {

    }
}