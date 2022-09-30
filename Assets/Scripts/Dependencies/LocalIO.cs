using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;  // Uses Newtonsoft JSON NUGET PACKAGE

/// <summary>
/// Created By Caleb Vaccaro (Before Project)
/// Script to Read and Write Effeciently to Client
/// File Systems such as %AppData%, LocalRoaming(Unity Save Location)
/// Can Create a Directory For Saving If Needed
/// Can GetObject from File
/// 
/// If you want to Load In Data from Assets or Non-Reachable Local Files:
/// Load Location Can Be Called Like "LoadInData.Json" from Assets
/// Or you can add them to Resources and Resources.Load("/LoadInData.Json");
/// </summary>
namespace System
{
    public static class LocalIO
    {
        private static readonly string platformDirectoryPath = UnityEngine.Application.dataPath;
        private static string appDataSaveDirectoryPath = UnityEngine.Application.persistentDataPath + "/";
        private static string persistantSaveDirectoryPath = UnityEngine.Application.persistentDataPath + "/";
        private static string shallowSaveDirectoryPath = "/";
        private static readonly string saveDataPathEnd = "GameData/";

        public static string GetAppDirectory()
        {
            return Directory.Exists(platformDirectoryPath) ? platformDirectoryPath : "Directory Does Not Exist: " + platformDirectoryPath;
        }

        public static bool IfAppDirectioryExists()
        {
            //Debug.Log(persistantSaveDirectoryPath);
            return Directory.Exists(appDataSaveDirectoryPath) ? true : false;
        }

        public static void CreateAppDirectiory()
        {
            try
            {
                Directory.CreateDirectory(appDataSaveDirectoryPath + saveDataPathEnd);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public static bool IfAppSaveDirectioryExists()
        {
            return Directory.Exists(appDataSaveDirectoryPath + saveDataPathEnd) ? true : false;
        }

        public static void CreateAppSaveDirectiory()
        {
            try
            {
                Directory.CreateDirectory((appDataSaveDirectoryPath + saveDataPathEnd));
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static void SwitchToPersitantDirectory()
        {
            appDataSaveDirectoryPath = persistantSaveDirectoryPath;
            Debug.Log("Switched to Persitant");
        }

        public static void SwitchToShortDirectory()
        {
            appDataSaveDirectoryPath = shallowSaveDirectoryPath;
            Debug.Log("Switched to Short");
        }

        /// <summary>
        /// Create a file
        /// Allowed to add an object to be serialized to JSON
        /// any object works
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="dataObject"></param>
        public static void CreateFile(string filename, object dataObject = null)
        {
            string ApplicationPath = appDataSaveDirectoryPath + saveDataPathEnd + filename;

            if (!File.Exists(ApplicationPath))
            {
                using (StreamWriter sr = File.CreateText(ApplicationPath))
                {
                    // StreamWriter Doesn't Need to Do Anything
                }
            }

            if (dataObject != null)
            {
                File.Delete(ApplicationPath);

                using (FileStream fs = File.OpenWrite(ApplicationPath))
                {
                    string dataString = JsonConvert.SerializeObject(dataObject);
                    Byte[] title = new UTF8Encoding(true).GetBytes(dataString);
                    fs.Write(title, 0, title.Length);
                    fs.Close();
                }
            }
        }


        /// <summary>
        /// Checks if File is Valid
        /// Checks Each level of the directoy
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static bool IfFileExists(string filename)
        {
            string ApplicationPath = appDataSaveDirectoryPath + saveDataPathEnd + filename;

            if (!IfAppDirectioryExists()) return false;

            if (!IfAppSaveDirectioryExists()) return false;

            if (File.Exists(ApplicationPath)) return true;

            return false;
        }

        /// <summary>
        /// Get a specific File Object form a file
        /// Can load from Save Directory
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static T GetStrictSavedFileObject<T>(string filename)
        {
            string ApplicationPath = appDataSaveDirectoryPath + saveDataPathEnd + filename;
            using (var sr = new StreamReader(ApplicationPath))
            {
                string jsonString = sr.ReadToEnd();
                return JsonConvert.DeserializeObject<T>(jsonString);
            }
        }

        /// <summary>
        /// Load Data From Asset or Non-Reachable Local Files
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filename"></param>
        /// <param name="fromResources"></param>
        /// <returns></returns>
        public static T GetFileObject<T>(string filename, bool fromResources = false)
        {
            string ApplicationPath = filename;
            string readInString = string.Empty;

            if (fromResources)
            {
                // Folder(optional)/SubFolder(optional)/textFileName.Json
                readInString = UnityEngine.Resources.Load<TextAsset>(ApplicationPath).text;
            }
            else
            {
                using (var sr = new StreamReader(ApplicationPath))
                {
                    readInString = sr.ReadToEnd();
                }
            }
            
            return JsonConvert.DeserializeObject<T>(readInString);

        }

        /// <summary>
        /// Find Top Directory
        /// If app cannot find default
        /// App will switch to next directory to locate
        /// Until one is found and Sub-SaveData is Created
        /// </summary>
        public static void FindDirectory()
        {
            if (!IfAppDirectioryExists())
            {
                CreateAppDirectiory();
            }

            if (!IfAppSaveDirectioryExists())
            {
                CreateAppSaveDirectiory();
            }

            if (!IfAppSaveDirectioryExists())
            {
                SwitchToPersitantDirectory();

                CreateAppSaveDirectiory();
            }

            if (!IfAppSaveDirectioryExists())
            {
                SwitchToShortDirectory();

                CreateAppSaveDirectiory();
            }

        }

    }
}
