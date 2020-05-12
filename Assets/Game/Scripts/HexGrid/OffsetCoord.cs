using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Layout
{
    /// <summary>
    /// Stores offset coordinates for easier drawing of hexagon layouts
    /// </summary>
    /// See https://www.redblobgames.com/grids/hexagons
    public class OffsetCoord
    {
        public OffsetCoord(int row, int col)
        {
            this.row = row;
            this.col = col;
        }
        public readonly int col;
        public readonly int row;
        static public int EVEN = 1;
        static public int ODD = -1;

        /// <summary>
        /// Get flat hexagon offset coordinates with given offset and cube coordinates
        /// </summary>
        /// <param name="offset">1 for even, -1 for odd layouts</param>
        /// <param name="h">Hex with cube coordinates</param>
        /// <returns>Returns the resulting OffsetCoord</returns>
        static public OffsetCoord QoffsetFromCube(int offset, Hex h)
        {
            int col = h.q;
            int row = h.r + (int)((h.q + offset * (h.q & 1)) / 2);
            if (offset != OffsetCoord.EVEN && offset != OffsetCoord.ODD)
            {
                throw new ArgumentException("offset must be EVEN (+1) or ODD (-1)");
            }
            return new OffsetCoord(col, row);
        }

        /// <summary>
        /// Get flat hexagon cube coordinates with given offset and cube coordinates
        /// </summary>
        /// <param name="offset">1 for even, -1 for odd layouts</param>
        /// <param name="h">Offset coordinates</param>
        /// <returns>Returns the resulting Hex with cube coordinates</returns>
        static public Hex QoffsetToCube(int offset, OffsetCoord h)
        {
            int q = h.col;
            int r = h.row - (int)((h.col + offset * (h.col & 1)) / 2);
            int s = -q - r;
            if (offset != OffsetCoord.EVEN && offset != OffsetCoord.ODD)
            {
                throw new ArgumentException("offset must be EVEN (+1) or ODD (-1)");
            }
            return new Hex(q, r, s);
        }
        
        /// <summary>
        /// Get pointy hexagon offset coordinates with given offset and cube coordinates
        /// </summary>
        /// <param name="offset">1 for even, -1 for odd layouts</param>
        /// <param name="h">Hex with cube coordinates</param>
        /// <returns>Returns the resulting OffsetCoord</returns>
        static public OffsetCoord RoffsetFromCube(int offset, Hex h)
        {
            int col = h.q + (int)((h.r + offset * (h.r & 1)) / 2);
            int row = h.r;
            if (offset != OffsetCoord.EVEN && offset != OffsetCoord.ODD)
            {
                throw new ArgumentException("offset must be EVEN (+1) or ODD (-1)");
            }
            return new OffsetCoord(col, row);
        }

        /// <summary>
        /// Get pointy hexagon offset coordinates with given offset and cube coordinates
        /// </summary>
        /// <param name="offset">1 for even, -1 for odd layouts</param>
        /// <param name="h">Hex with cube coordinates</param>
        /// <returns>Returns the resulting OffsetCoord</returns>
        static public Hex RoffsetToCube(int offset, OffsetCoord h)
        {
            int q = h.col - (int)((h.row + offset * (h.row & 1)) / 2);
            int r = h.row;
            int s = -q - r;
            if (offset != OffsetCoord.EVEN && offset != OffsetCoord.ODD)
            {
                throw new ArgumentException("offset must be EVEN (+1) or ODD (-1)");
            }
            return new Hex(q, r, s);
        }

    }
}
