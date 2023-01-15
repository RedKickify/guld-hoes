using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{

    private Transform TileSprite;
    private ParticleSystem ShakeParticles;

    public void SelfDestruct(float timer)
    {
        TileSprite = this.transform.Find("Sprite");
        ShakeParticles = this.transform.Find("ShakeParticles").GetComponent<ParticleSystem>();
        StartCoroutine(CountDown(timer));
    }

    IEnumerator CountDown(float timer)
    {
        yield return new WaitForSeconds(timer);

        StartCoroutine(StartFall());
    }

    IEnumerator StartFall()
    {
        //shake
        StartCoroutine(Shake());
        //particles
        ShakeParticles.Play();

        yield return new WaitForSeconds(5);

        //destroy
        Destroy(this.gameObject);
    }

    IEnumerator Shake()
    {
        var speed = 20.0f; //how fast it shakes
        var amount = 0.01f; //how much it shakes

        while(true)
        {
            float x = this.transform.position.x + Mathf.Sin(Time.time * speed) * amount;
            float y = this.transform.position.y + Mathf.Sin(Time.time * speed) * amount;
            TileSprite.position = new Vector3(x, y);
            yield return new WaitForFixedUpdate();
        }
    }

}
