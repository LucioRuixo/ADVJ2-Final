using UnityEngine;
using CustomMath;

public class Cubo : MonoBehaviour
{
    Vec3 pos;
    Quaternion rot;
    Vec3 sca;

     Xirtam4x4 mT;
     Xirtam4x4 mR;
     Xirtam4x4 mS;
     Xirtam4x4 mTRS;

    [Header("T")]
    public Vector4 t0;
    public Vector4 t1;
    public Vector4 t2;
    public Vector4 t3;

    [Header("R")]
    public Vector4 r0;
    public Vector4 r1;
    public Vector4 r2;
    public Vector4 r3;

    [Header("S")]
    public Vector4 s0;
    public Vector4 s1;
    public Vector4 s2;
    public Vector4 s3;

    [Header("TRS")]
    public Vector4 trs0;
    public Vector4 trs1;
    public Vector4 trs2;
    public Vector4 trs3;

    void Start()
    {
        mT = mR = mS = mTRS = Xirtam4x4.identity;
    }

    void Update()
    {
        pos = new Vec3(transform.position.x, transform.position.y, transform.position.z);
        rot = new Quaternion(transform.rotation.x, transform.rotation.y, transform.rotation.z, transform.rotation.w);
        sca = new Vec3(transform.localScale.x, transform.localScale.y, transform.localScale.z);

        mT = Xirtam4x4.Translate(pos);
        mR = Xirtam4x4.Rotate(rot);
        mS = Xirtam4x4.Scale(sca);
        mTRS = Xirtam4x4.TRS(pos, rot, sca);

        t0 = new Vector4(mT.m00, mT.m01, mT.m02, mT.m03);
        t1 = new Vector4(mT.m10, mT.m11, mT.m12, mT.m13);
        t2 = new Vector4(mT.m20, mT.m21, mT.m22, mT.m23);
        t3 = new Vector4(mT.m30, mT.m31, mT.m32, mT.m33);

        r0 = new Vector4(mR.m00, mR.m01, mR.m02, mR.m03);
        r1 = new Vector4(mR.m10, mR.m11, mR.m12, mR.m13);
        r2 = new Vector4(mR.m20, mR.m21, mR.m22, mR.m23);
        r3 = new Vector4(mR.m30, mR.m31, mR.m32, mR.m33);

        s0 = new Vector4(mS.m00, mS.m01, mS.m02, mS.m03);
        s1 = new Vector4(mS.m10, mS.m11, mS.m12, mS.m13);
        s2 = new Vector4(mS.m20, mS.m21, mS.m22, mS.m23);
        s3 = new Vector4(mS.m30, mS.m31, mS.m32, mS.m33);

        trs0 = new Vector4(mTRS.m00, mTRS.m01, mTRS.m02, mTRS.m03);
        trs1 = new Vector4(mTRS.m10, mTRS.m11, mTRS.m12, mTRS.m13);
        trs2 = new Vector4(mTRS.m20, mTRS.m21, mTRS.m22, mTRS.m23);
        trs3 = new Vector4(mTRS.m30, mTRS.m31, mTRS.m32, mTRS.m33);
    }
}
