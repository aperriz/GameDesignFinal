using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

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
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        switch (type)
        {
            case "Fire Storm":
                for(int i = 1; i < 4; i++)
                {
                    FireStorm(mousePos);
                }
                break;
            case "Shield":
                Shield();
                break;
            case "Holy Radiance":
                HolyRadiance(mousePos);
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

    private void FireStorm(Vector2 mousePos)
    {
        bool casted = false;
        int loopsWithoutCasting = 0;
        Vector2 pos = new Vector2(mousePos.x + Random.Range(-2f, 2f), mousePos.y + Random.Range(-2f, 2f));
        while (!casted)
        {
            if (GameObject.Find("TilemapVisualizer").GetComponent<TileMapVisualizer>().floorMap.HasTile(new Vector3Int((int)Mathf.Round(pos.x), (int)Mathf.Round(pos.y), 0)))
            {

                Instantiate(Resources.Load("Prefabs/Fire Storm") as GameObject, new Vector3(pos.x, pos.y, -10), Quaternion.identity);
                loopsWithoutCasting = 0;
                casted = true;
            }
            else if (loopsWithoutCasting >= 2)
            {
                Instantiate(Resources.Load("Prefabs/Fire Storm") as GameObject, new Vector3(pos.x, pos.y, -10), Quaternion.identity);
                casted = true;
            }
            else
            {
                pos = new Vector2(mousePos.x + Random.Range(-2f, 2f), mousePos.y + Random.Range(-2f, 2f));
                loopsWithoutCasting++;
                continue;
            }
        }
    }

    private void HolyRadiance(Vector2 mousePos)
    {   
        Instantiate(Resources.Load("Prefabs/Holy Radiance") as GameObject, new Vector3(mousePos.x, mousePos.y, -10), Quaternion.identity);   
    }

    private void Shield()
    {
        extraStats.ShieldCoroutine();
    }
}