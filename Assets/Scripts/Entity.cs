using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] private bool isSentient = false;

    public bool IsSentient { get => isSentient; }

    // Start is called before the first frame update
    void Start()
    {
        /*if (GetComponent<PlayerMovement>())
        {
            GameManager.instance.InsertEntity(this);
        }
        else if(IsSentient)
            GameManager.instance.AddEntity(this);*/
    }

    public void Move(Vector2 direction)
    {
        transform.position += (Vector3)direction.normalized;
    }
}
