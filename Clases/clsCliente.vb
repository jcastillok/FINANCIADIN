Imports System.Data.SqlClient

Public Class clsCliente

#Region "Datos Personales"
    Public Property Id As Integer
    Public Property PrimNombre As String
    Public Property SegNombre As String
    Public Property PrimApellido As String
    Public Property SegApellido As String
    Public Property Tipo As String
    Public Property RFC As String
    Public Property CURP As String
    Public Property Email As String
    Public Property FecNac As DateTime
    Public Property Pais As String
    Public Property Localidad As String
    Public Property Nacionalidad As String
    Public Property Sexo As String 'Nuevo
    Public Property PuestoLaboral As String 'Nuevo
    Public Property Ocupacion As String
    Public Property Celular As String
    Public Property EstadoCivil As String
    Public Property NombresConyuge As String  'Nuevo
    Public Property PrimApellidoConyuge As String  'Nuevo
    Public Property SegApellidoConyuge As String  'Nuevo
    Public Property FecNacConyuge As DateTime 'Nuevo
    Public Property Identificacion As Byte()
    Public Property ComprobanteDom As Byte()
    Public Property SolBuro As Byte()
    Public Property Id_Aval As Integer
    Public Property EsAval As Boolean
    Public Property IngresoMensual As Double
    'Public Property Id_Empleo As Integer? 'Nuevo
    'Public Property Id_Negocio As Integer? 'Nuevo
#End Region

#Region "Domicilio"
    Public Property Calle As String
    Public Property NumExt As String
    Public Property NumInt As String
    Public Property Colonia As String
    Public Property CodPostal As Integer
    Public Property LocalidadDomicilio As String 'Nuevo
    Public Property PaisDomicilio As String 'Nuevo
    Public Property EstadoDomicilio As String 'Renombrado
    Public Property MunicipioDomicilio As String 'Renombrado
    Public Property AntigEnDomicilio As Integer 'Nuevo
    Public Property TelefonoDomicilio As String 'Renombrado
    Public Property ReferenciaDomicilio As String 'Renombrado
#End Region

#Region "Empleo"
    Public Property TipoContratoLaboral As String
    Public Property AntigEmpleo As Integer
    Public Property AntigEmpAnterior As Integer
#End Region

#Region "Datos de Control"
    Public Property FecRegistro As DateTime
    Public Property Login As String
#End Region

    Public Shared Function obtener(ID As Integer) As clsCliente
        Dim clCliente As New clsCliente
        Dim StrSelect As String = "SELECT *" _
        & " FROM [CLIENTES]" _
        & " WHERE Id = " & ID & ""
        Dim conexion As String = clsConexion.ConnectionString
        Using Con As New SqlConnection(conexion)
            Con.Open()
            Using Com As New SqlCommand(StrSelect, Con)
                Using rst = Com.ExecuteReader()
                    If rst.HasRows Then
                        While rst.Read
                            clCliente.Id = rst("Id")
                            clCliente.PrimNombre = rst("PrimNombre")
                            clCliente.SegNombre = rst("SegNombre")
                            clCliente.PrimApellido = rst("PrimApellido")
                            clCliente.SegApellido = rst("SegApellido")
                            clCliente.Tipo = rst("Tipo")
                            clCliente.RFC = rst("RFC")
                            clCliente.CURP = rst("CURP")
                            clCliente.Email = rst("Email")
                            clCliente.FecNac = rst("FecNac")
                            clCliente.Pais = rst("Pais")
                            clCliente.Localidad = rst("Localidad")
                            clCliente.Nacionalidad = rst("Nacionalidad")
                            clCliente.Sexo = If(IsDBNull(rst("Sexo")), "", rst("Sexo"))
                            clCliente.PuestoLaboral = If(IsDBNull(rst("PuestoLaboral")), "", rst("PuestoLaboral"))
                            clCliente.Ocupacion = rst("Ocupacion")
                            clCliente.Celular = rst("Celular")
                            clCliente.EstadoCivil = rst("EstadoCivil")
                            clCliente.NombresConyuge = If(IsDBNull(rst("NombresConyuge")), "", rst("NombresConyuge"))
                            clCliente.PrimApellidoConyuge = If(IsDBNull(rst("PrimApellidoConyuge")), "", rst("PrimApellidoConyuge"))
                            clCliente.SegApellidoConyuge = If(IsDBNull(rst("SegApellidoConyuge")), "", rst("SegApellidoConyuge"))
                            clCliente.FecNacConyuge = If(IsDBNull(rst("FecNacConyuge")), Nothing, rst("FecNacConyuge"))
                            clCliente.Identificacion = If(IsDBNull(rst("Identificacion")), Nothing, rst("Identificacion"))
                            clCliente.ComprobanteDom = If(IsDBNull(rst("ComprobanteDom")), Nothing, rst("ComprobanteDom"))
                            clCliente.SolBuro = If(IsDBNull(rst("SolBuro")), Nothing, rst("SolBuro"))
                            'clCliente.Id_Aval = rst("Id_Aval")
                            clCliente.EsAval = rst("EsAval")
                            clCliente.IngresoMensual = rst("IngresoMensual")

                            clCliente.Calle = rst("Calle")
                            clCliente.NumExt = rst("NumExt")
                            clCliente.NumInt = If(IsDBNull(rst("NumInt")), "", rst("NumInt"))
                            clCliente.Colonia = rst("Colonia")
                            clCliente.CodPostal = rst("CodPostal")
                            clCliente.LocalidadDomicilio = If(IsDBNull(rst("LocalidadDom")), "", rst("LocalidadDom"))
                            clCliente.PaisDomicilio = If(IsDBNull(rst("PaisDom")), "", rst("PaisDom"))
                            clCliente.EstadoDomicilio = rst("EstadoDom")
                            clCliente.MunicipioDomicilio = rst("MunicipioDom")
                            clCliente.AntigEnDomicilio = If(IsDBNull(rst("AntigEnDom")), 0, rst("AntigEnDom"))
                            clCliente.TelefonoDomicilio = rst("TelefonoDom")
                            clCliente.ReferenciaDomicilio = rst("ReferenciaDom")

                            clCliente.TipoContratoLaboral = If(IsDBNull(rst("TipoContratoLaboral")), "", rst("TipoContratoLaboral"))
                            clCliente.AntigEmpleo = If(IsDBNull(rst("AntigEmpleo")), 0, rst("AntigEmpleo"))
                            clCliente.AntigEmpAnterior = If(IsDBNull(rst("AntigEmpAnt")), 0, rst("AntigEmpAnt"))

                            clCliente.FecRegistro = rst("FecRegistro")
                            clCliente.Login = rst("Login")
                        End While
                    End If
                End Using
            End Using
        End Using
        Return clCliente
    End Function

    Public Shared Function insertar(cliente As clsCliente) As Boolean
        Dim strIns As String = String.Format("INSERT INTO CLIENTES (PrimNombre,SegNombre,PrimApellido,SegApellido,Tipo,RFC,CURP,
                               Email,FecNac,Pais,Localidad,Nacionalidad,Sexo,PuestoLaboral,Ocupacion,Celular,EstadoCivil,NombresConyuge,
                               PrimApellidoConyuge,SegApellidoConyuge,FecNacConyuge,EsAval,IngresoMensual,Calle,NumExt,NumInt,
                               Colonia,CodPostal,LocalidadDom,PaisDom,EstadoDom,MunicipioDom,AntigEnDom,TelefonoDom,ReferenciaDom,
                               FecRegistro,Login,AntigEmpleo,AntigEmpAnt,TipoContratoLaboral,Estatus)
                               VALUES ('{0}','{1}','{2}','{3}',{4},'{5}','{6}','{7}',convert(datetime,'{8}',103),'{9}','{10}','{11}',
                               '{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}',{20},'{21}',{22},
                               '{23}','{24}','{25}','{26}',{27},'{28}','{29}','{30}','{31}',{32},'{33}','{34}',{35},'{36}',{37},{38},'{39}',{40})",
                               cliente.PrimNombre, cliente.SegNombre, cliente.PrimApellido, cliente.SegApellido, cliente.Tipo, cliente.RFC,
                               cliente.CURP, If(IsNothing(cliente.Email), "NULL", cliente.Email), cliente.FecNac.ToShortDateString(), cliente.Pais,
                               cliente.Localidad, cliente.Nacionalidad, cliente.Sexo, cliente.PuestoLaboral, cliente.Ocupacion, cliente.Celular, cliente.EstadoCivil,
                               cliente.NombresConyuge, cliente.PrimApellidoConyuge, cliente.SegApellidoConyuge, If(cliente.FecNacConyuge.Year = 1, "NULL", "convert(datetime," & cliente.FecNacConyuge.ToShortDateString() & ",103)"), cliente.EsAval,
                               cliente.IngresoMensual, cliente.Calle, cliente.NumExt, If(IsNothing(cliente.NumInt) Or cliente.NumInt = "", "NULL", cliente.NumInt), cliente.Colonia, cliente.CodPostal, cliente.LocalidadDomicilio,
                               cliente.PaisDomicilio, cliente.EstadoDomicilio, cliente.MunicipioDomicilio, cliente.AntigEnDomicilio, cliente.TelefonoDomicilio,
                               cliente.ReferenciaDomicilio, "GETDATE()", cliente.Login, cliente.AntigEmpleo, cliente.AntigEmpAnterior, cliente.TipoContratoLaboral, 1)
        Return Utilerias.setUpdInsDel(strIns)
    End Function

    Public Shared Function actualizar(cliente As clsCliente) As Boolean
        Dim strUpdt As String
        strUpdt = " UPDATE CLIENTES" _
            & " SET PrimNombre = '" & cliente.PrimNombre & "', SegNombre = '" & cliente.SegNombre & "', PrimApellido = '" & cliente.PrimApellido & "'," _
            & " SegApellido = '" & cliente.SegApellido & "', Tipo = " & cliente.Tipo & ", RFC = '" & cliente.RFC & "', CURP = '" & cliente.CURP & "'," _
            & " Email = '" & If(IsNothing(cliente.Email), "NULL", cliente.Email) & "', FecNac = convert(datetime,'" & cliente.FecNac.ToShortDateString() & "',103), Pais = '" & cliente.Pais & "'," _
            & " Localidad = '" & cliente.Localidad & "', Nacionalidad = '" & cliente.Nacionalidad & "', Sexo = '" & cliente.Sexo & "', PuestoLaboral = '" & cliente.PuestoLaboral & "'," _
            & " Ocupacion = '" & cliente.Ocupacion & "', Celular = '" & cliente.Celular & "', EstadoCivil = '" & cliente.EstadoCivil & "', NombresConyuge = '" & cliente.NombresConyuge & "'," _
            & " PrimApellidoConyuge = '" & cliente.PrimApellidoConyuge & "', SegApellidoConyuge = '" & cliente.SegApellidoConyuge & "', FecNacConyuge = " & If(cliente.FecNacConyuge.Year = 1, "NULL", "convert(datetime,'" & cliente.FecNacConyuge.ToShortDateString() & "',103)") & "," _
            & " EsAval = '" & cliente.EsAval & "', IngresoMensual = " & cliente.IngresoMensual & ", Calle = '" & cliente.Calle & "', NumExt = '" & cliente.NumExt & "'," _
            & " NumInt = '" & cliente.NumInt & "', Colonia = '" & cliente.Colonia & "', CodPostal = " & cliente.CodPostal & ", LocalidadDom = '" & cliente.LocalidadDomicilio & "'," _
            & " PaisDom = '" & cliente.PaisDomicilio & "', EstadoDom = '" & cliente.EstadoDomicilio & "', MunicipioDom = '" & cliente.MunicipioDomicilio & "'," _
            & " AntigEnDom = " & cliente.AntigEnDomicilio & ", TelefonoDom = '" & cliente.TelefonoDomicilio & "', ReferenciaDom = '" & cliente.ReferenciaDomicilio & "'," _
            & " AntigEmpleo= " & cliente.AntigEmpleo & ", AntigEmpAnt = " & cliente.AntigEmpAnterior & ", TipoContratoLaboral = '" & cliente.TipoContratoLaboral & "'" _
            & " WHERE (ID = " & cliente.Id & ") "
        Return Utilerias.setUpdInsDel(strUpdt)
    End Function

    Public Function curpRepetido() As Boolean
        Dim StrSelect As String
        If Me.Id <> 0 Then
            StrSelect = " SELECT * FROM CLIENTES" _
                      & " WHERE (CURP = '" & Me.CURP & "') AND (ID <> " & Me.Id & ")"
        Else
            StrSelect = " SELECT * FROM CLIENTES" _
                      & " WHERE (CURP = '" & Me.CURP & "')"
        End If
        Dim conexion As String = clsConexion.ConnectionString
        Using Con As New SqlConnection(conexion)
            Con.Open()
            Using Com As New SqlCommand(StrSelect, Con)
                Using rst = Com.ExecuteReader()
                    Return rst.HasRows
                End Using
            End Using
        End Using
    End Function

    Public Shared Function guardarDocumentos(cliente As clsCliente) As Boolean

        If (IsNothing(cliente.Identificacion)) And (IsNothing(cliente.ComprobanteDom)) And (IsNothing(cliente.SolBuro)) Then
            Return False
        End If

        Dim strUpdt As String
        strUpdt = "UPDATE CLIENTES SET" & If(IsNothing(cliente.Identificacion), "", " Identificacion = @Identificacion,") & "" _
            & " " & If(IsNothing(cliente.ComprobanteDom), "", " ComprobanteDom = @ComprobanteDom,") & "" _
            & " " & If(IsNothing(cliente.SolBuro), "", " SolBuro = @SolBuro,") & "" _
            & " WHERE (CURP = '" & cliente.CURP & "') "
        strUpdt = strUpdt.Remove(strUpdt.LastIndexOf(","), 1)
        Dim conexion As String = clsConexion.ConnectionString
        Using Con As New SqlConnection(conexion)
            Con.Open()
            Try
                Dim Identificacion As New SqlParameter
                Dim ComprobanteDom As New SqlParameter
                Dim SolBuro As New SqlParameter
                If IsNothing(cliente.Identificacion) = False Then
                    Identificacion.SqlDbType = SqlDbType.VarBinary
                    Identificacion.ParameterName = "Identificacion"
                    Identificacion.Value = cliente.Identificacion
                End If
                If IsNothing(cliente.ComprobanteDom) = False Then
                    ComprobanteDom.SqlDbType = SqlDbType.VarBinary
                    ComprobanteDom.ParameterName = "ComprobanteDom"
                    ComprobanteDom.Value = cliente.ComprobanteDom
                End If
                If IsNothing(cliente.SolBuro) = False Then
                    SolBuro.SqlDbType = SqlDbType.VarBinary
                    SolBuro.ParameterName = "SolBuro"
                    SolBuro.Value = cliente.SolBuro
                End If
                Dim cmd As New SqlCommand(strUpdt, Con)
                If IsNothing(cliente.Identificacion) = False Then cmd.Parameters.Add(Identificacion)
                If IsNothing(cliente.ComprobanteDom) = False Then cmd.Parameters.Add(ComprobanteDom)
                If IsNothing(cliente.SolBuro) = False Then cmd.Parameters.Add(SolBuro)
                cmd.ExecuteNonQuery()
                Con.Close()
                Return True
            Catch ex As Exception
                Con.Close()
                Return False
            End Try
        End Using
    End Function
End Class
