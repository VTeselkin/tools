using System.IO;
namespace GameSoft.Tools.Extensions
{
	public static class FileExtensions
	{
		public static void CreateDirectoryIfNull(string path)
		{
			DirectoryInfo info = new DirectoryInfo(path);
			if (!info.Exists)
			{
				info.Create();
			}
		}
	}
}