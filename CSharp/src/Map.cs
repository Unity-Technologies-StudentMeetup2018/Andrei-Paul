using System;
using System.Text;

namespace PathfindingCSharp
{
    public struct Vector2 : IEquatable<Vector2>
    {
        public int x, y;

        public bool Equals(Vector2 other)
        {
            return x == other.x && y == other.y;
        }
    }
    public class Map
    {
        public const char emptyChar = '.';
        public const char blockedChar = '#';
        public const char invalidChar = '?';
        public const char pathChar = '@';

        private char[,] mapPositions;
        Vector2 startPosition;
        Vector2 goalPosition;

        char[] markerFace = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
        int[,] mapWork;
        double theta;

        int[] OffsetX = { -1, -1, -1, 0, 0, 1, 1, 1 };
        int[] OffsetY = { -1, 0, 1, -1, 1, -1, 0, 1 };
        double[] OffsetAngle = { 0, 0, 0, 0, 0, 0, 0, 0 };
        int OffsetSize = 8;

        public Map(string[] mapData, Vector2 startPos, Vector2 endPos)
        {
            if (startPos.x < 0 || startPos.x >= mapData.Length || startPos.y < 0 || startPos.y >= mapData.Length)
                throw new Exception("Start position is out of bounds, provided coordinates were: " + startPos.x + "," + startPos.y);

            if (endPos.x < 0 || endPos.x >= mapData.Length || endPos.y < 0 || endPos.y >= mapData.Length)
                throw new Exception("End position is out of bounds, provided coordinates were: " + endPos.x + "," + endPos.y);

            if (mapData[startPos.y][startPos.x] == blockedChar)
                throw new Exception("Start position is invalid, provided coordinates were: " + startPos.x + "," + startPos.y);

            if (mapData[endPos.y][endPos.x] == blockedChar)
                throw new Exception("End position is invalid, provided coordinates were: " + endPos.x + "," + endPos.y);

            startPosition = startPos;
            goalPosition = endPos;

            mapPositions = new char[mapData.Length, mapData.Length];

            for (int i = 0; i < mapData.Length; ++i)
            {
                for (int j = 0; j < mapData[i].Length; ++j)
                {
                    int index = (i * mapData.Length) + j;

                    if (startPos.x == j && startPos.y == i)
                        mapPositions[i, j] = pathChar;
                    else if (endPos.x == j && endPos.y == i)
                        mapPositions[i, j] = pathChar;
                    else
                    {
                        switch (mapData[i][j])
                        {
                            case emptyChar:
                                mapPositions[i, j] = emptyChar;
                                break;

                            case blockedChar:
                                mapPositions[i, j] = blockedChar;
                                break;

                            default:
                                mapPositions[i, j] = invalidChar;
                                break;
                        }
                    }

                }
            }
        }

        private bool IsBlocked(Vector2 position)
        {
            return mapPositions[position.y, position.x] == blockedChar;
        }

        public void DisplayMap()
        {
            Console.Clear();
            Console.WriteLine(SolutionToString());
        }

        public string SolutionToString()
        {
            StringBuilder sb = new StringBuilder(mapPositions.Length + mapPositions.GetLength(0)); //add extra space for the new lines

            for (int i = 0; i < mapPositions.GetLength(0); ++i)
            {
                for (int j = 0; j < mapPositions.GetLength(1); ++j)
                {
                    sb.Append(mapPositions[i, j]);
                }
                sb.Append('\n');
            }

            return sb.ToString();
        }

        public void fartInGeneralDirection()
        {
            for (int Index = 0; Index < OffsetSize; Index++)
            {
                double slope = (double)(OffsetY[Index]) / (double)(OffsetX[Index]);
                OffsetAngle[Index] = Math.Atan(slope);
            }
        }

        public int[,,] fartInParticularDirection(int x, int y)
        {
            double slope = (double)(goalPosition.y - y) / (double)(goalPosition.x - x);
            double theta = Math.Atan(slope);

            int StartOffset = 0;
            for (int Index = 0; Index < OffsetSize; Index++)
            {
//                if(OffsetAngle - )
            }

            return null;
        }

        public bool lazyOmniWalk(int x, int y)
        {
            int marker = mapWork[y, x] + 1;

            mapWork[y, x] = -mapWork[y, x];
            if (x == goalPosition.x && y == goalPosition.y)
            {
                Console.WriteLine("\nAfter 'only' " + marker + " steps!\n");
                return true;
            }

            fartInParticularDirection(x, y);
            for (int Index = 0; Index < OffsetSize; Index++)
            {
                int X = x + OffsetX[Index];
                int Y = y + OffsetY[Index];
                if (X < 0 || Y < 0 || X > 31 || Y > 31)
                {
                    continue;
                }

                if (mapWork[Y, X] == 0)
                {
                    mapWork[Y, X] = marker;
                }
            }

            for (int Index = 0; Index < OffsetSize; Index++)
            {
                int X = x + OffsetX[Index];
                int Y = y + OffsetY[Index];
                if (X < 0 || Y < 0 || X > 31 || Y > 31)
                {
                    continue;
                }

                if (mapWork[Y, X] == marker)
                {
                    bool stop = lazyOmniWalk(X, Y);

                    if (stop == true)
                    {
                        return true;
                    }
                    else
                    {
                        mapWork[Y, X] = int.MinValue;
                    }
                }
            }

            return false;
        }

        public bool ComputePath()
        {
            fartInGeneralDirection();
            mapWork = new int[32, 32];

            for (int X = 0; X < 32; X++)
            {
                for (int Y = 0; Y < 32; Y++)
                {
                    if (mapPositions[Y, X] == '#')
                    {
                        mapWork[Y, X] = int.MaxValue;
                    }
                    else
                    {
                        mapWork[Y, X] = 0;
                    }
                }
            }

            mapWork[startPosition.y, startPosition.x] = 1;
            bool result = lazyOmniWalk(startPosition.x, startPosition.y);

            for (int X = 0; X < 32; X++)
            {
                for (int Y = 0; Y < 32; Y++)
                {
                    if (mapWork[Y, X] != int.MaxValue && mapWork[Y, X] != int.MinValue && mapPositions[Y, X] != '@' && mapWork[Y, X] != 0)
                    {
                        if (mapWork[Y, X] < 0)
                        {
                            mapWork[Y, X] = -mapWork[Y, X];
                        }

                        int markerIndex = mapWork[Y, X] % markerFace.Length;
                        mapPositions[Y, X] = ' ';// markerFace[markerIndex];
                    }
                }
            }

            DisplayMap();

            return result;
        }
    };
}
