using UnityEngine;
using System.Collections.Generic;
using System;

/* 
 * This struct is a data representation of a mathematical Hex, using the cubic coordinate system.
 * It is purely for bookkeeping and the math side of things.
 * It allows you to automatically convert cubic coordinates into Unity world coordinates,
 * find neighboring hexes, and much more.
 */
public struct Hex {

    // Values to keep track of the object's coordinates in the cube-based coordinate system
    private int q;
    private int r;
    private int s;

    // size of a hexagon, in units
    private float size;
    // padding multiplier
    private float padding;

    // x and z world coordinates (where to render this hex in-scene)
    // (y will always be 0)
    private Vector3 worldCoords;


    public Hex(int q, int r, int s) {
        this.q = q;
        this.r = r;
        this.s = s;
        // reinforce the mathematical constraint on the system
        if (q + r + s != 0) throw new ArgumentException("q + r + s has to equal 0");

        this.size = 1.0f;
        this.padding = 1.1f;

        this.worldCoords = Vector3.zero;

        this.UpdateWorldCoords();
    }

    public void SetCubeCoords(int q, int r, int s)
    {
        if (q + r + s != 0) throw new ArgumentException("q + r + s has to equal 0");
        this.q = q;
        this.r = r;
        this.s = s;
        
        // update world_x and world_z coordinates whenever cube coordinates change
        this.UpdateWorldCoords();
    }

    // updates the current hex's world position based on its cube coordination position
    // (effectively matrix math where q and r are treated as basis vectors)
    public void UpdateWorldCoords() {
        // EXPLANATION: this is simply a matrix multiplication where the basis vectors of the matrix are
        // the diagonal and horizontal axes represented by q and r!
        // notice that if you can move diagonally and horizontally you can find any hexagon
        // with a combination of diagonal and horizontal movements!
        float world_x = (float)(this.size * this.padding * ((Math.Sqrt(3) * this.q) + Math.Sqrt(3)/2 * this.r));
        float world_z = (float)(this.size * this.padding * ((1.5f) * this.r));

        this.worldCoords = new Vector3(world_x, 0, world_z);
    }

    // returns a tuple containing the q,r,s coordinates of this hex
    public (float, float, float) GetCubeCoords() {
        return (this.q, this.r, this.s);
    }

    // returns this Hex's world coordinates.
    public Vector3 GetWorldCoords() {
        return this.worldCoords;
    }

    // List of neighboring Hex structs to make finding specific neighbor easy
    // (starts at left neighbor, going clockwise)
    // (i.e. directions[0] = left neighbor, directions[1] = up-left neighbor, directions[2] = up-right neighbor, etc)
    static public List<Hex> directions = new List<Hex>{new Hex(-1,0,1), new Hex(0,-1,1), new Hex(1,-1,0), new Hex(1,0,-1), new Hex(0,1,-1), new Hex(-1,1,0)};
    
    static public Hex Direction(int i) {
        return Hex.directions[i];
    }

    // function to return a Hex struct representing this Hex's neighbor in a (q,r,s) direction
    // 0 < i < 5; i corresponds to a direction from this hex
    public Hex Neighbor(int i) {
        // Add the direction to this Hex to find the neighbor Hex
        // in that direction
        return Add(Hex.Direction(i));
    }

    // function to return list of Hex structs representing this Hex's neighbors!
    public List<Hex> AllNeighbors() {
        List<Hex> neighbors = new List<Hex>();
        for (int i = 0; i < directions.Count; i++) {
            if (Neighbor(i).q > 4 || Neighbor(i).q < -4) continue;
            if (Neighbor(i).r > 4 || Neighbor(i).r < -4) continue;
            if (Neighbor(i).s > 4 || Neighbor(i).s < -4) continue;
            neighbors.Add(Neighbor(i));
        }
        return neighbors;
    }

    // equals method will return True if this Hex's (q,r,s) match with
    // other's (q,r,s)
    public override bool Equals(object obj) {
        if (!(obj is Hex h)) {
            return false;
        }

        return this.q == h.q && this.r == h.r && this.s == h.s;
    }

    // -------- Private helper functions ---------

    // returns the Hex with a cubic coordinates
    // resulting from adding this Hex and other's
    // cubic coordinates.
    // (useful for finding a neighboring Hex)
    private Hex Add(Hex other) {
        return new Hex(this.q + other.q, this.r + other.r, this.s + other.s);
    }
}
