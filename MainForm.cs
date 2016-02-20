using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using sample3dscan.cs;
using SampleDX;


namespace sample3dscan.cs
{
    public partial class MainForm : Form
    {
        private PXCMSession session;
        private volatile bool closing = false;
        private volatile bool stop = false;
        private Boolean startStop = true;
        private Boolean userFacingCamera = true;
        private Boolean scanReconstruct = true;
        public bool scan_requested = false;
        public bool reconstruct_requested = false;
        public bool scanning = false;
        private string filename = null;
        private string mesh_filename = null;
        private Dictionary<ToolStripMenuItem, PXCMCapture.DeviceInfo> devices = new Dictionary<ToolStripMenuItem, PXCMCapture.DeviceInfo>();
        private Dictionary<ToolStripMenuItem, int> devices_iuid = new Dictionary<ToolStripMenuItem, int>();
        private Dictionary<ToolStripMenuItem, PXCMCapture.Device.StreamProfile> profiles = new Dictionary<ToolStripMenuItem, PXCMCapture.Device.StreamProfile>();
        private D2D1Render render = new D2D1Render();

        public enum ButtonState { 
            // LeftButtonState_RightButtonState
            SCe_SSd = 0,    // Streaming stopped (Start Camera enabled, Start Scanning disabled)
            Cd_SSd,         // Waiting for stream start (Cancel disabled, Start Scanning disabled)
            Ce_SSd,         // Start Camera pressed (Cancel enabled, Start Scanning disabled)
            Ce_SSd2,        // Streaming started (Cancel enabled, Start Scanning disabled)
            Ce_SSe,         // Scanning preconditions met (Cancel enabled, Start Scanning enabled)
            Ce_ESd,         // Start Scanning pressed (Cancel enabled, End Scanning disabled)
            Ce_ESe,         // Scanning started (Cancel enabled, End Scanning enabled)
            Cd_ESd,         // Scanning ended (Cancel disabled, End Scanning disabled)
        };
        private ButtonState buttonState = ButtonState.SCe_SSd;

        public ButtonState GetButtonState(ButtonState state)
        {
            return buttonState;
        }

        public void SetButtonState(ButtonState state)
        {
            buttonState = state;
            switch (buttonState)
            {
                case ButtonState.SCe_SSd: // Streaming stopped (Start Camera enabled, Start Scanning disabled)
                    Start.Text = "Start Camera";
                    Start.Enabled = true;
                    Start.Focus();
                    Reconstruct.Text = "Start Scanning";
                    Reconstruct.Enabled = false;
                    break;
                case ButtonState.Cd_SSd: // Waiting for streaming start (Cancel disabled, Start Scanning disabled)
                    Start.Text = "Cancel";
                    Start.Enabled = false;
                    Reconstruct.Text = "Start Scanning";
                    Reconstruct.Enabled = false;
                    break;
                case ButtonState.Ce_SSd: // Start Camera pressed (Cancel enabled, Start Scanning disabled)
                    Start.Text = "Cancel";
                    Start.Enabled = true;
                    Start.Focus();
                    Reconstruct.Text = "Start Scanning";
                    Reconstruct.Enabled = false; 
                    break;
                case ButtonState.Ce_SSd2: // Streaming started (Cancel enabled, Start Scanning disabled)
                    Start.Text = "Cancel";
                    Start.Enabled = true;
                    Start.Focus();
                    Reconstruct.Text = "Start Scanning";
                    Reconstruct.Enabled = false; 
                    break;
                case ButtonState.Ce_SSe: // Scanning preconditions met (Cancel enabled, Start Scanning enabled)
                    Start.Text = "Cancel";
                    Start.Enabled = true;
                    Reconstruct.Text = "Start Scanning";
                    Reconstruct.Enabled = true;
                    Reconstruct.Focus();
                    break;
                case ButtonState.Ce_ESd: // Start Scanning pressed (Cancel enabled, End Scanning disabled)
                    scan_requested = true;
                    Start.Text = "Cancel";
                    Start.Enabled = true;
                    Start.Focus();
                    Reconstruct.Text = "End Scanning";
                    Reconstruct.Enabled = false; 
                    break;
                case ButtonState.Ce_ESe: // Scanning started (Cancel enabled, End Scanning enabled)
                    Start.Text = "Cancel";
                    Start.Enabled = true;
                    Reconstruct.Text = "End Scanning";
                    Reconstruct.Enabled = true;
                    Reconstruct.Focus();
                    break;
                case ButtonState.Cd_ESd: // Scanning ended (Cancel disabled, End Scanning disabled)
                    Start.Text = "Cancel";
                    Start.Enabled = false;
                    Reconstruct.Text = "End Scanning";
                    Reconstruct.Enabled = false;
                    break;
            }
            Panel_Paint(MainPanel, null);
        }

        public bool GetScanRequested()
        {
            return scan_requested;
        }

        public void SetScanRequested( bool enabled )
        {
            scan_requested = enabled;
        }

        public MainForm(PXCMSession session)
        {
            InitializeComponent();

            this.session = session;
            PopulateDeviceMenu();

            FormClosing += new FormClosingEventHandler(MainForm_FormClosing);
            MainPanel.Paint += new PaintEventHandler(Panel_Paint);
            MainPanel.Resize += new EventHandler(Panel_Resize);
            render.SetHWND(MainPanel);
            ScanningArea.SelectedIndex = ScanningArea.Items.IndexOf("Face");
        }

        private void PopulateDeviceMenu()
        {
            devices.Clear();
            devices_iuid.Clear();

            PXCMSession.ImplDesc desc = new PXCMSession.ImplDesc();
            desc.group = PXCMSession.ImplGroup.IMPL_GROUP_SENSOR;
            desc.subgroup = PXCMSession.ImplSubgroup.IMPL_SUBGROUP_VIDEO_CAPTURE;

            DeviceMenu.DropDownItems.Clear();

            for (int i = 0; ; i++)
            {
                PXCMSession.ImplDesc desc1;
                if (session.QueryImpl(desc, i, out desc1) < pxcmStatus.PXCM_STATUS_NO_ERROR) break;
                PXCMCapture capture;
                if (session.CreateImpl<PXCMCapture>(desc1, out capture) < pxcmStatus.PXCM_STATUS_NO_ERROR) continue;
                for (int j = 0; ; j++)
                {
                    PXCMCapture.DeviceInfo dinfo;
                    if (capture.QueryDeviceInfo(j, out dinfo) < pxcmStatus.PXCM_STATUS_NO_ERROR) break;
                    if (dinfo.model == PXCMCapture.DeviceModel.DEVICE_MODEL_GENERIC) continue;

                    ToolStripMenuItem sm1 = new ToolStripMenuItem(dinfo.name, null, new EventHandler(Device_Item_Click));
                    devices[sm1] = dinfo;
                    devices_iuid[sm1] = desc1.iuid;
                    DeviceMenu.DropDownItems.Add(sm1);
                }
                capture.Dispose();
            }
            if (DeviceMenu.DropDownItems.Count > 0)
            {
                (DeviceMenu.DropDownItems[0] as ToolStripMenuItem).Checked = true;
                PopulateColorMenus(DeviceMenu.DropDownItems[DeviceMenu.DropDownItems.Count - 1] as ToolStripMenuItem);
                PopulateDepthMenus(DeviceMenu.DropDownItems[DeviceMenu.DropDownItems.Count - 1] as ToolStripMenuItem);
            }
        }

        private bool PopulateDeviceFromFileMenu()
        {
            devices.Clear();
            devices_iuid.Clear();

            PXCMSession.ImplDesc desc = new PXCMSession.ImplDesc();
            desc.group = PXCMSession.ImplGroup.IMPL_GROUP_SENSOR;
            desc.subgroup = PXCMSession.ImplSubgroup.IMPL_SUBGROUP_VIDEO_CAPTURE;

            PXCMSession.ImplDesc desc1;
            PXCMCapture.DeviceInfo dinfo;
            PXCMSenseManager pp = PXCMSenseManager.CreateInstance();
            if (pp == null)
            {
                UpdateStatus("Init Failed");
                return false;
            }
            try
            {
                if (session.QueryImpl(desc, 0, out desc1) < pxcmStatus.PXCM_STATUS_NO_ERROR) throw null;
                if (pp.captureManager.SetFileName(filename, false) < pxcmStatus.PXCM_STATUS_NO_ERROR) throw null;
                if (pp.captureManager.LocateStreams() < pxcmStatus.PXCM_STATUS_NO_ERROR) throw null;
                pp.captureManager.device.QueryDeviceInfo(out dinfo);
            }
            catch
            {
                pp.Dispose();
                UpdateStatus("Init Failed");
                return false;
            }
            DeviceMenu.DropDownItems.Clear();
            ToolStripMenuItem sm1 = new ToolStripMenuItem(dinfo.name, null, new EventHandler(Device_Item_Click));
            devices[sm1] = dinfo;
            devices_iuid[sm1] = desc1.iuid;
            DeviceMenu.DropDownItems.Add(sm1);

            sm1 = new ToolStripMenuItem("Playback from the file : ", null);
            sm1.Enabled = false;
            DeviceMenu.DropDownItems.Add(sm1);
            sm1 = new ToolStripMenuItem(filename, null);
            sm1.Enabled = false;
            DeviceMenu.DropDownItems.Add(sm1);
            if (DeviceMenu.DropDownItems.Count > 0)
                (DeviceMenu.DropDownItems[0] as ToolStripMenuItem).Checked = true;

            // populate color depth menu from the file
            profiles.Clear();
            ColorMenu.DropDownItems.Clear();
            DepthMenu.DropDownItems.Clear();
            PXCMCapture.Device device = pp.captureManager.QueryDevice();

            PXCMCapture.Device.StreamProfileSet profile = new PXCMCapture.Device.StreamProfileSet();
            if (dinfo.streams.HasFlag(PXCMCapture.StreamType.STREAM_TYPE_COLOR))
            {
                for (int p = 0; ; p++)
                {
                    if (device.QueryStreamProfileSet(PXCMCapture.StreamType.STREAM_TYPE_COLOR, p, out profile) < pxcmStatus.PXCM_STATUS_NO_ERROR) break;
                    PXCMCapture.Device.StreamProfile sprofile = profile[PXCMCapture.StreamType.STREAM_TYPE_COLOR];
                    sm1 = new ToolStripMenuItem(ProfileToString(sprofile), null, new EventHandler(Color_Item_Click));
                    profiles[sm1] = sprofile;
                    ColorMenu.DropDownItems.Add(sm1);
                }
            }

            if (((int)dinfo.streams & (int)PXCMCapture.StreamType.STREAM_TYPE_DEPTH) != 0)
            {
                for (int p = 0; ; p++)
                {
                    if (device.QueryStreamProfileSet(PXCMCapture.StreamType.STREAM_TYPE_DEPTH, p, out profile) < pxcmStatus.PXCM_STATUS_NO_ERROR) break;
                    PXCMCapture.Device.StreamProfile sprofile = profile[PXCMCapture.StreamType.STREAM_TYPE_DEPTH];
                    sm1 = new ToolStripMenuItem(ProfileToString(sprofile), null, new EventHandler(Depth_Item_Click));
                    profiles[sm1] = sprofile;
                    DepthMenu.DropDownItems.Add(sm1);
                }
            }

            GetCheckedDevice();

            pp.Close();
            pp.Dispose();
            return true;
        }

        private void PopulateColorMenus(ToolStripMenuItem device_item)
        {
            OperatingSystem os_version = Environment.OSVersion;
            PXCMSession.ImplDesc desc = new PXCMSession.ImplDesc();
            desc.group = PXCMSession.ImplGroup.IMPL_GROUP_SENSOR;
            desc.subgroup = PXCMSession.ImplSubgroup.IMPL_SUBGROUP_VIDEO_CAPTURE;
            desc.iuid = devices_iuid[device_item];
            desc.cuids[0] = PXCMCapture.CUID;

            profiles.Clear();
            ColorMenu.DropDownItems.Clear();
            PXCMCapture capture;
            PXCMCapture.DeviceInfo dinfo2 = GetCheckedDevice();

            PXCMSenseManager pp = session.CreateSenseManager();
            if (pp == null) return;
            if (pp.Enable3DScan() < pxcmStatus.PXCM_STATUS_NO_ERROR) return;
            PXCM3DScan s = pp.Query3DScan();
            if (s == null) return;
            PXCMVideoModule m = s.QueryInstance<PXCMVideoModule>();
            if (m == null) return;

            int count = 0;
            if (session.CreateImpl<PXCMCapture>(desc, out capture) >= pxcmStatus.PXCM_STATUS_NO_ERROR)
            {
                PXCMCapture.Device device = capture.CreateDevice(dinfo2.didx);
                if (device != null)
                {
                    PXCMCapture.Device.StreamProfileSet profile = new PXCMCapture.Device.StreamProfileSet(); ;
                    if (dinfo2.streams.HasFlag(PXCMCapture.StreamType.STREAM_TYPE_COLOR))
                    {
                        for (int p = 0; ; p++)
                        {
                            if (device.QueryStreamProfileSet(PXCMCapture.StreamType.STREAM_TYPE_COLOR, p, out profile) < pxcmStatus.PXCM_STATUS_NO_ERROR) break;
                            PXCMCapture.Device.StreamProfile sprofile = profile[PXCMCapture.StreamType.STREAM_TYPE_COLOR];

                            // Only populate profiles which are supported by the module
                            bool bFound = false;
                            int i = 0;
                            PXCMVideoModule.DataDesc inputs;
                            PXCMImage.PixelFormat format = PXCMImage.PixelFormat.PIXEL_FORMAT_RGB32;
                            if (dinfo2.orientation != PXCMCapture.DeviceOrientation.DEVICE_ORIENTATION_REAR_FACING)
                            {
                                format = PXCMImage.PixelFormat.PIXEL_FORMAT_RGB24;
                            }
                            while ((m.QueryCaptureProfile(i++, out inputs) >= pxcmStatus.PXCM_STATUS_NO_ERROR))
                            {
                                if ((sprofile.imageInfo.height == inputs.streams.color.sizeMax.height)
                                    && (sprofile.imageInfo.width == inputs.streams.color.sizeMax.width)
                                    && (sprofile.frameRate.max == inputs.streams.color.frameRate.max)
                                    && (sprofile.imageInfo.format == format)
                                    && (0==(sprofile.options & PXCMCapture.Device.StreamOption.STREAM_OPTION_UNRECTIFIED)))
                                {
                                    bFound = true;
                                    if (dinfo2.orientation != PXCMCapture.DeviceOrientation.DEVICE_ORIENTATION_REAR_FACING)
                                    {   // Hide rear facing resolutions when the front facing camera is connected...
                                        if (sprofile.imageInfo.width == 640) bFound = false;
                                    }
                                    // On Windows 7, filter non-functional 30 fps modes
                                    if (Environment.OSVersion.Version.Major == 6 && Environment.OSVersion.Version.Minor == 1)
                                    {
                                        if (sprofile.frameRate.max == 30) bFound = false;
                                    }
                                }
                            }
                            if (bFound)
                            {
                                ToolStripMenuItem sm1 = new ToolStripMenuItem(ProfileToString(sprofile), null, new EventHandler(Color_Item_Click));
                                profiles[sm1] = sprofile;
                                ColorMenu.DropDownItems.Add(sm1);
                                count++;
                            }
                        }
                    }
                    device.Dispose();
                }
                capture.Dispose();
            }
            m.Dispose();
            pp.Dispose();
            if (count > 0) (ColorMenu.DropDownItems[ColorMenu.DropDownItems.Count - 1] as ToolStripMenuItem).Checked = true;
        }

        private void PopulateDepthMenus(ToolStripMenuItem device_item)
        {
            PXCMSession.ImplDesc desc = new PXCMSession.ImplDesc();
            desc.group = PXCMSession.ImplGroup.IMPL_GROUP_SENSOR;
            desc.subgroup = PXCMSession.ImplSubgroup.IMPL_SUBGROUP_VIDEO_CAPTURE;
            desc.iuid = devices_iuid[device_item];
            desc.cuids[0] = PXCMCapture.CUID;

            DepthMenu.DropDownItems.Clear();
            PXCMCapture capture;
            PXCMCapture.DeviceInfo dinfo2 = GetCheckedDevice();

            PXCMSenseManager pp = session.CreateSenseManager();
            if (pp == null) return;
            if (pp.Enable3DScan() < pxcmStatus.PXCM_STATUS_NO_ERROR) return;
            PXCM3DScan s = pp.Query3DScan();
            if (s == null) return;
            PXCMVideoModule m = s.QueryInstance<PXCMVideoModule>();
            if (m == null) return;

            if (session.CreateImpl<PXCMCapture>(desc, out capture) >= pxcmStatus.PXCM_STATUS_NO_ERROR)
            {
                PXCMCapture.Device device = capture.CreateDevice(dinfo2.didx);
                if (device != null)
                {
                    PXCMCapture.Device.StreamProfileSet profile = new PXCMCapture.Device.StreamProfileSet(); ;
                    PXCMCapture.Device.StreamProfile color_profile = GetColorConfiguration();
                    if (((int)dinfo2.streams & (int)PXCMCapture.StreamType.STREAM_TYPE_DEPTH) != 0)
                    {
                        for (int p = 0; ; p++)
                        {
                            if (device.QueryStreamProfileSet(PXCMCapture.StreamType.STREAM_TYPE_DEPTH, p, out profile) < pxcmStatus.PXCM_STATUS_NO_ERROR) break;
                            PXCMCapture.Device.StreamProfile sprofile = profile[PXCMCapture.StreamType.STREAM_TYPE_DEPTH];

                            bool bFound = false;
                            int i = 0;
                            PXCMVideoModule.DataDesc inputs;
                            while ((m.QueryCaptureProfile(i++, out inputs) >= pxcmStatus.PXCM_STATUS_NO_ERROR))
                            {
                                if ((sprofile.imageInfo.height == inputs.streams.depth.sizeMax.height)
                                    && (sprofile.imageInfo.width == inputs.streams.depth.sizeMax.width)
                                    && (sprofile.frameRate.max == inputs.streams.depth.frameRate.max)
                                    && (color_profile.frameRate.max == inputs.streams.depth.frameRate.max))
                                {
                                    bFound = true;
                                }
                            }
                            if (bFound)
                            {
                                ToolStripMenuItem sm1 = new ToolStripMenuItem(ProfileToString(sprofile), null, new EventHandler(Depth_Item_Click));
                                profiles[sm1] = sprofile;
                                DepthMenu.DropDownItems.Add(sm1);
                            }
                        }
                    }
                    device.Dispose();
                }
                capture.Dispose();
            }
            m.Dispose();
            pp.Dispose();

            if (DepthMenu.DropDownItems.Count > 0) (DepthMenu.DropDownItems[DepthMenu.DropDownItems.Count - 1] as ToolStripMenuItem).Checked = true;
        }

        private string ProfileToString(PXCMCapture.Device.StreamProfile pinfo)
        {
            string line = pinfo.imageInfo.format.ToString().Substring(13) + " " + pinfo.imageInfo.width + "x" + pinfo.imageInfo.height + " ";
            if (pinfo.frameRate.min != pinfo.frameRate.max)
            {
                line += (float)pinfo.frameRate.min + "-" +
                      (float)pinfo.frameRate.max;
            }
            else
            {
                float fps = (pinfo.frameRate.min != 0) ? pinfo.frameRate.min : pinfo.frameRate.max;
                line += fps;
            }
            if (pinfo.options.HasFlag(PXCMCapture.Device.StreamOption.STREAM_OPTION_UNRECTIFIED))
                line += " RAW";
            return line;
        }

        private void Device_Item_Click(object sender, EventArgs e)
        {
            foreach (ToolStripMenuItem e1 in DeviceMenu.DropDownItems)
            {
                e1.Checked = (sender == e1);
            }
            PopulateColorMenus(sender as ToolStripMenuItem);
            PopulateDepthMenus(sender as ToolStripMenuItem);

            PXCMSession.ImplDesc desc = new PXCMSession.ImplDesc();
            PXCMCapture.DeviceInfo dev_info = devices[(sender as ToolStripMenuItem)];
        }

        private void Color_Item_Click(object sender, EventArgs e)
        {
            foreach (ToolStripMenuItem e1 in ColorMenu.DropDownItems)
                e1.Checked = (sender == e1);
            // Repopulate the depth menu in case we switched from 30 to 60 fps (or vise versa).
            foreach (ToolStripMenuItem e2 in DeviceMenu.DropDownItems)
                if (e2.Checked) PopulateDepthMenus(e2 as ToolStripMenuItem);
        }

        private void Depth_Item_Click(object sender, EventArgs e)
        {
            foreach (ToolStripMenuItem e1 in DepthMenu.DropDownItems)
                e1.Checked = (sender == e1);
        }

        private void Start_Click(object sender, EventArgs e)
        {
            SetButtonState(ButtonState.Cd_SSd);
            if (startStop)
            {
                // Wait for previous thread to exit.
                while (stop == true) System.Threading.Thread.Sleep(5);
                System.Threading.Thread thread = new System.Threading.Thread(DoRendering);
                thread.Start();
            }
            else
            {
                stop = true;
            }

            HideTrackingAlert();
            HideRangeAlerts();

            startStop ^= true;
        }

        private void EnableLandmarks(bool enable)
        {
            Landmarks.Enabled = enable; 
        }

        private void EnableControls(bool enable)
        {
            MainMenu.Enabled = enable;
            ScanningArea.Enabled = enable;
            EnableLandmarks(enable);
            Textured.Enabled = enable;
            Solid.Enabled = enable;
            MaxTrianglesEnabled.Enabled = enable;
            MaxVerticesEnabled.Enabled = enable;
            MaxTriangles.Enabled = enable;
            MaxVertices.Enabled = enable;
        }

        delegate String DoRenderingBegin();
        delegate void DoRenderingEnd();
        private void DoRendering()
        {
            RenderStreams rs = new RenderStreams(this);

            try
            {
                rs.StreamColorDepth((String)Invoke(new DoRenderingBegin(
                    delegate
                    {
                        EnableControls(false);
                        return ScanningArea.SelectedItem.ToString();
                    }
                )));

                this.Invoke(new DoRenderingEnd(
                    delegate
                    {
                        if (closing) Close();
                        EnableControls(true);
                        SetButtonState(ButtonState.SCe_SSd);
                        startStop = true;
                        scanReconstruct = true;
                    }
                ));
            }
            catch (Exception) { }
        }

        public PXCMCapture.DeviceInfo GetCheckedDevice()
        {
            foreach (ToolStripMenuItem e in DeviceMenu.DropDownItems)
            {
                if (devices.ContainsKey(e))
                {
                    if (e.Checked)
                    {
                        PXCMCapture.DeviceInfo dev_info = devices[e];
                        userFacingCamera = (dev_info.orientation != PXCMCapture.DeviceOrientation.DEVICE_ORIENTATION_REAR_FACING);
                        if (!userFacingCamera && Landmarks.Checked) Landmarks.Checked = false;
                        // Landmarks are only supported when using front facing cameras in this release
                        try { EnableLandmarks(userFacingCamera && Solid.Enabled); }
                        catch (Exception) { }
                        return devices[e];
                    }
                }
            }
            return new PXCMCapture.DeviceInfo();
        }

        private PXCMCapture.Device.StreamProfile GetConfiguration(ToolStripMenuItem m)
        {
            foreach (ToolStripMenuItem e in m.DropDownItems)
                if (e.Checked) return profiles[e];
            return new PXCMCapture.Device.StreamProfile();
        }

        public PXCMCapture.Device.StreamProfile GetColorConfiguration()
        {
            return GetConfiguration(ColorMenu);
        }

        public PXCMCapture.Device.StreamProfile GetDepthConfiguration()
        {
            return GetConfiguration(DepthMenu);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            stop = true;
            e.Cancel = false;
            closing = true;
        }

        private delegate void UpdateStatusDelegate(string status);
        public void UpdateStatus(string status)
        {
            try
            {
                Status2.Invoke(new UpdateStatusDelegate(delegate(string s) { StatusLabel.Text = s; }), new object[] { status });
            }
            catch (Exception) { }
        }

        public void SetBitmap(PXCMImage image)
        {
            if (image == null) return;
            lock (this)
            {
                render.UpdatePanel(image);
            }
        }

        private void Panel_Paint(object sender, PaintEventArgs e)
        {
            lock (this)
            {
                render.UpdatePanel();
            }
        }

        private void Panel_Resize(object sender, EventArgs e)
        {
            lock (this)
            {
                render.ResizePanel();
            }
        }

        public string GetFileName()
        {
            return filename;
        }

        public string GetMeshFileName()
        {
            return mesh_filename;
        }

        public void ReleaseMeshFileName()
        {
            mesh_filename = null;
        }
        public bool IsModeLive()
        {
            return ModeLive.Checked;
        }

        public bool IsModeRecord()
        {
            return ModeRecord.Checked;
        }

        public void WarnOfColorIssues()
        {
            MessageBox.Show("In this release, use of Solid is known to significantly reduce Texture accuracy. To work around this limitation, please unselect Solid or Texture.", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        public void WarnOfHtmlCompatibility()
        {
            MessageBox.Show("In this release, HTML generation is disabled when Texture is used. To inspect a textured mesh, import it into a mesh viewer (e.g. meshlab.sourceforge.net).", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        public bool isSolidificationSelected()
        {
            return Solid.Checked;
        }

        public bool isTextureSelected()
        {
            return Textured.Checked;
        }

        public bool isLandmarksSelected()
        {
            return Landmarks.Checked;
        }

        private void ModeLive_Click(object sender, EventArgs e)
        {
            ModeLive.Checked = true;
            ModePlayback.Checked = ModeRecord.Checked = false;
            PopulateDeviceMenu();
        }

        private void Playback()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = @"RSSDK clip|*.rssdk|All files|*.*";
            ofd.CheckFileExists = true;
            ofd.CheckPathExists = true;
            filename = (ofd.ShowDialog() == DialogResult.OK) ? ofd.FileName : null;
            ofd.Dispose();
            if (filename == null)
            {
                ModeLive.Checked = true;
                ModePlayback.Checked = ModeRecord.Checked = false;
                PopulateDeviceMenu();
            }
            else
            {
                ModePlayback.Checked = true;
                ModeLive.Checked = ModeRecord.Checked = false;
                if (PopulateDeviceFromFileMenu() == false)
                {
                    ModeLive.Checked = true;
                    ModePlayback.Checked = ModeRecord.Checked = false;
                }
            }
        }

        private void ModePlayback_Click(object sender, EventArgs e)
        {
            Playback();
        }

        private void ModeRecord_Click(object sender, EventArgs e)
        {
            ModeRecord.Checked = true;
            ModeLive.Checked = ModePlayback.Checked = false;
            PopulateDeviceMenu();

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = @"RSSDK clip|*.rssdk|All files|*.*";
            sfd.CheckPathExists = true;
            sfd.OverwritePrompt = true;
            try
            {
                filename = (sfd.ShowDialog() == DialogResult.OK) ? sfd.FileName : null;
            }
            catch
            {
                sfd.Dispose();
            }
            sfd.Dispose();
        }

        public bool GetStopState()
        {
            return stop;
        }

        public void StartScanning( bool enabled )
        {
            scanReconstruct = enabled;
        }

        private void Reconstruct_Click(object sender, System.EventArgs e)
        {
            if (scanReconstruct) SetButtonState( ButtonState.Ce_ESd );
            else EndScan();
            HideTrackingAlert();
            HideRangeAlerts();
            StartScanning(!scanReconstruct);
        }

        internal void EndScan()
        {
            SetButtonState(ButtonState.Cd_ESd);
            reconstruct_requested = true;
            stop = true;

            SaveFileDialog mesh_dialog = new SaveFileDialog();
            mesh_dialog.FileName = "3DScan";
            mesh_dialog.Filter = @"OBJ file|*.obj|PLY file|*.ply|STL file|*.stl";
            mesh_dialog.CheckPathExists = true;
            mesh_dialog.OverwritePrompt = true;
            try
            {
                mesh_filename = (mesh_dialog.ShowDialog() == DialogResult.OK) ? mesh_dialog.FileName : new String('c', 1);
            }
            catch
            {
                mesh_dialog.Dispose();
            }
            mesh_dialog.Dispose();
        }

        public bool landmarksChecked()
        {
            return Landmarks.Checked;
        }

        internal void HideLabel(Label l)
        {
            l.Visible = false;
        }

        internal void HideAlerts()
        {
            HideRangeAlerts();
            HideLabel(OutOfRange);
            HideLabel(FaceNotDetected);
            HideLabel(MoveRight);
            HideLabel(MoveLeft);
            HideLabel(MoveBack);
            HideLabel(MoveForward);
            HideLabel(TurnLeft);
            HideLabel(TurnRight);
            HideLabel(MoveDown);
            HideLabel(MoveUp);
            HideLabel(TiltDown);
            HideLabel(TiltUp);
        }

        internal void ShowLabel(Label l)
        {
            l.Visible = true;
        }

        internal void HideALERT_INSUFFICIENT_STRUCTURE()
        {
            OutOfRange.Visible = false;
        }

        internal void ShowTooClose()
        {
            TooClose.Visible = true;
        }

        internal void ShowTooFar()
        {
            TooFar.Visible = true;
        }

        internal void HideRangeAlerts()
        {
            TooClose.Visible = false;
            TooFar.Visible = false;
        }

        internal void ShowTrackingAlert()
        {
            TrackingLost.Visible = true;
        }

        internal void HideTrackingAlert()
        {
            TrackingLost.Visible = false;
        }

        internal void ResetStop()
        {
            stop = false;
            HideAlerts();
        }

        private void HelpClicked(object sender, System.EventArgs e)
        {
            try
            {
                string RSSDK_DIR = Environment.GetEnvironmentVariable("RSSDK_DIR");
                if (RSSDK_DIR != null)
                {
                    string helpFile = @"file://" + RSSDK_DIR + @"doc\chm\sdkhelp.chm";
                    System.Windows.Forms.Help.ShowHelp(
                        this, helpFile, @"\Intel\RSSDK\doc\CHM\sdkhelp.chm::/doc_scan_3d_scanning.html");
                }
            }
            catch { };
        }

        private void Textured_CheckedChanged(object sender, EventArgs e)
        {
            if (Textured.Checked && Solid.Checked) WarnOfColorIssues();
        }

        private void Solid_CheckedChanged(object sender, EventArgs e)
        {
            if (Textured.Checked && Solid.Checked) WarnOfColorIssues();
        }

        public void EnableReconstruction(bool enabled)
        {
            if (Reconstruct.Enabled != enabled)
            {
                Reconstruct.Enabled = enabled;
                if (enabled) Reconstruct.Focus();
                else Start.Focus();
            }
        }

        public bool getMaxVerticesEnabledChecked()
        {
            return MaxVerticesEnabled.Checked == true;
        }

        public int getMaxVertices()
        {
            return (int)this.MaxVertices.Value;
        }

        public bool getMaxTrianglesEnabledChecked()
        {
            return MaxTrianglesEnabled.Checked == true;
        }

        public int getMaxTriangles()
        {
            return (int)this.MaxTriangles.Value;
        }

        private void Landmarks_CheckedChanged(object sender, EventArgs e)
        {
            if (Landmarks.Checked == true)
            {
                if (!userFacingCamera)
                {
                    MessageBox.Show("This attached camera does not support Landmarks.", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    Landmarks.Checked = false;
                }
            }
        }

        private void ScanningArea_SelectedIndexChanged(object sender,
            System.EventArgs e)
        {
            if (userFacingCamera)
            {
                ComboBox comboBox = (ComboBox)sender;
                if (comboBox.SelectedIndex == ScanningArea.Items.IndexOf("Head"))
                {
                    MessageBox.Show("The attached camera does not have enough range for Head scanning.", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                else if (comboBox.SelectedIndex == ScanningArea.Items.IndexOf("Body"))
                {
                    MessageBox.Show("The attached camera does not have enough range for Body scanning.", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
            }
        }
    }
}
