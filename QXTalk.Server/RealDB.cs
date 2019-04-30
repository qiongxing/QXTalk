﻿using System.Collections.Generic;
using DataRabbit;
using DataRabbit.DBAccessing;
using DataRabbit.DBAccessing.Application;
using DataRabbit.DBAccessing.ORM;
using JustLib.Records;
using QXTalk.Core;

namespace QXTalk.Server
{  
    /// <summary>
    /// 真实数据库，支持SqlServer和MySQL，通过构造函数指定数据库类型。
    /// </summary>
    public class RealDB : DefaultChatRecordPersister, IDBPersister
    {
        private TransactionScopeFactory transactionScopeFactory;
                
        public RealDB(DataBaseType type , string dbName, string dbIP, int dbPort, string dbUser, string dbPwd)
        {
            DataConfiguration config = null;
            if (type == DataBaseType.MySQL)
            {
                config = new MysqlDataConfiguration(dbIP, dbPort, dbUser, dbPwd, dbName);
            }
            else
            {
                config = new SqlDataConfiguration(dbIP, dbUser, dbPwd, dbName ,dbPort);
            }
            this.transactionScopeFactory = new TransactionScopeFactory(config);
            this.transactionScopeFactory.Initialize();
            base.Initialize(this.transactionScopeFactory);

            this.GetChatRecordPage(ChatRecordTimeScope.All ,"111111" ,"591821945" ,20,0)  ;
        }

        public void InsertUser(GGUser t)
        {
            using (TransactionScope scope = this.transactionScopeFactory.NewTransactionScope())
            {
                IOrmAccesser<GGUser> accesser = scope.NewOrmAccesser<GGUser>();
                accesser.Insert(t);
                scope.Commit();
            }
        }

        public void InsertGroup(GGGroup t)
        {
            using (TransactionScope scope = this.transactionScopeFactory.NewTransactionScope())
            {
                IOrmAccesser<GGGroup> accesser = scope.NewOrmAccesser<GGGroup>();
                accesser.Insert(t);
                scope.Commit();
            }
        }

        public void DeleteGroup(string groupID)
        {
            using (TransactionScope scope = this.transactionScopeFactory.NewTransactionScope())
            {
                IOrmAccesser<GGGroup> accesser = scope.NewOrmAccesser<GGGroup>();
                accesser.Delete(groupID);
                scope.Commit();
            }
        }

        public void UpdateUser(GGUser t)
        {
            using (TransactionScope scope = this.transactionScopeFactory.NewTransactionScope())
            {
                IOrmAccesser<GGUser> accesser = scope.NewOrmAccesser<GGUser>();
                accesser.Update(t);
                scope.Commit();
            }
        }

        public void UpdateUserFriends(GGUser t)
        {
            using (TransactionScope scope = this.transactionScopeFactory.NewTransactionScope())
            {
                IOrmAccesser<GGUser> accesser = scope.NewOrmAccesser<GGUser>();
                accesser.Update(new ColumnUpdating(GGUser._Friends, t.Friends), new Filter(GGUser._UserID, t.UserID));
                scope.Commit();
            }
        }

        public void UpdateGroup(GGGroup t)
        {
            using (TransactionScope scope = this.transactionScopeFactory.NewTransactionScope())
            {
                IOrmAccesser<GGGroup> accesser = scope.NewOrmAccesser<GGGroup>();
                t.Version += 1;  //2018.09.25
                accesser.Update(t);
                scope.Commit();
            }
        }

        public List<GGUser> GetAllUser()
        {
            List<GGUser> list = new List<GGUser>();
            using (TransactionScope scope = this.transactionScopeFactory.NewTransactionScope())
            {
                IOrmAccesser<GGUser> accesser = scope.NewOrmAccesser<GGUser>();
                list = accesser.GetAll();
                scope.Commit();
            }
            return list;
        }

        public List<GGGroup> GetAllGroup()
        {
            List<GGGroup> list = new List<GGGroup>();
            using (TransactionScope scope = this.transactionScopeFactory.NewTransactionScope())
            {
                IOrmAccesser<GGGroup> accesser = scope.NewOrmAccesser<GGGroup>();
                list = accesser.GetAll();
                scope.Commit();
            }
            return list;
        }

        public void ChangeUserPassword(string userID, string newPasswordMD5)
        {
            using (TransactionScope scope = this.transactionScopeFactory.NewTransactionScope())
            {
                IOrmAccesser<GGUser> accesser = scope.NewOrmAccesser<GGUser>();
                accesser.Update(new ColumnUpdating(GGUser._PasswordMD5, newPasswordMD5), new Filter(GGUser._UserID, userID));
                scope.Commit();
            }
        }

        public void ChangeUserGroups(string userID, string groups)
        {
            using (TransactionScope scope = this.transactionScopeFactory.NewTransactionScope())
            {
                IOrmAccesser<GGUser> accesser = scope.NewOrmAccesser<GGUser>();
                accesser.Update(new ColumnUpdating(GGUser._Groups, groups), new Filter(GGUser._UserID, userID));
                scope.Commit();
            }
        }

        public void UpdateGroupInfo(GGGroup t)
        {
            this.UpdateGroup(t);
        }

        public GGUser GetUser(string userID)
        {
            GGUser user = null;
            using (TransactionScope scope = this.transactionScopeFactory.NewTransactionScope())
            {
                IOrmAccesser<GGUser> accesser = scope.NewOrmAccesser<GGUser>();
                user = accesser.GetOne(userID);
                scope.Commit();
            }
            return user;
        }

        public string GetUserPassword(string userID)
        {
            object pwd = null;
            using (TransactionScope scope = this.transactionScopeFactory.NewTransactionScope())
            {
                IOrmAccesser<GGUser> accesser = scope.NewOrmAccesser<GGUser>();
                pwd = accesser.GetColumnValue(userID, GGUser._PasswordMD5);
                scope.Commit();
            }
            if (pwd == null)
            {
                return null;
            }
            return pwd.ToString();
        }

        public GGGroup GetGroup(string groupID)
        {
            GGGroup group = null;
            using (TransactionScope scope = this.transactionScopeFactory.NewTransactionScope())
            {
                IOrmAccesser<GGGroup> accesser = scope.NewOrmAccesser<GGGroup>();
                group = accesser.GetOne(groupID);
                scope.Commit();
            }
            return group;
        }
    }
   
}
