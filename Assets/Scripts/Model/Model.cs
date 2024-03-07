using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Text;

public class Model : MonoBehaviour
{
    public saveData mysaveData;
    public SystemConfig mySystemConfig;
    public Dictionary<string, Dictionary<string, string>> fishConfigDic;
    public Dictionary<string, Dictionary<string, string>> shopConfigDic;
    public Dictionary<string, Dictionary<string, string>> languageConfigDic;
    public View view;

    //加载配置表
    private void Awake()
    {
        view = GameObject.FindWithTag("View").GetComponent<View>();
        LoadMyData();
        LoadConfiguration();
    }

    //加载配置1鱼儿的表格，2商店，3系统，语言版本
    public void LoadConfiguration()
    {
        ExcelData.LoadExcelFormCSV("fishConfig", out fishConfigDic);
        ExcelData.LoadExcelFormCSV("shopConfig", out shopConfigDic);
        ExcelData.LoadExcelFormCSV("languageConfig", out languageConfigDic);
        // print(shopConfigDic["ChineseText"]["201"]);
        //加载系统设置
        TextAsset txt = Resources.Load<TextAsset>("Text/SystemConfig");
        mySystemConfig = JsonUtility.FromJson<SystemConfig>(txt.text);
        //加载系统设置结束
    }

    //加载自身保存的数据 
    public void LoadMyData()
    {
        string path = Application.persistentDataPath + "/saveData.json";
        if (File.Exists(path))
        {
            using (StreamReader reader = new StreamReader(path))
            {
                mysaveData = JsonUtility.FromJson<saveData>(reader.ReadToEnd());
            }
        }
        else
        {
            TextAsset txt = Resources.Load<TextAsset>("Text/saveData");
            mysaveData = JsonUtility.FromJson<saveData>(txt.text);
            using (var s = File.Create(path))
            {
            }
            SaveMyData();
        }
    }

    //储存数据呢
    public void SaveMyData()
    {
        string json = JsonUtility.ToJson(mysaveData);
        string savePath = Application.persistentDataPath + "/saveData.json";
//        print(savePath);
        File.WriteAllText(savePath, json, Encoding.UTF8);
    }

    public string returnLanguageMessage(int id)
    {
        string message = "";
        switch (mysaveData.LanguageKey)
        {
            case 0:
                message = languageConfigDic["ChineseText"][id.ToString()];
                break;
            case 1:
                message = languageConfigDic["EnglishText"][id.ToString()];
                break;
            case 2:
                message = languageConfigDic["TradChinese"][id.ToString()];
                break;
        }
        return message;
    }

    //returnLanguageMessage的非语言配置表上的语言信息
    public string returnLanguageMessageOtherConfig(Dictionary<string, Dictionary<string, string>> messageDic, int id)
    {
        string message = "";
        switch (mysaveData.LanguageKey)
        {
            case 0:
                message = messageDic["ChineseText"][id.ToString()];
                break;
            case 1:
                message = messageDic["EnglishText"][id.ToString()];
                break;
            case 2:
                message = messageDic["TradChinese"][id.ToString()];
                break;
        }
        return message;
    }
}
