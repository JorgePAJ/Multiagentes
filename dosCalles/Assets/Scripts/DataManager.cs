using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    [SerializeField]
    private Carro[] _carros;
    WaitForSeconds delay = new WaitForSeconds(0.5f);
    [SerializeField]
    private Semaphore[] _semaforos;

    public GameObject[] _carrosGO;
    [SerializeField] GameObject[] _semaforosGO;
    private Frame[] _frames;

    [SerializeField] Material[] materiales;

    // Start is called before the first frame update
    private void Inicio()
    {
        _carrosGO = new GameObject[ _carros.Length];
        //_semaforosGO = new GameObject[_semaforos.Length];
        
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
                _carros[i].x, 0.1f, _carros[i].z);

            _carrosGO[i].transform.rotation = Quaternion.Euler(
                new Vector3(0, _carros[i].dir, 0));
        }
    }

    private void EstadoSemaforos() // TODO: AUN NO FUNCIONA ESTOY PROBANDOLO
    {
        MeshRenderer rendererActual;

        for(int i = 0; i < _semaforos.Length; i++)
        {

            rendererActual = _semaforosGO[i].GetComponent<MeshRenderer>();
            Material[] tempMaterialArray = rendererActual.materials;

            print(_semaforos[i].state);

            if (_semaforos[i].state == 0)
            {
                // luces verdes encendidas
                tempMaterialArray[1] = materiales[1];
                tempMaterialArray[4] = materiales[1];

                // luces amarillas apagadas
                tempMaterialArray[2] = materiales[2];
                tempMaterialArray[5] = materiales[2];

                // luces rojas apagadas
                tempMaterialArray[3] = materiales[4];
                tempMaterialArray[6] = materiales[4];
            }
            else if(_semaforos[i].state == 1)
            {
                // luces verdes apagadas
                tempMaterialArray[1] = materiales[0];
                tempMaterialArray[4] = materiales[0];

                // luces amarillas prendidas
                tempMaterialArray[2] = materiales[3];
                tempMaterialArray[5] = materiales[3];

                // luces rojas apagadas
                tempMaterialArray[3] = materiales[4];
                tempMaterialArray[6] = materiales[4];
            }
            else if (_semaforos[i].state == 2)
            {
                // luces verdes apagadas
                tempMaterialArray[1] = materiales[0];
                tempMaterialArray[4] = materiales[0];

                // luces amarillas apagadas
                tempMaterialArray[2] = materiales[2];
                tempMaterialArray[5] = materiales[2];

                // luces rojas prendidas
                tempMaterialArray[3] = materiales[5];
                tempMaterialArray[6] = materiales[5];
            }
            rendererActual.materials = tempMaterialArray;

        }
    }

    IEnumerator CambiarPosicion(GeneralInfo datos)
    {
        for (int i = 0; i < datos.frames.Length; i++)
        {
            _carros = datos.frames[i].cars;
            _semaforos = datos.frames[i].semaphores;
            PosicionarCarros();
            EstadoSemaforos(); // TODO: AUN NO FUNCIONA ESTOY PROBANDOLO
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