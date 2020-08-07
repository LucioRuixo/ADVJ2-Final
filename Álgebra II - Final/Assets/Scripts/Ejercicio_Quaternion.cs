using UnityEngine;
using MathDebbuger;
using CustomMath;
using System.Collections.Generic;

public class Ejercicio_Quaternion : MonoBehaviour
{
    public enum Ejercicios
    {
        Uno,
        Dos,
        Tres,
    }
    public Ejercicios ejercicio = Ejercicios.Uno;
    Ejercicios ultimoEjercicio;

    public float angle;

    public GameObject go;

    Transform E1;
    Transform E2_1;
    Transform E2_2;
    Transform E2_3;
    Transform E3_1;
    Transform E3_2;
    Transform E3_3;

    Quaternion rotacion;

    List<Vector3> E2;
    List<Vector3> E3;

    void Start()
    {
        ultimoEjercicio = ejercicio;

        E1 = Instantiate(go, transform).transform;
        E2_1 = Instantiate(go, transform).transform;
        E2_2 = Instantiate(go, E2_1).transform;
        E2_3 = Instantiate(go, E2_2).transform;
        E3_1 = Instantiate(go, transform).transform;
        E3_2 = Instantiate(go, E3_1).transform;
        E3_3 = Instantiate(go, E3_2).transform;

        E2_2.rotation *= Quaternion.AngleAxis(-90f, E2_2.right);

        rotacion = Quaternion.AngleAxis(angle, new Vec3(E1.up));

        E2 = new List<Vector3>();
        E2.Add(Vector3.zero);
        E2.Add(E2_1.transform.forward * 10f);
        E2.Add(E2_1.transform.forward * 10f + E2_2.transform.forward * 10f);
        E2.Add(E2_1.transform.forward * 10f + E2_2.transform.forward * 10f + E2_3.transform.forward * 10f);

        E3 = new List<Vector3>();
        E3.Add(Vector3.zero);
        E3.Add(E3_1.transform.forward * 10f);
        E3.Add(E3_1.transform.forward * 10f + E3_2.transform.forward * 10f);
        E3.Add(E3_1.transform.forward * 10f + E3_2.transform.forward * 10f + E3_3.transform.forward * 10f);

        VectorDebugger.EnableCoordinates();
        VectorDebugger.AddVector(E1.forward, Color.red, "E1");
        VectorDebugger.AddVectorsSecuence(E2, true, Color.blue, "E2");
        VectorDebugger.AddVectorsSecuence(E3, true, Color.yellow, "E3");
        VectorDebugger.EnableEditorView();
    }

    void Update()
    {
        if (ultimoEjercicio != ejercicio)
        {
            ActivarEjercicio();
            ultimoEjercicio = ejercicio;
        }

        switch (ejercicio)
        {
            case Ejercicios.Uno:
                {
                    E1.rotation *= rotacion;

                    VectorDebugger.UpdatePosition("E1", E1.forward * 10f);
                    break;
                }
            case Ejercicios.Dos:
                {
                    E2_1.rotation *= rotacion;
                    E2_3.rotation = E2_1.rotation;

                    E2[1] = E2_1.transform.forward * 10f;
                    E2[2] = E2_1.transform.forward * 10f + E2_2.transform.forward * 10f;
                    E2[3] = E2_1.transform.forward * 10f + E2_2.transform.forward * 10f + E2_3.transform.forward * 10f;

                    VectorDebugger.UpdatePositionsSecuence("E2", E2);
                    break;
                }
            case Ejercicios.Tres:
                {
                    E3_1.rotation *= rotacion;
                    E3_3.rotation = E3_1.rotation;

                    E3[1] = E3_1.transform.forward * 10f;
                    E3[2] = E3_1.transform.forward * 10f + E3_2.transform.forward * 10f;
                    E3[3] = E3_1.transform.forward * 10f + E3_2.transform.forward * 10f + E3_3.transform.forward * 10f;

                    VectorDebugger.UpdatePositionsSecuence("E3", E3);
                    break;
                }
        }
    }

    void ActivarEjercicio()
    {
        switch (ejercicio)
        {
            case Ejercicios.Uno:
                {
                    VectorDebugger.TurnOnVector("E1");
                    VectorDebugger.TurnOffVector("E2");
                    VectorDebugger.TurnOffVector("E3");

                    break;
                }
            case Ejercicios.Dos:
                {
                    VectorDebugger.TurnOffVector("E1");
                    VectorDebugger.TurnOnVector("E2");
                    VectorDebugger.TurnOffVector("E3");

                    break;
                }
            case Ejercicios.Tres:
                {
                    VectorDebugger.TurnOffVector("E1");
                    VectorDebugger.TurnOffVector("E2");
                    VectorDebugger.TurnOnVector("E3");

                    break;
                }
        }
    }
}