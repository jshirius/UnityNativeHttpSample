using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Text;
using UnityEngine.Networking;


/// <summary>
/// 住所録
/// </summary>
[System.Serializable]
public class AddressData{
	[SerializeField]
    public int id; //id

	[SerializeField]
    public string name;//名前

	[SerializeField]
    public string address;//住所

	[SerializeField]
    public System.DateTime date;//登録日
} 

//JsonUtilityは、RootがArrayだと処理してくれないので、以下のような構造にする
public class AddressList{
	public int id;	//ID
	public List<AddressData> AddressDatas;

}



public class HttpSample : MonoBehaviour {

	const string Url = "http://sion0521.sakura.ne.jp/test/HttpNativeTest.php";

	public void OnUnityHttpButton(){
		//Unity側のHTTPリクエスト
		Debug.Log("OnUnityHttpButton start");

		string str = MakeSampleData();
		StartCoroutine(UnityWebRequestDownload(str));

		Debug.Log("OnUnityHttpButton end");
	}

	public void OnNativeButton(){
		//Native側のHTTPリクエスト
		Debug.Log("OnNativeButton start");

		string str = MakeSampleData();
		HttpNativePlugin.HttpRequest(Url,str);

		Debug.Log("OnNativeButton end");
	}

	private string MakeSampleData(){
		//約300KB分のデータ作成
		AddressList addressList = new AddressList();
		addressList.AddressDatas = new List<AddressData>();
		addressList.id = 5001;
		for(int i = 0 ; i < 15000 ;i++){
		
			AddressData addressData = new AddressData();
			addressData.id = i;
			addressData.name = "Name_" + i;
			addressData.date = System.DateTime.Now;
			addressData.address = "AddressData_" + i;
			addressList.AddressDatas .Add(addressData);

			//Debug.Log("Send data" + JsonUtility.ToJson(addressData));
		}

		//Jsonに変換
		string json = JsonUtility.ToJson(addressList);


		return json;
	}

	private void OnApplicationPause( bool pauseStatus )
	{
    	Debug.Log("OnApplicationPause:" + pauseStatus);

		if(pauseStatus == true){
			//データ作成する
			string str = MakeSampleData();

			//ネイティブ側の通信処理を呼び出す
			HttpNativePlugin.HttpRequest(Url,str);
		}

	}



    //UnityWebRequestでhttpリクエストを実装する
    IEnumerator UnityWebRequestDownload(string sendData) {

    	WWWForm form = new WWWForm();
        form.AddField("unity_data", sendData);

        UnityWebRequest www = UnityWebRequest.Post(Url,form);

        //タイムアウトを3秒に設定する
        www.timeout = 3;
        Debug.Log("インターネット接続前:" + System.DateTime.Now.ToString());
        //リクエスト開始
		//ポーズがかかるとこの部分で止まる！！！！
		//つまり送信がここでとまってしまう！！
        yield return www.Send();

        Debug.Log(www.error + " " + System.DateTime.Now.ToString());
        Debug.Log(www.downloadHandler.text + " " + System.DateTime.Now.ToString());
  	}

}
