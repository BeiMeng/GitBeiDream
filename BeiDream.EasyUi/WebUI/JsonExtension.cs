using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using Util.Validations;

namespace BeiDream.EasyUi.WebUI
{
    public static class JsonExtension
    {
        public static JsonResultExtension ExtjsJsonResult(this Controller c, bool isSuccess, object data = null, List<string> msg = null)
        {
            JsonResultExtension result = new JsonResultExtension {ExtjsUiSerialize = true};
            string msgs = "";
            if(msg!=null)
            {
                msgs = msg.Aggregate(msgs, (current, message) => current + (message + "<br/>"));
            }
            result.Msg = msgs;
            result.Success = isSuccess;
            result.Data = data;
            return result;
        }
        public static JsonResultExtension ExtjsGridJsonResult(this Controller c, object data,long total)
        {
            dynamic errors = new System.Dynamic.ExpandoObject();
            JsonResultExtension result = new JsonResultExtension
            {
                ExtjsUiSerialize = true,
                Success = true,
                Total = total,
                Data = data
            };
            return result;
        }

        public static JsonResultExtension ValidateJsonResult(this Controller c, bool isSuccess,
            ValidationResultCollection validationResultCollection = null, List<string> msg = null)
        {
            JsonResultExtension result = new JsonResultExtension {ExtjsUiSerialize = true, Success = isSuccess};
            string msgs = "";
            if (msg != null)
            {
                foreach (var message in msg)
                {
                    msgs += message + ";";
                }
            }
            if (validationResultCollection != null && validationResultCollection.Count != 0)
            {
                foreach (var item in validationResultCollection)
                {
                    msgs += item.ErrorMessage + ";";
                }
            }
            result.Msg = msgs;
            return result;
        }

        public static JsonResultExtension ExtjsFromJsonResult(this Controller c, bool isSuccess, ValidationResultCollection ValidationResultCollection = null, List<string> msg = null)
        {
            JsonResultExtension result = new JsonResultExtension();
            if (ValidationResultCollection != null && ValidationResultCollection.Count != 0)
            {
                Dictionary<string, string> dictionary = new Dictionary<string, string>();
                foreach (var item in ValidationResultCollection)
                {
                    dictionary.Add(item.MemberNames.First(), item.ErrorMessage);
                }
                result.Errors = dictionary;
            }
            else
            {
                result.Errors = null;
            }
            result.ExtjsUiSerialize = true;
            result.Success = isSuccess;
            string msgs = "";
            if (msg != null)
            {
                foreach (var message in msg)
                {
                    msgs += message + "<br/>";
                }
            }
            result.Msg = msgs;
            return result;
        }
    }
}