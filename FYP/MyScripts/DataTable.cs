using UnityEngine;
using System.Collections.Generic;

public class DataTable {

    public class Row
    {
        public string id;
        public string ferrouscount;
        public string ferroussize;
        public string nonferrouscount;
        public string nonferroussize;
        public string peakvalue;
        public string rms;
        public string cf;
        public string kurtosis;
        public string fm4;
        public string na4;

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
            row.ferrouscount = grid[i][1];
            row.ferroussize = grid[i][2];
            row.nonferrouscount = grid[i][3];
            row.nonferroussize = grid[i][4];
            row.peakvalue = grid[i][5];
            row.rms = grid[i][6];
            row.cf = grid[i][7];
            row.kurtosis = grid[i][8];
            row.fm4 = grid[i][9];
            row.na4 = grid[i][10];

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
    public Row Find_ferrouscount(string find)
    {
        return rowList.Find(x => x.ferrouscount == find);
    }
    public List<Row> FindAll_ferrouscount(string find)
    {
        return rowList.FindAll(x => x.ferrouscount == find);
    }
    public Row Find_ferroussize(string find)
    {
        return rowList.Find(x => x.ferroussize == find);
    }
    public List<Row> FindAll_ferroussize(string find)
    {
        return rowList.FindAll(x => x.ferroussize == find);
    }
    public Row Find_nonferrouscount(string find)
    {
        return rowList.Find(x => x.nonferrouscount == find);
    }
    public List<Row> FindAll_nonferrouscount(string find)
    {
        return rowList.FindAll(x => x.nonferrouscount == find);
    }
    public Row Find_nonferroussize(string find)
    {
        return rowList.Find(x => x.nonferroussize == find);
    }
    public List<Row> FindAll_nonferroussize(string find)
    {
        return rowList.FindAll(x => x.nonferroussize == find);
    }
    public Row Find_peakvalue(string find)
    {
        return rowList.Find(x => x.peakvalue == find);
    }
    public List<Row> FindAll_peakvalue(string find)
    {
        return rowList.FindAll(x => x.peakvalue == find);
    }
    public Row Find_rms(string find)
    {
        return rowList.Find(x => x.rms == find);
    }
    public List<Row> FindAll_rms(string find)
    {
        return rowList.FindAll(x => x.rms == find);
    }
    public Row Find_cf(string find)
    {
        return rowList.Find(x => x.cf == find);
    }
    public List<Row> FindAll_cf(string find)
    {
        return rowList.FindAll(x => x.cf == find);
    }
    public Row Find_kurtosis(string find)
    {
        return rowList.Find(x => x.kurtosis == find);
    }
    public List<Row> FindAll_kurtosis(string find)
    {
        return rowList.FindAll(x => x.kurtosis == find);
    }
    public Row Find_fm4(string find)
    {
        return rowList.Find(x => x.fm4 == find);
    }
    public List<Row> FindAll_fm4(string find)
    {
        return rowList.FindAll(x => x.fm4 == find);
    }
    public Row Find_na4(string find)
    {
        return rowList.Find(x => x.na4 == find);
    }
    public List<Row> FindAll_na4(string find)
    {
        return rowList.FindAll(x => x.na4 == find);
    }
}
