' NOTE: You can use the "Rename" command on the context menu to change the interface name "Generales" in both code and config file together.
<ServiceContract()>
Public Interface Generales

#Region "Ordenes de Pago"

#Region "Polizas"
    ' TODO: Add your service operations here
    <OperationContract()>
    Function ObtieneAclaraciones(id_pv As Integer) As String

#End Region
#End Region

#Region "Utilidades"
#Region "EnvioCorreos"
    <OperationContract()>
    Function EnviaCorreo(strTo As String, strBody As String, strSubject As String, Optional strCc As String = vbNullString, Optional strBco As String = vbNullString) As Boolean
#End Region

#End Region

#Region "Firma Electronica"
    <OperationContract()>
    Function ActualizaFirma(NumOp As String, TipoPer As Integer, CodUsu As String) As Integer

    <OperationContract()>
    Function ObtieneUsuarioFirmaE(TipoUsuario As Integer) As List(Of spS_UsuarioFirma_Result1)

    <OperationContract()>
    Function ObtienePermisosXUsu(CodUsu As String) As List(Of spS_PermisosxUSuFirma_Result)
#End Region

End Interface

' Use a data contract as illustrated in the sample below to add composite types to service operations.

<DataContract()>
Public Class CompositeType

    <DataMember()>
    Public Property BoolValue() As Boolean

    <DataMember()>
    Public Property StringValue() As String

End Class
