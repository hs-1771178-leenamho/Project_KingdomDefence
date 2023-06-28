using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 순수 C# 클래스는 Serialized해도 인스펙터에 보이지 않음
// 따라서 System.Serializable 키워드를 추가해 클래스가 인스펙터에 나타날 수 있도록 만듬
[System.Serializable] 
public class Node
{
    // MonoBehaviour를 상속받지 않은 순수 C# 클래스는 오브젝트에 컴포넌트로 추가할 수 없음 
    public Vector2Int coordinates;
    public bool isWalkable;
    public bool isExplored;
    public bool isPathed;
    public Node connectedTo;

    public Node(Vector2Int _coordinates, bool _isWalkable){
        this.coordinates = _coordinates;
        this.isWalkable = _isWalkable;
    }
}
