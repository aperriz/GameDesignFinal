using System.Collections;
using System.Collections.Generic;
using UnityEngine;

sealed class ProcGen : MonoBehaviour
{
    
    public void GenerateDungeon(int mapWidth, int mapHeight, int roomMaxSize, int roomMinSize, int maxRooms, List<RectangularRoom> rooms)
    {
        for(int roomNum = 1; roomNum <= maxRooms; roomNum++)
        {
            int roomWidth = Random.Range(roomMaxSize, roomMinSize);
            int roomHeight = Random.Range(roomMaxSize, roomMinSize);
            
            int roomX = Random.Range(0, mapWidth - roomWidth - 1);
            int roomY = Random.Range(0, mapHeight - roomHeight - 1);

            RectangularRoom newRoom = new RectangularRoom(roomY, roomX, roomWidth, roomHeight);

            if (newRoom.Overlaps(rooms))
            {
                continue;
            }

            for (int x = roomX; x < roomX + roomWidth; x++) 
            {
                for(int y = roomY; y < roomY + roomHeight; y++)
                {
                    /*if(x == roomX || x == roomX + roomWidth - 1 || y == roomY || y == roomY + roomHeight-1)
                    {
                        if(SetWallTileIfEmpty(new Vector3Int(x, y, 0)))
                        {
                            continue;
                        }
                    }
                    else
                    {
                        if (MapManager.)
                        {

                        }
                    }*/
                }
            }
        }

        
    }

}
