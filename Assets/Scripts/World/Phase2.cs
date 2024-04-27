using Pathfinding.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class Phase2 : MonoBehaviour
{
    int loops = 0;
    [SerializeField]
    GameObject attack1Prefab, attack2Prefab, fireball;
    GameObject player;
    [SerializeField]
    Animator animator;
    [SerializeField]
    TileMapVisualizer tileMapVisualizer;
    int attack2ct = 0;
    [SerializeField]
    int moveItterations = 300, wallPadding = 5;

    [SerializeField]
    public float atk1cd, atk2cd, atk3cd, atk4cd, initcd;

    bool canMove = true;
    private void OnEnable()
    {
        player = GameObject.Find("Player");
        StartCoroutine(FirstAtkCD());
    }

    IEnumerator FirstAtkCD()
    {
        yield return new WaitForSeconds(initcd);
        StartCoroutine(Attack1CD());

    }

    private IEnumerator Attack1CD()
    {
        yield return new WaitForSeconds(atk1cd);
        loops++;
        if (loops % 10 == 0)
        {
            StartCoroutine(Attack4CD());
        }
        else if(loops % 7 == 0)
        {
            
            StartCoroutine(Attack3CD());

        }
        else if (loops % 3 != 0)
        {
            //Debug.Log("Fireball");
            for (int i = 0; i < Random.Range(1, 4); i++)
            {
                bool placed = false;
                int loopsWithoutPlacing = 0;

                while (!placed)
                {
                    /*
                     * -+|++
                     * _____
                     * --|+-
                     */

                    Vector3 playerPos = player.transform.position;
                    Vector3Int pos= new Vector3Int();

                    if(playerPos.x < 0 && playerPos.y < 0)
                    {
                       pos = new Vector3Int(Random.Range(-16, 0), Random.Range(-16, 0), -6);
                    }
                    else if (playerPos.x > 0 && playerPos.y < 0)
                    {
                        pos = new Vector3Int(Random.Range(0, 17), Random.Range(-16, 0), -6);
                    }
                    else if (playerPos.x > 0 && playerPos.y > 0)
                    {
                        pos = new Vector3Int(Random.Range(0, 17), Random.Range(0, 17), -6);
                    }
                    else if (playerPos.x < 0 && playerPos.y > 0)
                    {
                        pos = new Vector3Int(Random.Range(-16, 0), Random.Range(0, 17), -6);
                    }
                    else
                    {
                        pos = new Vector3Int(Random.Range(-8, 9), Random.Range(-8, 9), -6);
                    }

                    if (!tileMapVisualizer.floorMap.HasTile(pos) || loopsWithoutPlacing == 5)
                    {
                        placed = true;
                        Instantiate(attack1Prefab, pos, Quaternion.identity).GetComponent<FireBlast>().SpawnFireballs();
                        
                        break;
                    }
                    loopsWithoutPlacing++;
                }
                
            }
            StartCoroutine(Attack1CD());
        }
        else
        {
            attack2ct = 0;
            StartCoroutine(Attack2CD());
        }
    }
    private IEnumerator Attack2CD()
    {
        //Pillars
        yield return new WaitForSeconds(atk2cd);

        for (int i = 0; i < Random.Range(1, 11); i++)
        {
            bool placed = false;
            int loopsWithoutPlacing = 0;

            while (!placed)
            {
                Vector3Int pos = new Vector3Int(Random.Range(-32, 33), Random.Range(-32, 33), -6);
                if (tileMapVisualizer.floorMap.HasTile(pos) || loopsWithoutPlacing == 10)
                {
                    placed = true;
                    Instantiate(attack2Prefab, pos, Quaternion.identity);
                    break;
                }
                loopsWithoutPlacing++;
            }
        }
        attack2ct++;

        if(attack2ct == 3)
        {
            StartCoroutine(Attack1CD());
        }
        else
        {
            StartCoroutine(Attack2CD());
        }
    }

    private IEnumerator Attack3CD()
    {
        //Ring
        yield return new WaitForSeconds(atk3cd);
        for (int i = 0; i < 36; i++)
        {
            Instantiate(fireball, transform.position, Quaternion.Euler(0, 0, 10f * i));
            yield return new WaitForSeconds(0.05f);
        }
        StartCoroutine(Attack1CD());
    }

    private IEnumerator Attack4CD()
    {
        //Dash
        yield return new WaitForSeconds(atk4cd);

        HashSet<RaycastHit2D> validDirs = new HashSet<RaycastHit2D>();
        HashSet<Vector2> validAngles = new HashSet<Vector2>();
        HashSet<float> angs = new HashSet<float>();

        for(int i = -18; i < 19; i++)
        {
            Vector2 rayDir = (Quaternion.Euler(0,0,i*10) * transform.up).normalized;
            //Debug.Log("Ray");

            RaycastHit2D hit = Physics2D.Raycast(transform.position, rayDir, 64, LayerMask.GetMask("Walls", "MovementBlock"));
            
            if (hit.collider != null)
            {
                //Debug.Log("Hit "  + hit.collider.name);
                if (hit.distance > 10)
                {
                    validDirs.Add(hit);
                    validAngles.Add(rayDir);
                    angs.Add(i * 10);
                    Debug.Log("Valid hit on " + hit.collider.name);
                }
            }
        }

        int roll = Random.Range(0, validDirs.Count);
        RaycastHit2D dir = validDirs.ElementAt(roll);
        Vector2 angle = validAngles.ElementAt(roll);
        float travelAngle = angs.ElementAt(roll);
        //Gets distance of ray
        float dist = 0;

        if(dir.distance > 0)
        {
            dist = dir.distance - wallPadding;
        }
        else
        {
            dist = dir.distance + wallPadding;
        }

        //dist *= Time.deltaTime;
        //get target location
        Vector2 newPos = angle * dist;
        float xTranslate = (newPos.x - transform.position.x), 
            yTranslate = (newPos.y - transform.position.y);

        Debug.DrawRay(new Vector3(transform.position.x, transform.position.y, -15), newPos, Color.red, 100f);
        Debug.Log(String.Format("X: " + xTranslate + ", Y: " + yTranslate));
        Debug.Log(dist);
        Debug.Log(Math.Sqrt(Math.Pow(xTranslate, 2) + Math.Pow(yTranslate, 2)));
        Vector2 move = new Vector2(xTranslate, yTranslate);
        Vector2 startPos = transform.position;

        int movementLoops = 0;
        StartCoroutine(movement(move, movementLoops, newPos, startPos, dist, travelAngle));

        //StartCoroutine(Attack1CD());
    }

    IEnumerator movement(Vector2 move, int loops, Vector2 endPos, Vector2 startPos, float dist, float angle)
    {
        
        gameObject.transform.parent.Translate(move / moveItterations);
        if(loops < moveItterations && (startPos-(Vector2)transform.position).magnitude < dist) {
        
            loops++;
            yield return new WaitForFixedUpdate();
            StartCoroutine(movement(move, loops, endPos, startPos, dist, angle));
            if(loops % 25 == 0)
            {
                //Instantiate(fireball, transform.position, Quaternion.Euler(0, 0, angle));
                Instantiate(fireball, transform.position, Quaternion.Euler(0, 0, angle + 45));
                Instantiate(fireball, transform.position, Quaternion.Euler(0, 0, angle + 135));
                //Instantiate(fireball, transform.position, Quaternion.Euler(0, 0, angle - 135));
            }
        }
        else
        {
            StartCoroutine(Attack1CD());
        }
    }
}
