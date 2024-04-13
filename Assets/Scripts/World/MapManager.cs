using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour
{
    public static MapManager instance;
    [SerializeField] private int width = 200, height = 113;
    [SerializeField] private Color32 darkColor= new Color32(0,0,0,0), lightColor = new Color32(255,255,255,255);
    [SerializeField] public TileBase floorTile;
    [SerializeField] public TileBase[] wallTiles;
    [SerializeField] private Tilemap floorMap, obstacleMap;

    public Tilemap FloorMap { get => floorMap; }

    public Tilemap ObstacleMap { get => obstacleMap; }

    // Start is called before the first frame update
    void Start()
    {
        Vector3Int centerTile = new Vector3Int(width/2, height/2, 0);

        BoundsInt wallBounds = new BoundsInt(new Vector3Int(29, 28, 0), new Vector3Int(3, 1, 0));

        for(int x = 0; x < wallBounds.size.x; x++)
        {
            for(int y=0;y < wallBounds.size.y; y++)
            {
                Vector3Int wallPosition = new Vector3Int(wallBounds.min.x + x, wallBounds.min.y+y, 0);
               
                    obstacleMap.SetTile(wallPosition, wallTiles[0]);
                
            }
        }

        //Instantiate(Resources.Load<GameObject>("Player"), new Vector3 (0,0,-10), Quaternion.identity).name = "Player";

    }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else { Destroy(gameObject); }

    }

    public bool InBounds(int x, int y)
    {
        if(x < width & y < height)
        {
            return true;
        }
        return false;
    }

    //https://stackoverflow.com/questions/41909210/prevent-2d-player-from-moving-through-wall
}
