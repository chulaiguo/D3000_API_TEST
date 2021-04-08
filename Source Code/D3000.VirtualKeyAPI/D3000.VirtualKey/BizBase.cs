using System;
using D3000.DataService;
using Newtonsoft.Json;

namespace D3000.VirtualKey
{
    public abstract class BizBase
    {
        protected DateTime Now()
        {
            return UsrAccountBuildingMapWrapper.Now();
        }

        protected string ToJsonError(string error)
        {
            var entity = new
            {
                ok = false,
                message = error
            };

            return JsonConvert.SerializeObject(entity);
        }

        //public static string AccessToken
        //{
        //    get { return "FBFF3315-CCAA-3283-9FBA-322EA9DA0A09"; }
        //}

        public string AppUserId
        {
            get { return "D3000.VirtualKey"; }
        }

        public string AppName
        {
            get { return "Direct Connect"; }
        }

        public string AppPassword
        {
            get { return "7E57C707-F028-45C1-9F80-625A70A60D11"; }
        }
    }
}