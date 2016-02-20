namespace sample3dscan.cs
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.MaxVertices = new System.Windows.Forms.NumericUpDown();
            this.MaxTriangles = new System.Windows.Forms.NumericUpDown();
            this.Start = new System.Windows.Forms.Button();
            this.DeviceMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.ColorMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.ColorNone = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenu = new System.Windows.Forms.MenuStrip();
            this.DepthMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.DepthNone = new System.Windows.Forms.ToolStripMenuItem();
            this.ModeMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.ModeLive = new System.Windows.Forms.ToolStripMenuItem();
            this.ModePlayback = new System.Windows.Forms.ToolStripMenuItem();
            this.ModeRecord = new System.Windows.Forms.ToolStripMenuItem();
            this.Status2 = new System.Windows.Forms.StatusStrip();
            this.StatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.MainPanel = new System.Windows.Forms.PictureBox();
            this.Reconstruct = new System.Windows.Forms.Button();
            this.Solid = new System.Windows.Forms.CheckBox();
            this.ScanningArea = new System.Windows.Forms.ComboBox();
            this.Textured = new System.Windows.Forms.CheckBox();
            this.TooClose = new System.Windows.Forms.Label();
            this.TooFar = new System.Windows.Forms.Label();
            this.TrackingLost = new System.Windows.Forms.Label();
            this.Help = new System.Windows.Forms.Button();
            this.Landmarks = new System.Windows.Forms.CheckBox();
            this.MaxTrianglesEnabled = new System.Windows.Forms.CheckBox();
            this.MaxVerticesEnabled = new System.Windows.Forms.CheckBox();
            this.OutOfRange = new System.Windows.Forms.Label();
            this.FaceNotDetected = new System.Windows.Forms.Label();
            this.MoveRight = new System.Windows.Forms.Label();
            this.MoveLeft = new System.Windows.Forms.Label();
            this.MoveBack = new System.Windows.Forms.Label();
            this.MoveForward = new System.Windows.Forms.Label();
            this.TurnLeft = new System.Windows.Forms.Label();
            this.TurnRight = new System.Windows.Forms.Label();
            this.MoveDown = new System.Windows.Forms.Label();
            this.MoveUp = new System.Windows.Forms.Label();
            this.TiltDown = new System.Windows.Forms.Label();
            this.TiltUp = new System.Windows.Forms.Label();
            this.ModeLabel = new System.Windows.Forms.Label();
            this.OptionsLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.MaxVertices)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MaxTriangles)).BeginInit();
            this.MainMenu.SuspendLayout();
            this.Status2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MainPanel)).BeginInit();
            this.SuspendLayout();
            // 
            // MaxVertices
            // 
            this.MaxVertices.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MaxVertices.Increment = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.MaxVertices.Location = new System.Drawing.Point(22, 517);
            this.MaxVertices.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.MaxVertices.Name = "MaxVertices";
            this.MaxVertices.ReadOnly = true;
            this.MaxVertices.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.MaxVertices.Size = new System.Drawing.Size(117, 31);
            this.MaxVertices.TabIndex = 58;
            this.MaxVertices.ThousandsSeparator = true;
            this.MaxVertices.Value = new decimal(new int[] {
            60000,
            0,
            0,
            0});
            // 
            // MaxTriangles
            // 
            this.MaxTriangles.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MaxTriangles.Increment = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.MaxTriangles.Location = new System.Drawing.Point(22, 437);
            this.MaxTriangles.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.MaxTriangles.Name = "MaxTriangles";
            this.MaxTriangles.ReadOnly = true;
            this.MaxTriangles.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.MaxTriangles.Size = new System.Drawing.Size(117, 31);
            this.MaxTriangles.TabIndex = 54;
            this.MaxTriangles.ThousandsSeparator = true;
            this.MaxTriangles.Value = new decimal(new int[] {
            60000,
            0,
            0,
            0});
            // 
            // Start
            // 
            this.Start.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Start.Location = new System.Drawing.Point(7, 296);
            this.Start.Name = "Start";
            this.Start.Size = new System.Drawing.Size(134, 75);
            this.Start.TabIndex = 2;
            this.Start.Text = "Start Camera";
            this.Start.UseVisualStyleBackColor = true;
            this.Start.Click += new System.EventHandler(this.Start_Click);
            // 
            // DeviceMenu
            // 
            this.DeviceMenu.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DeviceMenu.Name = "DeviceMenu";
            this.DeviceMenu.Size = new System.Drawing.Size(90, 29);
            this.DeviceMenu.Text = "Device";
            // 
            // ColorMenu
            // 
            this.ColorMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ColorNone});
            this.ColorMenu.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ColorMenu.Name = "ColorMenu";
            this.ColorMenu.Size = new System.Drawing.Size(75, 29);
            this.ColorMenu.Text = "Color";
            this.ColorMenu.Click += new System.EventHandler(this.ColorMenu_Click);
            // 
            // ColorNone
            // 
            this.ColorNone.Name = "ColorNone";
            this.ColorNone.Size = new System.Drawing.Size(135, 30);
            this.ColorNone.Text = "None";
            // 
            // MainMenu
            // 
            this.MainMenu.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DeviceMenu,
            this.ColorMenu,
            this.DepthMenu,
            this.ModeMenu});
            this.MainMenu.Location = new System.Drawing.Point(0, 0);
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            this.MainMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.MainMenu.Size = new System.Drawing.Size(874, 33);
            this.MainMenu.TabIndex = 0;
            this.MainMenu.Text = "MainMenu";
            // 
            // DepthMenu
            // 
            this.DepthMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DepthNone});
            this.DepthMenu.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DepthMenu.Name = "DepthMenu";
            this.DepthMenu.Size = new System.Drawing.Size(81, 29);
            this.DepthMenu.Text = "Depth";
            // 
            // DepthNone
            // 
            this.DepthNone.Name = "DepthNone";
            this.DepthNone.Size = new System.Drawing.Size(135, 30);
            this.DepthNone.Text = "None";
            // 
            // ModeMenu
            // 
            this.ModeMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ModeLive,
            this.ModePlayback,
            this.ModeRecord});
            this.ModeMenu.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ModeMenu.Name = "ModeMenu";
            this.ModeMenu.Size = new System.Drawing.Size(78, 29);
            this.ModeMenu.Text = "Mode";
            // 
            // ModeLive
            // 
            this.ModeLive.Checked = true;
            this.ModeLive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ModeLive.Name = "ModeLive";
            this.ModeLive.Size = new System.Drawing.Size(172, 30);
            this.ModeLive.Text = "Live";
            this.ModeLive.Click += new System.EventHandler(this.ModeLive_Click);
            // 
            // ModePlayback
            // 
            this.ModePlayback.Name = "ModePlayback";
            this.ModePlayback.Size = new System.Drawing.Size(172, 30);
            this.ModePlayback.Text = "Playback";
            this.ModePlayback.Click += new System.EventHandler(this.ModePlayback_Click);
            // 
            // ModeRecord
            // 
            this.ModeRecord.Name = "ModeRecord";
            this.ModeRecord.Size = new System.Drawing.Size(172, 30);
            this.ModeRecord.Text = "Record";
            this.ModeRecord.Click += new System.EventHandler(this.ModeRecord_Click);
            // 
            // Status2
            // 
            this.Status2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F);
            this.Status2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusLabel});
            this.Status2.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.Status2.Location = new System.Drawing.Point(0, 566);
            this.Status2.Name = "Status2";
            this.Status2.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
            this.Status2.Size = new System.Drawing.Size(874, 0);
            this.Status2.TabIndex = 25;
            this.Status2.Text = "OK";
            // 
            // StatusLabel
            // 
            this.StatusLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StatusLabel.Margin = new System.Windows.Forms.Padding(140, 0, 0, 0);
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(0, 0);
            // 
            // MainPanel
            // 
            this.MainPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MainPanel.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.MainPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.MainPanel.ErrorImage = null;
            this.MainPanel.InitialImage = null;
            this.MainPanel.Location = new System.Drawing.Point(147, 36);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(576, 496);
            this.MainPanel.TabIndex = 27;
            this.MainPanel.TabStop = false;
            // 
            // Reconstruct
            // 
            this.Reconstruct.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Reconstruct.Enabled = false;
            this.Reconstruct.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Reconstruct.Location = new System.Drawing.Point(734, 293);
            this.Reconstruct.Name = "Reconstruct";
            this.Reconstruct.Size = new System.Drawing.Size(132, 78);
            this.Reconstruct.TabIndex = 4;
            this.Reconstruct.Text = "Start Scanning";
            this.Reconstruct.UseVisualStyleBackColor = true;
            this.Reconstruct.Click += new System.EventHandler(this.Reconstruct_Click);
            // 
            // Solid
            // 
            this.Solid.AutoSize = true;
            this.Solid.BackColor = System.Drawing.SystemColors.Control;
            this.Solid.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.Solid.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Solid.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Solid.Location = new System.Drawing.Point(12, 212);
            this.Solid.Name = "Solid";
            this.Solid.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Solid.Size = new System.Drawing.Size(63, 24);
            this.Solid.TabIndex = 48;
            this.Solid.Text = "Solid";
            this.Solid.UseVisualStyleBackColor = false;
            this.Solid.CheckedChanged += new System.EventHandler(this.Solid_CheckedChanged);
            // 
            // ScanningArea
            // 
            this.ScanningArea.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ScanningArea.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ScanningArea.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ScanningArea.Items.AddRange(new object[] {
            "Object",
            "Face",
            "Head",
            "Body",
            "Full"});
            this.ScanningArea.Location = new System.Drawing.Point(12, 92);
            this.ScanningArea.Name = "ScanningArea";
            this.ScanningArea.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ScanningArea.Size = new System.Drawing.Size(124, 33);
            this.ScanningArea.TabIndex = 44;
            this.ScanningArea.SelectedIndexChanged += new System.EventHandler(ScanningArea_SelectedIndexChanged);

            // 
            // Textured
            // 
            this.Textured.AutoSize = true;
            this.Textured.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Textured.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Textured.Location = new System.Drawing.Point(12, 241);
            this.Textured.Name = "Textured";
            this.Textured.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Textured.Size = new System.Drawing.Size(81, 24);
            this.Textured.TabIndex = 50;
            this.Textured.Text = "Texture";
            this.Textured.UseVisualStyleBackColor = true;
            this.Textured.CheckedChanged += new System.EventHandler(this.Textured_CheckedChanged);
            // 
            // TooClose
            // 
            this.TooClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TooClose.AutoSize = true;
            this.TooClose.BackColor = System.Drawing.Color.Red;
            this.TooClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.TooClose.ForeColor = System.Drawing.Color.White;
            this.TooClose.Location = new System.Drawing.Point(729, 399);
            this.TooClose.Name = "TooClose";
            this.TooClose.Size = new System.Drawing.Size(85, 20);
            this.TooClose.TabIndex = 47;
            this.TooClose.Text = "Move back";
            this.TooClose.Visible = false;
            // 
            // TooFar
            // 
            this.TooFar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TooFar.AutoSize = true;
            this.TooFar.BackColor = System.Drawing.Color.Red;
            this.TooFar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.TooFar.ForeColor = System.Drawing.Color.White;
            this.TooFar.Location = new System.Drawing.Point(729, 379);
            this.TooFar.Name = "TooFar";
            this.TooFar.Size = new System.Drawing.Size(104, 20);
            this.TooFar.TabIndex = 48;
            this.TooFar.Text = "Move forward";
            this.TooFar.Visible = false;
            // 
            // TrackingLost
            // 
            this.TrackingLost.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TrackingLost.AutoSize = true;
            this.TrackingLost.BackColor = System.Drawing.Color.Red;
            this.TrackingLost.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TrackingLost.ForeColor = System.Drawing.Color.White;
            this.TrackingLost.Location = new System.Drawing.Point(327, 3);
            this.TrackingLost.Name = "TrackingLost";
            this.TrackingLost.Size = new System.Drawing.Size(421, 29);
            this.TrackingLost.TabIndex = 49;
            this.TrackingLost.Text = "Tracking lost! Align images to recover.";
            this.TrackingLost.Visible = false;
            // 
            // Help
            // 
            this.Help.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Help.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F);
            this.Help.Location = new System.Drawing.Point(755, 0);
            this.Help.Name = "Help";
            this.Help.Size = new System.Drawing.Size(119, 33);
            this.Help.TabIndex = 100;
            this.Help.Text = "Help";
            this.Help.UseVisualStyleBackColor = true;
            this.Help.Click += new System.EventHandler(this.HelpClicked);
            // 
            // Landmarks
            // 
            this.Landmarks.AutoSize = true;
            this.Landmarks.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Landmarks.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Landmarks.Location = new System.Drawing.Point(12, 182);
            this.Landmarks.Name = "Landmarks";
            this.Landmarks.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Landmarks.Size = new System.Drawing.Size(107, 24);
            this.Landmarks.TabIndex = 46;
            this.Landmarks.Text = "Landmarks";
            this.Landmarks.UseVisualStyleBackColor = true;
            this.Landmarks.CheckedChanged += new System.EventHandler(this.Landmarks_CheckedChanged);
            // 
            // MaxTrianglesEnabled
            // 
            this.MaxTrianglesEnabled.AutoSize = true;
            this.MaxTrianglesEnabled.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MaxTrianglesEnabled.Location = new System.Drawing.Point(12, 407);
            this.MaxTrianglesEnabled.Name = "MaxTrianglesEnabled";
            this.MaxTrianglesEnabled.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.MaxTrianglesEnabled.Size = new System.Drawing.Size(129, 24);
            this.MaxTrianglesEnabled.TabIndex = 52;
            this.MaxTrianglesEnabled.Text = "Max. Triangles";
            this.MaxTrianglesEnabled.UseVisualStyleBackColor = true;
            // 
            // MaxVerticesEnabled
            // 
            this.MaxVerticesEnabled.AutoSize = true;
            this.MaxVerticesEnabled.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MaxVerticesEnabled.Location = new System.Drawing.Point(12, 487);
            this.MaxVerticesEnabled.Name = "MaxVerticesEnabled";
            this.MaxVerticesEnabled.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.MaxVerticesEnabled.Size = new System.Drawing.Size(123, 24);
            this.MaxVerticesEnabled.TabIndex = 56;
            this.MaxVerticesEnabled.Text = "Max. Vertices";
            this.MaxVerticesEnabled.UseVisualStyleBackColor = true;
            // 
            // OutOfRange
            // 
            this.OutOfRange.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.OutOfRange.AutoSize = true;
            this.OutOfRange.BackColor = System.Drawing.Color.Red;
            this.OutOfRange.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.OutOfRange.ForeColor = System.Drawing.Color.White;
            this.OutOfRange.Location = new System.Drawing.Point(729, 45);
            this.OutOfRange.Name = "OutOfRange";
            this.OutOfRange.Size = new System.Drawing.Size(98, 20);
            this.OutOfRange.TabIndex = 57;
            this.OutOfRange.Text = "Out of range";
            this.OutOfRange.Visible = false;
            // 
            // FaceNotDetected
            // 
            this.FaceNotDetected.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.FaceNotDetected.AutoSize = true;
            this.FaceNotDetected.BackColor = System.Drawing.Color.Red;
            this.FaceNotDetected.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FaceNotDetected.ForeColor = System.Drawing.Color.White;
            this.FaceNotDetected.Location = new System.Drawing.Point(729, 65);
            this.FaceNotDetected.Name = "FaceNotDetected";
            this.FaceNotDetected.Size = new System.Drawing.Size(139, 20);
            this.FaceNotDetected.TabIndex = 58;
            this.FaceNotDetected.Text = "Face not detected";
            this.FaceNotDetected.Visible = false;
            // 
            // MoveRight
            // 
            this.MoveRight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.MoveRight.AutoSize = true;
            this.MoveRight.BackColor = System.Drawing.Color.Red;
            this.MoveRight.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.MoveRight.ForeColor = System.Drawing.Color.White;
            this.MoveRight.Location = new System.Drawing.Point(729, 145);
            this.MoveRight.Name = "MoveRight";
            this.MoveRight.Size = new System.Drawing.Size(82, 20);
            this.MoveRight.TabIndex = 59;
            this.MoveRight.Text = "Move right";
            this.MoveRight.Visible = false;
            // 
            // MoveLeft
            // 
            this.MoveLeft.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.MoveLeft.AutoSize = true;
            this.MoveLeft.BackColor = System.Drawing.Color.Red;
            this.MoveLeft.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.MoveLeft.ForeColor = System.Drawing.Color.White;
            this.MoveLeft.Location = new System.Drawing.Point(729, 125);
            this.MoveLeft.Name = "MoveLeft";
            this.MoveLeft.Size = new System.Drawing.Size(73, 20);
            this.MoveLeft.TabIndex = 60;
            this.MoveLeft.Text = "Move left";
            this.MoveLeft.Visible = false;
            // 
            // MoveBack
            // 
            this.MoveBack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.MoveBack.AutoSize = true;
            this.MoveBack.BackColor = System.Drawing.Color.Red;
            this.MoveBack.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.MoveBack.ForeColor = System.Drawing.Color.White;
            this.MoveBack.Location = new System.Drawing.Point(729, 105);
            this.MoveBack.Name = "MoveBack";
            this.MoveBack.Size = new System.Drawing.Size(85, 20);
            this.MoveBack.TabIndex = 61;
            this.MoveBack.Text = "Move back";
            this.MoveBack.Visible = false;
            // 
            // MoveForward
            // 
            this.MoveForward.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.MoveForward.AutoSize = true;
            this.MoveForward.BackColor = System.Drawing.Color.Red;
            this.MoveForward.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.MoveForward.ForeColor = System.Drawing.Color.White;
            this.MoveForward.Location = new System.Drawing.Point(729, 85);
            this.MoveForward.Name = "MoveForward";
            this.MoveForward.Size = new System.Drawing.Size(104, 20);
            this.MoveForward.TabIndex = 62;
            this.MoveForward.Text = "Move forward";
            this.MoveForward.Visible = false;
            // 
            // TurnLeft
            // 
            this.TurnLeft.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TurnLeft.AutoSize = true;
            this.TurnLeft.BackColor = System.Drawing.Color.Red;
            this.TurnLeft.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.TurnLeft.ForeColor = System.Drawing.Color.White;
            this.TurnLeft.Location = new System.Drawing.Point(729, 245);
            this.TurnLeft.Name = "TurnLeft";
            this.TurnLeft.Size = new System.Drawing.Size(67, 20);
            this.TurnLeft.TabIndex = 63;
            this.TurnLeft.Text = "Turn left";
            this.TurnLeft.Visible = false;
            // 
            // TurnRight
            // 
            this.TurnRight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TurnRight.AutoSize = true;
            this.TurnRight.BackColor = System.Drawing.Color.Red;
            this.TurnRight.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.TurnRight.ForeColor = System.Drawing.Color.White;
            this.TurnRight.Location = new System.Drawing.Point(729, 265);
            this.TurnRight.Name = "TurnRight";
            this.TurnRight.Size = new System.Drawing.Size(76, 20);
            this.TurnRight.TabIndex = 64;
            this.TurnRight.Text = "Turn right";
            this.TurnRight.Visible = false;
            // 
            // MoveDown
            // 
            this.MoveDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.MoveDown.AutoSize = true;
            this.MoveDown.BackColor = System.Drawing.Color.Red;
            this.MoveDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.MoveDown.ForeColor = System.Drawing.Color.White;
            this.MoveDown.Location = new System.Drawing.Point(729, 165);
            this.MoveDown.Name = "MoveDown";
            this.MoveDown.Size = new System.Drawing.Size(89, 20);
            this.MoveDown.TabIndex = 65;
            this.MoveDown.Text = "Move down";
            this.MoveDown.Visible = false;
            // 
            // MoveUp
            // 
            this.MoveUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.MoveUp.AutoSize = true;
            this.MoveUp.BackColor = System.Drawing.Color.Red;
            this.MoveUp.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.MoveUp.ForeColor = System.Drawing.Color.White;
            this.MoveUp.Location = new System.Drawing.Point(729, 185);
            this.MoveUp.Name = "MoveUp";
            this.MoveUp.Size = new System.Drawing.Size(69, 20);
            this.MoveUp.TabIndex = 66;
            this.MoveUp.Text = "Move up";
            this.MoveUp.Visible = false;
            // 
            // TiltDown
            // 
            this.TiltDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TiltDown.AutoSize = true;
            this.TiltDown.BackColor = System.Drawing.Color.Red;
            this.TiltDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.TiltDown.ForeColor = System.Drawing.Color.White;
            this.TiltDown.Location = new System.Drawing.Point(729, 205);
            this.TiltDown.Name = "TiltDown";
            this.TiltDown.Size = new System.Drawing.Size(71, 20);
            this.TiltDown.TabIndex = 67;
            this.TiltDown.Text = "Tilt down";
            this.TiltDown.Visible = false;
            // 
            // TiltUp
            // 
            this.TiltUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TiltUp.AutoSize = true;
            this.TiltUp.BackColor = System.Drawing.Color.Red;
            this.TiltUp.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.TiltUp.ForeColor = System.Drawing.Color.White;
            this.TiltUp.Location = new System.Drawing.Point(729, 225);
            this.TiltUp.Name = "TiltUp";
            this.TiltUp.Size = new System.Drawing.Size(54, 20);
            this.TiltUp.TabIndex = 68;
            this.TiltUp.Text = "Tilt Up";
            this.TiltUp.Visible = false;
            // 
            // ModeLabel
            // 
            this.ModeLabel.AutoSize = true;
            this.ModeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ModeLabel.Location = new System.Drawing.Point(4, 65);
            this.ModeLabel.Name = "ModeLabel";
            this.ModeLabel.Size = new System.Drawing.Size(127, 20);
            this.ModeLabel.TabIndex = 69;
            this.ModeLabel.Text = "Scanning Area";
            // 
            // OptionsLabel
            // 
            this.OptionsLabel.AutoSize = true;
            this.OptionsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.OptionsLabel.Location = new System.Drawing.Point(4, 159);
            this.OptionsLabel.Name = "OptionsLabel";
            this.OptionsLabel.Size = new System.Drawing.Size(71, 20);
            this.OptionsLabel.TabIndex = 70;
            this.OptionsLabel.Text = "Options";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(874, 566);
            this.Controls.Add(this.OptionsLabel);
            this.Controls.Add(this.ModeLabel);
            this.Controls.Add(this.TiltUp);
            this.Controls.Add(this.TiltDown);
            this.Controls.Add(this.MoveUp);
            this.Controls.Add(this.MoveDown);
            this.Controls.Add(this.TurnRight);
            this.Controls.Add(this.TurnLeft);
            this.Controls.Add(this.MoveForward);
            this.Controls.Add(this.MoveBack);
            this.Controls.Add(this.MoveLeft);
            this.Controls.Add(this.MoveRight);
            this.Controls.Add(this.FaceNotDetected);
            this.Controls.Add(this.OutOfRange);
            this.Controls.Add(this.MaxVerticesEnabled);
            this.Controls.Add(this.MaxTrianglesEnabled);
            this.Controls.Add(this.Landmarks);
            this.Controls.Add(this.MaxVertices);
            this.Controls.Add(this.MaxTriangles);
            this.Controls.Add(this.Help);
            this.Controls.Add(this.TrackingLost);
            this.Controls.Add(this.TooFar);
            this.Controls.Add(this.TooClose);
            this.Controls.Add(this.Solid);
            this.Controls.Add(this.ScanningArea);
            this.Controls.Add(this.Textured);
            this.Controls.Add(this.Reconstruct);
            this.Controls.Add(this.MainPanel);
            this.Controls.Add(this.Status2);
            this.Controls.Add(this.Start);
            this.Controls.Add(this.MainMenu);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MainMenuStrip = this.MainMenu;
            this.Name = "MainForm";
            this.Text = "Intel(R) RealSense(TM) SDK - DF_3DScan.cs Sample";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.MaxVertices)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MaxTriangles)).EndInit();
            this.MainMenu.ResumeLayout(false);
            this.MainMenu.PerformLayout();
            this.Status2.ResumeLayout(false);
            this.Status2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MainPanel)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void ColorMenu_Click(object sender, System.EventArgs e)
        {
            this.ColorMenu.ShowDropDown();
        }

        #endregion

        private System.Windows.Forms.NumericUpDown MaxVertices;
        private System.Windows.Forms.NumericUpDown MaxTriangles;
        private System.Windows.Forms.Button Start;
        private System.Windows.Forms.ToolStripMenuItem DeviceMenu;
        private System.Windows.Forms.ToolStripMenuItem ColorMenu;
        private System.Windows.Forms.MenuStrip MainMenu;
        private System.Windows.Forms.StatusStrip Status2;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabel;
        private System.Windows.Forms.PictureBox MainPanel;
        private System.Windows.Forms.ToolStripMenuItem ModeMenu;
        private System.Windows.Forms.ToolStripMenuItem ModeLive;
        private System.Windows.Forms.ToolStripMenuItem ModePlayback;
        private System.Windows.Forms.ToolStripMenuItem ModeRecord;
        private System.Windows.Forms.ToolStripMenuItem DepthMenu;
        private System.Windows.Forms.ToolStripMenuItem ColorNone;
        private System.Windows.Forms.ToolStripMenuItem DepthNone;
        private System.Windows.Forms.Button Reconstruct;
        private System.Windows.Forms.CheckBox Solid;
        private System.Windows.Forms.ComboBox ScanningArea;
        private System.Windows.Forms.CheckBox Textured;
        private System.Windows.Forms.Label TooClose;
        private System.Windows.Forms.Label TooFar;
        private System.Windows.Forms.Label TrackingLost;
        private System.Windows.Forms.Button Help;
        private System.Windows.Forms.CheckBox Landmarks;
        private System.Windows.Forms.CheckBox MaxTrianglesEnabled;
        private System.Windows.Forms.CheckBox MaxVerticesEnabled;
        public System.Windows.Forms.Label OutOfRange;
        public System.Windows.Forms.Label FaceNotDetected;
        public System.Windows.Forms.Label MoveRight;
        public System.Windows.Forms.Label MoveLeft;
        public System.Windows.Forms.Label MoveBack;
        public System.Windows.Forms.Label MoveForward;
        public System.Windows.Forms.Label TurnLeft;
        public System.Windows.Forms.Label TurnRight;
        public System.Windows.Forms.Label MoveDown;
        public System.Windows.Forms.Label MoveUp;
        public System.Windows.Forms.Label TiltDown;
        public System.Windows.Forms.Label TiltUp;
        private System.Windows.Forms.Label ModeLabel;
        private System.Windows.Forms.Label OptionsLabel;
    }
}