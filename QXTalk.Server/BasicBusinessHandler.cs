using ESPlus.Application.Basic.Server;
using QXTalk.Core;

namespace QXTalk.Server
{
    /// <summary>
    /// ������������������֤��½���û���
    /// </summary>
    internal class BasicHandler : IBasicHandler
    {
        private GlobalCache globalCache;
        public BasicHandler(GlobalCache db)
        {
            this.globalCache = db;
        }

        /// <summary>
        /// �˴���֤�û����˺ź����롣����true��ʾͨ����֤��
        /// </summary>  
        public bool VerifyUser(string systemToken, string userID, string password, out string failureCause)
        {
            failureCause = "";          
            GGUser user = this.globalCache.GetUser(userID);
            if (user == null)
            {
                failureCause = "�û������ڣ�";
                return false;
            }

            if (user.PasswordMD5 != password)
            {
                failureCause = "�������";
                return false;
            }

            return true;
        }
    }
}
