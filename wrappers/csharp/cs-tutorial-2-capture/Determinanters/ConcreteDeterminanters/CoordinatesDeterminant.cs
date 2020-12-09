using Intel.RealSense;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Determinanters
{
    public struct Vector3Coordinate
    {
        private float x;
        private float y;
        private float z;

        public Vector3Coordinate(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public float X
        {
            get
            {
                return x;
            }
        }

        public float Y
        {
            get
            {
                return y;
            }
        }

        public float Z
        {
            get
            {
                return z;
            }
        }

        public override string ToString()
        {
            return $"x: {x}, y: {y}, z: {z}";
        }
    }

    public class CoordinatesDeterminant : BaseDeterminant<Vector3Coordinate>
    {
        private Intrinsics depthCamMetrix;
        public CoordinatesDeterminant(Intrinsics depthCamMetrix, params ISelectFilter<Vector3Coordinate>[] filters) : base(filters)
        {
            this.depthCamMetrix = depthCamMetrix;
        }


        public Intrinsics DepthCamMetrix
        {
            get
            {
                return depthCamMetrix;
            }
            set
            {
                depthCamMetrix = value;
            }
        }

        protected override IEnumerable<Vector3Coordinate> GetSourceData(DepthFrame frame, SelectArea selectArea)
        {
            for (int i = selectArea.LocationX; i < selectArea.LocationX + selectArea.Width; i++)
            {
                for (int j = selectArea.LocationY; j < selectArea.LocationY + selectArea.Heigh; j++)
                {
                    yield return GetPixelCoordinate(frame, i, j);
                }
            }
        }

        private Vector3Coordinate GetPixelCoordinate(DepthFrame frame, int x, int y)
        {
            var distanceToPoint = frame.GetDistance(x, y);
            float newX = (x - depthCamMetrix.ppx) / depthCamMetrix.fx * distanceToPoint;
            float newY = (y - depthCamMetrix.ppy) / depthCamMetrix.fy * distanceToPoint;

            return new Vector3Coordinate(newX, newY, distanceToPoint);
        }

        protected override Vector3Coordinate FinalCalculation(IEnumerable<Vector3Coordinate> data)
        {
            var pointsCount = data.Count();
            var x = data.Sum(coord => coord.X) / pointsCount;
            var y = data.Sum(coord => coord.Y) / pointsCount;
            var z = data.Sum(coord => coord.Z) / pointsCount;

            return new Vector3Coordinate(x, y, z);
        }
    }
}
