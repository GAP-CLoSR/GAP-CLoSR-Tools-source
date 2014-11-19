# ---------------------------------------------------------------------------
#ConvGraphabPatchesAndLabelToCirc_GC.py
# for use with GAP_CLoSR software.
#
# This script is part of the GAP_CLoSR tools and methods developed by 
# Dr Alex Lechner (alexmarklechner@yahoo.com.au) as a part of the 
# Australian Government's National Environmental Research Program 
# (NERP) Landscapes and Policy hub. This script was adapted by  
# Dr Michael Lacey (Michael.Lacey@utas.edu.au) for use with 
# GAP_CLoSR_Tools.exe. This script and GAP_CLoSR_GUI.exe are licensed 
# under the Creative Commons AttributionNonCommercial-ShareAlike 3.0
# Australia (CC BY-NC-SA 3.0 AU) license. To view a copy of this licence, 
# visit https://creativecommons.org/licenses/by-nc-sa/3.0/au/.
#    
# The script expects 7 input arguments listed below
#  <Output_Folder (string)>
#  <patches_tif (string)>
#  <Test_Small_Extent_shp (string)>
#  <Components_shp (string)>
#'"CircFocalNodesCompAsciiFile=" & GC_CC_FocalNodesComp_asc)
#'"CircFocalNodesCompRaster=" & GC_CC_FocalNodesComp_R)
#
# ---------------------------------------------------------------------------


# Import arcpy module
import arcpy, time, os, sys

# Check out any necessary licenses
arcpy.CheckOutExtension("spatial")
arcpy.env.overwriteOutput = True # Overwrite pre-existing files

# Script arguments
if len (sys.argv) >2: #ie arguments provided
    Output_Folder = str(sys.argv[1])
    patches_tif = str(sys.argv[2])
    Test_Small_Extent_shp = str(sys.argv[3])
    Components_shp = str(sys.argv[4])
    patchascii_asc = str(sys.argv[5])
    PatchTemp2 = Output_Folder +"/"+ str(sys.argv[6])#assumes no path included
    
    # Script arguments
    #Output_Folder = r"G:\testing"                   
    #patches_tif = r"G:\inputs\patches.tif"
    #Test_Small_Extent_shp = r"G:\inputs\Test_Small_Extent.shp"
    #Components_shp = r"G:\inputs\Components.shp"
    #patchascii_asc = r"\patchascii.asc"
    #PatchTemp2 = r"\patchcirc"
                        
    print "Running"+str(sys.argv[0])+" at "+time.strftime('%d/%m/%y %H:%M:%S')
    print "Output Folder: " + Output_Folder
    print "patches_tif: " + patches_tif
    print "Extent_shp: " + Test_Small_Extent_shp
    print "Components_shp: " + Components_shp
    print "CircFocalNodesCompAsciiFile: " + patchascii_asc
    print "CircFocalNodesCompRaster: " + PatchTemp2
                        
else:
    #
    print "This script is intended to be run with input arguments."

def main():
    # Local variables:
    # temp folder
    TempFolder=Output_Folder+"\\tmp_output\\"
    #create the temp folder if it does not exist
    try: 
        if not os.path.isdir(TempFolder):
            os.mkdir(TempFolder)
    except:
        print "Could not create temp folder." 
    PatchTemp =  TempFolder +"temp1"
    Components_Clip = TempFolder +"temp2.shp"
    Components_Clip_Ras = TempFolder +"temp3"
    #Components_Clip = Test_Small_Extent_shp

    print "\nCreating Circuitscape Inputs"
    print "Convert Graphab patches and label to Circuitscape input"
    print "Starting at " + time.strftime('%d/%m/%y %H:%M:%S')
    StartT=time.time()

    try:
        # Process: Extract by Mask
        tempEnvironment0 = arcpy.env.cellSize
        arcpy.env.cellSize = "MAXOF"
        arcpy.gp.ExtractByMask_sa(patches_tif, Test_Small_Extent_shp, PatchTemp)
        arcpy.env.cellSize = tempEnvironment0
        # Process: Clip
        arcpy.Clip_analysis(Components_shp, Test_Small_Extent_shp, Components_Clip, "")
        # Process: Polygon to Raster
        tempEnvironment0 = arcpy.env.snapRaster
        arcpy.env.snapRaster = PatchTemp
        tempEnvironment1 = arcpy.env.extent
        arcpy.env.extent = PatchTemp
        tempEnvironment2 = arcpy.env.cellSize
        arcpy.env.cellSize = PatchTemp
        arcpy.PolygonToRaster_conversion(Components_Clip, "Id", Components_Clip_Ras, "CELL_CENTER", "NONE", "31")
        arcpy.env.snapRaster = tempEnvironment0
        arcpy.env.extent = tempEnvironment1
        arcpy.env.cellSize = tempEnvironment2
        # Process: Raster Calculator
        #arcpy.gp.RasterCalculator_sa("Con(\"%PatchTemp%\">0,\"%Components_Clip_PolygonToRas.tif%\")", PatchTemp2)
        outputRaster = arcpy.sa.Con(arcpy.Raster(PatchTemp) > 0, arcpy.Raster(Components_Clip_Ras))
        outputRaster.save(PatchTemp2)
        # Process: Raster to ASCII
        arcpy.RasterToASCII_conversion(PatchTemp2, patchascii_asc)
    
    except:
        print "\nError in creating raster.\n"+ arcpy.GetMessages()

    print "Time elapsed:" +str(time.time()- StartT) + " seconds"
    print "Finished at:"  +  time.strftime('%d/%m/%y %H:%M:%S')
    ##end of main()##


if __name__=='__main__':
    main()