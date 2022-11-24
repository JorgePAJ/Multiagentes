using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    
    [SerializeField]
    private Carro[] _carros;
    private GameObject[] _carrosGO;
    private Frame[] _frames;
    private int carCounter;

    // Start is called before the first frame update
    void Start()
    {
        _carrosGO = new GameObject[_carros.Length];
        carCounter = 0;
        for(int i = 0; i < _carros.Length; i++)
        {
            _carrosGO[i] = CarPoolManager.Instance.ActivarObjeto(
                new Vector3(
                    _carros[i].x,
                    1,
                    _carros[i].z
                )
            );
        }
        
        
    }

    private void PosicionarCarros() 
    {
        for(int i = 0; i < _frames.Length; i++)
        {
            _carrosGO[carCounter].transform.position = new Vector3(
                _frames[i].cars[carCounter].x,
                1,
                _frames[i].cars[carCounter].z
                    
            );
            if(carCounter >= _frames[i].cars.Length - 1)
            {
                carCounter = 0;
            }
            else
            {
                carCounter++;
            }
        }
        
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void EscucharRequestSinArgumentos() {

        print("HUBO UN REQUEST MUY INTERESANTE!");
    }

    public void EscucharRequestConArgumentos(GeneralInfo datos){
        print("DATOS: " + datos);
        
        // actualizar arreglo _carros de esta clase con 
        // los carros que recibo de "datos"
        _carros = datos.cars;
        _frames = datos.frames;
        // invocar PosicionarCarros()
        PosicionarCarros();
    }
}
