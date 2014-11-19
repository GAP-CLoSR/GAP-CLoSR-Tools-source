# ---------------------------------------------------------------------------
# Convert_luseToRaster.py
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
#   <InputRasterTemplate (string)>
#   <InputLuse (string)>
#   <OuputRasterChangeLayer (string)>
#   <RasterValueField (string)> 
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
    InputRasterTemplate = Basefolder +"/"+ str(sys.argv[2]) #assumes no path included
    InputLuse = str(sys.argv[3]) # shapefile path included
    OuputRasterChangeLayer = Basefolder +"/"+ str(sys.argv[4]) #assumes no path included
    RasterValueField = str(sys.argv[5])
     
    #BaseFolder="G:/MCAS-S/GAP_CLoSR_App/Data/GAP_CLoSR_Tutorial_1.3/data"
    #InputRasterTemplate = BaseFolder+"/" + "lh_cc"
    #InputLuse = BaseFolder+"/" + "luse.shp"
    #OuputRasterChangeLayer = BaseFolder+"/OutputTest/luse"
    #RasterValueField = "GRIDCODE"

    print"Running"+str(sys.argv[0])+" at "+time.strftime('%d/%m/%y %H:%M:%S')
    print"Base folder: " + Basefolder
    print"Input raster template: " + InputRasterTemplate
    print"Input land-use shapefile: " + InputLuse
    print"Ouput raster change layer: " + OuputRasterChangeLayer
    print"Raster value field: " + RasterValueField
else:
    #
    print "This script is intended to be run with input arguments."

def main():
    # Local variables:
    # temp folder
    TempFolder=Basefolder+"\\tmp_output\\"
    #create the temp folder if it does not exist
    try: 
        if not os.path.isdir(TempFolder):
            os.mkdir(TempFolder)
    except:
        print "Could not create temp folder."   
    # temp raster
    tempRaster=TempFolder+"temp1"
    # temp shapefile
    tempShapefile=TempFolder+"temp1.shp"
    tempShapefile2=TempFolder+"temp2.shp"

    print "\nGAP_CLoSR Default Tools"
    print "Converting luse.shp to raster"
    print "Starting at " + time.strftime('%d/%m/%y %H:%M:%S')
    StartT=time.time()

    try:
        # Process: Times - Multiply by 0
        arcpy.gp.Times_sa(InputRasterTemplate, "0", tempRaster)
        # Process: Raster to Polygon
        arcpy.RasterToPolygon_conversion(tempRaster, tempShapefile, "NO_SIMPLIFY", "Value")
        # Process: Delete Field
        arcpy.DeleteField_management(tempShapefile, RasterValueField)
        # Process: Union
        arcpy.Union_analysis(InputLuse+" #;"+tempShapefile+" #", tempShapefile2, "ALL", "", "GAPS")
        # Process: Polygon to Raster
        tempEnvironment0 = arcpy.env.snapRaster
        arcpy.env.snapRaster = InputRasterTemplate
        tempEnvironment1 = arcpy.env.extent
        arcpy.env.extent = InputRasterTemplate
        arcpy.PolygonToRaster_conversion(tempShapefile2, RasterValueField, OuputRasterChangeLayer, "MAXIMUM_AREA", "NONE", InputRasterTemplate)
        arcpy.env.snapRaster = tempEnvironment0
        arcpy.env.extent = tempEnvironment1
    except:
        print "\nError in creating luse raster.\n"+ arcpy.GetMessages()

    # If previous steps were successful, delete all of the intermediate files in temp folder
    ##try:
    ##    top = TempFolder
    ##    if os.path.isdir(top):
    ##        # CAUTION:  This is dangerous!  For example, if top == '/', it
    ##        # could delete all your disk files.
    ##        for rootD, dirsD, filesD in os.walk(top, topdown=False):
    ##            for nameD in filesD:
    ##                os.remove(os.path.join(rootD, nameD))
    ##            for nameD in dirsD:
    ##                os.rmdir(os.path.join(rootD, nameD))
    ##except:
    ##    LogResult("error in clearing temporary files\n")

    print "Time elapsed:" +str(time.time()- StartT) + " seconds"
    print "Finished at:"  +  time.strftime('%d/%m/%y %H:%M:%S')
    ##end of main()##


if __name__=='__main__':
    main()
