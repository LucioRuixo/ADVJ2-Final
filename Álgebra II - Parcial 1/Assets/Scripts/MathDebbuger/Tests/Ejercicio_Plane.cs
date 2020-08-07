using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomMath;

public class Ejercicio_Plane : MonoBehaviour
{
    public Transform transformIzquierda;
    public Transform transformDerecha;
    public Transform transformArriba;
    public Transform transformAbajo;
    public Transform transformAdelante;
    public Transform transformAtras;

    public Transform esfera;
    Vec3 posicionEsferaV3;
    [Header("Esfera: ")]
    public Vector3 posicion;

    Enalp izquierda;
    Enalp derecha;
    Enalp arriba;
    Enalp abajo;
    Enalp adelante;
    Enalp atras;

    bool adentroEnX;
    bool adentroEnY;
    bool adentroEnZ;
    [SerializeField] bool adentro;

    void Start()
    {
        Vec3 PIzquierdaNormal = new Vec3(transformIzquierda.up);
        Vec3 PDerechaNormal = new Vec3(transformDerecha.up);
        Vec3 PArribaNormal = new Vec3(transformArriba.up);
        Vec3 PAbajoNormal = new Vec3(transformAbajo.up);
        Vec3 PAdelanteNormal = new Vec3(transformAdelante.up);
        Vec3 PAtrasNormal = new Vec3(transformAtras.up);

        Vec3 PIzquierdaPunto = new Vec3(transformIzquierda.localPosition);
        Vec3 PDerechaPunto = new Vec3(transformDerecha.localPosition);
        Vec3 PArribaPunto = new Vec3(transformArriba.localPosition);
        Vec3 PAbajoPunto = new Vec3(transformAbajo.localPosition);
        Vec3 PAdelantePunto = new Vec3(transformAdelante.localPosition);
        Vec3 PAtrasPunto = new Vec3(transformAtras.localPosition);

        izquierda = new Enalp(PIzquierdaNormal, PIzquierdaPunto);
        derecha = new Enalp(PDerechaNormal, PDerechaPunto);
        arriba = new Enalp(PArribaNormal, PArribaPunto);
        abajo = new Enalp(PAbajoNormal, PAbajoPunto);
        adelante = new Enalp(PAdelanteNormal, PAdelantePunto);
        atras = new Enalp(PAtrasNormal, PAtrasPunto);

        posicionEsferaV3 = new Vec3(esfera.localPosition);
        posicion = esfera.localPosition;
    }

    void Update()
    {
        esfera.localPosition = posicion;

        if (posicionEsferaV3 != esfera.localPosition)
            posicionEsferaV3 = new Vec3(esfera.localPosition);

        if (izquierda.GetSide(posicionEsferaV3) && derecha.GetSide(posicionEsferaV3))
            adentroEnX = true;
        else
            adentroEnX = false;

        if (arriba.GetSide(posicionEsferaV3) && abajo.GetSide(posicionEsferaV3))
            adentroEnY = true;
        else
            adentroEnY = false;

        if (adelante.GetSide(posicionEsferaV3) && atras.GetSide(posicionEsferaV3))
            adentroEnZ = true;
        else
            adentroEnZ = false;

        if (adentroEnX && adentroEnY && adentroEnZ)
            adentro = true;
        else
            adentro = false;
    }
}