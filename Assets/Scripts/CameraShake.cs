/* This code is an adaptation of the shake system implemented in the MilkShake Camera Shake pack available at the Asset store
   link = https://assetstore.unity.com/packages/tools/camera/milkshake-camera-shaker-165604 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float strength;
    public float roughness;
    public float fadeInTime;
    public float fadeOutTime;
    public int baseSeed = 1;

    public Vector3 positionInfluence;
    public Vector3 rotationInfluence;

    private float seed1;
    private float seed2;
    private float seed3;
    private float noiseTimer;

    private float fadeTimer;

    private ShakeState State;


    public enum ShakeState
    {
        FadingIn = 0,
        FadingOut = 1,
        Stopped = 2
    }

    public IEnumerator Shake()
    {
        State = ShakeState.FadingIn;

        seed1 = baseSeed / 2f;
        seed2 = baseSeed / 3f;
        seed3 = baseSeed / 4f;
        noiseTimer = baseSeed;
       

        while (State != ShakeState.Stopped)
        {
            Vector3 newPosition = getPositionShake();
            Vector3 newRotation = getRotationShake();

            //Update noise timer
            noiseTimer += 1 * Time.deltaTime * roughness * fadeTimer;

            //Update fade timer
            if (State == ShakeState.FadingIn)
            {
                if (fadeInTime > 0)
                    fadeTimer += Time.deltaTime / fadeInTime;
                else
                    fadeTimer = 1;
            }
            else if (State == ShakeState.FadingOut)
            {
                if (fadeOutTime > 0)
                    fadeTimer -= Time.deltaTime / fadeOutTime;
                else
                    fadeTimer = 0;
            }
            fadeTimer = Mathf.Clamp01(fadeTimer);

            //Update the state if needed
            if (fadeTimer == 1)
            {
                State = ShakeState.FadingOut;
            }
            else if (fadeTimer == 0)
            {
                State = ShakeState.Stopped;
            }

            transform.localPosition = newPosition;
            transform.localEulerAngles = newRotation;

            yield return null;
        }
    }

    public Vector3 getPositionShake()
    {
        Vector3 v = Vector3.zero;

        v.x = getNoise(noiseTimer + seed1, baseSeed);
        v.y = getNoise(baseSeed, noiseTimer);
        v.z = getNoise(seed3 + noiseTimer, baseSeed + noiseTimer);

        return Vector3.Scale(v * strength * fadeTimer, positionInfluence);
    }

    private Vector3 getRotationShake()
    {
        Vector3 v = Vector3.zero;

        v.x = getNoise(noiseTimer - baseSeed, seed3);
        v.y = getNoise(baseSeed, noiseTimer + seed2);
        v.z = getNoise(baseSeed + noiseTimer, seed1 + noiseTimer);

        return Vector3.Scale(v * strength * fadeTimer, rotationInfluence);
    }

    private float getNoise(float x, float y)
    {
        return (Mathf.PerlinNoise(x, y) - 0.5f) * 2f;
    }


}
