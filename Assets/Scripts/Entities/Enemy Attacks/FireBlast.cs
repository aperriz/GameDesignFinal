using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBlast : MonoBehaviour
{
    [SerializeField]
    GameObject fireball;
    Vector3 curPos;

    public void SpawnFireballs()
    {
        curPos = new Vector3(transform.position.x, transform.position.y, -10);

        int roll = Random.Range(1, 4);

        if(roll == 1)
        {
            for (int i = 0; i < 8; i++)
            {
                Instantiate(fireball, curPos, Quaternion.Euler(0, 0, 45 * i));
            }
        }
        else if (roll == 2)
        {
            for (int i = 0; i < 8; i++)
            {
                Instantiate(fireball, curPos, Quaternion.Euler(0, 0, 22.5f + 45 * i));
            }
        }
        else
        {
            for (int i = 0; i < 8; i++)
            {
                Instantiate(fireball, curPos, Quaternion.Euler(0, 0, Random.Range(0f, 45f) + 45 * i));
            }
        }

        Destroy(gameObject);
    }
    
}
