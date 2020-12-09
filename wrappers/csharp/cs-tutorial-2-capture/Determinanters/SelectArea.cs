using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Determinanters
{
    public struct SelectArea
    {
        private int width;
        private int heigh;
        private int locationX;
        private int locationY;

        public SelectArea(int locationX, int locationY, int width, int heigh)
        {
            this.width = width;
            this.heigh = heigh;
            this.locationX = locationX;
            this.locationY = locationY;
        }

        public SelectArea(int width, int height)
        {
            this.width = width;
            this.heigh = height;
            locationX = 0;
            locationY = 0;
        }

        public int Width
        {
            get
            {
                return width;
            }
        }

        public int Heigh
        {
            get
            {
                return heigh;
            }
        }

        public int LocationX
        {
            get
            {
                return locationX;
            }
        }

        public int LocationY
        {
            get
            {
                return locationY;
            }
        }

        public bool ContainsOther(SelectArea selectArea)
        {
            return IsLeftHighCornerInside(locationX, locationY, selectArea.locationX, selectArea.locationY) &&
                   IsRightLowCornerInside(locationX + width, locationY + heigh, selectArea.locationX + selectArea.width, selectArea.locationY + selectArea.heigh);
        }

        private bool IsLeftHighCornerInside(int selfX, int selfY, int otherX, int otherY)
        {
            return selfX <= otherX && selfY <= otherX;
        }

        private bool IsRightLowCornerInside(int selfX, int selfY, int otherX, int otherY)
        {
            return selfX >= otherX && selfY >= otherX;
        }
    }
}
