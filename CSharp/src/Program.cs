using PathfindingCSharp;
using System.IO;

namespace StudentMeetup2018
{
    public enum ErrCode
    {
        Success = 0,
        InvalidStart,
        InvalidEnd,
        BlockedStart,
        BlockedEnd,
        InvalidPath,
        InvalidArgCount
    }

    class Program
    {
        private static readonly int kExpectedArgCount = 5;
        static ErrCode ParseArgs(string[] args, out string[] mapData, out Vector2 startPoint, out Vector2 endPoint)
        {
            string[] fileLines;
            startPoint = new Vector2();
            endPoint = new Vector2();
            mapData = null;

            if (args.Length != kExpectedArgCount)
                return ErrCode.InvalidArgCount;

            try
            {
                fileLines = File.ReadAllLines(args[0]);
            }
            catch
            {
                return ErrCode.InvalidPath;
            }

            if (!int.TryParse(args[1], out startPoint.x))
                return ErrCode.InvalidStart;

            if (!int.TryParse(args[2], out startPoint.y))
                return ErrCode.InvalidStart;

            if (!int.TryParse(args[3], out endPoint.x))
                return ErrCode.InvalidEnd;

            if (!int.TryParse(args[4], out endPoint.y))
                return ErrCode.InvalidEnd;

            mapData = fileLines;

            return ErrCode.Success;
        }

        static void Main(string[] args)
        {
            Vector2 startPoint;
            Vector2 endPoint;
            string[] mapData;

			ErrCode code = ParseArgs(args, out mapData, out startPoint, out endPoint);
            if (ErrCode.Success != code)
                return code;

            MapSolution map = new MapSolution(mapData, startPoint, endPoint);

            map.ComputePath();
            map.DisplayMap();
			
			return ErrCode.Success;
        }
    }
}
