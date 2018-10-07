using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HttpNativePlugin  {

	public static void HttpRequest(string url, string postdata){
#if UNITY_ANDROID && !UNITY_EDITOR
		AndroidJavaClass unityPlayer = new AndroidJavaClass("jp.plugin.unity_native.UnityPluginManager"); 

		//あとコールバック先のゲームオブジェクト名とメソッド名
		//リトライ数
		unityPlayer.CallStatic("HttpRequest",url,postdata);
#endif
	}
}
