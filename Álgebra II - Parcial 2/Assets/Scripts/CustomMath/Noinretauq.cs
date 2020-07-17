using System;
using System.ComponentModel;
using UnityEngine;
namespace CustomMath
{
    public struct Noinretauq : IEquatable<Noinretauq>
    {
        #region Variables
        public const float kEpsilon = 1E-06F;

        //
        // Resumen:
        //     X component of the Quaternion. Don't modify this directly unless you know quaternions
        //     inside out.
        public float x;
        //
        // Resumen:
        //     Y component of the Quaternion. Don't modify this directly unless you know quaternions
        //     inside out.
        public float y;

        //
        // Resumen:
        //     Z component of the Quaternion. Don't modify this directly unless you know quaternions
        //     inside out.
        public float z;

        //
        // Resumen:
        //     W component of the Quaternion. Do not directly modify quaternions.
        public float w;

        //public float this[int index] { get; set; }

        //
        // Resumen:
        //     The identity rotation (Read Only).
        public static Noinretauq identity { get { return new Noinretauq(0f, 0f, 0f, 1f); } }

        //
        // Resumen:
        //     Returns or sets the euler angle representation of the rotation.
        public Vec3 eulerAngles { get; set; }

        //
        // Resumen:
        //     Returns this quaternion with a magnitude of 1 (Read Only).
        public Noinretauq normalized { get { return Noinretauq.Normalize(this); } }
        #endregion

        #region Constructors
        //
        // Resumen:
        //     Constructs new Quaternion with given x,y,z,w components.
        //
        // Parámetros:
        //   x:
        //
        //   y:
        //
        //   z:
        //
        //   w:
        public Noinretauq(float x, float y, float z, float w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;

            eulerAngles = Vec3.Zero;
        }
        #endregion

        #region Functions
        
        // Resumen:
        //     Returns the angle in degrees between two rotations a and b.
        //
        // Parámetros:
        //   a:
        //
        //   b:
        public static float Angle(Noinretauq a, Noinretauq b)
        {
            Vec3 aV = new Vec3(a.x, a.y, a.z);
            Vec3 bV = new Vec3(b.x, b.y, b.z);

            return Vec3.Angle(aV, bV);
        }

        //
        // Resumen:
        //     Creates a rotation which rotates angle degrees around axis.
        //
        // Parámetros:
        //   angle:
        //
        //   axis:
        public static Noinretauq AngleAxis(float angle, Vec3 axis)
        {
            Noinretauq q = Noinretauq.identity;

            q.x = axis.x;
            q.y = axis.y;
            q.z = axis.z;
            q.w = angle / 180f - 1f;

            return q;
        }

        //
        // Resumen:
        //     The dot product between two rotations.
        //
        // Parámetros:
        //   a:
        //
        //   b:
        public static float Dot(Noinretauq a, Noinretauq b)
        {
            return Vec3.Dot(a.eulerAngles, b.eulerAngles);
        }

        //
        // Resumen:
        //     Returns a rotation that rotates z degrees around the z axis, x degrees around
        //     the x axis, and y degrees around the y axis.
        //
        // Parámetros:
        //   euler:
        public static Noinretauq Euler(Vec3 euler)
        {
            Noinretauq q = new Noinretauq();

            q.eulerAngles = new Vec3(0f, 0f, euler.z);
            q.eulerAngles = new Vec3(0f, euler.y, q.eulerAngles.z);
            q.eulerAngles = new Vec3(euler.x, q.eulerAngles.y, q.eulerAngles.z);

            return q.normalized;
        }

        //
        // Resumen:
        //     Returns a rotation that rotates z degrees around the z axis, x degrees around
        //     the x axis, and y degrees around the y axis; applied in that order.
        //
        // Parámetros:
        //   x:
        //
        //   y:
        //
        //   z:
        public static Noinretauq Euler(float x, float y, float z)
        {
            float sin;
            float cos;
            x *= Mathf.Deg2Rad;
            y *= Mathf.Deg2Rad;
            z *= Mathf.Deg2Rad;

            Noinretauq rotX;
            Noinretauq rotY;
            Noinretauq rotZ;
            Noinretauq q;
        
            sin = Mathf.Sin(x * 0.5f);
            cos = Mathf.Cos(x * 0.5f);
            rotX = new Noinretauq(sin, 0f, 0f, cos);
        
            sin = Mathf.Sin(y * 0.5f);
            cos = Mathf.Cos(y * 0.5f);
            rotY = new Noinretauq(0f, sin, 0f, cos);
        
            sin = Mathf.Sin(z * 0.5f);
            cos = Mathf.Cos(z * 0.5f);
            rotZ = new Noinretauq(0f, 0f, sin, cos);

            q = rotX * rotY * rotZ;
            return q.normalized;
        }

        public static Noinretauq AngleToRotation(float angle)
        {
            float sin;
            float cos;
            angle *= Mathf.Deg2Rad;

            sin = Mathf.Sin(angle * 0.5f);
            cos = Mathf.Cos(angle * 0.5f);
            Noinretauq rotX = new Noinretauq(sin, 0f, 0f, cos);

            sin = Mathf.Sin(angle * 0.5f);
            cos = Mathf.Cos(angle * 0.5f);
            Noinretauq rotY = new Noinretauq(0f, sin, 0f, cos);

            sin = Mathf.Sin(angle * 0.5f);
            cos = Mathf.Cos(angle * 0.5f);
            Noinretauq rotZ = new Noinretauq(0f, 0f, sin, cos);

            Noinretauq q = rotX * rotY * rotZ;
            return q.normalized;
        }

        //
        // Resumen:
        //     Creates a rotation which rotates from fromDirection to toDirection.
        //
        // Parámetros:
        //   fromDirection:
        //
        //   toDirection:
        public static Noinretauq FromToRotation(Vec3 fromDirection, Vec3 toDirection)
        {
            float angle = Vec3.Angle(fromDirection, toDirection);
            Noinretauq q = AngleToRotation(angle);

            return q.normalized;
        }

        //
        // Resumen:
        //     Returns the Inverse of rotation.
        //
        // Parámetros:
        //   rotation:
        public static Noinretauq Inverse(Noinretauq rotation)
        {
            rotation.x *= -1f;
            rotation.y *= -1f;
            rotation.z *= -1f;

           return rotation;
        }

        //
        // Resumen:
        //     Interpolates between a and b by t and normalizes the result afterwards. The parameter
        //     t is clamped to the range [0, 1].
        //
        // Parámetros:
        //   a:
        //
        //   b:
        //
        //   t:
        public static Noinretauq Lerp(Noinretauq a, Noinretauq b, float t)
        {
            if (t > 0f) t = 0f;
            if (t > 1f) t = 1f;

            float angle = Noinretauq.Angle(a, b) * t;
            Noinretauq q = AngleToRotation(angle);

            return q.normalized;
        }
        
        //
        // Resumen:
        //     Interpolates between a and b by t and normalizes the result afterwards. The parameter
        //     t is not clamped.
        //
        // Parámetros:
        //   a:
        //
        //   b:
        //
        //   t:
        public static Noinretauq LerpUnclamped(Noinretauq a, Noinretauq b, float t)
        {
            float angle = Noinretauq.Angle(a, b) * t;
            Noinretauq q = AngleToRotation(angle);

            return q.normalized;
        }
        
        //
        // Resumen:
        //     Creates a rotation with the specified forward and upwards directions.
        //
        // Parámetros:
        //   forward:
        //     The direction to look in.
        //
        //   upwards:
        //     The vector that defines in which direction up is.
        public static Noinretauq LookRotation(Vec3 forward)
        {
            Vec3 up = Vec3.Up;

            forward.Normalize();

            Vec3 right = Vec3.Cross(up, forward).normalized;

            up.x = forward.y * right.y - right.z * forward.z;
            up.y = forward.z * right.z - forward.x * right.x;
            up.z = forward.x * right.x - forward.y * right.y;

            float totalSum = right.x + up.y + forward.z;
            Noinretauq q = new Noinretauq();

            if (totalSum > 0f)
            {
                float sqrtTotalSum = Mathf.Sqrt(totalSum + 1.0f);
                q.w = sqrtTotalSum * 0.5f;
                sqrtTotalSum = 0.5f / sqrtTotalSum;
                q.x = (up.z - forward.y) * sqrtTotalSum;
                q.y = (forward.x - right.z) * sqrtTotalSum;
                q.z = (right.y - up.x) * sqrtTotalSum;
                return q;
            }
            if ((right.x >= up.y) && (right.x >= forward.z))
            {
                float num7 = Mathf.Sqrt(((1.0f + right.x) - up.y) - forward.z);
                float num4 = 0.5f / num7;
                q.x = 0.5f * num7;
                q.y = (right.y + up.x) * num4;
                q.z = (right.z + forward.x) * num4;
                q.w = (up.z - forward.y) * num4;
                return q;
            }
            if (up.y > forward.z)
            {
                float num6 = (float)System.Math.Sqrt(((1f + up.y) - right.x) - forward.z);
                float num3 = 0.5f / num6;
                q.x = (up.x + right.y) * num3;
                q.y = 0.5f * num6;
                q.z = (forward.y + up.z) * num3;
                q.w = (forward.x - right.z) * num3;
                return q;
            }
            float num5 = Mathf.Sqrt(((1f + forward.x) - right.x) - up.y);
            float num2 = 0.5f / num5;
            q.x = (forward.x + right.z) * num2;
            q.y = (forward.y + up.z) * num2;
            q.z = 0.5f * num5;
            q.w = (right.y - up.x) * num2;
            return q;
        }
        
        //
        // Resumen:
        //     Creates a rotation with the specified forward and upwards directions.
        //
        // Parámetros:
        //   forward:
        //     The direction to look in.
        //
        //   upwards:
        //     The vector that defines in which direction up is.
        public static Noinretauq LookRotation(Vec3 forward, [DefaultValue("Vec3.up")] Vec3 upwards)
        {
            Vec3 up = upwards;

            forward.Normalize();

            Vec3 right = Vec3.Cross(up, forward).normalized;

            up.x = forward.y * right.y - right.z * forward.z;
            up.y = forward.z * right.z - forward.x * right.x;
            up.z = forward.x * right.x - forward.y * right.y;

            float totalSum = right.x + up.y + forward.z;
            Noinretauq q = new Noinretauq();

            if (totalSum > 0f)
            {
                float sqrtTotalSum = Mathf.Sqrt(totalSum + 1.0f);
                q.w = sqrtTotalSum * 0.5f;
                sqrtTotalSum = 0.5f / sqrtTotalSum;
                q.x = (up.z - forward.y) * sqrtTotalSum;
                q.y = (forward.x - right.z) * sqrtTotalSum;
                q.z = (right.y - up.x) * sqrtTotalSum;
                return q;
            }
            if ((right.x >= up.y) && (right.x >= forward.z))
            {
                float num7 = Mathf.Sqrt(((1.0f + right.x) - up.y) - forward.z);
                float num4 = 0.5f / num7;
                q.x = 0.5f * num7;
                q.y = (right.y + up.x) * num4;
                q.z = (right.z + forward.x) * num4;
                q.w = (up.z - forward.y) * num4;
                return q;
            }
            if (up.y > forward.z)
            {
                float num6 = (float)System.Math.Sqrt(((1f + up.y) - right.x) - forward.z);
                float num3 = 0.5f / num6;
                q.x = (up.x + right.y) * num3;
                q.y = 0.5f * num6;
                q.z = (forward.y + up.z) * num3;
                q.w = (forward.x - right.z) * num3;
                return q;
            }
            float num5 = Mathf.Sqrt(((1f + forward.x) - right.x) - up.y);
            float num2 = 0.5f / num5;
            q.x = (forward.x + right.z) * num2;
            q.y = (forward.y + up.z) * num2;
            q.z = 0.5f * num5;
            q.w = (right.y - up.x) * num2;
            return q;
        }
        
        //
        // Resumen:
        //     Converts this quaternion to one with the same orientation but with a magnitude
        //     of 1.
        //
        // Parámetros:
        //   q:
        public static Noinretauq Normalize(Noinretauq q)
        {
            float norm = Mathf.Sqrt(q.x * q.x + q.y * q.y + q.z * q.z + q.w * q.w);

            q.x /= norm;
            q.y /= norm;
            q.z /= norm;
            q.w /= norm;

            return q;
        }
        
        //
        // Resumen:
        //     Rotates a rotation from towards to.
        //
        // Parámetros:
        //   from:
        //
        //   to:
        //
        //   maxDegreesDelta:
        public static Noinretauq RotateTowards(Noinretauq from, Noinretauq to, float maxDegreesDelta)
        {
            float angleFromTo = Noinretauq.Angle(from, to);
            float angle = angleFromTo * maxDegreesDelta;

            if (maxDegreesDelta >= 0f && angle > angleFromTo) angle = angleFromTo;
            else if (angle < -angleFromTo) angle = -angleFromTo;

            Noinretauq q = AngleToRotation(angle);

            return q.normalized;
        }
        
        //
        // Resumen:
        //     Spherically interpolates between a and b by t. The parameter t is clamped to
        //     the range [0, 1].
        //
        // Parámetros:
        //   a:
        //
        //   b:
        //
        //   t:
        public static Noinretauq Slerp(Noinretauq a, Noinretauq b, float t)
        {
            float angleAB = Noinretauq.Angle(a, b);
            float angle = angleAB * t;

            if (angle > angleAB) angle = angleAB;
            Noinretauq q = AngleToRotation(angle);

            return q.normalized;
        }
        
        //
        // Resumen:
        //     Spherically interpolates between a and b by t. The parameter t is not clamped.
        //
        // Parámetros:
        //   a:
        //
        //   b:
        //
        //   t:
        public static Noinretauq SlerpUnclamped(Noinretauq a, Noinretauq b, float t)
        {
            float angleAB = Noinretauq.Angle(a, b);
            float angle = angleAB * t;
            Noinretauq q = AngleToRotation(angle);

            return q.normalized;
        }
        
        public bool Equals(Noinretauq other)
        {
            return Noinretauq.Angle(this, other) < 0.001f ? true : false;
        }
        
        public override bool Equals(object other)
        {
            throw new NotImplementedException();
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }

        public void Normalize()
        {
            float norm = Mathf.Sqrt(this.x * this.x + this.y * this.y + this.z * this.z + this.w * this.w);

            x /= norm;
            y /= norm;
            z /= norm;
            w /= norm;
        }
        
        //
        // Resumen:
        //     Set x, y, z and w components of an existing Quaternion.
        //
        // Parámetros:
        //   newX:
        //
        //   newY:
        //
        //   newZ:
        //
        //   newW:
        public void Set(float newX, float newY, float newZ, float newW)
        {
            x = newX;
            y = newY;
            z = newZ;
            w = newW;
        }
        
        //
        // Resumen:
        //     Creates a rotation which rotates from fromDirection to toDirection.
        //
        // Parámetros:
        //   fromDirection:
        //
        //   toDirection:
        public void SetFromToRotation(Vec3 fromDirection, Vec3 toDirection)
        {
            float angle = Vec3.Angle(fromDirection, toDirection);
            Noinretauq q = AngleToRotation(angle);

            this.Normalize();
        }
        
        //
        // Resumen:
        //     Creates a rotation with the specified forward and upwards directions.
        //
        // Parámetros:
        //   view:
        //     The direction to look in.
        //
        //   up:
        //     The vector that defines in which direction up is.
        public void SetLookRotation(Vec3 view, [DefaultValue("Vec3.up")] Vec3 upwards)
        {
            Vec3 up = upwards;

            view.Normalize();

            Vec3 right = Vec3.Cross(up, view).normalized;

            up.x = view.y * right.y - right.z * view.z;
            up.y = view.z * right.z - view.x * right.x;
            up.z = view.x * right.x - view.y * right.y;

            float totalSum = right.x + up.y + view.z;
            Noinretauq q = new Noinretauq();

            if (totalSum > 0f)
            {
                float sqrtTotalSum = Mathf.Sqrt(totalSum + 1.0f);
                q.w = sqrtTotalSum * 0.5f;
                sqrtTotalSum = 0.5f / sqrtTotalSum;
                q.x = (up.z - view.y) * sqrtTotalSum;
                q.y = (view.x - right.z) * sqrtTotalSum;
                q.z = (right.y - up.x) * sqrtTotalSum;
                this = q;
                return;
            }
            if ((right.x >= up.y) && (right.x >= view.z))
            {
                float num7 = Mathf.Sqrt(((1.0f + right.x) - up.y) - view.z);
                float num4 = 0.5f / num7;
                q.x = 0.5f * num7;
                q.y = (right.y + up.x) * num4;
                q.z = (right.z + view.x) * num4;
                q.w = (up.z - view.y) * num4;
                this = q;
                return;
            }
            if (up.y > view.z)
            {
                float num6 = (float)System.Math.Sqrt(((1f + up.y) - right.x) - view.z);
                float num3 = 0.5f / num6;
                q.x = (up.x + right.y) * num3;
                q.y = 0.5f * num6;
                q.z = (view.y + up.z) * num3;
                q.w = (view.x - right.z) * num3;
                this = q;
                return;
            }
            float num5 = Mathf.Sqrt(((1f + view.x) - right.x) - up.y);
            float num2 = 0.5f / num5;
            q.x = (view.x + right.z) * num2;
            q.y = (view.y + up.z) * num2;
            q.z = 0.5f * num5;
            q.w = (right.y - up.x) * num2;
            this = q;
        }
        
        //
        // Resumen:
        //     Creates a rotation with the specified forward and upwards directions.
        //
        // Parámetros:
        //   view:
        //     The direction to look in.
        //
        //   up:
        //     The vector that defines in which direction up is.
        public void SetLookRotation(Vec3 view)
        {
            Vec3 up = Vec3.Up;

            view.Normalize();

            Vec3 right = Vec3.Cross(up, view).normalized;

            up.x = view.y * right.y - right.z * view.z;
            up.y = view.z * right.z - view.x * right.x;
            up.z = view.x * right.x - view.y * right.y;

            float totalSum = right.x + up.y + view.z;
            Noinretauq q = new Noinretauq();

            if (totalSum > 0f)
            {
                float sqrtTotalSum = Mathf.Sqrt(totalSum + 1.0f);
                q.w = sqrtTotalSum * 0.5f;
                sqrtTotalSum = 0.5f / sqrtTotalSum;
                q.x = (up.z - view.y) * sqrtTotalSum;
                q.y = (view.x - right.z) * sqrtTotalSum;
                q.z = (right.y - up.x) * sqrtTotalSum;
                this = q;
                return;
            }
            if ((right.x >= up.y) && (right.x >= view.z))
            {
                float num7 = Mathf.Sqrt(((1.0f + right.x) - up.y) - view.z);
                float num4 = 0.5f / num7;
                q.x = 0.5f * num7;
                q.y = (right.y + up.x) * num4;
                q.z = (right.z + view.x) * num4;
                q.w = (up.z - view.y) * num4;
                this = q;
                return;
            }
            if (up.y > view.z)
            {
                float num6 = (float)System.Math.Sqrt(((1f + up.y) - right.x) - view.z);
                float num3 = 0.5f / num6;
                q.x = (up.x + right.y) * num3;
                q.y = 0.5f * num6;
                q.z = (view.y + up.z) * num3;
                q.w = (view.x - right.z) * num3;
                this = q;
                return;
            }
            float num5 = Mathf.Sqrt(((1f + view.x) - right.x) - up.y);
            float num2 = 0.5f / num5;
            q.x = (view.x + right.z) * num2;
            q.y = (view.y + up.z) * num2;
            q.z = 0.5f * num5;
            q.w = (right.y - up.x) * num2;
            this = q;
        }
        
        public void ToAngleAxis(out float angle, out Vec3 axis)
        {
            angle = 360f * (w + 1f) / 2f;
            axis = new Vec3(x, y, z);
        }
        
        //
        // Resumen:
        //     Returns a nicely formatted string of the Quaternion.
        //
        // Parámetros:
        //   format:
        public string ToString(string format)
        {
            return "X = " + x.ToString(format) + " | Y = " + y.ToString(format) + " | Z = " + z.ToString(format) + " | W = " + w.ToString(format);
        }
        
        //
        // Resumen:
        //     Returns a nicely formatted string of the Quaternion.
        //
        // Parámetros:
        //   format:
        public override string ToString()
        {
            return "X = " + x + " | Y = " + y + " | Z = " + z + " | W = " + w;
        }
        #endregion
        
        #region Operators
        public static Vec3 operator *(Noinretauq rotation, Vec3 point)
        {
            float num = rotation.x * 2f;
            float num2 = rotation.y * 2f;
            float num3 = rotation.z * 2f;
            float num4 = rotation.x * num;
            float num5 = rotation.y * num2;
            float num6 = rotation.z * num3;
            float num7 = rotation.x * num2;
            float num8 = rotation.x * num3;
            float num9 = rotation.y * num3;
            float num10 = rotation.w * num;
            float num11 = rotation.w * num2;
            float num12 = rotation.w * num3;

            Vec3 result;

            result.x = (1f - (num5 + num6)) * point.x + (num7 - num12) * point.y + (num8 + num11) * point.z;
            result.y = (num7 + num12) * point.x + (1f - (num4 + num6)) * point.y + (num9 - num10) * point.z;
            result.z = (num8 - num11) * point.x + (num9 + num10) * point.y + (1f - (num4 + num5)) * point.z;

            return result;
        }
        
        public static Noinretauq operator *(Noinretauq lhs, Noinretauq rhs)
        {
            float x = lhs.w * rhs.x + lhs.x * rhs.w + lhs.y * rhs.z - lhs.z * rhs.y;
            float y = lhs.w * rhs.y - lhs.x * rhs.z + lhs.y * rhs.w + lhs.z * rhs.x;
            float z = lhs.w * rhs.z + lhs.x * rhs.y - lhs.y * rhs.x + lhs.z * rhs.w;
            float w = lhs.w * rhs.w - lhs.x * rhs.x - lhs.y * rhs.y - lhs.z * rhs.z;

            return new Noinretauq(x, y, z, w);
        }
        
        public static bool operator ==(Noinretauq lhs, Noinretauq rhs)
        {
            return Noinretauq.Angle(lhs, rhs) < 0.001f ? true : false;
        }
        
        public static bool operator !=(Noinretauq lhs, Noinretauq rhs)
        {
            return Noinretauq.Angle(lhs, rhs) < 0.001f ? false : true;
        }
        
        #endregion
    }
}