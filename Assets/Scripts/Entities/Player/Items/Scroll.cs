using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.U2D;
public class Scroll : ScriptableObject
{
    public string type;
    public Sprite sprite;
    private PlayerExtraStats extraStats;
    private PlayerRecieveDamage playerDamage;
    private AgentMover playerMovement;

    public void Awake()
    {
        GameObject player = GameObject.Find("Player");
        extraStats = player.GetComponent<PlayerExtraStats>();
        playerMovement = player.GetComponent<AgentMover>();
        playerDamage = player.GetComponent<PlayerRecieveDamage>();
    }

    public void Cast()
    {
        switch (type)
        {
            case "Fire Storm":

                break;
            case "Shield":

                break;
            case "Holy Radiance":
                
                break;
            default:
                Debug.LogError("Invalid Scroll Type");
                break;
        }
        extraStats.UpdateScrolls();

    }

    public Scroll Clone()
    {
        Scroll clone = ScriptableObject.CreateInstance<Scroll>() as Scroll;
        clone.type = type;
        clone.sprite = sprite;
        return clone;
    }

    public Scroll(string type, Sprite sprite)
    {
        this.type = type;
        this.sprite = sprite;
    }
}