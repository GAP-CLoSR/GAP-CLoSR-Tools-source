# ---------------------------------------------------------------------------
# CreateGapCrossingLayer.py
# Based on ArcGIS 10 ModelBuilder model by Alex Lechner (alexmarklechner@yahoo.com.au)
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
#   <InputVegLayer (string)>
#   <OutputGapCros (string)>
#   <MaximumDistance (float)> 
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
    InputVegLayer =  Basefolder +"/"+ str(sys.argv[2]) #assumes no path included
    OutputGapCros = Basefolder +"/"+ str(sys.argv[3]) #assumes no path included
    MaximumDistance =  float(sys.argv[4])
    #expected format
    #BaseFolder="G:/MCAS-S/GAP_CLoSR_App/Data/GAP_CLoSR_Tutorial_1.3/data"
    #InputVegLayer = BaseFolder+"/" + "lh_cc" 
    #OutputGapCros = BaseFolder+"/OutputTest/gap_cross"
    #MaximumDistance = "53"

    print"Running "+str(sys.argv[0])+" at "+time.strftime('%d/%m/%y %H:%M:%S')
    print"Base folder: "+Basefolder
    print"Input vegetation raster: "+InputVegLayer
    print"Output gap-cross raster: "+OutputGapCros
    print"MaximumDistance: "+ str(MaximumDistance)
else:
    #
    print "This script is intended to be run with input arguments."

def main():
    #Local variables:
    # temp folder
    TempFolder=Basefolder+"\\tmp_output\\"
    # temp rasters
    tempRaster=TempFolder+"temp1"
    tempRaster2=TempFolder+"temp2"
    #create the temp folder if it does not exist
    try: 
        if not os.path.isdir(TempFolder):
            os.mkdir(TempFolder)
    except:
        print "Could not create temp folder."   
    print "\nGAP_CLoSR Default Tools"
    print "Creating gap crossing raster"
    print "Starting at " + time.strftime('%d/%m/%y %H:%M:%S')
    StartT=time.time()

    try:
        # Process: Reclassify
        arcpy.gp.Reclassify_sa(InputVegLayer, "VALUE", "0 NODATA;1 1", tempRaster, "DATA")
        # Process: Euclidean Distance
        tempEnvironment0 = arcpy.env.snapRaster
        arcpy.env.snapRaster = InputVegLayer
        tempEnvironment1 = arcpy.env.extent
        arcpy.env.extent = InputVegLayer
        arcpy.gp.EucDistance_sa(tempRaster, tempRaster2, MaximumDistance, InputVegLayer,)
        arcpy.env.snapRaster = tempEnvironment0
        arcpy.env.extent = tempEnvironment1
        # Process: Reclassify (2)
        arcpy.gp.Reclassify_sa(tempRaster2, "Value", "0 1000 1;NODATA 0", OutputGapCros, "DATA")
    except:
        print "\nError in creating gap crossing raster.\n"+ arcpy.GetMessages()


    print "Time elapsed:" +str(time.time()- StartT) + " seconds"
    print "Finished at:"  +  time.strftime('%d/%m/%y %H:%M:%S')
    ##end of main()##


if __name__=='__main__':
    main()

