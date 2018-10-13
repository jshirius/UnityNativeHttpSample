//
//  UnityBinding.m
//  httpTest
//
//  Unity側との連携(C言語として書く)
//

#import <Foundation/Foundation.h>

#import "HttpNative.h"

void _UnityHttpRequest(char* url, char* postData){
    
    NSString     *nsUrl   = [[ NSString alloc ]  initWithUTF8String: url];
    NSString     *nsPostData   = [[ NSString alloc ] initWithUTF8String: postData] ;
    
    //Http処理
    //[HttpNative httpPostWithUnityCallback:@"sss"] ;
    [HttpNative httpPostWithUnityCallback:nsUrl  lbPostData:nsPostData];
}

