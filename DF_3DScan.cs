using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Linq;
using System.Text;
using System.Windows.Forms; // MessageBox
using System.IO;

namespace sample3dscan.cs
{
    class RenderStreams
    {
        static private PXCMCapture.Device device;
        private MainForm form;
        private ulong notReadyBits;
        
        public RenderStreams(MainForm mf)
        {
            form = mf;

            // Setup the bit field which indicates scan start readiness...
            notReadyBits = 0;
        }

        bool isScanReady(bool landmarks) 
        {
            bool result = landmarks ? 
                  (notReadyBits == 0)
                : ((notReadyBits & 
                    (1 << ((int)PXCM3DScan.AlertEvent.ALERT_SUFFICIENT_STRUCTURE))) == 0);
            return result;
        }

        void OnAlert(PXCM3DScan.AlertData data)
        {
            try
            {
                switch (data.label)
                {
                    case PXCM3DScan.AlertEvent.ALERT_TOO_FAR:
                        {
                            form.Invoke(new Action(() => form.ShowTooFar()));
                            break;
                        }
                    case PXCM3DScan.AlertEvent.ALERT_TOO_CLOSE:
                        {
                            form.Invoke(new Action(() => form.ShowTooClose()));
                            break;
                        }
                    case PXCM3DScan.AlertEvent.ALERT_IN_RANGE:
                        {
                            form.Invoke(new Action(() => form.HideRangeAlerts()));
                            break;
                        }


                    case PXCM3DScan.AlertEvent.ALERT_LOST_TRACKING:
                        {
                            form.Invoke(new Action(() => form.ShowTrackingAlert()));
                            break;
                        }
                    case PXCM3DScan.AlertEvent.ALERT_TRACKING:
                        {
                            form.Invoke(new Action(() => form.HideTrackingAlert()));
                            break;
                        }


                    case PXCM3DScan.AlertEvent.ALERT_SUFFICIENT_STRUCTURE:
                        {
                            form.Invoke(new Action(() => form.HideLabel(form.OutOfRange)));
                            notReadyBits &= (ulong.MaxValue ^ (1 << (int)PXCM3DScan.AlertEvent.ALERT_SUFFICIENT_STRUCTURE));
                            break;
                        }
                    case PXCM3DScan.AlertEvent.ALERT_INSUFFICIENT_STRUCTURE:
                        {
                            form.Invoke(new Action(() => form.ShowLabel(form.OutOfRange)));
                            notReadyBits |= (1 << (int)PXCM3DScan.AlertEvent.ALERT_SUFFICIENT_STRUCTURE);
                            break;
                        }


                    case PXCM3DScan.AlertEvent.ALERT_FACE_DETECTED:
                        {
                            form.Invoke(new Action(() => form.HideLabel(form.FaceNotDetected)));
                            notReadyBits &= (ulong.MaxValue ^ (1 << (int)PXCM3DScan.AlertEvent.ALERT_FACE_DETECTED));
                            break;
                        }
                    case PXCM3DScan.AlertEvent.ALERT_FACE_NOT_DETECTED:
                        {
                            form.Invoke(new Action(() => form.ShowLabel(form.FaceNotDetected)));
                            notReadyBits |= (1 << (int)PXCM3DScan.AlertEvent.ALERT_FACE_DETECTED);
                            break;
                        }


                    case PXCM3DScan.AlertEvent.ALERT_FACE_X_TOO_FAR_RIGHT:
                        {
                            form.Invoke(new Action(() => form.ShowLabel(form.MoveLeft)));
                            notReadyBits |= (1 << (int)PXCM3DScan.AlertEvent.ALERT_FACE_X_IN_RANGE);
                            break;
                        }
                    case PXCM3DScan.AlertEvent.ALERT_FACE_X_TOO_FAR_LEFT:
                        {
                            form.Invoke(new Action(() => form.ShowLabel(form.MoveRight)));
                            notReadyBits |= (1 << (int)PXCM3DScan.AlertEvent.ALERT_FACE_X_IN_RANGE);
                            break;
                        }
                    case PXCM3DScan.AlertEvent.ALERT_FACE_X_IN_RANGE:
                        {
                            form.Invoke(new Action(() => form.HideLabel(form.MoveRight)));
                            form.Invoke(new Action(() => form.HideLabel(form.MoveLeft)));
                            notReadyBits &= (ulong.MaxValue ^ (1 << (int)PXCM3DScan.AlertEvent.ALERT_FACE_X_IN_RANGE));
                            break;
                        }


                    case PXCM3DScan.AlertEvent.ALERT_FACE_Y_TOO_FAR_DOWN:
                        {
                            form.Invoke(new Action(() => form.ShowLabel(form.MoveUp)));
                            notReadyBits |= (1 << (int)PXCM3DScan.AlertEvent.ALERT_FACE_Y_IN_RANGE);
                            break;
                        }
                    case PXCM3DScan.AlertEvent.ALERT_FACE_Y_TOO_FAR_UP:
                        {
                            form.Invoke(new Action(() => form.ShowLabel(form.MoveDown)));
                            notReadyBits |= (1 << (int)PXCM3DScan.AlertEvent.ALERT_FACE_Y_IN_RANGE);
                            break;
                        }
                    case PXCM3DScan.AlertEvent.ALERT_FACE_Y_IN_RANGE:
                        {
                            form.Invoke(new Action(() => form.HideLabel(form.MoveUp)));
                            form.Invoke(new Action(() => form.HideLabel(form.MoveDown)));
                            notReadyBits &= (ulong.MaxValue ^ (1 << (int)PXCM3DScan.AlertEvent.ALERT_FACE_Y_IN_RANGE));
                            break;
                        }


                    case PXCM3DScan.AlertEvent.ALERT_FACE_Z_TOO_FAR:
                        {
                            form.Invoke(new Action(() => form.ShowLabel(form.MoveForward)));
                            notReadyBits |= (1 << (int)PXCM3DScan.AlertEvent.ALERT_FACE_Z_IN_RANGE);
                            break;
                        }
                    case PXCM3DScan.AlertEvent.ALERT_FACE_Z_TOO_CLOSE:
                        {
                            form.Invoke(new Action(() => form.ShowLabel(form.MoveBack)));
                            notReadyBits |= (1 << (int)PXCM3DScan.AlertEvent.ALERT_FACE_Z_IN_RANGE);
                            break;
                        }
                    case PXCM3DScan.AlertEvent.ALERT_FACE_Z_IN_RANGE:
                        {
                            form.Invoke(new Action(() => form.HideLabel(form.MoveForward)));
                            form.Invoke(new Action(() => form.HideLabel(form.MoveBack)));
                            notReadyBits &= (ulong.MaxValue ^ (1 << (int)PXCM3DScan.AlertEvent.ALERT_FACE_Z_IN_RANGE));
                            break;
                        }


                    case PXCM3DScan.AlertEvent.ALERT_FACE_YAW_TOO_FAR_RIGHT:
                        {
                            form.Invoke(new Action(() => form.ShowLabel(form.TurnLeft)));
                            notReadyBits |= (1 << (int)PXCM3DScan.AlertEvent.ALERT_FACE_YAW_IN_RANGE);
                            break;
                        }
                    case PXCM3DScan.AlertEvent.ALERT_FACE_YAW_TOO_FAR_LEFT:
                        {
                            form.Invoke(new Action(() => form.ShowLabel(form.TurnRight)));
                            notReadyBits |= (1 << (int)PXCM3DScan.AlertEvent.ALERT_FACE_YAW_IN_RANGE);
                            break;
                        }
                    case PXCM3DScan.AlertEvent.ALERT_FACE_YAW_IN_RANGE:
                        {
                            form.Invoke(new Action(() => form.HideLabel(form.TurnLeft)));
                            form.Invoke(new Action(() => form.HideLabel(form.TurnRight)));
                            notReadyBits &= (ulong.MaxValue ^ (1 << (int)PXCM3DScan.AlertEvent.ALERT_FACE_YAW_IN_RANGE));
                            break;
                        }


                    case PXCM3DScan.AlertEvent.ALERT_FACE_PITCH_TOO_FAR_DOWN:
                        {
                            form.Invoke(new Action(() => form.ShowLabel(form.TiltUp)));
                            notReadyBits |= (1 << (int)PXCM3DScan.AlertEvent.ALERT_FACE_PITCH_IN_RANGE);
                            break;
                        }
                    case PXCM3DScan.AlertEvent.ALERT_FACE_PITCH_TOO_FAR_UP:
                        {
                            form.Invoke(new Action(() => form.ShowLabel(form.TiltDown)));
                            notReadyBits |= (1 << (int)PXCM3DScan.AlertEvent.ALERT_FACE_PITCH_IN_RANGE);
                            break;
                        }
                    case PXCM3DScan.AlertEvent.ALERT_FACE_PITCH_IN_RANGE:
                        {
                            form.Invoke(new Action(() => form.HideLabel(form.TiltUp)));
                            form.Invoke(new Action(() => form.HideLabel(form.TiltDown)));
                            notReadyBits &= (ulong.MaxValue ^ (1 << (int)PXCM3DScan.AlertEvent.ALERT_FACE_PITCH_IN_RANGE));
                            break;
                        }
                }
            }
            catch { };
        }

        public static byte[] GetRGB32Pixels(PXCMImage image, out int cwidth, out int cheight)
        {
            PXCMImage.ImageData cdata;
            byte[] cpixels = null;
            cwidth = cheight = 0;
            if (image.AcquireAccess(PXCMImage.Access.ACCESS_READ, PXCMImage.PixelFormat.PIXEL_FORMAT_RGB32, out cdata) >= pxcmStatus.PXCM_STATUS_NO_ERROR)
            {
                cwidth = (int)cdata.pitches[0] / sizeof(Int32);
                cheight = (int)image.info.height;
                cpixels = cdata.ToByteArray(0, (int)cdata.pitches[0] * cheight);
                image.ReleaseAccess(cdata);
            }
            return cpixels;
        }

        public void StreamColorDepth(String scanType) /* Stream Color and Depth Synchronously or Asynchronously */
        {
            bool sts = true;
            PXCM3DScan.Configuration scan_config = new PXCM3DScan.Configuration();
            String statusString;

            /* Create an instance of the PXCSenseManager interface */
            PXCMSenseManager pp = PXCMSenseManager.CreateInstance();
            if (pp == null)
            {
                form.UpdateStatus("Failed to create an SDK pipeline object");
                return;
            }
            if (!form.IsModeLive())
            {
                pp.captureManager.SetFileName(form.GetFileName(), form.IsModeRecord());
            }

            /* Set Input Source */
            PXCMCapture.DeviceInfo dinfo2 = form.GetCheckedDevice();
            if (form.IsModeLive() || form.IsModeRecord()) pp.captureManager.FilterByDeviceInfo(dinfo2);

            if (form.IsModeRecord())
            {
                // Delay recording frames until the scan starts
                pp.captureManager.SetPause(true);
            }
            else if (!form.IsModeLive())
            {
                // Disable realtime mode if we are playing back a file
                // to ensure that frames are not skipped.
                pp.captureManager.SetRealtime(false);
            }

            /* Set Color & Depth Resolution */
            PXCMCapture.Device.StreamProfile cinfo = form.GetColorConfiguration();
            if (cinfo.imageInfo.format != 0)
            {
                Single cfps = cinfo.frameRate.max;
                pp.EnableStream(PXCMCapture.StreamType.STREAM_TYPE_COLOR, cinfo.imageInfo.width, cinfo.imageInfo.height, cfps);
            }

            PXCMCapture.Device.StreamProfile dinfo = form.GetDepthConfiguration();
            if (dinfo.imageInfo.format != 0)
            {
                Single dfps = dinfo.frameRate.max;
                pp.EnableStream(PXCMCapture.StreamType.STREAM_TYPE_DEPTH, dinfo.imageInfo.width, dinfo.imageInfo.height, dfps);
            }

            /* Initialization */
            FPSTimer timer = new FPSTimer(form);
            if (form.IsModeLive())
            {
                form.UpdateStatus("Initializing...");
            }
            /* Enable the 3D Scan video module */
            pxcmStatus result = pp.Enable3DScan();
            if (result != pxcmStatus.PXCM_STATUS_NO_ERROR)
            {
                pp.Close();
                pp.Dispose();
                form.UpdateStatus("Enable3DScan() returned " + result);
                return;
            }

            /* Initialize the camera system */
            result = pp.Init();
            form.UpdateStatus("");
            device = pp.captureManager.device;
            if (result >= pxcmStatus.PXCM_STATUS_NO_ERROR && device != null)
            {
                bool bAutoExpAndWBChanged = false;
                bool bAutoExposureEnabled = true;
                bool bAutoWhiteBalanceEnabled = true;

                /* Setup the scanning configuration */
                if (scanType == "Object") scan_config.mode = PXCM3DScan.ScanningMode.OBJECT_ON_PLANAR_SURFACE_DETECTION;
                else if (scanType == "Face") scan_config.mode = PXCM3DScan.ScanningMode.FACE;
                else if (scanType == "Body") scan_config.mode = PXCM3DScan.ScanningMode.BODY;
                else if (scanType == "Head") scan_config.mode = PXCM3DScan.ScanningMode.HEAD;
                else if (scanType == "Full") scan_config.mode = PXCM3DScan.ScanningMode.VARIABLE;

                /* Select the Targeting Options */
                scan_config.options = PXCM3DScan.ReconstructionOption.NONE;
                if (form.isSolidificationSelected()) scan_config.options 
                    |= (PXCM3DScan.ReconstructionOption.SOLIDIFICATION);
                if (form.isTextureSelected()) scan_config.options 
                    |= (PXCM3DScan.ReconstructionOption.TEXTURE);
                if (form.isLandmarksSelected()) scan_config.options
                    |= (PXCM3DScan.ReconstructionOption.LANDMARKS);

                /* Try to initialize the scanning system */
                PXCM3DScan scan = pp.Query3DScan();
                sts = false;
                if ( scan == null ) form.UpdateStatus("3DScan module not found.");
                else
                {
                    result = scan.SetConfiguration(scan_config);
                    if (result < pxcmStatus.PXCM_STATUS_NO_ERROR)
                    {
                        scan.Dispose();

                        // Show the configuration related error code
                        switch (result)
                        {
                            case pxcmStatus.PXCM_STATUS_FEATURE_UNSUPPORTED:
                                form.UpdateStatus("Configuration not supported with selected camera.");
                                break;
                            case pxcmStatus.PXCM_STATUS_ITEM_UNAVAILABLE:
                                form.UpdateStatus("Face module not found.");
                                break;
                            default:
                                form.UpdateStatus("SetConfiguration returned an error.");
                                break;
                        }
                    }
                    else sts = true;
                }

                // Conditionally finish the initialization and enter the mainloop
                if (sts == true)
                {
                    // Subscribe to recieve range and tracking alerts 
                    scan.Subscribe(OnAlert);

                    Projection projection = new Projection(
                        pp.session, device, dinfo.imageInfo);

                    Boolean bScanning = false;
                    Boolean bPlaybackStarted = false;
                    form.Invoke(new Action(() => form.SetButtonState(sample3dscan.cs.MainForm.ButtonState.Ce_SSd)));

                    while (form.reconstruct_requested || !form.GetStopState())
                    {
                        if (form.GetScanRequested()) /* one time latch */
                        {
                            form.Invoke(new Action(() => form.SetScanRequested(false)));

                            // Delay recording frames until the start of the scan is requested
                            if (form.IsModeRecord()) pp.captureManager.SetPause(false);

                            // If the max tri/vert controls are enabled, 
                            // use the set values. Otherwise, disabled decimation
                            // by setting the values to zero.
                            scan_config.maxTriangles = form.getMaxTrianglesEnabledChecked()
                                ? form.getMaxTriangles() : 0;
                            scan_config.maxVertices = form.getMaxVerticesEnabledChecked()
                                ? form.getMaxVertices() : 0;

                            // Request that the scan starts as soon as possible
                            scan_config.startScan = true;
                            scan.SetConfiguration(scan_config);

                            /* Update the status bar to help users understand what the detector is looking for */
                            if (form.IsModeLive())
                            {
                                if (scan_config.mode == PXCM3DScan.ScanningMode.OBJECT_ON_PLANAR_SURFACE_DETECTION)
                                {
                                    form.UpdateStatus("Object not detected. Place object on flat surface in center of view.");
                                }
                            }
                        }
                        else if (form.reconstruct_requested)
                        {
                            sts = SaveMesh(scan);
                        }

                        /* Get preview image from the 3D Scan video module */
                        if (!form.GetStopState())
                        {
                            /* Wait until a frame is ready: Synchronized or Asynchronous */
                            if (pp.AcquireFrame() < pxcmStatus.PXCM_STATUS_NO_ERROR)
                            {
                                projection.Dispose();
                                if (!form.IsModeLive())
                                {
                                    form.Invoke(new Action(() => form.EndScan()));
                                    sts = SaveMesh(scan);
                                }
                                break;
                            }

                            /* Get preview image from the 3D Scan video module */
                            PXCMImage preview_image = scan.AcquirePreviewImage();
                            pp.ReleaseFrame();
                                                   
                            /* Display Image and Status */
                            if (preview_image != null)
                            {
                                form.SetBitmap(preview_image);

                                if (scan.IsScanning())
                                {
                                    statusString = "Scanning";
                                    timer.Tick(statusString + " ");
                                    if (bScanning == false) // Lazy initializer
                                    {
                                        bScanning = true; // One way latch

                                        // Once the scanning process starts, we want to enable the Reconstruct button
                                        form.Invoke(new Action(() => form.SetButtonState(sample3dscan.cs.MainForm.ButtonState.Ce_ESe)));

                                        // Object, head and body scanning with a rear facing camera involves walking 
                                        // around the target, which effectivly exposes the camera to the full 
                                        // environment, similar to a panorama. To avoid undesirable color 
                                        // inconsistencies (realted to the response of the auto-exposure/wb changes), 
                                        // it usually works best to disable them. 
                                        // Note that these property changes are restored (below).
                                        if (device.deviceInfo.orientation
                                            == PXCMCapture.DeviceOrientation.DEVICE_ORIENTATION_REAR_FACING
                                            && scan_config.mode != PXCM3DScan.ScanningMode.FACE
                                            && form.IsModeLive())
                                        {
                                            bAutoExpAndWBChanged = true;
                                            bAutoExposureEnabled = device.QueryColorAutoExposure();
                                            device.SetColorAutoExposure(false);
                                            bAutoWhiteBalanceEnabled = device.QueryColorAutoWhiteBalance();
                                            device.SetColorAutoWhiteBalance(false);
                                        }
                                    }
                                }
                                else
                                {
                                    if (!form.IsModeLive() && !form.IsModeRecord()) // In playback mode, automatically request the scan
                                    {
                                        if (bPlaybackStarted == false) // Lazy initializer
                                        {
                                            bPlaybackStarted = true; // One way latch
                                            form.scan_requested = true;
                                            form.Invoke(new Action(() => form.StartScanning(false)));
                                            form.Invoke(new Action(() => form.SetButtonState(sample3dscan.cs.MainForm.ButtonState.Ce_ESe)));
                                        }
                                    }
                                    else
                                    {
                                        if (!form.GetStopState())
                                        {
                                            if (isScanReady(form.landmarksChecked()))
                                            {
                                                form.Invoke(new Action(() => form.EnableReconstruction(true)));
                                            }
                                            else
                                            {
                                                form.Invoke(new Action(() => form.EnableReconstruction(false)));
                                            }
                                        }
                                    }
                                }
                                preview_image.Dispose();
                            }
                        }
                    }
                    projection.Dispose();
                    scan.Dispose();
                }
                // Restore the default camera properties
                if (bAutoExpAndWBChanged)
                {
                    device.SetColorAutoExposure(bAutoExposureEnabled);
                    device.SetColorAutoWhiteBalance(bAutoWhiteBalanceEnabled);
                }

                device.Dispose();
                device = null;
            }
            else
            {
                try { form.UpdateStatus(result + ""); }
                catch { }
                sts = false;
            }
            
            if (sts)
            {
                try { form.UpdateStatus(""); }
                catch { }
            }
            
            pp.Close();
            pp.Dispose();

            try { form.Invoke(new Action(() => form.ResetStop())); }
            catch { }
        }

        private struct vertexStruct
        {
            public float x, y, z;
            public float r, g, b;
        };
        private struct faceStruct
        {
            public Int32[] vertex_indicies;
        };
        private struct meshStruct
        {
            public vertexStruct[] vertices;
            public faceStruct[] faces;
        };

        private meshStruct LoadObjFile(string in_obj_file_name)
        {
            meshStruct mesh = new meshStruct();
            StreamReader stream = File.OpenText(in_obj_file_name);
            string text = stream.ReadToEnd();
            stream.Close();
            const int CHUNK_SIZE = 50000;
            int maxVerts = CHUNK_SIZE;
            mesh.vertices = new vertexStruct[maxVerts];
            int maxFaces = CHUNK_SIZE;
            mesh.faces = new faceStruct[maxFaces];
            int vertices = 0;
            int faces = 0;
            using (StringReader reader = new StringReader(text))
            {
                string currentText = reader.ReadLine();
                char[] split = { ' ' };
                string[] elements;
                while (currentText != null)
                {
                    if (!currentText.StartsWith("f ") && !currentText.StartsWith("v "))
                    {
                        currentText = reader.ReadLine();
                        if (currentText != null) currentText = currentText.Replace("  ", " ");
                    }
                    else
                    {
                        currentText = currentText.Trim();
                        elements = currentText.Split(split, 50);
                        switch (elements[0])
                        {
                            case "v":
                                mesh.vertices[vertices].x = System.Convert.ToSingle(elements[1]);
                                mesh.vertices[vertices].y = System.Convert.ToSingle(elements[2]);
                                mesh.vertices[vertices].z = System.Convert.ToSingle(elements[3]);
                                mesh.vertices[vertices].r = System.Convert.ToSingle(elements[4]);
                                mesh.vertices[vertices].g = System.Convert.ToSingle(elements[5]);
                                mesh.vertices[vertices].b = System.Convert.ToSingle(elements[6]);
                                if (++vertices == maxVerts)
                                {
                                    maxVerts += CHUNK_SIZE;
                                    Array.Resize(ref mesh.vertices, maxVerts);
                                }
                                break;
                            case "f":
                                char[] index_split = { '/' };
                                string[] index_elements;
                                mesh.faces[faces].vertex_indicies = new Int32[3];
                                index_elements = elements[1].Split(index_split, 50);
                                mesh.faces[faces].vertex_indicies[0] = System.Convert.ToInt32(index_elements[0]);
                                index_elements = elements[2].Split(index_split, 50);
                                mesh.faces[faces].vertex_indicies[1] = System.Convert.ToInt32(index_elements[0]);
                                index_elements = elements[3].Split(index_split, 50);
                                mesh.faces[faces].vertex_indicies[2] = System.Convert.ToInt32(index_elements[0]);
                                if (++faces == maxFaces)
                                {
                                    maxFaces += CHUNK_SIZE;
                                    Array.Resize(ref mesh.faces, maxFaces);
                                }
                                break;
                        }
                        currentText = reader.ReadLine();
                        if (currentText != null) currentText = currentText.Replace("  ", " ");
                    }
                }
            }
            Array.Resize(ref mesh.vertices, vertices);
            Array.Resize(ref mesh.faces, faces);
            return mesh;
        }

        private StreamReader GetPrefabStream(string filename)
        {
            StreamReader stream = null;
            String path = null;
            // Start by looking in the dev (relative) path
            try
            {
                path = @"..\..\data\3dscan\viewer\" + filename;
                stream = File.OpenText(path);
            }
            catch { };
            // Then try the installed location
            if (stream == null)
            {
                try
                {
                    string RSSDK_DIR = Environment.GetEnvironmentVariable("RSSDK_DIR");
                    if (RSSDK_DIR != null) path = RSSDK_DIR + @"data\3dscan\viewer\" + filename;
                    stream = File.OpenText(path);
                }
                catch { };
            }
            // Lastly, look in the local directory
            if (stream == null) try { stream = File.OpenText(filename); }
                catch { };
            return stream;
        }

        public bool GenerateHtmlFromObj(string in_obj_file_name, string out_html_file_name)
        {
            // Load the mesh
            meshStruct mesh = LoadObjFile(in_obj_file_name);
            if (mesh.vertices.Length == 0 || mesh.faces.Length == 0) return true;

            // Write out the header
            StreamReader stream = GetPrefabStream(@"3dscan_header.html");
            if (stream == null) return true;
            string text = stream.ReadToEnd();
            stream.Close();
            File.WriteAllText(out_html_file_name, text);

            // Then, compose and append the data vertex...
            {
                StringBuilder sbv = new StringBuilder();
                StringBuilder sbc = new StringBuilder();
                string PRECISION = "0.000000";
                sbv.Append("    var verticies = new Float64Array([");
                sbc.Append("    var colors = new Uint8Array([");
                for (Int32 v = 0; v < mesh.vertices.Length; v++)
                {
                    vertexStruct vert = mesh.vertices[v];
                    sbv.Append(vert.x.ToString(PRECISION) + ","
                             + vert.y.ToString(PRECISION) + ","
                             + vert.z.ToString(PRECISION) + ",");
                    sbc.Append((vert.r * 255).ToString("0.") + ","
                             + (vert.g * 255).ToString("0.") + ","
                             + (vert.b * 255).ToString("0.") + ",");
                }
                sbv.Append("]);\n");
                File.AppendAllText(out_html_file_name, sbv.ToString());
                sbc.Append("]);\n");
                File.AppendAllText(out_html_file_name, sbc.ToString());
            }
            // ...and face data.
            {
                StringBuilder sbf = new StringBuilder();
                sbf.Append("    var faces = new Int32Array([");
                for (Int32 f = 0; f < mesh.faces.Length; f++)
                {
                    faceStruct face = mesh.faces[f];
                    sbf.Append((face.vertex_indicies[0] - 1).ToString() + ","
                             + (face.vertex_indicies[1] - 1).ToString() + ","
                             + (face.vertex_indicies[2] - 1).ToString() + ",");

                }
                sbf.Append("]);\n");
                File.AppendAllText(out_html_file_name, sbf.ToString());
            }

            // Lastly, append the footer
            stream = GetPrefabStream(@"3dscan_footer.html");
            if (stream == null) return true;
            text = stream.ReadToEnd();
            stream.Close();
            File.AppendAllText(out_html_file_name, text);

            return false;
        }

        private bool SaveMesh(PXCM3DScan scan)
        {
            form.Invoke(new Action(() => form.HideTrackingAlert()));
            form.Invoke(new Action(() => form.HideRangeAlerts()));

            bool sts = true;

            form.UpdateStatus("Saving...");

            // wait for filename to be filled
            string filename = null;
            do
            {
                filename = form.GetMeshFileName();
                System.Threading.Thread.Sleep(100);
            } while (filename == null);

            if (filename != "c")
            {
                form.UpdateStatus("Saving " + filename + "...");

                string mesh_format = filename.Substring(filename.Length - 3);
                pxcmStatus res = pxcmStatus.PXCM_STATUS_NO_ERROR;

                if (mesh_format.ToLower() == "obj") res = scan.Reconstruct(PXCM3DScan.FileFormat.OBJ, filename);
                else if (mesh_format.ToLower() == "ply") res = scan.Reconstruct(PXCM3DScan.FileFormat.PLY, filename);
                else if (mesh_format.ToLower() == "stl") res = scan.Reconstruct(PXCM3DScan.FileFormat.STL, filename);

                if (res < pxcmStatus.PXCM_STATUS_NO_ERROR)
                {
                    if (res == pxcmStatus.PXCM_STATUS_FILE_WRITE_FAILED) form.UpdateStatus("Error: Write failed to " + filename + " Exiting...");
                    else form.UpdateStatus("Reconstruct() returned " + res);
                    sts = false;
                }
                else
                {
                    form.UpdateStatus(filename + " saved");
                    sts = false;

                    if (mesh_format.ToLower() == "obj") // Viewer only supports obj format in this release
                    {
                        if (!form.isTextureSelected()) // Viewer only supports non-textured meshes in this release
                        {
                            // Generate an html version of the obj file
                            String htmlFile = filename + ".html";
                            if (!GenerateHtmlFromObj(filename, htmlFile))
                            {
                                // Load the resulting html file
                                GC.Collect(); // Free up as much system RAM as possible before loading the html file.
                                System.Diagnostics.Process process = new System.Diagnostics.Process();
                                process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                                process.StartInfo.FileName = (string)htmlFile;
                                process.Start();
                                process.Dispose();
                            }
                        }
                    }
                }
            }
            else
            {
                form.UpdateStatus("Canceled");
            }
            form.UseWaitCursor = false;
            form.reconstruct_requested = false;
            form.ReleaseMeshFileName();

            return sts;
        }
    }
}
