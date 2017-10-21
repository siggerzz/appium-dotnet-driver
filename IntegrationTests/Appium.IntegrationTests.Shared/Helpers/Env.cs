﻿using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace Appium.IntegrationTests.Shared.Helpers
{
	public class Env
	{
		public static TimeSpan INIT_TIMEOUT_SEC = TimeSpan.FromSeconds(180);
		public static TimeSpan IMPLICIT_TIMEOUT_SEC = TimeSpan.FromSeconds(5);
		public static string ASSETS_ROOT_DIR = Path.GetFullPath(AppDomain.CurrentDomain.BaseDirectory + "../../assets");

		private static Dictionary<string, string> env;
		private static bool initialized = false;
		private static void Init() {
			try {
				if(!initialized) 
				{
					initialized = true;
					string path = AppDomain.CurrentDomain.BaseDirectory + "../../";
					StreamReader sr = new StreamReader(path + "env.json");
					string jsonString = sr.ReadToEnd();
					env = JsonConvert.DeserializeObject(jsonString, typeof(Dictionary<string, string>)) as Dictionary<string, string>;
				}
			} catch {
				env = new Dictionary<string, string> ();
			}
		}

		private static bool isTrue(string val) {
			if (val != null) {
				val = val.ToLower ().Trim ();
			}
			return (val == "true") || (val == "1");
		}

		static public bool isSauce() {
			Init ();
			return (env.ContainsKey("SAUCE") && isTrue(env["SAUCE"])) || isTrue( Environment.GetEnvironmentVariable ("SAUCE") );
		}

		static public bool isDev() {
			Init ();
			return (env.ContainsKey("DEV") && isTrue(env["DEV"])) || isTrue( Environment.GetEnvironmentVariable ("DEV") );
		}

		static public string getEnvVar(string name){
			if (env.ContainsKey(name) && (env [name] != null)) {
				return env [name];
			} else {
				return Environment.GetEnvironmentVariable (name);
			}
		}
	}
}
