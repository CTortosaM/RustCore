using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objetivo : MonoBehaviour
{
    public float health = 50f;

    public GameObject efectoExplosion;

    public ParticleSystem explosion;


    public void TakeImpact (float amount, RaycastHit hit)
    {
        health -= amount;
        Debug.Log("Heath = " + health.ToString());
        if (health <= 0)
        {
            Debug.Log("Explosion ordered");

            Exploit(hit);

        }
    }
    private void Exploit(RaycastHit hit)
    {
        explosion.Play();
        GameObject explosionado = Instantiate(efectoExplosion, gameObject.transform.position, gameObject.transform.localRotation);
//        SetMaterialTransparent();
//        iTween.FadeTo(gameObject, 0, 0.001f);
        Destroy(gameObject, 0.01f);  // Esto destruye la pelota manten-lo
      //  Destroy(explosionado, 4.5f);
    }
/*
    private void SetMaterialTransparent()
    {
        foreach (Material m in gameObject.GetComponent<Renderer>().materials)

        {

            m.SetFloat("_Mode", 2);

            m.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);

            m.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);

            m.SetInt("_ZWrite", 0);

            m.DisableKeyword("_ALPHATEST_ON");

            m.EnableKeyword("_ALPHABLEND_ON");

            m.DisableKeyword("_ALPHAPREMULTIPLY_ON");

            m.renderQueue = 3000;

        }
    }
    */
}
