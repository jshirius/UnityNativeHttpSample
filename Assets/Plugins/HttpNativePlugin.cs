using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class HttpNativePlugin  {

	[DllImport ("__Internal")]
	private static extern void _UnityHttpRequest (string url, string postData);

	public static void HttpRequest(string url, string postdata){
#if UNITY_ANDROID && !UNITY_EDITOR
		AndroidJavaClass unityPlayer = new AndroidJavaClass("jp.plugin.unity_native.UnityPluginManager"); 

		//あとコールバック先のゲームオブジェクト名とメソッド名
		//リトライ数
		unityPlayer.CallStatic("HttpRequest",url,postdata);

#elif UNITY_IPHONE && !UNITY_EDITOR

		_UnityHttpRequest(url,postdata);

#endif
	}
}
