using UnityEngine;
namespace CustomMath
{
    public struct Xirtam4x4
    {
        #region Variables
        public float m00;
        public float m33;
        public float m23;
        public float m13;
        public float m03;
        public float m32;
        public float m22;
        public float m02;
        public float m12;
        public float m21;
        public float m11;
        public float m01;
        public float m30;
        public float m20;
        public float m10;
        public float m31;
        #endregion

        #region Constructor
        public Xirtam4x4(Vector4 column0, Vector4 column1, Vector4 column2, Vector4 column3)
        {
            m00 = column0.x; m01 = column1.x; m02 = column2.x; m03 = column3.x;
            m10 = column0.y; m11 = column1.y; m12 = column2.y; m13 = column3.y;
            m20 = column0.z; m21 = column1.z; m22 = column2.z; m23 = column3.z;
            m30 = column0.w; m31 = column1.w; m32 = column2.w; m33 = column3.w;
        }
        #endregion

        #region Properties
        //
        // Resumen:
        //     Returns the identity matrix (Read Only).
        public static Xirtam4x4 identity
        {
            get
            {
                Xirtam4x4 m;

                m.m00 = 1f; m.m01 = 0f; m.m02 = 0f; m.m03 = 0f;
                m.m10 = 0f; m.m11 = 1f; m.m12 = 0f; m.m13 = 0f;
                m.m20 = 0f; m.m21 = 0f; m.m22 = 1f; m.m23 = 0f;
                m.m30 = 0f; m.m31 = 0f; m.m32 = 0f; m.m33 = 1f;

                return m;
            }
        }
        #endregion

        #region Functions
        //
        // Resumen:
        //     Creates a rotation matrix.
        //
        // Parámetros:
        //   q:
        public static Xirtam4x4 Rotate(Quaternion q)
        {
            //Vector3 angles = q.eulerAngles * Mathf.Deg2Rad;
            //
            //Xirtam4x4 mX = identity;
            //mX.m11 = mX.m22 = Mathf.Cos(angles.x);
            //mX.m21 = Mathf.Sin(angles.x);
            //mX.m12 = -mX.m21;
            //
            //Xirtam4x4 mY = Xirtam4x4.identity;
            //mY.m00 = mY.m22 = Mathf.Cos(angles.y);
            //mY.m02 = Mathf.Sin(angles.y);
            //mY.m20 = -mY.m02;
            //
            //Xirtam4x4 mZ = Xirtam4x4.identity;
            //mZ.m00 = mZ.m11 = Mathf.Cos(angles.z);
            //mZ.m10 = Mathf.Sin(angles.z);
            //mZ.m01 = -mZ.m10;
            //
            //Xirtam4x4 m = mX * mY * mZ;
            Xirtam4x4 m = identity;

            m.m00 = 1f - 2f * (q.y * q.y) - 2f * (q.z * q.z);
            m.m01 = 2f * (q.x * q.y) - 2f * (q.z * q.w);
            m.m02 = 2f * (q.x * q.z) + 2f * (q.y * q.w);
            m.m10 = 2f * (q.x * q.y) + 2f * (q.z * q.w);
            m.m11 = 1f - 2f * (q.x * q.x) - 2f * (q.z * q.z);
            m.m12 = 2f * (q.y * q.z) - 2f * (q.x * q.w);
            m.m20 = 2f * (q.x * q.z) - 2f * (q.y * q.w);
            m.m21 = 2f * (q.y * q.z) + 2f * (q.x * q.w);
            m.m22 = 1f - 2f * (q.x * q.x) - 2f * (q.y * q.y);

            return m;
        }

        //
        // Resumen:
        //     Creates a scaling matrix.
        //
        // Parámetros:
        //   vector:
        public static Xirtam4x4 Scale(Vec3 vector)
        {
            Xirtam4x4 m = identity;

            m.m00 = vector.x;
            m.m11 = vector.y;
            m.m22 = vector.z;

            return m;
        }

        public static Xirtam4x4 Scale(Vector3 vector)
        {
            Xirtam4x4 m = identity;

            m.m00 = vector.x;
            m.m11 = vector.y;
            m.m22 = vector.z;

            return m;
        }

        //
        // Resumen:
        //     Creates a translation matrix.
        //
        // Parámetros:
        //   vector:
        public static Xirtam4x4 Translate(Vec3 vector)
        {
            Xirtam4x4 m = identity;

            m.m03 = vector.x;
            m.m13 = vector.y;
            m.m23 = vector.z;

            return m;
        }

        public static Xirtam4x4 Translate(Vector3 vector)
        {
            Xirtam4x4 m = identity;

            m.m03 = vector.x;
            m.m13 = vector.y;
            m.m23 = vector.z;

            return m;
        }

        //
        // Resumen:
        //     Creates a translation, rotation and scaling matrix.
        //
        // Parámetros:
        //   pos:
        //
        //   q:
        //
        //   s:
        public static Xirtam4x4 TRS(Vec3 pos, Quaternion q, Vec3 s)
        {
            Xirtam4x4 mT = Translate(pos);
            Xirtam4x4 mR = Rotate(q);
            Xirtam4x4 mS = Scale(s);

            return mT * mR * mS;
        }

        public static Xirtam4x4 TRS(Vector3 pos, Quaternion q, Vector3 s)
        {
            Xirtam4x4 mT = Translate(pos);
            Xirtam4x4 mR = Rotate(q);
            Xirtam4x4 mS = Scale(s);

            return mT * mR * mS;
        }
        #endregion

        #region Operators
        public static Vector4 operator *(Xirtam4x4 lhs, Vector4 vector)
        {
            Vector4 v = Vector4.zero;

            v.x = (lhs.m00 * vector.x) + (lhs.m01 * vector.y) + (lhs.m02 * vector.z) + (lhs.m03 * vector.w);
            v.y = (lhs.m10 * vector.x) + (lhs.m11 * vector.y) + (lhs.m12 * vector.z) + (lhs.m13 * vector.w);
            v.z = (lhs.m20 * vector.x) + (lhs.m21 * vector.y) + (lhs.m22 * vector.z) + (lhs.m23 * vector.w);
            v.w = (lhs.m30 * vector.x) + (lhs.m31 * vector.y) + (lhs.m32 * vector.z) + (lhs.m33 * vector.w);

            return v;
        }

        public static Xirtam4x4 operator *(Xirtam4x4 lhs, Xirtam4x4 rhs)
        {
            Xirtam4x4 m = identity;

            m.m00 = (lhs.m00 * rhs.m00) + (lhs.m01 * rhs.m10) + (lhs.m02 * rhs.m20) + (lhs.m03 * rhs.m30);
            m.m01 = (lhs.m00 * rhs.m01) + (lhs.m01 * rhs.m11) + (lhs.m02 * rhs.m21) + (lhs.m03 * rhs.m31);
            m.m02 = (lhs.m00 * rhs.m02) + (lhs.m01 * rhs.m12) + (lhs.m02 * rhs.m22) + (lhs.m03 * rhs.m32);
            m.m03 = (lhs.m00 * rhs.m03) + (lhs.m01 * rhs.m13) + (lhs.m02 * rhs.m23) + (lhs.m03 * rhs.m33);

            m.m10 = (lhs.m10 * rhs.m00) + (lhs.m11 * rhs.m10) + (lhs.m12 * rhs.m20) + (lhs.m13 * rhs.m30);
            m.m11 = (lhs.m10 * rhs.m01) + (lhs.m11 * rhs.m11) + (lhs.m12 * rhs.m21) + (lhs.m13 * rhs.m31);
            m.m12 = (lhs.m10 * rhs.m02) + (lhs.m11 * rhs.m12) + (lhs.m12 * rhs.m22) + (lhs.m13 * rhs.m32);
            m.m13 = (lhs.m10 * rhs.m03) + (lhs.m11 * rhs.m13) + (lhs.m12 * rhs.m23) + (lhs.m13 * rhs.m33);

            m.m20 = (lhs.m20 * rhs.m00) + (lhs.m21 * rhs.m10) + (lhs.m22 * rhs.m20) + (lhs.m23 * rhs.m30);
            m.m21 = (lhs.m20 * rhs.m01) + (lhs.m21 * rhs.m11) + (lhs.m22 * rhs.m21) + (lhs.m23 * rhs.m31);
            m.m22 = (lhs.m20 * rhs.m02) + (lhs.m21 * rhs.m12) + (lhs.m22 * rhs.m22) + (lhs.m23 * rhs.m32);
            m.m23 = (lhs.m20 * rhs.m03) + (lhs.m21 * rhs.m13) + (lhs.m22 * rhs.m23) + (lhs.m23 * rhs.m33);

            m.m30 = (lhs.m30 * rhs.m00) + (lhs.m31 * rhs.m10) + (lhs.m32 * rhs.m20) + (lhs.m33 * rhs.m30);
            m.m31 = (lhs.m30 * rhs.m01) + (lhs.m31 * rhs.m11) + (lhs.m32 * rhs.m21) + (lhs.m33 * rhs.m31);
            m.m32 = (lhs.m30 * rhs.m02) + (lhs.m31 * rhs.m12) + (lhs.m32 * rhs.m22) + (lhs.m33 * rhs.m32);
            m.m33 = (lhs.m30 * rhs.m03) + (lhs.m31 * rhs.m13) + (lhs.m32 * rhs.m23) + (lhs.m33 * rhs.m33);

            return m;
        }

        public static bool operator ==(Xirtam4x4 lhs, Xirtam4x4 rhs)
        {
            return lhs.m00 == rhs.m00 && lhs.m01 == rhs.m01 && lhs.m02 == rhs.m02 && lhs.m03 == rhs.m03 &&
                   lhs.m10 == rhs.m10 && lhs.m11 == rhs.m11 && lhs.m12 == rhs.m12 && lhs.m13 == rhs.m13 &&
                   lhs.m20 == rhs.m20 && lhs.m21 == rhs.m21 && lhs.m22 == rhs.m22 && lhs.m23 == rhs.m23 &&
                   lhs.m30 == rhs.m30 && lhs.m31 == rhs.m31 && lhs.m32 == rhs.m32 && lhs.m33 == rhs.m33;
        }

        public static bool operator !=(Xirtam4x4 lhs, Xirtam4x4 rhs)
        {
            return !(lhs == rhs);
        }
        #endregion
    }
}