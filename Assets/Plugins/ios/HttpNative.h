//
//  HttpNative.h
//  httpTest
//
//

#ifndef HttpNative_h
#define HttpNative_h

#import <Foundation/Foundation.h>
@class HttpNative;

@interface HttpNative : NSObject
{


}
+(void) httpPostWithUnityCallback:(NSString *)nsUrl lbPostData:(NSString*)nsPostData;
//+(void) httpPostWithUnityCallback:(NSString *)nsUrl;

@end

#endif /* HttpNative_h */
