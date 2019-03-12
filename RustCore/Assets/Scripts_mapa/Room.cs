﻿
using UnityEngine;

public class Room : MonoBehaviour
{
    public Doorway[] doorways;
    public BoxCollider boxCollider;

    public Bounds RoomBounds
    {
        get { return boxCollider.bounds; }
    }

}
