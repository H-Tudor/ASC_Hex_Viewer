using System;
using System.Collections.Generic;

namespace ASC_Hex_Viewer {
	internal class Program {
		static void Main(string[] args) {
			HexViewer hv = new HexViewer();
			string filename;
			int lineLength;

			while(true) {
				(filename, lineLength) = PrintHeader();

				List<byte[]> lines = hv.ReadBytes(filename, lineLength, 4096);
				int linesCount = lines.Count;

				for(int i = 0; i < linesCount; i++){
					Console.WriteLine(hv.DisplayLine(lines[i], i, linesCount));
				}

				Refresh();
			}
		}
		
		static (string, int) PrintHeader() {
			Console.WriteLine($"Current Directory: {System.IO.Directory.GetCurrentDirectory()}\\..\\..\\");

			Console.Write("Enter filename: ");
			string filename = $"{System.IO.Directory.GetCurrentDirectory()}\\..\\..\\{Console.ReadLine()}";
			Console.WriteLine($"Current selected file: {filename}\r\n");

			Console.Write("Enter line length: ");
			int lineLength = int.Parse(Console.ReadLine());

			return (filename, lineLength);
		}

		static void Refresh() {
			Console.WriteLine("Press any key to try another time");
			Console.ReadKey();
			Console.Clear();
		}
	}
}
