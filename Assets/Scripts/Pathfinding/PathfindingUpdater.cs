using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfindingUpdater : MonoBehaviour
{
    private void Start()
    {
        DestructibleCrate.OnAnyDestroyed += DestructibleCrate_OnAnyDestroyed;
        DestructibleWall.OnAnyDestroyed += DestructibleWall_OnAnyDestroyed;
    }

    private void DestructibleCrate_OnAnyDestroyed(object sender, EventArgs e)
    {
        DestructibleCrate destructibleCrate = sender as DestructibleCrate;
        Pathfinding.Instance.SetIsWalkableGridPosition(destructibleCrate.GetGridPosition(), true);
    }

    private void DestructibleWall_OnAnyDestroyed(object sender, EventArgs e)
    {
        DestructibleWall destructibleWall = sender as DestructibleWall;
        Pathfinding.Instance.SetIsWalkableGridPosition(destructibleWall.GetGridPosition(), true);
    }
}
