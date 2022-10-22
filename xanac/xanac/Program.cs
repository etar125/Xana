/*
By InnieSharp(ix4/i#)
*/
using System;
using System.IO;
using System.Threading;
using System.Diagnostics;
using Microsoft.Win32;
using System.Linq;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Text;
using System.Security.Principal;
using System.Globalization;
using System.Web;
using System.Net;
using System.Windows.Forms;
using System.Drawing;

namespace xanac
{
	class Program
	{
		public static string path;
        public static List<string> vars;
        public static bool logs;
        public static string logf;
		
		public static void Main(string[] args)
		{
			vars = new List<string>
            {
            	"curdir=" + Environment.CurrentDirectory,
            	"sysdir=" + Environment.SystemDirectory,
            	"machinename=" + Environment.MachineName,
            	"username=" + Environment.UserName,
            	"osver=" + Environment.OSVersion
            };
			vars = new List<string>
			{
				"",
				""
			};
			
			bool have = false;
            foreach(string l in args)
            {
            	string s = l;
            	if(!have)
            	{
            		if(s.StartsWith("file:"))
            		{
            			s = s.Remove(0, 5);
	            		if(File.Exists(s))
	            		{
		        			path = s;
		        			have = true;
	            		}
	            		else
	            		{
	            			Console.WriteLine("Not found file " + s);
	            			Console.ReadKey();
        					Environment.Exit(0);
	            		}
	            		break;
            		}
            		else if(s == "ver")
            		{
            			Console.WriteLine("Xana by ix4Software");
            			Console.ReadKey();
    					Environment.Exit(0);
            		}
            		else
            		{
            			Console.WriteLine("Wrong argument " + s);
            			Console.ReadKey();
    					Environment.Exit(0);
            		}
            	}
            }
            if(!have)
            {
	            if(File.Exists("main.x"))
	        		path = "main.x";
	        	else
	        	{
	        		Console.WriteLine("Not found main.x");
	            	Console.ReadKey();
	        		Environment.Exit(0);
	        	}
            }

            //other
            logs = false;
            logf = "logs.txt";
            
            // Вызываем основной метод
            oth();
		}
		
		public static void oth()
		{
			string[] l = File.ReadAllLines(path);
			for(int i = 0; i < l.Length; i++)
			{
				try
				{
					if(l[i].StartsWith("print "))
					{
						string str = l[i].Remove(0, 6);
						string[] splitstr = { "&&&" };
						string[] splitres = str.Split(splitstr, StringSplitOptions.None);
						string result = "";
						for(int ch = 0; ch < splitres.Length; ch++)
						{
							string str2 = splitres[ch];
							if(str2.StartsWith("/"))
							{
								str2 = str2.Remove(0, 1);
								result += str2;
							}
							else if(str2.StartsWith("$"))
							{
								string var = str2.Remove(0, 1);
								bool act = false;
								for(int ln = 0; ln < vars.Count; ln++)
								{
									if(vars[ln].StartsWith(var + "="))
									{
										result += vars[ln].Substring(vars[ln].IndexOf("=") + 1);
										act = true;
										break;
									}
								}
								if(!act)
								{
									result += "empty";
								}
							}
							else if(str2 == "time")
							{
								result += DateTime.Now.ToString("HH:mm:ss");
							}
							else if(str2 == "dd.MM.yyyy")
							{
								result += DateTime.Now.ToString("HH:mm:ss");
							}
							else
							{
								result += str2;
							}
						}
						str = result;
						Console.WriteLine(str);
					}
					else if(l[i].StartsWith("color "))
					{
						string stra = l[i].Remove(0, 6);
						string[] splitstra = { " " };
						string[] splitresa = stra.Split(splitstra, StringSplitOptions.None);
						string bg = splitresa[1];
						string fg = splitresa[0];
						string[] splitstr = { "&&&" };
						string[] splitres = fg.Split(splitstr, StringSplitOptions.None);
						string result = "";
						for(int ch = 0; ch < splitres.Length; ch++)
						{
							string str2 = splitres[ch];
							if(str2.StartsWith("/"))
							{
								str2 = str2.Remove(0, 1);
								result += str2;
							}
							else if(str2.StartsWith("$"))
							{
								string var = str2.Remove(0, 1);
								bool act = false;
								for(int ln = 0; ln < vars.Count; ln++)
								{
									if(vars[ln].StartsWith(var + "="))
									{
										result += vars[ln].Substring(vars[ln].IndexOf("=") + 1);
										act = true;
										break;
									}
								}
								if(!act)
								{
									result += "empty";
								}
							}
							else if(str2 == "time")
							{
								result += DateTime.Now.ToString("HH:mm:ss");
							}
							else if(str2 == "dd.MM.yyyy")
							{
								result += DateTime.Now.ToString("HH:mm:ss");
							}
							else
							{
								result += str2;
							}
						}
						fg = result;
						splitres = bg.Split(splitstr, StringSplitOptions.None);
						result = "";
						for(int ch = 0; ch < splitres.Length; ch++)
						{
							string str2 = splitres[ch];
							if(str2.StartsWith("/"))
							{
								str2 = str2.Remove(0, 1);
								result += str2;
							}
							else if(str2.StartsWith("$"))
							{
								string var = str2.Remove(0, 1);
								bool act = false;
								for(int ln = 0; ln < vars.Count; ln++)
								{
									if(vars[ln].StartsWith(var + "="))
									{
										result += vars[ln].Substring(vars[ln].IndexOf("=") + 1);
										act = true;
										break;
									}
								}
								if(!act)
								{
									result += "empty";
								}
							}
							else if(str2.StartsWith("fromfile:"))
							{
								str2 = str2.Remove(0, 9);
								string[] splitstrs = { "||" };
								string[] splitress = str2.Split(splitstrs, StringSplitOptions.None);
								string file = splitress[0];
								string[] splitstrae = { "&&&" };
								string[] splitresae = file.Split(splitstrae, StringSplitOptions.None);
								string resulta = "";
								for(int cha = 0; ch < splitresae.Length; ch++)
								{
									string str2a = splitresae[ch];
									if(str2a.StartsWith("/"))
									{
										str2a= str2a.Remove(0, 1);
										resulta += str2a;
									}
									else if(str2a.StartsWith("$"))
									{
										string var = str2a.Remove(0, 1);
										bool act = false;
										for(int ln = 0; ln < vars.Count; ln++)
										{
											if(vars[ln].StartsWith(var + "="))
											{
												resulta += vars[ln].Substring(vars[ln].IndexOf("=") + 1);
												act = true;
												break;
											}
										}
										if(!act)
										{
											resulta += "empty";
										}
									}
									else if(str2a == "time")
									{
										resulta += DateTime.Now.ToString("HH:mm:ss");
									}
									else if(str2a == "dd.MM.yyyy")
									{
										resulta += DateTime.Now.ToString("HH:mm:ss");
									}
									else
									{
										resulta += str2a;
									}
								}
								file = resulta;
								int lna = int.Parse(splitress[1]);
								if(File.Exists(file))
								{
									string[] fils = File.ReadAllLines(file);
									result += fils[lna];
								}
								else
								{
									result += "empty";
								}
							}
							else if(str2 == "time")
							{
								result += DateTime.Now.ToString("HH:mm:ss");
							}
							else if(str2 == "dd.MM.yyyy")
							{
								result += DateTime.Now.ToString("HH:mm:ss");
							}
							else
							{
								result += str2;
							}
						}
						bg = result;
						switch (fg)
						{
							case "gray":
								Console.ForegroundColor = ConsoleColor.Gray;
								break;
							case "black":
								Console.ForegroundColor = ConsoleColor.Black;
								break;
							case "red":
								Console.ForegroundColor = ConsoleColor.Red;
								break;
							case "yellow":
								Console.ForegroundColor = ConsoleColor.Yellow;
								break;
							case "green":
								Console.ForegroundColor = ConsoleColor.Green;
								break;
							case "blue":
								Console.ForegroundColor = ConsoleColor.Blue;
								break;
							case "white":
								Console.ForegroundColor = ConsoleColor.White;
								break;
							case "cyan":
								Console.ForegroundColor = ConsoleColor.Cyan;
								break;
							default:
								Console.ForegroundColor = ConsoleColor.Gray;
								break;
						}
						switch (bg)
						{
							case "gray":
								Console.BackgroundColor = ConsoleColor.Gray;
								break;
							case "black":
								Console.BackgroundColor = ConsoleColor.Black;
								break;
							case "red":
								Console.BackgroundColor = ConsoleColor.Red;
								break;
							case "yellow":
								Console.BackgroundColor = ConsoleColor.Yellow;
								break;
							case "green":
								Console.BackgroundColor = ConsoleColor.Green;
								break;
							case "blue":
								Console.BackgroundColor = ConsoleColor.Blue;
								break;
							case "white":
								Console.BackgroundColor = ConsoleColor.White;
								break;
							case "cyan":
								Console.BackgroundColor = ConsoleColor.Cyan;
								break;
							default:
								Console.BackgroundColor = ConsoleColor.Black;
								break;
						}
					}
					else if(l[i].StartsWith("caption "))
					{
						string str = l[i].Remove(0, 8);
						string[] splitstr = { "&&&" };
						string[] splitres = str.Split(splitstr, StringSplitOptions.None);
						string result = "";
						for(int ch = 0; ch < splitres.Length; ch++)
						{
							string str2 = splitres[ch];
							if(str2.StartsWith("/"))
							{
								str2 = str2.Remove(0, 1);
								result += str2;
							}
							else if(str2.StartsWith("$"))
							{
								string var = str2.Remove(0, 1);
								bool act = false;
								for(int ln = 0; ln < vars.Count; ln++)
								{
									if(vars[ln].StartsWith(var + "="))
									{
										result += vars[ln].Substring(vars[ln].IndexOf("=") + 1);
										act = true;
										break;
									}
								}
								if(!act)
								{
									result += "empty";
								}
							}
							else if(str2.StartsWith("fromfile:"))
							{
								str2 = str2.Remove(0, 9);
								string[] splitstrs = { "||" };
								string[] splitress = str2.Split(splitstrs, StringSplitOptions.None);
								string file = splitress[0];
								string[] splitstra = { "&&&" };
								string[] splitresa = file.Split(splitstr, StringSplitOptions.None);
								string resulta = "";
								for(int cha = 0; ch < splitresa.Length; ch++)
								{
									string str2a = splitresa[cha];
									if(str2a.StartsWith("/"))
									{
										str2a= str2a.Remove(0, 1);
										resulta += str2a;
									}
									else if(str2a.StartsWith("$"))
									{
										string var = str2a.Remove(0, 1);
										bool act = false;
										for(int ln = 0; ln < vars.Count; ln++)
										{
											if(vars[ln].StartsWith(var + "="))
											{
												resulta += vars[ln].Substring(vars[ln].IndexOf("=") + 1);
												act = true;
												break;
											}
										}
										if(!act)
										{
											resulta += "empty";
										}
									}
									else if(str2a == "time")
									{
										resulta += DateTime.Now.ToString("HH:mm:ss");
									}
									else if(str2a == "dd.MM.yyyy")
									{
										resulta += DateTime.Now.ToString("HH:mm:ss");
									}
									else
									{
										resulta += str2a;
									}
								}
								file = resulta;
								int lna = int.Parse(splitress[1]);
								if(File.Exists(file))
								{
									string[] fils = File.ReadAllLines(file);
									result += fils[lna];
								}
								else
								{
									result += "empty";
								}
							}
							else if(str2 == "time")
							{
								result += DateTime.Now.ToString("HH:mm:ss");
							}
							else if(str2 == "dd.MM.yyyy")
							{
								result += DateTime.Now.ToString("HH:mm:ss");
							}
							else
							{
								result += str2;
							}
						}
						str = result;
						Console.Title = str;
					}
					else if(l[i].StartsWith("var "))
					{
						string stra = l[i].Remove(0, 4);
						string var1 = stra.Substring(0, stra.IndexOf("="));
						string str = stra.Substring(stra.IndexOf("=") + 1);
						string[] splitstr = { "&&&" };
						string[] splitres = str.Split(splitstr, StringSplitOptions.None);
						string result = "";
						for(int ch = 0; ch < splitres.Length; ch++)
						{
							string str2 = splitres[ch];
							if(str2.StartsWith("/"))
							{
								str2 = str2.Remove(0, 1);
								result += str2;
							}
							else if(str2.StartsWith("$"))
							{
								string var = str2.Remove(0, 1);
								bool act = false;
								for(int ln = 0; ln < vars.Count; ln++)
								{
									if(vars[ln].StartsWith(var + "="))
									{
										result += vars[ln].Substring(vars[ln].IndexOf("=") + 1);
										act = true;
										break;
									}
								}
								if(!act)
								{
									result += "empty";
								}
							}
							else if(str2 == "time")
							{
								result += DateTime.Now.ToString("HH:mm:ss");
							}
							else if(str2 == "dd.MM.yyyy")
							{
								result += DateTime.Now.ToString("HH:mm:ss");
							}
							else
							{
								result += str2;
							}
						}
						str = result;
						bool d = false;
						for (int ch = 0; ch < vars.Count; ch++)
	                    {
	                        if (vars[ch].StartsWith(var1 + "="))
	                        {
	                        	vars[ch] = var1 + "=" + str;
	                        	d = true;
	                            break;
	                        }
	                    }
	                    if(!d)
	                    	vars.Add(var1 + "=" + str);
					}
					else if(l[i].StartsWith("set "))
					{
						string stra = l[i].Remove(0, 4);
						string var1 = stra.Substring(0, stra.IndexOf("="));
						string str = stra.Substring(stra.IndexOf("=") + 1);
						string[] splitstr = { "&&&" };
						string[] splitres = str.Split(splitstr, StringSplitOptions.None);
						string result = "";
						for(int ch = 0; ch < splitres.Length; ch++)
						{
							string str2 = splitres[ch];
							if(str2.StartsWith("/"))
							{
								str2 = str2.Remove(0, 1);
								result += str2;
							}
							else if(str2.StartsWith("$"))
							{
								string var = str2.Remove(0, 1);
								bool act = false;
								for(int ln = 0; ln < vars.Count; ln++)
								{
									if(vars[ln].StartsWith(var + "="))
									{
										result += vars[ln].Substring(vars[ln].IndexOf("=") + 1);
										act = true;
										break;
									}
								}
								if(!act)
								{
									result += "empty";
								}
							}
							else if(str2 == "time")
							{
								result += DateTime.Now.ToString("HH:mm:ss");
							}
							else if(str2 == "dd.MM.yyyy")
							{
								result += DateTime.Now.ToString("HH:mm:ss");
							}
							else
							{
								result += str2;
							}
						}
						str = result;
						Console.Write(str); string va = Console.ReadLine();
						bool d = false;
						for (int ch = 0; ch < vars.Count; ch++)
	                    {
	                        if (vars[ch].StartsWith(var1 + "="))
	                        {
	                        	vars[ch] = var1 + "=" + va;
	                        	d = true;
	                            break;
	                        }
	                    }
	                    if(!d)
	                    	vars.Add(var1 + "=" + va);
					}
					else if(l[i] == "clear")
						Console.Clear();
					else if(l[i] == "pause")
						Console.ReadKey(true);
					else if(l[i] == "exit")
						Environment.Exit(0);
					else if(l[i].StartsWith("if "))
					{
						string def = l[i].Remove(0, 3);
						string[] splita = { " " };
						string[] st = def.Split(splita, StringSplitOptions.None);
						string vara = st[0];
						string actu = st[1];
						string txt = st[2];
						string func = st[3];
						bool act1 = false;
						for(int ln = 0; ln < vars.Count; ln++)
						{
							if(vars[ln].StartsWith(vara + "="))
							{
								vara = vars[ln].Substring(vars[ln].IndexOf("=") + 1);
								act1 = true;
								break;
							}
						}
						if(!act1)
						{
							vara = "empty";
						}
						string[] splitstr = { "&&&" };
						string[] splitres = txt.Split(splitstr, StringSplitOptions.None);
						string result = "";
						for(int ch = 0; ch < splitres.Length; ch++)
						{
							string str2 = splitres[ch];
							if(str2.StartsWith("/"))
							{
								str2 = str2.Remove(0, 1);
								result += str2;
							}
							else if(str2.StartsWith("$"))
							{
								string var = str2.Remove(0, 1);
								bool act = false;
								for(int ln = 0; ln < vars.Count; ln++)
								{
									if(vars[ln].StartsWith(var + "="))
									{
										result += vars[ln].Substring(vars[ln].IndexOf("=") + 1);
										act = true;
										break;
									}
								}
								if(!act)
								{
									result += "empty";
								}
							}
							else if(str2 == "time")
							{
								result += DateTime.Now.ToString("HH:mm:ss");
							}
							else if(str2 == "dd.MM.yyyy")
							{
								result += DateTime.Now.ToString("HH:mm:ss");
							}
							else
							{
								result += str2;
							}
						}
						txt = result;
						if(actu == "=")
						{
							if(vara == txt)
							{
								for(int ab = 0; ab < l.Length; ab++)
								{
									if(l[ab] == "[" + func + "]")
									{
										i = ab;
										break;
									}
								}
							}
						}
						if(actu == ">")
						{
							double a = double.Parse(vara);
							double b = double.Parse(txt);
							if(a > b)
							{
								for(int ab = 0; ab < l.Length; ab++)
								{
									if(l[ab] == "[" + func + "]")
									{
										i = ab;
										break;
									}
								}
							}
						}
						if(actu == "<")
						{
							double a = double.Parse(vara);
							double b = double.Parse(txt);
							if(a < b)
							{
								for(int ab = 0; ab < l.Length; ab++)
								{
									if(l[ab] == "[" + func + "]")
									{
										i = ab;
										break;
									}
								}
							}
						}
						if(actu == "!")
						{
							if(vara != txt)
							{
								for(int ab = 0; ab < l.Length; ab++)
								{
									if(l[ab] == "[" + func + "]")
									{
										i = ab;
										break;
									}
								}
							}
						}
						if(actu == "?")
						{
							if(vara.Contains(txt))
							{
								for(int ab = 0; ab < l.Length; ab++)
								{
									if(l[ab] == "[" + func + "]")
									{
										i = ab;
										break;
									}
								}
							}
						}
						if(actu == "(")
						{
							if(vara.StartsWith(txt))
							{
								for(int ab = 0; ab < l.Length; ab++)
								{
									if(l[ab] == "[" + func + "]")
									{
										i = ab;
										break;
									}
								}
							}
						}
						if(actu == ")")
						{
							if(vara.EndsWith(txt))
							{
								for(int ab = 0; ab < l.Length; ab++)
								{
									if(l[ab] == "[" + func + "]")
									{
										i = ab;
										break;
									}
								}
							}
						}
					}
				}
				catch
				{
					
				}
			}
		}
	}
}