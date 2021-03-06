﻿using MVC_homework.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Web;

namespace MVC_homework.Service
{
    public static class KeepAccountsAPI
    {
        private static Model1 _model1 = new Model1();

        public static List<KeepAccountsViewModel> GetData()
        {
            #region 自訂資料來源

            //DateTime now = DateTime.Now;
            //List<KeepAccountsViewModel> list = new List<KeepAccountsViewModel>();
            //for (int i = 1; i <= 100; i++)
            //{
            //    KeepAccountsViewModel account = new KeepAccountsViewModel();
            //    DateTime date = now.AddDays(-i);

            //    //5號領薪水
            //    if(date.Day == 5)
            //    {
            //        account.Type = (int)KeepAccountType.income;
            //        account.Money = 100000;
            //        account.Date = date;
            //        list.Add(account);
            //    }

            //    account = new KeepAccountsViewModel();

            //    //一天限制只能花1000以下
            //    account.Type = (int)KeepAccountType.expenditure;
            //    account.Money = new Random(Guid.NewGuid().GetHashCode()).Next(0, 1000);
            //    account.Date = date;

            //    list.Add(account);
            //}

            #endregion
            
            List<KeepAccountsViewModel> list = new List<KeepAccountsViewModel>();
            var accountBooks = _model1.AccountBooks.OrderByDescending(x=>x.Dateee);

            foreach (var item in accountBooks)
            {
                list.Add(new KeepAccountsViewModel {
                    ID= item.Id,
                    Date = item.Dateee,
                    Money = item.Amounttt,
                    Type = GetDescription((KeepAccountType)item.Categoryyy),
                    Remark = item.Remarkkk
                });
            }
                        
            return list;
        }

        public enum KeepAccountType
        {
            [Description("收入")]
            income,
            [Description("支出")]
            expenditure
        }

        public static string GetDescription(this Enum value)
        {
            FieldInfo field = value.GetType().GetField(value.ToString());

            DescriptionAttribute attribute
                    = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute))
                        as DescriptionAttribute;

            return attribute == null ? value.ToString() : attribute.Description;
        }

        public static Dictionary<int,string> GetKeepAccountType()
        {
            Dictionary<int, string> typies = new Dictionary<int, string>();
            foreach (var item in Enum.GetValues(typeof(KeepAccountType)))
            {
                KeepAccountType type = (KeepAccountType)item;
                typies.Add((int)type, GetDescription(type));
            }
            return typies;
        }

        public static Dictionary<string,string> DataCheck(NameValueCollection collection)
        {
            Dictionary<string, string> errorMsg = new Dictionary<string, string>();

            DateTime date;
            if (!DateTime.TryParse(collection["Date"], out date))
                errorMsg.Add("Date","日期格式錯誤!");

            int amount;
            if (!int.TryParse(collection["Amount"], out amount))
                errorMsg.Add("Amount", "金額格式錯誤!");

            return errorMsg;
        }

        public static bool Create(NameValueCollection collection)
        {
            try
            {
                _model1.AccountBooks.Add(
                            new AccountBook
                            {
                                Id = Guid.NewGuid(),
                                Amounttt = Convert.ToInt32(collection["Amount"]),
                                Categoryyy = Convert.ToInt32(collection["Category"]),
                                Dateee = Convert.ToDateTime(collection["Date"]),
                                Remarkkk = collection["Remark"]
                            });

                _model1.SaveChanges();
            }
            catch(Exception ex)
            {
                return false;
            }

            return true;            
        }

        public static bool Edit(NameValueCollection collection,Guid? id)
        {
            var accountBooks = _model1.AccountBooks.Find(id);
            if (accountBooks == null)
                return false;

            accountBooks.Amounttt = Convert.ToInt32(collection["Amount"]);
            accountBooks.Categoryyy = Convert.ToInt32(collection["Category"]);
            accountBooks.Dateee = Convert.ToDateTime(collection["Date"]);
            accountBooks.Remarkkk = collection["Remark"];

            try
            {
                _model1.SaveChanges();
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public static bool Delete(Guid id)
        {
            var accountBook = _model1.AccountBooks.Find(id);
            if (accountBook == null)
                return false;

            _model1.AccountBooks.Remove(accountBook);

            try
            {
                _model1.SaveChanges();
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }
    }
}