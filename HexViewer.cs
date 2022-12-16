using System;
using System.Collections.Generic;
using System.IO;
using static System.Math;

namespace ASC_Hex_Viewer {
	public class HexViewer {
		public HexViewer() {}

		public List<string> ReadStrings(string filename, int lineLength, int maxLinesCount) {
			using(FileStream fs = File.OpenRead(filename)) {
				int startIndex = 0, linesCount = 0;
				byte[] data = new byte[lineLength];
				List<string> toR = new List<string>();

				while(fs.Read(data, 0, lineLength) > 0 && linesCount < maxLinesCount) {
					string line = "";
					foreach(byte b in data) { line += (char)b; }

					toR.Add(line);
					startIndex += lineLength;
					linesCount++;
				}

				fs.Close();

				return toR;
			}
		}

		public List<byte[]> ReadBytes(string filename, int lineLength, int maxLinesCount) {
			using(FileStream fs = File.OpenRead(filename)) {
				int linesCount = 0;
				byte[] data = new byte[lineLength];
				List<byte[]> toR = new List<byte[]>();

				while(fs.Read(data, 0, lineLength) > 0 && linesCount < maxLinesCount) {
					byte[] line = new byte[lineLength];

					for(int i = 0; i < lineLength; i++) {
						line[i] = data[i];
					}

					linesCount++;
					toR.Add(line);
				}
				
				fs.Close();

				return toR;
			}
		}

		public string DisplayLine(byte[] byteLine, int lineCount, int linesNr) {
			string sep1 = ": ";
			string sep2 = "| ";
			return DisplayLine(byteLine, lineCount, linesNr, sep1, sep2);
		}

		public string DisplayLine(byte[] byteLine, int lineCount, int linesNr, string sep) {
			return DisplayLine(byteLine, lineCount, linesNr, sep, sep);
		}

		public string DisplayLine(byte[] byteLine, int lineCount, int linesNr, string sep1, string sep2) {
			string index = "";
			string hex = "";
			string ascii = "";
			int padding = 0;

			if(lineCount < linesNr) {
				if(lineCount > 0) padding = (int)Floor(Log10(linesNr));
				else padding = (int)Floor(Log10(linesNr));
			}

			index = lineCount.ToString().PadLeft(padding + 2, '0');
			index = index.PadRight(index.Length + 1, '0');

			foreach(byte b in byteLine) {
				string v = Convert.ToString(b, 16);
				if(v.Length == 1) v = v.PadLeft(2, '0');
				hex += v + " ";
			}

			foreach(byte b in byteLine) {
				if(b > 31) ascii += (char)b;
				else ascii += '.';
			}

			return (index + sep1 + hex + sep2 + ascii).ToUpper();
		}


	}
}
