using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public enum Direction
    {
        North,
        East,
        South,
        West
    }

    public enum TileType
    {
        Rat,
        Goblin,
        Orc,
        Dragon,
        Gold,
        Hero,
        Obstacle,
        OpenSpace
    }

    public int dungeonSize;
    public GameObject OpenSpace;
    public GameObject Hero;
    public GameObject Obstacle;
    public GameObject Gold;
    public GameObject[] enemies;

    TileType[,] dungeon;
    int posX;
    int posZ;

    // Start is called before the first frame update
    void Start()
    {
        InitializeDungeon();
        PlaceObstacles(20);
        PlaceEnemies(8);
        PlaceCoins(10);
        PlaceHero();

        // =========================

        Display();
    }

    // Update is called once per frame
    void Update()
    {
        //Get keyboard movements (W, A, S, D)
        if (Input.GetKeyDown(KeyCode.W))
        {
            MoveCharacter(Direction.North);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            MoveCharacter(Direction.West);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            MoveCharacter(Direction.South);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            MoveCharacter(Direction.East);
        }
    }

    public void InitializeDungeon()
    {
        dungeon = new TileType[dungeonSize, dungeonSize];

        for (int i = 0; i < dungeonSize; i++)
        {
            for (int j = 0; j < dungeonSize; j++)
            {
                dungeon[i, j] = TileType.OpenSpace;
            }
        }
    }

    private void Display()
    {
        GameObject[] tiles = GameObject.FindGameObjectsWithTag("tile");
        foreach(GameObject g in tiles)
        {
            Destroy(g);
        }

        for (int x = 0; x < dungeonSize; x++)
        {
            for (int z = 0; z < dungeonSize; z++)
            {
                switch (dungeon[x, z])
                {
                    case TileType.OpenSpace:
                        Instantiate(OpenSpace, new Vector3(x, 1f, z), Quaternion.identity);
                        break;
                    case TileType.Obstacle:
                        Instantiate(Obstacle, new Vector3(x, 2f, z), Quaternion.identity);
                        break;
                    case TileType.Gold:
                        Instantiate(Gold, new Vector3(x, 1.5f, z), Quaternion.identity);
                        break;
                    case TileType.Hero:
                        Instantiate(Hero, new Vector3(x, 2f, z), Quaternion.identity);
                        break;
                    case TileType.Rat:
                        Instantiate(enemies[0], new Vector3(x, 1f, z), Quaternion.identity);
                        break;
                    case TileType.Goblin:
                        Instantiate(enemies[1], new Vector3(x, 2f, z), Quaternion.identity);
                        break;
                    case TileType.Orc:
                        Instantiate(enemies[2], new Vector3(x, 1f, z), Quaternion.identity);
                        break;
                    case TileType.Dragon:
                        Instantiate(enemies[3], new Vector3(x, 1f, z), Quaternion.identity);
                        break;

                }
            }
        }
    }

    private void PlaceObstacles(int numObstacles)
    {
        for (int i = 0; i < numObstacles; i++)
        {
            int x = Random.Range(0, dungeonSize);
            int z = Random.Range(0, dungeonSize);
            if (dungeon[x, z] == TileType.OpenSpace)
            {
                dungeon[x, z] = TileType.Obstacle;
            }
            else
            {
                i--;
            }
        }
    }

    private void PlaceCoins(int numCoins)
    {
        for (int i = 0; i < numCoins; i++)
        {
            int x = Random.Range(0, dungeonSize);
            int z = Random.Range(0, dungeonSize);
            if (dungeon[x, z] == TileType.OpenSpace)
            {
                dungeon[x, z] = TileType.Gold;
            }
            else
            {
                i--;
            }
        }
    }

    private void PlaceHero()
    {
        for (int i = 0; i < 1; i++)
        {
            int x = Random.Range(0, dungeonSize);
            int z = Random.Range(0, dungeonSize);
            if (dungeon[x, z] == TileType.OpenSpace)
            {
                dungeon[x, z] = TileType.Hero;
                posX = x;
                posZ = z;
            }
            else
            {
                i--;
            }
        }
    }

    private void PlaceEnemies(int numEnemies)
    {
        for (int i = 0; i < numEnemies; i++)
        {
            int x = Random.Range(0, dungeonSize);
            int z = Random.Range(0, dungeonSize);
            if (dungeon[x, z] == TileType.OpenSpace)
            {
                dungeon[x, z] = (TileType)Random.Range(0, 4);
            }
            else
            {
                i--;
            }
        }

    }

    private void MoveCharacter(Direction dir)
    {
        int newX = posX;
        int newZ = posZ;
        switch (dir)
        {
            case Direction.North: newZ = posZ + 1; break;
            case Direction.South: newZ = posZ - 1; break;
            case Direction.East: newX = posX + 1; break;
            case Direction.West: newX = posX - 1; break;
        }
        if(dungeon[newZ,newX] == TileType.OpenSpace)
        {
            dungeon[posX, posZ] = TileType.OpenSpace;
            posX = newX;
            posZ = newZ;
            dungeon[posX, posZ] = TileType.Hero;
            Display();
        }
        else
        {
            
        }
    }
}
