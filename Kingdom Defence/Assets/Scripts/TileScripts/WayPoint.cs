using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour
{
    [SerializeField] GameObject tower;
    [SerializeField] bool mIsPlaceAble = false;

    public bool isPlaceAble{
        get{
            return mIsPlaceAble;
        }
    }


    
    // Start is called before the first frame update
    private void OnMouseDown() {
        if(mIsPlaceAble){
            Debug.Log(transform.name);
            Instantiate(tower, transform.position, Quaternion.identity);
            mIsPlaceAble = false;
        } 
    }
    
}
