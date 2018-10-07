package jp.plugin.unity_native;

import android.os.AsyncTask;
import android.util.Log;

import java.io.IOException;
import java.io.OutputStream;
import java.net.HttpURLConnection;
import java.net.URL;
import java.io.BufferedReader;
import java.io.InputStream;
import java.io.InputStreamReader;

public class HttpTask  extends AsyncTask<String, Void, String> {


    /**
     * コールバック用のstaticなclass
     */
    public static class CallBackTask {
        public void CallBack(String result) {
        }
    }

    private CallBackTask callbacktask;


    // 非同期処理
    @Override
    protected String doInBackground(String... params) {


        Log.d("debug","doInBackground start url:" + params[0]);

        //URLの設定
        String urlSt = params[0];

        HttpURLConnection con = null;
        String result = "";

        //Postで送信するデータ設定
        String word = "word=" + params[1];


        Log.d("debug","send data:" +params[1]);
        try {
            // URL設定
            URL url = new URL(urlSt);

            // HttpURLConnection
            con = (HttpURLConnection) url.openConnection();

            // request POST
            con.setRequestMethod("POST");

            // リダイレクトなし
            con.setInstanceFollowRedirects(false);

            // データを書き込む
            con.setDoOutput(true);

            // タイムアウトの設定
            con.setReadTimeout(10000);
            con.setConnectTimeout(20000);

            // 接続
            con.connect();

            // POSTデータ送信処理
            OutputStream out = null;
            try {
                out = con.getOutputStream();
                out.write( word.getBytes("UTF-8") );
                out.flush();
                Log.d("debug","flush");
            } catch (IOException e) {
                // POST送信エラー
                e.printStackTrace();
                result="error";
            } finally {
                if (out != null) {
                    //ストリームを閉じる
                    out.close();
                }
            }

            if (con != null) {
                final int status = con.getResponseCode();
                if (status == HttpURLConnection.HTTP_OK) {
                    // HTTPOK！　レスポンスを受け取る処理
                    result = InputStreamToString(con.getInputStream());
                } else {
                    //何かしらのエラー
                    result = "status=" + String.valueOf(status);
                }
            }
        } catch (IOException e) {
            e.printStackTrace();
        } finally {
            if (con != null) {
                con.disconnect();
            }
        }
        Log.d("debug",result);
        return result;
    }

    // 非同期処理が終了後、結果をメインスレッドに返す
    @Override
    protected void onPostExecute(String result) {
        super.onPostExecute(result);

        //コールバックの作成
        callbacktask.CallBack(result);


    }

    public void setOnCallBack(CallBackTask callback) {
        callbacktask = callback;
    }


    // InputStream -> String
    static String InputStreamToString(InputStream is) throws IOException {
        BufferedReader br = new BufferedReader(new InputStreamReader(is));
        StringBuilder sb = new StringBuilder();
        String line;
        while ((line = br.readLine()) != null) {
            sb.append(line);
        }
        br.close();
        return sb.toString();
    }

}
