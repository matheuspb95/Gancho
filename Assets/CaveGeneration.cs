using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class CaveGeneration : MonoBehaviour {
    public GameObject SquarePrefab;
    GameObject TempSquare;
    public int Width, Heigth;
    [Range(0,100)]
    public float FillPercentage = 50;
    int[,] map;
    int[,] TempMap;
    [Range(0, 9)]
    public int KillCondition, LifeCondition; 
    public int IterationsNumber, InitSeed;
	// Use this for initialization
	void Start () {
        map = new int[Width, Heigth];
        MakeMap();
	}

    public void MakeMap()
    {
        Random.seed = InitSeed;
        CleanMap();
        GenerateRandomNumbers();
        for (int i = 0; i < IterationsNumber; i++)
        {
            SmoothMap();
        }
        FillMap();
    }

    void SmoothMap()
    {
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Heigth; y++)
            {
                int neighbourWallTiles = GetSurroundingTiles(x, y);

                if (neighbourWallTiles > LifeCondition)
                    map[x, y] = 1;
                else if (neighbourWallTiles < KillCondition)
                    map[x, y] = 0;

            }
        }
    }

    int GetSurroundingTiles(int Posx, int Posy)
    {
        int wallCount = 0;
        for (int neighbourX = Posx - 1; neighbourX <= Posx + 1; neighbourX++)
        {
            for (int neighbourY = Posy - 1; neighbourY <= Posy + 1; neighbourY++)
            {
                if (neighbourX >= 0 && neighbourX < Width && neighbourY >= 0 && neighbourY < Heigth)
                {
                    if (neighbourX != Posx || neighbourY != Posy)
                    {
                        wallCount += map[neighbourX, neighbourY];
                    }
                }
                else
                {
                    wallCount++;
                }
            }
        }
        return wallCount;
    } 

    void GenerateRandomNumbers()
    {
        for (int i = 0; i < Width; i++)
        {
            for (int j = 0; j < Heigth; j++)
            {
                if (i == 0 || j == 0 || i == Width-1 || j == Heigth - 1)
                {
                    map[i, j] = 1;
                }else
                {
                    int r = Random.Range(0, 100);

                    if (r <  FillPercentage)
                    {
                        map[i, j] = 1;
                    }else
                    {
                        map[i, j] = 0;
                    }
                }                
            }
        }
    }

    public void FillMap()
    {
        for (int i = 0; i < Width; i++)
        {
            for (int j = 0; j < Heigth; j++)
            {
                if(map[i,j] == 1)
                {
                    TempSquare = (GameObject)Instantiate(SquarePrefab, new Vector3(i, j), Quaternion.identity);
                    TempSquare.transform.parent = transform;
                }
            }
        }
    }

    void CleanMap()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }
}

[CustomEditor(typeof(CaveGeneration))]
public class LevelScriptEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        CaveGeneration myScript = (CaveGeneration)target;
        if (GUILayout.Button("Make Map"))
        {
            myScript.MakeMap();
        }
    }
}