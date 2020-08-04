using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Huatek.Torch.Promotions.APP
{
	public class AppSettings
	{
		public string Str { get; set; }

		public int Num { get; set; }

		public List<int> Arr { get; set; }

		public SubObj SubObj { get; set; }
	}

	public class SubObj
	{
		public string a { get; set; }
	}
}
