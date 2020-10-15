//***********************************************************************/
//*                                                                     */
//*     IF-40 API Sample Program                                        */
//*     Copyright(c) 2012  TEXIO TECHNOLOGY CORPORATION                 */
//***********************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;

    // C# - PWAインターフェース定義
    // 各関数の詳細はデバイスドライバ取扱説明書を参照。
    public class TMI_Api
    {
        // デバイスのオープン
        [DllImport("TMI_API.dll")]
        public static extern int TMI_HandleOpen(string Str_Renamed, string Ptr);

        // デバイスのクローズ
        [DllImport("TMI_API.dll")]
        public static extern int TMI_HandleClose(int hID);

        // タイムアウト時間設定
        [DllImport("TMI_API.dll")]
        public static extern int TMI_TimeOut(int hID, int time);

        // リフレッシュ動作
        [DllImport("TMI_API.dll")]
        public static extern int TMI_Refresh(int hID);

        // モデル名の取得
        [DllImport("TMI_API.dll")]
		public static extern int TMI_ModelNameQ(int hID, StringBuilder Model);

        // 出力位置とプリセット位置を指定して電圧値を設定する
        [DllImport("TMI_API.dll")]
        public static extern int TMI_Voltage(int hID, byte ch, byte preset, double Data);

        // 出力位置とプリセット位置を指定して電圧値を取得する
        [DllImport("TMI_API.dll")]
		public static extern int TMI_VoltageQ(int hID, byte ch, byte preset, out double Voltage);

        // 出力位置とプリセット位置を指定して電流値を設定する
        [DllImport("TMI_API.dll")]
        public static extern int TMI_Current(int hID, byte ch, byte preset, double Data);

        // 出力位置とプリセット位置を指定して電流値を取得する
        [DllImport("TMI_API.dll")]
		public static extern int TMI_CurrentQ(int hID, byte ch, byte preset, out double Current);

        // MainOutputのON OFFを行う
        [DllImport("TMI_API.dll")]
        public static extern int TMI_MainOutput(int hID, byte onoff);

        // MainOutputの状態を取得する
        [DllImport("TMI_API.dll")]
        public static extern int TMI_MainOutputQ(int hID, out byte Ans);

        // Delay のON OFFを行う
        [DllImport("TMI_API.dll")]
        public static extern int TMI_Delay(int hID, byte onoff);

        // Delayの状態を取得する
        [DllImport("TMI_API.dll")]
        public static extern int TMI_DelayQ(int hID, out byte Ans);

        // OutputSelectの出力別ON OFFを行う
        [DllImport("TMI_API.dll")]
        public static extern int TMI_OutputSel(int hID, byte ch, byte onoff);

        // OutputSelectの出力別の状態を取得する
        [DllImport("TMI_API.dll")]
        public static extern int TMI_OutputSelQ(int hID, byte ch, out byte Ans);

        // TrackingのON OFFを行う
        [DllImport("TMI_API.dll")]
        public static extern int TMI_TrackingOnOff(int hID, byte onoff);

        // Trackingの状態を取得する
        [DllImport("TMI_API.dll")]
        public static extern int TMI_TrackingOnOffQ(int hID, out byte Ans);

        // TrackingModeを設定する
        [DllImport("TMI_API.dll")]
        public static extern int TMI_TrackingMode(int hID, byte mode);

        // TrackingModeの状態を取得する
        [DllImport("TMI_API.dll")]
        public static extern int TMI_TrackingModeQ(int hID, out byte Ans);

        // TrackingGroupを出力別に設定する
        [DllImport("TMI_API.dll")]
        public static extern int TMI_TrackingGroup(int hID, byte ch, byte Tracking_Set);

        // TrackingGroupの出力別設定状態を取得する
        [DllImport("TMI_API.dll")]
        public static extern int TMI_TrackingGroupQ(int hID, byte ch, out byte Ans);

        // TrackingGroupの電圧・電流値を出力別に増減させる
        [DllImport("TMI_API.dll")]
        public static extern int TMI_TrackingData(int hID, byte ch, byte va, double Data);

        // TrackingGroupの電圧・電流値を出力別に取得する
        [DllImport("TMI_API.dll")]
		public static extern int TMI_TrackingDataQ(int hID, byte ch, byte va, out double Data);
		//public static extern int TMI_TrackingDataQ(int hID, byte ch, byte va, IntPtr Data);

        // DELAY時間を出力別に設定する
        [DllImport("TMI_API.dll")]
        public static extern int TMI_DelayTime(int hID, byte ch, double Data);

        // DELAY時間を出力別に取得する
        [DllImport("TMI_API.dll")]
		public static extern int TMI_DelayTimeQ(int hID, byte ch, out double Data);

        // Display表示位置を設定する
        [DllImport("TMI_API.dll")]
        public static extern int TMI_Display(int hID, byte ch);

        // Display表示位置を取得する
        [DllImport("TMI_API.dll")]
        public static extern int TMI_DisplayQ(int hID, out byte Ans);

        // Preset番号を設定する
        [DllImport("TMI_API.dll")]
        public static extern int TMI_Preset(int hID, byte preset);

        // Preset番号を取得する
        [DllImport("TMI_API.dll")]
        public static extern int TMI_PresetQ(int hID, out byte Ans);

        // 指定した出力の電圧・電流モニタ値とCV/CC状態を取得する
        [DllImport("TMI_API.dll")]
		public static extern int TMI_MoniDataQ(int hID, byte ch, out double Voltage, out double Current, out byte cv_cc);

        // システムアドレスを取得する
        [DllImport("TMI_API.dll")]
        public static extern int TMI_AdrQ(int hID, out byte Adr);

        // リモートからローカルへ切り替える
        [DllImport("TMI_API.dll")]
        public static extern int TMI_RemoteLocal(int hID);

        // ローカルロックアウト状態へ移行する
        [DllImport("TMI_API.dll")]
        public static extern int TMI_LocalLockOut(int hID);

        // データのバックアップを行う
        [DllImport("TMI_API.dll")]
        public static extern int TMI_DataBackUp(int hID);

        // サービスリクエストの禁止・許可の設定をする
        [DllImport("TMI_API.dll")]
        public static extern int TMI_SRQEnable(int hID, byte ch);

		// 全出力のPRESET値の取得
		[DllImport("TMI_API.dll")]
		public static extern int TMI_AllPresetQ(int hID, out double PresetMem);

		// 全出力のPRESET値の取得
		[DllImport("TMI_API.dll")]
		public static extern int TMI_AllPresetQS(int hID, StringBuilder PresetMem);

        // コントロール関数：コマンド送信
        [DllImport("TMI_API.dll")]
        public static extern int TMI_Out(int hID, string Str_Renamed);

        // コントロール関数：コマンド受信
        [DllImport("TMI_API.dll")]
        public static extern int TMI_In(int hID, StringBuilder strbuf);
    }

