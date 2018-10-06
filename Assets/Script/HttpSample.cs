using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.GZip;
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

		MakeSampleData();

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


		//圧縮
		string gzip = "";
		for(int i = 0 ; i < 20 ;i++){
			gzip = ConvertCompressGZipBase64(json);
			Debug.Log("Send data:" + gzip);
		}
		

		return gzip;
	}

	private void OnApplicationPause( bool pauseStatus )
	{
    	Debug.Log("OnApplicationPause:" + pauseStatus);

		//ここまでは、Androidでpause状態にしても動く
		string str = MakeSampleData();

		//さてHTTPはどうかな？
		StartCoroutine(UnityWebRequestDownload(str));

		

		//ネットの渡す
		//Debug.Log("OnApplicationPause:End:" + pauseStatus);
	}

	/**
	* Gzip形式で圧縮し、Base64形式で返す
	* @param  変換前文字列
	* @result  圧縮した文字列にBase64形式変換した文字列
	*/ 
	protected string ConvertCompressGZipBase64(string rawData){
		//エンコード
		byte[] source = Encoding.UTF8.GetBytes(rawData);	
		
		//圧縮
		byte[]bs = 	CompressGZip(source);
		
		//ToBase64　変換
		return System.Convert.ToBase64String(bs);
	}

    //UnityWebRequestでhttpリクエストを実装する
    //UnityWebRequestには、タイムアウト処理が実装されており
    //UnityWebRequestのメンバー変数timeoutに整数を設定するだけです
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


	/**
		* 文字列をgzip形式で圧縮する
		* @param  圧縮元のbyte列
		* @return 圧縮後のbyte列
		*/
	public static byte[] CompressGZip(byte[] source){
		byte[] destination = null;
		using (MemoryStream ms = new MemoryStream()){
			//圧縮する
			GZipOutputStream s = new GZipOutputStream(ms);
			
			// ストリームに圧縮するデータを書き込みます 
			s.Write(source, 0, source.Length); 
			s.Close(); 
			
			// 圧縮されたデータを バイト配列で取得します 
			destination = ms.ToArray(); 
		}
		
		return destination;
	}	
}
