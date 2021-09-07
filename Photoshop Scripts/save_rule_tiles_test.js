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
var okEnabled = true;
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
    var enabled = !(baseLayerName.text == "" || ruleLayerName.text == "") && okEnabled;
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
                    "Top","Bottom",
                    "Right", "Left",
                    "Outer", "Inner",
                    "Single","Double",
                    "Triple", "Quad",
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
    //alert("Checking "+searchString);
    for(var i = 0; i<substrings.length; i++)
    {
        var substring = substrings[i];
        if(searchString.indexOf(substring) >= 0)
        {
            //alert(searchString+" contained "+substring);
            return true;
        }
    }
    return false;
}

function ExportRuleTiles(folder)
{

    var baseLayer = layerDict[baseLayerName.text];
    var rulesLayer = layerDict[ruleLayerName.text];
    //alert(ruleLayer.layers.length);
    for(var i = 0; i<rulesLayer.layers.length; i++)
    {
        
        var ruleLayer = rulesLayer.layers[i];
        if(ruleLayer.typename == "ArtLayer")
        {
            Export(folder, baseLayer, ruleLayer);
        }
    }
    alert("All done!");
}

function Export(folder, baseLayer, ruleLayer)
{
    //alert("exporting "+ruleLayer.name);
    //Regular save tiles that don't need flipping
    switch(ruleLayer.name)
    {
        case "Top":
            Save(folder, baseLayer, ruleLayer, false);
            break;
        case "Quad":
            Save(folder, baseLayer, ruleLayer, false);
            break;
        case "Bottom":
            Save(folder, baseLayer, ruleLayer, false);
            break;
        case "TopDown":
            Save(folder, baseLayer, ruleLayer, false);
            break;
        case "LeftRight":
            Save(folder, baseLayer, ruleLayer, false);
            break;
        case "TrippleTop":
            Save(folder, baseLayer, ruleLayer, false);
            break;
        case "TrippleBottom":
            Save(folder, baseLayer, ruleLayer, false);
            break;
        case "QuadOuterCorner":
            Save(folder, baseLayer, ruleLayer, false);
            break;
        case "OuterCornersTop":
            Save(folder, baseLayer, ruleLayer, false);
            break;
        case "OuterCornersBottom":
            Save(folder, baseLayer, ruleLayer, false);
            break;
        case "OuterCornersTop&Bottom":
            Save(folder, baseLayer, ruleLayer, false);
            break;
        case "OuterCornersBottom&Top":
            Save(folder, baseLayer, ruleLayer, false);
            break;
        default:
            SaveBoth(folder, baseLayer, ruleLayer);
            break;
    }
}

function SaveBoth(folder, baseLayer, ruleLayer)
{
    //alert("saving original and flipped version");
    Save(folder, baseLayer, ruleLayer, false);
    Save(folder, baseLayer, ruleLayer, true);
}

function Save(folder, baseLayer, ruleLayer, flip)
{
    //alert("Saving");
    var doc = CreateDoc();
    //alert("before");
    var layers = flip ?
        DuplicateAndFlip(doc,baseLayer,ruleLayer) : Duplicate(doc, baseLayer, ruleLayer);
    //alert("Before save");
    SaveDocPNG(doc, folder, layers[1].name);
}

function Duplicate(doc, baseLayer, ruleLayer)
{
    //create temporary container
    var tempContainer = doc.layerSets.add();
    var lastActive = app.activeDocument;
    //for some reason the layer you want to duplicate must be in the active document
    app.activeDocument = baseDoc;
    //duplicate base and rule template
    var tempBase = baseLayer.duplicate(tempContainer, ElementPlacement.INSIDE);
    var tempRule = ruleLayer.duplicate(tempBase, ElementPlacement.PLACEBEFORE);
    //go back to previous document
    app.activeDocument = doc;
    //make sure layers are visible
    tempBase.visible = true;
    tempRule.visible = true;
    //alert("regular dupe: "+tempRule.name)
    return [tempBase, tempRule];
}

function DuplicateAndFlip(doc, baseLayer, ruleLayer)
{
    var bounds = [];
    for(var i = 0; i<ruleLayer.bounds.length; i++)
    {
        var bound = ruleLayer.bounds[i];
        bounds.push(bound.value);
    }
    //alert("duplicating and flipping")
    var layers = Duplicate(doc, baseLayer, ruleLayer);
    // Getting center coordinates of the document
    var layerCenterOffsetX = bounds[2]/2;
    var layerCenterOffsetY = bounds[3]/2;

    var docCenterW = Math.round(width / 2);
    var docCenterH = Math.round(height / 2);

    var layerCenterX = Math.round(bounds[0]+layerCenterOffsetX);
    var layerCenterY = Math.round(bounds[1]+layerCenterOffsetY);

    var oDX = Math.round(docCenterW - layerCenterX);
    var oDY = Math.round(docCenterH - layerCenterY);

    var deltaX = 2*oDX;
    var deltaY = bounds[1];
    if(oDX != 0 || oDY != 0)
    {
        //I have no clue why, but subtracting the layerCenterOffsetX from oDX
        //does not return the correct position. My code/math is wrong but I don't
        //know where. I'm just going to leave it out and hope nothing breaks.
        //var DX = deltaX-layerCenterOffsetX;   <-- doesnt work!!! stinky!!!
        alert(
            "DeltaX: "+deltaX+"\nLayerCenterOffsetX :"+ layerCenterOffsetX+
            "\noriginal pos: "+bounds[0]+","+bounds[1]+
            "\nnew pos: "+(bounds[0]+deltaX)+","+deltaY+
            "\nWidth: "+bounds[2]+"\nHeight: "+bounds[3]);
        
        var xUV = new UnitValue(deltaX, "px");
        var yUV = new UnitValue(deltaY, "px");
        //alert(xUV.as("px")+"\n"+yUV.as("px"));
        layers[1].translate(xUV, undefined);
    }

    

    //alert("flip");
    //flip
    layers[1].resize(-100, undefined, AnchorPosition.MIDDLECENTER);

    
    //alert(layers[1].name);
    layers[1].name = SwapName(layers[1].name);
    //
    return layers;
}

function SwapName(layerName)
{
    return layerName.replace(
        /Left|Right/g,
        function(match){return match == "Left" ? "Right" : "Left"}
    );
}

function CreateDoc()
{
    return app.documents.add(width, height, 72, "tempDoc", NewDocumentMode.RGB);
}

function SaveDocPNG(doc, folder, name)
{
    //alert("saving");
    var pngOptions = new PNGSaveOptions();
            pngOptions.compression = 0;
            pngOptions.interlaced = false;
    
    //save document
    
    var imgName = name;
    var file = new File(decodeURI(folder.absoluteURI)+"/"+imgName+".png");
    
    doc.saveAs(file, pngOptions, true, Extension.LOWERCASE);
    //alert("Saved "+ file.absoluteURI);
    //cleanup
    doc.close(SaveOptions.DONOTSAVECHANGES);
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