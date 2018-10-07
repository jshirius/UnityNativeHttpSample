package jp.plugin.unity_native;

import android.util.Log;
import android.os.Bundle;


import static com.unity3d.player.UnityPlayer.UnitySendMessage;

public class UnityPluginManager {

   // private HttpTask task;

    public static void onCreate(Bundle savedInstanceState) {

        Log.d("plugin", "UnityPluginManagerActivity onCreate");



    }

    public static void onStart() {

        Log.d("plugin", "UnityPluginManagerActivity onStart");

    }

    public static void HttpRequest(String url, String postData){
        HttpTask task;
        task = new HttpTask();
        task.setOnCallBack(new HttpTask.CallBackTask(){

            @Override
            public void CallBack(String result) {
                super.CallBack(result);
                // resultにはdoInBackgroundの値が返されます
                // ここからHttpリクエスト後の処理実装
                Log.i("AsyncTaskCallback", "非同期処理が終了しました。取得したサーバーからの戻り値は：" + result);

                //Unity側に返す
                UnitySendMessage("CallbackHttpNative","CallbackHttpTask",result);

            }

        });
        task.execute(url,postData);
    }

}
