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

    /// <summary>
    /// Gets shortest path between two tiles
    /// </summary>
    /// <param name="start"></param>
    /// <param name="goal"></param>
    /// <returns>Returns shortest path as an IEnumerable of Tile</returns>
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

    /// <summary>
    /// Called when a tile is clicked
    /// </summary>
    /// <param name="tile"></param>
    public void OnTileClicked(Tile tile)
    {
        if (start == null) start = tile;
        else if (goal == null)
        {
            goal = tile;
        }
        else
        {
            start = tile;
            goal = null;
        }
    }

    /// <summary>
    /// Called when a tile is hovered over
    /// </summary>
    /// <param name="tile"></param>
    public void OnTileEntered(Tile tile)
    {
        if (start == null) return;
        else if (goal == null)
        {
            GetPath(start, tile);
        }
    }

    #endregion Public Methods

    #region Non Public Methods

    /// <summary>
    /// Updates the attached LineRenderer to display the last valid path
    /// </summary>
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
