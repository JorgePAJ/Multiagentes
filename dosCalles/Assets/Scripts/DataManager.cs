using System.Collections;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    [SerializeField]
    float delayTime = 1.0f;
    [SerializeField]
    private Carro[] _carros;
    WaitForSeconds delay = new WaitForSeconds(0.5f);
    [SerializeField]
    private Semaphore[] _semaforos;

    public GameObject[] _carrosGO;
    private GameObject[] _semaforosGO;
    private Frame[] _frames;
    private int carCounter;
    private int semaforoCounter;

    // Start is called before the first frame update
    private void Inicio()
    {
        _carrosGO = new GameObject[ _carros.Length];
        // _semaforosGO = new GameObject[_semaforos.Length];
        carCounter = 0;
        
        for (int i = 0; i < _carros.Length; i++)
        {
            _carrosGO[i] = CarPoolManager.Instance.ActivarObjeto(Vector3.zero);

        }

        
    }

    private void PosicionarCarros()
    {
      
        print(_carrosGO.Length + "carritos");
        for (int i = 0; i < _carros.Length; i++)
        {
           
            _carrosGO[i].transform.position = new Vector3(
                _carros[i].x, 1, _carros[i].z);
        }
    }

    // private void PosicionarSemaforos()
    // {
    //
    //     for (int i = 0; i < _frames.Length; i++)
    //     {
    //         _semaforosGO[semaforoCounter].transform.position = new Vector3(
    //             _frames[i].semaphores[semaforoCounter].x,
    //             1,
    //             _frames[i].semaphores[semaforoCounter].y
    //
    //         );
    //         if (semaforoCounter >= _frames[i].semaphores.Length - 1)
    //         {
    //             semaforoCounter = 0;
    //         }
    //         else
    //         {
    //             semaforoCounter++;
    //         }
    //
    //     }
    // }

    IEnumerator CambiarPosicion(GeneralInfo datos)
    {
        for (int i = 0; i < datos.frames.Length; i++)
        {
            _carros = datos.frames[i].cars;
            PosicionarCarros();
            yield return new WaitForSeconds(0.01f);
        }
    }


    public void EscucharRequestSinArgumentos()
    {
        print("HUBO UN REQUEST MUY INTERESANTE!");
    }

    public void EscucharRequestConArgumentos(GeneralInfo datos)
    {
        print("DATOS: " + datos);


        _carros = datos.cars;
        _semaforos = datos.semaphores;
        _frames = datos.frames;
        // invocar PosicionarCarros()
        Inicio();
        StartCoroutine(CambiarPosicion(datos));
        // CambiarPosicion(datos);
        // PosicionarSemaforos();
    }
}