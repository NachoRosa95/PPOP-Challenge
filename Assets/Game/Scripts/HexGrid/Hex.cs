using System;
using System.Collections.Generic;
using UnityEngine;

namespace Layout
{
    /// <summary>
    /// Stores cube coordinates of a hexagon and operates on them.
    /// See https://www.redblobgames.com/grids/hexagons
    /// </summary>
    public struct Hex
    {
        public Hex(int q, int r, int s)
        {
            this.q = q;
            this.r = r;
            this.s = s;
            if (q + r + s != 0) throw new ArgumentException("q + r + s must be 0");
        }

        public readonly int q;
        public readonly int r;
        public readonly int s;

        static public List<Hex> directions = new List<Hex> { new Hex(1, 0, -1), new Hex(1, -1, 0), new Hex(0, -1, 1), new Hex(-1, 0, 1), new Hex(-1, 1, 0), new Hex(0, 1, -1) };
        static public List<Hex> diagonals = new List<Hex> { new Hex(2, -1, -1), new Hex(1, -2, 1), new Hex(-1, -1, 2), new Hex(-2, 1, 1), new Hex(-1, 2, -1), new Hex(1, 1, -2) };

        /// <summary>
        /// Adds this hex and hex b together
        /// </summary>
        /// <param name="b"></param>
        /// <returns>Returns a new hex with the result of the addition</returns>
        public Hex Add(Hex b)
        {
            return new Hex(q + b.q, r + b.r, s + b.s);
        }

        /// <summary>
        /// Subtracts hex b from this hex
        /// </summary>
        /// <param name="b"></param>
        /// <returns>Returns a new hex with the result of the subtraction</returns>
        public Hex Subtract(Hex b)
        {
            return new Hex(q - b.q, r - b.r, s - b.s);
        }

        /// <summary>
        /// Scales this hex by constant k
        /// </summary>
        /// <param name="k"></param>
        /// <returns>Returns a new hex with the result of the scaling</returns>
        public Hex Scale(int k)
        {
            return new Hex(q * k, r * k, s * k);
        }

        /// <summary>
        /// Rotates this hex anti-clockwise
        /// </summary>
        /// <returns>Returns a new hex with the result of the rotation</returns>
        public Hex RotateLeft()
        {
            return new Hex(-s, -q, -r);
        }

        /// <summary>
        /// Rotates this hex clockwise
        /// </summary>
        /// <returns>Returns a new hex with the result of the rotation</returns>
        public Hex RotateRight()
        {
            return new Hex(-r, -s, -q);
        }

        /// <summary>
        /// Moves this hex a given direction
        /// </summary>
        /// <param name="direction"></param>
        /// <returns>Returns a new hex with the result of the translation</returns>
        static public Hex Direction(int direction)
        {
            return Hex.directions[direction];
        }

        /// <summary>
        /// Gets all hexes with adjacent coordinates
        /// </summary>
        /// <returns>Returns a list of hexes with neighboring coordinates</returns>
        public List<Hex> Neighbors()
        {
            var neighbors = new List<Hex>();
            for (int i = 0; i < directions.Count; i++)
            {
                neighbors.Add(Neighbor(i));
            }
            return neighbors;
        }

        /// <summary>
        /// Gets adjacent hex at given direction
        /// </summary>
        /// <param name="direction"></param>
        /// <returns>Returns a new hex with neighboring coordinates at given direction</returns>
        public Hex Neighbor(int direction)
        {
            return Add(Hex.Direction(direction));
        }

        /// <summary>
        /// Gets hex diagonally adjacent at given direction (neighbor of two of the neighbors)
        /// </summary>
        /// <param name="direction"></param>
        /// <returns>Returns a new hex with diagionally adjacent coordinates at given direction</returns>
        public Hex DiagonalNeighbor(int direction)
        {
            return Add(Hex.diagonals[direction]);
        }

        /// <summary>
        /// Gets length of this hex vector
        /// </summary>
        /// <returns>Returns the length in int form</returns>
        public int Length()
        {
            return (int)((Math.Abs(q) + Math.Abs(r) + Math.Abs(s)) / 2);
        }

        /// <summary>
        /// Gets the distance between this hex and hex b
        /// </summary>
        /// <param name="b"></param>
        /// <returns>Returns the distance in int form</returns>
        public int Distance(Hex b)
        {
            return Subtract(b).Length();
        }

        /// <summary>
        /// Checks for equality between this hex and object b
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>Returns true if b is a hex and has the same coordinates</returns>
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj is Hex)
            {
                var other = (Hex)obj;
                return (q == other.q && r == other.r && s == other.s);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Gets hash code
        /// </summary>
        /// <returns>Returns hash code in int form</returns>
        public override int GetHashCode()
        {
            return new Vector3(q, r, s).GetHashCode();
        }
    }
}
