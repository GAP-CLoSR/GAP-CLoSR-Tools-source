# ---------------------------------------------------------------------------
# 1_CreateRasterChangeLayer.py
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
#   <InputChangeLayer (string)>
#   <InputRasterTemplate (string)>
#   <RasterValueField (string)>
#   <OuputRasterChangeLayer (string)> 
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
    InputChangeLayer = str(sys.argv[2]) #path included
    InputRasterTemplate = Basefolder +"/"+ str(sys.argv[3]) #assumes no path included
    RasterValueField =str(sys.argv[4])
    OuputRasterChangeLayer = Basefolder +"/"+ str(sys.argv[5]) #assumes no path included

    #Basefolder = r"G:\MCAS-S\GAP_CLoSR_App\Data\GAP_CLoSR_Tutorial_1.3\data"
    #InputChangeLayer = Basefolder +"\\ChangeLayer.shp"
    #InputRasterTemplate = Basefolder +"\\lh_cc"
    #RasterValueField = "Clear"
    #OuputRasterChangeLayer = Basefolder + r"\OutputTest\ChangeLayer"

    print "Running"+str(sys.argv[0])+" at "+time.strftime('%d/%m/%y %H:%M:%S')
    print "Base folder: " + Basefolder
    print "Input Change Layer: "+ InputChangeLayer
    print "Input raster template: " + InputRasterTemplate
    print "RasterValueField: " + RasterValueField
    print "OuputRasterChangeLayer: " + OuputRasterChangeLayer
else:
    #
    print "This script is intended to be run with input arguments."

def main():
    # Local variables:
    # temp folder
    TempFolder = Basefolder + "\\tmp_output\\"
    # temp raster
    tempRaster=TempFolder + "temp1"
    # temp shapefiles
    tempShapefile=TempFolder + "temp1.shp"
    tempShapefile2=TempFolder + "temp2.shp"
    tempShapefile3=TempFolder + "temp3.shp"
    #create the temp folder if it does not exist
    try: 
        if not os.path.isdir(TempFolder):
            os.mkdir(TempFolder)
    except:
        print "Could not create temp folder."   
    print "\nGAP_CLoSR Scenario Tools"
    print "Creating raster change layer"
    print "Starting at " + time.strftime('%d/%m/%y %H:%M:%S')
    StartT=time.time()

    try:
        # Process: Times - Multiply by 0
        arcpy.gp.Times_sa(InputRasterTemplate, "0", tempRaster)
        # Process: Raster to Polygon
        arcpy.RasterToPolygon_conversion(tempRaster, tempShapefile, "NO_SIMPLIFY", "Value")
        # Process: Dissolve
        arcpy.Dissolve_management(InputChangeLayer, tempShapefile2, RasterValueField, "", "MULTI_PART", "DISSOLVE_LINES")
        # Process: Union
        arcpy.Union_analysis(tempShapefile+" #;"+tempShapefile2+" #", tempShapefile3, "ALL", "", "GAPS")
        # Process: Polygon to Raster
        tempEnvironment0 = arcpy.env.snapRaster
        arcpy.env.snapRaster = InputRasterTemplate
        tempEnvironment1 = arcpy.env.extent
        arcpy.env.extent = InputRasterTemplate
        arcpy.PolygonToRaster_conversion(tempShapefile3, RasterValueField, OuputRasterChangeLayer, "CELL_CENTER", "NONE", InputRasterTemplate)
        arcpy.env.snapRaster = tempEnvironment0
        arcpy.env.extent = tempEnvironment1
    except:
        print "\nError in geoprocessing.\n"+ arcpy.GetMessages()

    print "Time elapsed:" +str(time.time()- StartT) + " seconds"
    print "Finished at:"  +  time.strftime('%d/%m/%y %H:%M:%S')
    ##end of main()##


if __name__=='__main__':
    main()

