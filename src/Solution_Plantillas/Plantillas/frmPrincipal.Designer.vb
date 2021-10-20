<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPrincipal
	Inherits Telerik.WinControls.UI.RadForm

	'Form overrides dispose to clean up the component list.
	<System.Diagnostics.DebuggerNonUserCode()> _
	Protected Overrides Sub Dispose(ByVal disposing As Boolean)
		Try
			If disposing AndAlso components IsNot Nothing Then
				components.Dispose()
			End If
		Finally
			MyBase.Dispose(disposing)
		End Try
	End Sub

	'Required by the Windows Form Designer
	Private components As System.ComponentModel.IContainer

	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.  
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()> _
	Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPrincipal))
        Me.mnuPlantillas = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.mnuPlaCapturaPlazas = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuPlaCapturaPlantillas = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuPlaConsultaPlantillas = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuPlaCargaReportes = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuPlaConsultaSabaticos = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMantenimientoPlaza = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuPlaConsultaSuplencias = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuCatalogos = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.mnuCatMotivos = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuCatCondiciones = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuCatNiveles = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuHistoria = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.mnuHisCambiosHistoricos = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuHisConsultaHistoricos = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuReportes = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.mnuRepSeleccion = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuRepPlazas = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuRepPlazaPersona = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.picLogo = New System.Windows.Forms.PictureBox()
        Me.cmdHistoria = New Telerik.WinControls.UI.RadButton()
        Me.cmdEntidades = New Telerik.WinControls.UI.RadButton()
        Me.cmdCatalogos = New Telerik.WinControls.UI.RadButton()
        Me.cmdPlantillas = New Telerik.WinControls.UI.RadButton()
        Me.cmdReportes = New Telerik.WinControls.UI.RadButton()
        Me.cmdSalir = New Telerik.WinControls.UI.RadButton()
        Me.cmdAnalisis = New Telerik.WinControls.UI.RadButton()
        Me.mnuPlantillas.SuspendLayout()
        Me.mnuCatalogos.SuspendLayout()
        Me.mnuHistoria.SuspendLayout()
        Me.mnuReportes.SuspendLayout()
        CType(Me.picLogo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmdHistoria, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmdEntidades, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmdCatalogos, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmdPlantillas, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmdReportes, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmdSalir, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmdAnalisis, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'mnuPlantillas
        '
        Me.mnuPlantillas.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuPlaCapturaPlazas, Me.mnuPlaCapturaPlantillas, Me.mnuPlaConsultaPlantillas, Me.mnuPlaCargaReportes, Me.mnuPlaConsultaSabaticos, Me.mnuMantenimientoPlaza, Me.mnuPlaConsultaSuplencias})
        Me.mnuPlantillas.Name = "ContextMenuStrip1"
        Me.mnuPlantillas.Size = New System.Drawing.Size(247, 158)
        '
        'mnuPlaCapturaPlazas
        '
        Me.mnuPlaCapturaPlazas.Name = "mnuPlaCapturaPlazas"
        Me.mnuPlaCapturaPlazas.Size = New System.Drawing.Size(246, 22)
        Me.mnuPlaCapturaPlazas.Text = "Captura de Plazas"
        '
        'mnuPlaCapturaPlantillas
        '
        Me.mnuPlaCapturaPlantillas.Name = "mnuPlaCapturaPlantillas"
        Me.mnuPlaCapturaPlantillas.Size = New System.Drawing.Size(246, 22)
        Me.mnuPlaCapturaPlantillas.Text = "Captura de Plantillas"
        '
        'mnuPlaConsultaPlantillas
        '
        Me.mnuPlaConsultaPlantillas.Name = "mnuPlaConsultaPlantillas"
        Me.mnuPlaConsultaPlantillas.Size = New System.Drawing.Size(246, 22)
        Me.mnuPlaConsultaPlantillas.Text = "Consulta de Plantillas"
        '
        'mnuPlaCargaReportes
        '
        Me.mnuPlaCargaReportes.Name = "mnuPlaCargaReportes"
        Me.mnuPlaCargaReportes.Size = New System.Drawing.Size(246, 22)
        Me.mnuPlaCargaReportes.Text = "Carga Reporte Plazas"
        '
        'mnuPlaConsultaSabaticos
        '
        Me.mnuPlaConsultaSabaticos.Name = "mnuPlaConsultaSabaticos"
        Me.mnuPlaConsultaSabaticos.Size = New System.Drawing.Size(246, 22)
        Me.mnuPlaConsultaSabaticos.Text = "Consulta Sabáticos Terminados"
        '
        'mnuMantenimientoPlaza
        '
        Me.mnuMantenimientoPlaza.Name = "mnuMantenimientoPlaza"
        Me.mnuMantenimientoPlaza.Size = New System.Drawing.Size(246, 22)
        Me.mnuMantenimientoPlaza.Text = "Mantenimiento Solicitudes Plaza"
        '
        'mnuPlaConsultaSuplencias
        '
        Me.mnuPlaConsultaSuplencias.Name = "mnuPlaConsultaSuplencias"
        Me.mnuPlaConsultaSuplencias.Size = New System.Drawing.Size(246, 22)
        Me.mnuPlaConsultaSuplencias.Text = "Consulta Suplencias"
        '
        'mnuCatalogos
        '
        Me.mnuCatalogos.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuCatMotivos, Me.mnuCatCondiciones, Me.mnuCatNiveles})
        Me.mnuCatalogos.Name = "mnuProcesos"
        Me.mnuCatalogos.Size = New System.Drawing.Size(267, 70)
        '
        'mnuCatMotivos
        '
        Me.mnuCatMotivos.Name = "mnuCatMotivos"
        Me.mnuCatMotivos.Size = New System.Drawing.Size(266, 22)
        Me.mnuCatMotivos.Text = "Motivos"
        '
        'mnuCatCondiciones
        '
        Me.mnuCatCondiciones.Name = "mnuCatCondiciones"
        Me.mnuCatCondiciones.Size = New System.Drawing.Size(266, 22)
        Me.mnuCatCondiciones.Text = "Condiciones"
        '
        'mnuCatNiveles
        '
        Me.mnuCatNiveles.Name = "mnuCatNiveles"
        Me.mnuCatNiveles.Size = New System.Drawing.Size(266, 22)
        Me.mnuCatNiveles.Text = "Niveles de Estructura Organizacional"
        '
        'mnuHistoria
        '
        Me.mnuHistoria.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuHisCambiosHistoricos, Me.mnuHisConsultaHistoricos})
        Me.mnuHistoria.Name = "ContextMenuStrip1"
        Me.mnuHistoria.Size = New System.Drawing.Size(194, 48)
        '
        'mnuHisCambiosHistoricos
        '
        Me.mnuHisCambiosHistoricos.Name = "mnuHisCambiosHistoricos"
        Me.mnuHisCambiosHistoricos.Size = New System.Drawing.Size(193, 22)
        Me.mnuHisCambiosHistoricos.Text = "Cambios de Históricos"
        '
        'mnuHisConsultaHistoricos
        '
        Me.mnuHisConsultaHistoricos.Name = "mnuHisConsultaHistoricos"
        Me.mnuHisConsultaHistoricos.Size = New System.Drawing.Size(193, 22)
        Me.mnuHisConsultaHistoricos.Text = "Consulta de Históricos"
        '
        'mnuReportes
        '
        Me.mnuReportes.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuRepSeleccion, Me.mnuRepPlazas, Me.mnuRepPlazaPersona, Me.ToolStripMenuItem1})
        Me.mnuReportes.Name = "mnuProcesos"
        Me.mnuReportes.Size = New System.Drawing.Size(190, 114)
        '
        'mnuRepSeleccion
        '
        Me.mnuRepSeleccion.Name = "mnuRepSeleccion"
        Me.mnuRepSeleccion.Size = New System.Drawing.Size(189, 22)
        Me.mnuRepSeleccion.Text = "Selección de Reportes"
        '
        'mnuRepPlazas
        '
        Me.mnuRepPlazas.Name = "mnuRepPlazas"
        Me.mnuRepPlazas.Size = New System.Drawing.Size(189, 22)
        Me.mnuRepPlazas.Text = "Reporte de Plazas"
        '
        'mnuRepPlazaPersona
        '
        Me.mnuRepPlazaPersona.Name = "mnuRepPlazaPersona"
        Me.mnuRepPlazaPersona.Size = New System.Drawing.Size(189, 22)
        Me.mnuRepPlazaPersona.Text = "Plaza por Persona"
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(189, 22)
        Me.ToolStripMenuItem1.Text = "Reporte General"
        '
        'picLogo
        '
        Me.picLogo.Location = New System.Drawing.Point(8, 96)
        Me.picLogo.Name = "picLogo"
        Me.picLogo.Size = New System.Drawing.Size(660, 140)
        Me.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picLogo.TabIndex = 7
        Me.picLogo.TabStop = False
        Me.picLogo.Visible = False
        '
        'cmdHistoria
        '
        Me.cmdHistoria.Enabled = False
        Me.cmdHistoria.Image = CType(resources.GetObject("cmdHistoria.Image"), System.Drawing.Image)
        Me.cmdHistoria.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.cmdHistoria.Location = New System.Drawing.Point(200, 7)
        Me.cmdHistoria.Name = "cmdHistoria"
        Me.cmdHistoria.Size = New System.Drawing.Size(85, 77)
        Me.cmdHistoria.TabIndex = 2
        Me.cmdHistoria.Text = "&Historia"
        Me.cmdHistoria.TextAlignment = System.Drawing.ContentAlignment.BottomCenter
        '
        'cmdEntidades
        '
        Me.cmdEntidades.Enabled = False
        Me.cmdEntidades.Image = CType(resources.GetObject("cmdEntidades.Image"), System.Drawing.Image)
        Me.cmdEntidades.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.cmdEntidades.Location = New System.Drawing.Point(296, 7)
        Me.cmdEntidades.Name = "cmdEntidades"
        Me.cmdEntidades.Size = New System.Drawing.Size(85, 77)
        Me.cmdEntidades.TabIndex = 3
        Me.cmdEntidades.Text = "&Entidades"
        Me.cmdEntidades.TextAlignment = System.Drawing.ContentAlignment.BottomCenter
        '
        'cmdCatalogos
        '
        Me.cmdCatalogos.Enabled = False
        Me.cmdCatalogos.Image = CType(resources.GetObject("cmdCatalogos.Image"), System.Drawing.Image)
        Me.cmdCatalogos.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.cmdCatalogos.Location = New System.Drawing.Point(8, 7)
        Me.cmdCatalogos.Name = "cmdCatalogos"
        Me.cmdCatalogos.Size = New System.Drawing.Size(85, 77)
        Me.cmdCatalogos.TabIndex = 0
        Me.cmdCatalogos.Text = "&Catálogos"
        Me.cmdCatalogos.TextAlignment = System.Drawing.ContentAlignment.BottomCenter
        '
        'cmdPlantillas
        '
        Me.cmdPlantillas.Enabled = False
        Me.cmdPlantillas.Image = CType(resources.GetObject("cmdPlantillas.Image"), System.Drawing.Image)
        Me.cmdPlantillas.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.cmdPlantillas.Location = New System.Drawing.Point(104, 7)
        Me.cmdPlantillas.Name = "cmdPlantillas"
        Me.cmdPlantillas.Size = New System.Drawing.Size(85, 77)
        Me.cmdPlantillas.TabIndex = 1
        Me.cmdPlantillas.Text = "&Plantillas"
        Me.cmdPlantillas.TextAlignment = System.Drawing.ContentAlignment.BottomCenter
        '
        'cmdReportes
        '
        Me.cmdReportes.Enabled = False
        Me.cmdReportes.Image = CType(resources.GetObject("cmdReportes.Image"), System.Drawing.Image)
        Me.cmdReportes.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.cmdReportes.Location = New System.Drawing.Point(392, 7)
        Me.cmdReportes.Name = "cmdReportes"
        Me.cmdReportes.Size = New System.Drawing.Size(85, 77)
        Me.cmdReportes.TabIndex = 4
        Me.cmdReportes.Text = "&Reportes"
        Me.cmdReportes.TextAlignment = System.Drawing.ContentAlignment.BottomCenter
        '
        'cmdSalir
        '
        Me.cmdSalir.Image = CType(resources.GetObject("cmdSalir.Image"), System.Drawing.Image)
        Me.cmdSalir.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.cmdSalir.Location = New System.Drawing.Point(583, 7)
        Me.cmdSalir.Name = "cmdSalir"
        Me.cmdSalir.Size = New System.Drawing.Size(85, 77)
        Me.cmdSalir.TabIndex = 6
        Me.cmdSalir.Text = "&Salir"
        Me.cmdSalir.TextAlignment = System.Drawing.ContentAlignment.BottomCenter
        '
        'cmdAnalisis
        '
        Me.cmdAnalisis.Image = CType(resources.GetObject("cmdAnalisis.Image"), System.Drawing.Image)
        Me.cmdAnalisis.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.cmdAnalisis.Location = New System.Drawing.Point(488, 7)
        Me.cmdAnalisis.Name = "cmdAnalisis"
        Me.cmdAnalisis.Size = New System.Drawing.Size(85, 77)
        Me.cmdAnalisis.TabIndex = 5
        Me.cmdAnalisis.Text = "&Analisis"
        Me.cmdAnalisis.TextAlignment = System.Drawing.ContentAlignment.BottomCenter
        '
        'frmPrincipal
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(677, 246)
        Me.Controls.Add(Me.picLogo)
        Me.Controls.Add(Me.cmdHistoria)
        Me.Controls.Add(Me.cmdEntidades)
        Me.Controls.Add(Me.cmdCatalogos)
        Me.Controls.Add(Me.cmdPlantillas)
        Me.Controls.Add(Me.cmdReportes)
        Me.Controls.Add(Me.cmdSalir)
        Me.Controls.Add(Me.cmdAnalisis)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "frmPrincipal"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Plantillas"
        Me.mnuPlantillas.ResumeLayout(False)
        Me.mnuCatalogos.ResumeLayout(False)
        Me.mnuHistoria.ResumeLayout(False)
        Me.mnuReportes.ResumeLayout(False)
        CType(Me.picLogo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmdHistoria, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmdEntidades, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmdCatalogos, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmdPlantillas, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmdReportes, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmdSalir, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmdAnalisis, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents cmdReportes As Telerik.WinControls.UI.RadButton
	Friend WithEvents cmdSalir As Telerik.WinControls.UI.RadButton
	Friend WithEvents mnuPlantillas As System.Windows.Forms.ContextMenuStrip
	Friend WithEvents cmdCatalogos As Telerik.WinControls.UI.RadButton
	Friend WithEvents mnuCatalogos As System.Windows.Forms.ContextMenuStrip
	Friend WithEvents cmdPlantillas As Telerik.WinControls.UI.RadButton
	Friend WithEvents cmdHistoria As Telerik.WinControls.UI.RadButton
	Friend WithEvents cmdEntidades As Telerik.WinControls.UI.RadButton
	Friend WithEvents mnuHistoria As System.Windows.Forms.ContextMenuStrip
	Friend WithEvents mnuReportes As System.Windows.Forms.ContextMenuStrip
	Friend WithEvents mnuCatMotivos As System.Windows.Forms.ToolStripMenuItem
	Friend WithEvents mnuCatCondiciones As System.Windows.Forms.ToolStripMenuItem
	Friend WithEvents mnuCatNiveles As System.Windows.Forms.ToolStripMenuItem
	Friend WithEvents mnuPlaCapturaPlazas As System.Windows.Forms.ToolStripMenuItem
	Friend WithEvents mnuPlaCapturaPlantillas As System.Windows.Forms.ToolStripMenuItem
	Friend WithEvents mnuPlaConsultaPlantillas As System.Windows.Forms.ToolStripMenuItem
	Friend WithEvents mnuPlaCargaReportes As System.Windows.Forms.ToolStripMenuItem
	Friend WithEvents mnuPlaConsultaSabaticos As System.Windows.Forms.ToolStripMenuItem
	Friend WithEvents mnuHisCambiosHistoricos As System.Windows.Forms.ToolStripMenuItem
	Friend WithEvents mnuHisConsultaHistoricos As System.Windows.Forms.ToolStripMenuItem
	Friend WithEvents mnuRepSeleccion As System.Windows.Forms.ToolStripMenuItem
	Friend WithEvents mnuRepPlazas As System.Windows.Forms.ToolStripMenuItem
	Friend WithEvents mnuRepPlazaPersona As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmdAnalisis As Telerik.WinControls.UI.RadButton
    Private WithEvents picLogo As System.Windows.Forms.PictureBox
    Friend WithEvents mnuPlaConsultaSuplencias As ToolStripMenuItem
    Friend WithEvents mnuMantenimientoPlaza As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem1 As ToolStripMenuItem
End Class

