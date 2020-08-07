using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CustomMath
{
    public class Nave : MonoBehaviour
    {
        float speed = 1.0F;
        float startTime;
        float journeyLength;
        float distCovered;
        float fractionOfJourney;

        Vec3 positionVec3;
        Vec3 target;

        List<GameObject> proyectiles = new List<GameObject>();

        void Start()
        {
            positionVec3 = new Vec3(transform.position);
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                startTime = Time.time;
                target = new Vec3(Input.mousePosition);
                journeyLength = Vec3.Magnitude(target - positionVec3);

                proyectiles.Add(GameObject.CreatePrimitive(PrimitiveType.Sphere));
                proyectiles[0].transform.position = transform.position;
                proyectiles[0].transform.localScale /= 10;
            }

            if (proyectiles[0])
            {
                distCovered = (Time.time - startTime) * speed;

                fractionOfJourney = distCovered / journeyLength;

                proyectiles[0].transform.position = Vec3.Lerp(positionVec3, target, 0.001f);
            }
        }
    }
}
