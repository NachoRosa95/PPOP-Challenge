using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Layout;
using UnityEngine.EventSystems;
using PathFinding;

public class Tile : MonoBehaviour, IAStarNode
{
    #region Exposed fields

    public enum BiomeType : byte
    {
        Desert,
        Forest,
        Grass,
        Mountain,
        Water
    }

    public Hex Hex;
    public IEnumerable<IAStarNode> Neighbours => neighbours;

    #endregion Exposed fields

    #region Internal fields

    // In this case, biome and travel cost can be independently configured for more flexiblity

    [SerializeField]
    private bool canTravel = true;

    [SerializeField]
    private byte travelCost = 1;

    [SerializeField]
    private BiomeType biome = BiomeType.Grass;

    [SerializeField]
    private List<Material> biomeMaterials;

    [SerializeField]
    private MeshRenderer biomeMesh;

    [SerializeField]
    Pathfinder pathfinder;

    private EventTrigger eventTrigger;
    private List<Tile> neighbours = new List<Tile>();
    private HexLayout layout;

    #endregion Internal fields

    #region Properties

    public bool CanTravel => canTravel;
    public byte TravelCost => travelCost;
    public BiomeType Biome
    {
        get => biome;
        set
        {
            if (biome == value) return;
            UpdateBiome();
            biome = value;
        }
    }

    #endregion Properties

    #region Custom Events

    public CustomEvent<Tile> OnPointerEnter = new CustomEvent<Tile>();
    public CustomEvent<Tile> OnPointerClick = new CustomEvent<Tile>();

    #endregion Custom Events

    #region Events methods

    private void Awake()
    {
        eventTrigger = GetComponent<EventTrigger>();
        var pointerEnter = new EventTrigger.Entry();
        pointerEnter.callback.AddListener((data) => OnPointerEnter?.Invoke(this));
        pointerEnter.eventID = EventTriggerType.PointerEnter;
        eventTrigger.triggers.Add(pointerEnter);

        var pointerClick = new EventTrigger.Entry();
        pointerClick.callback.AddListener((data) => OnPointerClick?.Invoke(this));
        pointerClick.eventID = EventTriggerType.PointerClick;
        eventTrigger.triggers.Add(pointerClick);

        UpdateBiome();
    }

    #endregion Events methods

    #region Public Methods

    public void SetLayout(HexLayout layout)
    {
        this.layout = layout;
    }

    public void SetCoords(int i, int j)
    {
        var cubeCoords = OffsetCoord.RoffsetToCube(-1, new OffsetCoord(i, j));
        Hex = new Hex(cubeCoords.q, cubeCoords.r, cubeCoords.s);
    }

    public void CacheNeighbors()
    {
        neighbours.Clear();
        var neighborHexes = Hex.Neighbors();
        foreach (var neighborHex in neighborHexes)
        {
            if (!layout.TilesDictionary.ContainsKey(neighborHex)) continue;
            var neighborTile = layout.TilesDictionary[neighborHex];

            if (neighborTile && neighborTile.canTravel) neighbours.Add(neighborTile);
        }
    }

    public float CostTo(IAStarNode neighbour)
    {
        var tile = neighbour as Tile;
        if (!tile) return float.MaxValue;
        return tile.travelCost;
    }

    public float EstimatedCostTo(IAStarNode target)
    {
        var tile = target as Tile;
        if (!tile) return float.MaxValue;
        return Hex.Distance(tile.Hex);
    }

    #endregion Public Methods

    #region Non Public Methods

    private void UpdateBiome()
    {
        try
        {
            biomeMesh.material = biomeMaterials[(int)biome];
        }
        catch (ArgumentOutOfRangeException ex)
        {
            Debug.LogException(ex);
        }
    }

    #endregion Non Public Methods
}
