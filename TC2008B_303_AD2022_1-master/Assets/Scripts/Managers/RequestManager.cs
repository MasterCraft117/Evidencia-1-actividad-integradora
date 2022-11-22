//using System;
//using System;
//using System;
//using System;
//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Events;
using System;
//using System;
//using System.Runtime.InteropServices;
//using System.Diagnostics;
//using System.Security.AccessControl;
//using System;
//using System.Diagnostics;

// 3 cosas 
// 1. parsing de JSON
// 2. corrutinas 
// 3. eventos de unity (puede que quede mañana)

[Serializable]
public class RequestConArg : UnityEvent<ListaCarro> { }

public class RequestManager : MonoBehaviour
{

    private IEnumerator enumeratorCorrutina;
    private Coroutine corrutina;

    private IEnumerator enumeratorCorrutina1;
    private Coroutine corrutina1;

    private IEnumerator enumeratorCorrutina2;
    private Coroutine corrutina2;

    private IEnumerator enumeratorCorrutina3;
    private Coroutine corrutina3;

    private string json;
    public ListaCarro carros;

    public ListaCarro darCarros()
    {
        return carros;
    }

    [SerializeField]
    private RequestConArg _requestConArgumento;

    [SerializeField]
    private UnityEvent _requestSinArgumento;

    [SerializeField]
    private string _urlRequest = "http://127.0.0.1:5000/";


    void Start(){

        json = "{\"carros\": [" +
        "{\"id\": 0, \"x\": 0, \"y\": 0, \"z\": 0}," +
        "{\"id\": 1, \"x\": 0, \"y\": 0, \"z\": 0}," +
        "{\"id\": 2, \"x\": 0, \"y\": 0, \"z\": 0}," +
        "{\"id\": 3, \"x\": 0, \"y\": 0, \"z\": 0}," +
        "{\"id\": 4, \"x\": 0, \"y\": 0, \"z\": 0}" +
        "]}";

        carros = JsonUtility.FromJson<ListaCarro>(json);
        for(int i = 0; i < carros.carros.Length; i++){
            print(carros.carros[i].x + " , "  +
                    carros.carros[i].y + " , "  +
                    carros.carros[i].z);
        }
        
        enumeratorCorrutina = EjemploCorrutina(); 
        corrutina = StartCoroutine(enumeratorCorrutina);

        enumeratorCorrutina1 = CreadorServer();
        corrutina1 = StartCoroutine(enumeratorCorrutina1);

        enumeratorCorrutina2 = ServerShower();
        corrutina2 = StartCoroutine(enumeratorCorrutina2);

        enumeratorCorrutina3 = Request();
        corrutina3 = StartCoroutine(enumeratorCorrutina3);

        _requestSinArgumento.AddListener(EscuchaDummy);

    }

    void EscuchaDummy()
    {
        print("Metodo agregado a evento programaticamente");
    }

    void Update() {

        if(Input.GetKeyDown(KeyCode.A)){
            StopAllCoroutines();
        }

        if(Input.GetKeyDown(KeyCode.B)){
            StopCoroutine(enumeratorCorrutina);
        }

        if(Input.GetKeyDown(KeyCode.C)){
            StopCoroutine(corrutina);
        }
    }

    // CORRUTINAS 
    // mecanismo de manejo de pseudo concurrencia en Unity
    // NO es un hilo PERO se comporta como uno
    IEnumerator EjemploCorrutina() {
    
        while(true){
            yield return new WaitForSeconds(60);
            //print("HOLA SOY UNA CORRUTINA");
        }
    }

    IEnumerator CreadorServer()
    {

        while (true)
        {
            yield return new WaitForSeconds(0.5f);

            UnityWebRequest www = UnityWebRequest.Get(_urlRequest);

            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("NO");
            } else
            {
                json = www.downloadHandler.text;
            }

        }
        

    }

    IEnumerator ServerShower()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);

            carros = JsonUtility.FromJson<ListaCarro>(json);

            for (int i = 0; i < carros.carros.Length; i++)
            {
                print(carros.carros[i].x + " , " +
                        carros.carros[i].y + " , " +
                        carros.carros[i].z);
            }

            CarDataManager.Instance.PosicionarCarros();

        }
    }

    IEnumerator Request()
    {
        while (true)
        {
            UnityWebRequest www = UnityWebRequest.Get(_urlRequest);

            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("PROBLEMA EN REQUEST");
            } else
            {
                print(www.downloadHandler.text);

                ListaCarro listaCarro = JsonUtility.FromJson<ListaCarro>(
                    www.downloadHandler.text
                );

                _requestSinArgumento?.Invoke();

                _requestConArgumento?.Invoke(listaCarro);
            }

        }

        yield return new WaitForSeconds(1);

    }

    IEnumerator RequestSimulado()
    {
        while (true)
        {
            string json = ServerSimulado.Instance.JSON;

            ListaCarro listaCarro = JsonUtility.FromJson<ListaCarro>(json);

            yield return new WaitForSeconds(1);
        }
    }

}