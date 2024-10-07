using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using System;

public class SaveManager : MonoBehaviour
{
    public Transform Player;
    public GameObject Settings;
    private string FileDirectory = Application.dataPath;
    private string FileName = "\\SaveFile.txt";

    FileStream fs = null;
    StreamWriter sw = null;
    StreamReader sr = null; 
    SettingsMenu SetRef; 

    private void Start()
    {
        if (FindAnyObjectByType<SettingsMenu>() != null)
        {
            SetRef = FindAnyObjectByType<SettingsMenu>();
        }
        Debug.Log(Application.dataPath);

        LoadQualityFile(FileName); 
    }
    public void CreateFile(string file)
    {
      
        print("received message write flat");
        if (file != "")
            FileName = file;

        if (!Directory.Exists(FileDirectory))
        {
            Directory.CreateDirectory(FileDirectory);
            Debug.Log("A folder called " + FileDirectory + " has been created.");
        }
        //string Content = "Login Time: " + System.DateTime.Now + "\n";
        fs = new FileStream(FileDirectory + "\\" + FileName, FileMode.OpenOrCreate);
        sw = new StreamWriter(fs);

        //Player Position
        if (Player != null)
        {
            
            sw.WriteLine(Player.transform.position.x.ToString() + "," + Player.transform.position.y.ToString() + "," + Player.transform.position.z.ToString()); 
        }
        else
        {
            sw.WriteLine("14.934f,6.63800001f,-8.55500031f");
        }

        //Grabbing Volume

        SetRef.AudioMixer.GetFloat("VolumeChange", out float volume);
        sw.WriteLine(volume);

        //Getting quality settings
        sw.WriteLine(QualitySettings.GetQualityLevel());
            Debug.Log("Method: CreateFile " + QualitySettings.GetQualityLevel());
        sw.WriteLine(Screen.fullScreen);
            Debug.Log("Method: CreateFile " + Screen.fullScreen);
        sw.WriteLine(SetRef.resolution.width.ToString() + "," + SetRef.resolution.height.ToString());
            Debug.Log("Method: CreateFile " + SetRef.resolution.width.ToString() + "," + SetRef.resolution.height.ToString());
        sw.WriteLine(QualitySettings.shadows);
            Debug.Log("Method: CreateFile " + QualitySettings.shadows);
        //TimeOfWriting/saving
        //File.AppendAllText(FileDirectory, Content);

        sw.Flush();
        sw.Close();
    }
    
    public void LoadQualityFile(string FileName)
    {
        string Line = "";
        fs = new FileStream(FileDirectory + "\\" + FileName, FileMode.Open);
        sr = new StreamReader(fs);
        Debug.Log("The LoadQualityFile is loaded");
        for(int Lines = 1; Lines < 10; Lines++)
        {
            //Player Pos
            if(Lines == 1)
            {
                Line = sr.ReadLine();
                Debug.Log(Line + ", Line #" + Lines);
            }
            //Audio 
            else if(Lines == 2)
            {
                Line = sr.ReadLine();
                SetRef.SetVolume(float.Parse(Line));
                Debug.Log("Method: LoadFile " + Line + ", Line #" + Lines);
            }
            //Graphic setting
            else if(Lines == 3)
            {
                Line = sr.ReadLine();
                QualitySettings.SetQualityLevel(int.Parse(Line));
                Debug.Log("Method: LoadFile " + Line + ", Line #" + Lines);
            }
            else if (Lines == 4)
            {
                Line = sr.ReadLine();
                SetRef.SetFullScreen(false); 
                Debug.Log("Method: LoadFile " + Line + ", Line #" + Lines);
            }
            else if(Lines == 5)
            {
                Line = sr.ReadLine();
                Debug.Log("Method: LoadFile " + Line + ", Line #" + Lines);
            } 
        }
        sr.Close(); 
        Debug.Log("LoadMethod has ended it's stream");
    }
    
}

