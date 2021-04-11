using System;
using D3000.DataService;
using D3000.VirtualKey.Utils;

namespace D3000.VirtualKey
{
	public abstract class BizBase
	{
		protected DateTime Now()
		{
			return UsrAccountBuildingMapWrapper.Now();
		}

		protected object ToJsonError(string error)
		{
			return new
			{
				ok = false,
				message = error
			};
		}

		//public static string AccessToken
		//{
		//    get { return "FBFF3315-CCAA-3283-9FBA-322EA9DA0A09"; }
		//}

		public string AppName
		{
			get { return "Direct Connect"; }
		}

		protected string CreateAuthorization(UserInfo user)
		{
			return JwtHelper.EncodeJwt(user.ToPayload());
		}

		protected UserInfo CheckAuthorize(string authorization)
		{
			PayloadInfo payload = JwtHelper.DecodeJwt(authorization);
			if (payload.IsExpired())
			{
				throw new Exception("The user authentication has expired.");
			}

			return UserInfo.CreateFrom(payload);
		}

		protected class UserInfo
		{
			public Guid UserPK { get; set; } = Guid.Empty;
			public string UserId { get; set; } = string.Empty;

			internal PayloadInfo ToPayload()
			{
				PayloadInfo payload = new PayloadInfo();
				payload.UserId = this.UserId;
				payload.UserPK = this.UserPK;
				return payload;
			}

			internal static UserInfo  CreateFrom(PayloadInfo payload)
			{
				UserInfo user = new UserInfo();
				user.UserId = payload.UserId;
				user.UserPK = payload.UserPK;

				return user;
			}
		}
	}
}
