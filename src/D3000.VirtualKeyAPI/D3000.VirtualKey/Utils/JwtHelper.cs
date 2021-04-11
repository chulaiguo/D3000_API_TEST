using System;
using System.Collections.Generic;
using JWT.Algorithms;
using JWT.Builder;

namespace D3000.VirtualKey.Utils
{
	internal static class JwtHelper
	{
		private static readonly string _Secret = "C785C7F6-99F0-3AFC-86A3-E65BFAE993B6";

		public static string EncodeJwt(PayloadInfo payload)
		{
			return JwtBuilder.Create()
				.WithAlgorithm(new HMACSHA256Algorithm())
				.WithSecret(_Secret)
				.AddClaim("payload", payload.Serialize())
				.Encode();
		}

		public static PayloadInfo DecodeJwt(string token)
		{
			var payload = JwtBuilder.Create()
				.WithAlgorithm(new HMACSHA256Algorithm())
				.WithSecret(_Secret)
				.MustVerifySignature()
				.Decode<IDictionary<string, string>>(token);

			return PayloadInfo.Deserialize(payload["payload"]);
		}
	}

	internal class PayloadInfo
	{
		public long Expired { get; set; } = DateTimeOffset.UtcNow.AddHours(3).ToUnixTimeSeconds();


		public Guid UserPK { get; set; } = Guid.Empty;
		public string UserId { get; set; } = string.Empty;

		public bool IsExpired()
		{
			long current = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
			return current > this.Expired;
		}

		public string Serialize()
		{
			return Newtonsoft.Json.JsonConvert.SerializeObject(this);
		}

		public static PayloadInfo Deserialize(string json)
		{
			return Newtonsoft.Json.JsonConvert.DeserializeObject<PayloadInfo>(json);
		}
	}
}
