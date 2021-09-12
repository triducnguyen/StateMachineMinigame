using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolList : MonoBehaviour
{
    public Sprite sprite;
    public Sprite sprite2;
    LinkedList<Tool> toolList = new LinkedList<Tool>();
    public List<Tool> tools
    {
        get
        {
            return new List<Tool>(toolList);
        }
    }
    //the tools shown are the first three

    public List<Image> images = new List<Image>();

    private void Awake()
    {
        AddTool(ToolDictionary.Instance.tools["WaterCan"]) ;
        AddTool(ToolDictionary.Instance.tools["HandCultivator"]) ;
        AddTool(ToolDictionary.Instance.tools["HandClippers"]) ;
        AddTool(ToolDictionary.Instance.tools["WheatSeed"]) ;
        AddTool(ToolDictionary.Instance.tools["Fertilizer"]) ;
    }

    public void Up()
    {
        //rotate items up
        Tool first = toolList.First.Value;
        toolList.RemoveFirst();
        toolList.AddLast(first);
        UpdateImages();
    }

    public void Down()
    {
        //rotate items down
        Tool last = toolList.Last.Value;
        toolList.RemoveLast();
        toolList.AddFirst(last);
        UpdateImages();
    }

    public void Select1()
    {
        if (tools.Count > 0)
        {
            GameManager.Instance.tool = tools[0];
        }
    }

    public void Select2()
    {
        if(tools.Count > 1){
            GameManager.Instance.tool = tools[1];
        }
    }

    public void Select3()
    {
        if(tools.Count > 2)
        {
            GameManager.Instance.tool = tools[2];
        }
    }

    void UpdateImages()
    {
        var count = 0;
        foreach (Tool tool in tools)
        {
            if(count > 2)
            {
                return;
            }
            images[count].sprite = tool.sprite;
            var color = images[count].color;
            images[count].color = new Color(color.r, color.g, color.b, 1f);
            count++;
        }
        if (count < 2)
        {
            for (int i = tools.Count; i < 3; i++)
            {
                var color = images[i].color;
                images[i].color = new Color(color.r, color.g, color.b, 0f);
            }
        }
    }

    void AddTool(Tool tool)
    {
        toolList.AddLast(tool);
        UpdateImages();
    }

    void RemoveTool(Tool tool)
    {
        toolList.Remove(tool);
        UpdateImages();
    }
}
