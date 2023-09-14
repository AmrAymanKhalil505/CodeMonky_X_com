using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class GridSystem 
{
    private int width;
    private int height;
    private float cellSize;
    private GridObject[,] gridObjectArray;

    //private Transform[,] debugGridObjectArray2;
    public GridSystem(int width, int height, float cellSize)
    {
        this.width=width;
        this.height=height;
        this.cellSize=cellSize;
        gridObjectArray = new GridObject[width,height];

        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                GridPosition gridPosition = new GridPosition(x,z);
                gridObjectArray[x,z] = new GridObject(this,gridPosition);
                
            }
        }

        
    }

    public Vector3 GetWorldPosition(GridPosition gridPosition)
    {
        return new Vector3(gridPosition.X, 0.01f, gridPosition.Z) * cellSize;
    }

    public GridPosition GetGridPosition(Vector3 WorldPosition)
    {
        return new GridPosition(
            Mathf.RoundToInt(WorldPosition.x / cellSize),
            Mathf.RoundToInt(WorldPosition.z / cellSize)
        );
    }

    public void creatDebugObjects(Transform debugPrefab)
    {
        //debugGridObjectArray2= new Transform[width,height];



        for (int x = 0;x < width;x++)
        {
            for(int  z = 0;z < height;z++)
            {
                GridPosition gridPosition = new GridPosition(x, z);
                Transform debugGridTransform=  Transform.Instantiate(debugPrefab, GetWorldPosition(new GridPosition(x, z)), Quaternion.identity);
                debugGridTransform.GetComponent<GridDebugObject>().setGridObject(GetGridObject(new GridPosition(x, z)));
                //debugGridObjectArray2[x,z] = debugGridTransform;
            }
        }
    }

    public GridObject GetGridObject( GridPosition gridPosition )
    {
        return gridObjectArray[gridPosition.X, gridPosition.Z];
    }


    public bool isValidGridPosition(GridPosition gp)
    {
        if (gp.X<0 || gp.X>= this.width)
        {
            return false;
        }
        if(gp.Z<0 || gp.Z >this.height)
        {
            return false;
        }
        return true;
    }

    public int GetWidth()
    {
        return width;
    }
    public int GetHeight()
    {
        return height;
    }
}
