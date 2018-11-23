using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Node {

    public bool walkable;
    public Vector3 worldPosition;

    public Node(bool w, Vector3 wP)
    {
        walkable = w;
        worldPosition = wP;
    }
}
