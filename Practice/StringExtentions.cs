using System.Runtime.CompilerServices;

namespace Practice
{
	public static class StringExtentions
	{
		public static string AddThreeDots( this string content)
		{
			return $"{content}...";
		}
	}
}
