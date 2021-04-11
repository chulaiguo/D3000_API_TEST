using System;
using Newtonsoft.Json;


namespace D3000.VirtualKey
{
	public class BizLogin : BizBase
	{
		public object Login(string authorization, string options)
		{
			try
			{
				var inputType = new
				{
					userId = "admin",
					password = "123456"
				};

				//<ts-auto-generated>
				if (string.IsNullOrEmpty(authorization) && string.IsNullOrEmpty(options))
				{
					var outputType = new
					{
						ok = false,
						message = "",

						result = ""
					};

					return new { inputType, outputType };
				}

				var input = JsonConvert.DeserializeAnonymousType(options, inputType);
				//check input user

				//check success
				UserInfo user = new UserInfo();
				user.UserPK = Guid.NewGuid();
				user.UserId = input.userId;

				string x_auth_token = this.CreateAuthorization(user);
				return new
				{
					ok = true,
					message = "OK",

					result = x_auth_token
				};
			}
			catch (Exception ex)
			{
				return this.ToJsonError(ex.Message);
			}
		}
	}
}
