﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biometria.Models
{
    public class Minutiae
    {
        private int _x;
        private int _y;
        private int _directionX;
        private int _directionY;
        private float _distanceFromZeroPoint;
        private float _orientationAngle;
        private int _crossingNumber;

        public Minutiae()
        {
            _x = 0;
            _y = 0;
            _orientationAngle = 0.0f;
            _crossingNumber = 0;
        }

        public Minutiae(int x, int y, int crossingNumber)
        {
            _x = x;
            _y = y;
            _orientationAngle = 0.0f;
            _crossingNumber = crossingNumber;
        }

        public int X { get => _x; set => _x = value; }

        public int Y { get => _y; set => _y = value; }

        public int DirectionX { get => _directionX; set => _directionX = value; }

        public int DirectionY { get => _directionY; set => _directionY = value; }

        public float DistanceFromZeroPoint { get => _distanceFromZeroPoint; set => _distanceFromZeroPoint = value; }

        public float OrientationAngle { get => _orientationAngle; set => _orientationAngle = value; }

        public int CrossingNumber { get => _crossingNumber; set => _crossingNumber = value; }

        public static float Distance(Minutiae m1, Minutiae m2)
        {
            return (float)Math.Sqrt(Math.Pow(m1.X - m2.X, 2) + Math.Pow(m1.Y - m2.Y, 2));
        }
    }
}
