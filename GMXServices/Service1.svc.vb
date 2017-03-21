' NOTE: You can use the "Rename" command on the context menu to change the class name "GMXServices" in code, svc and config file together.
' NOTE: In order to launch WCF Test Client for testing this service, please select GMXServices.svc or GMXServices.svc.vb at the Solution Explorer and start debugging.
Imports System.Net
Imports System.Net.Mail
Public Class GMXServices
    Implements Generales

    Public db As New GMXEntities
    Public Sub New()
    End Sub

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

#Region "Usuarios"
    Public Function ObtieneUsuarioFirmaE(TipoUsuario As Integer) As List(Of spS_UsuarioFirma_Result) Implements Generales.ObtieneUsuarioFirmaE
        Dim Resultado As IList = Nothing
        Try
            Resultado = db.spS_UsuarioFirma(TipoUsuario).ToList
        Catch ex As Exception
            Return Nothing
        End Try
        Return Resultado
    End Function
#End Region

#Region "EnvioCorreos"
    Public Function EnviaCorreo() As Boolean Implements Generales.EnviaCorreo
        Dim Mensaje As New MailMessage
        Try
            Mensaje.To.Add("oscar.sandoval@GMX.COM.MX")
            Mensaje.CC.Add("martinem@gmx.com.mx")

            Mensaje.From = New MailAddress(ConfigurationManager.AppSettings("SMTPFromAddress"), ConfigurationManager.AppSettings("SMTPFrom"), Encoding.UTF8)
            Mensaje.Subject = "PRueba de correo"
            Mensaje.IsBodyHtml = True

            Dim strbody As String
            strbody = "<p>
<br></p><table style=""margin: 0px; border: medium; border-image: none; border-collapse: collapse;"" border=""1"" cellspacing=""0"" cellpadding=""0"">
 <tbody><tr style=""mso-yfti-irow0;mso-yfti-firstrow:yes;mso-yfti-lastrow:yes"">
  <td width=""299"" valign=""top"" style=""margin:  0px; padding: 0cm 5.4pt; border: 1px solid rgb(0, 0, 0); border-image: none; width: 224.45pt; background-color: transparent;"">
  <p style=""margin 0px; line-height: normal;""><span style=""margin: 0px;""><img width=""74"" height=""74"" src=""file:///C:/Users/martinem/AppData/Local/Temp/msohtmlclip1/01/clip_image002.png"" v:shapes=""Imagen_x0020_2""></span><span style=""margin: 0px;""><font face=""Calibri"">&nbsp;&nbsp;&nbsp;&nbsp; </font></span><b style=""mso-bidi-font-weight:normal""><span style=""margin:  0px; font-size: 14pt;"">GMX Seguros</span></b></p>
  <p style=""margin 0px; line-height: normal;""><font face=""Calibri"">Autorización de Órdenes de Pago.</font></p>
  <p style=""margin 0px; line-height: normal;""><font face=""Calibri"">&nbsp;</font></p>
  <p style=""margin 0px; line-height: normal;""><font face=""Calibri"">El </font><b style=""mso-bidi-font-weight:normal""><font face=""Calibri"">Solicitante Juan Pérez, </font></b><font face=""Calibri"">desea
  sean autorizadas las siguientes órdenes de pago:</font></p>
  <p style=""margin 0px; line-height: normal;""><font face=""Calibri"">&nbsp;</font></p>
  <table style=""margin 0px; border: medium; border-image: none; border-collapse: collapse;"" border=""1"" cellspacing=""0"" cellpadding=""0"">
   <tbody><tr style=""mso-yfti-irow0;mso-yfti-firstrow:yes"">
    <td width=""142"" valign=""top"" style=""margin:  0px; padding: 0cm 5.4pt; border: 1px solid rgb(0, 0, 0); border-image: none; width: 106.45pt; background-color: transparent;"">
    <p style=""margin 0px; line-height: normal;""><font face=""Calibri"">5163</font></p>
    </td>
    <td width=""142"" valign=""top"" style=""border-width 1px 1px 1px 0px; border-style: solid solid solid none; border-color: RGB(0, 0, 0); margin: 0px; padding: 0cm 5.4pt; border-image: none; width: 106.45pt; background-color: transparent;"">
    <p style=""margin 0px; line-height: normal;""><font face=""Calibri"">Orden de pago 1…</font></p>
    </td>
   </tr>
   <tr style=""mso-yfti-irow1"">
    <td width=""142"" valign=""top"" style=""border-width 0px 1px 1px; border-style: none solid solid; border-color: RGB(0, 0, 0); margin: 0px; padding: 0cm 5.4pt; border-image: none; width: 106.45pt; background-color: transparent;"">
    <p style=""margin 0px; line-height: normal;""><font face=""Calibri"">6132</font></p>
    </td>
    <td width=""142"" valign=""top"" style=""border-width 0px 1px 1px 0px; border-style: none solid solid none; border-color: RGB(0, 0, 0); margin: 0px; padding: 0cm 5.4pt; width: 106.45pt; background-color: transparent;"">
    <p style=""margin 0px; line-height: normal;""><font face=""Calibri"">Orden de pago 2…</font></p>
    </td>
   </tr>
   <tr style=""mso-yfti-irow2;mso-yfti-lastrow:yes"">
    <td width=""142"" valign=""top"" style=""border-width:  0px 1px 1px; border-style: none solid solid; border-color: RGB(0, 0, 0); margin: 0px; padding: 0cm 5.4pt; border-image: none; width: 106.45pt; background-color: transparent;"">
    <p style=""margin 0px; line-height: normal;""><font face=""Calibri"">7899</font></p>
    </td>
    <td width=""142"" valign=""top"" style=""border-width 0px 1px 1px 0px; border-style: none solid solid none; border-color: RGB(0, 0, 0); margin: 0px; padding: 0cm 5.4pt; width: 106.45pt; background-color: transparent;"">
    <p style=""margin 0px; line-height: normal;""><font face=""Calibri"">Orden de pago 3…</font></p>
    </td>
   </tr>
  </tbody></table>
  <p style=""margin 0px; line-height: normal;""><font face=""Calibri"">&nbsp;</font></p>
  <p style=""margin 0px; line-height: normal;""><font face=""Calibri"">&nbsp;</font></p>
  <p style=""margin 0px; line-height: normal;""><font face=""Calibri"">&nbsp;</font></p>
  <p style=""margin 0px; line-height: normal;""><font face=""Calibri"">&nbsp;</font></p>
  </td>
  <td width=""299"" valign=""top"" style=""background RGB(15, 36, 62); border-width: 1px 1px 1px 0px; border-style: solid solid solid none; border-color: RGB(0, 0, 0); margin: 0px; padding: 0cm 5.4pt; border-image: none; width: 224.45pt;"">
  <p align=""center"" style=""margin 0px; text-align: center; line-height: normal;""><img width=""62"" height=""62"" src=""file:///C:/Users/martinem/AppData/Local/Temp/msohtmlclip1/01/clip_image004.png"" v:shapes=""_x0000_i1025""></p>
  </td>
 </tr>
</tbody></table><p>

<br></p>"

            Mensaje.Body = strbody
            Mensaje.BodyEncoding = System.Text.Encoding.UTF8
            Mensaje.Priority = MailPriority.Normal
            Dim cli As New SmtpClient
            cli.Port = ConfigurationManager.AppSettings("SMTPPort")
            cli.Host = ConfigurationManager.AppSettings("SMTPServer")
            '.Credentials = CredentialCache.DefaultNetworkCredentials
            cli.Credentials = New System.Net.NetworkCredential("martinem@gmx.com.mx", "GMXSt33l")
            cli.EnableSsl = False

            cli.Send(Mensaje)
            Return True
        Catch ex As Exception
            MsgBox(ex.Message, vbExclamation, "error")
            Return False
        End Try

    End Function
#End Region

End Class
