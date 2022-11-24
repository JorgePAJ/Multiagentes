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

    private GameObject[] _carrosGO;
    private GameObject[] _semaforosGO;
    private Frame[] _frames;
    private int carCounter;
    private int semaforoCounter;

    // Start is called before the first frame update
    private void Start()
    {
        _carrosGO = new GameObject[_carros.Length];
        _semaforosGO = new GameObject[_semaforos.Length];
        carCounter = 0;
        for (int i = 0; i < _carros.Length; i++)
        {
            _carrosGO[i] = CarPoolManager.Instance.ActivarObjeto(
                new Vector3(
                    _carros[i].x,
                    1,
                    _carros[i].z
                )
            );
        }

        for (int i = 0; i < _semaforos.Length; i++)
        {
            _semaforosGO[i] = SemaforoManager.Instance.ActivarObjeto(
                new Vector3(
                    _semaforos[i].x,
                    1,
                    _semaforos[i].y
                    )
                );
        }
        //StartCoroutine(CambiarPosicion());
    }

    private void PosicionarCarros()
    {
        for (int i = 0; i < _frames.Length; i++)
        {
            _carrosGO[carCounter].transform.position = new Vector3(
                _frames[i].cars[carCounter].x,
                1,
                _frames[i].cars[carCounter].z

            );
            if (carCounter >= _frames[i].cars.Length - 1)
            {
                carCounter = 0;
            }
            else
            {
                carCounter++;
            }
        }
    }

    private void PosicionarSemaforos()
    {

        for (int i = 0; i < _frames.Length; i++)
        {
            _semaforosGO[semaforoCounter].transform.position = new Vector3(
                _frames[i].semaphores[semaforoCounter].x,
                1,
                _frames[i].semaphores[semaforoCounter].y

            );
            if (semaforoCounter >= _frames[i].semaphores.Length - 1)
            {
                semaforoCounter = 0;
            }
            else
            {
                semaforoCounter++;
            }

        }
    }

    IEnumerator CambiarPosicion()
    {
        for (int i = 0; i < _frames.Length; i++)
        {
            _semaforosGO[semaforoCounter].transform.position = new Vector3(
                _frames[i].semaphores[semaforoCounter].x,
                1,
                _frames[i].semaphores[semaforoCounter].y

            );
            if (semaforoCounter >= _frames[i].semaphores.Length - 1)
            {
                semaforoCounter = 0;
            }
            else
            {
                semaforoCounter++;
            }
            
            yield return delay;
        }
        for (int i = 0; i < _frames.Length; i++)
        {
            _semaforosGO[semaforoCounter].transform.position = new Vector3(
                _frames[i].semaphores[semaforoCounter].x,
                1,
                _frames[i].semaphores[semaforoCounter].y

            );
            if (semaforoCounter >= _frames[i].semaphores.Length - 1)
            {
                semaforoCounter = 0;
            }
            else
            {
                semaforoCounter++;
            }
        }
    }


    public void EscucharRequestSinArgumentos()
    {
        print("HUBO UN REQUEST MUY INTERESANTE!");
    }

    public void EscucharRequestConArgumentos(GeneralInfo datos)
    {
        print("DATOS: " + datos);

        // actualizar arreglo _carros de esta clase con
        // los carros que recibo de "datos"
        _carros = datos.cars;
        _semaforos = datos.semaphores;
        _frames = datos.frames;
        // invocar PosicionarCarros()
        PosicionarCarros();
        PosicionarSemaforos();
    }
}