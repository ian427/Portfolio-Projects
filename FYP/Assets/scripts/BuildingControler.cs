
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BuildingControler : MonoBehaviour
{
    [Header("Runtime state")]
    public int EnemiesInArea;

    [Header("Grid / Bounds")]
    public Grid2 CoverMap;               // Must be assigned or auto-found
    public Vector2Int min;               // inclusive (global grid coords)
    public Vector2Int max;               // inclusive (global grid coords)

    [Header("Map Indices (match your MapGenerator)")]
    public int GREEN_INDEX = 1;         // room floor index in CoverMap
    public int ORANGE_INDEX = 2;         // door index in CoverMap
    public int WALL_INDEX = 3;         // wall index in CoverMap
    public int WALL_MARK = 100;       // your special wall mark (if used)

    [Header("Graph output")]
    public BuildingGraph graph;

    // ------------------ TileKind ------------------
    public enum TileKind
    {
        Wall,
        RoomFloor,
        Door,
        Empty
    }

    // Map a CoverMap index to a TileKind
    private TileKind ConvertIndexToTile(int index)
    {
        if (index == GREEN_INDEX) return TileKind.RoomFloor;
        if (index == ORANGE_INDEX) return TileKind.Door;
        if (index == WALL_INDEX) return TileKind.Wall;
        if (index == WALL_MARK) return TileKind.Wall; // your special mark
        return TileKind.Empty;
    }

    // ------------------ RoomRegion + Graph ------------------
    [Serializable]
    public class RoomRegion
    {
        public int id;
        public List<Vector2Int> tiles = new();     // local coords (relative to min)
        public Vector2 centerLocal;                // local center (grid space)
        public Vector3 centerWorld;                // world-space center (optional helper)
        public List<GameObject> enemiesInRoom = new List<GameObject>();
    }

    [Serializable]
    public class BuildingGraph
    {
        public List<RoomRegion> rooms;
        public Dictionary<int, List<int>> adjacency;   // roomID -> neighbor roomIDs
        public List<Vector2Int> doorLocalPositions;
        public List<Vector2Int> outsideEntryPoints;// (optional) doors in local coords
    }
    /*
    public List<Vector3> GetCoverPositions(RoomRegion room)
    {
        List<Vector3> coverPositions = new List<Vector3>();

        foreach (var tile in room.tiles)
        {
            int coverIndex = CoverMap.GetValue(tile.x, tile.y);

            if (coverIndex == WALL_INDEX || coverIndex == WALL_MARK)
            {
                Vector3 worldPos = GridToWorld(tile);
                coverPositions.Add(worldPos);
            }
        }

        return coverPositions;
    }
    */
    // ------------------ Lifecycle ------------------
    private void Start()
    {


        min = new Vector2Int(
            Mathf.FloorToInt(transform.position.x - (transform.localScale.x * 0.5f)),
            Mathf.FloorToInt(transform.position.z - (transform.localScale.z * 0.5f))
        );

        max = new Vector2Int(
            Mathf.FloorToInt(transform.position.x + (transform.localScale.x * 0.5f)),
            Mathf.FloorToInt(transform.position.z + (transform.localScale.z * 0.5f))
        );


        // Try to auto-find CoverMap if not assigned
        if (CoverMap == null)
        {
            var gg = GameObject.Find("gridgenerator");
            if (gg != null)
            {
                var gt = gg.GetComponent<Gridtest>();
                if (gt != null) CoverMap = gt.grid;
            }
        }

        if (!ValidateReady()) return;

        RebuildGraph();
    }
    public List<Vector3> GetCoverPositions(BuildingControler.RoomRegion room, Grid2 coverMap, Vector2Int min)
    {
        List<Vector3> coverPositions = new List<Vector3>();

        foreach (var tile in room.tiles)
        {
            int coverIndex = coverMap.GetValue(tile.x, tile.y);

            // Only tiles that are cover (next to walls or marked)
            if (coverIndex == WALL_INDEX || coverIndex == WALL_MARK)
            {
                Vector3 worldPos = GridToWorld(min + tile);
                coverPositions.Add(worldPos);
            }
        }

        return coverPositions;
    }
    // ------------------ Public API ------------------
    /// <summary> Rebuild graph from CoverMap inside [min, max]. </summary>
    public void RebuildGraph()
    {
        if (!ValidateReady()) return;

        var local = ExtractTilesForBuilding();
        var rooms = DetectRooms(local);

        var adj = BuildConnections(local, rooms,
            out var doorLocals,
            out var outsideEntries);


        // Fill world centers for convenience
        foreach (var r in rooms)
            r.centerWorld = GridToWorld(min + Vector2Int.RoundToInt(r.centerLocal));


        graph = new BuildingGraph
        {
            rooms = rooms,
            adjacency = adj,
            doorLocalPositions = doorLocals,
            outsideEntryPoints = outsideEntries

        };

        // Debug
        // Debug.Log($"[{name}] Rooms: {rooms.Count}, Doors: {doorLocals.Count}, Bounds: {min} -> {max}");
    }

    // ------------------ Core steps ------------------
    /// <summary> Extract a local TileKind[,] for this building’s bounding box. </summary>
    private TileKind[,] ExtractTilesForBuilding()
    {
        int w = max.x - min.x + 1;
        int h = max.y - min.y + 1;
        var local = new TileKind[w, h];

        for (int lx = 0; lx < w; lx++)
        {
            int gx = min.x + lx;
            for (int ly = 0; ly < h; ly++)
            {
                int gy = max.y - ly; // NOTE: choose orientation. If your grid origin is bottom-left, use (min.y + ly)
                                     // If your generator uses world = (x,0,y) with y increasing upwards, pick the same mapping consistently.
                                     // Replace with: int gy = min.y + ly; if that's your convention.

                int idx = 0;
                local[lx, ly] = ConvertIndexToTile(idx);
            }
        }

        return local;
    }

    /// <summary> Detect all rooms (connected regions of RoomFloor) via flood fill. </summary>
    private List<RoomRegion> DetectRooms(TileKind[,] grid)
    {
        int w = grid.GetLength(0);
        int h = grid.GetLength(1);

        bool[,] visited = new bool[w, h];
        var rooms = new List<RoomRegion>();
        int nextId = 0;

        for (int x = 0; x < w; x++)
        {
            for (int y = 0; y < h; y++)
            {
                if (grid[x, y] == TileKind.RoomFloor && !visited[x, y])
                {
                    var rr = new RoomRegion { id = nextId++ };
                    FloodRoom(grid, visited, x, y, rr);
                    rooms.Add(rr);
                }
            }
        }
        return rooms;
    }

    private void FloodRoom(TileKind[,] grid, bool[,] visited, int sx, int sy, RoomRegion rr)
    {
        int w = grid.GetLength(0);
        int h = grid.GetLength(1);

        var q = new Queue<Vector2Int>();
        q.Enqueue(new Vector2Int(sx, sy));

        Vector2 sum = Vector2.zero;

        while (q.Count > 0)
        {
            var p = q.Dequeue();
            if (visited[p.x, p.y]) continue;

            visited[p.x, p.y] = true;
            rr.tiles.Add(p);
            sum += (Vector2)p;

            foreach (var d in FourDir)
            {
                var np = p + d;
                if (InBounds(np, w, h) &&
                    !visited[np.x, np.y] &&
                    grid[np.x, np.y] == TileKind.RoomFloor)
                {
                    q.Enqueue(np);
                }
            }
        }

        rr.centerLocal = sum / Mathf.Max(1, rr.tiles.Count);
    }

    /// <summary> Build door-based adjacency between room regions. </summary>

    private Dictionary<int, List<int>> BuildConnections(
        TileKind[,] grid,
        List<RoomRegion> rooms,
        out List<Vector2Int> doorLocalPositions,
        out List<Vector2Int> outsideEntryPoints)
    {
        int w = grid.GetLength(0);
        int h = grid.GetLength(1);

        // Map each room tile -> its region ID
        var tileToRoom = new Dictionary<Vector2Int, int>();
        foreach (var r in rooms)
            foreach (var t in r.tiles)
                tileToRoom[t] = r.id;

        // adjacency graph
        var result = new Dictionary<int, List<int>>();
        foreach (var r in rooms)
            result[r.id] = new List<int>();

        doorLocalPositions = new List<Vector2Int>(); // internal doors
        outsideEntryPoints = new List<Vector2Int>(); // NEW — outside doors

        // scan whole local grid
        for (int x = 0; x < w; x++)
        {
            for (int y = 0; y < h; y++)
            {
                if (grid[x, y] != TileKind.Door)
                    continue;

                Vector2Int door = new Vector2Int(x, y);
                HashSet<int> touchingRooms = new HashSet<int>();

                // check 4 sides
                foreach (var d in FourDir)
                {
                    var np = door + d;
                    if (np.x >= 0 && np.x < w && np.y >= 0 && np.y < h)
                    {
                        if (tileToRoom.TryGetValue(np, out int roomID))
                        {
                            touchingRooms.Add(roomID);
                        }
                    }
                }

                // Case 1: INTERNAL DOOR (connects two rooms)
                if (touchingRooms.Count == 2)
                {
                    var a = touchingRooms.ElementAt(0);
                    var b = touchingRooms.ElementAt(1);

                    if (!result[a].Contains(b)) result[a].Add(b);
                    if (!result[b].Contains(a)) result[b].Add(a);

                    doorLocalPositions.Add(door);
                }
                // Case 2: OUTSIDE ENTRY DOOR (touches exactly one room)
                else if (touchingRooms.Count == 1)
                {
                    outsideEntryPoints.Add(door);
                }
                // else (0 rooms) ignore
            }
        }

        return result;
    }


    // ------------------ Utilities ------------------
    private static readonly Vector2Int[] FourDir =
    {
        new Vector2Int( 1,  0),
        new Vector2Int(-1,  0),
        new Vector2Int( 0,  1),
        new Vector2Int( 0, -1),
    };

    private static bool InBounds(Vector2Int p, int w, int h)
        => (uint)p.x < (uint)w && (uint)p.y < (uint)h;



    private bool ValidateReady()
    {
        if (CoverMap == null)
        {
            Debug.LogError($"[{name}] CoverMap is null. Assign it or ensure gridgenerator/Gridtest exists.");
            return false;
        }
        if (min.x > max.x || min.y > max.y)
        {
            Debug.LogError($"[{name}] Invalid bounds: min {min} must be <= max {max}.");
            return false;
        }
        return true;
    }

    /// <summary>
    /// Convert global grid coordinate to world position.
    /// Your MapGenerator uses (x, 0, y) so we mirror that.
    /// </summary>
    public static Vector3 GridToWorld(Vector2Int g) => new Vector3(g.x, 0f, g.y);

    // ------------------ Gizmos (debug) ------------------
    private void OnDrawGizmosSelected()
    {
        // Draw bounds
        Gizmos.color = new Color(0, 0.6f, 1f, 0.35f);
        var a = GridToWorld(min);
        var b = GridToWorld(new Vector2Int(max.x + 1, max.y + 1)); // +1 so it covers cells
        var size = b - a;
        var center = a + size * 0.5f;
        Gizmos.DrawCube(center, new Vector3(size.x, 0.05f, size.z));

        if (graph == null || graph.rooms == null) return;

        // Draw room centers
        Gizmos.color = Color.green;
        foreach (var r in graph.rooms)
            Gizmos.DrawSphere(r.centerWorld + Vector3.up * 0.05f, 0.2f);

        // Draw adjacency
        if (graph.adjacency != null)
        {
            Gizmos.color = new Color(1f, 0.5f, 0f, 0.9f);
            foreach (var kv in graph.adjacency)
            {
                var from = graph.rooms.FirstOrDefault(rr => rr.id == kv.Key);
                if (from == null) continue;
                foreach (var nid in kv.Value)
                {
                    var to = graph.rooms.FirstOrDefault(rr => rr.id == nid);
                    if (to == null) continue;
                    Gizmos.DrawLine(from.centerWorld + Vector3.up * 0.05f,
                                    to.centerWorld + Vector3.up * 0.05f);
                }
            }
        }

        // Draw doors
        if (graph.doorLocalPositions != null)
        {
            Gizmos.color = Color.yellow;
            foreach (var d in graph.doorLocalPositions)
            {
                var g = min + d;
                Gizmos.DrawCube(GridToWorld(g) + Vector3.up * 0.02f, new Vector3(0.4f, 0.04f, 0.4f));
            }
        }
    }

    // ------------------ Enemy counting (your existing) ------------------
    public void RemoveCount() => EnemiesInArea--;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy")) EnemiesInArea++;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy")) EnemiesInArea--;
    }

    public Vector3 NodeLocalToWorld(Vector2Int localNode)
    {
        // Convert local → global
        Vector2Int global = min + localNode;

        // Convert grid → world (your world uses x,z)
        return new Vector3(global.x, 0f, global.y);

    }

}
