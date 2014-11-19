# ---------------------------------------------------------------------------
# 2a_ModifyVegetationLayer_Removal.py
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
#   <InputChangeLayer (string)>
#   <InputVegetationLayer (string)>
#   <OutputNewVegLayer (string)> 
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
    InputVegetationLayer = Basefolder +"/"+ str(sys.argv[3]) #assumes no path included
    OutputNewVegLayer = Basefolder +"/"+ str(sys.argv[4]) #assumes no path included

    #BaseFolder = r"G:\MCAS-S\GAP_CLoSR_App\Data\GAP_CLoSR_Tutorial_1.3\data"
    #InputChangeLayer = Basefolder + r"\OutputTest\ChangeLayer"
    #InputVegetationLayer = Basefolder + r"\lh_cc"
    #OutputNewVegLayer = Basefolder + r"\OutputTest\lh_ccREM"

    print "Running"+str(sys.argv[0])+" at "+time.strftime('%d/%m/%y %H:%M:%S')
    print "Base Folder: " + Basefolder
    print "Input Change Layer: " + InputChangeLayer
    print "Input Vegetation Layer: " + InputVegetationLayer
    print "Output New Veg. Layer: " + OutputNewVegLayer
else:
    #
    print "This script is intended to be run with input arguments."

def main():
    # Local variables:
    #none
    
    print "\nGAP_CLoSR Scenario Tools"
    print "Modify Vegetation Layer - Removal"
    print "Starting at " + time.strftime('%d/%m/%y %H:%M:%S')
    StartT=time.time()

    try:
        # Process: Con
        #arcpy.gp.Con_sa(InputChangeLayer, "0", OutputNewVegLayer, InputVegetationLayer, "\"VALUE\"=1")
        outputRaster = arcpy.sa.Con(arcpy.Raster(InputChangeLayer) == 1, 0, InputVegetationLayer)
        outputRaster.save(OutputNewVegLayer)
    except:
        print "\nError in creating raster.\n"+ arcpy.GetMessages()

    print "Time elapsed:" +str(time.time()- StartT) + " seconds"
    print "Finished at:"  +  time.strftime('%d/%m/%y %H:%M:%S')
    ##end of main()##


if __name__=='__main__':
    main()
