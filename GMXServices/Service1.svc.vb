' NOTE: You can use the "Rename" command on the context menu to change the class name "GMXServices" in code, svc and config file together.
' NOTE: In order to launch WCF Test Client for testing this service, please select GMXServices.svc or GMXServices.svc.vb at the Solution Explorer and start debugging.
Imports System.Net
Imports System.Net.Mail
Public Class GMXServices
    Implements Generales

    Public db As New GMXEntities
    Public Sub New()
    End Sub

#Region "Ordenes de Pago"

#Region "Polizas"
    Public Function ObtieneAclaraciones(id_pv As Integer) As String Implements Generales.ObtieneAclaraciones
        Dim Resultado As IList = Nothing
        Dim StrResultado As String = ""
        Dim Funcs As New FuncionesConversion
        Try
            Resultado = db.spS_Aclaracion(id_pv).ToList
            For Each Item In Resultado
                StrResultado = Funcs.RempCarEsp(Funcs.ConvertRtf2Html(Replace(Item.Descripcion.ToString(), vbCrLf, "")))
                StrResultado = Replace(Replace(StrResultado, vbCrLf, ""), vbTab, "")
            Next
        Catch ex As Exception
            Return String.Empty
        End Try
        Return StrResultado
    End Function

#End Region

#End Region

#Region "Utilidades"
#Region "EnvioCorreos"
    Public Function EnviaCorreo(strTo As String, strCc As String, strBody As String, strSubject As String, Optional strBco As String = vbNullString) As Boolean Implements Generales.EnviaCorreo
        Dim cm = ConfigurationManager.AppSettings
        Dim Mensaje As New MailMessage
        Try
            Mensaje.To.Add(strTo)
            If strCc <> vbNullString Then
                Mensaje.CC.Add(strCc)
            End If
            If strBco <> vbNullString Then
                Mensaje.Bcc.Add(strBco)
            End If

            Mensaje.From = New MailAddress(cm("SMTPFromAddress"), cm("SMTPFrom"), Encoding.UTF8)
            Mensaje.Subject = strSubject
            Mensaje.IsBodyHtml = True
            Mensaje.Body = strBody
            Mensaje.BodyEncoding = System.Text.Encoding.UTF8
            Mensaje.Priority = MailPriority.Normal
            Dim cli As New SmtpClient
            cli.Port = cm("SMTPPort")
            cli.Host = cm("SMTPServer")
            cli.Credentials = New System.Net.NetworkCredential(cm("SMTPUsu"), cm("SMTPPass"))
            cli.EnableSsl = False

            cli.Send(Mensaje)
            Return True
        Catch ex As Exception
            MsgBox(ex.Message, vbExclamation, "error")
            Return False
        End Try

    End Function
#End Region

#End Region

#Region "Firma Electronica"
    Public Function ActualizaFirma(NumOp As String, TipoPer As Integer, CodUsu As String) As Integer Implements Generales.ActualizaFirma
        Dim Resultado As Integer
        Try
            Resultado = db.spU_ActualizaFirmas(NumOp, TipoPer, CodUsu)
        Catch ex As Exception
            Return Nothing
        End Try
        Return Resultado
    End Function

    Public Function ObtieneUsuarioFirmaE(TipoUsuario As Integer) As List(Of spS_UsuarioFirma_Result1) Implements Generales.ObtieneUsuarioFirmaE
        Dim Resultado As IList = Nothing
        Try
            Resultado = db.spS_UsuarioFirma(TipoUsuario).ToList
        Catch ex As Exception
            Return Nothing
        End Try
        Return Resultado
    End Function

    Public Function ObtienePermisosXUsu(CodUsu As String) As List(Of spS_PermisosxUSuFirma_Result) Implements Generales.ObtienePermisosXUsu
        Dim Resultado As IList = Nothing
        Try
            Resultado = db.spS_PermisosxUSuFirma(CodUsu).ToList
        Catch ex As Exception
            Return Nothing
        End Try
        Return Resultado
    End Function


#End Region



End Class
