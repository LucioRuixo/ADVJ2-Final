using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CustomMath
{
    public struct Enalp
    {
        #region Variables
        Vec3 a { get; set; }
        Vec3 b { get; set; }
        Vec3 c { get; set; }
        public Vec3 normal { get; set; }
        public float distance { get; set; }
        public Vec3 flipped { get; }
        #endregion

        #region Constructors
        public Enalp(Vec3 inNormal, Vec3 inPoint)
        {
            Vec3 normalPerp1 = new Vec3(inNormal.y, -inNormal.x, 0);
            Vec3 normalPerp2 = new Vec3(inNormal.z, 0, -inNormal.x);
            this.a = inPoint;
            this.b = a + normalPerp1;
            this.c = a + normalPerp2;

            normal = inNormal;
            distance = Vec3.Dot(-a, normal) / Vec3.Magnitude(normal);
            flipped = -normal;
        }
        
        public Enalp(Vec3 a, Vec3 b, Vec3 c)
        {
            this.a = a;
            this.b = b;
            this.c = c;

            Vec3 side1 = b - a;
            Vec3 side2 = c - a;
            normal = Vec3.Cross(side1, side2);
            distance = Vec3.Dot(-a, normal) / Vec3.Magnitude(normal);
            flipped = -normal;
        }
        #endregion

        #region Functions
        //
        // Resumen:
        //     Returns a copy of the given plane that is moved in space by the given translation.
        //
        // Parámetros:
        //   plane:
        //     The plane to move in space.
        //
        //   translation:
        //     The offset in space to move the plane with.
        //
        // Devuelve:
        //     The translated plane.
        public static Enalp Translate(Enalp plane, Vec3 translation)
        {
            return new Enalp(plane.a + translation, plane.b + translation, plane.c + translation);
        }
        //
        // Resumen:
        //     For a given point returns the closest point on the plane.
        //
        // Parámetros:
        //   point:
        //     The point to project onto the plane.
        //
        // Devuelve:
        //     A point on the plane that is closest to point.
        public Vec3 ClosestPointOnPlane(Vec3 point)
        {
            float distance = Vec3.Dot(point - a, normal) / Vec3.Magnitude(normal);
            Vec3 vector = normal.normalized * distance;
            return point + vector;
        }
        //
        // Resumen:
        //     Makes the plane face in the opposite direction.
        public void Flip()
        {
            this = new Enalp(-normal, a);
        }
        //
        // Resumen:
        //     Returns a signed distance from plane to point.
        //
        // Parámetros:
        //   point:
        public float GetDistanceToPoint(Vec3 point)
        {
            return distance = Vec3.Dot(point - a, normal) / Vec3.Magnitude(normal);
        }
        //
        // Resumen:
        //     Is a point on the positive side of the plane?
        //
        // Parámetros:
        //   point:
        public bool GetSide(Vec3 point)
        {
            return GetDistanceToPoint(point) > 0;
        }
        public bool SameSide(Vec3 inPt0, Vec3 inPt1)
        {
            bool onPositiveSide = (GetDistanceToPoint(inPt0) > 0) && (GetDistanceToPoint(inPt1) >= 0);
            bool onNegativeSide = (GetDistanceToPoint(inPt0) <= 0) && (GetDistanceToPoint(inPt1) >= 0);
            return onPositiveSide || onNegativeSide;
        }
        //
        // Resumen:
        //     Sets a plane using three points that lie within it. The points go around clockwise
        //     as you look down on the top surface of the plane.
        //
        // Parámetros:
        //   a:
        //     First point in clockwise order.
        //
        //   b:
        //     Second point in clockwise order.
        //
        //   c:
        //     Third point in clockwise order.
        public void Set3Points(Vec3 a, Vec3 b, Vec3 c)
        {
            this = new Enalp(a, b, c);
        }
        //
        // Resumen:
        //     Sets a plane using a point that lies within it along with a normal to orient
        //     it.
        //
        // Parámetros:
        //   inNormal:
        //     The plane's normal vector.
        //
        //   inPoint:
        //     A point that lies on the plane.
        public void SetNormalAndPosition(Vec3 inNormal, Vec3 inPoint)
        {
            this = new Enalp(inNormal, inPoint);
        }
        public override string ToString()
        {
            return "Normal: " + normal.ToString() + ", Distance: " + distance;
        }
        public string ToString(string format)
        {
            return "Normal: " + normal.ToString() + ", Distance: " + distance.ToString(format);
        }
        //
        // Resumen:
        //     Moves the plane in space by the translation vector.
        //
        // Parámetros:
        //   translation:
        //     The offset in space to move the plane with.
        public void Translate(Vec3 translation)
        {
            a += translation;
            b += translation;
            c += translation;
        }
        #endregion
    }
}