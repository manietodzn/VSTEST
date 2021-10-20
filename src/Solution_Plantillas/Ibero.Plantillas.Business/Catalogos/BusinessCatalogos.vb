Imports Ibero.Plantillas.DataAcces
Imports Ibero.Plantillas.Entities

Public Class BusinessCatalogos

    Private dataAcces As New DataAccessCatalogos
    Public Sub ActualizaMotivos(sDescripcion As String, sUsuario As String, sMotivo As String)
        Try

            dataAcces.ActualizaMotivos(sDescripcion, sUsuario, sMotivo)

        Catch ex As IberoExcepcion
            Throw
        Catch ex As Exception
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & " Método:" & ex.StackTrace)
        End Try
    End Sub

    Public Sub ActualizaCondiciones(sDescripcion As String, sUsuario As String, sCondicion As String)
        Try

            dataAcces.ActualizaCondiciones(sDescripcion, sUsuario, sCondicion)

        Catch ex As IberoExcepcion
            Throw
        Catch ex As Exception
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & " Método:" & ex.StackTrace)
        End Try
    End Sub

    Public Sub ActualizaNivelesEstructura(sDescripcion As String, sUsuario As String, sConsecutivo As String, sNivel As String)
        Try

            dataAcces.ActualizaNivelesEstructura(sDescripcion, sUsuario, sConsecutivo, sNivel)

        Catch ex As IberoExcepcion
            Throw
        Catch ex As Exception
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & " Método:" & ex.StackTrace)
        End Try
    End Sub



    Public Sub InsertaMotivos(sDescripcion As String, sUsuario As String, sMotivo As String)
        Try

            dataAcces.InsertaMotivos(sDescripcion, sUsuario, sMotivo)

        Catch ex As IberoExcepcion
            Throw
        Catch ex As Exception
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & " Método:" & ex.StackTrace)
        End Try
    End Sub

    Public Sub InsertaCondiciones(sDescripcion As String, sUsuario As String, sCondicion As String)
        Try

            dataAcces.InsertaCondiciones(sDescripcion, sUsuario, sCondicion)

        Catch ex As IberoExcepcion
            Throw
        Catch ex As Exception
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & " Método:" & ex.StackTrace)
        End Try
    End Sub

    Public Sub InsertaNivelesEstructura(sDescripcion As String, sUsuario As String, sConsecutivo As String, sNivel As String)
        Try

            dataAcces.InsertaNivelesEstructura(sDescripcion, sUsuario, sConsecutivo, sNivel)

        Catch ex As IberoExcepcion
            Throw
        Catch ex As Exception
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & " Método:" & ex.StackTrace)
        End Try
    End Sub



    Public Sub EliminaMotivos(sIdentificador As String)
        Try

            dataAcces.EliminaMotivos(sIdentificador)

        Catch ex As IberoExcepcion
            Throw
        Catch ex As Exception
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & " Método:" & ex.StackTrace)
        End Try
    End Sub

    Public Sub EliminaCondiciones(sIdentificador As String)
        Try

            dataAcces.EliminaCondiciones(sIdentificador)

        Catch ex As IberoExcepcion
            Throw
        Catch ex As Exception
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & " Método:" & ex.StackTrace)
        End Try
    End Sub

    Public Function EliminaNiveles(sIdentificador As String) As Integer
        Try

            Return dataAcces.EliminaNiveles(sIdentificador)

        Catch ex As IberoExcepcion
            Throw
        Catch ex As Exception
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & " Método:" & ex.StackTrace)
        End Try
    End Function



    Public Function ObtieneMotivos() As List(Of String)
        Try

            Return dataAcces.ObtieneMotivos()

        Catch ex As IberoExcepcion
            Throw
        Catch ex As Exception
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & " Método:" & ex.StackTrace)
        End Try
    End Function

    Public Function ObtieneNiveles() As List(Of String)
        Try

            Return dataAcces.ObtieneNiveles()

        Catch ex As IberoExcepcion
            Throw
        Catch ex As Exception
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & " Método:" & ex.StackTrace)
        End Try
    End Function

    Public Function ObtieneCondiciones() As List(Of String)
        Try

            Return dataAcces.ObtieneCondiciones()

        Catch ex As IberoExcepcion
            Throw
        Catch ex As Exception
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & " Método:" & ex.StackTrace)
        End Try
    End Function



    Public Function CargarDatosTabulador(sTabulador As String) As DataTable

        Try

            Return dataAcces.CargarDatosTabulador(sTabulador)

        Catch ex As IberoExcepcion
            Throw
        Catch ex As Exception
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & " Método:" & ex.StackTrace)
        End Try

    End Function


    Public Function CargaTiposNomina() As DataTable
        Try

            Return dataAcces.CargaTiposNomina()

        Catch ex As IberoExcepcion
            Throw
        Catch ex As Exception
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & " Método:" & ex.StackTrace)
        End Try
    End Function


    Public Function CargaCondicionPlantillas() As DataTable
        Try

            Return dataAcces.CargaCondicionPlantillas()

        Catch ex As IberoExcepcion
            Throw
        Catch ex As Exception
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & " Método:" & ex.StackTrace)
        End Try
    End Function
End Class
