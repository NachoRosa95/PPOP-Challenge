using System;
using System.Collections.Generic;
using UnityEngine;

namespace Layout
{
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

        public Hex Add(Hex b)
        {
            return new Hex(q + b.q, r + b.r, s + b.s);
        }

        public Hex Subtract(Hex b)
        {
            return new Hex(q - b.q, r - b.r, s - b.s);
        }


        public Hex Scale(int k)
        {
            return new Hex(q * k, r * k, s * k);
        }


        public Hex RotateLeft()
        {
            return new Hex(-s, -q, -r);
        }


        public Hex RotateRight()
        {
            return new Hex(-r, -s, -q);
        }

        static public List<Hex> directions = new List<Hex> { new Hex(1, 0, -1), new Hex(1, -1, 0), new Hex(0, -1, 1), new Hex(-1, 0, 1), new Hex(-1, 1, 0), new Hex(0, 1, -1) };

        static public Hex Direction(int direction)
        {
            return Hex.directions[direction];
        }

        public List<Hex> Neighbors()
        {
            var neighbors = new List<Hex>();
            for (int i = 0; i < directions.Count; i++)
            {
                neighbors.Add(Neighbor(i));
            }
            return neighbors;
        }

        public Hex Neighbor(int direction)
        {
            return Add(Hex.Direction(direction));
        }

        static public List<Hex> diagonals = new List<Hex> { new Hex(2, -1, -1), new Hex(1, -2, 1), new Hex(-1, -1, 2), new Hex(-2, 1, 1), new Hex(-1, 2, -1), new Hex(1, 1, -2) };

        public Hex DiagonalNeighbor(int direction)
        {
            return Add(Hex.diagonals[direction]);
        }

        public int Length()
        {
            return (int)((Math.Abs(q) + Math.Abs(r) + Math.Abs(s)) / 2);
        }


        public int Distance(Hex b)
        {
            return Subtract(b).Length();
        }

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

        public override int GetHashCode()
        {
            return new Vector3(q, r, s).GetHashCode();
        }
    }
}
