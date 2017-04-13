using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class GearBoxOperatingConditionUpdater : MonoBehaviour {

    public TextAsset operatingConditionFile;

    public Text acCurrent;
    public Text loadSetting;
    public Text gearboxTemperature;
    public Text operatingSpeed;
    public Text torqueSetting;

    public Text dashAcCurrent;
    public Text dashLoadSetting;
    public Text dashGearboxTemperature;
    public Text dashOperatingSpeed;
    public Text dashTorqueSetting;

    private int curSeconds;

    // Use this for initialization
    void Start ()
    {
        Load(operatingConditionFile);
	}
	
	// Update is called once per frame
	void Update ()
    {
        curSeconds = DateTime.Now.Second;
        acCurrent.text = Find_id(curSeconds.ToString()).accurrent + " A";
        loadSetting.text = Find_id(curSeconds.ToString()).load + " N";
        gearboxTemperature.text = Find_id(curSeconds.ToString()).gearboxtemperature + " °C";
        operatingSpeed.text = Find_id(curSeconds.ToString()).operatingspeed + " rpm";
        torqueSetting.text = Find_id(curSeconds.ToString()).torque + " Nm";

        dashAcCurrent.text = Find_id(curSeconds.ToString()).accurrent + " A";
        dashLoadSetting.text = Find_id(curSeconds.ToString()).load + " N";
        dashGearboxTemperature.text = Find_id(curSeconds.ToString()).gearboxtemperature + " °C";
        dashOperatingSpeed.text = Find_id(curSeconds.ToString()).operatingspeed + " rpm";
        dashTorqueSetting.text = Find_id(curSeconds.ToString()).torque + " Nm";
    }

    public class Row
    {
        public string id;
        public string accurrent;
        public string load;
        public string gearboxtemperature;
        public string operatingspeed;
        public string torque;

    }

    List<Row> rowList = new List<Row>();
    bool isLoaded = false;

    public bool IsLoaded()
    {
        return isLoaded;
    }

    public List<Row> GetRowList()
    {
        return rowList;
    }

    public void Load(TextAsset csv)
    {
        rowList.Clear();
        string[][] grid = CsvParser2.Parse(csv.text);
        for (int i = 1; i < grid.Length; i++)
        {
            Row row = new Row();
            row.id = grid[i][0];
            row.accurrent = grid[i][1];
            row.load = grid[i][2];
            row.gearboxtemperature = grid[i][3];
            row.operatingspeed = grid[i][4];
            row.torque = grid[i][5];

            rowList.Add(row);
        }
        isLoaded = true;
    }

    public int NumRows()
    {
        return rowList.Count;
    }

    public Row GetAt(int i)
    {
        if (rowList.Count <= i)
            return null;
        return rowList[i];
    }

    public Row Find_id(string find)
    {
        return rowList.Find(x => x.id == find);
    }
    public List<Row> FindAll_id(string find)
    {
        return rowList.FindAll(x => x.id == find);
    }
    public Row Find_accurrent(string find)
    {
        return rowList.Find(x => x.accurrent == find);
    }
    public List<Row> FindAll_accurrent(string find)
    {
        return rowList.FindAll(x => x.accurrent == find);
    }
    public Row Find_load(string find)
    {
        return rowList.Find(x => x.load == find);
    }
    public List<Row> FindAll_load(string find)
    {
        return rowList.FindAll(x => x.load == find);
    }
    public Row Find_gearboxtemperature(string find)
    {
        return rowList.Find(x => x.gearboxtemperature == find);
    }
    public List<Row> FindAll_gearboxtemperature(string find)
    {
        return rowList.FindAll(x => x.gearboxtemperature == find);
    }
    public Row Find_operatingspeed(string find)
    {
        return rowList.Find(x => x.operatingspeed == find);
    }
    public List<Row> FindAll_operatingspeed(string find)
    {
        return rowList.FindAll(x => x.operatingspeed == find);
    }
    public Row Find_torque(string find)
    {
        return rowList.Find(x => x.torque == find);
    }
    public List<Row> FindAll_torque(string find)
    {
        return rowList.FindAll(x => x.torque == find);
    }

}
