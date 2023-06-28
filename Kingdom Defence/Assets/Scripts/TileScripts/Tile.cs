using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] Tower towerPrefab;
    [SerializeField] bool mIsPlaceAble = false;

    GridManager gridManager;
    PathFinding pathFinder;

    Vector2Int coordinates = new Vector2Int();

    void Awake() {
        gridManager = FindObjectOfType<GridManager>();
        pathFinder = FindObjectOfType<PathFinding>();
    }

    void Start() {
        if(gridManager != null){
            coordinates = gridManager.GetCoordinatesFromPostion(transform.position);

            if(!isPlaceAble){
                gridManager.BlockNode(coordinates);
            }
       
        }
        
    }

    public bool isPlaceAble{
        get{
            return mIsPlaceAble;
        }
    }


    
    // Start is called before the first frame update
    private void OnMouseDown() {
        if(gridManager.GetNode(coordinates).isWalkable && !pathFinder.WillBlockPath(coordinates)){
            bool isSuccessful = towerPrefab.CreateTower(towerPrefab, transform.position);
            if(isSuccessful){
                gridManager.BlockNode(coordinates);
                pathFinder.NotifiyReceiver();
            } 
        } 
    }
    
}
