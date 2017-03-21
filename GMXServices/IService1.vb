' NOTE: You can use the "Rename" command on the context menu to change the interface name "Generales" in both code and config file together.
<ServiceContract()>
Public Interface Generales

#Region "Polizas"
    ' TODO: Add your service operations here
    <OperationContract()>
    Function ObtieneAclaraciones(id_pv As Integer) As String

#End Region

#Region "Usuarios"
    <OperationContract()>
    Function ObtieneUsuarioFirmaE(TipoUsuario As Integer) As List(Of spS_UsuarioFirma_Result)
#End Region

#Region "EnvioCorreos"
    <OperationContract()>
    Function EnviaCorreo() As Boolean
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
