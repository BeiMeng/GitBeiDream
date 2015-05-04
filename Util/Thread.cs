
namespace Util {
    /// <summary>
    /// 线程操作
    /// </summary>
    public class Thread {

        #region ThreadId(线程编号)

        /// <summary>
        /// 线程编号
        /// </summary>
        public static string ThreadId {
            get {
                return System.Threading.Thread.CurrentThread.ManagedThreadId.ToString();
            }
        }

        #endregion

        #region Sleep(线程等待)

        /// <summary>
        /// 线程等待
        /// </summary>
        /// <param name="time">等待时间，单位：毫秒</param>
        public static void Sleep( int time ) {
            System.Threading.Thread.Sleep( time );
        }

        #endregion
    }
}
