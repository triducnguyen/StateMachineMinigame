var baseLayerDD;
var ruleLayerDD;

var baseLayer;
var ruleLayer;

var okBtn;
var cancelBtn;


//first fuction called
function main()
{
    //window layout
    var window = new Window("dialog", "Test");
    //var layers = app.activeDocument.layers;
    //var txt = window.add("edittext");
    
    //controls
    baseLayerDD = window.add("dropdownlist");

    ruleLayerDD = window.add("dropdownlist");

    okBtn = windowControls.add("button", undefined ,"OK");

    cancelBtn = windowControls.add("button", undefined, "Cancel");
    //Base layer dropdown
    
    baseLayerDD.title = "Base Layer";
    var artLayers = FilterLayers(GetLayers());
    //txt.text = text;
    LoadDropDownItems(baseLayerDD, artLayers);
    baseLayerDD.onChange = OnBaseDropDownChange();

    //Rule LayerSet
    
    ruleLayerDD.title = "Rule Group";
    var setLayers = FilterLayers(GetLayers(undefined, false), false);
    LoadDropDownItems(ruleLayerDD, setLayers);
    ruleLayersDD.onChange = OnRuleDropDownChange();
    var windowControls = window.add("panel")
    
    
    okBtn.enabled = false;
    okBtn.onClick = okClicked;

    
    window.show();
}


function OnBaseDropDownChange()
{
    baseLayer = baseLayerDD.selection;
    UpdateOK();
}

function OnRuleDropDownChange()
{
    ruleLayer = ruleLayerDD.selection;
    UpdateOK();
}

function UpdateOK()
{
    var enabled = typeof baseLayer !== 'undefined' && typeof ruleLayer !== 'undefined';
    okBtn.enabled = enabled;
}

function LoadDropDownItems(dropdown, items)
{
    //load names
    for(var i = 0; i<items.length; i++){
        var name = items[i].name;
        dropdown.add("item", name);
    }
}

function GetLayers(layers, art){
    var artLayers = [];
    var setLayers = [];
    
    layers = typeof layers == 'undefined' ?
        app.activeDocument.layers : layers;

    art = typeof art == 'undefined' ?
        true : art;

    //check each layer type
    for(var i = 0; i<layers.length; i++){
        
        var layer = layers[i];
        switch(layer.typename)
        {
            case "LayerSet":
                setLayers.push(layer);
                break;
            case "ArtLayer":
                artLayers.push(layer);
                break;
        }
    }

    //no more layers to explore
    if(setLayers.length == 0){
        //return art ? artLayers : setLayers;
        
        return art ?
            artLayers : setLayers;
    }
    else//more layers to explore
    {
        for(i = 0; i<setLayers.length; i++)
        {
            var layer = setLayers[i];
            if(art)
            {
                //looking for art layers only
                artLayers = artLayers.concat(GetLayers(layer.layers, art))
            }
            else
            {
                setLayers = setLayers.concat(GetLayers(layer.layers, art))
            }
        }
        return art ?
            artLayers : setLayers;
    }
}

function FilterLayers(layersIn, art)
{
    var layersOut = [];
    
    if(typeof art == "undefined")
    {
        art = true;
    }

    for(var i = 0; i<layersIn.length; i++)
    {
        
        var layer = layersIn[i];
        if(art && ArtLayerNameCheck(layer.name))
        {
            
            continue;
        }
        else if(!art && (!SetLayerNameCheck(layer.name) || layer.artLayers.length == 0)){
            continue;
        }
        layersOut.push(layer)
    }

    return layersOut;
}

//figure out naming convention

function ArtLayerNameCheck(name)
{
    //check if layer name matches Rule Tile naming
    return  Contains(name, 
                [ 
                    "TopLeft", "Top", "TopRight",
                    "Left", "Right", "BottomLeft",
                    "Bottom","BottomRight"
                ]);
}

function SetLayerNameCheck(name)
{
    //check if layer name matches Rule Tile naming
    return Contains(name, 
                [
                    "Rule"
                ]);
}

function Contains(searchString, substrings)
{
    for(var i = 0; i<substrings.length; i++)
    {
        var substring = substrings[i];
        if(searchString.indexOf(substring) >= 0)
        {
            //text += searchString+"-"+substring+",";
            return true;
        }
    }
    return false;
    //text += searchString+"-NoMatch";
}

function ExportRuleTiles()
{
    //
}

function okClicked()
{
    ExportRuleTiles();
}

function cancelClicked()
{
    window.close();
}

main();