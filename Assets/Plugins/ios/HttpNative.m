//
//  HttpNative.m
//  httpTest
//
//

//#import <Foundation/Foundation.h>

#import "HttpNative.h"

@interface HttpNative ()

@end

@implementation HttpNative

+(void) httpPostWithUnityCallback:(NSString *)nsUrl lbPostData:(NSString*) nsPostData
//+(void) httpPostWithUnityCallback:(NSString *)nsUrl

{
    //リトライできるか
    //タイムアウトの考慮
    NSURLSessionConfiguration *defaultConfigObject = [NSURLSessionConfiguration defaultSessionConfiguration];
    defaultConfigObject.HTTPAdditionalHeaders = @{
                                                  
                                                  //@"Content-Type"  : [NSString stringWithFormat:
                                                  //                    @""]
                                                  };
    
    NSURLSession *defaultSession = [NSURLSession sessionWithConfiguration: defaultConfigObject delegate: nil delegateQueue: [NSOperationQueue mainQueue]];
    
    //URLの設定
    NSURL * url = [NSURL URLWithString:nsUrl];
    NSMutableURLRequest * urlRequest = [NSMutableURLRequest requestWithURL:url];
    
    //NSString * params = nsPostData;
    nsPostData = @"name=kabu&addr=2332";
    [urlRequest setHTTPMethod:@"POST"];
    [urlRequest setHTTPBody:[nsPostData dataUsingEncoding:NSUTF8StringEncoding]];
    NSURLSessionDataTask * dataTask =[defaultSession
                                      dataTaskWithRequest:urlRequest
                                      completionHandler:^(NSData *data, NSURLResponse *response, NSError *error)
                                      {
                                          NSLog(@"Response:%@ %@\n", response, error);
                                          if(error == nil)
                                          {
                                              NSString * text = [[NSString alloc] initWithData: data encoding: NSUTF8StringEncoding];
                                              NSLog(@"Data = %@",text);
                                          }
                                      }];
    [dataTask resume];
}

@end
