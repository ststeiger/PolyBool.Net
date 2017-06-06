﻿using System;
using Polybool.Net.Objects;

namespace Polybool.Net.Logic
{
    public static class PointUtils
    {
        public static bool PointAboveOrOnLine(Point point, Point left, Point right)
        {
            return (right.X - left.X) * (point.Y - left.Y) - (right.Y - left.Y) * (point.X - left.X) >= -Epsilon.Eps;
        }

        public static bool PointBetween(Point point, Point left, Point right)
        {
            // p must be collinear with left->right
            // returns false if p == left, p == right, or left == right
            decimal d_py_ly = point.Y - left.Y;
            decimal d_rx_lx = right.X - left.X;
            decimal d_px_lx = point.X - left.X;
            decimal d_ry_ly = right.Y - left.Y;

            decimal dot = d_px_lx * d_rx_lx + d_py_ly * d_ry_ly;

            if (dot < Epsilon.Eps)
            {
                return false;
            }

            decimal sqlen = d_rx_lx * d_rx_lx + d_ry_ly * d_ry_ly;
            if (dot - sqlen > -Epsilon.Eps)
            {
                return false;
            }

            return true;
        }

        public static bool PointsSameX(Point point1, Point point2)
        {
            return Math.Abs(point1.X - point2.X) < Epsilon.Eps;
        }

        public static bool PointsSameY(Point point1, Point point2)
        {
            return Math.Abs(point1.Y - point2.Y) < Epsilon.Eps;
        }

        public static bool PointsSame(Point point1, Point point2)
        {
            return PointsSameX(point1, point2) && PointsSameY(point1, point2);
        }

        public static int PointsCompare(Point point1, Point point2)
        {
            if (PointsSameX(point1, point2))
            {
                return PointsSameY(point1, point2) ? 0 : (point1.Y < point2.Y ? -1 : 1);
            }
            return point1.X < point2.X ? -1 : 1;
        }

        public static bool PointsCollinear(Point point1, Point point2, Point point3)
        {
            return Math.Abs((point1.X - point2.X) * (point1.Y - point2.Y) - (point2.X - point3.X) * (point2.Y - point3.Y)) < Epsilon.Eps;
        }

        public static IntersectionPoint LinesIntersect(Point a0, Point a1, Point b0, Point b1)
        {
            decimal adx = a1.X - a0.X;
            decimal ady = a1.Y - a0.Y;
            decimal bdx = b1.X - b0.X;
            decimal bdy = b1.Y - b0.Y;

            decimal axb = adx * bdy - ady * bdx;

            if (Math.Abs(axb) < Epsilon.Eps)
            {
                return null;
            }

            decimal dx = a0.X - b0.X;
            decimal dy = a0.Y - b0.Y;

            decimal a = (bdx * dy - bdy * dx) / axb;
            decimal b = (adx * dy - ady * dx) / axb;

            return new IntersectionPoint(CalcAlongUsingValue(a), CalcAlongUsingValue(b), new Point(a0.X + a * adx, a0.Y + a * ady));
        }

        public static int CalcAlongUsingValue(decimal value)
        {
            if (value <= -Epsilon.Eps)
            {
                return -2;
            }
            else if (value < Epsilon.Eps)
            {
                return -1;
            }
            else if (value - 1 <= -Epsilon.Eps)
            {
                return 0;
            }
            else if (value - 1 < Epsilon.Eps)
            {
                return 1;
            }
            else
            {
                return 2;
            }
        }
    }
}