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
        public object GetAllList(string authorization, string options)
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
                                address = "4401 east high way"
                            }
                        }
                    };

                    return new { inputType, outputType };
                }

                //var input = JsonConvert.DeserializeAnonymousType(options, inputType);
                SecurityToken token = new SecurityToken("admin");

                string sql = string.Format("SELECT [{1}], [{2}] FROM [{0}]",
                    BDBuildingSchema.TableName, BDBuildingSchema.BDBuildingPK, BDBuildingSchema.Address1);
                List<object> list = BDBuildingWrapper.ExecuteList<object>(sql, null, record =>
                {
                    return new
                    {
                        pk = record.GetNextGuid(),
                        address = record.GetNextString()
                    };
                }, token);

                return new
                {
                    ok = true,
                    message = "OK",

                    result = list
                };
            }
            catch (Exception ex)
            {
                return this.ToJsonError(ex.Message);
            }
        }

        public object GetByAddress(string authorization, string options)
        {
            try
            {
                var inputType = new
                {
                    address = "988 changning"
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
                                address = "969 hongqiao"
                            }
                        }
                    };

                    return new { inputType, outputType };
                }

                var input = JsonConvert.DeserializeAnonymousType(options, inputType);
                SecurityToken token = new SecurityToken("admin");
                
                string sql = string.Format("SELECT [{1}], [{2}] FROM [{0}] WHERE [{2}] ILIKE @{2}",
                    BDBuildingSchema.TableName, BDBuildingSchema.BDBuildingPK, BDBuildingSchema.Address1);

                List<DbParameter> paras = new List<DbParameter>();
                BDBuildingWrapper.AddParameter(paras, string.Format("@{0}", BDBuildingSchema.Address1),
                    DbType.String, input.address);

                List<object> list = BDBuildingWrapper.ExecuteList<object>(sql, paras, record =>
                {
                    return new
                    {
                        pk = record.GetNextGuid(),
                        address = record.GetNextString()
                    };
                }, token);

                return new
                {
                    ok = true,
                    message = "OK",

                    result = list
                };
            }
            catch (Exception ex)
            {
                return this.ToJsonError(ex.Message);
            }
        }

        public object Insert(string authorization, string options)
        {
            try
            {
                var inputType = new
                {
                    id = 0,
                    address = ""
                };

                //<ts-auto-generated>
                if (string.IsNullOrEmpty(authorization) && string.IsNullOrEmpty(options))
                {
                    var outputType = new
                    {
                        ok = false,
                        message = ""
                    };

                    return new { inputType, outputType };
                }

                var input = JsonConvert.DeserializeAnonymousType(options, inputType);
                SecurityToken token = new SecurityToken("admin");

                BDBuildingData data = new BDBuildingData();
                data.Address1 = input.address;
                data.BuildingID = input.id;

                Result result = BDBuildingWrapper.Save(data, token);
                if (result.OK)
                {
                    return new
                    {
                        ok = true,
                        message = "OK"
                    };
                }

                return new
                {
                    ok = false,
                    message = result.Error
                };
            }
            catch (Exception ex)
            {
                return this.ToJsonError(ex.Message);
            }
        }

        public object Update(string authorization, string options)
        {
            try
            {
                var inputType = new
                {
                    address = "4401 east high way",
                    id = 1000
                };

                //<ts-auto-generated>
                if (string.IsNullOrEmpty(authorization) && string.IsNullOrEmpty(options))
                {
                    var outputType = new
                    {
                        ok = false,
                        message = ""
                    };

                    return new { inputType, outputType };
                }

                var input = JsonConvert.DeserializeAnonymousType(options, inputType);
                SecurityToken token = new SecurityToken("admin");

                BDBuildingData data = BDBuildingWrapper.GetByAddress1(input.address, token);
                if (data == null)
                {
                    return this.ToJsonError("The address does not exist.");
                }

                data.BuildingID = input.id;

                Result result = BDBuildingWrapper.Save(data, token);
                if (result.OK)
                {
                    return new
                    {
                        ok = true,
                        message = "OK"
                    };
                }

                return new
                {
                    ok = false,
                    message = result.Error
                };
            }
            catch (Exception ex)
            {
                return this.ToJsonError(ex.Message);
            }
        }

        public object InsertWithChildren(string authorization, string options)
        {
            try
            {
                var inputType = new
                {
                    address = "4401 east high way",
                    tenantList = new[]
                    {
                        new
                        {
                            tenantName = "abc",
                            suite = "101"
                        }
                    }
                };

                //<ts-auto-generated>
                if (string.IsNullOrEmpty(authorization) && string.IsNullOrEmpty(options))
                {
                    var outputType = new
                    {
                        ok = false,
                        message = ""
                    };

                    return new { inputType, outputType };
                }

                var input = JsonConvert.DeserializeAnonymousType(options, inputType);
                SecurityToken token = new SecurityToken("admin");

                BDBuildingData data = BDBuildingWrapper.GetByAddress1(input.address, token);
                if (data == null)
                {
                    data = new BDBuildingData();
                    data.Address1 = input.address;
                    data.BuildingID = new Random().Next(1000, 99999);
                }

                data.BDTenantList = new BDTenantDataCollection();
                foreach (var item in input.tenantList)
                {
                    BDTenantData tenant = new BDTenantData();
                    tenant.Tenant = item.tenantName;
                    tenant.Suite = item.suite;
                    tenant.BDBuildingPK = data.BDBuildingPK;

                    data.BDTenantList.Add(tenant);
                }

                Result result = BDBuildingWrapper.Save(data, token);
                if (result.OK)
                {
                    return new
                    {
                        ok = true,
                        message = "OK"
                    };
                }

                return new
                {
                    ok = false,
                    message = result.Error
                };
            }
            catch (Exception ex)
            {
                return this.ToJsonError(ex.Message);
            }
        }
    }
}