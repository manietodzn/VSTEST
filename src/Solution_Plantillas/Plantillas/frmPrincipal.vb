Option Strict Off

Imports LibreriaUIA.Funciones
Imports System.Data.SqlClient
Imports System.Data
Imports FrameWork
Imports Ibero.Plantillas.Entities

Public Class frmPrincipal
    Private blnbtnSalir As Boolean = False
    Private NOpcMenu As String

    Private Sub frmPrincipal_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose()
    End Sub

    Private Sub frmPrincipal_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If Not blnbtnSalir Then Cerrar(e.Cancel)
    End Sub

    Private Sub frmPrincipal_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim objDat As Dat = Nothing
        Dim blnContinuar As Boolean = True
        Dim blnSalir As Boolean = False
        Dim objLogin As New Formularios.FormaLogin

        Try
            Me.Location = New Point(Me.Left, 50)
            Me.Show()

            Inicializar()

            objLogin.TipoLogin = LibreriaUIA.Formularios.FormaLogin.eTipoLogin.Empresarial

            objLogin.ConexionPrincipal = strConexionPrincipal
            objLogin.NombrePrograma = "PLANTILL"
            objLogin.Login = My.Settings.Login.Trim
            objLogin.Empresa = My.Settings.Empresa.Trim
            If objLogin.MostrarLogin(Me) = Windows.Forms.DialogResult.OK Then

                ObtenerCadenaConexion()

                strConexion = objLogin.ConexionUsuario
                objConexion = objLogin.ObjetoConexion

                UsuarioConexion.strConexion = strConexion

                strUSER = objLogin.Login
                strUserApp = objLogin.Login

                'ABBR 10/02/2017. Ruta PathsHome.
                strConexionUsuario = strConexion

                My.Settings.Login = objLogin.Login
                My.Settings.Save()

                TodosPermisos = objLogin.TodosPermisos
                PrmsEmpresa = objLogin.PrmsEmpresa
                PrmsRegional = objLogin.PrmsRegional
                PrmsNomina = objLogin.PrmsNomina
                PrmsDepto = objLogin.PrmsDepto
                PrmsTipNomina = objLogin.PrmsTipNomina
                nPrmsEmpresa = objLogin.nPrmsEmpresa
                nPrmsRegional = objLogin.nPrmsRegional
                nPrmsNomina = objLogin.nPrmsNomina
                nPrmsDepto = objLogin.nPrmsDepto
                nPrmsTipNomina = objLogin.nPrmsTipNomina
                QueEmpresa = objLogin.QueEmpresa
                QueTipoNomina = objLogin.QueTipoNomina
                PrmsClvIndent = objLogin.PrmsClvIndent

                Try
                    objDat = New Dat(strConexion)
                    blnContinuar = True
                Catch exSql As SqlException
                    blnContinuar = False
                    MsgBox("Error al conectarse al servidor: " & objConexion.Server & "." & objConexion.DataBase & vbCrLf & vbCrLf & exSql.Message, MsgBoxStyle.Critical, "Iniciando Aplicación")
                Catch ex1 As Exception
                    blnContinuar = False
                    MessageBox.Show(ex1.ToString(), My.Application.Info.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try

                Try
                    objDat.Dispose()
                Catch ex2 As Exception
                End Try

                If blnContinuar Then

                    'ABBR 10/02/2017. Ruta PathsHome.
                    Dim trdPaths As New Threading.Thread(AddressOf PathsHome)
                    trdPaths.IsBackground = True
                    trdPaths.Start()

                    Select Case ObtieneEmpresa()
                        Case "SIP2000"
                            NombreEmpresaTitulo = "Universidad Iberoamericana"
                            enuTipoEmpresa = eTipoEmpresa.Universidad
                            Me.picLogo.Image = My.Resources.Ibero
                            enuEmpresa = eEmpresa.Universidad       'ABBR 25/08/2016... Empate del modulo

                        Case "PrepaIbero"
                            NombreEmpresaTitulo = "Prepa Ibero"
                            enuTipoEmpresa = eTipoEmpresa.PrepaIbero
                            Me.picLogo.Image = My.Resources.PrepaIbero
                            enuEmpresa = eEmpresa.Otras             'ABBR 25/08/2016... Empate del modulo

                        Case "RadioIbero"
                            NombreEmpresaTitulo = "Radio Ibero"
                            enuTipoEmpresa = eTipoEmpresa.RadioIbero
                            Me.picLogo.Image = My.Resources.RadioIbero
                            enuEmpresa = eEmpresa.Otras             'ABBR 25/08/2016... Empate del modulo

                        Case "CasaMeneses"
                            NombreEmpresaTitulo = "Casa Meneses"
                            enuTipoEmpresa = eTipoEmpresa.CasaMeneses
                            Me.picLogo.Image = My.Resources.CasaMeneses
                            enuEmpresa = eEmpresa.Otras             'ABBR 25/08/2016... Empate del modulo

                        Case "IberoTijuana"
                            NombreEmpresaTitulo = "Ibero Tijuana"
                            enuTipoEmpresa = eTipoEmpresa.IberoTijuana
                            Me.picLogo.Image = My.Resources.IberoTijuana
                            enuEmpresa = eEmpresa.Otras             'ABBR 25/08/2016... Empate del modulo
                        Case "ValleChalco"
                            NombreEmpresaTitulo = "Ibero Chalco"
                            enuTipoEmpresa = eTipoEmpresa.IberoChalco
                            Me.picLogo.Image = My.Resources.IberoChalco
                            enuEmpresa = eEmpresa.Otras

                    End Select


                    Me.picLogo.Visible = True
                    Me.Text = Me.Text & " - " & NombreEmpresaTitulo & " - Versión: " & Application.ProductVersion.ToString

                    TranscribeInicia()
                    InitDiasMes()

                    Fecpro = DateString
                    FechaProceso = Mid(Fecpro, 7, 4) & Mid(Fecpro, 1, 2) & Mid(Fecpro, 4, 2)
                    AnioProceso = Val(Mid(FechaProceso, 1, 4))

                    ChecaPermisosMenus()
                Else
                    blnSalir = True
                End If
            Else
                blnSalir = True
            End If

            objLogin.Dispose()

            If blnSalir Then
                blnbtnSalir = True
                Me.Close()
            End If
        Catch ex As Exception
            DesplegarError(ErrorLog.eIterfazArchivo, ex, objEnsamblado, strUserApp, Me.GetType().Name, "", "", "", "", strArchivoLogErr, False)
        End Try
    End Sub

    'ABBR 10/02/2017. Ruta PathsHome.
    Private Sub PathsHome()
        Dim objPathsHome As New PathsHome(0)

        Try
            objPathsHome = ObtenerPathsHome(strConexionUsuario, 1, strUserApp)

            If objPathsHome.SalidasExiste Then
                Formas = objPathsHome.Salidas
                QueriesDir = objPathsHome.Salidas
            End If

            If objPathsHome.ReportesExiste Then
                strReportsPath = objPathsHome.Reportes
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString(), My.Application.Info.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub cmdCatalogos_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCatalogos.Click
        Me.mnuCatalogos.Show(cmdCatalogos, New Point(0, cmdCatalogos.Bottom))
    End Sub

    Private Sub cmdPlantillas_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPlantillas.Click
        Me.mnuPlantillas.Show(cmdPlantillas, New Point(0, cmdPlantillas.Bottom))
    End Sub

    Private Sub cmdHistoria_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdHistoria.Click
        Me.mnuHistoria.Show(cmdHistoria, New Point(0, cmdHistoria.Bottom))
    End Sub

    Private Sub cmdEntidades_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEntidades.Click
        frmEntidades.ShowDialog(Me)
    End Sub

    Private Sub cmdReportes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdReportes.Click
        Me.mnuReportes.Show(cmdReportes, New Point(0, cmdReportes.Bottom))
    End Sub

    Private Sub cmdAnalisis_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAnalisis.Click
        Try
            frmAnalisisPlantillas.Show(Me)
        Catch ex As Exception
            frmAnalisisPlantillas.Activate()
        End Try
    End Sub

    Private Sub cmdSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSalir.Click
        blnbtnSalir = True
        Cerrar()
    End Sub

    Public Sub Cerrar(Optional ByRef Cancelar As Boolean = False)
        Dim ds As DialogResult

        ds = Telerik.WinControls.RadMessageBox.Show(Me, "¿Desea salir del Sistema?", "Salir del Sistema", MessageBoxButtons.YesNo, Telerik.WinControls.RadMessageIcon.Question, Telerik.WinControls.RadMessageIcon.Info, Windows.Forms.RightToLeft.Inherit)

        If ds = Windows.Forms.DialogResult.Yes Then
            If blnbtnSalir Then Me.Close()
        Else
            blnbtnSalir = False
            Cancelar = True
        End If
    End Sub

    Private Sub ChecaPermisosMenus()
        Dim V1, V2, F, I, sm As Integer
        Dim V3 As String
        Dim strSQL As String
        Dim objDat As New Dat(strConexion)
        Dim Parametros As New Collection
        Dim datReader As SqlDataReader

        Try
            strSQL = "SELECT menu, sub_menu, opciones  FROM usuarios_prg_mnu_o WHERE usuario = '" & strUSER & "' AND modulo = 'PLANTILL' AND sub_menu = 1"
            datReader = objDat.EjecutarComandoReader(strSQL, Parametros)
            If datReader.HasRows Then
                Do While datReader.Read
                    V1 = datReader.Item("menu")
                    V2 = datReader.Item("sub_menu")

                    NOpcMenu = datReader.Item("opciones")
                    F = Len(NOpcMenu)

                    For I = 10 To F Step 10
                        V3 = Mid(NOpcMenu, I, 1)
                        If V3 = "S" Then
                            Select Case I
                                Case 10
                                    cmdCatalogos.Enabled = True
                                    If mnuCatalogos.Items.Count > 0 Then
                                        For sm = (I + 1) To (I + 4)
                                            V3 = Mid(NOpcMenu, sm, 1)
                                            Try
                                                If V3 = "S" Then mnuCatalogos.Items(sm - 11).Enabled = True Else mnuCatalogos.Items(sm - 11).Enabled = False
                                            Catch ex1 As Exception
                                            End Try
                                        Next sm
                                    End If
                                Case 20
                                    cmdPlantillas.Enabled = True
                                    If mnuPlantillas.Items.Count > 0 Then
                                        For sm = (I + 1) To (I + 4)
                                            V3 = Mid(NOpcMenu, sm, 1)
                                            Try
                                                If V3 = "S" Then mnuPlantillas.Items(sm - 21).Enabled = True Else mnuPlantillas.Items(sm - 21).Enabled = False
                                            Catch ex1 As Exception
                                            End Try
                                        Next sm
                                    End If
                                Case 30
                                    cmdHistoria.Enabled = True
                                    If mnuHistoria.Items.Count > 0 Then
                                        For sm = (I + 1) To (I + 2)
                                            V3 = Mid(NOpcMenu, sm, 1)
                                            Try
                                                If V3 = "S" Then mnuHistoria.Items(sm - 31).Enabled = True Else mnuHistoria.Items(sm - 31).Enabled = False
                                            Catch ex1 As Exception
                                            End Try
                                        Next sm
                                    End If
                                Case 40
                                    cmdEntidades.Enabled = True
                                Case 50
                                    cmdReportes.Enabled = True
                            End Select
                        End If
                    Next I
                Loop
            End If
            datReader.Close()
        Catch ex As Exception
            DesplegarError(ErrorLog.eIterfazArchivo, ex, objEnsamblado, strUserApp, Me.GetType().Name, "", "", "", "", strArchivoLogErr, False)
        Finally
            objDat.Dispose()
        End Try
    End Sub

    Private Sub mnuCatMotivos_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuCatMotivos.Click
        frmCatalogos.TipoCatalogo = frmCatalogos.eTipoCatalogo.Motivos
        frmCatalogos.ShowDialog(Me)
    End Sub

    Private Sub mnuCatCondiciones_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuCatCondiciones.Click
        frmCatalogos.TipoCatalogo = frmCatalogos.eTipoCatalogo.Condiciones
        frmCatalogos.ShowDialog(Me)
    End Sub

    Private Sub mnuCatNiveles_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuCatNiveles.Click
        frmCatalogos.TipoCatalogo = frmCatalogos.eTipoCatalogo.Niveles
        frmCatalogos.ShowDialog(Me)
    End Sub

    Private Sub mnuPlaCapturaPlazas_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPlaCapturaPlazas.Click
        Try
            frmCapturaPlazas.Show(Me)
        Catch ex As Exception
            frmCapturaPlazas.Activate()
        End Try
    End Sub

    Private Sub mnuPlaCapturaPlantillas_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPlaCapturaPlantillas.Click
        Try
            frmCapturaPlantillas.Show(Me)
        Catch ex As Exception
            frmCapturaPlantillas.Activate()
        End Try
    End Sub

    Private Sub mnuPlaConsultaPlantillas_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPlaConsultaPlantillas.Click
        Try
            frmConsultaPlantillas.Show(Me)
        Catch ex As Exception
            frmConsultaPlantillas.Activate()
        End Try
    End Sub

    Private Sub mnuPlaCargaReportes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPlaCargaReportes.Click
        Try
            frmCargaReportes.Show(Me)
        Catch ex As Exception
            frmCargaReportes.Activate()
        End Try
    End Sub

    Private Sub mnuPlaConsultaSabaticos_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPlaConsultaSabaticos.Click
        Try
            frmConsultaSabaticos.Show(Me)
        Catch ex As Exception
            frmConsultaSabaticos.Activate()
        End Try
    End Sub

    Private Sub mnuHisCambiosHistoricos_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuHisCambiosHistoricos.Click
        frmCambiosHistoricos.ShowDialog(Me)
    End Sub

    Private Sub mnuHisConsultaHistoricos_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuHisConsultaHistoricos.Click
        frmConsultaHistoricos.ShowDialog(Me)
    End Sub

    Private Sub mnuRepSeleccion_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuRepSeleccion.Click
        frmReporteSeleccion.ShowDialog(Me)
    End Sub

    Private Sub mnuRepPlazas_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuRepPlazas.Click
        Dim FileName As String = ""
        Dim FileCRW As String = ""

        Try
            Me.Cursor = Cursors.WaitCursor

            FileCRW = "plazavac"

            'CDIAZ 08/FEB/2013
            'SE COMENTA PARTE DE ESTE CODIFGO TEMPORALMENTE
            'PARA QUE SE MANDEN A LLAMAR LOR REPORTES FISICOS
            'If LibreriaUIA.Formularios.FormaImprimir.CargaReportCRW(FileCRW, QueriesDir, FileName) Then
            '	LibreriaUIA.Formularios.FormaImprimir.ImprimeDocto("Reporte de Plazas", strReportsPath, FileName)
            'End If
            '*****

            'CDIAZ 08/FEB/2013
            'ESTO ES TEMPORAL, SE QUITA Y SE HABILITA EL DE ARRIBA
            Dim strPathTemp As String
            If strReportsPath.EndsWith("\") Then
                strPathTemp = strReportsPath & "Varios\"
            Else
                strPathTemp = strReportsPath & "\Varios\"
            End If
            FileName = "plazavac.RPT"
            LibreriaUIA.Formularios.FormaImprimir.ImprimeDocto("Reporte de Plazas", strPathTemp, FileName)
            '*****
        Catch ex As Exception
            DesplegarError(ErrorLog.eIterfazArchivo, ex, objEnsamblado, strUserApp, Me.GetType().Name, "", "", "", "", strArchivoLogErr, False)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub mnuRepPlazaPersona_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuRepPlazaPersona.Click
        frmReportePlazaPersona.ShowDialog(Me)
    End Sub

    Private Sub mnuPlaConsultaSuplencias_Click(sender As Object, e As EventArgs) Handles mnuPlaConsultaSuplencias.Click
        Dim suplencias As New frmConsultaSuplencias
        suplencias.ShowDialog(Me)
    End Sub

    Private Sub mnuMantenimientoPlaza_Click(sender As Object, e As EventArgs) Handles mnuMantenimientoPlaza.Click
        'ABBR 04/04/2017. Impresion Solicitud
        Try
            FrmManteSolicitudPlaza.Show(Me)
        Catch ex As Exception
            FrmManteSolicitudPlaza.Activate()
        End Try
    End Sub

    Private Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem1.Click
        frmReporteGeneral.Show(Me)
    End Sub
End Class
