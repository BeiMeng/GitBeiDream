using System;
using System.Security.Principal;

namespace Util.Security {
    /// <summary>
    /// 身份标识
    /// </summary>
    public class Identity : IIdentity {
        /// <summary>
        /// 初始化身份标识
        /// </summary>
        public Identity()
            : this( false, "" ) {
        }

        /// <summary>
        /// 初始化身份标识
        /// </summary>
        /// <param name="isAuthenticated">是否认证</param>
        /// <param name="userId">用户标识</param>
        public Identity( bool isAuthenticated, string userId ) {
            IsAuthenticated = isAuthenticated;
            Name = userId;
            FullName = string.Empty;
            Role = string.Empty;
        }

        /// <summary>
        /// 认证类型
        /// </summary>
        public string AuthenticationType { get { return "Custom"; } }

        /// <summary>
        /// 是否认证
        /// </summary>
        public bool IsAuthenticated { get; private set; }

        /// <summary>
        /// 用户标识
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// 用户编号
        /// </summary>
        public string UserId {
            get { return Name; }
        }

        /// <summary>
        /// 用户姓名
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// 角色
        /// </summary>
        public string Role { get; set; }

        /// <summary>
        /// 应用程序编码
        /// </summary>
        public string ApplicationCode { get; set; }

        /// <summary>
        /// 应用程序
        /// </summary>
        public string Application { get; set; }

        /// <summary>
        /// 未认证的身份标识
        /// </summary>
        public static UnauthenticatedIdentity Unauthenticated() {
            return new UnauthenticatedIdentity();
        }
    }
}
