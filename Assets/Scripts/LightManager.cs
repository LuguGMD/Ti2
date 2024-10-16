using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightManager : MonoBehaviour
{
    public GameManager gm;
    public Light sceneLight;
    public List<Material> materials;
    
    public Color currentColor = Color.white;
    public Gradient gradient;
    [Range(0f, 1f)]
    public float gradientValue;

    public bool change = false;

    public float speed;

    private void Awake()
    {
        for (int i = 0; i < materials.Count; i++)
        {
            materials[i].SetFloat("_NightTime", 0f);
        }
    }

    private void Update()
    {
        if (gradientValue != 1 && gradientValue != 0)
        {
            gradientValue += change == true ? 0.1f * Time.deltaTime * speed : -0.1f * Time.deltaTime * speed;
            gradientValue = Mathf.Clamp(gradientValue, 0f, 1f);

            currentColor = gradient.Evaluate(gradientValue);
            sceneLight.color = currentColor;
            
            for(int i = 0; i < materials.Count; i++) 
            {
                materials[i].SetFloat("_NightTime", gradientValue);   
            }

        }
    }

    private void OnEnable()
    {
        gm.powerupStart += ChangeColor;
        gm.powerupEnd += DefaultColor;
    }

    private void OnDisable()
    {
        gm.powerupStart -= ChangeColor;
        gm.powerupEnd -= DefaultColor;
    }

    private void OnDestroy()
    {
        for (int i = 0; i < materials.Count; i++)
        {
            materials[i].SetFloat("_NightTime", 0f);
        }
    }

    public void ChangeColor()
    {
        change = true;
        gradientValue += 0.1f;
    }

    public void DefaultColor()
    {
        change = false;
        gradientValue -= 0.1f;
    }

}
