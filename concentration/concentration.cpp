#include <stdio.h>
#include "Dxlib/DxLib.h"

//数値定義

#define WID 1200
#define LEN 720
#define CHARBIG 100
#define CHARSMALL 50
#define CHARDEPTH 5
#define CARDWID 85
#define CARDLEN 125
#define CARDFIRST 42.5

//Dxlib初期設定
int Dxlib_Start(){
    //ウィンドウサイズ指定
    SetGraphMode(WID,LEN,16);
    //画面サイズをウィンドウに設定
    ChangeWindowMode( true );
    //文字コードを設定
    SetUseCharCodeFormat(DX_CHARCODEFORMAT_UTF8);
    //マウス可視化設定
    SetMouseDispFlag( TRUE ) ;

    // ＤＸライブラリ初期化処理
    if( DxLib_Init() == -1 ){
		return -1 ;			// エラーが起きたら直ちに終了
	}
    
    return 0;
}

//文字データセット
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

//画像データセット
int picHandleset(int kind,int num){

    //画像データセット

    //クラブ
    static int clubHandle1 = LoadGraph("cards/card_club_01.png");
    static int clubHandle2 = LoadGraph("cards/card_club_02.png");
    static int clubHandle3 = LoadGraph("cards/card_club_03.png");
    static int clubHandle4 = LoadGraph("cards/card_club_04.png");
    static int clubHandle5 = LoadGraph("cards/card_club_05.png");
    static int clubHandle6 = LoadGraph("cards/card_club_06.png");
    static int clubHandle7 = LoadGraph("cards/card_club_07.png");
    static int clubHandle8 = LoadGraph("cards/card_club_08.png");
    static int clubHandle9 = LoadGraph("cards/card_club_09.png");
    static int clubHandle10 = LoadGraph("cards/card_club_10.png");
    static int clubHandle11 = LoadGraph("cards/card_club_11.png");
    static int clubHandle12 = LoadGraph("cards/card_club_12.png");
    static int clubHandle13 = LoadGraph("cards/card_club_13.png");
    //ダイア
    static int diaHandle1 = LoadGraph("cards/card_diamond_01.png");
    static int diaHandle2 = LoadGraph("cards/card_diamond_02.png");
    static int diaHandle3 = LoadGraph("cards/card_diamond_03.png");
    static int diaHandle4 = LoadGraph("cards/card_diamond_04.png");
    static int diaHandle5 = LoadGraph("cards/card_diamond_05.png");
    static int diaHandle6 = LoadGraph("cards/card_diamond_06.png");
    static int diaHandle7 = LoadGraph("cards/card_diamond_07.png");
    static int diaHandle8 = LoadGraph("cards/card_diamond_08.png");
    static int diaHandle9 = LoadGraph("cards/card_diamond_09.png");
    static int diaHandle10 = LoadGraph("cards/card_diamond_10.png");
    static int diaHandle11 = LoadGraph("cards/card_diamond_11.png");
    static int diaHandle12 = LoadGraph("cards/card_diamond_12.png");
    static int diaHandle13 = LoadGraph("cards/card_diamond_13.png");
    //ハート
    static int heartHandle1 = LoadGraph("cards/card_heart_01.png");
    static int heartHandle2 = LoadGraph("cards/card_heart_02.png");
    static int heartHandle3 = LoadGraph("cards/card_heart_03.png");
    static int heartHandle4 = LoadGraph("cards/card_heart_04.png");
    static int heartHandle5 = LoadGraph("cards/card_heart_05.png");
    static int heartHandle6 = LoadGraph("cards/card_heart_06.png");
    static int heartHandle7 = LoadGraph("cards/card_heart_07.png");
    static int heartHandle8 = LoadGraph("cards/card_heart_08.png");
    static int heartHandle9 = LoadGraph("cards/card_heart_09.png");
    static int heartHandle10 = LoadGraph("cards/card_heart_10.png");
    static int heartHandle11 = LoadGraph("cards/card_heart_11.png");
    static int heartHandle12 = LoadGraph("cards/card_heart_12.png");
    static int heartHandle13 = LoadGraph("cards/card_heart_13.png");
    //スペード
    static int spadeHandle1 = LoadGraph("cards/card_spade_01.png");
    static int spadeHandle2 = LoadGraph("cards/card_spade_02.png");
    static int spadeHandle3 = LoadGraph("cards/card_spade_03.png");
    static int spadeHandle4 = LoadGraph("cards/card_spade_04.png");
    static int spadeHandle5 = LoadGraph("cards/card_spade_05.png");
    static int spadeHandle6 = LoadGraph("cards/card_spade_06.png");
    static int spadeHandle7 = LoadGraph("cards/card_spade_07.png");
    static int spadeHandle8 = LoadGraph("cards/card_spade_08.png");
    static int spadeHandle9 = LoadGraph("cards/card_spade_09.png");
    static int spadeHandle10 = LoadGraph("cards/card_spade_10.png");
    static int spadeHandle11 = LoadGraph("cards/card_spade_11.png");
    static int spadeHandle12 = LoadGraph("cards/card_spade_12.png");
    static int spadeHandle13 = LoadGraph("cards/card_spade_13.png");
    //裏面
    static int backHandle = LoadGraph("cards/card_back.png");
    
    //画像呼び出し
    if(kind == 4){  //裏面
        return backHandle;
    } else if(kind == 0) {   //クラブ
        switch(num){
                case 1:
                    return clubHandle1;
                    break;
                case 2:
                    return clubHandle2;
                    break;
                case 3:
                    return clubHandle3;
                    break;
                case 4:
                    return clubHandle4;
                    break;
                case 5:
                    return clubHandle5;
                    break;
                case 6:
                    return clubHandle6;
                    break;
                case 7:
                    return clubHandle7;
                    break;
                case 8:
                    return clubHandle8;
                    break;
                case 9:
                    return clubHandle9;
                    break;
                case 10:
                    return clubHandle10;
                    break;
                case 11:
                    return clubHandle11;
                    break;
                case 12:
                    return clubHandle12;
                    break;
                case 13:
                    return clubHandle13;
                    break;
            }
    } else if(kind == 1){   //ダイアモンド
        switch(num){
                case 1:
                    return diaHandle1;
                    break;
                case 2:
                    return diaHandle2;
                    break;
                case 3:
                    return diaHandle3;
                    break;
                case 4:
                    return diaHandle4;
                    break;
                case 5:
                    return diaHandle5;
                    break;
                case 6:
                    return diaHandle6;
                    break;
                case 7:
                    return diaHandle7;
                    break;
                case 8:
                    return diaHandle8;
                    break;
                case 9:
                    return diaHandle9;
                    break;
                case 10:
                    return diaHandle10;
                    break;
                case 11:
                    return diaHandle11;
                    break;
                case 12:
                    return diaHandle12;
                    break;
                case 13:
                    return diaHandle13;
                    break;
            }
    } else if(kind == 2){   //ハート  
        switch(num){
                case 1:
                    return heartHandle1;
                    break;
                case 2:
                    return heartHandle2;
                    break;
                case 3:
                    return heartHandle3;
                    break;
                case 4:
                    return heartHandle4;
                    break;
                case 5:
                    return heartHandle5;
                    break;
                case 6:
                    return heartHandle6;
                    break;
                case 7:
                    return heartHandle7;
                    break;
                case 8:
                    return heartHandle8;
                    break;
                case 9:
                    return heartHandle9;
                    break;
                case 10:
                    return heartHandle10;
                    break;
                case 11:
                    return heartHandle11;
                    break;
                case 12:
                    return heartHandle12;
                    break;
                case 13:
                    return heartHandle13;
                    break;
            }
    } else if(kind == 3){    //スペード
        switch(num){
                case 1:
                    return spadeHandle1;
                    break;
                case 2:
                    return spadeHandle2;
                    break;
                case 3:
                    return spadeHandle3;
                    break;
                case 4:
                    return spadeHandle4;
                    break;
                case 5:
                    return spadeHandle5;
                    break;
                case 6:
                    return spadeHandle6;
                    break;
                case 7:
                    return spadeHandle7;
                    break;
                case 8:
                    return spadeHandle8;
                    break;
                case 9:
                    return spadeHandle9;
                    break;
                case 10:
                    return spadeHandle10;
                    break;
                case 11:
                    return spadeHandle11;
                    break;
                case 12:
                    return spadeHandle12;
                    break;
                case 13:
                    return spadeHandle13;
                    break;
            }
    }

    return 0;
}

//タイトル画面
int TitleScene(){
    static int select = 0; //モードセレクト

    while(CheckHitKey( KEY_INPUT_RETURN ) == 0 ){
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

    return select;
}

//遊び方画面
void HowToPlayScene(){
    while(CheckHitKey( KEY_INPUT_SPACE ) == 0 ){
        DrawStringToHandle( WID/2-CHARBIG*1.5 , LEN/6 , "遊び方" , GetColor(0,255,255) ,Handleset(1) );
        DrawStringToHandle( WID/2-CHARBIG*1 , LEN/6*3 , "割愛" , GetColor(0,255,255) ,Handleset(1) );
        DrawStringToHandle( WID/2-CHARSMALL*6 , LEN/6*4 , "スペースでゲームスタート" , GetColor(0,255,255) ,Handleset(2) );
    }
    ClearDrawScreen();
}

//ゲーム初期設定
void GameInit(int setcard[4][13]){
    int card[53] = {0};   //0は置かれていないカード 1は置かれたカード

    for(int i = 0; i < 4; i++){
        for(int j = 0; j < 13; j++){
            do{
                setcard[i][j] = rand()%52+1;//1～52が入る
            }while(card[setcard[i][j]] != 0);
            card[setcard[i][j]] = 1;
        }
    }
}
//ゲーム進行

//ゲーム画面
int GameScene(){

    //カードを設定する 
    int setcard[4][13] = {0};  //1～53の数字が入る
    int setcardkind = 0;    //置かれたカードの種類
    int setcardnum = 0;     //置かれたカードの数字
    int MouseX,MouseY;  //マウスの座標
    int firstcardnum;   //一枚目にめくったカードの数字
    int secondcardnum;  //二枚目にめくったカードの数字
    int cardX,cardY;    //めくるカードの座標
    int MouseInput;     //マウスの入力
    int opencard = 0;   //開かれたカードの数
    //初期配置
    while(CheckHitKey( KEY_INPUT_SPACE)  == 0 ){
        for(int i = 0; i < 4; i++){
            for(int j = 0; j < 13; j++){
            DrawGraph(CARDWID*j+CARDFIRST,CARDLEN*i+CARDLEN,picHandleset(4,0),false);
            }
        }
    }

    //ゲーム初期設定
    GameInit(setcard); 

//デバッグ
/*
    //カードを全て裏返す    
    WaitTimer(2000);
    ClearDrawScreen();

    while(CheckHitKey( KEY_INPUT_SPACE )  == 0 ){
        for(int i = 0; i < 4; i++){
            for(int j = 0; j < 13; j++){
                setcardkind = (setcard[i][j]-1) / 13;   //1～13が０でクラブ 14～26が１でダイア・・・
                setcardnum = setcard[i][j] % 13 + 1 ;    //ダイアのAなら14-13で１が出る
                DrawGraph(CARDWID*j+CARDFIRST,CARDLEN*i+CARDLEN,picHandleset(setcardkind,setcardnum),false);
                //DrawFormatString( 300*i, 40 * j, GetColor(255,255,255), "setcardnum=%d", setcardnum );

                while(CheckHitKey( KEY_INPUT_RETURN )  == 0 ){
                    GetMousePoint( &MouseX , &MouseY ); //マウスの位置を取得
                    cardX = (MouseX-CARDFIRST)/CARDWID;      //カードの列を指定
                    cardY = (MouseY-CARDLEN)/CARDLEN;   //カードの行を指定
                    DrawFormatString( 30, 40 , GetColor(255,255,255), "cardX=%d cardY=%d", cardX ,cardY );
                    DrawFormatString( 30, 80 , GetColor(255,255,255), "MouseX=%d MouseY=%d", MouseX ,MouseY );
                    WaitTimer(300);
                    ClearDrawScreen();
                }
            }

        }
    }

*/
   

    //クリックでカードを開く

    while(opencard != 2){
        GetMousePoint( &MouseX , &MouseY ); //マウスの位置を取得
        MouseInput = GetMouseInput() ;      //カードのボタン状態を取得
        cardX = (MouseX-CARDFIRST)/CARDWID; //カードの列を指定
        cardY = (MouseY-CARDLEN)/CARDLEN;   //カードの行を指定

        //デバッグ
        //DrawFormatString( 30, 40 , GetColor(255,255,255), "cardX=%d cardY=%d", cardX ,cardY );
        //DrawFormatString( 30, 70 , GetColor(255,255,255), "setcardkind=%d setcardnum=%d", setcardkind,setcardnum );

        if(cardX<0 || cardX>12 || cardY<0 || cardY>3 || MouseX<CARDFIRST || MouseY < CARDLEN ){ //カードがないところでボタンを押しても何も起こらない
            continue;
        }else {
            setcardkind = (setcard[cardY][cardX]-1) / 13;
            setcardnum = setcard[cardY][cardX] % 13 + 1 ;
        }

        //一枚目を開く
        if( ( MouseInput & MOUSE_INPUT_LEFT ) != 0 ){
            DrawGraph(CARDWID*cardX+CARDFIRST,CARDLEN*cardY+CARDLEN,picHandleset(setcardkind,setcardnum),false);
            opencard = 1;
        }
        //二枚目も開く
        if( ( MouseInput & MOUSE_INPUT_RIGHT ) != 0 && opencard == 1){
            DrawGraph(CARDWID*cardX+CARDFIRST,CARDLEN*cardY+CARDLEN,picHandleset(setcardkind,setcardnum),false);
            opencard = 2;
        }
    }
    WaitTimer(300);

    //同じだったら消える

    //違ったら閉じる

    //全部消えたらクリア
    //return 1;
    
    return 0;
}

void ClearScene(){
    //クリア画面
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

    int mode;
    int result = 0;
    //タイトル画面移行
    mode = TitleScene();
    if(mode == 0){
        result = GameScene();
    } else {
        HowToPlayScene();
        result = GameScene();
    }

    if(result) ClearScene();

	WaitKey() ;				// キー入力待ち
    
    //Dxlib終了
    Dxlib_Finish();
    return 0;
}