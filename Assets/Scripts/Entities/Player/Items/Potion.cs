using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Potion : PlayerItem
{
    public string type;

    public Potion(Vector2 pos, string type)
    {
        this.type = type;
        Instantiate(Resources.Load("Prefabs/World/Potion Prefab") as GameObject, pos, Quaternion.Euler(0, 0, 0));
    }

}
