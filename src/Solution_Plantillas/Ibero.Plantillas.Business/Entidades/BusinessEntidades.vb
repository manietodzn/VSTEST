Imports Ibero.Plantillas.DataAcces
Imports Ibero.Plantillas.Entities


Public Class BusinessEntidades

    Private dataAcces As New DataAccessEntidades
    Public Function ValidaEmpleado(sNomina As String) As List(Of String)
        Try

            Return dataAcces.ValidaEmpleado(sNomina)

        Catch ex As IberoExcepcion
            Throw
        Catch ex As Exception
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & " Método:" & ex.StackTrace)
        End Try
    End Function

    Public Function ObtieneNivelEstructura(sLiReporta As String) As String
        Try

            Return dataAcces.ObtieneNivelEstructura(sLiReporta)

        Catch ex As IberoExcepcion
            Throw
        Catch ex As Exception
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & " Método:" & ex.StackTrace)
        End Try
    End Function

    Public Sub ActualizaEntidades(request As Entidad_Request)

        Try

            dataAcces.ActualizaEntidades(request.Entidad, request.Nombre, request.Estructura, request.NominaResponsable, request.Contable, request.Departamento, request.CentroCosto, request.DepartamentoSCE, request.CoordinacionSCE, request.Reporta, request.Interaccion _
                                         , request.Estatus, request.Edificio, request.Piso, request.Siglas, request.FechaCreacion, request.FechaCancelacion, request.ClaveEntidad, request.Usuario, request.NominaResponsableOrg, request.FolioEntidad)

        Catch ex As IberoExcepcion
            Throw
        Catch ex As Exception
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & " Método:" & ex.StackTrace)
        End Try

    End Sub

    Public Sub InsertaEntidades(request As Entidad_Request)

        Try

            dataAcces.InsertaEntidades(request.Entidad, request.Nombre, request.Estructura, request.NominaResponsable, request.Contable, request.Departamento, request.CentroCosto, request.DepartamentoSCE, request.CoordinacionSCE, request.Reporta, request.Interaccion _
                                         , request.Estatus, request.Edificio, request.Piso, request.Siglas, request.FechaCreacion, request.FechaCancelacion, request.ClaveEntidad, request.Usuario, request.NominaResponsableOrg, request.FolioEntidad)

        Catch ex As IberoExcepcion
            Throw
        Catch ex As Exception
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & " Método:" & ex.StackTrace)
        End Try

    End Sub

    Public Function ObtieneMaximoFolioEntidad() As Integer
        Try

            Return dataAcces.ObtieneMaximoFolioEntidad()

        Catch ex As IberoExcepcion
            Throw
        Catch ex As Exception
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & " Método:" & ex.StackTrace)
        End Try
    End Function

    Public Function ObtieneNumeroFolioEntidad(sFolioEntidad As String) As Integer
        Try

            Return dataAcces.ObtieneNumeroFolioEntidad(sFolioEntidad)

        Catch ex As IberoExcepcion
            Throw
        Catch ex As Exception
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & " Método:" & ex.StackTrace)
        End Try
    End Function

    Public Function ObtieneEntidad(sFolioEntidad As String) As List(Of Entidad_Response)

        Dim entidad As New Entidad_Response
        Dim entidadesList As New List(Of Entidad_Response)
        Dim dt As DataTable

        Try

            dt = dataAcces.ObtieneEntidad(sFolioEntidad)


            For Each row As DataRow In dt.Rows

                entidad = New Entidad_Response
                entidad.Entidad = row.Item("entidad").ToString
                entidad.Nombre = row.Item("nombre_entidad").ToString
                entidad.Estructura = row.Item("nivel_estructura").ToString
                entidad.NominaResponsable = row.Item("nomina_responsable").ToString
                entidad.NominaResponsableOrg = row.Item("responsable_organigrama").ToString
                entidad.Contable = row.Item("contable").ToString
                entidad.Departamento = row.Item("depto").ToString
                entidad.CentroCosto = row.Item("centro_costo").ToString
                entidad.DepartamentoSCE = row.Item("depto_sce").ToString
                entidad.CoordinacionSCE = row.Item("coordinacion_sce").ToString
                entidad.Reporta = row.Item("reporta").ToString
                entidad.Interaccion = row.Item("interaccion").ToString
                entidad.Estatus = row.Item("status").ToString
                entidad.Edificio = row.Item("edificio").ToString
                entidad.Piso = row.Item("piso").ToString
                entidad.Siglas = row.Item("siglas").ToString
                entidad.FechaCreacion = row.Item("FCreacion").ToString
                entidad.FechaCancelacion = row.Item("FCancelacion").ToString

                entidadesList.Add(entidad)

            Next row


        Catch ex As IberoExcepcion
            Throw
        Catch ex As Exception
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & " Método:" & ex.StackTrace)
        End Try

        Return entidadesList

    End Function

    Public Function ObtieneEmpleado(sNomina As String) As Empleado_Response

        Dim empleado As New Empleado_Response
        Dim dt As DataTable

        Try

            dt = dataAcces.ObtieneEntidad(sNomina)


            For Each row As DataRow In dt.Rows

                empleado = New Empleado_Response
                empleado.ApellidoPaterno = row.Item("apellido_paterno").ToString
                empleado.ApellidoMaterno = row.Item("apellido_materno").ToString
                empleado.Nombre = row.Item("nombre").ToString

            Next row


        Catch ex As IberoExcepcion
            Throw
        Catch ex As Exception
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & " Método:" & ex.StackTrace)
        End Try

        Return empleado
    End Function

    Public Function ObtieneNumeroEntidades(sFolioEntidad As String, sEntidad As String) As Integer
        Try

            Return dataAcces.ObtieneNumeroEntidades(sFolioEntidad, sEntidad)

        Catch ex As IberoExcepcion
            Throw
        Catch ex As Exception
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & " Método:" & ex.StackTrace)
        End Try

    End Function

    Public Function ObtieneNumeroEstructura(sDepartamento As String) As Integer
        Try

            Return dataAcces.ObtieneNumeroEstructura(sDepartamento)

        Catch ex As IberoExcepcion
            Throw
        Catch ex As Exception
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & " Método:" & ex.StackTrace)
        End Try
    End Function

    Public Function ObtieneNumeroPlantillas(sFolioEntidad As String) As Integer
        Try

            Return dataAcces.ObtieneNumeroPlantillas(sFolioEntidad)

        Catch ex As IberoExcepcion
            Throw
        Catch ex As Exception
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & " Método:" & ex.StackTrace)
        End Try
    End Function

    Public Function ObtieneNumeroHistoricoPlantillas(sFolioEntidad As String) As Integer
        Try

            Return dataAcces.ObtieneNumeroHistoricoPlantillas(sFolioEntidad)

        Catch ex As IberoExcepcion
            Throw
        Catch ex As Exception
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & " Método:" & ex.StackTrace)
        End Try
    End Function

    Public Sub EliminaEntidad(sFolioEntidad As String)
        Try

            dataAcces.EliminaEntidad(sFolioEntidad)

        Catch ex As IberoExcepcion
            Throw
        Catch ex As Exception
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & " Método:" & ex.StackTrace)
        End Try
    End Sub

    Public Function ObtieneNivelesEstructura() As List(Of String)
        Try

            Return dataAcces.ObtieneNivelesEstructura()

        Catch ex As IberoExcepcion
            Throw
        Catch ex As Exception
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & " Método:" & ex.StackTrace)
        End Try
    End Function

    Public Function ObtieneEdificios() As List(Of String)
        Try

            Return dataAcces.ObtieneEdificios()

        Catch ex As IberoExcepcion
            Throw
        Catch ex As Exception
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & " Método:" & ex.StackTrace)
        End Try
    End Function
	
	    Public Sub EliminaEntidad(sFolioEntidad As String, idFolio as integer)
        Try
			if(idFolio<>0)
				dataAcces.EliminaEntidad(sFolioEntidad)

        Catch ex As IberoExcepcion
            Throw
        Catch ex As Exception
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & " Método:" & ex.StackTrace)
        End Try
    End Sub


End Class
