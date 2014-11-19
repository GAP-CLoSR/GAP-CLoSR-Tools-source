# ---------------------------------------------------------------------------
# 4_ModifyLuseLayer_AddInfra.py
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
# The script expects 5 input arguments listed below
#   <Basefolder (string)>
#   <Input__ChangeLayer (string)>
#   <Output__New_Veg_Layer (string)>
#   <Input__Hydrology_layer_value (string)>
#   <Input__luse (string)> 
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
    InputChangeLayer = Basefolder +"/"+ str(sys.argv[2]) #assumes no path included
    OutputNewVegLayer = Basefolder +"/"+ str(sys.argv[3]) #assumes no path included
    InputHydrologyLayerValue = float(sys.argv[4])
    Input_luse = Basefolder +"/"+ str(sys.argv[5]) #assumes no path included

    #BaseFolder = r"G:\MCAS-S\GAP_CLoSR_App\Data\GAP_CLoSR_Tutorial_1.3\data"
    #InputChangeLayer = "ChangeLayer"
    #OutputNewVegLayer = "Z:\\GAP_CLoSR_Tutorial\\Data\\output\\luse_Add"
    #InputHydrologyLayerValue = 30
    #Input_luse = "luse"

    print"Running"+str(sys.argv[0])+" at "+time.strftime('%d/%m/%y %H:%M:%S')
    print "Base Folder: " + Basefolder
    print "Input Change Layer: " + InputChangeLayer
    print "Output New Veg. Layer: " + OutputNewVegLayer
    print "Input Hydrology Layer Value: " + str(InputHydrologyLayerValue)
    print "Input_luse: " + Input_luse
else:
    #
    print "This script is intended to be run with input arguments."

def main():
    # Local variables:
    #none
    print "\nGAP_CLoSR Scenario Tools"
    print "Modify luse Layer - Add infrastructure"
    print "Starting at " + time.strftime('%d/%m/%y %H:%M:%S')
    StartT=time.time()

    try:
        #
        outputRaster = arcpy.sa.Con((arcpy.Raster(InputChangeLayer) == 1) & (arcpy.Raster(Input_luse) != InputHydrologyLayerValue), 10, Input_luse)
        outputRaster.save(OutputNewVegLayer)
    except:
        print "\nError in creating raster.\n"+ arcpy.GetMessages()

    print "Time elapsed:" +str(time.time()- StartT) + " seconds"
    print "Finished at:"  +  time.strftime('%d/%m/%y %H:%M:%S')
    ##end of main()##

if __name__=='__main__':
    main()

