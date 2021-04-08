using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using D3000.Data;
using D3000.DataService;
using Jet.Data;
using Newtonsoft.Json;

namespace D3000.VirtualKey
{
    public class BizBuilding : BizBase
    {
        public string GetBuildingList(string authorization, string options)
        {
            try
            {
                var inputType = new
                {
                };

                //<ts-auto-generated>
                if (string.IsNullOrEmpty(authorization) && string.IsNullOrEmpty(options))
                {
                    var outputType = new
                    {
                        ok = false,
                        message = "",

                        result = new[]
                        {
                            new
                            {
                                pk = "00000000-0000-0000-0000-000000000000",
                                address = ""
                            }
                        }
                    };

                    return JsonConvert.SerializeObject(new { inputType, outputType });
                }

                //var input = JsonConvert.DeserializeAnonymousType(options, inputType);
                SecurityToken token = new SecurityToken("admin");
                //BDBuildingDataCollection list = BDBuildingWrapper.GetAll(token);

                //List<DbParameter> paras = new List<DbParameter>();
                //BDBuildingWrapper.AddParameter(paras, string.Format("@{0}", BDBuildingSchema.Address1),
                //    DbType.String, "");

                //string where = "";
                //list = BDBuildingWrapper.GetCollection(where, paras, token);

                string sql = string.Format("SELECT [{1}], [{2}] FROM [{0}]",
                    BDBuildingSchema.TableName, BDBuildingSchema.BDBuildingPK, BDBuildingSchema.Address1);
                //List<object> list = BDBuildingWrapper.ExecuteList<object>(sql, null, record =>
                //{
                //    return new
                //    {
                //        pk = record.GetNextGuid(),
                //        addrrss = record.GetNextString()
                //    };
                //}, token);

                //string json = JsonConvert.SerializeObject(list);

                string json = BDBuildingWrapper.ExecuteJson(sql, null, token);

                //List<object> retList = new List<object>();
                //foreach (BDBuildingData item in list)
                //{
                //    retList.Add(new
                //    {
                //        pk = item.BDBuildingPK,
                //        address = item.Address1
                //    });
                //}

                var retEntity = new
                {
                    ok = true,
                    message = "OK",

                    result = json
                };

                return JsonConvert.SerializeObject(retEntity);
            }
            catch (Exception ex)
            {
                return this.ToJsonError(ex.Message);
            }
        }
    }
}