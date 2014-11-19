# ---------------------------------------------------------------------------
# ConvGraphabPatchToCirc_GC.py
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
#  <Output_Folder (string)>
#  <patches_tif (string)>
#  <Test_Small_Extent_shp (string)>
#  '"CircFocalNodesPatchAsciiFile=" & GC_CC_PatchAscii)
#  '"CircFocalNodesPatchRaster=" & GC_CC_PatchCirc_R)
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
    patchascii_asc = str(sys.argv[4])
    Patch =  Output_Folder +"/"+ str(sys.argv[5])

    # Script arguments
    #Output_Folder = r"G:\testing"                   
    #patches_tif = r"G:\inputs\patches.tif"
    #Test_Small_Extent_shp = r"G:\inputs\Test_Small_Extent.shp"
    #patchascii_asc = r"\patchascii.asc"
    #Patch = r"\patchcirc"
                        
    print "Running"+str(sys.argv[0])+" at "+time.strftime('%d/%m/%y %H:%M:%S')
    print "Output Folder: " + Output_Folder
    print "patches_tif: " + patches_tif
    print "Test_Small_Extent_shp: " + Test_Small_Extent_shp
    print "CircFocalNodesPatchAsciiFile: " + patchascii_asc
    print "CircFocalNodesPatchRaster: " + Patch
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
    PatchTemp =  TempFolder +"patchtemp"
    #
    print "\nCreating Circuitscape Inputs"
    print "Convert Graphab patches to Circuitscape input"
    print "Starting at " + time.strftime('%d/%m/%y %H:%M:%S')
    StartT=time.time()
 
    try:   
        # Process: Extract by Mask
        tempEnvironment0 = arcpy.env.cellSize
        arcpy.env.cellSize = "MAXOF"
        arcpy.gp.ExtractByMask_sa(patches_tif, Test_Small_Extent_shp, PatchTemp)
        arcpy.env.cellSize = tempEnvironment0

        # Process: Raster Calculator
        #arcpy.gp.RasterCalculator_sa("Con(\"%PatchTemp%\">0,\"%PatchTemp%\")", Patch)
        #outputRaster = arcpy.sa.Con(arcpy.Raster(PatchTemp) > 0, arcpy.Raster(PatchTemp))
        outputRaster = arcpy.sa.Con(PatchTemp > 0, PatchTemp)
        outputRaster.save(Patch)

        # Process: Raster to ASCII
        arcpy.RasterToASCII_conversion(Patch, patchascii_asc)
    
    except:
        print "\nError in creating raster.\n"+ arcpy.GetMessages()

    print "Time elapsed:" +str(time.time()- StartT) + " seconds"
    print "Finished at:"  +  time.strftime('%d/%m/%y %H:%M:%S')
    ##end of main()##


if __name__=='__main__':
    main()
