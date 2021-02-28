// Shaderブロック
// シェーダーの情報を記述する
Shader "Unlit/UnlitShaderTemplate"
{
    // Propertiesブロック
    // Unity上でやり取りをするプロパティ情報を記述する
    // マテリアルのInspectorウィンドウ上に表示され、スクリプト上からも設定できる
    Properties
    {
        // プロパティ名 ("Inspectorに表示する名前", 型) = "デフォルト値" { オプション }
        _MainTex ("Texture", 2D) = "white" {}
        _Color("Color", Color) = (1,1,1,1)
    }

    // SubShaderブロック
    // シェーダーの主な処理はこの中に記述する
    // サブシェーダーは複数書くことも可能が、基本は一つ
    SubShader
    {
        // Unity側にシェーダーの設定を伝えるために付けるタグのこと
        Tags
        {
            // レンダリングタイプ
            "RenderType" = "Opaque" // 半透明ならTransparent、それ以外ならOpaque
            // レンダーキュー：オブジェクトの描画順を制御（数字が小さいものから順番に描画される）
            //"Queue" = "Geometry+10"
        }
        // Level of Detail：サブシェーダーを使い分けるための仕組みで、しきい値の役割を果たす
        LOD 100
        // Zテスト(デプステスト)
        //ZTest Always

        // Passブロック
        // 1つのオブジェクトの1度の描画で行う処理をここに書く
        // これも基本一つだが、複雑な描画をするときは複数書くことも可能
        Pass
        {
            // プログラムを書き始めるという宣言
            CGPROGRAM // Cg(C for graphics)言語
            // HLSLPROGRAM // HLSL言語：DirectXで使用される
            // GLSLPROGRAM // GLSL言語：GLSLで使用される

            // 関数宣言
            #pragma vertex vert // "vert" 関数を頂点シェーダーとして使用する宣言
            #pragma fragment frag // "frag" 関数をフラグメントシェーダーとして使用する宣言
            // make fog work
            #pragma multi_compile_fog // multi_compileから始まるのはシェーダーバリアントという機能を使用するための命令

            // 指定したファイルの中身をそのままこの位置に貼り付ける命令
            #include "UnityCG.cginc"

            // 頂点シェーダーへ入力する頂点データ定義
            struct appdata
            {
                float4 vertex : POSITION; // 頂点座標
                float2 uv : TEXCOORD0; // テクスチャUV
            };

            // 頂点シェーダーからフラグメントシェーダーへ受け渡すデータの定義
            struct v2f
            {
                float2 uv : TEXCOORD0; // テクスチャUV
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION; // 座標変換された後の頂点座標
            };

            // グローバル変数の宣言
            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _Color;

            // 頂点シェーダー
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex); // 3D空間からスクリーン上の位置への座標変換
                o.uv = TRANSFORM_TEX(v.uv, _MainTex); // テクスチャのUV値を計算する処理
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            // フラグメントシェーダー
            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv) * _Color; // 指定したテクスチャから、指定UV値の色を取り出す処理（テクスチャサンプリング）
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            // プログラムを書き終わるという宣言
            ENDCG // Gg言語
            // ENDHLSL // HLSL言語
            // ENDGLSL // GLSL言語
        }
    }
}
