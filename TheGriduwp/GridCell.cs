using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace TheGriduwp
{
    public class GridCell
    {
        public int X_Pos { get; private set; }
        public int Y_Pos { get; private set; }      
        public int TileIndex { get; set; }
        public Dictionary<Direction, GridCell> Neighbours { get; set; } = new Dictionary<Direction, GridCell>();
        public Dictionary<Direction, GridCell> LinkedNeighbours { get; set; } = new Dictionary<Direction, GridCell>();

        public GridCell(int x, int y)
        {
            X_Pos = x;
            Y_Pos = y;

        }

        public bool HasLinkedNeighbourInDirection(Direction direction)
        {
            var isLinked = false;

            if (Neighbours.Any())
            {
                if (LinkedNeighbours.ContainsKey(direction))
                    isLinked = true;
            }

            return isLinked;
        }
        



        public void Link(GridCell cell, Direction direction, bool bidirectional = true)
        {
            Direction returnDirection = Direction.None;

            switch (direction)
            {
                case Direction.North:
                    returnDirection = Direction.South;
                    break;
                case Direction.East:
                    returnDirection = Direction.West;
                    break;
                case Direction.South:
                    returnDirection = Direction.North;
                    break;
                case Direction.West:
                    returnDirection = Direction.East;
                    break;
                case Direction.None:
                case Direction.Up:
                case Direction.Down:
                default:
                    break;
            }

            PerformLink(cell, direction, returnDirection, bidirectional);




        }
        private void PerformLink(GridCell cell, Direction direction, Direction sourceDirection, bool bidirectional = true)
        {
            this.LinkedNeighbours.Add(direction, cell);

            if (bidirectional)
                cell.LinkedNeighbours.Add(sourceDirection, this);
        }

        public void Unlink(GridCell cell, Direction direction, bool bidirectional = true)
        {
            Direction returnDirection = Direction.None;

            switch (direction)
            {
                case Direction.North:
                    returnDirection = Direction.South;
                    break;
                case Direction.East:
                    returnDirection = Direction.West;
                    break;
                case Direction.South:
                    returnDirection = Direction.North;
                    break;
                case Direction.West:
                    returnDirection = Direction.East;
                    break;
                case Direction.None:
                case Direction.Up:
                case Direction.Down:
                default:
                    break;
            }

            PerformUnlink(cell, direction, returnDirection, bidirectional);
        }
        private void PerformUnlink(GridCell cell, Direction direction, Direction sourceDirection, bool bidirectional = true)
        {
            this.LinkedNeighbours.Remove(direction);
            if (bidirectional)
                cell.LinkedNeighbours.Remove(sourceDirection);
        }

        public GridCellDistances GetDistances()
        {
            var distances = new GridCellDistances(this);

            var frontier = new List<GridCell> { this };

            while (frontier.Any())
            {
                var newFrontier = new List<GridCell>();

                foreach (var cell in frontier)
                {
                    foreach (var linkedCell in cell.LinkedNeighbours.Values)
                    {
                        if (!distances.GridCellDistanceLookup.ContainsKey(linkedCell))
                        {
                            if (distances.GridCellDistanceLookup.TryGetValue(cell, out int distance))
                            {
                                distances.SetDistanceOf(linkedCell, distance + 1);
                                newFrontier.Add(linkedCell);
                            }
                        }
                    }
                }

                frontier = newFrontier;
            }

            return distances;
        }

        

    }
}
