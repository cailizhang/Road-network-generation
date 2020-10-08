namespace LinkIdentification
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.加载shpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.文章2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.删除狭长边ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.探测候选linkToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.规则2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.规则3ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.探测候选linkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.规则4ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.剔除伪link规则3迭代ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.计算linkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.优化匹配交叉点4ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.axTOCControl1 = new ESRI.ArcGIS.Controls.AxTOCControl();
            this.axLicenseControl1 = new ESRI.ArcGIS.Controls.AxLicenseControl();
            this.axMapControl1 = new ESRI.ArcGIS.Controls.AxMapControl();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axTOCControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axMapControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.文件ToolStripMenuItem,
            this.文章2ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(620, 25);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 文件ToolStripMenuItem
            // 
            this.文件ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.加载shpToolStripMenuItem});
            this.文件ToolStripMenuItem.Name = "文件ToolStripMenuItem";
            this.文件ToolStripMenuItem.Size = new System.Drawing.Size(96, 21);
            this.文件ToolStripMenuItem.Text = "Load dataset";
            // 
            // 加载shpToolStripMenuItem
            // 
            this.加载shpToolStripMenuItem.Name = "加载shpToolStripMenuItem";
            this.加载shpToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.加载shpToolStripMenuItem.Text = "Load shp file";
            this.加载shpToolStripMenuItem.Click += new System.EventHandler(this.加载shpToolStripMenuItem_Click);
            // 
            // 文章2ToolStripMenuItem
            // 
            this.文章2ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.删除狭长边ToolStripMenuItem,
            this.探测候选linkToolStripMenuItem1,
            this.规则2ToolStripMenuItem,
            this.规则3ToolStripMenuItem,
            this.探测候选linkToolStripMenuItem,
            this.规则4ToolStripMenuItem,
            this.剔除伪link规则3迭代ToolStripMenuItem,
            this.计算linkToolStripMenuItem,
            this.优化匹配交叉点4ToolStripMenuItem});
            this.文章2ToolStripMenuItem.Name = "文章2ToolStripMenuItem";
            this.文章2ToolStripMenuItem.Size = new System.Drawing.Size(105, 21);
            this.文章2ToolStripMenuItem.Text = "Main Methods";
            // 
            // 删除狭长边ToolStripMenuItem
            // 
            this.删除狭长边ToolStripMenuItem.Name = "删除狭长边ToolStripMenuItem";
            this.删除狭长边ToolStripMenuItem.Size = new System.Drawing.Size(331, 22);
            this.删除狭长边ToolStripMenuItem.Text = "delete peripheral long and narrow triangles";
            this.删除狭长边ToolStripMenuItem.Click += new System.EventHandler(this.删除狭长边ToolStripMenuItem_Click);
            // 
            // 探测候选linkToolStripMenuItem1
            // 
            this.探测候选linkToolStripMenuItem1.Name = "探测候选linkToolStripMenuItem1";
            this.探测候选linkToolStripMenuItem1.Size = new System.Drawing.Size(331, 22);
            this.探测候选linkToolStripMenuItem1.Text = "Criterion 1";
            this.探测候选linkToolStripMenuItem1.Click += new System.EventHandler(this.探测候选linkToolStripMenuItem1_Click);
            // 
            // 规则2ToolStripMenuItem
            // 
            this.规则2ToolStripMenuItem.Name = "规则2ToolStripMenuItem";
            this.规则2ToolStripMenuItem.Size = new System.Drawing.Size(331, 22);
            this.规则2ToolStripMenuItem.Text = "Criterion 2";
            this.规则2ToolStripMenuItem.Click += new System.EventHandler(this.规则2ToolStripMenuItem_Click);
            // 
            // 规则3ToolStripMenuItem
            // 
            this.规则3ToolStripMenuItem.Name = "规则3ToolStripMenuItem";
            this.规则3ToolStripMenuItem.Size = new System.Drawing.Size(331, 22);
            this.规则3ToolStripMenuItem.Text = "Criterion 3";
            this.规则3ToolStripMenuItem.Click += new System.EventHandler(this.规则3ToolStripMenuItem_Click);
            // 
            // 探测候选linkToolStripMenuItem
            // 
            this.探测候选linkToolStripMenuItem.Name = "探测候选linkToolStripMenuItem";
            this.探测候选linkToolStripMenuItem.Size = new System.Drawing.Size(331, 22);
            this.探测候选linkToolStripMenuItem.Text = "Criterion 4";
            this.探测候选linkToolStripMenuItem.Click += new System.EventHandler(this.探测候选linkToolStripMenuItem_Click);
            // 
            // 规则4ToolStripMenuItem
            // 
            this.规则4ToolStripMenuItem.Name = "规则4ToolStripMenuItem";
            this.规则4ToolStripMenuItem.Size = new System.Drawing.Size(331, 22);
            this.规则4ToolStripMenuItem.Text = "Criterion 5";
            this.规则4ToolStripMenuItem.Click += new System.EventHandler(this.规则4ToolStripMenuItem_Click);
            // 
            // 剔除伪link规则3迭代ToolStripMenuItem
            // 
            this.剔除伪link规则3迭代ToolStripMenuItem.Name = "剔除伪link规则3迭代ToolStripMenuItem";
            this.剔除伪link规则3迭代ToolStripMenuItem.Size = new System.Drawing.Size(331, 22);
            this.剔除伪link规则3迭代ToolStripMenuItem.Text = "Criterion 6";
            this.剔除伪link规则3迭代ToolStripMenuItem.Click += new System.EventHandler(this.剔除伪link规则3迭代ToolStripMenuItem_Click);
            // 
            // 计算linkToolStripMenuItem
            // 
            this.计算linkToolStripMenuItem.Name = "计算linkToolStripMenuItem";
            this.计算linkToolStripMenuItem.Size = new System.Drawing.Size(331, 22);
            this.计算linkToolStripMenuItem.Text = "sub-trajectory point extraction";
            this.计算linkToolStripMenuItem.Click += new System.EventHandler(this.计算linkToolStripMenuItem_Click);
            // 
            // 优化匹配交叉点4ToolStripMenuItem
            // 
            this.优化匹配交叉点4ToolStripMenuItem.Name = "优化匹配交叉点4ToolStripMenuItem";
            this.优化匹配交叉点4ToolStripMenuItem.Size = new System.Drawing.Size(331, 22);
            this.优化匹配交叉点4ToolStripMenuItem.Text = "Optimizing by morphological methods";
            this.优化匹配交叉点4ToolStripMenuItem.Click += new System.EventHandler(this.优化匹配交叉点4ToolStripMenuItem_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 25);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.axTOCControl1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.axLicenseControl1);
            this.splitContainer1.Panel2.Controls.Add(this.axMapControl1);
            this.splitContainer1.Size = new System.Drawing.Size(620, 436);
            this.splitContainer1.SplitterDistance = 206;
            this.splitContainer1.TabIndex = 2;
            // 
            // axTOCControl1
            // 
            this.axTOCControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axTOCControl1.Location = new System.Drawing.Point(0, 0);
            this.axTOCControl1.Name = "axTOCControl1";
            this.axTOCControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axTOCControl1.OcxState")));
            this.axTOCControl1.Size = new System.Drawing.Size(206, 436);
            this.axTOCControl1.TabIndex = 0;
            // 
            // axLicenseControl1
            // 
            this.axLicenseControl1.Enabled = true;
            this.axLicenseControl1.Location = new System.Drawing.Point(207, 334);
            this.axLicenseControl1.Name = "axLicenseControl1";
            this.axLicenseControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axLicenseControl1.OcxState")));
            this.axLicenseControl1.Size = new System.Drawing.Size(32, 32);
            this.axLicenseControl1.TabIndex = 1;
            // 
            // axMapControl1
            // 
            this.axMapControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axMapControl1.Location = new System.Drawing.Point(0, 0);
            this.axMapControl1.Name = "axMapControl1";
            this.axMapControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axMapControl1.OcxState")));
            this.axMapControl1.Size = new System.Drawing.Size(410, 436);
            this.axMapControl1.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(620, 461);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "road network extraction";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axTOCControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axMapControl1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private ESRI.ArcGIS.Controls.AxMapControl axMapControl1;
        private System.Windows.Forms.ToolStripMenuItem 文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 加载shpToolStripMenuItem;
        private ESRI.ArcGIS.Controls.AxTOCControl axTOCControl1;
        private ESRI.ArcGIS.Controls.AxLicenseControl axLicenseControl1;
        private System.Windows.Forms.ToolStripMenuItem 文章2ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 探测候选linkToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 探测候选linkToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 计算linkToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 删除狭长边ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 剔除伪link规则3迭代ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 规则4ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 优化匹配交叉点4ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 规则2ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 规则3ToolStripMenuItem;
    }
}

