using System;
using System.Text;

namespace Aid
{
	public static class TextFormater
	{
		private const string TimeWithDays = "{0} d {1} h";
		private const string TimeWithHours = "{0} h {1} m";
		private const string TimeWithMinutes = "{0} m {1} s";
		private const string TimeWithSeconds = "{0} s";

		public static string FormatTime(TimeSpan timeSpan)
		{
			if (timeSpan.Days > 0)
				return string.Format(TimeWithDays, timeSpan.Days, timeSpan.Hours);

			if (timeSpan.Hours > 0)
				return string.Format(TimeWithHours, timeSpan.Hours, timeSpan.Minutes);

			if (timeSpan.Minutes > 0)
				return string.Format(TimeWithMinutes, timeSpan.Minutes, timeSpan.Seconds);

			return string.Format(TimeWithSeconds, timeSpan.Seconds);
		}

		private static readonly string[] Units =
		{
			"K", "M", "B", "T", "aa", "bb", "cc", "dd", "ee", "ff", "gg", "hh", "ii", "jj", "kk", "ll", "mm", "nn", "oo",
			"pp", "qq", "rr", "ss", "tt", "uu", "vv", "ww", "xx", "yy", "zz"
		};

		public static string FormatCurrency(long currency)
		{
			const string dot = ".";

			
			var bigNumber = 1000L;
			var unitIndex = -1;

			if (currency >= bigNumber)
			{
				while (currency >= bigNumber)
				{
					bigNumber *= 1000;
					unitIndex++;
				}

				bigNumber /= 1000;
				var unit = Units[unitIndex];
				var rest = (currency % bigNumber) / (bigNumber / 10);
				currency /= bigNumber;

				if (currency > 100)
				{
					const string result = "{0}{1}";

					return string.Format(result, currency, unit);
				}

				const string longResult = "{0}{1}{2}{3}";
				return string.Format(longResult, currency, dot, rest, unit);
			}

			return currency.ToString();
		}
	}
}