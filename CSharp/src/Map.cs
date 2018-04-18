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
        public const char startChar = 's';
        public const char endChar = 'e';
        public const char invalidChar = '?';
        public const char pathChar = '@';

        private char[,] mapPositions;
        StringBuilder sb;
        Vector2 startPosition;
        Vector2 goalPosition;

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
                        mapPositions[i, j] = startChar;
                    else if (endPos.x == j && endPos.y == i)
                        mapPositions[i, j] = endChar;
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
            sb = new StringBuilder((mapPositions.Length * mapPositions.Length) + mapPositions.Length); //plus 32 chars for newlines
        }

        private bool IsBlocked(Vector2 position)
        {
            return mapPositions[position.y, position.x] == blockedChar;
        }

        private void UpdateStringRepresentation()
        {
            sb.Clear();

            for (int i = 0; i < mapPositions.GetLength(0); ++i)
            {
                for (int j = 0; j < mapPositions.GetLength(1); ++j)
                {
                    sb.Append(mapPositions[i, j]);
                }
                sb.Append('\n');
            }
        }

        public void DisplayMap()
        {
            Console.Clear();
            UpdateStringRepresentation();
            Console.WriteLine(sb);
        }

        public string SolutionToString()
        {
            UpdateStringRepresentation();
            return sb.ToString();
        }

        public void ComputePath()
        {           
            //TODO: Implement solution here
        }
    };
}
