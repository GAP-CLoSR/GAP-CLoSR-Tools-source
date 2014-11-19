# ---------------------------------------------------------------------------
# ConvCostLayerToCirc.py
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
# The script expects 6 input arguments listed below
#   <Output_Folder (string)>
#   <luse_4fin_tif (string)>
#   <Test_Small_Extent_shp (string)>
#   <costascii_asc (string)> 
#   <Output__New_Veg_Layer (string)>
#   <InfinteCostValue (string)>
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
    luse_4fin_tif = str(sys.argv[2])
    Test_Small_Extent_shp = str(sys.argv[3])
    costascii_asc = str(sys.argv[4])
    Output__New_Veg_Layer = Output_Folder +"/"+ str(sys.argv[5])#assumes no path included
    InfinteCostValue = float(sys.argv[6])

    # Script arguments
    #Output_Folder = r"G:\testing"                   
    #luse_4fin_tif = r"G:\inputs\luse_4fin.tif"
    #Test_Small_Extent_shp = r"G:\inputs\Test_Small_Extent.shp"
    #costascii_asc = r"G:\testing\costascii.asc"
    #Output__New_Veg_Layer = r"G:\testing\cost"
    #InfinteCostValue = float("1105")
                        
    print "Running"+str(sys.argv[0])+" at "+time.strftime('%d/%m/%y %H:%M:%S')
    print "Output Folder: " + Output_Folder
    print "luse_4fin_tif: " + luse_4fin_tif
    print "Test_Small_Extent_shp: " + Test_Small_Extent_shp
    print "costascii_asc: " + costascii_asc
    print "Output__New_Veg_Layer: " + Output__New_Veg_Layer
    print "InfinteCostValue: " + str(InfinteCostValue)
                        
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
    CostTemp = TempFolder +"costtemp"

    print "\nCreating Circuitscape Inputs"
    print "Convert cost layer to Circuitscape input"
    print "Starting at " + time.strftime('%d/%m/%y %H:%M:%S')
    StartT=time.time()

    try:
        # Process: Extract by Mask
        tempEnvironment0 = arcpy.env.cellSize
        arcpy.env.cellSize = "MAXOF"
        arcpy.gp.ExtractByMask_sa(luse_4fin_tif, Test_Small_Extent_shp, CostTemp)
        arcpy.env.cellSize = tempEnvironment0
        # Process: Raster Calculator
        #arcpy.gp.RasterCalculator_sa("Con(\"%CostTemp%\"<%InfinteCostValue%,\"%CostTemp%\")", Output__New_Veg_Layer)
        outputRaster = arcpy.sa.Con(arcpy.Raster(CostTemp) < InfinteCostValue, arcpy.Raster(CostTemp))
        outputRaster.save(Output__New_Veg_Layer)
        # Process: Raster to ASCII
        arcpy.RasterToASCII_conversion(Output__New_Veg_Layer, costascii_asc)
    
    except:
        print "\nError in creating raster.\n"+ arcpy.GetMessages()

    print "Time elapsed:" +str(time.time()- StartT) + " seconds"
    print "Finished at:"  +  time.strftime('%d/%m/%y %H:%M:%S')
    ##end of main()##


if __name__=='__main__':
    main()
