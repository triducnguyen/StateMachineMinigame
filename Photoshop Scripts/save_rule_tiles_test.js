var baseDoc;
var tmpDoc;


var baseLayerDD;
var ruleLayerDD;

var baseLayerName;
var ruleLayerName;

var allLayers;
var artLayers;
var setLayers;
var layerSets;

var okBtn;
var cancelBtn;

var layerDict = {};

var visible = [];

var width;
var height;

//first fuction called
function main()
{
    baseDoc = app.activeDocument;
    //window layout
    var window = new Window("dialog", "Save Rule Tiles");
    //var layers = app.activeDocument.layers;
    //var txt = window.add("edittext");
    
    //controls
    baseLayerDD = window.add("dropdownlist");

    ruleLayerDD = window.add("dropdownlist");
    var windowControls = window.add("panel")
        okBtn = windowControls.add("button", undefined ,"Save");
        cancelBtn = windowControls.add("button", undefined, "Cancel");
    //Base layer dropdown
    
    allLayers = GetLayers();
    
    baseLayerDD.title = "Base Layer";
    artLayers = FilterLayers(GetLayers(undefined, true), true);
    //txt.text = text;
    LoadDropDownItems(baseLayerDD, artLayers);
    baseLayerDD.onChange = OnBaseDropDownChange;

    //Rule LayerSet
    
    ruleLayerDD.title = "Rule Group";
    setLayers = FilterLayers(GetLayers(undefined, false), false);
    PopulateLayerDict();
    
    LoadDropDownItems(ruleLayerDD, setLayers);
    ruleLayerDD.onChange = OnRuleDropDownChange;
    
    
    okBtn.enabled = false;
    okBtn.onClick = SaveClicked;

    
    window.show();
}

function PopulateLayerDict()
{
    //alert(layerDict);
    for(var i = 0; i<setLayers.length; i++)
    {
        var layer = setLayers[i];
        layerDict[layer.name] = layer;
    }
    for(var i = 0; i<artLayers.length; i++)
    {
        var layer = artLayers[i];
        layerDict[layer.name] = layer;
    }
}

function OnBaseDropDownChange()
{
    baseLayerName = baseLayerDD.selection;
    width = app.activeDocument.width;
    height = app.activeDocument.height;
    UpdateOK();
}

function OnRuleDropDownChange()
{
    ruleLayerName = ruleLayerDD.selection;
    UpdateOK();
}

function UpdateOK()
{
    var enabled = !(baseLayerName.text == "" || ruleLayerName.text == "");
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
    var allLayers = [];

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
        return art ? artLayers : setLayers;
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
        if(art)
        {
            return artLayers;
        }
        else
        {
            return setLayers;
        }
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
            return true;
        }
    }
    return false;
    //text += searchString+"-NoMatch";
}

function ExportRuleTiles(folder)
{
    //alert("exporting");
    
    //alert("New doc");
    var baseLayer = layerDict[baseLayerName.text];
    var ruleLayer = layerDict[ruleLayerName.text];
    
    //var doc = app.activeDocument;

    var pngOptions = new PNGSaveOptions();
            pngOptions.compression = 0;
            pngOptions.interlaced = false;

    //alert(ruleLayer.layers.length);
    for(var i = 0; i<ruleLayer.layers.length; i++)
    {
        tmpDoc = app.documents.add(width, height, 72, "tempDoc", NewDocumentMode.RGB);
        var ilayer = ruleLayer.layers[i];
        if(ilayer.typename == "ArtLayer")
        {
            alert(ilayer.name);
            //create temporary container
            var tempContainer = tmpDoc.layerSets.add();
            //for some reason the layer you want to duplicate must be in the active document
            app.activeDocument = baseDoc;
            //duplicate base and rule template
            var tempBase = baseLayer.duplicate(tempContainer, ElementPlacement.INSIDE);
            var tempRule = ilayer.duplicate(tempBase,ElementPlacement.PLACEBEFORE);
            //go back to tmp doc
            app.activeDocument = tmpDoc;
            //make sure layers are visible
            tempBase.visible = true;
            tempRule.visible = true;
            //save document
            
            var imgName = ilayer.name;
            var file = new File(decodeURI(folder.absoluteURI)+"/"+imgName+".png");
            
            tmpDoc.saveAs(file, pngOptions, true, Extension.LOWERCASE);
            //alert("Saved "+ file.absoluteURI);
            //cleanup
            tmpDoc.close(SaveOptions.DONOTSAVECHANGES);
        }
    }
    alert("All done!");
}

function SaveClicked()
{
    //ask for folder path
    var folder = Folder.selectDialog("Select Rule Folder For: "+ruleLayerName.text);
    ExportRuleTiles(folder);
}

function CancelClicked()
{
    window.close();
}

main();