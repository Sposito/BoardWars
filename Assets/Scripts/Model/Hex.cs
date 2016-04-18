using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
/// <summary>
/// Class with static mathods to create Color objects from hexadecimal formated strings
/// </summary>
namespace GeneralTools {
	public static class Hex {

		public static Color ToColor(string hex){

			hex.ToUpper (); //Makes it case insensitive

			if (hex [0] == '#') //Ignores hash sign if its present at the beggining of the string
				hex = hex.Substring (1);


			float r = 1f; // red
			float g = 1f; // green
			float b = 1f; // blue
			float a = 1f; // alpha

			if (hex.Length == 6) { // eg: "FF6677"
				r = hexToFloat (hex.Substring (0, 2), 255f);
				g = hexToFloat (hex.Substring (2, 2), 255f);
				b = hexToFloat (hex.Substring (4, 2), 255f);
			}

			else if (hex.Length == 8) { // eg: "FF6677FF"
				r = hexToFloat (hex.Substring (0, 2), 255f);
				g = hexToFloat (hex.Substring (2, 2), 255f);
				b = hexToFloat (hex.Substring (4, 2), 255f);
				a = hexToFloat (hex.Substring (6, 2), 255f);
			}

			else if (hex.Length == 3) { // eg: "F67
				r = hexToFloat (hex.Substring (0, 1), 15f);
				g = hexToFloat (hex.Substring (1, 1), 15f);
				b = hexToFloat (hex.Substring (2, 1), 15f);
			}

			else if (hex.Length == 4) { // eg: "F67F"
				r = hexToFloat (hex.Substring (0, 1), 15f);
				g = hexToFloat (hex.Substring (1, 1), 15f);
				b = hexToFloat (hex.Substring (2, 1), 15f);
				a = hexToFloat (hex.Substring (3, 1), 15f);
			}

			return new Color (r, g, b, a);
		}

		/// <summary>Convert a hexadecimal string into a normalized float</summary>
		public static float hexToFloat(string hex, float maxValue){
			return (float)hexToInt(hex) / maxValue;
		}

		public static int hexToInt(string hex){
			Dictionary<char, int> HexValues = new Dictionary<char, int> () {
				{ '0',  0 }, { '1',  1 }, { '2',  2 }, { '3',  3 }, { '4',  4 }, { '5',  5 }, { '6',  6 }, { '7',  7 },
				{ '8',  8 }, { '9',  9 }, { 'A', 10 }, { 'B', 11 }, { 'C', 12 }, { 'D', 13 }, { 'E', 14 }, { 'F', 15 }
			};

			int num = 0;
			string reverse = Reverse (hex);

			for (int i = 0; i < hex.Length; i++) {
				int iValue = HexValues [reverse [i]];
				num += IntPow(16,i) * iValue;
			}

			return num;
		}
		/// <summary>Reverse an Array</summary>
		private static string Reverse( string s )
		{
			char[] charArray = s.ToCharArray();
			Array.Reverse( charArray );
			return new string( charArray );
		}
		/// <summary>Simpler way to use Power operations with Intagers </summary>
		public static int IntPow(int x, int p){
			// TODO: it is probably faster with a for loop, should be tested in the future
			if (p > 2)
				return x * IntPow (x, p - 1);
			else if (p == 2)
				return x * x;
			else if (p == 1)
				return x;
			else if (p == 0)
				return 1;
			else
				return -1;			
		}
	}
}