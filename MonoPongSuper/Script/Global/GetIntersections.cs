using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoPongSuper.Script.Global
{
    public static class GetIntersections
    {
        public static bool DoesLineIntersectLine(Vector2 lineAStart, Vector2 lineAEnd, Vector2 lineBStart, Vector2 lineBEnd, out Vector2 intersectionPoint)
        {
            intersectionPoint = Vector2.Zero;
            Vector2 B = lineAEnd - lineAStart;
            Vector2 D = lineBEnd - lineBStart;
            float bDotPerp = B.X * D.Y - B.Y * D.X;
            if (bDotPerp == 0)
                return false;
            Vector2 C = lineBStart - lineAStart;
            float T = (C.X * D.Y - C.Y * D.X) / bDotPerp;
            if (T < 0 || T > 1)
                return false;
            float U = (C.X * B.Y - C.Y * B.X) / bDotPerp;
            if (U < 0 || U > 1)
                return false;
            intersectionPoint = lineAStart + T * B;
            return true;
        }

        public static bool DoesLineIntersectLine(Vector2 lineAStart, Vector2 lineAEnd, Vector2 lineBStart, Vector2 lineBEnd)
        {
            Vector2 B = lineAEnd - lineAStart;
            Vector2 D = lineBEnd - lineBStart;
            float bDotPerp = B.X * D.Y - B.Y * D.X;
            if (bDotPerp == 0)
                return false;
            Vector2 C = lineBStart - lineAStart;
            float T = (C.X * D.Y - C.Y * D.X) / bDotPerp;
            if (T < 0 || T > 1)
                return false;
            float U = (C.X * B.Y - C.Y * B.X) / bDotPerp;
            if (U < 0 || U > 1)
                return false;
            return true;
        }

        public static bool DoesLineIntersectRectangle(Vector2 lineStart, Vector2 lineEnd, Rectangle rect, out Vector2 intersectionPoint)
        {
            // left
            if (DoesLineIntersectLine(lineStart, lineEnd, new Vector2(rect.X, rect.Y), new Vector2(rect.X, rect.Y + rect.Height), out intersectionPoint))
                return true;
            // right
            if (DoesLineIntersectLine(lineStart, lineEnd, new Vector2(rect.X + rect.Width, rect.Y), new Vector2(rect.X + rect.Width, rect.Y + rect.Height), out intersectionPoint))
                return true;
            // upper
            if (DoesLineIntersectLine(lineStart, lineEnd, new Vector2(rect.X, rect.Y), new Vector2(rect.X + rect.Width, rect.Y + rect.Height), out intersectionPoint))
                return true;
            // lower
            if (DoesLineIntersectLine(lineStart, lineEnd, new Vector2(rect.X, rect.Y + rect.Height), new Vector2(rect.X + rect.Width, rect.Y + rect.Height), out intersectionPoint))
                return true;
            return false;
        }

        public static bool DoesLineIntersectRectangle(Vector2 lineStart, Vector2 lineEnd, Rectangle rect)
        {
            // left
            if (DoesLineIntersectLine(lineStart, lineEnd, new Vector2(rect.X, rect.Y), new Vector2(rect.X, rect.Y + rect.Height)))
                return true;
            // right
            if (DoesLineIntersectLine(lineStart, lineEnd, new Vector2(rect.X + rect.Width, rect.Y), new Vector2(rect.X + rect.Width, rect.Y + rect.Height)))
                return true;
            // upper
            if (DoesLineIntersectLine(lineStart, lineEnd, new Vector2(rect.X, rect.Y), new Vector2(rect.X + rect.Width, rect.Y + rect.Height)))
                return true;
            // lower
            if (DoesLineIntersectLine(lineStart, lineEnd, new Vector2(rect.X, rect.Y + rect.Height), new Vector2(rect.X + rect.Width, rect.Y + rect.Height)))
                return true;
            return false;
        }

        public static bool DoesLineIntersectRectangleX(Vector2 lineStart, Vector2 lineEnd, Rectangle rect, out float intersectionPointX)
        {
            Vector2 intersectionPoint;
            // left
            if (DoesLineIntersectLine(lineStart, lineEnd, new Vector2(rect.X, rect.Y), new Vector2(rect.X, rect.Y + rect.Height), out intersectionPoint))
            {
                intersectionPointX = intersectionPoint.X;
                Debug.WriteLine($"leftside colls: {intersectionPointX}"); // for debugging
                return true;
            }
            // right
            if (DoesLineIntersectLine(lineStart, lineEnd, new Vector2(rect.X + rect.Width, rect.Y), new Vector2(rect.X + rect.Width, rect.Y + rect.Height), out intersectionPoint))
            {
                intersectionPointX = intersectionPoint.X;
                Debug.WriteLine($"rightside colls: {intersectionPointX}"); // for debugging
                return true;
            }
            intersectionPointX = 0f;
            return false;
        }

        public static bool DoesLineIntersectRectangleX(Vector2 lineStart, Vector2 lineEnd, Rectangle rect)
        {
            // left
            if (DoesLineIntersectLine(lineStart, lineEnd, new Vector2(rect.X, rect.Y), new Vector2(rect.X, rect.Y + rect.Height)))
                return true;
            // right
            if (DoesLineIntersectLine(lineStart, lineEnd, new Vector2(rect.X + rect.Width, rect.Y), new Vector2(rect.X + rect.Width, rect.Y + rect.Height)))
                return true;
            return false;
        }

        public static bool DoesLineIntersectRectangleLeft(Vector2 lineStart, Vector2 lineEnd, Rectangle rect, out float intersectionPointX)
        {
            Vector2 intersectionPoint;
            if (DoesLineIntersectLine(lineStart, lineEnd, new Vector2(rect.X, rect.Y), new Vector2(rect.X, rect.Y + rect.Height), out intersectionPoint))
            {
                intersectionPointX = intersectionPoint.X;
                Debug.WriteLine($"leftside colls: {intersectionPointX}"); // for debugging
                return true;
            }
            intersectionPointX = 0f;
            return false;
        }

        public static bool DoesLineIntersectRectangleLeft(Vector2 lineStart, Vector2 lineEnd, Rectangle rect)
        {
            if (DoesLineIntersectLine(lineStart, lineEnd, new Vector2(rect.X, rect.Y), new Vector2(rect.X, rect.Y + rect.Height)))
                return true;
            return false;
        }

        public static bool DoesLineIntersectRectangleRight(Vector2 lineStart, Vector2 lineEnd, Rectangle rect, out float intersectionPointX)
        {
            Vector2 intersectionPoint;
            if (DoesLineIntersectLine(lineStart, lineEnd, new Vector2(rect.X + rect.Width, rect.Y), new Vector2(rect.X + rect.Width, rect.Y + rect.Height), out intersectionPoint))
            {
                intersectionPointX = intersectionPoint.X;
                Debug.WriteLine($"rightside colls: {intersectionPointX}"); // for debugging
                return true;
            }

            intersectionPointX = 0f;
            return false;
        }

        public static bool DoesLineIntersectRectangleRight(Vector2 lineStart, Vector2 lineEnd, Rectangle rect)
        {
            if (DoesLineIntersectLine(lineStart, lineEnd, new Vector2(rect.X + rect.Width, rect.Y), new Vector2(rect.X + rect.Width, rect.Y + rect.Height)))
                return true;
            return false;
        }



        public static bool DoesLineIntersectRectangleY(Vector2 lineStart, Vector2 lineEnd, Rectangle rect, out float intersectionPointY)
        {
            Vector2 intersectionPoint;
            // upper
            if (DoesLineIntersectLine(lineStart, lineEnd, new Vector2(rect.X, rect.Y), new Vector2(rect.X + rect.Width, rect.Y + rect.Height), out intersectionPoint))
            {
                intersectionPointY = intersectionPoint.Y;
                Debug.WriteLine($"topside colls: {intersectionPointY}"); // for debugging
                return true;
            }
            // lower
            if (DoesLineIntersectLine(lineStart, lineEnd, new Vector2(rect.X, rect.Y + rect.Height), new Vector2(rect.X + rect.Width, rect.Y + rect.Height), out intersectionPoint))
            {
                intersectionPointY = intersectionPoint.Y;
                Debug.WriteLine($"bottomside colls: {intersectionPointY}"); // for debugging
                return true;
            }
            intersectionPointY = 0f;
            return false;
        }

        public static bool DoesLineIntersectRectangleY(Vector2 lineStart, Vector2 lineEnd, Rectangle rect)
        {
            // upper
            if (DoesLineIntersectLine(lineStart, lineEnd, new Vector2(rect.X, rect.Y), new Vector2(rect.X + rect.Width, rect.Y + rect.Height)))
                return true;
            // lower
            if (DoesLineIntersectLine(lineStart, lineEnd, new Vector2(rect.X, rect.Y + rect.Height), new Vector2(rect.X + rect.Width, rect.Y + rect.Height)))
                return true;
            return false;
        }

        public static bool DoesLineIntersectRectangleTop(Vector2 lineStart, Vector2 lineEnd, Rectangle rect, out float intersectionPointY)
        {
            Vector2 intersectionPoint;
            if (DoesLineIntersectLine(lineStart, lineEnd, new Vector2(rect.X, rect.Y), new Vector2(rect.X + rect.Width, rect.Y + rect.Height), out intersectionPoint))
            {
                intersectionPointY = intersectionPoint.Y;
                Debug.WriteLine($"topside colls: {intersectionPointY}"); // for debugging
                return true;
            }
            intersectionPointY = 0f;
            return false;
        }

        public static bool DoesLineIntersectRectangleTop(Vector2 lineStart, Vector2 lineEnd, Rectangle rect)
        {
            if (DoesLineIntersectLine(lineStart, lineEnd, new Vector2(rect.X, rect.Y), new Vector2(rect.X + rect.Width, rect.Y + rect.Height)))
                return true;
            return false;
        }

        public static bool DoesLineIntersectRectangleBottom(Vector2 lineStart, Vector2 lineEnd, Rectangle rect, out float intersectionPointY)
        {
            Vector2 intersectionPoint;
            if (DoesLineIntersectLine(lineStart, lineEnd, new Vector2(rect.X, rect.Y + rect.Height), new Vector2(rect.X + rect.Width, rect.Y + rect.Height), out intersectionPoint))
            {
                intersectionPointY = intersectionPoint.Y;
                Debug.WriteLine($"bottomside colls: {intersectionPointY}"); // for debugging
                return true;
            }
            intersectionPointY = 0f;
            return false;
        }

        public static bool DoesLineIntersectRectangleBottom(Vector2 lineStart, Vector2 lineEnd, Rectangle rect)
        {
            if (DoesLineIntersectLine(lineStart, lineEnd, new Vector2(rect.X, rect.Y + rect.Height), new Vector2(rect.X + rect.Width, rect.Y + rect.Height)))
                return true;
            return false;
        }
    }
}