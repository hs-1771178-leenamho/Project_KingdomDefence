using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour
{
    [SerializeField] Tower towerPrefab;
    [SerializeField] bool mIsPlaceAble = false;

    public bool isPlaceAble{
        get{
            return mIsPlaceAble;
        }
    }


    
    // Start is called before the first frame update
    private void OnMouseDown() {
        if(mIsPlaceAble){
            bool isPlaced = towerPrefab.CreateTower(towerPrefab, transform.position);
            mIsPlaceAble = !isPlaced;
        } 
    }
    
}
