using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using System.IO;
using System;

public class DatabaseConnection : MonoBehaviour
{

    public static SqliteConnection connection { get; set; }

    static DatabaseConnection()
    {
        Connect();
    }

    private static void Connect()
    {

        try
        {
            string originPath = Path.Combine(Application.streamingAssetsPath, "database");
            string realPath = Path.Combine(Application.persistentDataPath, "database");

            if (Application.platform == RuntimePlatform.Android)
            {
                if (!File.Exists(realPath))
                {
                    WWW reader = new WWW(originPath);
                    while (!reader.isDone) { }

                    File.WriteAllBytes(realPath, reader.bytes);
                }
            }
            else
            {
                realPath = originPath;
            }

            string connectionString = "URI=file:" + realPath;

            connection = new SqliteConnection(connectionString);

            connection.Open();

            if (connection.State == ConnectionState.Open)
            {
                Debug.Log("Database connection SUCCESSFULL.");
            }
            else
            {
                Debug.Log("Database connection FAILED.");
            }
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
        }

    }

}
