#include <stdio.h>
#include "Dxlib/DxLib.h"

//数値定義

#define WID 1200
#define LEN 720
#define CHARBIG 100
#define CHARSMALL 50
#define CHARDEPTH 5

//Dxlib初期設定
int Dxlib_Start(){
    //ウィンドウサイズ指定
    SetGraphMode(WID,LEN,16);
    //画面サイズをウィンドウに設定
    ChangeWindowMode( true );
    //文字コードを設定
    SetUseCharCodeFormat(DX_CHARCODEFORMAT_UTF8);

    // ＤＸライブラリ初期化処理
    if( DxLib_Init() == -1 ){
		return -1 ;			// エラーが起きたら直ちに終了
	}
    
    return 0;
}

//各種データセット
int Handleset(int font){
    static int TitleFontHandle = CreateFontToHandle( NULL , CHARBIG, CHARDEPTH ,DX_FONTTYPE_ANTIALIASING_EDGE_8X8);
    static int TitleSubFontHandle = CreateFontToHandle( NULL , CHARSMALL , CHARDEPTH ,DX_FONTTYPE_ANTIALIASING_EDGE_8X8);
    switch(font){
        case 1:
            return TitleFontHandle;
            break;
        case 2:
            return TitleSubFontHandle;
            break;
        default:
            break;
    }
    return TitleFontHandle;
}

int TitleScene(){
    static int select = 0; //モードセレクト

    while(CheckHitKey( KEY_INPUT_ESCAPE ) == 0 ){
        if(CheckHitKey(KEY_INPUT_UP)){ select = 0;}
        if(CheckHitKey(KEY_INPUT_DOWN)){ select = 1 ;}
        
        //画面表示
        DrawStringToHandle( WID/2-CHARBIG*2 , LEN/6 , "神経衰弱" , GetColor(0,255,255) ,Handleset(1) );
        DrawStringToHandle( WID/2-CHARSMALL*2, LEN/6*3 , "スタート" , GetColor(0,255,255) ,Handleset(2) );
        DrawStringToHandle( WID/2-CHARSMALL*1.5, LEN/6*4 , "遊び方" , GetColor(0,255,255) ,Handleset(2) );
        
        if(select % 2 == 0){
            DrawCircle( WID/2-CHARSMALL*2.5 , LEN/6*3 + CHARSMALL/2  ,  10 , GetColor(255,0,0), true );
        } else {
            DrawCircle( WID/2-CHARSMALL*2.5 , LEN/6*4 + CHARSMALL/2  ,  10 , GetColor(255,0,0), true );
        }
        WaitKey();
        ClearDrawScreen();
    }

    return 0;
}


//Dxlib終了
int Dxlib_Finish(){
    DxLib_End() ;				// ＤＸライブラリ使用の終了処理
    return 0;
}

// プログラムは WinMain から始まります
int WINAPI WinMain( HINSTANCE hInstance, HINSTANCE hPrevInstance, LPSTR lpCmdLine, int nCmdShow )
{
    //Dxlib初期設定
	Dxlib_Start();
    
    //タイトル画面以降
    TitleScene();

	WaitKey() ;				// キー入力待ち
    
    //Dxlib終了
    Dxlib_Finish();
    return 0;
}