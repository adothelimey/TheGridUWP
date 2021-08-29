using System;
using System.Collections.Generic;
using System.Text;

namespace TheGriduwp
{
    public class GridCellDistances
    {
        public Dictionary<GridCell, int> GridCellDistanceLookup { get; private set; } //contains distance of all cells from rootCell

        public GridCellDistances(GridCell rootCell)
        {
            GridCellDistanceLookup = new Dictionary<GridCell, int>();
            GridCellDistanceLookup.Add(rootCell, 0); //rootcell has 0 distance from itself
        }

        public int GetDistanceOf(GridCell cell)
        {
            if (GridCellDistanceLookup.TryGetValue(cell, out int distance)) { }

            return distance;
        }

        public void SetDistanceOf(GridCell cell, int distance)
        {
            GridCellDistanceLookup.Add(cell, distance);
        }
    }
}
