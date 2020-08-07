using UnityEngine;
using MathDebbuger;
using CustomMath;

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

    Quaternion rotacionE1;
    Quaternion rotacionE2_1;
    Quaternion rotacionE2_2;
    Quaternion rotacionE2_3;
    Quaternion rotacionE3_1;
    Quaternion rotacionE3_2;
    Quaternion rotacionE3_3;

    void Start()
    {
        ultimoEjercicio = ejercicio;

        E1 = Instantiate(go, transform).transform;
        E2_1 = Instantiate(go, transform).transform;
        E2_2 = Instantiate(go, E2_1.transform.forward, Quaternion.identity, E2_1).transform;
        E2_3 = Instantiate(go, E2_2.transform.forward, Quaternion.identity, E2_2).transform;
        E3_1 = Instantiate(go, transform).transform;
        E3_2 = Instantiate(go, E3_1.transform.forward, Quaternion.identity, E3_1).transform;
        E3_3 = Instantiate(go, E3_2.transform.forward, Quaternion.identity, E3_2).transform;

        rotacionE1 = Quaternion.AngleAxis(angle, new Vec3(E1.up));
        rotacionE2_1 = Quaternion.AngleAxis(angle, new Vec3(E2_1.up));
        rotacionE2_2 = Quaternion.AngleAxis(angle, new Vec3(E2_2.up));
        rotacionE2_3 = Quaternion.AngleAxis(angle, new Vec3(E2_3.up));
        rotacionE3_1 = Quaternion.AngleAxis(angle, new Vec3(E3_1.up));
        rotacionE3_2 = Quaternion.AngleAxis(angle, new Vec3(E3_2.up));
        rotacionE3_3 = Quaternion.AngleAxis(angle, new Vec3(E3_3.up));

        VectorDebugger.EnableCoordinates();
        VectorDebugger.AddVector(E1.forward, Color.red, "E1");
        VectorDebugger.AddVector(E2_1.forward, Color.yellow, "E2_1");
        VectorDebugger.AddVector(E2_2.forward, Color.yellow, "E2_2");
        VectorDebugger.AddVector(E2_3.forward, Color.yellow, "E2_3");
        VectorDebugger.AddVector(E3_1.forward, Color.blue, "E3_1");
        VectorDebugger.AddVector(E3_2.forward, Color.blue, "E3_2");
        VectorDebugger.AddVector(E3_3.forward, Color.blue, "E3_3");
        VectorDebugger.EnableEditorView();
    }

    void Update()
    {
        Debug.Log(ejercicio);
        if (ultimoEjercicio != ejercicio)
        {
            ActivarEjercicio();
            ultimoEjercicio = ejercicio;
        }

        switch (ejercicio)
        {
            case Ejercicios.Uno:
                {
                    E1.rotation *= rotacionE1;

                    MathDebbuger.VectorDebugger.UpdatePosition("E1", E1.forward * 10f);
                    break;
                }
            case Ejercicios.Dos:
                {


                    MathDebbuger.VectorDebugger.UpdatePosition("E2_1", E2_1.forward);
                    MathDebbuger.VectorDebugger.UpdatePosition("E2_2", E2_2.forward);
                    MathDebbuger.VectorDebugger.UpdatePosition("E2_3", E2_3.forward);
                    break;
                }
            case Ejercicios.Tres:
                {


                    MathDebbuger.VectorDebugger.UpdatePosition("E3_1", E3_1.forward);
                    MathDebbuger.VectorDebugger.UpdatePosition("E3_2", E3_2.forward);
                    MathDebbuger.VectorDebugger.UpdatePosition("E3_3", E3_3.forward);
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
                    VectorDebugger.TurnOffVector("E2_1");
                    VectorDebugger.TurnOffVector("E2_2");
                    VectorDebugger.TurnOffVector("E2_3");
                    VectorDebugger.TurnOffVector("E3_1");
                    VectorDebugger.TurnOffVector("E3_2");
                    VectorDebugger.TurnOffVector("E3_3");

                    break;
                }
            case Ejercicios.Dos:
                {
                    VectorDebugger.TurnOffVector("E1");
                    VectorDebugger.TurnOnVector("E2_1");
                    VectorDebugger.TurnOnVector("E2_2");
                    VectorDebugger.TurnOnVector("E2_3");
                    VectorDebugger.TurnOffVector("E3_1");
                    VectorDebugger.TurnOffVector("E3_2");
                    VectorDebugger.TurnOffVector("E3_3");

                    break;
                }
            case Ejercicios.Tres:
                {
                    VectorDebugger.TurnOffVector("E1");
                    VectorDebugger.TurnOffVector("E2_1");
                    VectorDebugger.TurnOffVector("E2_2");
                    VectorDebugger.TurnOffVector("E2_3");
                    VectorDebugger.TurnOnVector("E3_1");
                    VectorDebugger.TurnOnVector("E3_2");
                    VectorDebugger.TurnOnVector("E3_3");

                    break;
                }
        }
    }
}