﻿// This code automatically generated by TableCodeGen
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CsvTable : MonoBehaviour
{
    public class Row
    {
        public string id;
        public string ferrouscount;
        public string nonferrouscount;

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
            row.nonferrouscount = grid[i][2];

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
    public Row Find_nonferrouscount(string find)
    {
        return rowList.Find(x => x.nonferrouscount == find);
    }
    public List<Row> FindAll_nonferrouscount(string find)
    {
        return rowList.FindAll(x => x.nonferrouscount == find);
    }

}