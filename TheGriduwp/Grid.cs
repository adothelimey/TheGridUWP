using System;
using System.Collections.Generic;
using System.Text;

namespace TheGriduwp
{
    public class Grid
    {
        public GridCell[,] GridCells { get; private set; }

        private int gridWidth;
        private int gridHeight;

        public Grid(int width, int height)
        {
            gridWidth = width;
            gridHeight = height;

            GridCells = new GridCell[gridWidth, gridHeight];

            ResetGrid();
        }

        public void ResetGrid()
        {
            for (int x = 0; x < gridWidth; x++)
            {
                for (int y = 0; y < gridHeight; y++)
                {
                    var gridcell = new GridCell(x, y);
                    GridCells[x, y] = gridcell;
                }
            }

            setGridCellNeighbours();
        }

        private void setGridCellNeighbours()
        {
            for (int x = 0; x < gridWidth; x++)
            {
                for (int y = 0; y < gridHeight; y++)
                {
                    GridCell northNeighbour = null;
                    GridCell eastNeighbour = null;
                    GridCell southNeighbour = null;
                    GridCell westNeighbour = null;

                    if (y > 0)
                        northNeighbour = GridCells[x, y - 1];

                    if (y < gridHeight - 1)
                        southNeighbour = GridCells[x, y + 1];

                    if (x < gridWidth - 1)
                        eastNeighbour = GridCells[x + 1, y];

                    if (x > 0)
                        westNeighbour = GridCells[x - 1, y];

                    var gridCell = GridCells[x, y];
                    if (southNeighbour != null)
                        gridCell.Neighbours.Add(Direction.South, southNeighbour);
                    if (eastNeighbour != null)
                        gridCell.Neighbours.Add(Direction.East, eastNeighbour);
                    if (northNeighbour != null)
                        gridCell.Neighbours.Add(Direction.North, northNeighbour);
                    if (westNeighbour != null)
                        gridCell.Neighbours.Add(Direction.West, westNeighbour);
                }
            }
        }

        public void Link(GridCell sourceCell, Direction direction, bool bidirectional = true)
        {
            Direction returnDirection = Direction.None;

            GridCell targetCell = null;

            switch (direction)
            {
                case Direction.North:
                    returnDirection = Direction.South;
                    targetCell = GridCells[sourceCell.X_Pos, sourceCell.Y_Pos - 1];
                    break;
                case Direction.East:
                    returnDirection = Direction.West;
                    targetCell = GridCells[sourceCell.X_Pos + 1, sourceCell.Y_Pos];
                    break;
                case Direction.South:
                    returnDirection = Direction.North;
                    targetCell = GridCells[sourceCell.X_Pos, sourceCell.Y_Pos + 1];
                    break;
                case Direction.West:
                    returnDirection = Direction.East;
                    targetCell = GridCells[sourceCell.X_Pos -1 , sourceCell.Y_Pos];
                    break;
                case Direction.None:
                case Direction.Up:
                case Direction.Down:
                default:
                    break;
            }

            PerformLink(sourceCell, targetCell, direction, returnDirection, bidirectional);
        }

        private void PerformLink(GridCell sourceCell, GridCell targetCell, Direction direction, Direction sourceDirection, bool bidirectional = true)
        {
            sourceCell.LinkedNeighbours.Add(direction, targetCell);

            if (bidirectional)
                targetCell.LinkedNeighbours.Add(sourceDirection, sourceCell);
        }
    }
}
