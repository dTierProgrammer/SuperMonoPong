using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoPongSuper.Script.Global
{
    public enum Collision
    {
        NONE,
        LEFT,
        RIGHT,
        TOP,
        BOTTOM
    }
    public static class GetIntersections
    {
        public static bool LineLine(float x1, float y1 /* lineA start pt */, float x2, float y2, /* lineA end pt */
                                    float x3, float y3 /* lineB start pt */, float x4, float y4 /* lineB end pt */)
        {
            float uA = ((x4 - x3) * (y1 - y3) - (y4 - y3) * (x1 - x3)) / ((y4 - y3) * (x2 - x1) - (x4 - x3) * (y2 - y1));
            float uB = ((x2 - x1) * (y1 - y3) - (y2 - y1) * (x1 - x3)) / ((y4 - y3) * (x2 - x1) - (x4 - x3) * (y2 - y1));

            if (uA >= 0 && uA <= 1 && uB >= 0 && uB <= 1)
            {
                return true;
            }
            return false;
        }

        public static Vector2 LineLineReturnIntersection(float x1, float y1 /* lineA start pt */, float x2, float y2, /* lineA end pt */
                                                         float x3, float y3 /* lineB start pt */, float x4, float y4 /* lineB end pt */)
        {
            float uA = ((x4 - x3) * (y1 - y3) - (y4 - y3) * (x1 - x3)) / ((y4 - y3) * (x2 - x1) - (x4 - x3) * (y2 - y1));
            float uB = ((x2 - x1) * (y1 - y3) - (y2 - y1) * (x1 - x3)) / ((y4 - y3) * (x2 - x1) - (x4 - x3) * (y2 - y1));
            if (uA >= 0 && uA <= 1 && uB >= 0 && uB <= 1)
            {
                return new Vector2(x1 + (uA * (x2 - x1)), y1 + (uA * (y2 - y1)));
            }
            return new Vector2(-1000, -1000);
        }

        public static bool LineRect(float x1, float y1 /* line start pt */, float x2, float y2 /* line end pt */,
                                    Rectangle rect)
        {
            // Check if the line intersects with any of the rectangle's sides using 4 line line collision checks
            bool left = LineLine(x1, y1, x2, y2, rect.Right, rect.Top, rect.Right, rect.Bottom);
            bool right = LineLine(x1, y1, x2, y2, rect.Left, rect.Bottom, rect.Left, rect.Top);
            bool top = LineLine(x1, y1, x2, y2, rect.Left, rect.Top, rect.Right, rect.Top);
            bool bottom = LineLine(x1, y1, x2, y2, rect.Right, rect.Bottom, rect.Left, rect.Bottom);

            if (left || right || top || bottom)
            {
                return true;
            }

            return false;
        }

        public static Vector2 LineRectReturnIntersection(float x1, float y1 /* line start pt */, float x2, float y2 /* line end pt */,
                                    Rectangle rect)
        {
            // Check if the line intersects with any of the rectangle's sides using 4 line line collision checks
            Vector2 left = LineLineReturnIntersection(x1, y1, x2, y2, rect.Right, rect.Top, rect.Right, rect.Bottom);
            Vector2 right = LineLineReturnIntersection(x1, y1, x2, y2, rect.Left, rect.Bottom, rect.Left, rect.Top);
            Vector2 top = LineLineReturnIntersection(x1, y1, x2, y2, rect.Left, rect.Top, rect.Right, rect.Top);
            Vector2 bottom = LineLineReturnIntersection(x1, y1, x2, y2, rect.Right, rect.Bottom, rect.Left, rect.Bottom);
            if (left != new Vector2(-1000, -1000))
            {
                return left;
            }
            else if (right != new Vector2(-1000, -1000))
            {
                return right;
            }
            else if (top != new Vector2(-1000, -1000))
            {
                return top;
            }
            else if (bottom != new Vector2(-1000, -1000))
            {
                return bottom;
            }
            return new Vector2(-1000, -1000);
        }

        public static Collision LineRectReturnCollision(float x1, float y1 /* line start pt */, float x2, float y2 /* line end pt */,
                                           Rectangle rect) 
        {
            Collision collision = Collision.NONE;
            // Check if the line intersects with any of the rectangle's sides using 4 line line collision checks
            bool right = LineLine(x1, y1, x2, y2, rect.Right, rect.Top, rect.Right, rect.Bottom);
            bool left = LineLine(x1, y1, x2, y2, rect.Left, rect.Bottom, rect.Left, rect.Top);
            bool top = LineLine(x1, y1, x2, y2, rect.Left, rect.Top, rect.Right, rect.Top);
            bool bottom = LineLine(x1, y1, x2, y2, rect.Right, rect.Bottom, rect.Left, rect.Bottom);

            if (left)
            {
                collision = Collision.LEFT;
                return collision;
            }
            else if (right)
            {
                collision = Collision.RIGHT;
                return collision;
            }
            else if (top)
            {
                collision = Collision.TOP;
                return collision;
            }
            else if (bottom)
            {
                collision = Collision.BOTTOM;
                return collision;
            }
            return collision;
        }

        public static Vector2 LineRectCollisionTest(float x1, float y1 /* line start pt */, float x2, float y2 /* line end pt */,
                                   Rectangle rect)
        {
            // Check if the line intersects with any of the rectangle's sides using 4 line line collision checks
            Vector2 left = LineLineReturnIntersection(x1, y1, x2, y2, rect.Right, rect.Top, rect.Right, rect.Bottom);
            Vector2 right = LineLineReturnIntersection(x1, y1, x2, y2, rect.Left, rect.Bottom, rect.Left, rect.Top);
            Vector2 top = LineLineReturnIntersection(x1, y1, x2, y2, rect.Left, rect.Top, rect.Right, rect.Top);
            Vector2 bottom = LineLineReturnIntersection(x1, y1, x2, y2, rect.Right, rect.Bottom, rect.Left, rect.Bottom);
            if (left != new Vector2(-1000, -1000))
            {
                return left;
            }
            else if (right != new Vector2(-1000, -1000))
            {
                return right;
            }
            else if (top != new Vector2(-1000, -1000))
            {
                return top;
            }
            else if (bottom != new Vector2(-1000, -1000))
            {
                return bottom;
            }
            return new Vector2(-1000, -1000);
        }

        public static bool PointRect(float x, float y, Rectangle rect)
        {
            // Check if the point is inside the rectangle
            if (x >= rect.Left && x <= rect.Right && y >= rect.Top && y <= rect.Bottom)
            {
                return true;
            }
            return false;
        }
    }
}