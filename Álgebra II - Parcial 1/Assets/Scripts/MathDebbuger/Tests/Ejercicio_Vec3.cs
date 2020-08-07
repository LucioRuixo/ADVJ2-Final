using System.Collections.Generic;
using UnityEngine;
using MathDebbuger;
using CustomMath;

public class Ejercicio_Vec3 : MonoBehaviour
{
    public enum Ejercicios
    {
        Uno,
        Dos,
        Tres,
        Cuatro,
        Cinco,
        Seis,
        Siete,
        Ocho,
        Nueve,
        Diez
    }
    public Ejercicios ejercicio = Ejercicios.Uno;
    Ejercicios ultimoEjercicio;
    
    bool seCambioElEjercicio = false;
    
    public Color vectorColor = Color.red;
    
    public Vector3 A = Vector3.zero;
    public Vector3 B = Vector3.zero;
    public Vector3 C = Vector3.zero;
    Vec3 AV3 = Vec3.Zero;
    Vec3 BV3 = Vec3.Zero;
    Vec3 CV3 = Vec3.Zero;
    
    float E5_lerpTime = 1f;
    float E5_startTime;
    float E5_timeSinceStarted;
    float E5_percentageComplete;
    
    float E10_lerpTime = 1f;
    float E10_startTime;
    float E10_timeSinceStarted;
    float E10_percentageComplete;
    
    void Start()
    {
        ultimoEjercicio = ejercicio;
    
        VectorDebugger.EnableCoordinates();
        VectorDebugger.AddVector(A, Color.white, "A");
        VectorDebugger.AddVector(B, Color.black, "B");
        VectorDebugger.AddVector(C, vectorColor, "C");
        VectorDebugger.EnableEditorView();
    
        E5_startTime = E10_startTime = Time.time;
    }
    
    void Update()
    {
        if (ultimoEjercicio != ejercicio)
            seCambioElEjercicio = true;
    
        if (AV3 != A)
            AV3 = new Vec3(A);
        if (BV3 != B)
            BV3 = new Vec3(B);
    
        switch (ejercicio)
        {
            case Ejercicios.Uno:
                {
                    CV3 = AV3 + BV3;
    
                    break;
                }
            case Ejercicios.Dos:
                {
                    CV3 = BV3 - AV3;
    
                    break;
                }
            case Ejercicios.Tres:
                {
                    CV3 = AV3;
                    CV3.Scale(BV3);
    
                    break;
                }
            case Ejercicios.Cuatro:
                {
                    CV3.x = AV3.z * BV3.y - AV3.y * BV3.z;
                    CV3.y = AV3.x * BV3.z - AV3.z * BV3.x;
                    CV3.z = AV3.y * BV3.x - AV3.x * BV3.y;
    
                    break;
                }
            case Ejercicios.Cinco:
                {
                    E5_timeSinceStarted = Time.time - E5_startTime;
                    E5_percentageComplete = E5_timeSinceStarted / E5_lerpTime;
    
                    CV3 = Vec3.Lerp(AV3, BV3, E5_percentageComplete);
    
                    if (CV3 == BV3)
                    {
                        E5_startTime = Time.time;
                        E5_timeSinceStarted = E5_percentageComplete = 0;
    
                        CV3 = AV3;
                    }
    
                    break;
                }
            case Ejercicios.Seis:
                {
                    if (AV3.x > BV3.x)
                        CV3.x = AV3.x;
                    else
                        CV3.x = BV3.x;
    
                    if (AV3.y > BV3.y)
                        CV3.y = AV3.y;
                    else
                        CV3.y = BV3.y;
    
                    if (AV3.z > BV3.z)
                        CV3.z = AV3.z;
                    else
                        CV3.z = BV3.z;
    
                    break;
                }
            case Ejercicios.Siete:
                {
                    CV3 = Vec3.Project(AV3, BV3);
    
                    break;
                }
            case Ejercicios.Ocho:
                {
    
                    break;
                }
            case Ejercicios.Nueve:
                {
                    Vec3 BPerpendicular = new Vec3(BV3.y, -BV3.x, 0);
                    CV3 = Vec3.Reflect(-AV3, BPerpendicular);
    
                    break;
                }
            case Ejercicios.Diez:
                {
                    E10_timeSinceStarted = Time.time - E10_startTime;
                    E10_percentageComplete = E10_timeSinceStarted / E10_lerpTime;
    
                    CV3 = Vec3.LerpUnclamped(BV3, AV3, E10_percentageComplete);
    
                    if (seCambioElEjercicio)
                    {
                        E10_startTime = Time.time;
                        E10_timeSinceStarted = E10_percentageComplete = 0;
    
                        CV3 = BV3;
                    }
    
                    break;
                }
        }
    
        if (seCambioElEjercicio)
        {
            ultimoEjercicio = ejercicio;
            seCambioElEjercicio = false;
        }
    
        A = AV3;
        B = BV3;
        C = CV3;
        MathDebbuger.VectorDebugger.UpdatePosition("A", A);
        MathDebbuger.VectorDebugger.UpdatePosition("B", B);
        MathDebbuger.VectorDebugger.UpdatePosition("C", C);
    }
}
