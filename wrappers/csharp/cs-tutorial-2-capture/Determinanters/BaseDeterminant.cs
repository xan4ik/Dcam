using Intel.RealSense;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Determinanters
{
    public abstract class BaseDeterminant<T>
    {
        private List<ISelectFilter<T>> filters;
        protected BaseDeterminant(params ISelectFilter<T>[] filters)
        {
            this.filters = new List<ISelectFilter<T>>();
            foreach (ISelectFilter<T> item in filters)
            {
                this.filters.Add(item);
            }
        }

        public void AddFilter(ISelectFilter<T> filter)
        {
            if (filter != null)
            {
                filters.Add(filter);
            }
            else throw new NullReferenceException("Trying to add unexisted filter");
        }

        public void RemoveFilter(ISelectFilter<T> filter)
        {
            if (filters.Contains(filter))
            {
                filters.Remove(filter);
            }
            else throw new ArgumentException("Filter that you trying to remove didn't add to filters");
        }

        public T CalculateArea(DepthFrame depthFrame, SelectArea selectArea)
        {
            if (IsSelectAreaInFrameRange(depthFrame, selectArea))
            {
                IEnumerable<T> source = GetSourceData(depthFrame, selectArea);
                source = ApplyFilters(source);
                return FinalCalculation(source);
            }
            else throw new OutOfBoundsException();

        }

        protected abstract IEnumerable<T> GetSourceData(DepthFrame frame, SelectArea selectArea);
        protected abstract T FinalCalculation(IEnumerable<T> data);

        private bool IsSelectAreaInFrameRange(VideoFrame frame, SelectArea selectArea)
        {
            SelectArea frameArea = new SelectArea(frame.Width, frame.Height);
            return frameArea.ContainsOther(selectArea);
        }

        private IEnumerable<T> ApplyFilters(IEnumerable<T> sourse)
        {
            try
            {
                var current = sourse;
                foreach (ISelectFilter<T> item in filters)
                {
                    current = item.Apply(current);
                }
                return current;
            }
            catch (Exception exs)
            {
                throw new ApplyFilterException(exs);
            }
        }

    }
}
