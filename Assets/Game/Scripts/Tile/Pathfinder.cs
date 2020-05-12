using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathFinding;
using Layout;
using System.Linq;

public class Pathfinder : MonoBehaviour
{
    #region Exposed fields

    [SerializeField]
    HexLayout layout;

    [SerializeField]
    Mesh tilePreviewMesh;

    #endregion Exposed fields

    #region Internal fields

    private Tile start;
    private Tile goal;

    private IEnumerable<Tile> lastPath;
    private LineRenderer lineRenderer;

    #endregion Internal fields

    #region Properties

    #endregion Properties

    #region Custom Events

    #endregion Custom Events

    #region Events methods

    #endregion Events methods

    #region Public Methods

    public IEnumerable<Tile> GetPath(Tile start, Tile goal)
    {
        var path = AStar.GetPath(start, goal);
        if (path == null || path.Count == 0)
        {
            // No Path found
            return null;
        }
        lastPath = path.Cast<Tile>();
        UpdateRenderer();
        return lastPath;
    }

    public void OnTileClicked(Tile tile)
    {
        if (start == null) start = tile;
        else if (goal == null)
        {
            goal = tile;
            GetPath(goal, start);
        }
        else
        {
            start = tile;
            goal = null;
        }
    }

    #endregion Public Methods

    #region Non Public Methods

    private void UpdateRenderer()
    {
        if (!lineRenderer) lineRenderer = GetComponent<LineRenderer>();
        if (lastPath == null) return;
        var count = lastPath.Count();
        var positions = new List<Vector3>();
        foreach (var tile in lastPath) positions.Add(tile.transform.position + Vector3.up * 0.3f);
        lineRenderer.positionCount = count;
        lineRenderer.SetPositions(positions.ToArray());
        lineRenderer.enabled = true;
    }

    #endregion Non Public Methods
}
