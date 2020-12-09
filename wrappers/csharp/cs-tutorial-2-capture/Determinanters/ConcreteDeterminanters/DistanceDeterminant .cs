using Intel.RealSense;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Determinanters
{
    public class DistanceDeterminant : BaseDeterminant<float>
    {
        public DistanceDeterminant(params ISelectFilter<float>[] filters) : base(filters)
        { }

        protected override IEnumerable<float> GetSourceData(DepthFrame frame, SelectArea selectArea)
        {
            for (int i = selectArea.LocationX; i < selectArea.LocationX + selectArea.Width; i++)
            {
                for (int j = selectArea.LocationY; j < selectArea.LocationY + selectArea.Heigh; j++)
                {
                    yield return frame.GetDistance(i, j);
                }
            }
        }

        protected override float FinalCalculation(IEnumerable<float> data)
        {
            return data.Sum() / data.Count();
        }
    }
}
