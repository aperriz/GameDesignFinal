using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private int entityNum = 0;
    [SerializeField] private List<Entity> entities = new List<Entity>();

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void AddEntity(Entity entity)
    {
        entities.Add(entity);
    }


    public void InsertEntity(Entity entity)
    {
        entities.Insert(entityNum, entity);
    }
}
