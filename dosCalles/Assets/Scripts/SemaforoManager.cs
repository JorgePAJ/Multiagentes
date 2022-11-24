using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SemaforoManager: MonoBehaviour
{
    public static SemaforoManager Instance
    {
        get;
        private set;
    }

    [SerializeField] private GameObject _semaforoOriginal;

    [SerializeField]
    private int _numSemaforos;

    private Queue<GameObject> _pool;

    void Awake()
    {
        //print("AWAKE");

        // mecanismo de singleton correctivo
        // osea, si ya existe pelas
        if (Instance != null)
        {
            // significa que ya fue asignada
            Destroy(gameObject);
            return;
        }

        Instance = this;

        //print("START");

        // vamos a crear el pool
        _pool = new Queue<GameObject>();
        for (int i = 0; i < _numSemaforos; i++)
        {

            GameObject nuevoObjeto = Instantiate<GameObject>(_semaforoOriginal);
            _pool.Enqueue(nuevoObjeto);
            nuevoObjeto.SetActive(false);
        }
    }
    public GameObject ActivarObjeto(Vector3 posicion)
    {

        print(posicion);
        // revisar si queue tiene objetos disponibles
        if (_pool == null || _pool.Count == 0)
        {
            Debug.LogError("SE ACABO EL POOL, YA TRANQUILIZATE");
            return null;
        }

        GameObject objetoActivado = _pool.Dequeue();
        objetoActivado.SetActive(true);
        objetoActivado.transform.position = posicion;
        return objetoActivado;
    }

    public void DesactivarObjeto(GameObject objetoADesactivar)
    {
        objetoADesactivar.SetActive(false);
        _pool.Enqueue(objetoADesactivar);
    }
}
