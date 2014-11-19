   
"""
===================================================

Original Script Name:GAP_CLoSR.py

Creation date: 03-Mar-2014
Modified: 17-Jul-2014
          New Name = GAP_CLoSR_ArgV4.py 
          Now accepts input arguments (filenames, locations and processing parameters)
          as script arguments sys.ArgV[1] to sys.ArgV.[10].
          Script modified by Michael Lacey, Michael.Lacey@utas.edu.au
          to run with GUI.
About:

Method for automating pre-processing of spatial data for connectivity modelling using Graphab.

Author: Alex Lechner

alexmarklechner@yahoo.com.au

===================================================

File structure required:

RootDir -CurrentDirectory
GAP_CLoSR_Code - Contains python code and common folder
InputRasters - Location of rasters 
OutputRasters - Empty directory for the processed rasters
OutputRasters\tmp_output - Empty directory for temporary files

===================================================
"""
#
# This script is part of the GAP_CLoSR tools and methods developed by 
# Dr Alex Lechner (alexmarklechner@yahoo.com.au) as a part of the 
# Australian Government's National Environmental Research Program 
# (NERP) Landscapes and Policy hub. This script was adapted for use with 
# GAP_CLoSR_Tools.exe. This script and GAP_CLoSR_GUI.exe are licensed 
# under the Creative Commons AttributionNonCommercial-ShareAlike 3.0
# Australia (CC BY-NC-SA 3.0 AU) license. To view a copy of this licence, 
# visit https://creativecommons.org/licenses/by-nc-sa/3.0/au/.
#

#Parameters NOTE USE FORWARD SLASH NOT BACKSLASH

"""Filenames and location"""
#Importing python libraries
import time
import arcpy
#from arcpy import env 
from arcpy.sa import *
#import numpy as np
import os
import sys
#import scipy as scipy
#from scipy import ndimage
#from collections import Counter
#import pylab
#import csv
#import string

arcpy.CheckOutExtension("spatial")

##RootDir = 'D:/GAP_CLoSR_Tutorial/Python_Processing'
##input_CC =  "lh_cc"
##input_gap_cross = "gap_cross"
##input_luse =  "luse"
##
##"""Processing parameters"""
##original_pixel_size = 2.5
##new_pixel_size = 25
##Max_cost = 75 # highest value a pixel can have not including those pixels with infinite value
##Max_distance = 1100
if len (sys.argv) >2: #ie no arguments provided
    """Filenames and location"""

    RootDir = str(sys.argv[1])
    output_folderP =  str(sys.argv[2])
    input_CC =  str(sys.argv[3])
    input_gap_cross = str(sys.argv[4])
    input_luse =  str(sys.argv[5])

    """Processing parameters"""
    original_pixel_size = float(sys.argv[6])
    new_pixel_size = int(sys.argv[7])
    Max_cost = int(sys.argv[8])
    Max_distance = int(sys.argv[9])
    reclass_txt = str(sys.argv[10])

    print "RootDir: " + RootDir
    print "Output folder: " + output_folderP
    print "input_CC: " + input_CC 
    print "input_gap_cross: " + input_gap_cross
    print "input_luse: " + input_luse
    print "original_pixel_size: " + str(original_pixel_size)
    print "new_pixel_size: " + str(new_pixel_size)
    print "Max_cost: " + str(Max_cost)
    print "Max_distance: " + str(Max_distance)
    print "reclass_txt: " + str(reclass_txt)

else:
    #return "Missing Arguments"
    print "This script is intended to be run with input arguments."
    
########################################################################################

#Do not change anything below here unless you know what you are doing!!!

#min_patch_area_pixels = 160 #160 2.5m pixels

def output_raster_with_spat_ref(input_array, output_raster_name, input_raster_wi_spatial_ref):
    """ Creates a ArcGIS raster file from a 2D array spatially referenced using another ArcGIS raster """
    
    descData=arcpy.Describe(input_raster_wi_spatial_ref) #Get spatial reference from another raster
    cellSize=descData.meanCellHeight
    extent=descData.Extent
    spatialReference=descData.spatialReference
    pnt=arcpy.Point(extent.XMin,extent.YMin)
    newRaster = arcpy.NumPyArrayToRaster(input_array,pnt, cellSize,cellSize)
    arcpy.DefineProjection_management(newRaster,spatialReference)
    newRaster.save(output_raster_name) # Saving raster
    
def reclass_by_ASCII(input_raster,reclass_txt,output_raster): 
    """
    input_raster  = "Z:\\_UTAS_analysis\\Hunter\\GISData\\WorkingData2July\\MCCP2July\\Data\\NumpyTest\\InputRasters\\luse"
    reclass_txt = "Z:\\_UTAS_analysis\\Hunter\\GISData\\WorkingData2July\\MCCP2July\\Data\\NumpyTest\\InputRasters\\luse_reclass.txt"
    output_raster = "Z:\\_UTAS_analysis\\Hunter\\GISData\\WorkingData2July\\MCCP2July\\Data\\NumpyTest\\OutputRasters\\temp\\luse_reclass"
    """
    # Import arcpy module
    #import arcpy
    
    # Check out any necessary licenses
    #arcpy.CheckOutExtension("spatial")
    
    # Process: Reclass by ASCII File
    arcpy.gp.ReclassByASCIIFile_sa(input_raster , reclass_txt, output_raster, "DATA")
    

    
def logfile(log_filename, open_test):
    """Add or update logfile with time based on parameter open_test"""
    current_time = time.asctime( time.localtime(time.time()) )
    
   
    if open_test == "start":
        fileout = open(log_filename,'w') # open and close a text file to be used to record value of comparison between original and processed image
        print "Start time is: "+ current_time
        fileout.write("Start time is: "+ current_time + "\n")       # write the header
    elif open_test == "end":
        fileout = open(log_filename,'a') # open and close a text file to be used to record value of comparison between original and processed image
        print "End time is: "+ current_time
        fileout.write("Finsh time is: "+ current_time + "\n")       # write the header
    else:
        fileout = open(log_filename,'a') # open and close a text file to be used to record value of comparison between original and processed image
        print "Processing " + open_test + current_time
        fileout.write("Processing " + open_test + " "+ current_time + "\n")       # write the header
        
    fileout.close()


def main():
    
    print "Running GAP_CLoSR"
    ###Importing python libraries
    ##import time
    ##import arcpy
    ##from arcpy import env 
    ##from arcpy.sa import *
    ##import numpy as np
    ##import os
    ##import sys
    ##
    ##arcpy.CheckOutExtension("spatial")


    #Importing local libraries
    ##library_path = RootDir +'/GAP_CLoSR_Code/Common'
    ##
    ##sys.path.append(library_path)
    ##from ProcessingMethodsV2 import *
    #from ProcessImage import *

    #############################################
    """ Setup """
    os.chdir(RootDir) # Change current working directory for OS operations
    arcpy.env.workspace = RootDir # Change current working directory for ArcGIS operations
    print(os.getcwd()) #Current directory
    log_filename = RootDir +"/logfile.txt"

    arcpy.env.parallelProcessingFactor = "75%" #http://resources.arcgis.com/en/help/main/10.1/index.html#//001w0000004m000000
    arcpy.env.overwriteOutput = True # Overwrite pre-existing files

    #############################################
    logfile(log_filename, "start") #edit logfile

    input_folder_dir = RootDir +"/"    
    output_folder = output_folderP + "/"
    tmp_output_folder = RootDir + "/tmp_output/"
    #create the temp folder if it does not exist
    try: 
        if not os.path.isdir(tmp_output_folder):
            os.mkdir(tmp_output_folder)
    except:
        print "Could not create temp folder."
        
    """Ensure rasters are multiples of the aggregation_width_factor """
    aggregation_width_factor = new_pixel_size/original_pixel_size
    print "The aggregation_width_factor =" , aggregation_width_factor 
    aggregation_area_factor = (new_pixel_size** 2)/(original_pixel_size**2)
    infinite_cost = aggregation_area_factor * Max_distance+1
    default_cost = new_pixel_size

    # test if infinite cost is greater than maximum pixel value of unsigned integer

    ##############################################
    print "#################"
    print "Parameters"

    print "input_folder_dir = " +input_folder_dir  
     
    print "input_raster_CC_name = " + input_CC
    print "input_raster_gap_cross_name = " + input_gap_cross
    print "input_raster_luse_name = " + input_luse
    print "output_folder = " + output_folder  
    print "tmp_output_folder = " + tmp_output_folder

    print "!!!!! Ensure that infinite cost values in the reclass file is greater than - the total value based on the maximum cost" + str(infinite_cost) + " and maximum dispersal distance" + str(Max_distance)

    print "All values must be integers!"

    """ ################################################################################################### 
    PRE PREPARATION - Reclass all rasters
    """
        
    """Gap crossing array"""
    print "Ensure the gap_array is 0 for no gap crossing structure and 1 if gap crossing structure is present" 


    """Reclass landuse layer
    Infinite cost areas have cost calculated as maximum dispersal distance * aggregation factor 
    http://help.arcgis.com/en/arcgisdesktop/10.0/help/index.html#/How_Reclass_By_ASCII_File_works/009z000000t3000000/
    0 : 2.5 #Other
    10 : -9999 #Infrastructure
    20 : 5 # Road and train
    30 : 7.5 # Hydrology
    """
    print "++++++Reclass landuse layer- NOTE RECLASS MUST BE INTEGERS"
    input_raster = input_folder_dir+input_luse
##    reclass_txt = input_folder_dir+input_luse+"_reclass.txt"
    luse_1RC = tmp_output_folder+input_luse+"_1RC"

    print input_raster 
    print reclass_txt
    print luse_1RC

    """ ####### Process: Reclass """ 
    reclass_by_ASCII(input_raster,reclass_txt,luse_1RC)


    """####################################################"""
    print "STEP 1 Give high cost to areas that have no structural connectivity - identify areas within gap crossing threshold as a property of the landuse cost" 
    print "++++++Identify areas with no structural connectivity and change those values to infinite"

    gap_cross_input = input_folder_dir+ input_gap_cross
    luse_2GC_fname = tmp_output_folder+input_luse+"_2GC"

    print gap_cross_input
    print luse_2GC_fname

    inRaster1 = Raster(gap_cross_input)
    inRaster2 = Raster(luse_1RC)

    """####### Process: Overlay""" 
    luse_2GC = Con(inRaster1 == 0,  infinite_cost, inRaster2) #This is more sensible
    #luse_2GC = Con(inRaster1 == 1, inRaster2, 99999) #Works!!
    luse_2GC.save(luse_2GC_fname)

       
    print "STEP 2 Aggregate image"
    print "++++++Aggregate image"
     

    # Local variables:
    inRaster = luse_2GC_fname
    luse_3Agg_fname = tmp_output_folder+input_luse+"_3Agg"

    print inRaster
    print luse_3Agg_fname

    """ ####### Process: Aggregate""" 
    arcpy.gp.Aggregate_sa(inRaster, luse_3Agg_fname, aggregation_width_factor, "MEAN", "EXPAND", "DATA")

    print "++++++Remove all large values THIS HAS NOT BEEN TESTED"

    luse_4fin_fname = tmp_output_folder+input_luse+"_4fin"

    print luse_3Agg_fname
    print luse_4fin_fname

    inRaster1 = Raster(luse_3Agg_fname)

    """####### Process: Change pixels that are greater than the maximum cost possible to infinite
    In this step pixels that include one pixel of infinite cost e.g. urban are convert to infinite
    """
     
    #Max_cost = highest_pixel_cost * aggregation_width_factor**2
    #print "Maximum cost value per pixel is " + str(Max_cost) + ". Converting to pixel value 99999"

    luse_4fin = Con(inRaster1 > Max_cost,  infinite_cost, inRaster1) #This is more sensible
    #luse_2GC = Con(inRaster1 == 1, inRaster2, 99999) #Works!!
    luse_4fin.save(luse_4fin_fname)

    """ ####### Export to Raster to Tiff""" 
    print "Export resistance file to Raster to Tiff"

    # Local variables:
    input_raster = luse_4fin_fname
    output_raster = output_folder + input_luse +"_4fin" + ".tif"

    print output_raster 

    # "8_BIT_SIGNED" , "8_BIT_UNSIGNED" , "32_BIT_UNSIGNED", "32_BIT_FLOAT"
    # Process: Copy Raster
    arcpy.CopyRaster_management(input_raster, output_raster, "", "", "", "NONE", "NONE", "16_BIT_UNSIGNED", "NONE", "NONE")

    """ ################################################################################################### """


    print "STEP 3 Identify veg where OTHER landuse class exists and where canopy cover is in majority " 
    print "++++++Aggregate CC image"

    inRaster = input_folder_dir + input_CC
    CC_1Agg_fname = tmp_output_folder + input_CC +"_1Agg"

    print inRaster
    print CC_1Agg_fname

    """ ####### Process: Aggregate""" 
    arcpy.gp.Aggregate_sa(inRaster, CC_1Agg_fname, aggregation_width_factor, "MEAN", "EXPAND", "DATA")

    print "++++++threshold raster - majority Convert cells greater than 0.5 to 1 (e.g. majority"

    CC_2Maj_fname = tmp_output_folder + input_CC +"_2Maj"
    inRaster1 = Raster(CC_1Agg_fname)

    print CC_2Maj_fname

    """ ####### Process: overlay""" 
    conRaster1 = Con(inRaster1 > 0.4999, 1,0) #Where inRaster>0.5 make value 1 where not make value 0
    conRaster1.save(CC_2Maj_fname)

    print "++++++remove pixels that are not in the OTHER landuse class"

    """ ####### Process: overlay""" 
    CC_3oth_fname = tmp_output_folder + input_CC +"_3oth"

    print luse_3Agg_fname
    print CC_2Maj_fname

    inRaster1landuse = Raster(luse_3Agg_fname)
    inRaster2 = Raster(CC_2Maj_fname)

    CC_3oth = Con(inRaster1landuse == default_cost,  inRaster2, 0) #This is more sensible
    CC_3oth.save(CC_3oth_fname)


    """ ####### Process: Convert to NumPy Arrays"""  
    CC_array = arcpy.RasterToNumPyArray(CC_3oth_fname)

    #""" ####### Process: Identify patches """ 
    #print "Identify patches"

    #CC_array = identify_patches (CC_array,min_patch_area_pixels)

    """ ####### Process: Convert NumPy Arrays to raster""" 

    print "Convert NumPy Arrays to raster"

    CC_4pat_fname = tmp_output_folder + input_CC +"_4pat"

    output_raster_with_spat_ref(CC_array, CC_4pat_fname, CC_3oth_fname)

    """ ####### Export to Raster to Tiff""" 
    print "Export patch file to Raster to Tiff"

    # Local variables:
    input_raster = CC_4pat_fname
    output_raster = output_folder + input_CC +"_4pat" + ".tif"

    print output_raster 

    # "8_BIT_SIGNED" , "8_BIT_UNSIGNED" , "32_BIT_UNSIGNED", "32_BIT_FLOAT"
    # Process: Copy Raster
    arcpy.CopyRaster_management(input_raster, output_raster, "", "", "", "NONE", "NONE", "16_BIT_UNSIGNED", "NONE", "NONE")

    """ ################################################################################################### """


    """Finished"""

    logfile(log_filename, "end") #edit logfile


    print "Success!!"

    ##end of main()##


if __name__=='__main__':
    main()



