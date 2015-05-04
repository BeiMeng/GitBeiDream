namespace Util.Security {
    /// <summary>
    /// 未认证的安全主体
    /// </summary>
    public class UnauthenticatedPrincipal : Principal {
        /// <summary>
        /// 初始化未认证的安全主体
        /// </summary>
        public UnauthenticatedPrincipal()
            : base( new UnauthenticatedIdentity() ) {
        }
    }
}
