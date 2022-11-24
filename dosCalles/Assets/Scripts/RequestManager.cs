using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;


[System.Serializable]
public class RequestConArgumentos : UnityEvent<GeneralInfo> {}
public class RequestManager : MonoBehaviour
{

    [SerializeField]
    private UnityEvent _requestRecibidaSinArgumentos;

    [SerializeField]
    private RequestConArgumentos _requestConArgumentos;
    
    [SerializeField]
    private float _esperaEntreRequests = 1;
    
    public TextAsset jsonFile;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(HacerRequest());

        // _requestRecibidaSinArgumentos += funcion();
    }

    IEnumerator HacerRequest() {
    //load json from scripts folder
    
    if (jsonFile == null) {
        Debug.Log("Error: No se pudo cargar el archivo");
    }
    else {
        Debug.Log("Archivo cargado");
    }
    string json = jsonFile.text;
    print(json);
    
            //revisar si no hubo broncas

            
            if(jsonFile != null){

                GeneralInfo generalInfo = JsonUtility.FromJson<GeneralInfo>(json);
                print(generalInfo);
                
                _requestRecibidaSinArgumentos?.Invoke();
                _requestConArgumentos?.Invoke(generalInfo);
            }
            yield return new WaitForSeconds(_esperaEntreRequests);
        }
    }

