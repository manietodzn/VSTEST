Option Strict Off

Imports System.Data
Imports System.Data.SqlClient
Imports FrameWork
Imports LibreriaUIA.Funciones

Module Funciones

	Sub InitDiasMes()
		Diasmes(1) = 31
		Diasmes(2) = 28
		Diasmes(3) = 31
		Diasmes(4) = 30
		Diasmes(5) = 31
		Diasmes(6) = 30
		Diasmes(7) = 31
		Diasmes(8) = 31
		Diasmes(9) = 30
		Diasmes(10) = 31
		Diasmes(11) = 30
		Diasmes(12) = 31

		TextoMes(1) = "Enero"
		TextoMes(2) = "Febrero"
		TextoMes(3) = "Marzo"
		TextoMes(4) = "Abril"
		TextoMes(5) = "Mayo"
		TextoMes(6) = "Junio"
		TextoMes(7) = "Julio"
		TextoMes(8) = "Agosto"
		TextoMes(9) = "Septiembre"
		TextoMes(10) = "Octubre"
		TextoMes(11) = "Noviembre"
		TextoMes(12) = "Diciembre"
	End Sub

	Sub TranscribeInicia()
		Unidades(1) = " UN"
		Unidades(2) = " DOS"
		Unidades(3) = " TRES"
		Unidades(4) = " CUATRO"
		Unidades(5) = " CINCO"
		Unidades(6) = " SEIS"
		Unidades(7) = " SIETE"
		Unidades(8) = " OCHO"
		Unidades(9) = " NUEVE"

		Decenas(1) = " DIEZ"
		Decenas(2) = " VEINTE"
		Decenas(3) = " TREINTA"
		Decenas(4) = " CUARENTA"
		Decenas(5) = " CINCUENTA"
		Decenas(6) = " SESENTA"
		Decenas(7) = " SETENTA"
		Decenas(8) = " OCHENTA"
		Decenas(9) = " NOVENTA"

		Centenas(1) = " CIENTO"
		Centenas(2) = " DOSCIENTOS"
		Centenas(3) = " TRESCIENTOS"
		Centenas(4) = " CUATROCIENTOS"
		Centenas(5) = " QUINIENTOS"
		Centenas(6) = " SEISCIENTOS"
		Centenas(7) = " SETECIENTOS"
		Centenas(8) = " OCHOCIENTOS"
		Centenas(9) = " NOVECIENTOS"

		Separadores(1) = " CIEN"
		Separadores(2) = " MIL"
		Separadores(3) = " MILLON"
        Separadores(4) = " BILLON"
        Separadores(5) = " TRILLON"

        DiezUnit(1) = " ONCE"
		DiezUnit(2) = " DOCE"
		DiezUnit(3) = " TRECE"
		DiezUnit(4) = " CATORCE"
		DiezUnit(5) = " QUINCE"
        DiezUnit(6) = " DIECISEIS"
        DiezUnit(7) = " DIECISIETE"
    End Sub

	Public Sub Inicializar()
		Dim strErrNombre As String = "Err_" + LibreriaUIA.ArchivosDirectorios.InfoArchivo.NombreSinExtension(objEnsamblado.Location) + "_" + Environment.MachineName.Trim().ToUpper() + "." + Environment.UserName.Trim().ToUpper() + ".txt"

		Try
			If My.Computer.Screen.Bounds.Width < 1024 And My.Computer.Screen.Bounds.Height < 768 Then
				MessageBox.Show("La resolución de la pantalla no es la adecuada." & vbCrLf & "Como minímo necesita 1024x768.", "Inicio", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				End
			End If

			VersionCLR = Environment.Version.ToString()
			NombreProceso = My.Application.Info.AssemblyName
			IDProceso = objProceso.Id
			InfoEnsamblados = True

			strArchivoLogErr = strRutaApp + strErrNombre
		Catch ex As Exception
			MessageBox.Show(ex.ToString(), My.Application.Info.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error)
		End Try
	End Sub

End Module
