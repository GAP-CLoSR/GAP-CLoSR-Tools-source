# ---------------------------------------------------------------------------
# 3_CreateGapCrossingLayer.py
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
# The script expects 4 input arguments listed below
#   <Basefolder (string)>
#   <Input__Veg_layer (string)>
#   <Output__Gap_Crossing_Layer (string)>
#   <Input__Cell_Multiplier (string)>
# 
# ---------------------------------------------------------------------------

# Import arcpy module
import arcpy, time, os, sys

# Check out any necessary licenses
arcpy.CheckOutExtension("spatial")
arcpy.env.overwriteOutput = True # Overwrite pre-existing files

# Script arguments
if len (sys.argv) >2: #ie arguments provided
    Basefolder = str(sys.argv[1])
    InputVegLayer = Basefolder +"/"+ str(sys.argv[2]) #assumes no path included
    OutputGapCrossingLayer = Basefolder +"/"+ str(sys.argv[3]) #assumes no path included
    InputCellMultiplier = str(sys.argv[4])
      
    #BaseFolder = r"G:\MCAS-S\GAP_CLoSR_App\Data\GAP_CLoSR_Tutorial_1.3\data"
    #InputVegLayer = BaseFolder + r"\OutputTest\lh_ccrem"
    #OutputGapCrossingLayer = BaseFolder + r"\OutputTest\gap_crossAdd"
    #InputCellMultiplier = "40"
      
    print "Running"+str(sys.argv[0])+" at "+time.strftime('%d/%m/%y %H:%M:%S')
    print "Base Folder: " + Basefolder
    print "InputVegLayer: " + InputVegLayer
    print "OutputGapCrossingLayer: " + OutputGapCrossingLayer
    print "InputCellMultiplier: " + InputCellMultiplier
else:
    #
    print "This script is intended to be run with input arguments."

def main():
    # Local variables:
    TempFolder = Basefolder + "\\tmp_output\\"
    tempRaster=TempFolder+"temp1"
    tempRaster2=TempFolder+"temp2"
    #create the temp folder if it does not exist
    try: 
        if not os.path.isdir(TempFolder):
            os.mkdir(TempFolder)
    except:
        print "Could not create temp folder."   
    print "\nGAP_CLoSR Scenario Tools"
    print "Create Gap-Crossing-Add Layer"
    print "Starting at " + time.strftime('%d/%m/%y %H:%M:%S')
    StartT=time.time()

    try:
        # Process: Aggregate
        arcpy.gp.Aggregate_sa(InputVegLayer, tempRaster, InputCellMultiplier, "SUM", "EXPAND", "DATA")
        # Process: Reclassify
        arcpy.gp.Reclassify_sa(tempRaster, "Value", "1 1000000 1", tempRaster2, "DATA")
        # Process: Resample
        tempEnvironment0 = arcpy.env.snapRaster
        arcpy.env.snapRaster = InputVegLayer
        tempEnvironment1 = arcpy.env.extent
        arcpy.env.extent = InputVegLayer
        tempEnvironment2 = arcpy.env.cellSize
        arcpy.env.cellSize = InputVegLayer
        arcpy.Resample_management(tempRaster2, OutputGapCrossingLayer, "2.5 2.5", "MAJORITY")
        arcpy.env.snapRaster = tempEnvironment0
        arcpy.env.extent = tempEnvironment1
        arcpy.env.cellSize = tempEnvironment2
    except:
        print "\nError in creating raster.\n"+ arcpy.GetMessages()

    print "Time elapsed:" +str(time.time()- StartT) + " seconds"
    print "Finished at:"  +  time.strftime('%d/%m/%y %H:%M:%S')
    ##end of main()##

if __name__=='__main__':
    main()
