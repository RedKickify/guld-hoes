using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFruitPickup : MonoBehaviour
{

    [SerializeField] private LineRenderer _tongue;
    [SerializeField] private float _eatRadius;
    private GameObject _currentFruit;
    [SerializeField] private float _pullSpeed;
    [SerializeField] private AudioSource _eatingSound;
    void Update()
    {
        if (GameManager.instance.gameState != GameState.Main) return;

        _tongue.SetPosition(0, this.transform.position);

        if (_currentFruit != null)
        {
            _tongue.SetPosition(1, _currentFruit.transform.position);
            _currentFruit.transform.position = Vector3.Lerp(_currentFruit.transform.position, this.transform.position, _pullSpeed * Time.deltaTime);
            if (Vector3.Distance(this.transform.position,_currentFruit.transform.position) < 0.1f)
            {
                GameManager.instance.FruitEaten();
                _eatingSound.Play();
                Destroy(_currentFruit);
                _currentFruit = null;
            }
        }

        if (_currentFruit == null)
        {
            _tongue.SetPosition(1, this.transform.position);
            _currentFruit = CheckForFruit();
        }
    }

    private GameObject CheckForFruit()
    {
        Collider2D[] colls = Physics2D.OverlapCircleAll(this.transform.position, _eatRadius);
        foreach (Collider2D col in colls)
        {
            if (col.tag == "Fruit")
            {
                col.gameObject.GetComponent<Animator>().SetBool("floating", false);
                return col.gameObject;
            }
        }
        return null;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _eatRadius);
    }
}
