using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : PlayerItem
{
    string type;
    char storage;

    Potion(string type, char storage)
    {
        this.type = type;
        this.storage = storage;
    }

    public void Drink()
    {

    }

    public void Spawn()
    {

    }
}
