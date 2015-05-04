using Json.Net;

namespace Util {
    /// <summary>
    /// Json操作
    /// </summary>
    public static class Json {
        /// <summary>
        /// 将Json字符串转换为对象
        /// </summary>
        /// <param name="json">Json字符串</param>
        public static T ToObject<T>( string json ) {
            return JsonConvert.DeserializeObject<T>( json );
        }

        /// <summary>
        /// 将对象转换为Json字符串
        /// </summary>
        /// <param name="target">目标对象</param>
        /// <param name="isConvertSingleQuotes">是否将双引号转成单引号</param>
        public static string ToJson( object target,bool isConvertSingleQuotes = false ) {
            if ( target == null )
                return "{}";
            var result = JsonConvert.SerializeObject( target );
            if ( isConvertSingleQuotes )
                result = result.Replace( "\"", "'" );
            return result;
        }
    }
}
