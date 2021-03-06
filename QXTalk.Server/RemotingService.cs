﻿using System;
using System.Collections.Generic;
using ESPlus.Rapid;
using JustLib.Records;
using QXTalk.Core;

namespace QXTalk.Server
{
    /// <summary>
    /// 服务端发布的Remoting服务，供客户端调用。提供如下功能：
    /// （1）注册用户。
    /// （2）查询用户。
    /// （3）发送系统通知。
    /// （4）查询聊天记录。
    /// </summary>
    internal class RemotingService :MarshalByRefObject, IRemotingService
    {
        private GlobalCache globalCache;
        private IRapidServerEngine rapidServerEngine;
        public RemotingService(GlobalCache db ,IRapidServerEngine engine)
        {
            this.globalCache = db;
            this.rapidServerEngine = engine;
        }

        public void SendSystemNotify(string title, string content)
        {
            SystemNotifyContract contract = new SystemNotifyContract(title, content, "", null);
            byte[] info = ESPlus.Serialization.CompactPropertySerializer.Default.Serialize(contract);
            foreach (string userID in this.rapidServerEngine.UserManager.GetOnlineUserList())
            {
                this.rapidServerEngine.CustomizeController.Send(userID, InformationTypes.SystemNotify4AllOnline, info);
            }
        }

        public RegisterResult Register(GGUser user)
        {
            try
            {
                if (this.globalCache.IsUserExist(user.UserID))
                {
                    return RegisterResult.Existed;
                }

                this.globalCache.InsertUser(user);
                return RegisterResult.Succeed;
            }
            catch (Exception ee)
            {
                return RegisterResult.Error;
            }
        }
        /// <summary>
        /// 查询用户是否存在
        /// </summary>
        /// <param name="idOrName"></param>
        /// <returns></returns>
        public List<GGUser> SearchUser(string idOrName)
        {
            return this.globalCache.SearchUser(idOrName);
        }

        public override object InitializeLifetimeService()
        {
            return null;
        }

        public ChatRecordPage GetChatRecordPage(ChatRecordTimeScope timeScope, string senderID, string accepterID, int pageSize, int pageIndex)
        {
            return this.globalCache.GetChatRecordPage(timeScope ,senderID, accepterID, pageSize, pageIndex);
        }

        public ChatRecordPage GetGroupChatRecordPage(ChatRecordTimeScope timeScope, string groupID, int pageSize, int pageIndex)
        {
            ChatRecordPage page = this.globalCache.GetGroupChatRecordPage(timeScope ,groupID, pageSize, pageIndex);
            return page;
        }       

        public void InsertChatMessageRecord(ChatMessageRecord record)
        {
            //目前没有通过remoting插入数据库
        }
    }
}
