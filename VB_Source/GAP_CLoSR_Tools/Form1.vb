'This work was supported by the Landscapes and Policy Research Hub, which is funded 
'through the Australian Government’s National Environmental Research Programme (NERP). 
'The wildlife connectivity study was an initiative of the Sustainable Regional 
'Development Program in the Department of the Environment.
'
'This application is an implementation of GAP_CLoSR tools and methods  
'developed by Dr Alex Lechner (alexmarklechner@yahoo.com.au) as a part 
'of the NERP Landscapes and Policy hub.

'GAP_CLoSR_GUI.exe was developed by Dr Michael Lacey (Michael.Lacey@utas.edu.au), in 
'association with Alex Lechner and the NERP Landscapes and Policy hub and is licensed 
'under the Creative Commons AttributionNonCommercial-ShareAlike 3.0 Australia 
'(CC BY-NC-SA 3.0 AU) license . To view a copy of this licence, 
'visit https://creativecommons.org/licenses/by-nc-sa/3.0/au/).

Public Class frm_Main
    'set some module level variables
#Region "Module-level variables"
    'Filenames and locations
    Private GC_RootDir As String   'base folder for raster processing
    Private GC_OutputDir As String 'output folder for for GAP_CLoSR.py
    Private GC_input_CC As String            'lh_cc            raster variable name
    Private GC_input_gap_cross As String     'gap_cross        raster variable name
    Private GC_input_gap_crossO As String    'gap_crosso       raster variable name
    Private GC_input_luse As String          'luse             raster variable name
    Private GC_input_luse_shp As String      'luse.shp         shapefile variable name
    Private GC_input_changelyr As String     'changelayer      raster variable name
    Private GC_input_changelyr_shp As String 'changelayer.shp  shapefile variable name
    Private GC_veg_removal As String         'lh_ccrem         raster variable name
    Private GC_veg_addition As String        'lh_ccadd         raster variable name
    Private GC_gapcross_add As String        'gap_crossadd     raster variable name
    Private GC_luse_add As String            'luse_add         raster variable name
    'circuitscape
    Private GC_CC_OutFolder As String   'Circ output folder
    Private GC_CC_TestExt_shp As String 'Circ test-extent shapefile
    Private GC_CC_Comp_shp As String 'Circ components shapefile
    Private GC_CC_luse_4fin As String 'Circ luse_4fin.tif file
    Private GC_CC_patches_tif As String 'Circ patches.tif file
    Private GC_CC_OutpNV_Ras As String   'Circ output new veg raster
    Private GC_CC_CostAscii As String   'Circ output cost ascii file
    Private GC_CC_InfiniteCostV As Double   'Circ infinite cost value   ## maybe set to string format
    Private GC_CC_FocalNodesComp_R As String   'Circ patch temp raster->reasigned variable to FocalNodesComp raster
    Private GC_CC_PatchCirc_R As String   'Circ patch circ raster
    Private GC_CC_PatchAscii As String   'Circ patch ascii file
    Private GC_CC_FocalNodesComp_asc As String 'new variable to FocalNodesComp ascii file name
    'testing
    Private GC_LastFolder As String  'the last visited folder first two tabs
    Private GC_LastFolderCC As String  'the last visited folder for circuitscape tab
    Private GC_v As New List(Of String)  'list of vegetation rasters
    Private GC_g As New List(Of String)  'list of gapcross rasters
    Private GC_l As New List(Of String)  'list of landuse rasters
    Private GC_v_sel As String           'the selected veg raster for GAP_CLoSR script
    Private GC_g_sel As String           'the selected gapcross raster for GAP_CLoSR script
    Private GC_l_sel As String           'the selected landuse raster for GAP_CLoSR script

    'Processing parameters
    Private GC_conv_luse_RVfield As String   'variable for Python Script Convert_luseToRaster.py
    Private GC_Max_dist1 As Double           'variable for Python Script CreateGapCrossingLayer.py
    Private GC_CellMultiplier As Double      'variable for Python Script CreateGapCrossingLayerOLD.py
    Private GC_FieldName As String           'variable for Python Script 1_CreateRasterChangeLayer.py
    Private GC_HydrologyValue1 As Double     'variable for Python Script 2b_ModifyVegetationLayer_Addition.py
    Private GC_CellMultiplier2 As Double     'variable for Python Script 3_CreateGapCrossingLayer.py
    Private GC_HydrologyValue2 As Double     'variable for Python Script 4_ModifyLuseLayer_AddInfra.py
    Private GC_original_pixel_size As Single 'variable for Python Script GAP_CLoSR_ArgV.py 
    Private GC_new_pixel_size As Single      'variable for Python Script GAP_CLoSR_ArgV.py
    Private GC_Max_cost As Single            'variable for Python Script GAP_CLoSR_ArgV.py
    Private GC_Max_distance As Double        'variable for Python Script GAP_CLoSR_ArgV.py
    Private GC_reclass_txt As String         'variable for Python Script GAP_CLoSR_ArgV.py

    'flags that datasets and paths exist
    Private GC_RootDir_exists As Boolean = False             'base folder exists
    Private GC_OutputDir_exists As Boolean = False           'output folder exists
    Private GC_input_CC_exists As Boolean = False            'lh_cc            exists
    Private GC_input_gap_cross_exists As Boolean = False     'gap_cross        exists
    Private GC_input_gap_crossO_exists As Boolean = False    'gap_crosso       exists
    Private GC_input_luse_exists As Boolean = False         'luse             exists
    Private GC_input_luse_shp_exists As Boolean = False     'luse.shp         exists
    Private GC_input_changelyr_exists As Boolean = False    'changelayer      exists
    Private GC_input_changelyr_shp_exists As Boolean = False 'changelayer.shp  exists
    Private GC_veg_removal_exists As Boolean = False        'lh_ccrem         exists
    Private GC_veg_addition_exists As Boolean = False       'lh_ccadd         exists
    Private GC_gapcross_add_exists As Boolean = False       'gap_crossadd     exists
    Private GC_luse_add_exists As Boolean = False           'luse_add         exists
    Private GC_reclass_txt_exists As Boolean = False         'reclass.txt      exists
    '                                                   Circuitscape variables
    Private GC_CC_OutFolder_exists As Boolean = False   'Circ output folder    exists
    Private GC_CC_TestExt_shp_exists As Boolean = False 'Circ test-extent shapefile    exists
    Private GC_CC_Comp_shp_exists As Boolean = False 'Circ components shapefile    exists
    Private GC_CC_luse_4fin_exists As Boolean = False 'Circ luse_4fin.tif file    exists
    Private GC_CC_patches_tif_exists As Boolean = False 'Circ patches.tif file    exists
    Private GC_CC_OutpNV_Ras_exists As Boolean = False   'Circ output new veg raster
    Private GC_CC_CostAscii_exists As Boolean = False   'Circ output cost ascii file
    Private GC_CC_InfiniteCostV_exists As Boolean = False   'Circ infinite cost value
    Private GC_CC_FocalNodesComp_R_exists As Boolean = False   'Circ patch temp raster->reasigned variable for FocalNodesComp raster
    Private GC_CC_PatchCirc_R_exists As Boolean = False   'Circ patch circ raster
    Private GC_CC_PatchAscii_exists As Boolean = False   'Circ patch ascii file
    Private GC_CC_FocalNodesComp_asc_exists As Boolean = False 'new variable for FocalNodesComp asc

    Private GC_v_sel_exists As Boolean = False           'the selected veg raster for GAP_CLoSR script    exists
    Private GC_g_sel_exists As Boolean = False          'the selected gapcross raster for GAP_CLoSR script    exists
    Private GC_l_sel_exists As Boolean = False          'the selected landuse raster for GAP_CLoSR script    exists

    'flags that processing parameters are valid
    Private GC_conv_luse_RVfield_exists As Boolean = False   'for Python Script Convert_luseToRaster.py
    Private GC_Max_dist1_exists As Boolean = False           'for Python Script CreateGapCrossingLayer.py
    Private GC_CellMultiplier_exists As Boolean = False      'for Python Script CreateGapCrossingLayerOLD.py
    Private GC_FieldName_exists As Boolean = False           'for Python Script 1_CreateRasterChangeLayer.py
    Private GC_HydrologyValue1_exists As Boolean = False     'for Python Script 2b_ModifyVegetationLayer_Addition.py
    Private GC_CellMultiplier2_exists As Boolean = False     'for Python Script 3_CreateGapCrossingLayer.py
    Private GC_HydrologyValue2_exists As Boolean = False     'for Python Script 4_ModifyLuseLayer_AddInfra.py
    Private GC_original_pixel_size_exists As Boolean = False 'for Python Script GAP_CLoSR_ArgV.py 
    Private GC_new_pixel_size_exists As Boolean = False     'for Python Script GAP_CLoSR_ArgV.py
    Private GC_Max_cost_exists As Boolean = False           'for Python Script GAP_CLoSR_ArgV.py
    Private GC_Max_distance_exists As Boolean = False       'for Python Script GAP_CLoSR_ArgV.py
    'Python parameters
    Private GC_theApplication As String
    Private GC_ScriptsFolder As String  'Python Scripts folder
    Private GC_Scr_DefL As String       'Python Script Convert_luseToRaster.py
    Private GC_Scr_DefGC As String      'Python Script CreateGapCrossingLayer.py
    Private GC_Scr_DefO As String       'Python Script CreateGapCrossingLayerOLD.py
    Private GC_Scr_Scen1 As String      'Python Script 1_CreateRasterChangeLayer.py
    Private GC_Scr_Scen2a As String     'Python Script 2a_ModifyVegetationLayer_Removal.py
    Private GC_Scr_Scen2b As String     'Python Script 2b_ModifyVegetationLayer_Addition.py
    Private GC_Scr_Scen3 As String      'Python Script 3_CreateGapCrossingLayer.py
    Private GC_Scr_Scen4 As String      'Python Script 4_ModifyLuseLayer_AddInfra.py
    Private GC_theScript As String      'Python Script GAP_CLoSR_ArgV.py 
    Private GC_ScriptCirc1 As String    'Python Script ConvCostLayerToCirc_GC.py
    Private GC_ScriptCirc2 As String    'Python Script ConvGraphabPatchToCirc_GC.py
    Private GC_ScriptCirc3 As String    'Python Script ConvGraphabPatchesAndLabelToCirc_GC.py
    'flags for existence of the scripts
    Private GC_theApplication_exists As Boolean = False 'Python application
    Private GC_ScriptsFolder_exists As Boolean = False  'Scripts folder
    Private GC_Scr_DefL_exists As Boolean = False
    Private GC_Scr_DefGC_exists As Boolean = False
    Private GC_Scr_DefO_exists As Boolean = False
    Private GC_Scr_Scen1_exists As Boolean = False
    Private GC_Scr_Scen2a_exists As Boolean = False
    Private GC_Scr_Scen2b_exists As Boolean = False
    Private GC_Scr_Scen3_exists As Boolean = False
    Private GC_Scr_Scen4_exists As Boolean = False
    Private GC_theScript_exists As Boolean = False
    Private GC_ScriptCirc1_exists As Boolean = False
    Private GC_ScriptCirc2_exists As Boolean = False
    Private GC_ScriptCirc3_exists As Boolean = False

    Private GC_Gr1ButtonsChecked As Integer = 0 'default and scenario buttons
    Private GC_Gr3ButtonsChecked As Integer = 0 'circuitscape buttons

    Private GC_theArguments As String
    'coding parameters - some of these may be redundant
    Private GC_DefaultsLoaded As Boolean = False       'set initially to false (Have parameter defaults been loaded?) Variable currently not being used.
    'Private GC_AllVariablesComplete As Boolean = False 'set initially to false (Are all variables entered?)
    Private GC_DefaultsSaved As Boolean = False        'set initially to false (Have parameter defaults been saved?) Variable currently not being used.
    'Private GC_HasScripRun As Boolean = False          'set initially to false (Has the script run?)
    Private GC_PyAppSelected As Boolean = False 'has the python exe application been selected?  Variable currently not being used.
    Private GC_PyAppSaved As Boolean = False    'has the python exe application name been saved?  Variable currently not being used.
    Private GC_startup As Boolean = True  'Variable currently not being used.
    'get the current exe application path to locate settings file
    Private GC_appPath As String 'Application path. This variable now set at form load(), see below.
    Private GC_PPath As String 'Configs.txt settings file path. Now set after appPath at form load(), see below.
    Private GC_CloseFlagged As Boolean = False
    Private GC_AltScriptPath_exists As Boolean = False 'Existence of the AltScriptPath.txt file in application folder
    'Private GC_ScriptsFolder_exists As Boolean = False 'Existence of the scripts folder (already declared above)
    Private GC_ScriptsFolder_isDefault As Boolean = True 'Scripts folder is GAP_CLoSR_Code in application folder
    Private GC_ConfigsFile_exists As Boolean = False 'Configs.txt file exists

#End Region
    'Form load
    Private Sub frm_Main_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim Msg1 As String = ""

        'get the current exe application path to locate settings file
        GC_appPath = IO.Path.GetDirectoryName(Application.ExecutablePath)
        'find scripts folder
        'first look for AltScriptPath.txt if it exists
        Try
            If My.Computer.FileSystem.FileExists(GC_appPath & "\AltScriptPath.txt") Then 'AltScriptPath.txt exists
                GC_AltScriptPath_exists = True
                'open the AltScriptPath.txt file and read the first line
                Dim aspf As New IO.StreamReader(GC_appPath & "\AltScriptPath.txt")
                Dim aspfdata As String
                aspfdata = aspf.ReadLine() 'scripts folder
                aspf.Close()
                'what is on the first line?
                If aspfdata = GC_appPath & "\GAP_CLoSR_Code" Then 'its GAP_CLoSR_Code
                    'default
                    If My.Computer.FileSystem.DirectoryExists(aspfdata) Then
                        GC_ScriptsFolder = aspfdata
                        GC_ScriptsFolder_exists = True
                        GC_ScriptsFolder_isDefault = True
                    End If
                ElseIf aspfdata = "" Or aspfdata = Nothing Then 'its nothing
                    'use default and write file
                    SetDefaultScriptsPath()
                    If GC_ScriptsFolder_exists = False Then
                        Msg1 = "Scripts folder ('GAP_CLoSR_Code') folder could not be found in application folder."
                    End If
                Else 'its another path
                    If My.Computer.FileSystem.DirectoryExists(aspfdata) Then 'that is valid
                        GC_ScriptsFolder = aspfdata
                        GC_ScriptsFolder_exists = True
                        GC_ScriptsFolder_isDefault = False
                    Else 'path not valid - set to default and write file
                        SetDefaultScriptsPath()
                        If GC_ScriptsFolder_exists = False Then
                            Msg1 = "A scripts folder could not be found at" & vbCrLf
                            Msg1 = Msg1 & aspfdata & vbCrLf
                            Msg1 = Msg1 & "and the default scripts folder ('GAP_CLoSR_Code')" & vbCrLf
                            Msg1 = Msg1 & "folder could not be found in application folder."
                        End If
                    End If
                End If
            Else 'AltScriptPath.txt does not exist
                GC_AltScriptPath_exists = False
                'set to default and write file
                SetDefaultScriptsPath()
                If GC_ScriptsFolder_exists = False Then
                    Msg1 = "Scripts folder ('GAP_CLoSR_Code') folder could not be found in application folder."
                End If
            End If
        Catch
        End Try
        'error mesage if there was an error in locating the scripts folder
        If Msg1 <> "" Then
            Msg1 = Msg1 & "Please ensure that there is a valid scripts folder" & vbCrLf
            Msg1 = Msg1 & "either named 'GAP_CLoSR_Code' in the application folder or" & vbCrLf
            Msg1 = Msg1 & "at the path specified in AltScriptPath.txt and" & vbCrLf
            Msg1 = Msg1 & "contains a Configs.txt file and" & vbCrLf
            Msg1 = Msg1 & "then restart GAP_CLoSR_Tools.exe."

            MsgBox(Msg1)
        End If




        'load python settings
        GC_PPath = GC_ScriptsFolder & "\Configs.txt"
        If GC_ScriptsFolder_exists = True Then
            If My.Computer.FileSystem.FileExists(GC_PPath) = True Then 'Configs.txt file also exists within scripts folder
                GC_ConfigsFile_exists = True
                Try
                    btn_LoadPyExePth_Click(Me, Nothing)
                Catch ex As Exception
                    MsgBox("Could not load '...\GAP_CLoSR_Code\Configs.txt'.")
                End Try
                'Else
                '    Msg1 = "There is no Configs.txt file in the selected scripts folder."
            End If
        Else
            'add code here
        End If



        'create scenarios folder if it does not exist
        If My.Computer.FileSystem.DirectoryExists(GC_appPath & "\Scenarios") = False Then
            My.Computer.FileSystem.CreateDirectory(GC_appPath & "\Scenarios")
        End If
        'add some text to script and script error text boxes
        txt_PythonOutput.Text = "Script output." & vbCrLf & "====="
        txt_PythonError.Text = "Script error output." & vbCrLf & "====="
        'set default
        rb_C_No.Checked = True
        rb_S_No.Checked = True
        btn_RunCombined.Enabled = False
        btn_ProcessrastersTab4.Enabled = False
        btn_RunCircuitscape_Tab4.Enabled = False


    End Sub
    'set default scripts path
    Private Sub SetDefaultScriptsPath()
        If My.Computer.FileSystem.DirectoryExists(GC_appPath & "\GAP_CLoSR_Code") = True Then
            GC_ScriptsFolder = GC_appPath & "\GAP_CLoSR_Code"
            GC_ScriptsFolder_exists = True
            GC_ScriptsFolder_isDefault = True
            'if found set it as scripts folder and write new AltScriptPath.txt file
            Try
                'the AltScriptPath.txt file
                Dim swf As New IO.StreamWriter(GC_appPath & "\AltScriptPath.txt", False) 'overwrite
                swf.WriteLine(GC_ScriptsFolder) '
                swf.Close()
            Catch ex As Exception
                MsgBox("Error while saving AltScriptPath.txt file")
            End Try
            GC_AltScriptPath_exists = True
        Else
            GC_ScriptsFolder_exists = False
            GC_ScriptsFolder_isDefault = True
        End If
    End Sub
    'load parameter settings generic code
    Private Sub LoadParameters(path1 As String)

        'this routine opens a file to read based on the path passed
        'set combo box lists to nothing
        'it goes through the file line by line and reads the variable into GC_...
        'updates relevant text boxes
        'checks existence of the variable
        'close settings file
        'note type casting should be automatic as variables have been declared

        'listed below are the variables in the settings file in the expected order

        '"___Parameter_Settings_File___")
        '"___Root_Directory___")
        '"RootDir=" & GC_RootDir)
        '"___Shapefiles___")
        '"LanduseShapefile=" & GC_input_luse_shp)
        '"ParamRasterValueField1=" & GC_conv_luse_RVfield)
        '"ChangeShapfile=" & GC_input_changelyr_shp)
        '"ParamRasterValueField2=" & GC_FieldName)
        '"___Default___")
        '"HabitatTemplateRaster=" & GC_input_CC)
        '"DefaultLandUseRaster=" & GC_input_luse)
        '"DefaultGapCrossRaster=" & GC_input_gap_cross)
        '"ParamMaxDistance=" & GC_Max_dist1)
        '"DefaultGapCrossMethod2Raster=" & GC_input_gap_crossO)
        '"ParamCellMultiplier1=" & GC_CellMultiplier)
        '"___Scenario___")
        '"ScenarioChangeRaster=" & )
        '"ScenarioHabitatRemovalRaster=" & GC_veg_removal)
        '"ScenarioHabitatAdditionRaster=" & GC_veg_addition)
        '"ParamHydrologyValue1=" & GC_HydrologyValue1)
        '"ScenarioGapCrossRaster=" & GC_gapcross_add)
        '"ParamCellMultiplier2=" & GC_CellMultiplier2)
        '"ScenarioLandUseInfr=" & GC_luse_add)
        '"ParamHydrologyValue2=" & GC_HydrologyValue2)
        '"___Model___")
        '"ModelHabitatRaster=" & GC_v_sel)
        '"ModelGapCrossRaster=" & GC_g_sel)
        '"ModelLandUseRaster=" & GC_l_sel)
        '"OutputFolder=" & GC_OutputDir)
        '"ParamOriginalPixelSize=" & GC_original_pixel_size)
        '"ParamNewPixelSize=" & GC_new_pixel_size)
        '"ParamMaxCost=" & GC_Max_cost)
        '"ParamMaxDistance=" & GC_Max_distance)
        '"ParamReclassTextFile=" & GC_reclass_txt)
        '"___Circuitscape___")
        '"CircOutputFolder=" & GC_CC_OutFolder)
        '"CircTestExtentShapefile=" & GC_CC_TestExt_shp)
        '"CircComponentsShapefile=" & GC_CC_Comp_shp)
        '"CircLuse_4fin_tifFile=" & GC_CC_luse_4fin)
        '"CircPatches_tifFile=" & GC_CC_patches_tif)
        '"CircOutputNewVegRaster=" & GC_CC_OutpNV_Ras)
        '"CircOutputCostAsciiFile=" & GC_CC_CostAscii)
        '"CircInfiniteCostValue=" & GC_CC_InfiniteCostV)
        '"CircPatchTempRaster=" & GC_CC_PatchTemp_R)
        '"CircPatchCircRaster=" & GC_CC_PatchCirc_R)
        '"CircPatchAsciiFile=" & GC_CC_PatchAscii)
        '"___End_File___")

        Try
            Dim swrd As New IO.StreamReader(path1)
            '"___Parameter_Settings_File___")
            swrd.ReadLine() 'no data in this line

            '"___Root_Directory___")
            swrd.ReadLine() 'no data in this line

            '"RootDir=" & GC_RootDir)
            GC_RootDir = swrd.ReadLine().Split(CChar("=")).Last()
            txt_SelRootDir.Text = GC_RootDir
            txt_InputF.Text = GC_RootDir
            txt_rasterFolderTab2.Text = GC_RootDir
            GC_RootDir_exist()

            '"___Shapefiles___")
            swrd.ReadLine() 'no data in this line

            '"LanduseShapefile=" & GC_input_luse_shp)
            GC_input_luse_shp = swrd.ReadLine().Split(CChar("=")).Last()
            txt_LanduseShp.Text = GC_input_luse_shp
            GC_input_luse_shp_exist()

            '"ParamRasterValueField1=" & GC_conv_luse_RVfield)
            GC_conv_luse_RVfield = swrd.ReadLine().Split(CChar("=")).Last()
            txt_RasterValueField.Text = GC_conv_luse_RVfield
            GC_conv_luse_RVfield_exist()

            '"ChangeShapfile=" & GC_input_changelyr_shp)
            GC_input_changelyr_shp = swrd.ReadLine().Split(CChar("=")).Last()
            txt_ChngLyrShpTab2.Text = GC_input_changelyr_shp
            GC_input_changelyr_shp_exist()

            '"ParamRasterValueField2=" & GC_FieldName)
            GC_FieldName = swrd.ReadLine().Split(CChar("=")).Last()
            txt_RasValFldTab2.Text = GC_FieldName
            GC_FieldName_exist()

            '"___Default___")
            swrd.ReadLine() 'no data in this line

            '"HabitatTemplateRaster=" & GC_input_CC)
            GC_input_CC = swrd.ReadLine().Split(CChar("=")).Last()
            'copy to other tabs
            txt_inplh_cc1.Text = GC_input_CC
            txt_habitatTab2.Text = GC_input_CC

            '"DefaultLandUseRaster=" & GC_input_luse)
            GC_input_luse = swrd.ReadLine().Split(CChar("=")).Last()
            txt_luse1.Text = GC_input_luse
            txt_landuseTab2.Text = GC_input_luse

            '"DefaultGapCrossRaster=" & GC_input_gap_cross)
            GC_input_gap_cross = swrd.ReadLine().Split(CChar("=")).Last()
            txt_gap_cross1.Text = GC_input_gap_cross

            '"ParamMaxDistance=" & GC_Max_dist1)
            GC_Max_dist1 = CDbl(swrd.ReadLine().Split(CChar("=")).Last())
            If GC_Max_dist1 <> Nothing Then
                txt_MaxDist1.Text = CStr(GC_Max_dist1)
                GC_Max_dist1_exists = True
                txt_MaxDist1.ForeColor = Color.Black
            Else
                GC_Max_dist1_exists = False
                txt_MaxDist1.ForeColor = Color.Red
            End If

            '"DefaultGapCrossMethod2Raster=" & GC_input_gap_crossO)
            GC_input_gap_crossO = swrd.ReadLine().Split(CChar("=")).Last()
            txt_gapcrossOld.Text = GC_input_gap_crossO

            '"ParamCellMultiplier1=" & GC_CellMultiplier)
            GC_CellMultiplier = CDbl(swrd.ReadLine().Split(CChar("=")).Last())
            If GC_CellMultiplier <> Nothing Then
                txt_inpMultiTab1.Text = CStr(GC_CellMultiplier)
                GC_CellMultiplier_exists = True
                txt_inpMultiTab1.ForeColor = Color.Black
            Else
                GC_CellMultiplier_exists = False
                txt_inpMultiTab1.ForeColor = Color.Red
            End If

            '"___Scenario___")
            swrd.ReadLine() 'no data in this line

            '"ScenarioChangeRaster=" & GC_input_changelyr)
            GC_input_changelyr = swrd.ReadLine().Split(CChar("=")).Last()
            txt_ChangerasterTab2.Text = GC_input_changelyr

            '"ScenarioHabitatRemovalRaster=" & GC_veg_removal)
            GC_veg_removal = swrd.ReadLine().Split(CChar("=")).Last()
            txt_NewRasterRemv.Text = GC_veg_removal

            '"ScenarioHabitatAdditionRaster=" & GC_veg_addition)
            GC_veg_addition = swrd.ReadLine().Split(CChar("=")).Last()
            txt_NewRastAdd.Text = GC_veg_addition

            '"ParamHydrologyValue1=" & GC_HydrologyValue1)
            GC_HydrologyValue1 = CDbl(swrd.ReadLine().Split(CChar("=")).Last())
            If GC_HydrologyValue1 <> Nothing Then
                txt_HydrolVal1.Text = CStr(GC_HydrologyValue1)
                GC_HydrologyValue1_exists = True
                txt_HydrolVal1.ForeColor = Color.Black
            Else
                GC_HydrologyValue1_exists = False
                txt_HydrolVal1.ForeColor = Color.Red
            End If

            '"ScenarioGapCrossRaster=" & GC_gapcross_add)
            GC_gapcross_add = swrd.ReadLine().Split(CChar("=")).Last()
            txt_GapcrossTab2.Text = GC_gapcross_add

            '"ParamCellMultiplier2=" & GC_CellMultiplier2)
            GC_CellMultiplier2 = CDbl(swrd.ReadLine().Split(CChar("=")).Last())
            If GC_CellMultiplier2 <> Nothing Then
                txt_CellMultip2.Text = CStr(GC_CellMultiplier2)
                GC_CellMultiplier2_exists = True
                txt_CellMultip2.ForeColor = Color.Black
            Else
                GC_CellMultiplier2_exists = False
                txt_CellMultip2.ForeColor = Color.Red
            End If

            '"ScenarioLandUseInfr=" & GC_luse_add)
            GC_luse_add = swrd.ReadLine().Split(CChar("=")).Last()
            txt_infrastructure.Text = GC_luse_add

            '"ParamHydrologyValue2=" & GC_HydrologyValue2)
            GC_HydrologyValue2 = CDbl(swrd.ReadLine().Split(CChar("=")).Last())
            If GC_HydrologyValue2 <> Nothing Then
                txt_HydrolVal2.Text = CStr(GC_HydrologyValue2)
                GC_HydrologyValue2_exists = True
                txt_HydrolVal2.ForeColor = Color.Black
            Else
                GC_HydrologyValue2_exists = False
                txt_HydrolVal2.ForeColor = Color.Red
            End If

            '"___Model___")
            swrd.ReadLine() 'no data in this line

            '"ModelHabitatRaster=" & GC_v_sel)
            GC_v_sel = swrd.ReadLine().Split(CChar("=")).Last()
            txt_inp_cc.Text = GC_v_sel

            '"ModelGapCrossRaster=" & GC_g_sel)
            GC_g_sel = swrd.ReadLine().Split(CChar("=")).Last()
            txt_inp_gc.Text = GC_g_sel

            '"ModelLandUseRaster=" & GC_l_sel)
            GC_l_sel = swrd.ReadLine().Split(CChar("=")).Last()
            txt_inp_luse.Text = GC_l_sel

            '"OutputFolder=" & GC_OutputDir)
            GC_OutputDir = swrd.ReadLine().Split(CChar("=")).Last()
            txt_OutputF.Text = GC_OutputDir
            GC_OutputDir_exist()

            '"ParamOriginalPixelSize=" & GC_original_pixel_size)
            GC_original_pixel_size = CSng(swrd.ReadLine().Split(CChar("=")).Last())
            If GC_original_pixel_size <> Nothing Then
                txt_OriginalPixelSize.Text = CStr(GC_original_pixel_size)
                GC_original_pixel_size_exists = True
                txt_OriginalPixelSize.ForeColor = Color.Black
            Else
                GC_original_pixel_size_exists = False
                txt_OriginalPixelSize.ForeColor = Color.Red
            End If

            '"ParamNewPixelSize=" & GC_new_pixel_size)
            GC_new_pixel_size = CSng(swrd.ReadLine().Split(CChar("=")).Last())
            If GC_new_pixel_size <> Nothing Then
                txt_NewPixelSize.Text = CStr(GC_new_pixel_size)
                GC_new_pixel_size_exists = True
                txt_NewPixelSize.ForeColor = Color.Black
            Else
                GC_new_pixel_size_exists = False
                txt_NewPixelSize.ForeColor = Color.Red
            End If
            '"ParamMaxCost=" & GC_Max_cost)
            GC_Max_cost = CSng(swrd.ReadLine().Split(CChar("=")).Last())
            If GC_Max_cost <> Nothing Then
                txt_MaxCost.Text = CStr(GC_Max_cost)
                GC_Max_cost_exists = True
                txt_MaxCost.ForeColor = Color.Black
            Else
                GC_Max_cost_exists = False
                txt_MaxCost.ForeColor = Color.Red
            End If

            '"ParamMaxDistance=" & GC_Max_distance)
            GC_Max_distance = CDbl(swrd.ReadLine().Split(CChar("=")).Last())
            If GC_Max_distance <> Nothing Then
                txt_MaxDist.Text = CStr(GC_Max_distance)
                GC_Max_distance_exists = True
                txt_MaxDist.ForeColor = Color.Black
            Else
                GC_Max_distance_exists = False
                txt_MaxDist.ForeColor = Color.Red
            End If

            '"ParamReclassTextFile=" & GC_reclass_txt)
            GC_reclass_txt = swrd.ReadLine().Split(CChar("=")).Last()
            txt_ReclassTxtFile.Text = GC_reclass_txt
            GC_reclass_txt_exist()

            '"___Circuitscape___")
            swrd.ReadLine() 'no data in this line

            '"CircOutputFolder=" & GC_CC_OutFolder)
            GC_CC_OutFolder = swrd.ReadLine().Split(CChar("=")).Last()
            txt_Circ_OutpFolder.Text = GC_CC_OutFolder
            GC_CC_OutFolder_exist()

            '"CircTestExtentShapefile=" & GC_CC_TestExt_shp)
            GC_CC_TestExt_shp = swrd.ReadLine().Split(CChar("=")).Last()
            txt_CircTestExt.Text = GC_CC_TestExt_shp
            GC_CC_TestExt_shp_exist()

            '"CircComponentsShapefile=" & GC_CC_Comp_shp)
            GC_CC_Comp_shp = swrd.ReadLine().Split(CChar("=")).Last()
            txt_ComponentsShp.Text = GC_CC_Comp_shp
            GC_CC_Comp_shp_exist()

            '"CircLuse_4fin_tifFile=" & GC_CC_luse_4fin)
            GC_CC_luse_4fin = swrd.ReadLine().Split(CChar("=")).Last()
            txt_luse_4fin_file.Text = GC_CC_luse_4fin
            GC_CC_luse_4fin_exist()

            '"CircPatches_tifFile=" & GC_CC_patches_tif)
            GC_CC_patches_tif = swrd.ReadLine().Split(CChar("=")).Last()
            txt_patches_tif.Text = GC_CC_patches_tif
            GC_CC_patches_tif_exist()

            '"CircOutputNewVegRaster=" & GC_CC_OutpNV_Ras)
            GC_CC_OutpNV_Ras = swrd.ReadLine().Split(CChar("=")).Last()
            txt_OutpNV_Ras.Text = GC_CC_OutpNV_Ras
            GC_CC_OutpNV_Ras_exist()

            '"CircOutputCostAsciiFile=" & GC_CC_CostAscii)
            GC_CC_CostAscii = swrd.ReadLine().Split(CChar("=")).Last()
            txt_CostAscii.Text = GC_CC_CostAscii
            GC_CC_CostAscii_exist()

            '"CircInfiniteCostValue=" & GC_CC_InfiniteCostV)
            GC_CC_InfiniteCostV = CDbl(swrd.ReadLine().Split(CChar("=")).Last())
            txt_InfiniteCostV.Text = CStr(GC_CC_InfiniteCostV)
            GC_CC_InfiniteCostV_exist()

            '"CircFocalNodesCompRaster=" & GC_CC_FocalNodesComp_R)
            GC_CC_FocalNodesComp_R = swrd.ReadLine().Split(CChar("=")).Last()
            txt_FocalNodesComp_R.Text = GC_CC_FocalNodesComp_R
            GC_CC_FocalNodesComp_R_exist()

            '"CircFocalNodesCompAsciiFile=" & GC_CC_FocalNodesComp_asc)
            GC_CC_FocalNodesComp_asc = swrd.ReadLine().Split(CChar("=")).Last()
            txt_FocalNodesComp_asc.Text = GC_CC_FocalNodesComp_asc
            GC_CC_FocalNodesComp_asc_exist()

            '"CircFocalNodesPatchRaster=" & GC_CC_PatchCirc_R)
            GC_CC_PatchCirc_R = swrd.ReadLine().Split(CChar("=")).Last()
            txt_PatchCirc_R.Text = GC_CC_PatchCirc_R
            GC_CC_PatchCirc_R_exist()

            '"CircFocalNodesPatchAsciiFile=" & GC_CC_PatchAscii)
            GC_CC_PatchAscii = swrd.ReadLine().Split(CChar("=")).Last()
            txt_PatchAscii.Text = GC_CC_PatchAscii
            GC_CC_PatchAscii_exist()

            '"___End_File___")



            'close the settings file
            swrd.Close()
            '
            GC_DefaultsLoaded = True

            'update raster text boxcolours and existence flags
            UpdateRasterFieldsColoursAndExistence()

            'update list for combo boxes
            UpdateListsForComboBoxes()


        Catch ex As Exception
            If GC_startup = False Then
                MsgBox("There was an error in loading the parameters file.") 'not shown when attempting to load settings at startup
            End If
        End Try

    End Sub
    'update raster fields colours and existence tabs 1,2,3
    Private Sub UpdateRasterFieldsColoursAndExistence()

        Try
            GC_input_CC_exist()
            GC_input_luse_exist()
            GC_input_gap_cross_exist()
            GC_input_gap_crossO_exist()

            GC_input_changelyr_exist()
            GC_veg_removal_exist()
            GC_veg_addition_exist()
            GC_gapcross_add_exist()
            GC_luse_add_exist()

            GC_v_sel_exist()
            GC_g_sel_exist()
            GC_l_sel_exist()
        Catch ex As Exception
            MsgBox("there was an error in updating the raster fields")
        End Try
    End Sub

    'raster folder leave generic tabs 1,2,3
    Private Sub RasterFolderLeave()
        'update raster text boxcolours and existence flags
        UpdateRasterFieldsColoursAndExistence()
        'update list for combo boxes
        UpdateListsForComboBoxes()
    End Sub

#Region "Tab 3 (graphab tab) Text Boxes"
    'entering Select Root Dir text box
    Private Sub txt_SelRootDir_Enter(sender As Object, e As EventArgs) Handles txt_SelRootDir.Enter
        txt_SelRootDir.ForeColor = Color.Blue
    End Sub
    'leaving select root directory text box
    Private Sub txt_SelRootDir_Leave(sender As Object, e As EventArgs) Handles txt_SelRootDir.Leave
        Try
            If txt_SelRootDir.Text <> "" Then
                GC_RootDir = txt_SelRootDir.Text
                txt_rasterFolderTab2.Text = txt_SelRootDir.Text 'update to tab 2
                txt_InputF.Text = txt_SelRootDir.Text 'update to tab 1
                GC_RootDir_exist()
                'update raster existence, colors and combo box lists
                RasterFolderLeave()
            End If
        Catch ex As Exception
            MsgBox("enter text")
        End Try
    End Sub
    'GAP_CLoSR root directory browser -tab 3
    Private Sub btn_RootDir_Click(sender As Object, e As EventArgs) Handles btn_RootDir.Click
        Dim tempText As String = txt_SelRootDir.Text

        'sets the initial selected directory to the luse.shp path if that has been previously selected
        dlg_GetRootFolder.SelectedPath = GC_LastFolder

        Try
            Dim dlgc = dlg_GetRootFolder.ShowDialog()
            If dlgc = Windows.Forms.DialogResult.OK Then
                txt_SelRootDir.Text = dlg_GetRootFolder.SelectedPath
                GC_RootDir = txt_SelRootDir.Text
                GC_LastFolder = dlg_GetRootFolder.SelectedPath
                'copy to other tabs
                txt_rasterFolderTab2.Text = txt_SelRootDir.Text 'update to tab 2
                txt_InputF.Text = txt_SelRootDir.Text 'update to tab 1
                'update text colour on all tabs if folder exists/does not exist
                GC_RootDir_exist()
                'update colours on text boxes existence flags and combo box lists


            ElseIf dlgc = Windows.Forms.DialogResult.Cancel Then
                txt_SelRootDir.Text = tempText
            End If
        Catch ex As Exception
            MsgBox("Please enter or select the path to the GAP_CLoSR\python_processing folder")
        End Try
    End Sub

    'entering inp cc text box
    Private Sub txt_inp_cc_Enter(sender As Object, e As EventArgs) Handles txt_inp_cc.Enter
        txt_inp_cc.ForeColor = Color.Blue
    End Sub
    'leaving inp cc text box
    Private Sub GC_v_sel_exist()
        If (GC_v_sel <> "") And My.Computer.FileSystem.DirectoryExists(GC_RootDir & "\" & GC_v_sel) Then
            GC_v_sel_exists = True
            txt_inp_cc.ForeColor = Color.Black
        ElseIf IsValidGrid(GC_v_sel) = False Then
            GC_v_sel_exists = False
            txt_inp_cc.ForeColor = Color.Violet
        Else
            GC_v_sel_exists = False
            txt_inp_cc.ForeColor = Color.Red
        End If
    End Sub
    Private Sub txt_inp_cc_Leave(sender As Object, e As EventArgs) Handles txt_inp_cc.Leave
        Try
            GC_v_sel = txt_inp_cc.Text
            GC_v_sel_exist()

        Catch ex As Exception
            MsgBox("enter text")
        End Try
    End Sub

    'entering inp gc text box
    Private Sub txt_inp_gc_Enter(sender As Object, e As EventArgs) Handles txt_inp_gc.Enter
        txt_inp_gc.ForeColor = Color.Blue
    End Sub
    'leaving inp gc text box
    Private Sub GC_g_sel_exist()
        If (GC_g_sel <> "") And My.Computer.FileSystem.DirectoryExists(GC_RootDir & "\" & GC_g_sel) Then
            GC_g_sel_exists = True
            txt_inp_gc.ForeColor = Color.Black
        ElseIf IsValidGrid(GC_g_sel) = False Then
            GC_g_sel_exists = False
            txt_inp_gc.ForeColor = Color.Violet
        Else
            GC_g_sel_exists = False
            txt_inp_gc.ForeColor = Color.Red
        End If
    End Sub
    Private Sub txt_inp_gc_Leave(sender As Object, e As EventArgs) Handles txt_inp_gc.Leave
        Try
            GC_g_sel = txt_inp_gc.Text
            GC_g_sel_exist()

        Catch ex As Exception
            MsgBox("enter text")
        End Try
    End Sub

    'entering inp luse text box
    Private Sub txt_inp_luse_Enter(sender As Object, e As EventArgs) Handles txt_inp_luse.Enter
        txt_inp_luse.ForeColor = Color.Blue
    End Sub
    'leaving inp luse text box
    Private Sub GC_l_sel_exist()
        If (GC_l_sel <> "") And My.Computer.FileSystem.DirectoryExists(GC_RootDir & "\" & GC_l_sel) Then
            GC_l_sel_exists = True
            txt_inp_luse.ForeColor = Color.Black
        ElseIf IsValidGrid(GC_l_sel) = False Then
            GC_l_sel_exists = False
            txt_inp_luse.ForeColor = Color.Violet
        Else
            GC_l_sel_exists = False
            txt_inp_luse.ForeColor = Color.Red
        End If
    End Sub
    Private Sub txt_inp_luse_Leave(sender As Object, e As EventArgs) Handles txt_inp_luse.Leave
        Try
            GC_l_sel = txt_inp_luse.Text
            GC_l_sel_exist()

        Catch ex As Exception
            MsgBox("enter text")
        End Try
    End Sub

    'entering OriginalPixelSize text box
    Private Sub txt_OriginalPixelSize_Enter(sender As Object, e As EventArgs) Handles txt_OriginalPixelSize.Enter
        txt_OriginalPixelSize.ForeColor = Color.Blue
    End Sub
    'leaving OriginalPixelSize text box - numerical
    Private Sub txt_OriginalPixelSize_Leave(sender As Object, e As EventArgs) Handles txt_OriginalPixelSize.Leave
        Try
            If txt_OriginalPixelSize.Text <> Nothing Then
                GC_original_pixel_size = CSng(txt_OriginalPixelSize.Text)
                GC_original_pixel_size_exists = True
                txt_OriginalPixelSize.ForeColor = Color.Black
            Else
                GC_original_pixel_size_exists = False
                txt_OriginalPixelSize.ForeColor = Color.Red
            End If

        Catch ex As Exception
            MsgBox("The value entered for Original Pixel Size must be a number")
            txt_OriginalPixelSize.Select()
        End Try
    End Sub

    'entering NewPixelSize text box
    Private Sub txt_NewPixelSize_Enter(sender As Object, e As EventArgs) Handles txt_NewPixelSize.Enter
        txt_NewPixelSize.ForeColor = Color.Blue
    End Sub
    'leaving NewPixelSize text box - numerical
    Private Sub txt_NewPixelSize_Leave(sender As Object, e As EventArgs) Handles txt_NewPixelSize.Leave
        Try
            If txt_NewPixelSize.Text <> Nothing Then
                GC_new_pixel_size = CSng(txt_NewPixelSize.Text)
                GC_new_pixel_size_exists = True
                txt_NewPixelSize.ForeColor = Color.Black
            Else
                GC_new_pixel_size_exists = False
                txt_NewPixelSize.ForeColor = Color.Red
            End If

        Catch ex As Exception
            MsgBox("The value entered for New Pixel Size must be a number")
            txt_NewPixelSize.Select()
        End Try
    End Sub

    'entering MaxCost text box
    Private Sub txt_MaxCost_Enter(sender As Object, e As EventArgs) Handles txt_MaxCost.Enter
        txt_MaxCost.ForeColor = Color.Blue
    End Sub
    'leaving MaxCost text box - numerical
    Private Sub txt_MaxCost_Leave(sender As Object, e As EventArgs) Handles txt_MaxCost.Leave
        Try
            If txt_MaxCost.Text <> Nothing Then
                GC_Max_cost = CSng(txt_MaxCost.Text)
                GC_Max_cost_exists = True
                txt_MaxCost.ForeColor = Color.Black
            Else
                GC_Max_cost_exists = False
                txt_MaxCost.ForeColor = Color.Red
            End If

        Catch ex As Exception
            MsgBox("The value entered for Max Cost must be a number")
            txt_MaxCost.Select()
        End Try
    End Sub

    'entering MaxDist text box
    Private Sub txt_MaxDist_Enter(sender As Object, e As EventArgs) Handles txt_MaxDist.Enter
        txt_MaxDist.ForeColor = Color.Blue
    End Sub
    'leaving MaxDist text box - numerical
    Private Sub txt_MaxDist_Leave(sender As Object, e As EventArgs) Handles txt_MaxDist.Leave
        Try
            If txt_MaxDist.Text <> Nothing Then
                GC_Max_distance = CDbl(txt_MaxDist.Text)
                GC_Max_distance_exists = True
                txt_MaxDist.ForeColor = Color.Black
            Else
                GC_Max_distance_exists = False
                txt_MaxDist.ForeColor = Color.Red
            End If

        Catch ex As Exception
            MsgBox("The value entered for Max Distance must be a number")
            txt_MaxDist.ForeColor = Color.Red
            txt_MaxDist.Select()
        End Try
    End Sub

    'entering reclass_txt text box
    Private Sub txt_ReclassTxtFile_Enter(sender As Object, e As EventArgs) Handles txt_ReclassTxtFile.Enter
        txt_ReclassTxtFile.ForeColor = Color.Blue
    End Sub
    'leaving reclass_txt text box
    Private Sub GC_reclass_txt_exist()
        If (GC_reclass_txt <> "") And My.Computer.FileSystem.FileExists(GC_reclass_txt) Then
            GC_reclass_txt_exists = True
            txt_ReclassTxtFile.ForeColor = Color.Black
        Else
            GC_reclass_txt_exists = False
            txt_ReclassTxtFile.ForeColor = Color.Red
        End If
    End Sub
    Private Sub txt_ReclassTxtFile_Leave(sender As Object, e As EventArgs) Handles txt_ReclassTxtFile.Leave
        Try
            GC_reclass_txt = txt_ReclassTxtFile.Text
            GC_reclass_txt_exist()

        Catch ex As Exception
            MsgBox("Please enter reclass_txt name with path.")
        End Try
    End Sub
    'button to select the reclass_txt file - Tab 3
    Private Sub btn_ReclTextFile_Click(sender As Object, e As EventArgs) Handles btn_ReclTextFile.Click

        Dim temp5 As String = txt_ReclassTxtFile.Text
        Try
            dlg_ReclassText.Title = "Select reclass text (.txt) file"
            dlg_ReclassText.FileName = ""
            Dim dlgb = dlg_ReclassText.ShowDialog() ' find file dialog to find or set scenario file name
            If dlgb = Windows.Forms.DialogResult.OK Then
                txt_ReclassTxtFile.Text = dlg_ReclassText.FileName
                GC_reclass_txt = txt_ReclassTxtFile.Text
                GC_reclass_txt_exist()

            ElseIf dlgb = Windows.Forms.DialogResult.Cancel Then
                txt_ReclassTxtFile.Text = temp5
            End If
        Catch ex As Exception
            MsgBox("Error in selecting reclass text file")
        End Try
    End Sub

    'enter output folder text box - tab 3
    Private Sub txt_OutputF_Enter(sender As Object, e As EventArgs) Handles txt_OutputF.Enter
        txt_OutputF.ForeColor = Color.Blue
    End Sub
    'leave output folder text box - tab 3
    Private Sub GC_OutputDir_exist()
        If (GC_OutputDir <> "") And My.Computer.FileSystem.DirectoryExists(GC_OutputDir) Then
            GC_OutputDir_exists = True
            txt_OutputF.ForeColor = Color.Black
        Else
            GC_OutputDir_exists = False
            txt_OutputF.ForeColor = Color.Red
        End If
    End Sub
    Private Sub txt_OutputF_Leave(sender As Object, e As EventArgs) Handles txt_OutputF.Leave
        Try
            GC_OutputDir = txt_OutputF.Text
            GC_OutputDir_exist()

        Catch ex As Exception
            MsgBox("Error in setting output folder.")
        End Try
    End Sub
    'select output folder' button - Tab 3
    Private Sub btn_OutputF_Click(sender As Object, e As EventArgs) Handles btn_OutputF.Click
        Dim tempText As String = txt_OutputF.Text

        Try
            dlg_GetRootFolder.Description = "Select output folder (for Graphab inputs)"
            Dim dlgc = dlg_GetRootFolder.ShowDialog()
            If dlgc = Windows.Forms.DialogResult.OK Then
                txt_OutputF.Text = dlg_GetRootFolder.SelectedPath
                GC_OutputDir = txt_OutputF.Text
                GC_OutputDir_exist()

            ElseIf dlgc = Windows.Forms.DialogResult.Cancel Then
                txt_OutputF.Text = tempText
            End If
        Catch ex As Exception
            MsgBox("Please enter or select the path to the raster folder")
        End Try
    End Sub

    'select tab 3 by clicking on it - Tab 3
    Private Sub tb_3_Click(sender As Object, e As EventArgs) Handles tb_3.Click
        tb_3.Select()
    End Sub
    'select panel 18 tab 3 by clicking on it - Tab 3
    Private Sub Panel18_Click(sender As Object, e As EventArgs) Handles Panel18.Click
        Panel18.Select()
    End Sub

#End Region

    'clear all parameter settings (tabs 1,2 graphab and circuitscape)
    Private Sub btn_ClearAll_Click(sender As Object, e As EventArgs) Handles btn_ClearAll.Click
        'clear text boxes
        txt_InputF.Text = ""
        txt_rasterFolderTab2.Text = ""
        txt_SelRootDir.Text = ""
        txt_OutputF.Text = ""
        txt_inplh_cc1.Text = ""
        txt_habitatTab2.Text = ""
        txt_NewRasterRemv.Text = ""
        txt_NewRastAdd.Text = ""
        txt_gap_cross1.Text = ""
        txt_gapcrossOld.Text = ""
        txt_GapcrossTab2.Text = ""
        txt_LanduseShp.Text = ""
        txt_luse1.Text = ""
        txt_landuseTab2.Text = ""
        txt_infrastructure.Text = ""
        txt_ChngLyrShpTab2.Text = ""
        txt_ChangerasterTab2.Text = ""
        txt_ReclassTxtFile.Text = ""
        txt_RasterValueField.Text = ""
        txt_HydrolVal1.Text = ""
        txt_MaxDist1.Text = ""
        txt_inpMultiTab1.Text = ""
        txt_CellMultip2.Text = ""
        txt_HydrolVal2.Text = ""
        txt_RasValFldTab2.Text = ""
        txt_OriginalPixelSize.Text = ""
        txt_NewPixelSize.Text = ""
        txt_MaxCost.Text = ""
        txt_MaxDist.Text = ""
        txt_inp_cc.Text = ""
        txt_inp_gc.Text = ""
        txt_inp_luse.Text = ""
        txt_PythonOutput.Text = ""
        txt_PythonError.Text = ""
        'circuitscape
        txt_CircTestExt.Text = ""
        txt_luse_4fin_file.Text = ""
        txt_Circ_OutpFolder.Text = ""
        txt_patches_tif.Text = ""
        txt_ComponentsShp.Text = ""
        txt_OutpNV_Ras.Text = ""
        txt_CostAscii.Text = ""
        txt_InfiniteCostV.Text = ""
        txt_FocalNodesComp_R.Text = ""
        txt_FocalNodesComp_asc.Text = ""
        txt_PatchCirc_R.Text = ""
        txt_PatchAscii.Text = ""

        'reset variables
        GC_RootDir = Nothing
        GC_OutputDir = Nothing
        GC_input_CC = Nothing
        GC_input_gap_cross = Nothing
        GC_input_gap_crossO = Nothing
        GC_input_luse = Nothing
        GC_input_luse_shp = Nothing
        GC_input_changelyr = Nothing
        GC_input_changelyr_shp = Nothing
        GC_veg_removal = Nothing
        GC_veg_addition = Nothing
        GC_gapcross_add = Nothing
        GC_luse_add = Nothing
        GC_v.Clear()
        GC_g.Clear()
        GC_l.Clear()
        GC_v_sel = Nothing
        GC_g_sel = Nothing
        GC_l_sel = Nothing
        GC_conv_luse_RVfield = Nothing
        GC_Max_dist1 = Nothing
        GC_CellMultiplier = Nothing
        GC_FieldName = Nothing
        GC_HydrologyValue1 = Nothing
        GC_CellMultiplier2 = Nothing
        GC_HydrologyValue2 = Nothing
        GC_original_pixel_size = Nothing
        GC_new_pixel_size = Nothing
        GC_Max_cost = Nothing
        GC_Max_distance = Nothing
        GC_reclass_txt = Nothing

        GC_CC_OutFolder = Nothing
        GC_CC_TestExt_shp = Nothing
        GC_CC_Comp_shp = Nothing
        GC_CC_luse_4fin = Nothing
        GC_CC_patches_tif = Nothing
        GC_CC_OutpNV_Ras = Nothing
        GC_CC_CostAscii = Nothing
        GC_CC_InfiniteCostV = Nothing
        GC_CC_FocalNodesComp_R = Nothing
        GC_CC_FocalNodesComp_asc = Nothing
        GC_CC_PatchCirc_R = Nothing
        GC_CC_PatchAscii = Nothing

        GC_RootDir_exists = False
        GC_OutputDir_exists = False
        GC_input_CC_exists = False
        GC_input_gap_cross_exists = False
        GC_input_gap_crossO_exists = False
        GC_input_luse_exists = False
        GC_input_luse_shp_exists = False
        GC_input_changelyr_exists = False
        GC_input_changelyr_shp_exists = False
        GC_veg_removal_exists = False
        GC_veg_addition_exists = False
        GC_gapcross_add_exists = False
        GC_luse_add_exists = False
        GC_reclass_txt_exists = False
        GC_v_sel_exists = False
        GC_g_sel_exists = False
        GC_l_sel_exists = False
        GC_conv_luse_RVfield_exists = False
        GC_Max_dist1_exists = False
        GC_CellMultiplier_exists = False
        GC_FieldName_exists = False
        GC_HydrologyValue1_exists = False
        GC_CellMultiplier2_exists = False
        GC_HydrologyValue2_exists = False
        GC_original_pixel_size_exists = False
        GC_new_pixel_size_exists = False
        GC_Max_cost_exists = False
        GC_Max_distance_exists = False

        GC_CC_OutFolder_exists = False
        GC_CC_TestExt_shp_exists = False
        GC_CC_Comp_shp_exists = False
        GC_CC_luse_4fin_exists = False
        GC_CC_patches_tif_exists = False
        GC_CC_OutpNV_Ras_exists = False
        GC_CC_CostAscii_exists = False
        GC_CC_InfiniteCostV_exists = False
        GC_CC_FocalNodesComp_R_exists = False
        GC_CC_FocalNodesComp_asc_exists = False
        GC_CC_PatchCirc_R_exists = False
        GC_CC_PatchAscii_exists = False

        GC_DefaultsLoaded = False 'set initially to false (Have defaults been loaded?)
        btn_ClearAll.Enabled = False 'reset the button to not enabled
        'btn_ReadSettings.Text = "Load Settings"



        'GC_AllVariablesComplete = False
    End Sub
    'write Parameters to output file generic code   
    Private Sub SaveParamsGeneric(path2 As String)
        Try
            Dim sw As New IO.StreamWriter(path2, False) 'overwrite
            sw.WriteLine("___Parameter_Settings_File___")
            sw.WriteLine("___Root_Directory___")
            sw.WriteLine("RootDir=" & GC_RootDir)
            sw.WriteLine("___Shapefiles___")
            sw.WriteLine("LanduseShapefile=" & GC_input_luse_shp)
            sw.WriteLine("ParamRasterValueField1=" & GC_conv_luse_RVfield)
            sw.WriteLine("ChangeShapfile=" & GC_input_changelyr_shp)
            sw.WriteLine("ParamRasterValueField2=" & GC_FieldName)
            sw.WriteLine("___Default___")
            sw.WriteLine("HabitatTemplateRaster=" & GC_input_CC)
            sw.WriteLine("DefaultLandUseRaster=" & GC_input_luse)
            sw.WriteLine("DefaultGapCrossRaster=" & GC_input_gap_cross)
            sw.WriteLine("ParamMaxDistance=" & GC_Max_dist1)
            sw.WriteLine("DefaultGapCrossMethod2Raster=" & GC_input_gap_crossO)
            sw.WriteLine("ParamCellMultiplier1=" & GC_CellMultiplier)
            sw.WriteLine("___Scenario___")
            sw.WriteLine("ScenarioChangeRaster=" & GC_input_changelyr)
            sw.WriteLine("ScenarioHabitatRemovalRaster=" & GC_veg_removal)
            sw.WriteLine("ScenarioHabitatAdditionRaster=" & GC_veg_addition)
            sw.WriteLine("ParamHydrologyValue1=" & GC_HydrologyValue1)
            sw.WriteLine("ScenarioGapCrossRaster=" & GC_gapcross_add)
            sw.WriteLine("ParamCellMultiplier2=" & GC_CellMultiplier2)
            sw.WriteLine("ScenarioLandUseInfr=" & GC_luse_add)
            sw.WriteLine("ParamHydrologyValue2=" & GC_HydrologyValue2)
            sw.WriteLine("___Model___")
            sw.WriteLine("ModelHabitatRaster=" & GC_v_sel)
            sw.WriteLine("ModelGapCrossRaster=" & GC_g_sel)
            sw.WriteLine("ModelLandUseRaster=" & GC_l_sel)
            sw.WriteLine("OutputFolder=" & GC_OutputDir)
            sw.WriteLine("ParamOriginalPixelSize=" & GC_original_pixel_size)
            sw.WriteLine("ParamNewPixelSize=" & GC_new_pixel_size)
            sw.WriteLine("ParamMaxCost=" & GC_Max_cost)
            sw.WriteLine("ParamMaxDistance=" & GC_Max_distance)
            sw.WriteLine("ParamReclassTextFile=" & GC_reclass_txt)
            sw.WriteLine("___Circuitscape___")
            sw.WriteLine("CircOutputFolder=" & GC_CC_OutFolder)
            sw.WriteLine("CircTestExtentShapefile=" & GC_CC_TestExt_shp)
            sw.WriteLine("CircComponentsShapefile=" & GC_CC_Comp_shp)
            sw.WriteLine("CircLuse_4fin_tifFile=" & GC_CC_luse_4fin)
            sw.WriteLine("CircPatches_tifFile=" & GC_CC_patches_tif)
            sw.WriteLine("CircOutputNewVegRaster=" & GC_CC_OutpNV_Ras)
            sw.WriteLine("CircOutputCostAsciiFile=" & GC_CC_CostAscii)
            sw.WriteLine("CircInfiniteCostValue=" & GC_CC_InfiniteCostV)
            sw.WriteLine("CircFocalNodesCompRaster=" & GC_CC_FocalNodesComp_R)
            sw.WriteLine("CircFocalNodesCompAsciiFile=" & GC_CC_FocalNodesComp_asc)
            sw.WriteLine("CircFocalNodesPatchRaster=" & GC_CC_PatchCirc_R)
            sw.WriteLine("CircFocalNodesPatchAsciiFile=" & GC_CC_PatchAscii)
            sw.WriteLine("___End_File___")

            sw.Close()
            GC_DefaultsSaved = True
        Catch ex As Exception
            MsgBox("Error while saving settings to file")
        End Try
    End Sub

#Region "Script Execution"
    'run python script - generic code
    Private Sub ExecutePythonScript(PyApp As String, PyScript As String, PyArgs As String)
        Dim theApplication As String
        Dim theScript As String
        Dim theArguments As String

        theApplication = PyApp
        'add path to the script name
        theScript = PyScript
        'include the arguments
        theArguments = PyArgs
        'debug
        'MsgBox(theApplication & vbCrLf & theScript & vbCrLf & theArguments)

        Try
            Dim p As Process = New Process()
            p.StartInfo.UseShellExecute = False
            p.StartInfo.RedirectStandardOutput = True
            p.StartInfo.RedirectStandardError = True
            p.StartInfo.FileName = theApplication
            p.StartInfo.Arguments = theScript & theArguments
            p.StartInfo.UseShellExecute = False
            p.StartInfo.CreateNoWindow = True
            p.Start()
            'txt_PythonOutput.Text = txt_PythonOutput.Text & vbCrLf & "=====" & vbCrLf & (p.StandardOutput.ReadToEnd())
            'txt_PythonError.Text = txt_PythonError.Text & vbCrLf & "=====" & vbCrLf & (p.StandardError.ReadToEnd())
            txt_PythonOutput.AppendText(vbCrLf & (p.StandardOutput.ReadToEnd()) & "=====")
            txt_PythonOutput.Select(txt_PythonOutput.TextLength(), 0)
            txt_PythonError.AppendText(vbCrLf & (p.StandardError.ReadToEnd()) & "=====")
            txt_PythonError.Select(txt_PythonError.TextLength(), 0)

            p.WaitForExit()
            p.Close()

        Catch ex As Exception
            MsgBox("There was a problem with script execution." & vbCrLf & "Python Application: " & theApplication & vbCrLf & "Script and arguments: " & theScript & theArguments)
        End Try
    End Sub
    'runs Python Script GAP_CLoSR_ArgV4.py (GC_theScript) (10 arguments) - button currently on tab 4
    Private Sub btn_Run_GC_Click(sender As Object, e As EventArgs) Handles btn_Run_GC.Click

        Dim theApplication As String
        Dim theScript As String
        Dim theArguments As String
        Dim s As String
        'buttons disabled so that user does not press it twice
        btn_Run_GC.Enabled = False
        btn_ProcessrastersTab4.Enabled = False

        'Script Arguments        
        'RootDir				        GC_RootDir
        'output_folder                  GC_OutputDir
        'input_CC			            GC_v_sel
        'input_gap_cross			    GC_g_sel
        'input_luse			            GC_l_sel
        'original_pixel_size		    GC_original_pixel_size
        'new_pixel_size			        GC_new_pixel_size
        'Max_cost			            GC_Max_cost
        'Max_distance			        GC_Max_distance
        'reclass_txt			        GC_reclass_txt

        'there is probably a more efficient way to do it but will try it this way:
        s = "" & vbCrLf
        If GC_RootDir_exists = False Then
            s = s & "raster folder (Root folder)" & vbCrLf
        End If
        If GC_OutputDir_exists = False Then
            s = s & "output folder (Graphab)" & vbCrLf
        End If
        If GC_v_sel_exists = False Then
            s = s & "input vegetation (habitat) raster" & vbCrLf
        End If
        If GC_g_sel_exists = False Then
            s = s & "input gap cross raster" & vbCrLf
        End If
        If GC_l_sel_exists = False Then
            s = s & "input land use raster" & vbCrLf
        End If
        If GC_original_pixel_size_exists = False Then
            s = s & "original pixel size value" & vbCrLf
        End If
        If GC_new_pixel_size_exists = False Then
            s = s & "new pixel size value" & vbCrLf
        End If
        If GC_Max_cost_exists = False Then
            s = s & "maximum resistance cost value" & vbCrLf
        End If
        If GC_Max_distance_exists = False Then
            s = s & "maximum interpatch dispersal distance value" & vbCrLf
        End If
        If GC_reclass_txt_exists = False Then
            s = s & "input reclass text file name" & vbCrLf
        End If

        If s <> ("" & vbCrLf) Then
            MessageBox.Show("please enter the following data:" & s, "Some inputs are missing", MessageBoxButtons.OK)
        Else
            theApplication = CheckForSpaces(GC_theApplication)
            'add path to the script name
            theScript = CheckForSpaces(GC_ScriptsFolder & "\" & GC_theScript)
            'there are currently 10 arguments
            theArguments = " " & CheckForSpaces(GC_RootDir)
            theArguments = theArguments & " " & CheckForSpaces(GC_OutputDir)
            theArguments = theArguments & " " & CheckForSpaces(GC_v_sel)
            theArguments = theArguments & " " & CheckForSpaces(GC_g_sel)
            theArguments = theArguments & " " & CheckForSpaces(GC_l_sel)
            theArguments = theArguments & " " & GC_original_pixel_size
            theArguments = theArguments & " " & GC_new_pixel_size
            theArguments = theArguments & " " & GC_Max_cost
            theArguments = theArguments & " " & GC_Max_distance
            theArguments = theArguments & " " & CheckForSpaces(GC_reclass_txt)

            ExecutePythonScript(theApplication, theScript, theArguments)

        End If
        'buttons enabled again
        btn_Run_GC.Enabled = True
        chk_SelGraphab.Checked = False
        btn_ProcessrastersTab4.Enabled = False

    End Sub
    'runs  Python Script Convert_luseToRaster.py (GC_Scr_DefL) (5 arguments)
    Private Sub RunScriptDefL()
        Dim theApplication As String
        Dim theScript As String
        Dim theArguments As String

        'Script Arguments        
        'basefolder			            GC_RootDir
        'lh_cc				            GC_input_CC
        'luse.shp with path		        GC_input_luse_shp
        'luse				            GC_input_luse
        'rastervalue field "GRIDCODE"	GC_conv_luse_RVfield

        'run the script
        theApplication = CheckForSpaces(GC_theApplication)
        'add path to the script name
        theScript = CheckForSpaces(GC_ScriptsFolder & "\" & GC_Scr_DefL)
        'there are currently 5 arguments
        theArguments = " " & CheckForSpaces(GC_RootDir)
        theArguments = theArguments & " " & CheckForSpaces(GC_input_CC)
        theArguments = theArguments & " " & CheckForSpaces(GC_input_luse_shp)
        theArguments = theArguments & " " & CheckForSpaces(GC_input_luse)
        theArguments = theArguments & " " & GC_conv_luse_RVfield

        ExecutePythonScript(theApplication, theScript, theArguments)
        '


    End Sub
    '   prepare data for Convert_luseToRaster.py
    Private Sub btn_RunConvLusetoRaster_Click(sender As Object, e As EventArgs) Handles btn_RunConvLusetoRaster.Click
        Dim s As String
        'button disabled so that user does not press it twice
        btn_RunConvLusetoRaster.Enabled = False
        'Script Arguments        
        'basefolder			            GC_RootDir
        'lh_cc				            GC_input_CC
        'luse.shp with path		        GC_input_luse_shp
        'luse				            GC_input_luse
        'rastervalue field "GRIDCODE"	GC_conv_luse_RVfield


        'there is probably a more efficient way to do it but will try it this way:
        s = "" & vbCrLf
        If GC_RootDir_exists = False Then
            s = s & "raster folder (Root folder)" & vbCrLf
        End If
        If GC_input_CC_exists = False Then
            s = s & "input vegetation (habitat) raster" & vbCrLf
        End If
        If GC_input_luse_shp_exists = False Then
            s = s & "input land-use shapefile" & vbCrLf
        End If
        If GC_conv_luse_RVfield_exists = False Then
            s = s & "land-use ID field name" & vbCrLf
        End If
        If txt_luse1.TextLength = 0 Then 'just look for a valid name for the output dataset
            s = s & "output land use raster name" & vbCrLf
        End If
        If s <> ("" & vbCrLf) Then
            MessageBox.Show("please enter the following data:" & s, "Some inputs are missing", MessageBoxButtons.OK)
        Else
            If GC_input_luse_exists = True And chk_WarnIfExists.Checked = True Then
                Dim MB1 = MessageBox.Show("An output dataset with the same name already exists. Do you want to replace it?", "Output land use raster already exists", MessageBoxButtons.OKCancel)
                If MB1 = Windows.Forms.DialogResult.OK Then
                    'run the script
                    RunScriptDefL()
                End If
            Else
                'run the script
                RunScriptDefL()
            End If
        End If
        'button enabled
        chk_SelDefL.Checked = False
        btn_RunConvLusetoRaster.Enabled = True
        'check existence of output
        'update text colour on all tabs if folder exists/does not exist
        GC_input_luse_exist()
        'update landuse list for combo box
        If Not GC_input_luse = "" Then
            UpdateLanduseList(GC_input_luse, GC_input_luse_exists)
        End If

    End Sub
    'runs Python Script CreateGapCrossingLayer.py (GC_Scr_DefGC) (4 arguments)
    Private Sub RunScriptDefGC()
        Dim theApplication As String
        Dim theScript As String
        Dim theArguments As String
        'Script Arguments        
        'basefolder			        GC_RootDir
        'lh_cc				        GC_input_CC
        'gapcross			        GC_input_gap_cross
        'maxdistance1 (numeric)		GC_Max_dist1

        theApplication = CheckForSpaces(GC_theApplication)
        'add path to the script name
        theScript = CheckForSpaces(GC_ScriptsFolder & "\" & GC_Scr_DefGC)
        'there are currently 4 arguments
        theArguments = " " & CheckForSpaces(GC_RootDir)
        theArguments = theArguments & " " & CheckForSpaces(GC_input_CC)
        theArguments = theArguments & " " & CheckForSpaces(GC_input_gap_cross)
        theArguments = theArguments & " " & GC_Max_dist1

        ExecutePythonScript(theApplication, theScript, theArguments)
    End Sub
    '   prepare data for CreateGapCrossingLayer.py
    Private Sub btn_CreateGapCross_Click(sender As Object, e As EventArgs) Handles btn_CreateGapCross.Click
        Dim s As String
        'button disabled so that user does not press it twice
        btn_CreateGapCross.Enabled = False
        'Script Arguments        
        'basefolder			        GC_RootDir
        'lh_cc				        GC_input_CC
        'gapcross			        GC_input_gap_cross
        'maxdistance1 (numeric)		GC_Max_dist1
        'there is probably a more efficient way to do it but will try it this way:
        s = "" & vbCrLf
        If GC_RootDir_exists = False Then
            s = s & "raster folder (Root folder)" & vbCrLf
        End If
        If GC_input_CC_exists = False Then
            s = s & "input vegetation (habitat) raster" & vbCrLf
        End If
        If GC_Max_dist1_exists = False Then '####fix this bit
            s = s & "maximum distance value" & vbCrLf
        End If
        If txt_gap_cross1.TextLength = 0 Then 'just look for a valid name for the output dataset
            s = s & "output gap cross raster name" & vbCrLf
        End If
        If s <> ("" & vbCrLf) Then
            MessageBox.Show("please enter the following data:" & s, "Some inputs are missing", MessageBoxButtons.OK)
        Else
            If GC_input_gap_cross_exists = True And chk_WarnIfExists.Checked = True Then
                Dim MB1 = MessageBox.Show("An output dataset with the same name already exists. Do you want to replace it?", "Output gap cross raster already exists", MessageBoxButtons.OKCancel)
                If MB1 = Windows.Forms.DialogResult.OK Then
                    'run the script
                    RunScriptDefGC()
                End If
            Else
                'run the script
                RunScriptDefGC()
             End If
        End If
        'button enabled
        chk_SelDefGC.Checked = False
        btn_CreateGapCross.Enabled = True
        'check existence
        GC_input_gap_cross_exist()
        'update gapcross list for combo box
        If Not GC_input_gap_cross = "" Then
            UpdateGapcrossList(GC_input_gap_cross, GC_input_gap_cross_exists)
        End If

    End Sub
    'runs Python Script CreateGapCrossingLayerOLD.py (GC_Scr_DefO) (4 arguments)
    Private Sub RunScriptDefO()
        Dim theApplication As String
        Dim theScript As String
        Dim theArguments As String
        'Script Arguments        
        'basefolder			        GC_RootDir
        'lh_cc				        GC_input_CC
        'gapcrosso			        GC_input_gap_crossO
        'maxdistance1 (numeric)		GC_CellMultiplier

        theApplication = CheckForSpaces(GC_theApplication)
        'add path to the script name
        theScript = CheckForSpaces(GC_ScriptsFolder & "\" & GC_Scr_DefO)
        'there are currently 4 arguments
        theArguments = " " & CheckForSpaces(GC_RootDir)
        theArguments = theArguments & " " & CheckForSpaces(GC_input_CC)
        theArguments = theArguments & " " & CheckForSpaces(GC_input_gap_crossO)
        theArguments = theArguments & " " & GC_CellMultiplier

        ExecutePythonScript(theApplication, theScript, theArguments)
    End Sub
    '  prepare data for CreateGapCrossingLayerOLD.py
    Private Sub btn_GapCrossOld_Click(sender As Object, e As EventArgs) Handles btn_GapCrossOld.Click
        Dim s As String
        'button disabled so that user does not press it twice
        btn_GapCrossOld.Enabled = False
        'Script Arguments        
        'basefolder			        GC_RootDir
        'lh_cc				        GC_input_CC
        'gapcrosso			        GC_input_gap_crossO
        'maxdistance1 (numeric)		GC_CellMultiplier
        'there is probably a more efficient way to do it but will try it this way:
        s = "" & vbCrLf
        If GC_RootDir_exists = False Then
            s = s & "raster folder (Root folder)" & vbCrLf
        End If
        If GC_input_CC_exists = False Then
            s = s & "vegetation (habitat) raster" & vbCrLf
        End If
        If GC_CellMultiplier_exists = False Then '####fix this bit
            s = s & "cell multiplier value" & vbCrLf
        End If
        If txt_gapcrossOld.TextLength = 0 Then 'just look for a valid name for the output dataset
            s = s & "output gap cross (old method) raster name" & vbCrLf
        End If
        If s <> ("" & vbCrLf) Then
            MessageBox.Show("please enter the following data:" & s, "Some inputs are missing", MessageBoxButtons.OK)
        Else
            If GC_input_gap_crossO_exists = True And chk_WarnIfExists.Checked = True Then
                Dim MB1 = MessageBox.Show("An output dataset with the same name already exists. Do you want to replace it?", "Output gap cross raster (old method) already exists", MessageBoxButtons.OKCancel)
                If MB1 = Windows.Forms.DialogResult.OK Then
                    'run the script
                    RunScriptDefO()
                End If
            Else
                'run the script
                RunScriptDefO()
            End If
        End If
        'button enabled
        chk_SelDefGCO.Checked = False
        btn_GapCrossOld.Enabled = True
        'update existence
        GC_input_gap_crossO_exist()
        'update gapcross list for combo box
        If Not GC_input_gap_crossO = "" Then
            UpdateGapcrossList(GC_input_gap_crossO, GC_input_gap_crossO_exists)
        End If

    End Sub
    'runs Python Script 1_CreateRasterChangeLayer.py (GC_Scr_Scen1) (5 arguments)
    Private Sub RunScriptScen1()
        Dim theApplication As String
        Dim theScript As String
        Dim theArguments As String
        '   basefolder			        GC_RootDir
        '	changelayer.shp with path   GC_input_changelyr_shp
        '   lh_cc				        GC_input_CC
        '   rastervaluefield            GC_FieldName
        '   changelayer                 GC_input_changelyr

        theApplication = CheckForSpaces(GC_theApplication)
        'add path to the script name
        theScript = CheckForSpaces(GC_ScriptsFolder & "\" & GC_Scr_Scen1)
        'there are currently 5 arguments
        theArguments = " " & CheckForSpaces(GC_RootDir)
        theArguments = theArguments & " " & CheckForSpaces(GC_input_changelyr_shp)
        theArguments = theArguments & " " & CheckForSpaces(GC_input_CC)
        theArguments = theArguments & " " & GC_FieldName
        theArguments = theArguments & " " & CheckForSpaces(GC_input_changelyr)

        ExecutePythonScript(theApplication, theScript, theArguments)
    End Sub
    Private Sub btn_RunScen1_Click(sender As Object, e As EventArgs) Handles btn_RunScen1.Click
        '   basefolder			        GC_RootDir
        '	changelayer.shp with path   GC_input_changelyr_shp
        '   lh_cc				        GC_input_CC
        '   rastervaluefield            GC_FieldName
        '   changelayer                 GC_input_changelyr
        btn_RunScen1.Enabled = False
        Dim s As String
        s = "" & vbCrLf
        If GC_RootDir_exists = False Then
            s = s & "raster folder (Root folder)" & vbCrLf
        End If
        If GC_input_changelyr_shp_exists = False Then '
            s = s & "change layer shapefile" & vbCrLf
        End If
        If GC_input_CC_exists = False Then '
            s = s & "vegetation (habitat) raster" & vbCrLf
        End If
        If GC_FieldName_exists = False Then '
            s = s & "change polygon ID field name" & vbCrLf
        End If
        If txt_ChangerasterTab2.TextLength = 0 Then 'just look for a valid name for the output dataset
            s = s & "output change raster name" & vbCrLf
        End If
        If s <> ("" & vbCrLf) Then
            MessageBox.Show("please enter the following data:" & s, "Some inputs are missing", MessageBoxButtons.OK)
        Else
            If GC_input_changelyr_exists = True And chk_WarnIfExists.Checked = True Then
                Dim MB1 = MessageBox.Show("An output dataset with the same name already exists. Do you want to replace it?", "Output change raster (Scenario 1) already exists", MessageBoxButtons.OKCancel)
                If MB1 = Windows.Forms.DialogResult.OK Then
                    'run the script
                    RunScriptScen1()
                End If
            Else
                'run the script
                RunScriptScen1()
            End If
        End If
        'button enabled
        chk_selS1.Checked = False
        btn_RunScen1.Enabled = True
        'update existence
        GC_input_changelyr_exist()
    End Sub
    'runs Python Script 2a_ModifyVegetationLayer_Removal.py (GC_Scr_Scen2a) (4 arguments)
    Private Sub RunScriptScen2a()
        Dim theApplication As String
        Dim theScript As String
        Dim theArguments As String
        '   basefolder		GC_RootDir
        '   changelayer     GC_input_changelyr
        '   lh_cc			GC_input_CC
        '   lh_ccrem		GC_veg_removal

        theApplication = CheckForSpaces(GC_theApplication)
        'add path to the script name
        theScript = CheckForSpaces(GC_ScriptsFolder & "\" & GC_Scr_Scen2a)
        'there are currently 4 arguments
        theArguments = " " & CheckForSpaces(GC_RootDir)
        theArguments = theArguments & " " & CheckForSpaces(GC_input_changelyr)
        theArguments = theArguments & " " & CheckForSpaces(GC_input_CC)
        theArguments = theArguments & " " & CheckForSpaces(GC_veg_removal)

        ExecutePythonScript(theApplication, theScript, theArguments)
    End Sub
    Private Sub btn_RunScen2a_Click(sender As Object, e As EventArgs) Handles btn_RunScen2a.Click
        '   basefolder		GC_RootDir
        '   changelayer     GC_input_changelyr
        '   lh_cc			GC_input_CC
        '   lh_ccrem		GC_veg_removal
        btn_RunScen2a.Enabled = False
        Dim s As String
        s = "" & vbCrLf
        If GC_RootDir_exists = False Then
            s = s & "raster folder (Root folder)" & vbCrLf
        End If
        If GC_input_changelyr_exists = False Then '
            s = s & "change raster" & vbCrLf
        End If
        If GC_input_CC_exists = False Then '
            s = s & "vegetation (habitat) raster" & vbCrLf
        End If
        If txt_NewRasterRemv.TextLength = 0 Then 'just look for a valid name for the output dataset
            s = s & "output modified vegetation (removal) raster name" & vbCrLf
        End If
        If s <> ("" & vbCrLf) Then
            MessageBox.Show("please enter the following data:" & s, "Some inputs are missing", MessageBoxButtons.OK)
        Else
            If GC_veg_removal_exists = True And chk_WarnIfExists.Checked = True Then
                Dim MB1 = MessageBox.Show("An output dataset with the same name already exists. Do you want to replace it?", "Output modified vegetation (removal) raster (Scenario 2a) already exists", MessageBoxButtons.OKCancel)
                If MB1 = Windows.Forms.DialogResult.OK Then
                    'run the script
                    RunScriptScen2a()
                End If
            Else
                'run the script
                RunScriptScen2a()
            End If
        End If
        'button enabled
        chk_selS2a.Checked = False
        btn_RunScen2a.Enabled = True
        'update existence
        GC_veg_removal_exist()
        'update veg list for combo box
        UpdateVegList(GC_veg_removal, GC_veg_removal_exists)

    End Sub
    'runs Python Script 2b_ModifyVegetationLayer_Addition.py (GC_Scr_Scen2b) (6 arguments)
    Private Sub RunScriptScen2b()
        Dim theApplication As String
        Dim theScript As String
        Dim theArguments As String
        '   basefolder			            GC_RootDir
        '   changelayer                     GC_input_changelyr
        '   lh_cc				            GC_input_CC
        '   luse				            GC_input_luse
        '   HydrologyValue1 (numeric, 10)   GC_HydrologyValue1
        '	lh_ccadd			            GC_veg_addition
        theApplication = CheckForSpaces(GC_theApplication)
        'add path to the script name
        theScript = CheckForSpaces(GC_ScriptsFolder & "\" & GC_Scr_Scen2b)
        'there are currently 6 arguments
        theArguments = " " & CheckForSpaces(GC_RootDir)
        theArguments = theArguments & " " & CheckForSpaces(GC_input_changelyr)
        theArguments = theArguments & " " & CheckForSpaces(GC_input_CC)
        theArguments = theArguments & " " & CheckForSpaces(GC_input_luse)
        theArguments = theArguments & " " & GC_HydrologyValue1
        theArguments = theArguments & " " & CheckForSpaces(GC_veg_addition)

        ExecutePythonScript(theApplication, theScript, theArguments)
    End Sub
    Private Sub btn_RunScen2b_Click(sender As Object, e As EventArgs) Handles btn_RunScen2b.Click
        '   basefolder			            GC_RootDir
        '   changelayer                     GC_input_changelyr
        '   lh_cc				            GC_input_CC
        '   luse				            GC_input_luse
        '   HydrologyValue1 (numeric, 10)   GC_HydrologyValue1
        '	lh_ccadd			            GC_veg_addition
        btn_RunScen2b.Enabled = False
        Dim s As String
        s = "" & vbCrLf
        If GC_RootDir_exists = False Then
            s = s & "raster folder (Root folder)" & vbCrLf
        End If
        If GC_input_changelyr_exists = False Then '
            s = s & "change raster" & vbCrLf
        End If
        If GC_input_CC_exists = False Then '
            s = s & "vegetation (habitat) raster" & vbCrLf
        End If
        If GC_input_luse_exists = False Then '
            s = s & "land-use raster" & vbCrLf
        End If
        If GC_HydrologyValue1_exists = False Then '
            s = s & "hydrology value 1" & vbCrLf
        End If

        If txt_NewRastAdd.TextLength = 0 Then 'just look for a valid name for the output dataset
            s = s & "output modified vegetation (addition) raster name" & vbCrLf
        End If
        If s <> ("" & vbCrLf) Then
            MessageBox.Show("please enter the following data:" & s, "Some inputs are missing", MessageBoxButtons.OK)
        Else
            If GC_veg_addition_exists = True And chk_WarnIfExists.Checked = True Then
                Dim MB1 = MessageBox.Show("An output dataset with the same name already exists. Do you want to replace it?", "Output modified vegetation (addition) raster (Scenario 2b) already exists", MessageBoxButtons.OKCancel)
                If MB1 = Windows.Forms.DialogResult.OK Then
                    'run the script
                    RunScriptScen2b()
                End If
            Else
                'run the script
                RunScriptScen2b()
            End If
        End If
        'button enabled
        chk_sel2b.Checked = False
        btn_RunScen2b.Enabled = True
        'update existence
        GC_veg_addition_exist()
        'update veg list for combo box
        UpdateVegList(GC_veg_addition, GC_veg_addition_exists)

    End Sub
    'runs Python Script 3_CreateGapCrossingLayer.py (GC_Scr_Scen3) (4 arguments)
    Private Sub RunScriptScen3()
        Dim theApplication As String
        Dim theScript As String
        Dim theArguments As String
        '   basefolder			           GC_RootDir
        '   lh_ccrem			           GC_veg_removal
        '   gap_crossadd                   GC_gapcross_add
        '   InputCellMultiplier2 (numeric) GC_CellMultiplier2
        theApplication = CheckForSpaces(GC_theApplication)
        'add path to the script name
        theScript = CheckForSpaces(GC_ScriptsFolder & "\" & GC_Scr_Scen3)
        'there are currently 4 arguments
        theArguments = " " & CheckForSpaces(GC_RootDir)
        theArguments = theArguments & " " & CheckForSpaces(GC_veg_removal)
        theArguments = theArguments & " " & CheckForSpaces(GC_gapcross_add)
        theArguments = theArguments & " " & GC_CellMultiplier2

        ExecutePythonScript(theApplication, theScript, theArguments)
    End Sub
    Private Sub btn_RunScen3_Click(sender As Object, e As EventArgs) Handles btn_RunScen3.Click
        '   basefolder			           GC_RootDir
        '   lh_ccrem			           GC_veg_removal
        '   gap_crossadd                   GC_gapcross_add
        '   InputCellMultiplier2 (numeric) GC_CellMultiplier2
        btn_RunScen3.Enabled = False
        Dim s As String
        s = "" & vbCrLf
        If GC_RootDir_exists = False Then
            s = s & "raster folder (Root folder)" & vbCrLf
        End If
        If GC_veg_removal_exists = False Then '
            s = s & "modified vegetation (removal) raster" & vbCrLf
        End If
        If GC_CellMultiplier2_exists = False Then '
            s = s & "cell multiplier value" & vbCrLf
        End If

        If txt_GapcrossTab2.TextLength = 0 Then 'just look for a valid name for the output dataset
            s = s & "output gap cross (scenario) raster name" & vbCrLf
        End If
        If s <> ("" & vbCrLf) Then
            MessageBox.Show("please enter the following data:" & s, "Some inputs are missing", MessageBoxButtons.OK)
        Else
            If GC_gapcross_add_exists = True And chk_WarnIfExists.Checked = True Then
                Dim MB1 = MessageBox.Show("An output dataset with the same name already exists. Do you want to replace it?", "Output  gapp cross (addition) raster (Scenario 3) already exists", MessageBoxButtons.OKCancel)
                If MB1 = Windows.Forms.DialogResult.OK Then
                    'run the script
                    RunScriptScen3()
                End If
            Else
                'run the script
                RunScriptScen3()
            End If
        End If
        'button enabled
        chk_sel3.Checked = False
        btn_RunScen3.Enabled = True
        'update existence
        GC_gapcross_add_exist()
        'update gapcross list for combo box
        If Not GC_gapcross_add = "" Then
            UpdateGapcrossList(GC_gapcross_add, GC_gapcross_add_exists)
        End If


    End Sub
    'runsPython Script 4_ModifyLuseLayer_AddInfra.py (GC_Scr_Scen4) (5 arguments)
    Private Sub RunScriptScen4()
        Dim theApplication As String
        Dim theScript As String
        Dim theArguments As String
        '   basefolder		               GC_RootDir
        '   changelayer                    GC_input_changelyr
        '   luse_add                       GC_luse_add
        '   HydrologyValue2 (numeric, 30)  GC_HydrologyValue2 (passed as string)
        '   luse				           GC_input_luse
        theApplication = CheckForSpaces(GC_theApplication)
        'add path to the script name
        theScript = CheckForSpaces(GC_ScriptsFolder & "\" & GC_Scr_Scen4)
        'there are currently 4 arguments
        theArguments = " " & CheckForSpaces(GC_RootDir)
        theArguments = theArguments & " " & CheckForSpaces(GC_input_changelyr)
        theArguments = theArguments & " " & CheckForSpaces(GC_luse_add)
        theArguments = theArguments & " " & GC_HydrologyValue2
        theArguments = theArguments & " " & CheckForSpaces(GC_input_luse)

        ExecutePythonScript(theApplication, theScript, theArguments)
    End Sub
    Private Sub btn_RunScen4_Click(sender As Object, e As EventArgs) Handles btn_RunScen4.Click
        '   basefolder		               GC_RootDir
        '   changelayer                    GC_input_changelyr
        '   luse_add                       GC_luse_add
        '   HydrologyValue2 (numeric, 30)  GC_HydrologyValue2 (passed as string)
        '   luse				           GC_input_luse
        btn_RunScen4.Enabled = False
        Dim s As String
        s = "" & vbCrLf
        If GC_RootDir_exists = False Then
            s = s & "raster folder (Root folder)" & vbCrLf
        End If
        If GC_input_changelyr_exists = False Then '
            s = s & "change raster" & vbCrLf
        End If
        If GC_input_luse_exists = False Then '
            s = s & "land use raster" & vbCrLf
        End If
        If GC_HydrologyValue2_exists = False Then '
            s = s & "hydrology value 2" & vbCrLf
        End If

        If txt_infrastructure.TextLength = 0 Then 'just look for a valid name for the output dataset
            s = s & "output modified land use (infrastructure) raster name" & vbCrLf
        End If
        If s <> ("" & vbCrLf) Then
            MessageBox.Show("please enter the following data:" & s, "Some inputs are missing", MessageBoxButtons.OK)
        Else
            If GC_luse_add_exists = True And chk_WarnIfExists.Checked = True Then
                Dim MB1 = MessageBox.Show("An output dataset with the same name already exists. Do you want to replace it?", "Output modified land use (infrastructure) raster (Scenario 4) already exists", MessageBoxButtons.OKCancel)
                If MB1 = Windows.Forms.DialogResult.OK Then
                    'run the script
                    RunScriptScen4()
                End If
            Else
                'run the script
                RunScriptScen4()
            End If
        End If
        'button enabled
        chk_selS4.Checked = False
        btn_RunScen4.Enabled = True
        'update existence
        GC_luse_add_exist()
        'update landuse list for combo box
        If Not GC_luse_add = "" Then
            UpdateLanduseList(GC_luse_add, GC_luse_add_exists)
        End If

    End Sub

    'runsPython Script ConvCostLayerToCirc_GC.py (GC_ScriptCirc1) (6 arguments)
    Private Sub RunScriptCirc1()
        Dim theApplication As String
        Dim theScript As String
        Dim theArguments As String
        ' list variables in order and GC names
        '#   <Output_Folder (string)> GC_CC_OutFolder
        '#   <luse_4fin_tif (string)>  GC_CC_luse_4fin
        '#   <Test_Small_Extent_shp (string)>  GC_CC_TestExt_shp
        '#   <costascii_asc (string)>  GC_CC_CostAscii
        '#   <Output__New_Veg_Layer (string)>  GC_CC_OutpNV_Ras
        '#   <InfinteCostValue (string)>  GC_CC_InfiniteCostV

        theApplication = CheckForSpaces(GC_theApplication)
        'add path to the script name
        theScript = CheckForSpaces(GC_ScriptsFolder & "\" & GC_ScriptCirc1)
        'there are currently 6 arguments
        theArguments = " " & CheckForSpaces(GC_CC_OutFolder)
        theArguments = theArguments & " " & CheckForSpaces(GC_CC_luse_4fin)
        theArguments = theArguments & " " & CheckForSpaces(GC_CC_TestExt_shp)
        theArguments = theArguments & " " & CheckForSpaces(GC_CC_CostAscii)
        theArguments = theArguments & " " & CheckForSpaces(GC_CC_OutpNV_Ras)
        theArguments = theArguments & " " & GC_CC_InfiniteCostV

        ExecutePythonScript(theApplication, theScript, theArguments)
    End Sub
    Private Sub btn_RunCirc1_Click(sender As Object, e As EventArgs) Handles btn_RunCirc1.Click
        '#   <Output_Folder (string)> GC_CC_OutFolder
        '#   <luse_4fin_tif (string)>  GC_CC_luse_4fin
        '#   <Test_Small_Extent_shp (string)>  GC_CC_TestExt_shp
        '#   <costascii_asc (string)>  GC_CC_CostAscii
        '#   <Output__New_Veg_Layer (string)>  GC_CC_OutpNV_Ras
        '#   <InfinteCostValue (string)>  GC_CC_InfiniteCostV
        btn_RunCirc1.Enabled = False
        Dim s As String
        s = "" & vbCrLf
        If GC_CC_OutFolder_exists = False Then
            s = s & "output folder (Circuitscape)" & vbCrLf
        End If
        If GC_CC_luse_4fin_exists = False Then '
            s = s & "input Graphab resistance tif file (land use)" & vbCrLf
        End If
        If GC_CC_TestExt_shp_exists = False Then '
            s = s & "extent shapefile" & vbCrLf
        End If

        If GC_CC_CostAscii = Nothing Or Microsoft.VisualBasic.Right(GC_CC_CostAscii, 4) <> ".asc" Then 'just look for a valid name for the output dataset '
            'also checking for the asc extension
            s = s & "output resistance ascii file name" & vbCrLf
        End If
        If GC_CC_OutpNV_Ras = Nothing Then 'just look for a valid name for the output dataset
            s = s & "output resistance raster name" & vbCrLf
        End If
        If GC_CC_InfiniteCostV_exists = False Then '
            s = s & "infinite cost value" & vbCrLf
        End If
        If s <> ("" & vbCrLf) Then
            MessageBox.Show("please enter the following data:" & s, "Some inputs are missing", MessageBoxButtons.OK)
        Else
            If GC_CC_OutpNV_Ras_exists = True And chk_WarnIfExists.Checked = True Then
                Dim MB1 = MessageBox.Show("Output datasets with the same name already exist. Do you want to replace them?", "output resistance raster already exists", MessageBoxButtons.OKCancel)
                If MB1 = Windows.Forms.DialogResult.OK Then
                    'run the script
                    RunScriptCirc1()
                End If
            Else
                'run the script
                RunScriptCirc1()
            End If
        End If
        'button enabled
        chk_Circ1.Checked = False
        btn_RunCirc1.Enabled = True
        'update existence (GC_CC_OutpNV_Ras)
        GC_CC_OutpNV_Ras_exist()
        'update existence (GC_CC_CostAscii)
        GC_CC_CostAscii_exist()
    End Sub
    'runsPython Script ConvGraphabPatchToCirc_GC.py (GC_ScriptCirc2) (6 arguments)
    Private Sub RunScriptCirc2()
        Dim theApplication As String
        Dim theScript As String
        Dim theArguments As String
        ' list variables in order and GC names
        '#  <Output_Folder (string)>  GC_CC_OutFolder
        '#  <patches_tif (string)>  GC_CC_patches_tif
        '#  <Test_Small_Extent_shp (string)>  GC_CC_TestExt_shp
        '"CircFocalNodesPatchAsciiFile=" & GC_CC_PatchAscii)
        '"CircFocalNodesPatchRaster=" & GC_CC_PatchCirc_R)


        theApplication = CheckForSpaces(GC_theApplication)
        'add path to the script name
        theScript = CheckForSpaces(GC_ScriptsFolder & "\" & GC_ScriptCirc2)
        'there are now 5 arguments
        theArguments = " " & CheckForSpaces(GC_CC_OutFolder)
        theArguments = theArguments & " " & CheckForSpaces(GC_CC_patches_tif)
        theArguments = theArguments & " " & CheckForSpaces(GC_CC_TestExt_shp)
        theArguments = theArguments & " " & CheckForSpaces(GC_CC_PatchAscii)
        theArguments = theArguments & " " & CheckForSpaces(GC_CC_PatchCirc_R)

        ExecutePythonScript(theApplication, theScript, theArguments)
    End Sub
    Private Sub btn_RunCirc2_Click(sender As Object, e As EventArgs) Handles btn_RunCirc2.Click
        '#  <Output_Folder (string)>  GC_CC_OutFolder
        '#  <patches_tif (string)>  GC_CC_patches_tif
        '#  <Test_Small_Extent_shp (string)>  GC_CC_TestExt_shp
        '"CircFocalNodesPatchAsciiFile=" & GC_CC_PatchAscii)
        '"CircFocalNodesPatchRaster=" & GC_CC_PatchCirc_R)

        btn_RunCirc2.Enabled = False
        Dim s As String
        s = "" & vbCrLf
        If GC_CC_OutFolder_exists = False Then
            s = s & "output folder (Circuitscape)" & vbCrLf
        End If
        If GC_CC_patches_tif_exists = False Then '
            s = s & "input Grahpab patches.tif file" & vbCrLf
        End If
        If GC_CC_TestExt_shp_exists = False Then '
            s = s & "extent shapefile" & vbCrLf
        End If

        If GC_CC_PatchAscii = Nothing Or Microsoft.VisualBasic.Right(GC_CC_PatchAscii, 4) <> ".asc" Then 'just look for a valid name for the output dataset '
            'also checking for the asc extension
            s = s & "output Focal Nodes Patch ascii file name" & vbCrLf
        End If
        If GC_CC_PatchCirc_R = Nothing Then 'just look for a valid name for the output dataset
            s = s & "output Focal Nodes Patch raster name" & vbCrLf
        End If
        If s <> ("" & vbCrLf) Then
            MessageBox.Show("please enter the following data:" & s, "Some inputs are missing", MessageBoxButtons.OK)
        Else
            If GC_CC_PatchCirc_R_exists = True And chk_WarnIfExists.Checked = True Then
                Dim MB1 = MessageBox.Show("Output datasets with the same name already exist. Do you want to replace them?", "output Focal Nodes Patch raster already exists", MessageBoxButtons.OKCancel)
                If MB1 = Windows.Forms.DialogResult.OK Then
                    'run the script
                    RunScriptCirc2()
                End If
            Else
                'run the script
                RunScriptCirc2()
            End If
        End If
        'button enabled
        chk_Circ2.Checked = False
        btn_RunCirc2.Enabled = True
        'update existence (GC_CC_PatchTemp_R)
        'GC_CC_PatchTemp_R_exist()
        'update existence (GC_CC_PatchCirc_R)
        GC_CC_PatchCirc_R_exist()
        'update existence (GC_CC_PatchAscii)
        GC_CC_PatchAscii_exist()
    End Sub
    'runsPython Script ConvGraphabPatchesAndLabelToCirc_GC.py (GC_ScriptCirc3) (7 arguments)
    Private Sub RunScriptCirc3()
        Dim theApplication As String
        Dim theScript As String
        Dim theArguments As String
        ' list variables in order and GC names
        '#  <Output_Folder (string)>  GC_CC_OutFolder
        '#  <patches_tif (string)>  GC_CC_patches_tif
        '#  <Test_Small_Extent_shp (string)>  GC_CC_TestExt_shp
        '#  <Components_shp (string)>  GC_CC_Comp_shp
        '"CircFocalNodesCompAsciiFile=" & GC_CC_FocalNodesComp_asc)
        '"CircFocalNodesCompRaster=" & GC_CC_FocalNodesComp_R)

        theApplication = CheckForSpaces(GC_theApplication)
        'add path to the script name
        theScript = CheckForSpaces(GC_ScriptsFolder & "\" & GC_ScriptCirc3)
        'there are now 6 arguments
        theArguments = " " & CheckForSpaces(GC_CC_OutFolder)
        theArguments = theArguments & " " & CheckForSpaces(GC_CC_patches_tif)
        theArguments = theArguments & " " & CheckForSpaces(GC_CC_TestExt_shp)
        theArguments = theArguments & " " & CheckForSpaces(GC_CC_Comp_shp)
        theArguments = theArguments & " " & CheckForSpaces(GC_CC_FocalNodesComp_asc)
        theArguments = theArguments & " " & CheckForSpaces(GC_CC_FocalNodesComp_R)
 
        ExecutePythonScript(theApplication, theScript, theArguments)
    End Sub
    Private Sub btn_RunCirc3_Click(sender As Object, e As EventArgs) Handles btn_RunCirc3.Click
        '#  <Output_Folder (string)>  GC_CC_OutFolder
        '#  <patches_tif (string)>  GC_CC_patches_tif
        '#  <Test_Small_Extent_shp (string)>  GC_CC_TestExt_shp
        '#  <Components_shp (string)>  GC_CC_Comp_shp
        '"CircFocalNodesCompAsciiFile=" & GC_CC_FocalNodesComp_asc)
        '"CircFocalNodesCompRaster=" & GC_CC_FocalNodesComp_R)

        btn_RunCirc3.Enabled = False
        Dim s As String
        s = "" & vbCrLf
        If GC_CC_OutFolder_exists = False Then
            s = s & "output folder (Circuitscape)" & vbCrLf
        End If
        If GC_CC_patches_tif_exists = False Then '
            s = s & "input Graphab patches tif file" & vbCrLf
        End If
        If GC_CC_TestExt_shp_exists = False Then '
            s = s & "extent shapefile" & vbCrLf
        End If
        If GC_CC_Comp_shp_exists = False Then '
            s = s & "input components shapefile" & vbCrLf
        End If

        If GC_CC_FocalNodesComp_asc = Nothing Or Microsoft.VisualBasic.Right(GC_CC_FocalNodesComp_asc, 4) <> ".asc" Then 'just look for a valid name for the output dataset '
            'also maybe worth checking for the asc extension
            s = s & "output Focal Nodes Comp. ascii file name" & vbCrLf
        End If
        If GC_CC_FocalNodesComp_R = Nothing Then 'just look for a valid name for the output dataset
            s = s & "output Focal Nodes Comp raster name" & vbCrLf
        End If
        If s <> ("" & vbCrLf) Then
            MessageBox.Show("please enter the following data:" & s, "Some inputs are missing", MessageBoxButtons.OK)
        Else
            If GC_CC_FocalNodesComp_R_exists = True And chk_WarnIfExists.Checked = True Then
                Dim MB1 = MessageBox.Show("Output datasets with the same name already exist. Do you want to replace them?", "output Focal Nodes Comp raster already exists", MessageBoxButtons.OKCancel)
                If MB1 = Windows.Forms.DialogResult.OK Then
                    'run the script
                    RunScriptCirc3()
                End If
            Else
                'run the script
                RunScriptCirc3()
            End If
        End If
        'button enabled
        chk_Circ3.Checked = False
        btn_RunCirc3.Enabled = True
        'update existence (GC_CC_PatchTemp_R)
        GC_CC_FocalNodesComp_R_exist()
        'update existence (GC_CC_PatchCirc_R)
        'GC_CC_PatchCirc_R_exist()
        'update existence (GC_CC_PatchAscii)
        GC_CC_FocalNodesComp_asc_exist()
    End Sub

    Private Function CheckForSpaces(S1 As String) As String
        'if the string has spaces, then check for quotes
        '    then if no quotes add them
        'Chr(34) = '"'character (added to path string in case it contains spaces which would interfere with script execution)
        Dim S2 As String
        If S1.Contains(" ") Then
            If Microsoft.VisualBasic.Left(S1, 1) = Chr(34) And Microsoft.VisualBasic.Right(S1, 1) = Chr(34) And Len(S1) > 3 Then
                S2 = S1
            Else
                S2 = Chr(34) & S1 & Chr(34)
            End If
            Return S2

        Else 'otherwise return the original string
            Return S1
        End If
    End Function

#End Region

#Region "Tab 1 data entry controls"
    'input landuse.shp (GC_input_luse_shp)
    Private Sub txt_LanduseShp_Enter(sender As Object, e As EventArgs) Handles txt_LanduseShp.Enter
        txt_LanduseShp.ForeColor = Color.Blue
    End Sub
    Private Sub GC_input_luse_shp_exist()
        If (GC_input_luse_shp <> "") And My.Computer.FileSystem.FileExists(GC_input_luse_shp) Then
            GC_input_luse_shp_exists = True
            txt_LanduseShp.ForeColor = Color.Black
        Else
            GC_input_luse_shp_exists = False
            txt_LanduseShp.ForeColor = Color.Red
        End If
    End Sub
    Private Sub txt_LanduseShp_Leave(sender As Object, e As EventArgs) Handles txt_LanduseShp.Leave
        Try
            GC_input_luse_shp = txt_LanduseShp.Text
            GC_input_luse_shp_exist()
        Catch ex As Exception
            MsgBox("Error in selecting land use shapefile name.")
        End Try
    End Sub
    Private Sub btn_LanduseShp_Click(sender As Object, e As EventArgs) Handles btn_LanduseShp.Click
        Dim temp5 As String = txt_LanduseShp.Text
        If GC_LastFolder <> "" Then
            dlg_OpenShapefile.InitialDirectory = GC_LastFolder
        End If
        Try
            dlg_OpenShapefile.Title = "Select land use shapefile"
            dlg_OpenShapefile.FileName = ""
            Dim dlgb = dlg_OpenShapefile.ShowDialog() ' find file dialog to find or set scenario file name

            If dlgb = Windows.Forms.DialogResult.OK Then
                txt_LanduseShp.Text = dlg_OpenShapefile.FileName
                txt_LanduseShp_Leave(Me, Nothing)
                'save the path to this folder
                GC_LastFolder = IO.Path.GetDirectoryName(GC_input_luse_shp)
            ElseIf dlgb = Windows.Forms.DialogResult.Cancel Then
                txt_LanduseShp.Text = temp5
            End If
        Catch ex As Exception
            MsgBox("Error in selecting land use shapefile") ': {0}", ex.ToString())
        End Try
    End Sub

    'input 'input folder' (GC_RootDir)
    Private Sub txt_InputF_Enter(sender As Object, e As EventArgs) Handles txt_InputF.Enter
        txt_InputF.ForeColor = Color.Blue
    End Sub
    'updates txt_InputF (tab 1), txt_rasterFolderTab2 (tab 2) and txt_SelRootDir (graphab tab)
    Private Sub GC_RootDir_exist()
        If (GC_RootDir <> "") And My.Computer.FileSystem.DirectoryExists(GC_RootDir) Then
            GC_RootDir_exists = True
            txt_InputF.ForeColor = Color.Black
            txt_rasterFolderTab2.ForeColor = Color.Black
            txt_SelRootDir.ForeColor = Color.Black
        Else
            GC_RootDir_exists = False
            txt_InputF.ForeColor = Color.Red
            txt_rasterFolderTab2.ForeColor = Color.Red
            txt_SelRootDir.ForeColor = Color.Red
        End If
    End Sub
    Private Sub txt_InputF_Leave(sender As Object, e As EventArgs) Handles txt_InputF.Leave
        Try
            If txt_InputF.Text <> "" Then
                GC_RootDir = txt_InputF.Text
                txt_rasterFolderTab2.Text = GC_RootDir 'update to tab 2
                txt_SelRootDir.Text = GC_RootDir 'update to tab 3
                GC_RootDir_exist()
                'update raster existence, colors and combo box lists
                RasterFolderLeave()
            End If
        Catch ex As Exception
            MsgBox("Error in setting raster folder.")
        End Try
    End Sub
    Private Sub btn_InputF_Click(sender As Object, e As EventArgs) Handles btn_InputF.Click
        Dim tempText As String = txt_InputF.Text
        If GC_LastFolder <> "" Then
            dlg_GetRootFolder.SelectedPath = GC_LastFolder
        End If
        Try
            dlg_GetRootFolder.Description = "Select raster root folder"
            Dim dlgc = dlg_GetRootFolder.ShowDialog()

            If dlgc = Windows.Forms.DialogResult.OK Then
                txt_InputF.Text = dlg_GetRootFolder.SelectedPath
                GC_LastFolder = dlg_GetRootFolder.SelectedPath
                txt_InputF_Leave(Me, Nothing)
            ElseIf dlgc = Windows.Forms.DialogResult.Cancel Then
                txt_InputF.Text = tempText
            End If
        Catch ex As Exception
            MsgBox("Error in selecting the raster folder") ': {0}", ex.ToString())
        End Try
    End Sub

    'enter text box - tab 1 (GC_input_CC)
    Private Sub txt_inplh_cc1_Enter(sender As Object, e As EventArgs) Handles txt_inplh_cc1.Enter
        txt_inplh_cc1.ForeColor = Color.Blue
    End Sub
    'updates txt_inplh_cc1 (tab 1) and txt_habitatTab2 (tab2)
    Private Sub GC_input_CC_exist()

        If (GC_input_CC <> "") And My.Computer.FileSystem.DirectoryExists(GC_RootDir & "\" & GC_input_CC) Then
            GC_input_CC_exists = True
            txt_inplh_cc1.ForeColor = Color.Black
            txt_habitatTab2.ForeColor = Color.Black
        ElseIf IsValidGrid(GC_input_CC) = False Then
            GC_input_CC_exists = False
            txt_inplh_cc1.ForeColor = Color.Violet
            txt_habitatTab2.ForeColor = Color.Violet
        Else
            GC_input_CC_exists = False
            txt_inplh_cc1.ForeColor = Color.Red
            txt_habitatTab2.ForeColor = Color.Red
        End If
    End Sub
    Private Sub txt_inplh_cc1_Leave(sender As Object, e As EventArgs) Handles txt_inplh_cc1.Leave

        Try
            GC_input_CC = txt_inplh_cc1.Text
            'copy to other tabs
            txt_habitatTab2.Text = txt_inplh_cc1.Text 'update to tab 2 (lh_cc)
            'update text colour on all tabs if folder exists/does not exist
            GC_input_CC_exist()
            'update veg list for combo box
            If Not GC_input_CC = "" Then
                UpdateVegList(GC_input_CC, GC_input_CC_exists)
            End If

        Catch ex As Exception
            MsgBox("Error in entering habitat raster name.")
        End Try
    End Sub
    Private Sub btn_BrHabRasTab1_Click(sender As Object, e As EventArgs) Handles btn_BrHabRasTab1.Click
        Try
            'sets the initial selected directory root directory if that has been previously selected
            If GC_RootDir <> "" Then
                dlg_GetRootFolder.SelectedPath = GC_RootDir
            End If
            dlg_GetRootFolder.Description = "Select habitat raster (folder) within raster root folder"
            Dim dlgc = dlg_GetRootFolder.ShowDialog()
            If dlgc = Windows.Forms.DialogResult.OK Then
                txt_inplh_cc1.Text = dlg_GetRootFolder.SelectedPath.Split(IO.Path.DirectorySeparatorChar).Last() 'get last part only
                txt_inplh_cc1_Leave(Me, Nothing)
            End If
        Catch ex As Exception
            MsgBox("Error in selecting the raster") ': {0}", ex.ToString())
        End Try
    End Sub

    'output landuse raster (GC_input_luse)
    Private Sub txt_luse1_Enter(sender As Object, e As EventArgs) Handles txt_luse1.Enter
        txt_luse1.ForeColor = Color.Blue
    End Sub
    'updates txt_luse1 (tab 1) and txt_landuseTab2 (tab 2)
    Private Sub GC_input_luse_exist()
        If (GC_input_luse <> "") And My.Computer.FileSystem.DirectoryExists(GC_RootDir & "\" & GC_input_luse) Then
            GC_input_luse_exists = True
            txt_landuseTab2.ForeColor = Color.Black
            txt_luse1.ForeColor = Color.Black
        ElseIf IsValidGrid(GC_input_luse) = False Then
            GC_input_luse_exists = False
            txt_landuseTab2.ForeColor = Color.Violet
            txt_luse1.ForeColor = Color.Violet
        Else
            GC_input_luse_exists = False
            txt_landuseTab2.ForeColor = Color.Red
            txt_luse1.ForeColor = Color.Red
        End If
    End Sub
    Private Sub txt_luse1_Leave(sender As Object, e As EventArgs) Handles txt_luse1.Leave
        Try
            GC_input_luse = txt_luse1.Text
            'copy to other tabs
            txt_landuseTab2.Text = txt_luse1.Text 'update to tab 1 (luse)
            'update text colour on all tabs if folder exists/does not exist
            GC_input_luse_exist()
            'update landuse list for combo box
            If Not GC_input_luse = "" Then
                UpdateLanduseList(GC_input_luse, GC_input_luse_exists)
            End If

        Catch ex As Exception
            MsgBox("Error in entering land use raster name.")
        End Try
    End Sub
    Private Sub btn_BrLuRasTab1_Click(sender As Object, e As EventArgs) Handles btn_BrLuRasTab1.Click
        Try
            If GC_RootDir <> "" Then
                dlg_GetRootFolder.SelectedPath = GC_RootDir
            End If
            dlg_GetRootFolder.Description = "Select land use raster (folder) within raster root folder"
            Dim dlgc = dlg_GetRootFolder.ShowDialog()
            If dlgc = Windows.Forms.DialogResult.OK Then
                txt_luse1.Text = dlg_GetRootFolder.SelectedPath.Split(IO.Path.DirectorySeparatorChar).Last() 'get last part only
                txt_luse1_Leave(Me, Nothing)
            End If
        Catch ex As Exception
            MsgBox("Error in selecting the raster") ': {0}", ex.ToString())
        End Try
    End Sub

    'output gapcross default raster (GC_input_gap_cross)
    Private Sub txt_gap_cross1_Enter(sender As Object, e As EventArgs) Handles txt_gap_cross1.Enter
        txt_gap_cross1.ForeColor = Color.Blue
    End Sub
    Private Sub GC_input_gap_cross_exist()
        If (GC_input_gap_cross <> "") And My.Computer.FileSystem.DirectoryExists(GC_RootDir & "\" & GC_input_gap_cross) Then
            GC_input_gap_cross_exists = True
            txt_gap_cross1.ForeColor = Color.Black
        ElseIf IsValidGrid(GC_input_gap_cross) = False Then
            GC_input_gap_cross_exists = False
            txt_gap_cross1.ForeColor = Color.Violet
        Else
            GC_input_gap_cross_exists = False
            txt_gap_cross1.ForeColor = Color.Red
        End If
    End Sub
    Private Sub txt_gap_cross1_Leave(sender As Object, e As EventArgs) Handles txt_gap_cross1.Leave
        Try
            GC_input_gap_cross = txt_gap_cross1.Text
            GC_input_gap_cross_exist()
            'update gapcross list for combo box
            If Not GC_input_gap_cross = "" Then
                UpdateGapcrossList(GC_input_gap_cross, GC_input_gap_cross_exists)
            End If

        Catch ex As Exception
            MsgBox("error in entering gappcross raster name.")
        End Try
    End Sub
    Private Sub btn_BrGcRasTab1_Click(sender As Object, e As EventArgs) Handles btn_BrGcRasTab1.Click
        Try
            If GC_RootDir <> "" Then
                dlg_GetRootFolder.SelectedPath = GC_RootDir
            End If
            dlg_GetRootFolder.Description = "Select gap-cross raster (folder) within raster root folder"
            Dim dlgc = dlg_GetRootFolder.ShowDialog()
            If dlgc = Windows.Forms.DialogResult.OK Then
                txt_gap_cross1.Text = dlg_GetRootFolder.SelectedPath.Split(IO.Path.DirectorySeparatorChar).Last() 'get last part only
                txt_gap_cross1_Leave(Me, Nothing)
            End If
        Catch ex As Exception
            MsgBox("Error in selecting the raster")
        End Try
    End Sub

    'output gapcross default-oldmethod raster
    Private Sub txt_gapcrossOld_Enter(sender As Object, e As EventArgs) Handles txt_gapcrossOld.Enter
        txt_gapcrossOld.ForeColor = Color.Blue
    End Sub
    Private Sub GC_input_gap_crossO_exist()
        If (GC_input_gap_crossO <> "") And My.Computer.FileSystem.DirectoryExists(GC_RootDir & "\" & GC_input_gap_crossO) Then
            GC_input_gap_crossO_exists = True
            txt_gapcrossOld.ForeColor = Color.Black
        ElseIf IsValidGrid(GC_input_gap_crossO) = False Then
            GC_input_gap_crossO_exists = False
            txt_gapcrossOld.ForeColor = Color.Violet
        Else
            GC_input_gap_crossO_exists = False
            txt_gapcrossOld.ForeColor = Color.Red
        End If
    End Sub
    Private Sub txt_gapcrossOld_Leave(sender As Object, e As EventArgs) Handles txt_gapcrossOld.Leave
        Try
            GC_input_gap_crossO = txt_gapcrossOld.Text
            GC_input_gap_crossO_exist()
            'update gapcross list for combo box
            If Not GC_input_gap_crossO = "" Then
                UpdateGapcrossList(GC_input_gap_crossO, GC_input_gap_crossO_exists)
            End If

        Catch ex As Exception
            MsgBox("Error in entering gapcross (old method) raster name.")
        End Try
    End Sub
    Private Sub btn_BrGcORasTab1_Click(sender As Object, e As EventArgs) Handles btn_BrGcORasTab1.Click
        Try
            'sets the initial selected directory to the luse.shp path if that has been previously selected
            dlg_GetRootFolder.SelectedPath = GC_LastFolder
            dlg_GetRootFolder.Description = "Select gap-cross (old method) raster (folder) within raster root folder"
            Dim dlgc = dlg_GetRootFolder.ShowDialog()
            If dlgc = Windows.Forms.DialogResult.OK Then
                txt_gapcrossOld.Text = dlg_GetRootFolder.SelectedPath.Split(IO.Path.DirectorySeparatorChar).Last() 'get last part only
                txt_gapcrossOld_Leave(Me, Nothing)
            End If
        Catch ex As Exception
            MsgBox("Error in selecting the raster")
        End Try
    End Sub

    'input parameter raster value field
    Private Sub txt_RasterValueField_Enter(sender As Object, e As EventArgs) Handles txt_RasterValueField.Enter
        txt_RasterValueField.ForeColor = Color.Blue
    End Sub
    Private Sub GC_conv_luse_RVfield_exist()
        If txt_RasterValueField.TextLength > 0 Then
            GC_conv_luse_RVfield_exists = True
            txt_RasterValueField.ForeColor = Color.Black
        Else
            GC_conv_luse_RVfield_exists = False
            txt_RasterValueField.ForeColor = Color.Red 'if length is zero then wont be seen anyway but just keeping code consistent
        End If
    End Sub
    Private Sub txt_RasterValueField_Leave(sender As Object, e As EventArgs) Handles txt_RasterValueField.Leave
        Try
            GC_conv_luse_RVfield = txt_RasterValueField.Text
            GC_conv_luse_RVfield_exist()

        Catch ex As Exception
            MsgBox("Error in selecting raster value field.")
        End Try
    End Sub

    'input parameter maximum distance
    Private Sub txt_MaxDist1_Enter(sender As Object, e As EventArgs) Handles txt_MaxDist1.Enter
        txt_MaxDist1.ForeColor = Color.Blue
    End Sub
    Private Sub GC_Max_dist1_exist()
        If txt_MaxDist1.Text <> Nothing Then
            GC_Max_dist1 = CDbl(txt_MaxDist1.Text)
            GC_Max_dist1_exists = True
            txt_MaxDist1.ForeColor = Color.Black
        Else
            GC_Max_dist1_exists = False
            txt_MaxDist1.ForeColor = Color.Red 'if is Nothing then wont be seen anyway but just keeping code consistent
        End If
    End Sub
    Private Sub txt_MaxDist1_Leave(sender As Object, e As EventArgs) Handles txt_MaxDist1.Leave
        Try
            GC_Max_dist1_exist()

        Catch ex As Exception
            MsgBox("The value entered must be a number.")
            txt_MaxDist1.Select()
        End Try
    End Sub

    'input parameter multiplier factor1
    Private Sub txt_inpMultiTab1_Enter(sender As Object, e As EventArgs) Handles txt_inpMultiTab1.Enter
        txt_inpMultiTab1.ForeColor = Color.Blue
    End Sub
    Private Sub GC_CellMultiplier_exist()
        If txt_inpMultiTab1.Text <> Nothing Then
            GC_CellMultiplier = CDbl(txt_inpMultiTab1.Text)
            GC_CellMultiplier_exists = True
            txt_inpMultiTab1.ForeColor = Color.Black
        Else
            GC_CellMultiplier_exists = False
            txt_inpMultiTab1.ForeColor = Color.Red 'if is Nothing then wont be seen anyway but just keeping code consistent
        End If
    End Sub
    Private Sub txt_inpMultiTab1_Leave(sender As Object, e As EventArgs) Handles txt_inpMultiTab1.Leave
        Try
            GC_CellMultiplier_exist()
        Catch ex As Exception
            MsgBox("The value entered for cell multiplier must be a number")
            txt_inpMultiTab1.Select()
        End Try
    End Sub

    'select tab 1 with click
    Private Sub tb_1_Click(sender As Object, e As EventArgs) Handles tb_1.Click
        tb_1.Select()
    End Sub
    'select panel 16 tab 1 with click
    Private Sub Panel16_Click(sender As Object, e As EventArgs) Handles Panel16.Click
        Panel16.Select()
    End Sub

#End Region

#Region "Tab 2 data entry controls"
    'change layer shapefile - tab 2
    Private Sub txt_ChngLyrShpTab2_Enter(sender As Object, e As EventArgs) Handles txt_ChngLyrShpTab2.Enter
        txt_ChngLyrShpTab2.ForeColor = Color.Blue
    End Sub
    Private Sub GC_input_changelyr_shp_exist()
        If (GC_input_changelyr_shp <> "") And My.Computer.FileSystem.FileExists(GC_input_changelyr_shp) Then
            GC_input_changelyr_shp_exists = True
            txt_ChngLyrShpTab2.ForeColor = Color.Black
        Else
            GC_input_changelyr_shp_exists = False
            txt_ChngLyrShpTab2.ForeColor = Color.Red
        End If
    End Sub
    Private Sub txt_ChngLyrShpTab2_Leave(sender As Object, e As EventArgs) Handles txt_ChngLyrShpTab2.Leave
        Try
            GC_input_changelyr_shp = txt_ChngLyrShpTab2.Text
            GC_input_changelyr_shp_exist()
        Catch ex As Exception
            MsgBox("Please enter change layer shapefile.")
        End Try
    End Sub
    Private Sub btn_browseChngShpTab2_Click(sender As Object, e As EventArgs) Handles btn_browseChngShpTab2.Click
        Dim temp5 As String = txt_ChngLyrShpTab2.Text
        Try
            dlg_OpenShapefile.Title = "Select change layer shapefile"
            dlg_OpenShapefile.FileName = ""
            Dim dlgb = dlg_OpenShapefile.ShowDialog() ' find file dialog to find or set scenario file name
            If dlgb = Windows.Forms.DialogResult.OK Then
                txt_ChngLyrShpTab2.Text = dlg_OpenShapefile.FileName
                txt_ChngLyrShpTab2_Leave(Me, Nothing)

            ElseIf dlgb = Windows.Forms.DialogResult.Cancel Then
                txt_ChngLyrShpTab2.Text = temp5
            End If
        Catch ex As Exception
            MsgBox("error in selecting shapefile")
        End Try
    End Sub

    'raster folder - tab 2
    Private Sub txt_rasterFolderTab2_Enter(sender As Object, e As EventArgs) Handles txt_rasterFolderTab2.Enter
        txt_rasterFolderTab2.ForeColor = Color.Blue
    End Sub
    '## updates GC_RootDir
    Private Sub txt_rasterFolderTab2_Leave(sender As Object, e As EventArgs) Handles txt_rasterFolderTab2.Leave
        Try
            If txt_rasterFolderTab2.Text <> "" Then
                GC_RootDir = txt_rasterFolderTab2.Text
                txt_InputF.Text = txt_rasterFolderTab2.Text 'update to tab 1
                txt_SelRootDir.Text = txt_rasterFolderTab2.Text 'update to tab 3
                GC_RootDir_exist()
                'update raster existence, colors and combo box lists
                RasterFolderLeave()
            End If
        Catch ex As Exception
            MsgBox("Please enter raster path.")
        End Try
    End Sub
    Private Sub btn_browse_RasFolderTab2_Click(sender As Object, e As EventArgs) Handles btn_browse_RasFolderTab2.Click
        Dim tempText As String = txt_rasterFolderTab2.Text

        'sets the initial selected directory to the luse.shp path if that has been previously selected
        dlg_GetRootFolder.SelectedPath = GC_LastFolder

        Try
            dlg_GetRootFolder.Description = "Select raster root folder"
            Dim dlgc = dlg_GetRootFolder.ShowDialog()
            If dlgc = Windows.Forms.DialogResult.OK Then
                txt_rasterFolderTab2.Text = dlg_GetRootFolder.SelectedPath
                GC_RootDir = txt_rasterFolderTab2.Text
                GC_LastFolder = dlg_GetRootFolder.SelectedPath
                'copy to other tabs
                txt_SelRootDir.Text = txt_rasterFolderTab2.Text 'update to tab 3
                txt_InputF.Text = txt_rasterFolderTab2.Text 'update to tab 1
                'update text colour on all tabs if folder exists/does not exist
                GC_RootDir_exist()

            ElseIf dlgc = Windows.Forms.DialogResult.Cancel Then
                txt_rasterFolderTab2.Text = tempText
            End If
        Catch ex As Exception
            MsgBox("Please enter or select the path to the raster folder")
        End Try
    End Sub

    'habitat raster - tab 2
    Private Sub txt_habitatTab2_Enter(sender As Object, e As EventArgs) Handles txt_habitatTab2.Enter
        txt_habitatTab2.ForeColor = Color.Blue
    End Sub
    '## updates GC_input_CC
    Private Sub txt_habitatTab2_Leave(sender As Object, e As EventArgs) Handles txt_habitatTab2.Leave
        Try
            GC_input_CC = txt_habitatTab2.Text
            'copy to other tabs
            txt_inplh_cc1.Text = txt_habitatTab2.Text 'update to tab 1 (lh_cc)
            'update text colour on all tabs if folder exists/does not exist
            GC_input_CC_exist()
            'update veg list for combo box
            If Not GC_input_CC = "" Then
                UpdateVegList(GC_input_CC, GC_input_CC_exists)
            End If

        Catch ex As Exception
            MsgBox("Please enter habitat raster.")
        End Try
    End Sub

    'land use raster - tab 2
    Private Sub txt_landuseTab2_Enter(sender As Object, e As EventArgs) Handles txt_landuseTab2.Enter
        txt_landuseTab2.ForeColor = Color.Blue
    End Sub
    '## updates GC_input_luse
    Private Sub txt_landuseTab2_Leave(sender As Object, e As EventArgs) Handles txt_landuseTab2.Leave
        Try
            GC_input_luse = txt_landuseTab2.Text
            'copy to other tabs
            txt_luse1.Text = txt_landuseTab2.Text 'update to tab 1 (luse)
            'update text colour on all tabs if folder exists/does not exist
            GC_input_luse_exist()
            'update landuse list for combo box
            If Not GC_input_luse = "" Then
                UpdateLanduseList(GC_input_luse, GC_input_luse_exists)
            End If

        Catch ex As Exception
            MsgBox("Please enter land use raster.")
        End Try
    End Sub

    'change raster - tab 2
    Private Sub txt_ChangerasterTab2_Enter(sender As Object, e As EventArgs) Handles txt_ChangerasterTab2.Enter
        txt_ChangerasterTab2.ForeColor = Color.Blue
    End Sub
    Private Sub GC_input_changelyr_exist()
        If (GC_input_changelyr <> "") And My.Computer.FileSystem.DirectoryExists(GC_RootDir & "\" & GC_input_changelyr) Then
            GC_input_changelyr_exists = True
            txt_ChangerasterTab2.ForeColor = Color.Black
        ElseIf IsValidGrid(GC_input_changelyr) = False Then
            GC_input_changelyr_exists = False
            txt_ChangerasterTab2.ForeColor = Color.Violet
        Else
            GC_input_changelyr_exists = False
            txt_ChangerasterTab2.ForeColor = Color.Red
        End If
    End Sub
    Private Sub txt_ChangerasterTab2_Leave(sender As Object, e As EventArgs) Handles txt_ChangerasterTab2.Leave
        Try
            GC_input_changelyr = txt_ChangerasterTab2.Text
            GC_input_changelyr_exist()
        Catch ex As Exception
            MsgBox("Please enter change layer raster.")
        End Try
    End Sub
    Private Sub btn_BrChTab2_Click(sender As Object, e As EventArgs) Handles btn_BrChTab2.Click
        Try
            'sets the initial selected directory to the luse.shp path if that has been previously selected
            dlg_GetRootFolder.SelectedPath = GC_LastFolder
            dlg_GetRootFolder.Description = "Select change raster (folder) within raster root folder"
            Dim dlgc = dlg_GetRootFolder.ShowDialog()
            If dlgc = Windows.Forms.DialogResult.OK Then
                txt_ChangerasterTab2.Text = dlg_GetRootFolder.SelectedPath.Split(IO.Path.DirectorySeparatorChar).Last() 'get last part only
                GC_input_changelyr = txt_ChangerasterTab2.Text
                GC_input_changelyr_exist()
            End If
        Catch ex As Exception
            MsgBox("Error in selecting the raster")
        End Try
    End Sub
    'new habitat raster - removal
    Private Sub txt_NewRasterRemv_Enter(sender As Object, e As EventArgs) Handles txt_NewRasterRemv.Enter
        txt_NewRasterRemv.ForeColor = Color.Blue
    End Sub
    Private Sub GC_veg_removal_exist()
        If (GC_veg_removal <> "") And My.Computer.FileSystem.DirectoryExists(GC_RootDir & "\" & GC_veg_removal) Then
            GC_veg_removal_exists = True
            txt_NewRasterRemv.ForeColor = Color.Black
        ElseIf IsValidGrid(GC_veg_removal) = False Then
            GC_veg_removal_exists = False
            txt_NewRasterRemv.ForeColor = Color.Violet
        Else
            GC_veg_removal_exists = False
            txt_NewRasterRemv.ForeColor = Color.Red
        End If
    End Sub
    Private Sub txt_NewRasterRemv_Leave(sender As Object, e As EventArgs) Handles txt_NewRasterRemv.Leave
        Try
            GC_veg_removal = txt_NewRasterRemv.Text
            GC_veg_removal_exist()
            'update veg list for combo box
            If Not GC_veg_removal = "" Then
                UpdateVegList(GC_veg_removal, GC_veg_removal_exists)
            End If

        Catch ex As Exception
            MsgBox("Please enter new vegetation layer (removal) name.")
        End Try
    End Sub
    Private Sub btn_BrRemTab2_Click(sender As Object, e As EventArgs) Handles btn_BrRemTab2.Click
        Try
            'sets the initial selected directory to the luse.shp path if that has been previously selected
            dlg_GetRootFolder.SelectedPath = GC_LastFolder
            dlg_GetRootFolder.Description = "Select new habitat (removal) raster (folder) within raster root folder"
            Dim dlgc = dlg_GetRootFolder.ShowDialog()
            If dlgc = Windows.Forms.DialogResult.OK Then
                txt_NewRasterRemv.Text = dlg_GetRootFolder.SelectedPath.Split(IO.Path.DirectorySeparatorChar).Last() 'get last part only
                GC_veg_removal = txt_NewRasterRemv.Text
                If (GC_veg_removal <> "") And My.Computer.FileSystem.DirectoryExists(GC_RootDir & "\" & GC_veg_removal) Then
                    GC_veg_removal_exists = True
                    txt_NewRasterRemv.ForeColor = Color.Black
                Else
                    GC_veg_removal_exists = False
                    txt_NewRasterRemv.ForeColor = Color.Red
                End If
                'update veg list for combo box
                If Not GC_veg_removal = "" Then
                    UpdateVegList(GC_veg_removal, GC_veg_removal_exists)
                End If
            End If
        Catch ex As Exception
            MsgBox("Error in selecting the raster")
        End Try
    End Sub
    'new habitat raster - addition
    Private Sub txt_NewRastAdd_Enter(sender As Object, e As EventArgs) Handles txt_NewRastAdd.Enter
        txt_NewRastAdd.ForeColor = Color.Blue
    End Sub
    Private Sub GC_veg_addition_exist()
        If (GC_veg_addition <> "") And My.Computer.FileSystem.DirectoryExists(GC_RootDir & "\" & GC_veg_addition) Then
            GC_veg_addition_exists = True
            txt_NewRastAdd.ForeColor = Color.Black
        ElseIf IsValidGrid(GC_veg_addition) = False Then
            GC_veg_addition_exists = False
            txt_NewRastAdd.ForeColor = Color.Violet
        Else
            GC_veg_addition_exists = False
            txt_NewRastAdd.ForeColor = Color.Red
        End If
    End Sub
    Private Sub txt_NewRastAdd_Leave(sender As Object, e As EventArgs) Handles txt_NewRastAdd.Leave
        Try
            GC_veg_addition = txt_NewRastAdd.Text
            GC_veg_addition_exist()
            'update veg list for combo box
            If Not GC_veg_addition = "" Then
                UpdateVegList(GC_veg_addition, GC_veg_addition_exists)
            End If

        Catch ex As Exception
            MsgBox("Please enter ne vegetation layer (addition) name.")
        End Try
    End Sub
    Private Sub btn_BrAddTab2_Click(sender As Object, e As EventArgs) Handles btn_BrAddTab2.Click
        Try
            'sets the initial selected directory to the luse.shp path if that has been previously selected
            dlg_GetRootFolder.SelectedPath = GC_LastFolder
            dlg_GetRootFolder.Description = "Select new habitat (addition) raster (folder) within raster root folder"
            Dim dlgc = dlg_GetRootFolder.ShowDialog()
            If dlgc = Windows.Forms.DialogResult.OK Then
                txt_NewRastAdd.Text = dlg_GetRootFolder.SelectedPath.Split(IO.Path.DirectorySeparatorChar).Last() 'get last part only
                GC_veg_addition = txt_NewRastAdd.Text
                GC_veg_addition_exist()
                'update veg list for combo box
                If Not GC_veg_addition = "" Then
                    UpdateVegList(GC_veg_addition, GC_veg_addition_exists)
                End If
            End If
        Catch ex As Exception
            MsgBox("Error in selecting the raster")
        End Try
    End Sub
    'gap-cross (scenario) raster - tab 2
    Private Sub txt_GapcrossTab2_Enter(sender As Object, e As EventArgs) Handles txt_GapcrossTab2.Enter
        txt_GapcrossTab2.ForeColor = Color.Blue
    End Sub
    Private Sub GC_gapcross_add_exist()
        If (GC_gapcross_add <> "") And My.Computer.FileSystem.DirectoryExists(GC_RootDir & "\" & GC_gapcross_add) Then
            GC_gapcross_add_exists = True
            txt_GapcrossTab2.ForeColor = Color.Black
        ElseIf IsValidGrid(GC_gapcross_add) = False Then
            GC_gapcross_add_exists = False
            txt_GapcrossTab2.ForeColor = Color.Violet
        Else
            GC_gapcross_add_exists = False
            txt_GapcrossTab2.ForeColor = Color.Red
        End If
    End Sub
    Private Sub txt_GapcrossTab2_Leave(sender As Object, e As EventArgs) Handles txt_GapcrossTab2.Leave
        Try
            GC_gapcross_add = txt_GapcrossTab2.Text
            GC_gapcross_add_exist()
            'update gapcross list for combo box
            If Not GC_gapcross_add = "" Then
                UpdateGapcrossList(GC_gapcross_add, GC_gapcross_add_exists)
            End If

        Catch ex As Exception
            MsgBox("Please enter gapcross (addition) name.")
        End Try
    End Sub
    Private Sub btn_BrGCTab2_Click(sender As Object, e As EventArgs) Handles btn_BrGCTab2.Click
        Try
            'sets the initial selected directory to the luse.shp path if that has been previously selected
            dlg_GetRootFolder.SelectedPath = GC_LastFolder
            dlg_GetRootFolder.Description = "Select gap-cross (scenario) raster (folder) within raster root folder"
            Dim dlgc = dlg_GetRootFolder.ShowDialog()
            If dlgc = Windows.Forms.DialogResult.OK Then
                txt_GapcrossTab2.Text = dlg_GetRootFolder.SelectedPath.Split(IO.Path.DirectorySeparatorChar).Last() 'get last part only
                GC_gapcross_add = txt_GapcrossTab2.Text
                GC_gapcross_add_exist()
                'update gapcross list for combo box
                If Not GC_gapcross_add = "" Then
                    UpdateGapcrossList(GC_gapcross_add, GC_gapcross_add_exists)
                End If
            End If
        Catch ex As Exception
            MsgBox("Error in selecting the raster")
        End Try
    End Sub
    'modified land use (infrastructure) raster - tab 2
    Private Sub txt_infrastructure_Enter(sender As Object, e As EventArgs) Handles txt_infrastructure.Enter
        txt_infrastructure.ForeColor = Color.Blue
    End Sub
    Private Sub GC_luse_add_exist()
        If (GC_luse_add <> "") And My.Computer.FileSystem.DirectoryExists(GC_RootDir & "\" & GC_luse_add) Then
            GC_luse_add_exists = True
            txt_infrastructure.ForeColor = Color.Black
        ElseIf IsValidGrid(GC_luse_add) = False Then
            GC_luse_add_exists = False
            txt_infrastructure.ForeColor = Color.Violet
        Else
            GC_luse_add_exists = False
            txt_infrastructure.ForeColor = Color.Red
        End If
    End Sub
    Private Sub txt_infrastructure_Leave(sender As Object, e As EventArgs) Handles txt_infrastructure.Leave
        Try
            GC_luse_add = txt_infrastructure.Text
            GC_luse_add_exist()
            'update landuse list for combo box
            If Not GC_luse_add = "" Then
                UpdateLanduseList(GC_luse_add, GC_luse_add_exists)
            End If

        Catch ex As Exception
            MsgBox("Please enter landuse (infrastructure) name.")
        End Try
    End Sub
    Private Sub btn_BrInfTab2_Click(sender As Object, e As EventArgs) Handles btn_BrInfTab2.Click
        Try
            'sets the initial selected directory to the luse.shp path if that has been previously selected
            dlg_GetRootFolder.SelectedPath = GC_LastFolder
            dlg_GetRootFolder.Description = "Select modified land use (infrastructure) raster (folder) within raster root folder"
            Dim dlgc = dlg_GetRootFolder.ShowDialog()
            If dlgc = Windows.Forms.DialogResult.OK Then
                txt_infrastructure.Text = dlg_GetRootFolder.SelectedPath.Split(IO.Path.DirectorySeparatorChar).Last() 'get last part only
                GC_luse_add = txt_infrastructure.Text
                GC_luse_add_exist()
                'update landuse list for combo box
                If Not GC_luse_add = "" Then
                    UpdateLanduseList(GC_luse_add, GC_luse_add_exists)
                End If
            End If
        Catch ex As Exception
            MsgBox("Error in selecting the raster")
        End Try
    End Sub
    'change polygon ID field parameter - tab 2
    Private Sub txt_RasValFldTab2_Enter(sender As Object, e As EventArgs) Handles txt_RasValFldTab2.Enter
        txt_RasValFldTab2.ForeColor = Color.Blue
    End Sub
    Private Sub GC_FieldName_exist()
        If txt_RasValFldTab2.TextLength > 0 Then
            GC_FieldName_exists = True
            txt_RasValFldTab2.ForeColor = Color.Black
        Else
            GC_FieldName_exists = False
            txt_RasValFldTab2.ForeColor = Color.Red 'if length is zero then wont be seen anyway but just keeping code consistent
        End If
    End Sub
    Private Sub txt_RasValFldTab2_Leave(sender As Object, e As EventArgs) Handles txt_RasValFldTab2.Leave
        Try
            GC_FieldName = txt_RasValFldTab2.Text
            GC_FieldName_exist()

        Catch ex As Exception
            MsgBox("Please enter output path.")
        End Try
    End Sub

    'hydrology value 1 parameter - tab 2
    Private Sub txt_HydrolVal1_Enter(sender As Object, e As EventArgs) Handles txt_HydrolVal1.Enter
        txt_HydrolVal1.ForeColor = Color.Blue
    End Sub
    'leave numerical
    Private Sub GC_HydrologyValue1_exist()
        If txt_HydrolVal1.Text <> Nothing Then
            GC_HydrologyValue1 = CDbl(txt_HydrolVal1.Text)
            GC_HydrologyValue1_exists = True
            txt_HydrolVal1.ForeColor = Color.Black
        Else
            GC_HydrologyValue1_exists = False
            txt_HydrolVal1.ForeColor = Color.Red
        End If
    End Sub
    Private Sub txt_HydrolVal1_Leave(sender As Object, e As EventArgs) Handles txt_HydrolVal1.Leave
        Try
            GC_HydrologyValue1_exist()

        Catch ex As Exception
            MsgBox("The value entered must be a number")
            txt_HydrolVal1.Select()
        End Try
    End Sub

    'cell multiplier2 parameter - tab 2
    Private Sub txt_CellMultip2_Enter(sender As Object, e As EventArgs) Handles txt_CellMultip2.Enter
        txt_CellMultip2.ForeColor = Color.Blue
    End Sub
    'leave numerical
    Private Sub GC_CellMultiplier2_exist()
        If txt_CellMultip2.Text <> Nothing Then
            GC_CellMultiplier2 = CDbl(txt_CellMultip2.Text)
            GC_CellMultiplier2_exists = True
            txt_CellMultip2.ForeColor = Color.Black
        Else
            GC_CellMultiplier2_exists = False
            txt_CellMultip2.ForeColor = Color.Red
        End If
    End Sub
    Private Sub txt_CellMultip2_Leave(sender As Object, e As EventArgs) Handles txt_CellMultip2.Leave
        Try
            GC_CellMultiplier2_exist()

        Catch ex As Exception
            MsgBox("The value entered must be a number")
            txt_CellMultip2.Select()
        End Try
    End Sub

    'hydrology value 2 parameter - tab 2
    Private Sub txt_HydrolVal2_Enter(sender As Object, e As EventArgs) Handles txt_HydrolVal2.Enter
        txt_HydrolVal2.ForeColor = Color.Blue
    End Sub
    'leave numerical
    Private Sub GC_HydrologyValue2_exist()
        If txt_HydrolVal2.Text <> Nothing Then
            GC_HydrologyValue2 = CDbl(txt_HydrolVal2.Text)
            GC_HydrologyValue2_exists = True
            txt_HydrolVal2.ForeColor = Color.Black
        Else
            GC_HydrologyValue2_exists = False
            txt_HydrolVal2.ForeColor = Color.Red
        End If
    End Sub
    Private Sub txt_HydrolVal2_Leave(sender As Object, e As EventArgs) Handles txt_HydrolVal2.Leave
        Try
            GC_HydrologyValue2_exist()

        Catch ex As Exception
            MsgBox("The value entered must be a number")
            txt_HydrolVal2.Select()
        End Try

    End Sub

    'select panel 17 (tab2) with click
    Private Sub Panel17_Click(sender As Object, e As EventArgs) Handles Panel17.Click
        Panel17.Select()
    End Sub
    'select tab 2 with click
    Private Sub tb_2_Click(sender As Object, e As EventArgs) Handles tb_2.Click
        tb_2.Select()
    End Sub
#End Region

#Region "Combo boxes on graphab tab"
    'update lists for combo boxes (graphab tab)
    Private Sub UpdateListsForComboBoxes()
        Try
            'clear combo box lists
            GC_v.Clear()
            GC_g.Clear()
            GC_l.Clear()
            'update vegetation list for combo box
            If Not GC_input_CC = "" Then
                UpdateVegList(GC_input_CC, GC_input_CC_exists)
            End If
            If Not GC_veg_removal = "" Then
                UpdateVegList(GC_veg_removal, GC_veg_removal_exists)
            End If
            If Not GC_veg_addition = "" Then
                UpdateVegList(GC_veg_addition, GC_veg_addition_exists)
            End If
            'update gapcross list for combo box
            If Not GC_input_gap_cross = "" Then
                UpdateGapcrossList(GC_input_gap_cross, GC_input_gap_cross_exists)
            End If
            If Not GC_input_gap_crossO = "" Then
                UpdateGapcrossList(GC_input_gap_crossO, GC_input_gap_crossO_exists)
            End If
            If Not GC_gapcross_add = "" Then
                UpdateGapcrossList(GC_gapcross_add, GC_gapcross_add_exists)
            End If
            'update landuse list for combo box
            If Not GC_input_luse = "" Then
                UpdateLanduseList(GC_input_luse, GC_input_luse_exists)
            End If
            If Not GC_luse_add = "" Then
                UpdateLanduseList(GC_luse_add, GC_luse_add_exists)
            End If
        Catch ex As Exception
            MsgBox("There was an error in updating lists")
        End Try
    End Sub

    'update veg combo box list
    Private Sub UpdateVegList(s_raster As String, ex_raster As Boolean) 'raster name and does it exist
        Try
            'if exists and not already there add veg raster to list
            If (Not GC_v.Contains(s_raster)) And (ex_raster = True) Then
                GC_v.Add(s_raster)
            End If
            'if not exists and is already there delete veg raster from list
            If (GC_v.Contains(s_raster)) And (ex_raster = False) Then
                GC_v.Remove(s_raster)
            End If
        Catch
            MsgBox("error in populating list")
        End Try
        'also update selected item if it exists
        txt_inp_cc_Leave(Me, Nothing)
    End Sub
    'make veg combo box selection
    Private Sub cbo_SelectVegRaster_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbo_SelectVegRaster.SelectedIndexChanged
        txt_inp_cc.Text = CStr(cbo_SelectVegRaster.SelectedItem)
        GC_v_sel = txt_inp_cc.Text
    End Sub
    'populate veg combo box
    Private Sub cbo_SelectVegRaster_Enter(sender As Object, e As EventArgs) Handles cbo_SelectVegRaster.Enter
        cbo_SelectVegRaster.Items.Clear()
        If Not GC_v Is Nothing Then
            For Each x In GC_v
                cbo_SelectVegRaster.Items.Add(x)
            Next
        End If
    End Sub
    'clear veg combo box afterwards
    Private Sub cbo_SelectVegRaster_Leave(sender As Object, e As EventArgs) Handles cbo_SelectVegRaster.Leave
        cbo_SelectVegRaster.Text = ""
    End Sub

    'update gapcross combo box list
    Private Sub UpdateGapcrossList(s_raster As String, ex_raster As Boolean)
        Try
            'if exists and not already there add gapcross raster to list
            If (Not GC_g.Contains(s_raster)) And (ex_raster = True) Then
                GC_g.Add(s_raster)
            End If
            'if not exists and is already there delete gapcross raster from list
            If (GC_g.Contains(s_raster)) And (ex_raster = False) Then
                GC_g.Remove(s_raster)
            End If
        Catch
            MsgBox("error in populating list")
        End Try
        'also update selected item if it exists
        txt_inp_gc_Leave(Me, Nothing)
    End Sub
    'make gapcross combo box selection
    Private Sub cbo_SelectGapcrossRaster_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbo_SelectGapcrossRaster.SelectedIndexChanged
        txt_inp_gc.Text = CStr(cbo_SelectGapcrossRaster.SelectedItem)
        GC_g_sel = txt_inp_gc.Text
    End Sub
    'populate gapcross combo box
    Private Sub cbo_SelectGapcrossRaster_Enter(sender As Object, e As EventArgs) Handles cbo_SelectGapcrossRaster.Enter
        cbo_SelectGapcrossRaster.Items.Clear()
        If Not GC_g Is Nothing Then
            For Each x In GC_g
                cbo_SelectGapcrossRaster.Items.Add(x)
            Next
        End If
    End Sub
    'clear gapcross combo box afterwards
    Private Sub cbo_SelectGapcrossRaster_Leave(sender As Object, e As EventArgs) Handles cbo_SelectGapcrossRaster.Leave
        cbo_SelectGapcrossRaster.Text = ""
    End Sub

    'update landuse combo box list
    Private Sub UpdateLanduseList(s_raster As String, ex_raster As Boolean)
        Try
            'if exists and not already there add landuse raster to list
            If (Not GC_l.Contains(s_raster)) And (ex_raster = True) Then
                GC_l.Add(s_raster)
            End If
            'if not exists and is already there delete landuse raster from list
            If (GC_l.Contains(s_raster)) And (ex_raster = False) Then
                GC_l.Remove(s_raster)
            End If
        Catch
            MsgBox("error in populating list")
        End Try
        'also update selected item if it exists
        txt_inp_luse_Leave(Me, Nothing)
    End Sub
    'make landuse combo box selection
    Private Sub cbo_SelectLanduseRaster_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbo_SelectLanduseRaster.SelectedIndexChanged
        txt_inp_luse.Text = CStr(cbo_SelectLanduseRaster.SelectedItem)
        GC_l_sel = txt_inp_luse.Text
    End Sub
    'populate landuse combo box
    Private Sub cbo_SelectLanduseRaster_Enter(sender As Object, e As EventArgs) Handles cbo_SelectLanduseRaster.Enter
        cbo_SelectLanduseRaster.Items.Clear()
        If Not GC_l Is Nothing Then
            For Each x In GC_l
                cbo_SelectLanduseRaster.Items.Add(x)
            Next
        End If
    End Sub
    'clear landuse combo box afterwards
    Private Sub cbo_SelectLanduseRaster_Leave(sender As Object, e As EventArgs) Handles cbo_SelectLanduseRaster.Leave
        cbo_SelectLanduseRaster.Text = ""
    End Sub
#End Region

    'About box
    Private Sub btn_About_Click(sender As Object, e As EventArgs) Handles btn_About.Click
        Dim About_Text As String = "This work was supported by the Landscapes and Policy Research Hub, " & vbCrLf
        About_Text = About_Text & "which is funded through the Australian Government’s National " & vbCrLf
        About_Text = About_Text & "Environmental Research Programme (NERP). The wildlife connectivity study " & vbCrLf
        About_Text = About_Text & "was an initiative of the Sustainable Regional Development Program " & vbCrLf
        About_Text = About_Text & "in the Department of the Environment." & vbCrLf
        About_Text = About_Text & "" & vbCrLf
        About_Text = About_Text & "This application is an implementation of GAP_CLoSR tools and methods  " & vbCrLf
        About_Text = About_Text & "developed by Dr Alex Lechner (alexmarklechner<at>yahoo.com.au) as a part " & vbCrLf
        About_Text = About_Text & "of the NERP Landscapes and Policy hub." & vbCrLf
        About_Text = About_Text & " " & vbCrLf
        About_Text = About_Text & "This GUI was developed by Dr Michael Lacey (Michael.Lacey<at>utas.edu.au)," & vbCrLf
        About_Text = About_Text & "in association with Alex Lechner and the NERP Landscapes and Policy" & vbCrLf
        About_Text = About_Text & "hub and is licensed under the Creative Commons " & vbCrLf
        About_Text = About_Text & "AttributionNonCommercial-ShareAlike 3.0 Australia (CC BY-NC-SA 3.0 AU)" & vbCrLf
        About_Text = About_Text & "license . To view a copy of this licence, " & vbCrLf
        About_Text = About_Text & "visit https://creativecommons.org/licenses/by-nc-sa/3.0/au/)." & vbCrLf
        About_Text = About_Text & " " & vbCrLf

        About_Text = About_Text & My.Application.Info.ProductName & vbCrLf
        About_Text = About_Text & String.Format("Version {0}", My.Application.Info.Version.ToString) & vbCrLf
        About_Text = About_Text & My.Application.Info.Copyright & vbCrLf
        About_Text = About_Text & My.Application.Info.CompanyName & vbCrLf
        Dim MB_About = MessageBox.Show(About_Text, "About GAP CLoSR Tools", MessageBoxButtons.OK)

    End Sub
    'main form closing
    Private Sub frm_Main_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If GC_CloseFlagged = False Then 'if selected close by clicking close box (X) on form
            If rb_S_Yes.Checked = True Then
                Dim MB = MessageBox.Show("Save current scenario parameters?", "Save Scenerio", MessageBoxButtons.YesNo)
                If MB = Windows.Forms.DialogResult.Yes Then
                    SaveScenarioToolStripMenuItem_Click(Me, Nothing)
                ElseIf MB = Windows.Forms.DialogResult.No Then
                    'MsgBox("Save was cancelled.")
                End If
            End If
            If rb_C_Yes.Checked = True Then
                btn_SavePyExePth_Click(Me, Nothing)
            End If
        End If

    End Sub

#Region "Menus"
    Private Sub DefaultsTabToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DefaultsTabToolStripMenuItem.Click
        tb_Settings.SelectedTab = tb_1
    End Sub

    Private Sub ScenariosTabToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ScenariosTabToolStripMenuItem.Click
        tb_Settings.SelectedTab = tb_2
    End Sub

    Private Sub ProcessrastersForModelTabToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ProcessrastersForModelTabToolStripMenuItem.Click
        tb_Settings.SelectedTab = tb_3
    End Sub

    Private Sub CircuitscapeInputsTabToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CircuitscapeInputsTabToolStripMenuItem.Click
        tb_Settings.SelectedTab = tb_Circuitscape
    End Sub

    Private Sub ScriptOutputsTabToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CircuitscapeInputsTabToolStripMenuItem.Click
        tb_Settings.SelectedTab = tb_4
    End Sub

    Private Sub PythonConfigTabToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ScriptOutputsTabToolStripMenuItem.Click
        tb_Settings.SelectedTab = tb_Config
    End Sub

    Private Sub LoadScenarioToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles LoadScenarioToolStripMenuItem1.Click
        'open load file dialog box
        Dim ScenarioPath As String = GC_appPath & "\Scenarios\Defaults.txt"

        Try
            Dim Rst = dlg_GetScenarioFile.ShowDialog() ' find file dialog to find or set scenario file name
            If Rst = Windows.Forms.DialogResult.OK Then
                ScenarioPath = dlg_GetScenarioFile.FileName
                LoadParameters(ScenarioPath)
            ElseIf Rst = Windows.Forms.DialogResult.Cancel Then
                'nothing happens when cancel selected
            End If
        Catch ex As Exception
            MsgBox("unhandled exception")
        End Try
    End Sub

    Private Sub SaveScenarioToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveScenarioToolStripMenuItem.Click
        'open save file dialog box
        Dim Defaultspath As String = GC_appPath & "\Scenarios\Defaults.txt"
        Try
            Dim Rst = dlg_SaveF.ShowDialog() 'find save file name
            If Rst = Windows.Forms.DialogResult.OK Then
                Defaultspath = dlg_SaveF.FileName
                SaveParamsGeneric(Defaultspath)
            ElseIf Rst = Windows.Forms.DialogResult.Cancel Then
                MsgBox("Save file was cancelled.")
            End If
        Catch ex As Exception
            MsgBox("unhandled exception")
        End Try
    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        'note: This runs the frm_Main_FormClosing() routine twice, first with GC_CloseFlagged = False then again with GC_CloseFlagged = True
        'the flag stops code in frm_Main_FormClosing() from be executed twice. There may be another way to do it but this works.
        frm_Main_FormClosing(Me, Nothing)
        GC_CloseFlagged = True 'close selected from menu
        Close()

    End Sub

    Private Sub AboutGAPCLosRToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AboutGAPCLosRToolStripMenuItem.Click
        btn_About_Click(Me, Nothing)
    End Sub
#End Region

#Region "Check boxes for run buttons"
    'default tab
    Private Sub DoButtonText1()
        If GC_Gr1ButtonsChecked > 0 Then
            btn_RunCombined.Text = "Generate selected Default and Scenario input rasters (" & CStr(GC_Gr1ButtonsChecked) & ")"
        Else
            btn_RunCombined.Text = "Generate selected Default and Scenario input rasters"
        End If
    End Sub
    Private Sub chk_SelDefL_CheckedChanged(sender As Object, e As EventArgs) Handles chk_SelDefL.CheckedChanged
        If chk_SelDefL.Checked = True Then
            btn_RunConvLusetoRaster.Enabled = False
            btn_RunCombined.Enabled = True
            GC_Gr1ButtonsChecked = GC_Gr1ButtonsChecked + 1
        Else
            btn_RunConvLusetoRaster.Enabled = True
            GC_Gr1ButtonsChecked = GC_Gr1ButtonsChecked - 1
            If GC_Gr1ButtonsChecked < 1 Then
                btn_RunCombined.Enabled = False
            End If
        End If
        DoButtonText1()

    End Sub
    Private Sub chk_SelDefGC_CheckedChanged(sender As Object, e As EventArgs) Handles chk_SelDefGC.CheckedChanged
        If chk_SelDefGC.Checked = True Then
            btn_CreateGapCross.Enabled = False
            btn_RunCombined.Enabled = True
            GC_Gr1ButtonsChecked = GC_Gr1ButtonsChecked + 1
        Else
            btn_CreateGapCross.Enabled = True
            'btn_RunCombined.Enabled = False
            GC_Gr1ButtonsChecked = GC_Gr1ButtonsChecked - 1
            If GC_Gr1ButtonsChecked < 1 Then
                btn_RunCombined.Enabled = False
            End If
        End If
        DoButtonText1()
    End Sub
    Private Sub chk_SelDefGCO_CheckedChanged(sender As Object, e As EventArgs) Handles chk_SelDefGCO.CheckedChanged
        If chk_SelDefGCO.Checked = True Then
            btn_GapCrossOld.Enabled = False
            btn_RunCombined.Enabled = True
            GC_Gr1ButtonsChecked = GC_Gr1ButtonsChecked + 1
        Else
            btn_GapCrossOld.Enabled = True
            'btn_RunCombined.Enabled = False
            GC_Gr1ButtonsChecked = GC_Gr1ButtonsChecked - 1
            If GC_Gr1ButtonsChecked < 1 Then
                btn_RunCombined.Enabled = False
            End If
        End If
        DoButtonText1()
    End Sub
    'scenario tab
    Private Sub chk_selS1_CheckedChanged(sender As Object, e As EventArgs) Handles chk_selS1.CheckedChanged
        If chk_selS1.Checked = True Then
            btn_RunScen1.Enabled = False
            btn_RunCombined.Enabled = True
            GC_Gr1ButtonsChecked = GC_Gr1ButtonsChecked + 1
        Else
            btn_RunScen1.Enabled = True
            'btn_RunCombined.Enabled = False
            GC_Gr1ButtonsChecked = GC_Gr1ButtonsChecked - 1
            If GC_Gr1ButtonsChecked < 1 Then
                btn_RunCombined.Enabled = False
            End If
        End If
        DoButtonText1()
    End Sub
    Private Sub chk_selS2a_CheckedChanged(sender As Object, e As EventArgs) Handles chk_selS2a.CheckedChanged
        If chk_selS2a.Checked = True Then
            btn_RunScen2a.Enabled = False
            btn_RunCombined.Enabled = True
            GC_Gr1ButtonsChecked = GC_Gr1ButtonsChecked + 1
        Else
            btn_RunScen2a.Enabled = True
            'btn_RunCombined.Enabled = False
            GC_Gr1ButtonsChecked = GC_Gr1ButtonsChecked - 1
            If GC_Gr1ButtonsChecked < 1 Then
                btn_RunCombined.Enabled = False
            End If
        End If
        DoButtonText1()
    End Sub
    Private Sub chk_sel2b_CheckedChanged(sender As Object, e As EventArgs) Handles chk_sel2b.CheckedChanged
        If chk_sel2b.Checked = True Then
            btn_RunScen2b.Enabled = False
            btn_RunCombined.Enabled = True
            GC_Gr1ButtonsChecked = GC_Gr1ButtonsChecked + 1
        Else
            btn_RunScen2b.Enabled = True
            'btn_RunCombined.Enabled = False
            GC_Gr1ButtonsChecked = GC_Gr1ButtonsChecked - 1
            If GC_Gr1ButtonsChecked < 1 Then
                btn_RunCombined.Enabled = False
            End If
        End If
        DoButtonText1()
    End Sub
    Private Sub chk_sel3_CheckedChanged(sender As Object, e As EventArgs) Handles chk_sel3.CheckedChanged
        If chk_sel3.Checked = True Then
            btn_RunScen3.Enabled = False
            btn_RunCombined.Enabled = True
            GC_Gr1ButtonsChecked = GC_Gr1ButtonsChecked + 1
        Else
            btn_RunScen3.Enabled = True
            'btn_RunCombined.Enabled = False
            GC_Gr1ButtonsChecked = GC_Gr1ButtonsChecked - 1
            If GC_Gr1ButtonsChecked < 1 Then
                btn_RunCombined.Enabled = False
            End If
        End If
        DoButtonText1()
    End Sub
    Private Sub chk_selS4_CheckedChanged(sender As Object, e As EventArgs) Handles chk_selS4.CheckedChanged
        If chk_selS4.Checked = True Then
            btn_RunScen4.Enabled = False
            btn_RunCombined.Enabled = True
            GC_Gr1ButtonsChecked = GC_Gr1ButtonsChecked + 1
        Else
            btn_RunScen4.Enabled = True
            GC_Gr1ButtonsChecked = GC_Gr1ButtonsChecked - 1
            If GC_Gr1ButtonsChecked < 1 Then
                btn_RunCombined.Enabled = False
            End If
        End If
        DoButtonText1()
    End Sub
    'graphab 
    Private Sub chk_SelGraphab_CheckedChanged(sender As Object, e As EventArgs) Handles chk_SelGraphab.CheckedChanged
        If chk_SelGraphab.Checked = True Then
            btn_Run_GC.Enabled = False
            btn_ProcessrastersTab4.Enabled = True
        Else
            btn_Run_GC.Enabled = True
            btn_ProcessrastersTab4.Enabled = False
        End If
    End Sub

    'circuitscape
    Private Sub DoButtonText3()
        If GC_Gr3ButtonsChecked > 0 Then
            btn_RunCircuitscape_Tab4.Text = "Create Circuitscape inputs (" & CStr(GC_Gr3ButtonsChecked) & ")"
        Else
            btn_RunCircuitscape_Tab4.Text = "Create Circuitscape inputs"
        End If
    End Sub
    Private Sub chk_Circ1_CheckedChanged(sender As Object, e As EventArgs) Handles chk_Circ1.CheckedChanged
        If chk_Circ1.Checked = True Then
            btn_RunCirc1.Enabled = False
            btn_RunCircuitscape_Tab4.Enabled = True
            GC_Gr3ButtonsChecked = GC_Gr3ButtonsChecked + 1
        Else
            btn_RunCirc1.Enabled = True
            'btn_RunCircuitscape_Tab4.Enabled = False
            GC_Gr3ButtonsChecked = GC_Gr3ButtonsChecked - 1
            If GC_Gr3ButtonsChecked < 1 Then
                btn_RunCircuitscape_Tab4.Enabled = False
            End If
        End If
        DoButtonText3()
    End Sub
    Private Sub chk_Circ2_CheckedChanged(sender As Object, e As EventArgs) Handles chk_Circ2.CheckedChanged
        If chk_Circ2.Checked = True Then
            btn_RunCirc2.Enabled = False
            btn_RunCircuitscape_Tab4.Enabled = True
            GC_Gr3ButtonsChecked = GC_Gr3ButtonsChecked + 1
        Else
            btn_RunCirc2.Enabled = True
            'btn_RunCircuitscape_Tab4.Enabled = False
            GC_Gr3ButtonsChecked = GC_Gr3ButtonsChecked - 1
            If GC_Gr3ButtonsChecked < 1 Then
                btn_RunCircuitscape_Tab4.Enabled = False
            End If
        End If
        DoButtonText3()
    End Sub
    Private Sub chk_Circ3_CheckedChanged(sender As Object, e As EventArgs) Handles chk_Circ3.CheckedChanged
        If chk_Circ3.Checked = True Then
            btn_RunCirc3.Enabled = False
            btn_RunCircuitscape_Tab4.Enabled = True
            GC_Gr3ButtonsChecked = GC_Gr3ButtonsChecked + 1
        Else
            btn_RunCirc3.Enabled = True
            'btn_RunCircuitscape_Tab4.Enabled = False
            GC_Gr3ButtonsChecked = GC_Gr3ButtonsChecked - 1
            If GC_Gr3ButtonsChecked < 1 Then
                btn_RunCircuitscape_Tab4.Enabled = False
            End If
        End If
        DoButtonText3()
    End Sub

#End Region

#Region "front tab"
    Private Sub Panel10_Click(sender As Object, e As EventArgs) Handles Panel10.Click
        Dim URL As String = "http://www.environment.gov.au/science/nerp"
        Dim sInfo = New ProcessStartInfo(URL)
        Try
            Process.Start(sInfo)
        Catch
            MsgBox("Could not open link")
        End Try

        'http://www.utas.edu.au
    End Sub

    Private Sub Panel10_MouseEnter(sender As Object, e As EventArgs) Handles Panel10.MouseEnter
        Panel10.Cursor = Cursors.Hand
    End Sub

    Private Sub Panel10_MouseLeave(sender As Object, e As EventArgs) Handles Panel10.MouseLeave
        Panel10.Cursor = Cursors.Default
    End Sub

    Private Sub Panel12_Click(sender As Object, e As EventArgs) Handles Panel12.Click
        Dim URL As String = "http://www.nerplandscapes.edu.au/"
        Dim sInfo = New ProcessStartInfo(URL)
        Try
            Process.Start(sInfo)
        Catch
            MsgBox("Could not open link")
        End Try
    End Sub

    Private Sub Panel12_MouseEnter(sender As Object, e As EventArgs) Handles Panel12.MouseEnter
        Panel12.Cursor = Cursors.Hand
    End Sub

    Private Sub Panel12_MouseLeave(sender As Object, e As EventArgs) Handles Panel12.MouseLeave
        Panel12.Cursor = Cursors.Default
    End Sub

    Private Sub Panel11_Click(sender As Object, e As EventArgs) Handles Panel11.Click
        Dim URL As String = "http://www.utas.edu.au"
        Dim sInfo = New ProcessStartInfo(URL)
        Try
            Process.Start(sInfo)
        Catch
            MsgBox("Could not open link")
        End Try
    End Sub

    Private Sub Panel11_MouseEnter(sender As Object, e As EventArgs) Handles Panel11.MouseEnter
        Panel11.Cursor = Cursors.Hand
    End Sub

    Private Sub Panel11_MouseLeave(sender As Object, e As EventArgs) Handles Panel11.MouseLeave
        Panel11.Cursor = Cursors.Default
    End Sub

    Private Sub lbl_GCG_MouseEnter(sender As Object, e As EventArgs) Handles lbl_GCG.MouseEnter
        lbl_GCG.Cursor = Cursors.Hand
    End Sub

    Private Sub lbl_GCG_MouseLeave(sender As Object, e As EventArgs) Handles lbl_GCG.MouseLeave
        lbl_GCG.Cursor = Cursors.Default
    End Sub

    Private Sub lbl_GCG_Click(sender As Object, e As EventArgs) Handles lbl_GCG.Click
        btn_About_Click(Me, Nothing)
    End Sub

    Private Sub lbl_GAP_CLoSR_Click(sender As Object, e As EventArgs) Handles lbl_GAP_CLoSR.Click
        Dim URL As String = "http://www.nerplandscapes.edu.au/GAP_CLoSR"
        Dim sInfo = New ProcessStartInfo(URL)
        Try
            Process.Start(sInfo)
        Catch
            MsgBox("Could not open link")
        End Try
    End Sub

    Private Sub lbl_GAP_CLoSR_MouseEnter(sender As Object, e As EventArgs) Handles lbl_GAP_CLoSR.MouseEnter
        lbl_GAP_CLoSR.Cursor = Cursors.Hand
    End Sub

    Private Sub lbl_GAP_CLoSR_MouseLeave(sender As Object, e As EventArgs) Handles lbl_GAP_CLoSR.MouseLeave
        lbl_GAP_CLoSR.Cursor = Cursors.Default
    End Sub
#End Region

#Region "Circuitscape tab"
    'input circuitscape output folder (GC_CC_OutFolder)
    Private Sub txt_Circ_OutpFolder_Enter(sender As Object, e As EventArgs) Handles txt_Circ_OutpFolder.Enter
        txt_Circ_OutpFolder.ForeColor = Color.Blue
    End Sub
    Private Sub GC_CC_OutFolder_exist()
        If (GC_CC_OutFolder <> "") And My.Computer.FileSystem.DirectoryExists(GC_CC_OutFolder) Then
            GC_CC_OutFolder_exists = True
            txt_Circ_OutpFolder.ForeColor = Color.Black
        Else
            GC_CC_OutFolder_exists = False
            txt_Circ_OutpFolder.ForeColor = Color.Red
        End If
    End Sub
    Private Sub txt_Circ_OutpFolder_Leave(sender As Object, e As EventArgs) Handles txt_Circ_OutpFolder.Leave
        Try
            If txt_Circ_OutpFolder.Text <> "" Then
                GC_CC_OutFolder = txt_Circ_OutpFolder.Text
                GC_CC_OutFolder_exist()
                GC_CC_OutpNV_Ras_exist()
                GC_CC_FocalNodesComp_R_exist()
                GC_CC_PatchCirc_R_exist()
            End If
        Catch ex As Exception
            MsgBox("enter text")
        End Try
    End Sub
    Private Sub btn_Circ_BrowseFolder_Click(sender As Object, e As EventArgs) Handles btn_Circ_BrowseFolder.Click
        Dim tempText As String = txt_Circ_OutpFolder.Text
        If GC_LastFolderCC <> "" Then
            dlg_GetRootFolder.SelectedPath = GC_LastFolderCC
        End If
        Try
            dlg_GetRootFolder.Description = "Select output folder for Circuitscape rasters and asc files"
            Dim dlgc = dlg_GetRootFolder.ShowDialog()
            If dlgc = Windows.Forms.DialogResult.OK Then
                txt_Circ_OutpFolder.Text = dlg_GetRootFolder.SelectedPath
                txt_Circ_OutpFolder_Leave(Me, Nothing)
                GC_LastFolderCC = IO.Path.GetDirectoryName(GC_CC_OutFolder)
                'update raster existence, colors and combo box lists
                'RasterFolderLeave()  TODO add new function for circuitscape tab
            ElseIf dlgc = Windows.Forms.DialogResult.Cancel Then
                txt_Circ_OutpFolder.Text = tempText
            End If
        Catch ex As Exception
            MsgBox("enter text")
        End Try
    End Sub
    'input test extent shapefile (GC_CC_TestExt_shp)
    Private Sub txt_CircTestExt_Enter(sender As Object, e As EventArgs) Handles txt_CircTestExt.Enter
        txt_CircTestExt.ForeColor = Color.Blue
    End Sub
    Private Sub GC_CC_TestExt_shp_exist()
        If (GC_CC_TestExt_shp <> "") And My.Computer.FileSystem.FileExists(GC_CC_TestExt_shp) Then
            GC_CC_TestExt_shp_exists = True
            txt_CircTestExt.ForeColor = Color.Black
        Else
            GC_CC_TestExt_shp_exists = False
            txt_CircTestExt.ForeColor = Color.Red
        End If
    End Sub
    Private Sub txt_CircTestExt_Leave(sender As Object, e As EventArgs) Handles txt_CircTestExt.Leave
        Try
            GC_CC_TestExt_shp = txt_CircTestExt.Text
            GC_CC_TestExt_shp_exist()
            'update raster existence, colors and combo box lists
            'RasterFolderLeave()  TODO add new function for circuitscape tab
        Catch ex As Exception
            MsgBox("enter text")
        End Try
    End Sub
    Private Sub btn_CircBrowseTestExt_Click(sender As Object, e As EventArgs) Handles btn_CircBrowseTestExt.Click
        Dim tempS As String = txt_CircTestExt.Text
        If GC_CC_OutFolder <> "" Then
            dlg_OpenShapefile.InitialDirectory = GC_CC_OutFolder
        End If
        Try
            dlg_OpenShapefile.Title = "Select extent shapefile"
            dlg_OpenShapefile.FileName = ""
            Dim dlgS = dlg_OpenShapefile.ShowDialog()
            dlg_OpenShapefile.Title = "Select test-extent shapefile"
            If dlgS = Windows.Forms.DialogResult.OK Then
                txt_CircTestExt.Text = dlg_OpenShapefile.FileName
                txt_CircTestExt_Leave(Me, Nothing)
                GC_LastFolderCC = IO.Path.GetDirectoryName(GC_CC_TestExt_shp)
            ElseIf dlgS = Windows.Forms.DialogResult.Cancel Then
                txt_CircTestExt.Text = tempS
            End If
        Catch ex As Exception
            MsgBox("enter text")
        End Try
    End Sub
    'input components.shp (GC_CC_Comp_shp)
    Private Sub txt_ComponentsShp_Enter(sender As Object, e As EventArgs) Handles txt_ComponentsShp.Enter
        txt_ComponentsShp.ForeColor = Color.Blue
    End Sub
    Private Sub GC_CC_Comp_shp_exist()
        If (GC_CC_Comp_shp <> "") And My.Computer.FileSystem.FileExists(GC_CC_Comp_shp) Then
            GC_CC_Comp_shp_exists = True
            txt_ComponentsShp.ForeColor = Color.Black
        Else
            GC_CC_Comp_shp_exists = False
            txt_ComponentsShp.ForeColor = Color.Red
        End If
    End Sub
    Private Sub txt_ComponentsShp_Leave(sender As Object, e As EventArgs) Handles txt_ComponentsShp.Leave
        Try
            GC_CC_Comp_shp = txt_ComponentsShp.Text
            GC_CC_Comp_shp_exist()
        Catch ex As Exception
            MsgBox("enter text")
        End Try
    End Sub
    Private Sub btn_ComponentsShp_Click(sender As Object, e As EventArgs) Handles btn_ComponentsShp.Click
        Dim tempS As String = txt_ComponentsShp.Text
        If GC_CC_OutFolder <> "" Then
            dlg_OpenShapefile.InitialDirectory = GC_CC_OutFolder
        End If
        Try
            dlg_OpenShapefile.Title = "Select components shapefile"
            dlg_OpenShapefile.FileName = ""
            Dim dlgS = dlg_OpenShapefile.ShowDialog()
            dlg_OpenShapefile.Title = "Select components shapefile"
            If dlgS = Windows.Forms.DialogResult.OK Then
                txt_ComponentsShp.Text = dlg_OpenShapefile.FileName
                txt_ComponentsShp_Leave(Me, Nothing)
                GC_LastFolderCC = IO.Path.GetDirectoryName(GC_CC_Comp_shp)
            ElseIf dlgS = Windows.Forms.DialogResult.Cancel Then
                txt_ComponentsShp.Text = tempS
            End If
        Catch ex As Exception
            MsgBox("enter text")
        End Try
    End Sub
    'input luse_4fin.tif (GC_CC_luse_4fin)
    Private Sub txt_luse_4fin_file_Enter(sender As Object, e As EventArgs) Handles txt_luse_4fin_file.Enter
        txt_luse_4fin_file.ForeColor = Color.Blue
    End Sub
    Private Sub GC_CC_luse_4fin_exist()
        If (GC_CC_luse_4fin <> "") And My.Computer.FileSystem.FileExists(GC_CC_luse_4fin) Then
            GC_CC_luse_4fin_exists = True
            txt_luse_4fin_file.ForeColor = Color.Black
        Else
            GC_CC_luse_4fin_exists = False
            txt_luse_4fin_file.ForeColor = Color.Red
        End If
    End Sub
    Private Sub txt_luse_4fin_file_Leave(sender As Object, e As EventArgs) Handles txt_luse_4fin_file.Leave
        Try
            GC_CC_luse_4fin = txt_luse_4fin_file.Text
            GC_CC_luse_4fin_exist()
        Catch ex As Exception
            MsgBox("enter text")
        End Try
    End Sub
    Private Sub btn_luse_4fin_file_Click(sender As Object, e As EventArgs) Handles btn_luse_4fin_file.Click
        Dim tempS As String = txt_luse_4fin_file.Text
        If GC_CC_OutFolder <> "" Then
            OpenFileDialog_tif.InitialDirectory = GC_CC_OutFolder
        End If
        Try
            OpenFileDialog_tif.Title = "Select Graphab resistance (.tif) file"
            OpenFileDialog_tif.FileName = ""
            Dim dlgS = OpenFileDialog_tif.ShowDialog()
            OpenFileDialog_tif.Title = "Select luse_4fin.tif file"
            If dlgS = Windows.Forms.DialogResult.OK Then
                txt_luse_4fin_file.Text = OpenFileDialog_tif.FileName
                txt_luse_4fin_file_Leave(Me, Nothing)
                GC_LastFolderCC = IO.Path.GetDirectoryName(GC_CC_luse_4fin)
            ElseIf dlgS = Windows.Forms.DialogResult.Cancel Then
                txt_luse_4fin_file.Text = tempS
            End If
        Catch ex As Exception
            MsgBox("enter text")
        End Try
    End Sub
    'input patches.tif (GC_CC_patches_tif)
    Private Sub txt_patches_tif_Enter(sender As Object, e As EventArgs) Handles txt_patches_tif.Enter
        txt_patches_tif.ForeColor = Color.Blue
    End Sub
    Private Sub GC_CC_patches_tif_exist()
        If (GC_CC_patches_tif <> "") And My.Computer.FileSystem.FileExists(GC_CC_patches_tif) Then
            GC_CC_patches_tif_exists = True
            txt_patches_tif.ForeColor = Color.Black
        Else
            GC_CC_patches_tif_exists = False
            txt_patches_tif.ForeColor = Color.Red
        End If
    End Sub
    Private Sub txt_patches_tif_Leave(sender As Object, e As EventArgs) Handles txt_patches_tif.Leave
        Try
            GC_CC_patches_tif = txt_patches_tif.Text
            GC_CC_patches_tif_exist()
            'update raster existence, colors and combo box lists
            'RasterFolderLeave()  TODO add new function for circuitscape tab
        Catch ex As Exception
            MsgBox("enter text")
        End Try
    End Sub
    Private Sub btn_Patches_tif_Click(sender As Object, e As EventArgs) Handles btn_Patches_tif.Click
        Dim tempS As String = txt_patches_tif.Text
        If GC_CC_OutFolder <> "" Then
            OpenFileDialog_tif.InitialDirectory = GC_CC_OutFolder
        End If
        Try
            OpenFileDialog_tif.Title = "Select Graphab patches (.tif) file"
            OpenFileDialog_tif.FileName = ""
            Dim dlgS = OpenFileDialog_tif.ShowDialog()
            OpenFileDialog_tif.Title = "Select patches.tif file"
            If dlgS = Windows.Forms.DialogResult.OK Then
                txt_patches_tif.Text = OpenFileDialog_tif.FileName
                txt_patches_tif_Leave(Me, Nothing)
                GC_LastFolderCC = IO.Path.GetDirectoryName(GC_CC_patches_tif)
            ElseIf dlgS = Windows.Forms.DialogResult.Cancel Then
                txt_patches_tif.Text = tempS
            End If
        Catch ex As Exception
            MsgBox("enter text")
        End Try
    End Sub
    'set initial folder for raster outputs
    Private Sub CircSetInitial()
        'sets the initial selected directory to GC_CC_OutFolder or GC_LastFolderCC if they has been previously selected
        If GC_CC_OutFolder <> "" Then
            dlg_GetRootFolder.SelectedPath = GC_CC_OutFolder
        ElseIf GC_LastFolderCC <> "" Then
            dlg_GetRootFolder.SelectedPath = GC_LastFolderCC
        End If
    End Sub
    'output new veg raster (GC_CC_OutpNV_Ras)
    Private Sub txt_OutpNV_Ras_Enter(sender As Object, e As EventArgs) Handles txt_OutpNV_Ras.Enter
        txt_OutpNV_Ras.ForeColor = Color.Blue
    End Sub
    Private Sub GC_CC_OutpNV_Ras_exist()
        If (GC_CC_OutpNV_Ras <> "") And My.Computer.FileSystem.DirectoryExists(GC_CC_OutFolder & "\" & GC_CC_OutpNV_Ras) Then
            GC_CC_OutpNV_Ras_exists = True
            txt_OutpNV_Ras.ForeColor = Color.Black
        ElseIf IsValidGrid(GC_CC_OutpNV_Ras) = False Then
            GC_CC_OutpNV_Ras_exists = False
            txt_OutpNV_Ras.ForeColor = Color.Violet
        Else
            GC_CC_OutpNV_Ras_exists = False
            txt_OutpNV_Ras.ForeColor = Color.Red
        End If
    End Sub
    Private Sub txt_OutpNV_Ras_Leave(sender As Object, e As EventArgs) Handles txt_OutpNV_Ras.Leave
        Try
            GC_CC_OutpNV_Ras = txt_OutpNV_Ras.Text
            GC_CC_OutpNV_Ras_exist()
        Catch ex As Exception
            MsgBox("Error in entering output new veg raster name.")
        End Try
    End Sub
    Private Sub btn_OutpNV_Ras_Click(sender As Object, e As EventArgs) Handles btn_OutpNV_Ras.Click
        Try
            CircSetInitial()
            dlg_GetRootFolder.Description = "Select output resistance raster (folder) in Circuitscape outputs folder"
            Dim dlgc = dlg_GetRootFolder.ShowDialog()
            If dlgc = Windows.Forms.DialogResult.OK Then
                txt_OutpNV_Ras.Text = dlg_GetRootFolder.SelectedPath.Split(IO.Path.DirectorySeparatorChar).Last() 'get last part only
                txt_OutpNV_Ras_Leave(Me, Nothing)
            End If
        Catch ex As Exception
            MsgBox("Error in selecting the raster") ': {0}", ex.ToString())
        End Try
    End Sub
    'output costascii.asc (GC_CC_CostAscii)
    Private Sub txt_CostAscii_Enter(sender As Object, e As EventArgs) Handles txt_CostAscii.Enter
        txt_CostAscii.ForeColor = Color.Blue
    End Sub
    Private Sub GC_CC_CostAscii_exist()
        If (GC_CC_CostAscii <> "") And My.Computer.FileSystem.FileExists(GC_CC_CostAscii) Then 'assumes full path entered
            GC_CC_CostAscii_exists = True
            txt_CostAscii.ForeColor = Color.Black
        Else
            GC_CC_CostAscii_exists = False
            txt_CostAscii.ForeColor = Color.Red
        End If
    End Sub
    Private Sub txt_CostAscii_Leave(sender As Object, e As EventArgs) Handles txt_CostAscii.Leave
        Try
            GC_CC_CostAscii = txt_CostAscii.Text
            GC_CC_CostAscii_exist()
        Catch ex As Exception
            MsgBox("Error in entering output cost ascii file name and path.")
        End Try
    End Sub
    Private Sub btn_CostAscii_Click(sender As Object, e As EventArgs) Handles btn_CostAscii.Click
        Try
            If GC_CC_OutFolder <> "" Then
                SaveFileDialog_asc.InitialDirectory = GC_CC_OutFolder
            End If
            SaveFileDialog_asc.Title = "Output resistance ASCII file name (.asc)"
            SaveFileDialog_asc.FileName = ""
            Dim dlgA = SaveFileDialog_asc.ShowDialog()
            If dlgA = Windows.Forms.DialogResult.OK Then
                txt_CostAscii.Text = SaveFileDialog_asc.FileName 'use whole path for this one
                txt_CostAscii_Leave(Me, Nothing)
            End If
        Catch ex As Exception
            MsgBox("Error in selecting the cost ascii file") ': {0}", ex.ToString())
        End Try
    End Sub
    'input infinite cost value parameter (GC_CC_InfiniteCostV) ("1105")
    Private Sub txt_InfiniteCostV_Enter(sender As Object, e As EventArgs) Handles txt_InfiniteCostV.Enter
        txt_InfiniteCostV.ForeColor = Color.Blue
    End Sub
    Private Sub GC_CC_InfiniteCostV_exist()
        If txt_InfiniteCostV.Text <> Nothing Then
            GC_CC_InfiniteCostV = CDbl(txt_InfiniteCostV.Text)
            GC_CC_InfiniteCostV_exists = True
            txt_InfiniteCostV.ForeColor = Color.Black
        Else
            GC_CC_InfiniteCostV_exists = False
            txt_InfiniteCostV.ForeColor = Color.Red
        End If
    End Sub
    Private Sub txt_InfiniteCostV_Leave(sender As Object, e As EventArgs) Handles txt_InfiniteCostV.Leave
        Try
            GC_CC_InfiniteCostV_exist()
        Catch ex As Exception
            MsgBox("The value entered must be a number.")
            txt_MaxDist1.Select()
        End Try
    End Sub
    'output patchtemp raster (GC_CC_PatchTemp_R) ## Now FocalNodesComp raster file
    Private Sub txt_FocalNodesComp_R_Enter(sender As Object, e As EventArgs) Handles txt_FocalNodesComp_R.Enter
        txt_FocalNodesComp_R.ForeColor = Color.Blue
    End Sub
    Private Sub GC_CC_FocalNodesComp_R_exist()
        If ((GC_CC_FocalNodesComp_R <> "") And My.Computer.FileSystem.DirectoryExists(GC_CC_OutFolder & "\" & GC_CC_FocalNodesComp_R)) Then
            GC_CC_FocalNodesComp_R_exists = True
            txt_FocalNodesComp_R.ForeColor = Color.Black
        ElseIf IsValidGrid(GC_CC_FocalNodesComp_R) = False Then
            GC_CC_FocalNodesComp_R_exists = False
            txt_FocalNodesComp_R.ForeColor = Color.Violet
        Else
            GC_CC_FocalNodesComp_R_exists = False
            txt_FocalNodesComp_R.ForeColor = Color.Red
        End If
    End Sub
    Private Sub txt_FocalNodesComp_R_Leave(sender As Object, e As EventArgs) Handles txt_FocalNodesComp_R.Leave
        Try
            GC_CC_FocalNodesComp_R = txt_FocalNodesComp_R.Text
            GC_CC_FocalNodesComp_R_exist()
        Catch ex As Exception
            MsgBox("Error in entering FocalNodesComp raster name.")
        End Try
    End Sub
    Private Sub btn_FocalNodesComp_R_Click(sender As Object, e As EventArgs) Handles btn_FocalNodesComp_R.Click
        Try
            CircSetInitial()
            dlg_GetRootFolder.Description = "Select output Focal Nodes Comp. raster (folder) in Circuitscape outputs folder"
            Dim dlgc = dlg_GetRootFolder.ShowDialog()
            If dlgc = Windows.Forms.DialogResult.OK Then
                txt_FocalNodesComp_R.Text = dlg_GetRootFolder.SelectedPath.Split(IO.Path.DirectorySeparatorChar).Last() 'get last part only
                txt_FocalNodesComp_R_Leave(Me, Nothing)
            End If
        Catch ex As Exception
            MsgBox("Error in selecting the raster") ': {0}", ex.ToString())
        End Try
    End Sub

    '## New FocalNodesComp_asc file (GC_CC_FocalNodesComp_asc) ##
    Private Sub txt_FocalNodesComp_asc_Enter(sender As Object, e As EventArgs) Handles txt_FocalNodesComp_asc.Enter
        txt_FocalNodesComp_asc.ForeColor = Color.Blue
    End Sub
    Private Sub GC_CC_FocalNodesComp_asc_exist()
        If (GC_CC_FocalNodesComp_asc <> "") And My.Computer.FileSystem.FileExists(GC_CC_FocalNodesComp_asc) Then 'assumes full path entered
            GC_CC_FocalNodesComp_asc_exists = True
            txt_FocalNodesComp_asc.ForeColor = Color.Black
        Else
            GC_CC_FocalNodesComp_asc_exists = False
            txt_FocalNodesComp_asc.ForeColor = Color.Red
        End If
    End Sub
    Private Sub txt_FocalNodesComp_asc_Leave(sender As Object, e As EventArgs) Handles txt_FocalNodesComp_asc.Leave
        Try
            GC_CC_FocalNodesComp_asc = txt_FocalNodesComp_asc.Text
            GC_CC_FocalNodesComp_asc_exist()
        Catch ex As Exception
            MsgBox("Error in entering FocalNodesComp ascii file name and path.")
        End Try
    End Sub
    Private Sub btn_FocalNodesComp_asc_Click(sender As Object, e As EventArgs) Handles btn_FocalNodesComp_asc.Click
        Try
            If GC_CC_OutFolder <> "" Then
                SaveFileDialog_asc.InitialDirectory = GC_CC_OutFolder
            End If
            SaveFileDialog_asc.Title = "Output Focal Nodes Comp. ASCII file name (.asc)"
            SaveFileDialog_asc.FileName = ""
            Dim dlgA = SaveFileDialog_asc.ShowDialog()
            If dlgA = Windows.Forms.DialogResult.OK Then
                txt_FocalNodesComp_asc.Text = SaveFileDialog_asc.FileName 'use whole path for this one
                txt_FocalNodesComp_asc_Leave(Me, Nothing)
            End If
        Catch ex As Exception
            MsgBox("Error in selecting the ascii file") ': {0}", ex.ToString())
        End Try
    End Sub

    'output patch circ raster (GC_CC_PatchCirc_R) ## Now FocalNodesPatch raster file
    Private Sub txt_PatchCirc_R_Enter(sender As Object, e As EventArgs) Handles txt_PatchCirc_R.Enter
        txt_PatchCirc_R.ForeColor = Color.Blue
    End Sub
    Private Sub GC_CC_PatchCirc_R_exist()
        If ((GC_CC_PatchCirc_R <> "") And My.Computer.FileSystem.DirectoryExists(GC_CC_OutFolder & "\" & GC_CC_PatchCirc_R)) Then
            GC_CC_PatchCirc_R_exists = True
            txt_PatchCirc_R.ForeColor = Color.Black
        ElseIf IsValidGrid(GC_CC_PatchCirc_R) = False Then
            GC_CC_PatchCirc_R_exists = False
            txt_PatchCirc_R.ForeColor = Color.Violet
        Else
            GC_CC_PatchCirc_R_exists = False
            txt_PatchCirc_R.ForeColor = Color.Red
        End If
    End Sub
    Private Sub txt_PatchCirc_R_Leave(sender As Object, e As EventArgs) Handles txt_PatchCirc_R.Leave
        Try
            GC_CC_PatchCirc_R = txt_PatchCirc_R.Text
            GC_CC_PatchCirc_R_exist()
        Catch ex As Exception
            MsgBox("Error in entering patch circ raster name.")
        End Try
    End Sub
    Private Sub btn_PatchCirc_R_Click(sender As Object, e As EventArgs) Handles btn_PatchCirc_R.Click
        Try
            CircSetInitial()
            dlg_GetRootFolder.Description = "Select output Focal Nodes Patch raster (folder) in Circuitscape outputs folder"
            Dim dlgc = dlg_GetRootFolder.ShowDialog()
            If dlgc = Windows.Forms.DialogResult.OK Then
                txt_PatchCirc_R.Text = dlg_GetRootFolder.SelectedPath.Split(IO.Path.DirectorySeparatorChar).Last() 'get last part only
                txt_PatchCirc_R_Leave(Me, Nothing)
            End If
        Catch ex As Exception
            MsgBox("Error in selecting the raster") ': {0}", ex.ToString())
        End Try
    End Sub
    'output patchasc.asc (GC_CC_PatchAscii) ## Now FocalNodesPatch asii file
    Private Sub txt_PatchAscii_Enter(sender As Object, e As EventArgs) Handles txt_PatchAscii.Enter
        txt_PatchAscii.ForeColor = Color.Blue
    End Sub
    Private Sub GC_CC_PatchAscii_exist()
        If (GC_CC_PatchAscii <> "") And My.Computer.FileSystem.FileExists(GC_CC_PatchAscii) Then 'assumes full path entered
            GC_CC_PatchAscii_exists = True
            txt_PatchAscii.ForeColor = Color.Black
        Else
            GC_CC_PatchAscii_exists = False
            txt_PatchAscii.ForeColor = Color.Red
        End If
    End Sub
    Private Sub txt_PatchAscii_Leave(sender As Object, e As EventArgs) Handles txt_PatchAscii.Leave
        Try
            GC_CC_PatchAscii = txt_PatchAscii.Text
            GC_CC_PatchAscii_exist()
        Catch ex As Exception
            MsgBox("Error in entering output cost ascii file name and path.")
        End Try
    End Sub
    Private Sub btn_PatchAscii_Click(sender As Object, e As EventArgs) Handles btn_PatchAscii.Click
        Try
            If GC_CC_OutFolder <> "" Then
                SaveFileDialog_asc.InitialDirectory = GC_CC_OutFolder
            End If
            SaveFileDialog_asc.Title = "Output Focal Nodes Patch ASCII file name (.asc)"
            SaveFileDialog_asc.FileName = ""
            Dim dlgA = SaveFileDialog_asc.ShowDialog()
            If dlgA = Windows.Forms.DialogResult.OK Then
                txt_PatchAscii.Text = SaveFileDialog_asc.FileName 'use whole path for this one
                txt_PatchAscii_Leave(Me, Nothing)
            End If
        Catch ex As Exception
            MsgBox("Error in selecting the ascii file") ': {0}", ex.ToString())
        End Try
    End Sub

    'select circuitscape tab with click
    Private Sub tb_Circuitscape_Click(sender As Object, e As EventArgs) Handles tb_Circuitscape.Click
        tb_Circuitscape.Select()
    End Sub
    'circuitscape tab panel clicks
    Private Sub Panel13_Click(sender As Object, e As EventArgs) Handles Panel13.Click
        Panel13.Select()
    End Sub
    Private Sub Panel14_Click(sender As Object, e As EventArgs) Handles Panel14.Click
        Panel14.Select()
    End Sub
    Private Sub Panel15_Click(sender As Object, e As EventArgs) Handles Panel15.Click
        Panel15.Select()
    End Sub

#End Region

#Region "Controls on script outputs tab"
    'button to save script output to file
    Private Sub btn_saveScriptOutput_Click(sender As Object, e As EventArgs) Handles btn_saveScriptOutput.Click
        'open save file dialog box
        Dim SaveScriptOutput As String = GC_appPath & "\Scenarios\ScriptOutput.txt"
        Try
            Dim Rst = dlg_SaveF.ShowDialog() 'find save file name
            If Rst = Windows.Forms.DialogResult.OK Then
                SaveScriptOutput = dlg_SaveF.FileName
                Try
                    Dim sw As New IO.StreamWriter(SaveScriptOutput, False) 'overwrite
                    sw.Write(txt_PythonOutput.Text)
                    sw.Close()
                Catch

                End Try
            ElseIf Rst = Windows.Forms.DialogResult.Cancel Then
                MsgBox("Save file was cancelled.")
            End If
        Catch ex As Exception
            MsgBox("there was an error with saving the script output")
        End Try
    End Sub
    'run scripts for selected rasters on tabs 1 and 2 together on scripts tab
    Private Sub btn_RunCombined_Click(sender As Object, e As EventArgs) Handles btn_RunCombined.Click
        Try
            'what to do here
            'disable relevant controls
            btn_RunCombined.Enabled = False
            'check which check boxes have been selected
            If chk_SelDefL.Checked = True Then
                btn_RunConvLusetoRaster_Click(Me, Nothing)
            End If
            If chk_SelDefGC.Checked = True Then
                btn_CreateGapCross_Click(Me, Nothing)
            End If
            If chk_SelDefGCO.Checked = True Then
                btn_GapCrossOld_Click(Me, Nothing)
            End If
            If chk_selS1.Checked = True Then
                btn_RunScen1_Click(Me, Nothing)
            End If
            If chk_selS2a.Checked = True Then
                btn_RunScen2a_Click(Me, Nothing)
            End If
            If chk_sel2b.Checked = True Then
                btn_RunScen2b_Click(Me, Nothing)
            End If
            If chk_sel3.Checked = True Then
                btn_RunScen3_Click(Me, Nothing)
            End If
            If chk_selS4.Checked = True Then
                btn_RunScen4_Click(Me, Nothing)
            End If

            'identify which inputs are missing, cancel and prompt for changes
            'notice to say which outputs will be overwritten
            'run scripts in sequence
            'unselect check boxes
            btn_RunCombined.Enabled = False
        Catch
            MsgBox("There was an error with running the selected scripts")
        End Try
    End Sub
    'run graphab script from scripts tab
    Private Sub btn_ProcessrastersTab4_Click(sender As Object, e As EventArgs) Handles btn_ProcessrastersTab4.Click
        btn_Run_GC_Click(Me, Nothing)
    End Sub
    'run circuitscape scripts from scripts tab
    Private Sub btn_RunCircuitscape_Tab4_Click(sender As Object, e As EventArgs) Handles btn_RunCircuitscape_Tab4.Click
        'add code here
        Try
            'what to do here
            'disable relevant controls
            btn_RunCircuitscape_Tab4.Enabled = False
            'check which check boxes have been selected
            If chk_Circ1.Checked = True Then
                btn_RunCirc1_Click(Me, Nothing)
            End If
            If chk_Circ2.Checked = True Then
                btn_RunCirc2_Click(Me, Nothing)
            End If
            If chk_Circ3.Checked = True Then
                btn_RunCirc3_Click(Me, Nothing)
            End If
            'identify which inputs are missing, cancel and prompt for changes
            'notice to say which outputs will be overwritten
            'run scripts in sequence
            'unselect check boxes
            btn_RunCircuitscape_Tab4.Enabled = False
        Catch ex As Exception
            MsgBox("There was an error with running the selected scripts")
        End Try
    End Sub
    'clear error output - Tab 4
    Private Sub btn_ClearErrorOutput_Click(sender As Object, e As EventArgs) Handles btn_ClearErrorOutput.Click
        txt_PythonError.Text = ""
    End Sub
    'clear script output - Tab 4
    Private Sub btn_ClearScriptOutput_Click(sender As Object, e As EventArgs) Handles btn_ClearScriptOutput.Click
        txt_PythonOutput.Text = ""
    End Sub
    'select tab 4 (script outputs) with click
    Private Sub tb_4_Click(sender As Object, e As EventArgs) Handles tb_4.Click
        tb_4.Select()
    End Sub
#End Region

#Region "enter or leave text boxes on config tab"
    'entering Python Application text box - Tab 5
    Private Sub txt_ArcPythonApp_Enter(sender As Object, e As EventArgs) Handles txt_ArcPythonApp.Enter
        txt_ArcPythonApp.ForeColor = Color.Blue
    End Sub
    'leaving Python Application text box - Tab 5
    Private Sub txt_ArcPythonApp_Leave(sender As Object, e As EventArgs) Handles txt_ArcPythonApp.Leave
        If ((txt_ArcPythonApp.Text <> "") And My.Computer.FileSystem.FileExists(txt_ArcPythonApp.Text)) Or txt_ArcPythonApp.Text = "python" Then  '
            GC_theApplication = txt_ArcPythonApp.Text
            GC_theApplication_exists = True
            txt_ArcPythonApp.ForeColor = Color.Black
        Else
            GC_theApplication_exists = False
            txt_ArcPythonApp.ForeColor = Color.Red
        End If
    End Sub
    'entering Scripts folder text box - config tab
    Private Sub txt_ScriptsFolder_Enter(sender As Object, e As EventArgs) Handles txt_ScriptsFolder.Enter
        txt_ScriptsFolder.ForeColor = Color.Blue
    End Sub
    'leaving Scripts folder text box  - config tab
    Private Sub txt_ScriptsFolder_Leave(sender As Object, e As EventArgs) Handles txt_ScriptsFolder.Leave
        If (txt_ScriptsFolder.Text <> "") And My.Computer.FileSystem.DirectoryExists(txt_ScriptsFolder.Text) Then  '
            GC_ScriptsFolder = txt_ScriptsFolder.Text
            'If Microsoft.VisualBasic.Left(txt_ScriptsFolder.Text, 1) = Chr(34) And Microsoft.VisualBasic.Right(txt_ScriptsFolder.Text, 1) = Chr(34) Then
            'GC_ScriptsFolder = txt_ScriptsFolder.Text
            'Else
            'GC_ScriptsFolder = Chr(34) & txt_ScriptsFolder.Text & Chr(34)
            'End If
            'Chr(34) = '"'character (added to path string in case it contains spaces which would interfere with script execution)
            GC_ScriptsFolder_exists = True
            txt_ScriptsFolder.ForeColor = Color.Black
        Else
            GC_ScriptsFolder_exists = False
            txt_ScriptsFolder.ForeColor = Color.Red
        End If
        'do existence checks for the scripts
        txt_ScriptLuseToRaster_Leave(Me, Nothing)
        txt_ScriptCreateGapCross_Leave(Me, Nothing)
        txt_ScriptcreateGC_old_Leave(Me, Nothing)
        txt_ScriptScenario1_Leave(Me, Nothing)
        txt_ScriptScenario2a_Leave(Me, Nothing)
        txt_ScriptScenario2b_Leave(Me, Nothing)
        txt_ScriptScenario3_Leave(Me, Nothing)
        txt_scriptScenario4_Leave(Me, Nothing)
        txt_GC_ScriptName_Leave(Me, Nothing)
        txt_ScriptCirc1_Leave(Me, Nothing)
        txt_ScriptCirc2_Leave(Me, Nothing)
        txt_ScriptCirc3_Leave(Me, Nothing)
    End Sub
    'entering ScriptLuseToRaster text box - config tab
    Private Sub txt_ScriptLuseToRaster_Enter(sender As Object, e As EventArgs) Handles txt_ScriptLuseToRaster.Enter
        txt_ScriptLuseToRaster.ForeColor = Color.Blue
    End Sub

    Private Sub txt_ScriptLuseToRaster_Leave(sender As Object, e As EventArgs) Handles txt_ScriptLuseToRaster.Leave
        If (txt_ScriptLuseToRaster.Text <> "") And My.Computer.FileSystem.FileExists(txt_ScriptsFolder.Text & "/" & txt_ScriptLuseToRaster.Text) Then
            GC_Scr_DefL = txt_ScriptLuseToRaster.Text
            GC_Scr_DefL_exists = True
            txt_ScriptLuseToRaster.ForeColor = Color.Black
        Else
            GC_Scr_DefL_exists = False
            txt_ScriptLuseToRaster.ForeColor = Color.Red
        End If
    End Sub
    'entering ScriptCreateGapCross text box - config tab
    Private Sub txt_ScriptCreateGapCross_Enter(sender As Object, e As EventArgs) Handles txt_ScriptCreateGapCross.Enter
        txt_ScriptCreateGapCross.ForeColor = Color.Blue
    End Sub
    Private Sub txt_ScriptCreateGapCross_Leave(sender As Object, e As EventArgs) Handles txt_ScriptCreateGapCross.Leave
        If (txt_ScriptCreateGapCross.Text <> "") And My.Computer.FileSystem.FileExists(txt_ScriptsFolder.Text & "/" & txt_ScriptCreateGapCross.Text) Then  'assumes full path entered
            GC_Scr_DefGC = txt_ScriptCreateGapCross.Text
            GC_Scr_DefGC_exists = True
            txt_ScriptCreateGapCross.ForeColor = Color.Black
        Else
            GC_Scr_DefGC_exists = False
            txt_ScriptCreateGapCross.ForeColor = Color.Red
        End If
    End Sub
    'entering ScriptcreateGC_old text box - config tab
    Private Sub txt_ScriptcreateGC_old_Enter(sender As Object, e As EventArgs) Handles txt_ScriptcreateGC_old.Enter
        txt_ScriptcreateGC_old.ForeColor = Color.Blue
    End Sub
    Private Sub txt_ScriptcreateGC_old_Leave(sender As Object, e As EventArgs) Handles txt_ScriptcreateGC_old.Leave
        If (txt_ScriptcreateGC_old.Text <> "") And My.Computer.FileSystem.FileExists(txt_ScriptsFolder.Text & "/" & txt_ScriptcreateGC_old.Text) Then  'assumes full path entered
            GC_Scr_DefO = txt_ScriptcreateGC_old.Text
            GC_Scr_DefO_exists = True
            txt_ScriptcreateGC_old.ForeColor = Color.Black
        Else
            GC_Scr_DefO_exists = False
            txt_ScriptcreateGC_old.ForeColor = Color.Red
        End If
    End Sub
    'entering ScriptScenario1 text box - config tab
    Private Sub txt_ScriptScenario1_Enter(sender As Object, e As EventArgs) Handles txt_ScriptScenario1.Enter
        txt_ScriptScenario1.ForeColor = Color.Blue
    End Sub
    Private Sub txt_ScriptScenario1_Leave(sender As Object, e As EventArgs) Handles txt_ScriptScenario1.Leave
        If (txt_ScriptScenario1.Text <> "") And My.Computer.FileSystem.FileExists(txt_ScriptsFolder.Text & "/" & txt_ScriptScenario1.Text) Then  'assumes full path entered
            GC_Scr_Scen1 = txt_ScriptScenario1.Text
            GC_Scr_Scen1_exists = True
            txt_ScriptScenario1.ForeColor = Color.Black
        Else
            GC_Scr_Scen1_exists = False
            txt_ScriptScenario1.ForeColor = Color.Red
        End If
    End Sub
    'entering ScriptScenario2a text box - config tab
    Private Sub txt_ScriptScenario2a_Enter(sender As Object, e As EventArgs) Handles txt_ScriptScenario2a.Enter
        txt_ScriptScenario2a.ForeColor = Color.Blue
    End Sub
    Private Sub txt_ScriptScenario2a_Leave(sender As Object, e As EventArgs) Handles txt_ScriptScenario2a.Leave
        If (txt_ScriptScenario2a.Text <> "") And My.Computer.FileSystem.FileExists(txt_ScriptsFolder.Text & "/" & txt_ScriptScenario2a.Text) Then  'assumes full path entered
            GC_Scr_Scen2a = txt_ScriptScenario2a.Text
            GC_Scr_Scen2a_exists = True
            txt_ScriptScenario2a.ForeColor = Color.Black
        Else
            GC_Scr_Scen2a_exists = False
            txt_ScriptScenario2a.ForeColor = Color.Red
        End If
    End Sub
    'entering ScriptScenario2b text box - config tab
    Private Sub txt_ScriptScenario2b_Enter(sender As Object, e As EventArgs) Handles txt_ScriptScenario2b.Enter
        txt_ScriptScenario2b.ForeColor = Color.Blue
    End Sub
    Private Sub txt_ScriptScenario2b_Leave(sender As Object, e As EventArgs) Handles txt_ScriptScenario2b.Leave
        If (txt_ScriptScenario2b.Text <> "") And My.Computer.FileSystem.FileExists(txt_ScriptsFolder.Text & "/" & txt_ScriptScenario2b.Text) Then  'assumes full path entered
            GC_Scr_Scen2b = txt_ScriptScenario2b.Text
            GC_Scr_Scen2b_exists = True
            txt_ScriptScenario2b.ForeColor = Color.Black
        Else
            GC_Scr_Scen2b_exists = False
            txt_ScriptScenario2b.ForeColor = Color.Red
        End If
    End Sub
    'entering ScriptScenario3 text box - config tab
    Private Sub txt_ScriptScenario3_Enter(sender As Object, e As EventArgs) Handles txt_ScriptScenario3.Enter
        txt_ScriptScenario3.ForeColor = Color.Blue
    End Sub
    Private Sub txt_ScriptScenario3_Leave(sender As Object, e As EventArgs) Handles txt_ScriptScenario3.Leave
        If (txt_ScriptScenario3.Text <> "") And My.Computer.FileSystem.FileExists(txt_ScriptsFolder.Text & "/" & txt_ScriptScenario3.Text) Then  'assumes full path entered
            GC_Scr_Scen3 = txt_ScriptScenario3.Text
            GC_Scr_Scen3_exists = True
            txt_ScriptScenario3.ForeColor = Color.Black
        Else
            GC_Scr_Scen3_exists = False
            txt_ScriptScenario3.ForeColor = Color.Red
        End If
    End Sub
    'entering ScriptScenario4 text box - config tab
    Private Sub txt_scriptScenario4_Enter(sender As Object, e As EventArgs) Handles txt_scriptScenario4.Enter
        txt_scriptScenario4.ForeColor = Color.Blue
    End Sub
    Private Sub txt_scriptScenario4_Leave(sender As Object, e As EventArgs) Handles txt_scriptScenario4.Leave
        If (txt_scriptScenario4.Text <> "") And My.Computer.FileSystem.FileExists(txt_ScriptsFolder.Text & "/" & txt_scriptScenario4.Text) Then  'assumes full path entered
            GC_Scr_Scen4 = txt_scriptScenario4.Text
            GC_Scr_Scen4_exists = True
            txt_scriptScenario4.ForeColor = Color.Black
        Else
            GC_Scr_Scen4_exists = False
            txt_scriptScenario4.ForeColor = Color.Red
        End If
    End Sub
    'entering GAP_CLoSR Script text box -config tab
    Private Sub txt_GC_ScriptName_Enter(sender As Object, e As EventArgs) Handles txt_GC_ScriptName.Enter
        txt_GC_ScriptName.ForeColor = Color.Blue
    End Sub
    Private Sub txt_GC_ScriptName_Leave(sender As Object, e As EventArgs) Handles txt_GC_ScriptName.Leave
        If (txt_GC_ScriptName.Text <> "") And My.Computer.FileSystem.FileExists(txt_ScriptsFolder.Text & "/" & txt_GC_ScriptName.Text) Then
            GC_theScript = txt_GC_ScriptName.Text
            GC_theScript_exists = True
            txt_GC_ScriptName.ForeColor = Color.Black
        Else
            GC_theScript_exists = False
            txt_GC_ScriptName.ForeColor = Color.Red
        End If

    End Sub
    'entering ScriptCirc1 text box - config tab
    Private Sub txt_ScriptCirc1_Enter(sender As Object, e As EventArgs) Handles txt_ScriptCirc1.Enter
        txt_ScriptCirc1.ForeColor = Color.Blue
    End Sub
    Private Sub txt_ScriptCirc1_Leave(sender As Object, e As EventArgs) Handles txt_ScriptCirc1.Leave

        If (txt_ScriptCirc1.Text <> "") And My.Computer.FileSystem.FileExists(txt_ScriptsFolder.Text & "/" & txt_ScriptCirc1.Text) Then
            GC_ScriptCirc1 = txt_ScriptCirc1.Text
            GC_ScriptCirc1_exists = True
            txt_ScriptCirc1.ForeColor = Color.Black
        Else
            GC_ScriptCirc1_exists = False
            txt_ScriptCirc1.ForeColor = Color.Red
        End If

    End Sub
    'entering ScriptCirc2 text box - config tab
    Private Sub txt_ScriptCirc2_Enter(sender As Object, e As EventArgs) Handles txt_ScriptCirc2.Enter
        txt_ScriptCirc2.ForeColor = Color.Blue
    End Sub
    Private Sub txt_ScriptCirc2_Leave(sender As Object, e As EventArgs) Handles txt_ScriptCirc2.Leave
        If (txt_ScriptCirc2.Text <> "") And My.Computer.FileSystem.FileExists(txt_ScriptsFolder.Text & "/" & txt_ScriptCirc2.Text) Then  'assumes full path entered
            GC_ScriptCirc2 = txt_ScriptCirc2.Text
            GC_ScriptCirc2_exists = True
            txt_ScriptCirc2.ForeColor = Color.Black
        Else
            GC_ScriptCirc2_exists = False
            txt_ScriptCirc2.ForeColor = Color.Red
        End If
    End Sub
    'entering ScriptCirc3 text box - config tab
    Private Sub txt_ScriptCirc3_Enter(sender As Object, e As EventArgs) Handles txt_ScriptCirc3.Enter
        txt_ScriptCirc3.ForeColor = Color.Blue
    End Sub
    Private Sub txt_ScriptCirc3_Leave(sender As Object, e As EventArgs) Handles txt_ScriptCirc3.Leave
        If (txt_ScriptCirc3.Text <> "") And My.Computer.FileSystem.FileExists(txt_ScriptsFolder.Text & "/" & txt_ScriptCirc3.Text) Then  'assumes full path entered
            GC_ScriptCirc3 = txt_ScriptCirc3.Text
            GC_ScriptCirc3_exists = True
            txt_ScriptCirc3.ForeColor = Color.Black
        Else
            GC_ScriptCirc3_exists = False
            txt_ScriptCirc3.ForeColor = Color.Red
        End If
    End Sub
#End Region

#Region "Controls on config tab"
    'button on config tab to set scripts folder as appPath & "\GAP_CLoSR_Code"
    Private Sub btn_SetScriptsFolder_Click(sender As Object, e As EventArgs) Handles btn_SetScriptsFolder.Click
        GC_ScriptsFolder = GC_appPath & "\GAP_CLoSR_Code"
        txt_ScriptsFolder.Text = GC_ScriptsFolder
    End Sub
    'ArcGIS Python file and path browser button - Tab 5
    Private Sub btn_BrowseArcPyPath_Click(sender As Object, e As EventArgs) Handles btn_BrowseArcPyPath.Click
        Dim tempText2 As String = txt_ArcPythonApp.Text

        Try
            Dim dlga = dlg_FindArcPyPath.ShowDialog()
            If dlga = Windows.Forms.DialogResult.OK Then
                txt_ArcPythonApp.Text = CheckForSpaces(dlg_FindArcPyPath.FileName)
                If ((txt_ArcPythonApp.Text <> "") And My.Computer.FileSystem.FileExists(txt_ArcPythonApp.Text)) Or txt_ArcPythonApp.Text = "python" Then  '
                    GC_theApplication = txt_ArcPythonApp.Text
                    GC_theApplication_exists = True
                    txt_ArcPythonApp.ForeColor = Color.Black
                Else
                    GC_theApplication_exists = False
                    txt_ArcPythonApp.ForeColor = Color.Red
                End If
            ElseIf dlga = Windows.Forms.DialogResult.Cancel Then
                txt_ArcPythonApp.Text = tempText2
            End If
        Catch ex As Exception
            MsgBox("Error in selecting python.exe application")
        End Try
    End Sub
    'saves the Python Configuration settings -Tab 5
    Private Sub btn_SavePyExePth_Click(sender As Object, e As EventArgs) Handles btn_SavePyExePth.Click
        Try
            'the configs.txt file
            Dim sw As New IO.StreamWriter(GC_PPath, False) 'overwrite
            sw.WriteLine("ArcGISPythonApp=" & GC_theApplication) '
            sw.WriteLine("__Scripts__")
            sw.WriteLine("Scr_DefL=" & GC_Scr_DefL)
            sw.WriteLine("Scr_DefG=" & GC_Scr_DefGC)
            sw.WriteLine("Scr_DefO=" & GC_Scr_DefO)
            sw.WriteLine("Scr_Scen1=" & GC_Scr_Scen1)
            sw.WriteLine("Scr_Scen2a=" & GC_Scr_Scen2a)
            sw.WriteLine("Scr_Scen2b=" & GC_Scr_Scen2b)
            sw.WriteLine("Scr_Scen3=" & GC_Scr_Scen3)
            sw.WriteLine("Scr_Scen4=" & GC_Scr_Scen4)
            sw.WriteLine("Scr_Ghab=" & GC_theScript)
            sw.WriteLine("Scr_Circ1=" & GC_ScriptCirc1)
            sw.WriteLine("Scr_Circ2=" & GC_ScriptCirc2)
            sw.WriteLine("Scr_Circ3=" & GC_ScriptCirc3)
            sw.Close()
            GC_PyAppSaved = True
        Catch ex As Exception
            MsgBox("Error while saving Configs.txt file")
        End Try
        Try
            'the AltScriptPath.txt file
            Dim swf As New IO.StreamWriter(GC_appPath & "\AltScriptPath.txt", False) 'overwrite
            swf.WriteLine(GC_ScriptsFolder) '
            swf.Close()
        Catch ex As Exception
            MsgBox("Error while saving AltScriptPath.txt file")
        End Try
    End Sub
    'loads the Python Configuration settings -Tab 5
    Private Sub btn_LoadPyExePth_Click(sender As Object, e As EventArgs) Handles btn_LoadPyExePth.Click

        If GC_ConfigsFile_exists = True Then
            Try
                'load variables
                Dim swrd As New IO.StreamReader(GC_PPath)
                Dim Linedata As String
                Dim ConfigReadError As String = ""
                'read variables text from setting file, display them in the text boxes and convert to variables

                'ArcGISPythonApp="C:\Python27\ArcGIS10.2\python.exe"
                '__Scripts__
                'Scr_DefL = Convert_luseToRaster.py
                'Scr_DefGC = CreateGapCrossingLayer.py
                'Scr_DefO = CreateGapCrossingLayerOLD.py
                'Scr_Scen1=1_CreateRasterChangeLayer.py
                'Scr_Scen2a=2a_ModifyVegetationLayer_Removal.py
                'Scr_Scen2b=2b_ModifyVegetationLayer_Addition.py
                'Scr_Scen3=3_CreateGapCrossingLayer.py
                'Scr_Scen4=4_ModifyLuseLayer_AddInfra.py
                'Scr_Ghab=GAP_CLoSR_ArgV4.py
                'Scr_Circ1=ConvCostLayerToCirc_GC.py
                'Scr_Circ2=ConvGraphabPatchToCirc_GC.py
                'Scr_Circ3=ConvGraphabPatchesAndLabelToCirc_GC.py

                'config line 1
                Linedata = swrd.ReadLine() 'python application
                If Linedata.Split(CChar("=")).First() = "ArcGISPythonApp" Then
                    txt_ArcPythonApp.Text = Linedata.Split(CChar("=")).Last()

                    'add some code to check that this exists
                    If ((txt_ArcPythonApp.Text <> "") And My.Computer.FileSystem.FileExists(txt_ArcPythonApp.Text)) = True Then
                        GC_theApplication = txt_ArcPythonApp.Text
                        GC_theApplication_exists = True
                        txt_ArcPythonApp.ForeColor = Color.Black
                        '    ElseIf ((txt_ArcPythonApp.Text <> "") And 'My.Computer.FileSystem.FileExists(txt_ArcPythonApp.Text)) = False Then
                        '       GC_theApplication = txt_ArcPythonApp.Text
                        '       GC_theApplication_exists = False
                        '       txt_ArcPythonApp.ForeColor = Color.Red
                    Else
                        txt_ArcPythonApp.Text = "python"
                        GC_theApplication = "python"
                        GC_theApplication_exists = True
                        txt_ArcPythonApp.ForeColor = Color.Black
                    End If

                Else
                    ConfigReadError = "ArcGISPythonApp"
                End If
                'config line 2
                Linedata = swrd.ReadLine() 'currently this line contains no data (= '__Scripts__' heading)
                'scripts folder. This path needs to be added to the script names.
                txt_ScriptsFolder.Text = GC_appPath & "\GAP_CLoSR_Code"
                GC_ScriptsFolder = GC_appPath & "\GAP_CLoSR_Code"

                'config line 3
                Linedata = swrd.ReadLine()  ' default luse script
                If Linedata.Split(CChar("=")).First() = "Scr_DefL" Then
                    txt_ScriptLuseToRaster.Text = Linedata.Split(CChar("=")).Last()
                    GC_Scr_DefL = txt_ScriptLuseToRaster.Text
                Else
                    ConfigReadError = "Scr_DefL"
                End If
                'config line 4
                Linedata = swrd.ReadLine() 'default gap cross script
                If Linedata.Split(CChar("=")).First() = "Scr_DefG" Then
                    txt_ScriptCreateGapCross.Text = Linedata.Split(CChar("=")).Last()
                    GC_Scr_DefGC = txt_ScriptCreateGapCross.Text
                Else
                    ConfigReadError = "Scr_DefG"
                End If
                'config line 5
                Linedata = swrd.ReadLine()  'default gap cross script old
                If Linedata.Split(CChar("=")).First() = "Scr_DefO" Then
                    txt_ScriptcreateGC_old.Text = Linedata.Split(CChar("=")).Last()
                    GC_Scr_DefO = txt_ScriptcreateGC_old.Text
                Else
                    ConfigReadError = "Scr_DefO"
                End If
                'config line 6
                Linedata = swrd.ReadLine()  'scenario 1 script
                If Linedata.Split(CChar("=")).First() = "Scr_Scen1" Then
                    txt_ScriptScenario1.Text = Linedata.Split(CChar("=")).Last()
                    GC_Scr_Scen1 = txt_ScriptScenario1.Text
                Else
                    ConfigReadError = "Scr_Scen1"
                End If
                'config line 7
                Linedata = swrd.ReadLine()  'scenario 2a script
                If Linedata.Split(CChar("=")).First() = "Scr_Scen2a" Then
                    txt_ScriptScenario2a.Text = Linedata.Split(CChar("=")).Last()
                    GC_Scr_Scen2a = txt_ScriptScenario2a.Text
                Else
                    ConfigReadError = "Scr_Scen2a"
                End If
                'config line 8
                Linedata = swrd.ReadLine()  'scenario 2b script
                If Linedata.Split(CChar("=")).First() = "Scr_Scen2b" Then
                    txt_ScriptScenario2b.Text = Linedata.Split(CChar("=")).Last()
                    GC_Scr_Scen2b = txt_ScriptScenario2b.Text
                Else
                    ConfigReadError = "Scr_Scen2b"
                End If
                'config line 9
                Linedata = swrd.ReadLine()  'scenario 3 script
                If Linedata.Split(CChar("=")).First() = "Scr_Scen3" Then
                    txt_ScriptScenario3.Text = Linedata.Split(CChar("=")).Last()
                    GC_Scr_Scen3 = txt_ScriptScenario3.Text
                Else
                    ConfigReadError = "Scr_Scen3"
                End If
                'config line 10
                Linedata = swrd.ReadLine()  'scenario 4 script
                If Linedata.Split(CChar("=")).First() = "Scr_Scen4" Then
                    txt_scriptScenario4.Text = Linedata.Split(CChar("=")).Last()
                    GC_Scr_Scen4 = txt_scriptScenario4.Text
                Else
                    ConfigReadError = "Scr_Scen4"
                End If
                'config line 11
                Linedata = swrd.ReadLine()  'GAP_CLoSR script
                If Linedata.Split(CChar("=")).First() = "Scr_Ghab" Then
                    txt_GC_ScriptName.Text = Linedata.Split(CChar("=")).Last()
                    GC_theScript = txt_GC_ScriptName.Text
                Else
                    ConfigReadError = "Scr_Ghab"
                End If
                'config line 12
                Linedata = swrd.ReadLine()  'ConvCostLayerToCirc_GC.py
                If Linedata.Split(CChar("=")).First() = "Scr_Circ1" Then
                    txt_ScriptCirc1.Text = Linedata.Split(CChar("=")).Last()
                    GC_ScriptCirc1 = txt_ScriptCirc1.Text
                Else
                    ConfigReadError = "Scr_Circ1"
                End If
                'config line 13
                Linedata = swrd.ReadLine()  'ConvGraphabPatchToCirc_GC.py
                If Linedata.Split(CChar("=")).First() = "Scr_Circ2" Then
                    txt_ScriptCirc2.Text = Linedata.Split(CChar("=")).Last()
                    GC_ScriptCirc2 = txt_ScriptCirc2.Text
                Else
                    ConfigReadError = "Scr_Circ2"
                End If
                'config line 14
                Linedata = swrd.ReadLine()  'ConvGraphabPatchesAndLabelToCirc_GC.py
                If Linedata.Split(CChar("=")).First() = "Scr_Circ3" Then
                    txt_ScriptCirc3.Text = Linedata.Split(CChar("=")).Last()
                    GC_ScriptCirc3 = txt_ScriptCirc3.Text
                Else
                    ConfigReadError = "Scr_Circ3"
                End If

                'close the settings file
                swrd.Close()
                'flag as python exe application selected
                GC_PyAppSelected = True
                If ConfigReadError = "" Then
                    'check if scripts exist
                    Dim msgF As String = ""
                    If (GC_ScriptsFolder & "/" & GC_Scr_DefL <> "") And My.Computer.FileSystem.FileExists(GC_ScriptsFolder & "/" & GC_Scr_DefL) Then
                        GC_Scr_DefL_exists = True
                    Else
                        msgF = msgF & GC_Scr_DefL & vbCrLf
                    End If
                    If (GC_ScriptsFolder & "/" & GC_Scr_DefGC <> "") And My.Computer.FileSystem.FileExists(GC_ScriptsFolder & "/" & GC_Scr_DefGC) Then
                        GC_Scr_DefGC_exists = True
                    Else
                        msgF = msgF & GC_Scr_DefGC & vbCrLf
                    End If
                    If (GC_ScriptsFolder & "/" & GC_Scr_DefO <> "") And My.Computer.FileSystem.FileExists(GC_ScriptsFolder & "/" & GC_Scr_DefO) Then
                        GC_Scr_DefO_exists = True
                    Else
                        msgF = msgF & GC_Scr_DefO & vbCrLf
                    End If
                    If (GC_ScriptsFolder & "/" & GC_Scr_Scen1 <> "") And My.Computer.FileSystem.FileExists(GC_ScriptsFolder & "/" & GC_Scr_Scen1) Then
                        GC_Scr_Scen1_exists = True
                    Else
                        msgF = msgF & GC_Scr_Scen1 & vbCrLf
                    End If
                    If (GC_ScriptsFolder & "/" & GC_Scr_Scen2a <> "") And My.Computer.FileSystem.FileExists(GC_ScriptsFolder & "/" & GC_Scr_Scen2a) Then
                        GC_Scr_Scen2a_exists = True
                    Else
                        msgF = msgF & GC_Scr_Scen2a & vbCrLf
                    End If
                    If (GC_ScriptsFolder & "/" & GC_Scr_Scen2b <> "") And My.Computer.FileSystem.FileExists(GC_ScriptsFolder & "/" & GC_Scr_Scen2b) Then
                        GC_Scr_Scen2b_exists = True
                    Else
                        msgF = msgF & GC_Scr_Scen2b & vbCrLf
                    End If
                    If (GC_ScriptsFolder & "/" & GC_Scr_Scen3 <> "") And My.Computer.FileSystem.FileExists(GC_ScriptsFolder & "/" & GC_Scr_Scen3) Then
                        GC_Scr_Scen3_exists = True
                    Else
                        msgF = msgF & GC_Scr_Scen3 & vbCrLf
                    End If
                    If (GC_ScriptsFolder & "/" & GC_Scr_Scen4 <> "") And My.Computer.FileSystem.FileExists(GC_ScriptsFolder & "/" & GC_Scr_Scen4) Then
                        GC_Scr_Scen4_exists = True
                    Else
                        msgF = msgF & GC_Scr_Scen4 & vbCrLf
                    End If
                    If (GC_ScriptsFolder & "/" & GC_theScript <> "") And My.Computer.FileSystem.FileExists(GC_ScriptsFolder & "/" & GC_theScript) Then
                        GC_theScript_exists = True
                    Else
                        msgF = msgF & GC_theScript & vbCrLf
                    End If
                    If (GC_ScriptsFolder & "/" & GC_ScriptCirc1 <> "") And My.Computer.FileSystem.FileExists(GC_ScriptsFolder & "/" & GC_ScriptCirc1) Then
                        GC_ScriptCirc1_exists = True
                    Else
                        msgF = msgF & GC_ScriptCirc1 & vbCrLf
                    End If
                    If (GC_ScriptsFolder & "/" & GC_ScriptCirc2 <> "") And My.Computer.FileSystem.FileExists(GC_ScriptsFolder & "/" & GC_ScriptCirc2) Then
                        GC_ScriptCirc2_exists = True
                    Else
                        msgF = msgF & GC_ScriptCirc2 & vbCrLf
                    End If
                    If (GC_ScriptsFolder & "/" & GC_ScriptCirc3 <> "") And My.Computer.FileSystem.FileExists(GC_ScriptsFolder & "/" & GC_ScriptCirc3) Then
                        GC_ScriptCirc3_exists = True
                    Else
                        msgF = msgF & GC_ScriptCirc3 & vbCrLf
                    End If
                    If msgF <> "" Then
                        MsgBox("The following scripts appear to be missing or their" & vbCrLf & "names do not match the names in the config file" & vbCrLf & msgF)
                    End If
                Else
                    Dim msgC As String = "There was an error with the config settings file." & vbCrLf
                    msgC = msgC & "The headings do not match the expected format. (Scr_xxx=)"
                    MsgBox(msgC)
                End If
            Catch ex As Exception
                MsgBox("There was an error in loading the configs file.")
            End Try
        Else
            txt_ArcPythonApp.Text = "python"
            GC_theApplication = "python"
            MsgBox("A config. file 'Configs.txt' was not be found in Scripts folder")
        End If

    End Sub
    'select the configs tab by clicking on it - Tab 5
    Private Sub tb_Config_Click(sender As Object, e As EventArgs) Handles tb_Config.Click
        tb_Config.Select()
    End Sub
    'scripts list panel on cnfig tab
    Private Sub Panel1_Click(sender As Object, e As EventArgs) Handles Panel1.Click
        Panel1.Select()
    End Sub
    'other settings panel on config tab
    Private Sub Panel19_Click(sender As Object, e As EventArgs) Handles Panel19.Click
        Panel19.Select()
    End Sub
#End Region
    'is the text a valid ESRI GRID name
    Private Function IsValidGrid(InR As String) As Boolean
        'does it contain spaces, start with a numeral or is greater than 13 characters
        Dim Msg2 As String = ""
        If IsNothing(InR) Then 'no data had been entered
            Return False
        ElseIf Len(InR) > 13 Or InR.Contains(" ") Or Char.IsDigit(CChar(Microsoft.VisualBasic.Left(InR, 1))) Then
            Msg2 = Msg2 & InR & vbCrLf
            Msg2 = Msg2 & "is an invalid name for a raster of ESRI GRID format." & vbCrLf
            Msg2 = Msg2 & "The name must be no more than 13 characters in length," & vbCrLf
            Msg2 = Msg2 & "must contain no spaces" & vbCrLf
            Msg2 = Msg2 & "and must not begin with a numeral."
            MsgBox(Msg2)
            Return False
        Else
            'the raster is a valid ESRI GRID
            Return True
        End If
    End Function

    
   
End Class
'todo
'
'more testing

